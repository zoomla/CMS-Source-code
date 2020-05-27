using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Helper.Addon;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.SQLDAL;
using ZoomLa.SQLDAL.SQL;

namespace ZoomLaCMS.Tools
{
    public partial class ImportData : System.Web.UI.Page
    {
        B_Model modBll = new B_Model();
        //*给定的 ColumnMapping 与源或目标中的任意列均不匹配。
        //1,字段大小写问题,其字段名是大小写敏感的
        //2,dt中列不存在,或数据库中无该列
        //*如果需要插入主键,则需要关掉SQL中的标识,否则其会自动跳过

        //MYSQL与SQL对拷
        //string targetcon = "Data Source=(local);Initial Catalog=For312;User ID=test;Password=test";
        //string win100con = "Data Source=192.168.1.111;Initial Catalog=bbs_zoomla;User ID=test;Password=test";
        //添加的用户,主机必须是需要远程连接到Mysql的主机,如我的机器,则主机必须是win01或IP地址
        //string sourceCon = "Server=192.168.1.101;Database=hbncw;Uid=test;Pwd=test;";
        //string targetCon = @"Data Source=(local);Initial Catalog=test;User ID=test;Password=test";
        string targetCon="Data Source=win22;Initial Catalog=jxysedu;User ID=jxysedu_f;Password=jxysedu_password2006";
        //详细的MYSQL导出可见 312文件
        SqlBase access = SqlBase.CreateHelper("access");
         
