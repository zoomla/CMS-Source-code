using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Helper;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.SQLDAL;


namespace ZoomLaCMS.Manage.APP
{
    public partial class ConPush : System.Web.UI.Page
    {
        /*
    * 用于312对接
    * 文章的话只允许文章模型,或必须在XML中描述好哪个字段为内容
    * 问答的NodeID为0
    */
        //1,我们的content是经过UrlEncode加过的,对方需要解一次
        //Title	Varchar(255)	否	标题	
        //Contents	Text	否	内容	
        //CateName	Varchar(100)	否	栏目名称	见附件1
        //Author	Varchar(50)	否	作者	
        //PublishDate	Datetime	否	发布时间	
        //ImgUrl	Varchar(255)	是	封面图	
        //Keyword	Varchar(500)	是	关键字	
        //CityName	Varchar(50)	否	地区名称	如湖北省，武汉市
        //Platform	Varchar(50)	否	平台CYT产业通DQT党群通	
        //ProductType	Varchar(50)		产业类型名称	见附件2
        private DataTable CurDT
        {
            get
            {
                return (DataTable)Session["ConPush_CurDT"];
            }
            set { Session["ConPush_CurDT"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            B_Admin.IsSuperManage();
            if (!IsPostBack)
            {
                MyBind();
                Call.HideBread(Master);
            }
        }
        public void MyBind()
        {
            DataTable configDT = GetConfig();
            string nodes = "";
            foreach (DataRow dr in configDT.Rows)
            {
                if (DataConvert.CLng(dr["NodeID"]) > 0)
                {
                    nodes += dr["NodeID"] + ",";
                }
            }
            nodes = nodes.TrimEnd(',');
            Nodes_Hid.Value = nodes;
            NodeRPT.DataSource = configDT;
            NodeRPT.DataBind();
            DataTable dt = SelFromArticle(nodes);
            dt.Merge(SelFromAsk());
            CurDT = InitDT(dt, configDT);
            EGV.DataSource = CurDT;
            EGV.DataBind();
        }
        public void ReBind()
        {
            DataTable dt = new DataTable();
            Nodes_Hid.Value = Nodes_Hid.Value.TrimEnd(',');
            if (string.IsNullOrEmpty(Nodes_Hid.Value)) { dt = null; }
            else
            {
                dt = SelFromArticle(Nodes_Hid.Value);
                if (Nodes_Hid.Value.Split(',').Contains("0"))
                { dt.Merge(SelFromAsk()); }
            }
            CurDT = InitDT(dt, GetConfig());
            EGV.DataSource = CurDT;
            EGV.DataBind();
        }
        private DataTable InitDT(DataTable dt, DataTable configDT)
        {
            //增加Index索引,增加CateName(依据NodeID)
            if (dt == null) return dt;
            if (!dt.Columns.Contains("Index"))//增加序号列 
            {
                dt.Columns.Add(new DataColumn("Index", typeof(int)));
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["Index"] = i;
            }
            //根据NodeID获取对应的CateName
            foreach (DataRow config in configDT.Rows)
            {
                DataRow[] drs = dt.Select("NodeID=" + config["NodeID"]);
                for (int i = 0; i < drs.Length; i++)
                {
                    drs[i]["CateName"] = config["T_CateName"];
                }
            }
            return dt;
        }
        protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            EGV.PageIndex = e.NewPageIndex;
            MyBind();
        }
        //XML文件的结构
        private DataTable GetStruct()
        {
            //取出生成的必须与接口需求的一致
            DataTable dt = new DataTable();
            dt.TableName = "Table";
            dt.Columns.Add(new DataColumn("Source", typeof(string)));//根据来源不同使用不同的方式获取数据
            dt.Columns.Add(new DataColumn("SourceAlias", typeof(string)));
            dt.Columns.Add(new DataColumn("NodeID", typeof(string)));
            dt.Columns.Add(new DataColumn("T_CateID", typeof(string)));//对应节点名
            dt.Columns.Add(new DataColumn("T_CateName", typeof(string)));//对应节点名
            return dt;
        }
        private DataTable GetConfig()
        {
            string xmlppath = Server.MapPath("/Config/ConPush.config");
            if (!File.Exists(xmlppath)) { throw new Exception("缺少配置文件,ConPush.config"); }
            DataSet ds = new DataSet();
            DataTable nodeDT = new DataTable();
            ds.ReadXml(xmlppath);
            return ds.Tables[0];
        }
        //catch
        public DataTable SelFromArticle(string nodes)
        {
            //B.Pic AS ImgUrl,
            if (string.IsNullOrEmpty(nodes)) return null;
            SafeSC.CheckIDSEx(nodes);
            DataTable dt = SqlHelper.JoinQuery("A.GeneralID AS ID,A.NodeID,Source='content',A.Title,B.Content AS Contents,CateName='',A.Inputer AS Author,A.CreateTime AS PublishDate,A.TagKey AS KeyWord,CityName='',PlatForm='DQT',ProductType=12", "ZL_CommonModel", "ZL_C_Article", "A.ItemID=B.ID", "(IsCatch=0 OR IsCatch Is Null) AND NodeID IN(" + nodes + ")", "A.CreateTime DESC");
            return dt;
        }
        public DataTable SelFromAsk()
        {
            string fields = "ID,NodeID=0,Source='ask',Qcontent AS Title,Supplyment AS Contents,CateName='',UserName AS Author,AddTime AS PublishDate,ImgUrl='',KeyWord='',CityName='',PlatForm='DQT',ProductType=12";
            string sql = "SELECT " + fields + " FROM ZL_Ask WHERE IsCatch=0 ORDER BY AddTime DESC";//需加IsCatch
            return SqlHelper.ExecuteTable(CommandType.Text, sql);
        }
        protected void PushSel_Btn_Click(object sender, EventArgs e)
        {
            //另外还需要加入来源判断吧,或重新生成Index直接从dt中取值
            if (!string.IsNullOrEmpty(Request.Form["idchk"]))
            {
                CurDT.DefaultView.RowFilter = "Index IN(" + Request.Form["idchk"] + ")";
                int count = CurDT.DefaultView.ToTable().Rows.Count;
                PushContent(CurDT.DefaultView.ToTable());
                ReBind();
                ScriptManager.RegisterStartupScript(LeftPanel, LeftPanel.GetType(), "", "<script>alert('推送完成,已" + count + "篇内容');</script>", false);
            }
        }
        protected void PushAll_Btn_Click(object sender, EventArgs e)
        {
            CurDT.DefaultView.RowFilter = "";
            int count = CurDT.Rows.Count;
            PushContent(CurDT);
            ReBind();
            ScriptManager.RegisterStartupScript(LeftPanel, LeftPanel.GetType(), "", "<script>alert('推送完成,已" + count + "篇内容');</script>", false);
        }
        protected void ReBind_Btn_Click(object sender, EventArgs e)
        {
            ReBind();
        }
        /// <summary>
        /// 推送文章
        /// </summary>
        /// <param name="dt">需要推送的文章dt</param>
        private void PushContent(DataTable dt)
        {
            if (dt == null || dt.Rows.Count < 0) return;
            HttpHelper http = new HttpHelper();
            HtmlHelper htmlHelp = new HtmlHelper();
            string[] strArr = new string[] { "http://chanye.hbncw.cn:8080/AddArticle.aspx|CYT", "http://dqt.hbncw.net:89/AddArticle.aspx|DQT" };
            foreach (string str in strArr)
            {
                string url = str.Split('|')[0];
                string plat = str.Split('|')[1];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    try
                    {
                        DataRow dr = dt.Rows[i];
                        Dictionary<string, object> param = new Dictionary<string, object>();
                        param.Add("Title", dr["Title"]);
                        string content = htmlHelp.ConvertImgUrl(dr["Contents"].ToString(), SiteConfig.SiteInfo.SiteUrl);
                        param.Add("Contents", HttpUtility.UrlEncode(content));//这里需要对方用UrlDecode
                        param.Add("CateName", dr["CateName"]);//目标栏目名称
                        param.Add("Author", dr["Author"]);
                        param.Add("PublishDate", dr["PublishDate"]);
                        param.Add("ImgUrl", dr["ImgUrl"]);
                        param.Add("Keyword", dr["Keyword"]);
                        param.Add("CityName", dr["CityName"]);
                        param.Add("Platform", dr["Platform"]);
                        param.Add("ProductType", plat);
                        HttpResult result = http.UploadParam(url, param);
                        if (!result.Html.Contains("-1"))
                        {
                            UpdateCatch(dr);
                        }
                    }
                    catch (Exception ex)
                    {
                        ScriptManager.RegisterStartupScript(LeftPanel, LeftPanel.GetType(), "", "<script>alert('推送错误,原因:" + ex.Message + "');</script>", false);
                    }
                }
            }

            //有几个问题需要咨询 
            //1,文章是否要同时推送到党群与产业通,或根据节点,推送至不同的平台(党群||产业通)
            //2,对方有无对我们的内容解码处理
            //3,需要配置好栏目节点之间的对应XML
            //1,图片需要转为http格式(需要配后应)
        }
        //更新IsCatch字段
        private void UpdateCatch(DataRow dr)
        {
            string sql = "UPDATE {0} Set IsCatch=1 WHERE {1}=" + dr["ID"];
            switch (dr["Source"].ToString())
            {
                case "content":
                    sql = string.Format(sql, "ZL_CommonModel", "GeneralID");
                    break;
                case "ask":
                    sql = string.Format(sql, "ZL_Ask", "ID");
                    break;
            }
            SqlHelper.ExecuteSql(sql);
        }
    }
}