        //1,清空Node表,取消主键
        //2,为model与对应的附表增加模型字段和ArticleID
        //3,为对应的增加视图
        //4,执行
        //5,为node设定主键
        protected void Page_Load(object sender, EventArgs e)
        {
            B_Admin.CheckIsLogged(Request.RawUrl);
            M_AdminInfo adminMod=B_Admin.GetLogin();
            if (!adminMod.IsSuperAdmin()) { function.WriteErrMsg("无权访问该页面"); return; }
            //access.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=D:\old.accdb;User Id=;Password=;";
            //DBCenter.DB.ConnectionString = targetCon;
            //------------------用户拷贝(注意是否要关闭UserID自动生成)
            //AddUsers();
            //------------------用户拷贝()
            //AddGuestBar();
            //------------------内容文章
            //Begin_Btn_Click(null,null);
            if (!IsPostBack)
            {
                DataTable modDT = modBll.GetModel("内容模型");
                Model_DP.DataSource = modDT;
                Model_DP.DataBind();
            }

        }
        protected void Node_Btn_Click(object sender, EventArgs e)
        {
            PageSetting setting = PageSetting.Single(1, int.MaxValue, "PE_Class", "ClassID", "");
            DataTable dt = access.SelPage(setting);
            dt.Columns["ClassID"].ColumnName = "NodeID";
            dt.Columns["ClassName"].ColumnName = "NodeName";
            dt.Columns["ParentID"].ColumnName = "ParentID";
            CopyNodes(dt);
            function.WriteSuccessMsg("拷入完成");
        }
        protected void Article_Btn_Click(object sender, EventArgs e)
        {
            PageSetting setting = PageSetting.Single(1, int.MaxValue, "PE_Article", "ArticleID", "");
            DataTable dt = access.SelPage(setting);
            M_ModelInfo modMod = modBll.SelReturnModel(2);
            //PE_Article,PE_Announce,PE_Class
            //节点--内容--用户
            dt.Columns["Title"].ColumnName = "Title";
            dt.Columns["Inputer"].ColumnName = "Inputer";//添加人,有很多null值,需要做null判断
            dt.Columns["CreateTime"].ColumnName = "CreateTime";
            dt.Columns["UpDateTime"].ColumnName = "UpDateTime";
            dt.Columns["ClassID"].ColumnName = "NodeID";
            dt.Columns["ArticleID"].ColumnName = "ArticleID"; ;//来源ID,用于后期还有数据需要对比获取
            dt.Columns.Add(new DataColumn("ModelID", typeof(int)));
            dt.Columns.Add(new DataColumn("TableName", typeof(string)));
            if (!dt.Columns.Contains("Status")) { dt.Columns.Add(new DataColumn("Status", typeof(int))); }
            dt.Columns.Add(new DataColumn("ItemID", typeof(int)));//读返回值
            dt.Columns["Hits"].ColumnName = "Hits";
            dt.Columns["DefaultPicUrl"].ColumnName = "TopImg";//预览图
            //---------从表数据
            dt.Columns["Content"].ColumnName = "content";
            dt.Columns["UploadFiles"].ColumnName = "uploadfiles";//上传文件(需要关联字段)
            dt.Columns["Author"].ColumnName = "author";//实际作者
            dt.Columns["Intro"].ColumnName = "synopsis";//简介
            dt.Columns["CopyFrom"].ColumnName = "source";//c
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                dr["ModelID"] = modMod.ModelID;
                dr["TableName"] = modMod.TableName;
                dr["ItemID"] = 0;
                dr["Status"] = 99;
                if (string.IsNullOrEmpty(DataConvert.CStr(dr["Inputer"]))) { dr["Inputer"] = "empty"; }
                string[] pics = DataConvert.CStr(dr["UploadFiles"]).Split("|".ToCharArray(),StringSplitOptions.RemoveEmptyEntries);
                if (pics.Length > 0)
                {
                    string pic_result = "";
                    foreach (string pic in pics)
                    {
                        pic_result += "{\"url\":\"Article/" + pic + "\",\"desc\":\"\"},";
                    }
                    pic_result = "[" + pic_result.Trim(',') + "]";
                    dr["UploadFiles"] = pic_result;
                }
               
            }
            //与数据库大小写必须一致
            string[] mfields = "Title|Inputer|CreateTime|UpDateTime|NodeID|ModelID|TableName|Status|ItemID|TopImg|Hits|ArticleID".Split('|');
            string[] subfields = "author|content|source|ArticleID|synopsis|uploadfiles".Split('|');
            CopyArticle(dt, mfields, modMod.TableName, subfields);
        }
        protected void Photo_Btn_Click(object sender, EventArgs e)
        {
            PageSetting setting = PageSetting.Single(1, int.MaxValue, "PE_Photo", "PhotoID", "");
            DataTable dt = access.SelPage(setting);
            M_ModelInfo modMod = modBll.SelReturnModel(3);
            //PE_Article,PE_Announce,PE_Class
            //节点--内容--用户
            dt.Columns["PhotoName"].ColumnName = "Title";
            dt.Columns["Inputer"].ColumnName = "Inputer";//添加人,有很多null值,需要做null判断
            dt.Columns["CreateTime"].ColumnName = "CreateTime";
            dt.Columns["UpDateTime"].ColumnName = "UpDateTime";
            dt.Columns["ClassID"].ColumnName = "NodeID";
            dt.Columns["PhotoID"].ColumnName = "ArticleID"; ;//来源ID,用于后期还有数据需要对比获取
            dt.Columns.Add(new DataColumn("ModelID", typeof(int)));
            dt.Columns.Add(new DataColumn("TableName", typeof(string)));
            if (!dt.Columns.Contains("Status")) { dt.Columns.Add(new DataColumn("Status", typeof(int))); }
            dt.Columns.Add(new DataColumn("ItemID", typeof(int)));//读返回值
            dt.Columns["Hits"].ColumnName = "Hits";
            dt.Columns["PhotoThumb"].ColumnName = "TopImg";//预览图
            //---------从表数据
            //图片地址3|200710/20071016163946346.jpg$$$图片地址2|200710/20071016164226965.jpg$$$图片地址3|200710/20071016164226498.jpg$$$图片地址4|201006/20100604112746823.jpg
            //[{"url":"http://www.z01.com/skin/default/productshow1.jpg","desc":"产品展示1"},{"url":"https://www.z01.com/skin/default/productshow2.jpg","desc":"产品展示2"},{"url":"https://www.z01.com/skin/default/productshow3.jpg","desc":"产品展示3"},{"url":"https://www.z01.com/skin/default/productshow4.jpg","desc":"产品展示4"},{"url":"https://www.z01.com/skin/default/productshow5.jpg","desc":"产品展示5"}]
            dt.Columns["PhotoUrl"].ColumnName = "PhotoUrl";
            dt.Columns["Author"].ColumnName = "author";//实际作者
            dt.Columns["PhotoIntro"].ColumnName = "content";//简介
            dt.Columns["CopyFrom"].ColumnName = "source";//c
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                dr["ModelID"] = modMod.ModelID;
                dr["TableName"] = modMod.TableName;
                dr["ItemID"] = 0;
                dr["Status"] = 99;
                if (string.IsNullOrEmpty(DataConvert.CStr(dr["Inputer"]))) { dr["Inputer"] = "empty"; }
                string[] pics = Regex.Split(DataConvert.CStr(dr["PhotoUrl"]), Regex.Escape("$$$"));
                string pic_result = "";
                foreach (string pic in pics)
                {
                    string desc = pic.Split('|')[0];
                    string url = "Photo/"+pic.Split('|')[1];
                    pic_result += "{\"url\":\"" + url + "\",\"desc\":\"" + desc + "\"},";
                }
                pic_result = "[" + pic_result.Trim(',') + "]";
                dr["PhotoUrl"] = pic_result;
            }
            //与数据库大小写必须一致
            string[] mfields = "Title|Inputer|CreateTime|UpDateTime|NodeID|ModelID|TableName|Status|ItemID|Hits|ArticleID".Split('|');
            string[] subfields = "author|source|ArticleID|content|PhotoUrl".Split('|');
            CopyArticle(dt, mfields, modMod.TableName, subfields);
        }
        //拷贝节点
        //将指定的数据批量拷入,被指定的数据先将[列名]改好再开始导
        private void CopyArticle(DataTable dt, string[] mfields, string subTbName, string[] subfields)
        {
            using (SqlBulkCopy bulk = new SqlBulkCopy(targetCon))
            {
                bulk.DestinationTableName = "ZL_CommonModel";
                bulk.BatchSize = 1000;
                foreach (string field in mfields)
                {
                    if (!dt.Columns.Contains(field)) { continue; }
                    bulk.ColumnMappings.Add(field, field);
                }
                bulk.WriteToServer(dt);
            }
            using (SqlBulkCopy bulk = new SqlBulkCopy(targetCon))
            {
                bulk.BatchSize = 1000;
                bulk.DestinationTableName = subTbName;
                foreach (string field in subfields)
                {
                    if (!dt.Columns.Contains(field)) { continue; }
                    bulk.ColumnMappings.Add(field, field);
                }
                bulk.WriteToServer(dt);
            }
            //建立视图，然后执行语句   update 视图名 set itemid=id
            //Create View ZL_Article AS SELECT A.GeneralID,A.ItemID,A.TableName,B.ArticleID,B.ID FROM ZL_CommonModel A LEFT JOIN ZL_C_Article B ON a.ArticleID=B.ArticleID
            //改为临时创建一个
            SqlHelper.ExecuteTable(targetCon, CommandType.Text, "Update ZL_Photo Set ItemID=ID WHERE ItemID=0");
            function.WriteErrMsg("操作完成!");
        }
        /// <summary>
        /// 添加节点数据进表,再依表生成xml
        /// 注意:必须将NodeID设为可插入值的状态,并且修改PK
        /// </summary>
        public void CopyNodes(DataTable dt)
        {
            B_Node bll = new B_Node();//需先关闭主键,与M_Node中主键标识
            foreach (DataRow dr in dt.Rows)
            {
                //DataTable NodeName, NodeDir;
                //NodeName = bll.GetNodeForNodeName(TxtNodeName.Text, DataConverter.CLng(HdnDepth.Value), DataConverter.CLng(Request.QueryString["ParentID"]));
                //NodeDir = bll.GetNodeForDirname(TxtNodeDir.Text, DataConverter.CLng(HdnDepth.Value), DataConverter.CLng(Request.QueryString["ParentID"]));
                //HttpResponse.RemoveOutputCacheItem(customPath2 + "Content/NodeTree.aspx");
                //if (NodeName.Rows.Count > 0 || NodeDir.Rows.Count > 0) { function.Script(this, "alert('发现同栏目下栏目名或标识名重复，请点击确定重新添加节点');"); return; }
                /*-------------------------------------------------------------------------------------------------*/
                M_Node node = new M_Node();
                node.NodeID = Convert.ToInt32(dr["NodeID"]);
                node.NodeName = dr["NodeName"].ToString();
                node.NodeType = 1;
                node.NodePic = "";
                node.NodeDir = PinYin.GetFirstPinYin(node.NodeName);
                node.ParentID = DataConvert.CLng(dr["ParentID"]);
                node.Depth = node.ParentID == 0 ? 2 : 1;//后期更新下深度
                node.NodeUrl = "";
                node.OrderID = 1;
                node.Tips = "";
                node.Description = "";
                node.Meta_Keywords = "";
                node.Meta_Description = "";
                node.OpenNew = false;
                node.ItemOpenType = false;
                node.PurviewType = false;
                node.CommentType = "1";
                node.HitsOfHot = 0;
                node.Viewinglimit = "";

                node.ConsumePoint = 0;
                node.ConsumeType = 0;
                node.ConsumeTime = 0;
                node.ConsumeCount = 0;
                node.Shares = 0;
                node.OpenTypeTrue = "0";
                node.ItemOpenTypeTrue = "0";
                node.Custom = "";
                node.NodeListUrl = "";
                node.SiteConfige = "";
                /////////////////////////////////////////////////////
                node.NodeListType = 1;
                node.ListPageHtmlEx = 3;
                node.ContentFileEx = 3;

                node.ListPageEx = 3;
                node.LastinfoPageEx = 3;
                node.HotinfoPageEx = 3;
                node.ProposePageEx = 3;

                node.SafeGuard = 0;
                node.ContentPageHtmlRule = 2;
                node.HtmlPosition = 1;
                node.Contribute = 0;
                node.SiteContentAudit = 99;
                node.Purview = "<View>allUser</View><ViewGroup>-1,1,2</ViewGroup><ViewSunGroup>-1,1,2</ViewSunGroup><input>-1,1,2</input><forum>1,2</forum>";
                node.PK = "";
                DBCenter.Insert(node);
            }
        }
        public void AddUsers()
        {
            //string sql = "SELECT *,'test1' AS Question,'fasfafwtas' AS Answer,1 AS GroupID FROM ZL_User";
            //DataTable dt = MySqlHelper.ExecuteTable(sourceCon, CommandType.Text, sql, null);
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    DataRow dr = dt.Rows[i];
            //    if (dr["EMAIL"] == DBNull.Value || string.IsNullOrEmpty(dr["EMAIL"].ToString()))
            //    { dr["EMAIL"] = "test123@foxmail.com"; }
            //}
            //using (SqlBulkCopy bulk = new SqlBulkCopy(targetCon))
            //{
            //    bulk.BatchSize = 1000;
            //    bulk.DestinationTableName = "ZL_User";
            //    bulk.ColumnMappings.Add("USER_ID", "UserID");
            //    bulk.ColumnMappings.Add("USERNAME", "UserName");
            //    bulk.ColumnMappings.Add("REALNAME", "HoneyName");
            //    bulk.ColumnMappings.Add("PASSWORD", "UserPwd");
            //    //bulk.ColumnMappings.Add("GROUP_ID", "GroupID");
            //    bulk.ColumnMappings.Add("EMAIL", "Email");
            //    bulk.ColumnMappings.Add("REGISTER_TIME", "RegTime");
            //    bulk.ColumnMappings.Add("REGISTER_IP", "RegisterIP");
            //    //-----默认值区
            //    bulk.ColumnMappings.Add("Question", "Question");
            //    bulk.ColumnMappings.Add("Answer", "Answer");
            //    bulk.ColumnMappings.Add("GroupID", "GroupID");
            //    bulk.WriteToServer(dt);
            //}
        }
        public void AddGuestBar()
        {
            DataTable dt = new DataTable();
            /*---------------------------贴吧,论坛数据导入-------------------------------------------------*/
            //---------------------栏目数据
            //dt = MySqlHelper.ExecuteTable(MySQLConnStr, CommandType.Text, "SELECT *,0 AS ParentID,1 AS GType,0 AS NeedLog,1 AS Status FROM bbs_forum", null);
            //using (SqlBulkCopy bulk = new SqlBulkCopy(targetcon))
            //{
            //    bulk.BatchSize = 1000;
            //    bulk.DestinationTableName = "ZL_Guestcate";
            //    bulk.ColumnMappings.Add("Forum_ID", "Cateid");//bbs_formum
            //    bulk.ColumnMappings.Add("Title", "Catename");
            //    //默认值区
            //    bulk.ColumnMappings.Add("ParentID", "ParentID");//0
            //    bulk.ColumnMappings.Add("GType", "GType");//1
            //    bulk.ColumnMappings.Add("NeedLog", "NeedLog");//0
            //    bulk.ColumnMappings.Add("Status", "Status");//1
            //    bulk.WriteToServer(dt);
            //}

            //---------------------贴子数据
            //*formumid为0的是精品线路与游记攻略
            //dt = MySqlHelper.ExecuteTable(sourceCon, CommandType.Text, "SELECT *,0 AS ReplyID,0 AS ReplyUserID,0 AS Pid,1 AS Status,1 AS MsgType  FROM ZL_Guest_Bar2", null);
            using (SqlBulkCopy bulk = new SqlBulkCopy(targetCon))//bbs_topic
            {
                bulk.BatchSize = 1000;
                bulk.DestinationTableName = "ZL_Guest_Bar";
                bulk.ColumnMappings.Add("FORUM_ID", "CateID");//forum_id
                bulk.ColumnMappings.Add("Title", "Title");
                bulk.ColumnMappings.Add("POST_CONTENT", "MsgContent");
                //bulk.ColumnMappings.Add("POSTER_IP", "IP");
                bulk.ColumnMappings.Add("CREATE_TIME", "CDate");
                bulk.ColumnMappings.Add("CREATE_TIME", "R_CDate");
                bulk.ColumnMappings.Add("CREATER_ID", "CUser");
                bulk.ColumnMappings.Add("USERNAME", "CUName");
                //默认值区
                bulk.ColumnMappings.Add("ReplyID", "ReplyID");//0 
                bulk.ColumnMappings.Add("ReplyUserID", "ReplyUserID");//0 
                bulk.ColumnMappings.Add("Pid", "Pid");//0 默认主贴
                bulk.ColumnMappings.Add("Status", "Status");//1
                bulk.ColumnMappings.Add("MsgType", "MsgType");//1
                bulk.WriteToServer(dt);
            }
        }
        private void MySqlToSQL()
        {
            //zl_channel与zl_channel_ext等连接查询,有1000多个节点,需要导出后,再通过我们的方法创建节点,查找
            //请先在zl_C_Article与ZL_CommonModel中添加ArticleID
            //string sql = "Select ModelID={0},NodeID={1},TableName={2},ArticleID,Status=99,ItemID=0,Title,Editor,Abstract,CreateTime,SourceName,Editor,Content,Liability,PicLinks From RELEASELIB1 Where MASTERID={3} And Content IS NOT NULL";
            //string realsql = string.Format(sql, ModelID_T.Text, TNode_T.Text, "'ZL_C_Article'", SNode_T.Text);
            DataTable dt = new DataTable();
            //dt = SqlHelper.ExecuteTable(targetcon, CommandType.Text, realsql);
            //dt = MySqlHelper.ExecuteTable(sourceCon, CommandType.Text, "SELECT *,2 AS ModelID,'ZL_C_Article' AS TableName,99 AS Status,0 AS ItemID FROM ZL_Article", null);
            //CopyArticle(dt);
        }
}
}