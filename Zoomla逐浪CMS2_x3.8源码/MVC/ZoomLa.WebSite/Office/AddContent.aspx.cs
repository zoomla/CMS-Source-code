using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;
using ZoomLa.SQLDAL;
/*
 * OA发送通知等，根据所选ID，载入不同的预置模板
 */

namespace ZoomLaCMS.MIS.OA
{
    public partial class AddContent : System.Web.UI.Page
    {
        protected B_User buser = new B_User();
        protected B_Structure strBll = new B_Structure();
        protected B_Mis_Model bmis = new B_Mis_Model();
        protected M_Mis_Model mmis = new M_Mis_Model();
        //---------用户组权限
        protected B_UserPromotions promBll = new B_UserPromotions();
        private string oaVPath = SiteConfig.SiteOption.UploadDir + "OA/";
        //---------添加文章
        protected B_Node bnode = new B_Node();
        protected B_Model bmode = new B_Model();
        protected B_ModelField bfield = new B_ModelField();
        protected B_Content bll = new B_Content();
        protected B_Sensitivity sll = new B_Sensitivity();
        int modelID = OAConfig.ModelID;
        //---------
        protected M_CommonData mcon = new M_CommonData();
        protected M_UserInfo minfo = new M_UserInfo();
        protected B_OA_Document boa = new B_OA_Document();
        protected B_UserPurview purBll = new B_UserPurview();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                NodeDPBind();
                TypeDataBind();
                if (!string.IsNullOrEmpty(Request.QueryString["NodeID"]))
                {
                    nodeDP.SelectedValue = Request.QueryString["NodeID"];
                }
                if (string.IsNullOrEmpty(Request.QueryString["gid"]))
                {
                    nodeNameL.Text = GetNodeName(Request.QueryString["NodeID"]);
                    nodeNameL2.Text = nodeNameL.Text;
                    userNameT.Text = buser.GetLogin().HoneyName;
                    userGroupL.Text = strBll.SelNameByUid(buser.GetLogin().UserID);
                    CreateTime.Text = DateTime.Now.ToString("yyyy年MM月dd日 hh:mm");
                }
                else //修改
                {
                    BindText(Convert.ToInt32(Request.QueryString["Gid"]));
                }
            }
        }
        private void TypeDataBind()
        {
            DataTable dt = new DataTable();
            dt = bmis.Sel();
            TypeDP.DataSource = dt;
            TypeDP.DataTextField = "ModelName";
            TypeDP.DataValueField = "ID";
            TypeDP.DataBind();
            TypeDP.Items.Insert(0, new ListItem("请选择", "0"));
        }
        private void NodeDPBind()
        {
            //----权限检测
            DataTable dt = bmode.getNodesByModel(OAConfig.ModelID.ToString());
            string nodeids = promBll.GetNodeIDS(buser.GetLogin().GroupID);
            if (string.IsNullOrEmpty(nodeids))
                function.WriteErrMsg("无权限,请联系管理员!!!");
            dt.DefaultView.RowFilter = "NodeID in (" + nodeids + ")";
            dt = dt.DefaultView.ToTable();
            if (dt.Rows.Count < 1)
                function.WriteErrMsg("你没有添加文档的权限，请联系管理员!!!");
            if (!string.IsNullOrEmpty(Request.QueryString["NodeID"]) && dt.Select("NodeID = '" + Request.QueryString["NodeID"] + "'").Length < 1)
            {
                function.WriteErrMsg("你无权限在该节点添加文章，请联系管理员!!!");
            }

            nodeDP.DataSource = dt;
            nodeDP.DataValueField = "NodeID";
            nodeDP.DataTextField = "NodeName";
            nodeDP.DataBind();
            nodeDP.Items.Insert(0, new ListItem("请选择栏目", "-1"));
        }
        protected void Type_SelectedIndexChanged(object sender, EventArgs e)
        {
            int modelID = DataConvert.CLng(TypeDP.SelectedValue.ToString());
            if (modelID > 0)
            {
                mmis = bmis.SelReturnModel(modelID);
                content.Text = mmis.ModelContent;
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "setContent('"+mmis.ModelContent+"');", true);//不显示代码
            }
        }
        //修改模式使用,更改前台页面
        protected void BindText(int gid)
        {
            mcon = bll.GetCommonData(gid);
            if (buser.GetLogin().UserName != mcon.Inputer && !purBll.Auth("OAEdit", buser.GetLogin().UserRole, mcon.NodeID))
                function.WriteErrMsg("非本人添加或无修改权限！！");
            Title_T.Text = mcon.Title;
            nodeDP.SelectedValue = mcon.NodeID.ToString();
            CreateTime.Text = mcon.CreateTime.ToString("yyyy年MM月dd日 HH:mm:ss");
            userNameT.Text = mcon.Inputer;
            userGroupL.Text = strBll.SelNameByUid(buser.GetLogin().UserID);
            DataRow dr = boa.SelByItemID(mcon.TableName, mcon.ItemID).Rows[0];

            Secret.SelectedValue = dr["Secret"].ToString();
            Urgency.SelectedValue = dr["Urgency"].ToString();
            Importance.SelectedValue = dr["Importance"].ToString();
            userGroupL.Text = dr["userGroupT"].ToString();
            content.Text = dr["content"].ToString();
            if (!string.IsNullOrEmpty(dr["attach"].ToString()))
            {
                string[] af = dr["attach"].ToString().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                string h = "";
                for (int i = 0; i < af.Length; i++)
                {
                    h += "<span class='disupFile'>";
                    h += GroupPic.GetShowExtension(GroupPic.GetExtName(af[i]));
                    h += "<a href='" + af[i] + "' title='点击下载'>" + af[i].Split('/')[(af[i].Split('/').Length - 1)] +
                         "<a href='javascript:;' title='删除' onclick='delHasFile(\"" + af[i] + "\",this);' ><img src='/App_Themes/AdminDefaultTheme/images/del.png'/></a></span>";
                }
                hasFileTD.InnerHtml = h;
            }
            hasFileData.Value = dr["attach"].ToString();//便于删除管理
            saveBtn.Text = "修改";
        }
        //---------Tool
        public string GetNodeName(string nid)
        {
            string result = OACommon.GetNodeID(nid, 1) + "发文";
            return result;
        }

        protected void EBtnSubmit_Click(object sender, EventArgs e)//添加文章
        {
            int nodeID = Convert.ToInt32(nodeDP.SelectedValue);
            if (nodeID < 0)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('请先选择栏目!!!');", true);
                return;
            }
            if (!AuthCheck(nodeID))
                function.WriteErrMsg("你没有对该栏目的添加权限!!!");
            M_CommonData CData = new M_CommonData();
            DataTable dt = this.bfield.SelByModelID(modelID);
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("FieldName", typeof(string)));
            table.Columns.Add(new DataColumn("FieldType", typeof(string)));
            table.Columns.Add(new DataColumn("FieldValue", typeof(string)));
            //手动完成赋值

            string[] fieldArr = { "Secret", "Urgency", "Importance" };
            for (int i = 0; i < fieldArr.Length; i++)
            {
                DataRow dr = table.NewRow();
                dr["FieldName"] = fieldArr[i];
                dr["FieldType"] = "TextType";
                dr["FieldValue"] = Request.Form[fieldArr[i]];
                table.Rows.Add(dr);
            }
            //内容
            DataRow dr2 = table.NewRow();
            dr2["FieldName"] = "content";
            dr2["FieldType"] = "MultipleHtmlType";
            dr2["FieldValue"] = content.Text;
            table.Rows.Add(dr2);
            //所属部门
            dr2 = table.NewRow();
            dr2["FieldName"] = "UserGroupT";
            dr2["FieldType"] = "TextType";
            dr2["FieldValue"] = strBll.SelNameByUid(buser.GetLogin().UserID);
            table.Rows.Add(dr2);
            //附件
            dr2 = table.NewRow();
            dr2["FieldName"] = "Attach";
            dr2["FieldType"] = "TextType";
            dr2["FieldValue"] = SaveFile();
            table.Rows.Add(dr2);

            //将无法获取的值，手动写入table中
            if (!string.IsNullOrEmpty(Request.QueryString["Gid"]))
                CData = bll.GetCommonData(Convert.ToInt32(Request.QueryString["Gid"]));

            CData.NodeID = nodeID;
            CData.ModelID = modelID;
            CData.TableName = this.bmode.GetModelById(modelID).TableName;
            CData.Title = Title_T.Text.Trim();
            //判断是否使用文件流程
            CData.Status = 99;
            CData.EliteLevel = 0;
            CData.InfoID = "";
            CData.SpecialID = "";
            //CData.CreateTime = "";
            CData.UpDateType = 2;
            CData.UpDateTime = DateTime.Now;
            string Keyword = this.TxtTagKey.Text.Trim().Replace(" ", "|");
            ;
            CData.TagKey = Keyword;
            CData.Titlecolor = "";
            CData.Template = "";
            CData.CreateTime = DataConvert.CDate(CreateTime.Text);
            CData.ProWeek = 0;
            CData.Pronum = 0;
            CData.BidType = 0;
            CData.IsBid = (CData.BidType > 0) ? 1 : 0;
            CData.BidMoney = DataConverter.CDouble(0);
            CData.DefaultSkins = 0;
            CData.FirstNodeID = GetFriestNode(nodeID);
            CData.TitleStyle = Request.Form["ThreadStyle"];
            CData.ParentTree = GetParentTree(nodeID);//父级别树
            CData.TopImg = Request.Form["selectpic"];//首页图片
            CData.PdfLink = "";
            CData.Subtitle = "";
            CData.PYtitle = "";
            int newID = 0;
            if (string.IsNullOrEmpty(Request.QueryString["Gid"]))//新增
            {
                CData.Inputer = buser.GetLogin().UserName;
                CData.Hits = 0;
                newID = this.bll.AddContent(table, CData);//插入信息给两个表，主表和从表:CData-主表的模型，table-从表   

                B_KeyWord kll = new B_KeyWord();//添加关键词
                if (!string.IsNullOrEmpty(Keyword))
                {
                    string[] arrKey = Keyword.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                    for (int tt = 0; tt < arrKey.Length; tt++)
                    {
                        if (kll.IsExist(arrKey[tt]))
                        {
                            M_KeyWord kinfo = kll.GetKeyByName(arrKey[tt]);
                            kinfo.QuoteTimes++;
                            kinfo.LastUseTime = DateTime.Now;
                            if (string.IsNullOrEmpty(kinfo.ArrGeneralID))
                                kinfo.ArrGeneralID = newID.ToString() + ",";
                            else
                                kinfo.ArrGeneralID = kinfo.ArrGeneralID + newID.ToString() + ",";
                            kll.Update(kinfo);
                        }
                        else
                        {
                            M_KeyWord kinfo1 = new M_KeyWord();
                            kinfo1.KeyWordID = 0;
                            kinfo1.KeywordText = arrKey[tt];
                            kinfo1.KeywordType = 1;
                            kinfo1.LastUseTime = DateTime.Now;
                            kinfo1.Hits = 0;
                            kinfo1.Priority = 10;
                            kinfo1.QuoteTimes = 1;
                            kinfo1.ArrGeneralID = "," + newID.ToString() + ",";
                            kll.Add(kinfo1);
                        }
                    }
                }
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('添加成功');window.location.href='ContentManage.aspx?NodeID=" + nodeID + "'", true);
            }
            else //修改
            {
                newID = CData.GeneralID;
                bll.UpdateContent(table, CData);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('修改成功');window.location.href='ContentManage.aspx?NodeID=" + nodeID + "'", true);
            }
        }
        public string returnnodelist = "";
        /// <summary>
        /// 获得父级树
        /// </summary>
        /// <returns></returns>
        public string GetParentTree(int NodeID)
        {
            M_Node nodelist = bnode.GetNodeXML(NodeID);
            returnnodelist = NodeID + "," + returnnodelist;
            if (nodelist.NodeID > 0 && nodelist.ParentID > 0)
            {
                GetParentTree(nodelist.ParentID);
            }
            if (returnnodelist != "")
            {
                if (BaseClass.Left(returnnodelist, 1) != ",")
                {
                    returnnodelist = "," + returnnodelist;
                }
                if (BaseClass.Right(returnnodelist, 1) != ",")
                {
                    returnnodelist = returnnodelist + ",";
                }
            }
            return returnnodelist;
        }
        public string getKey()
        {
            B_KeyWord bll = new B_KeyWord();
            DataTable temptable = bll.GetKeyWordAll();
            string keys = "";
            for (int i = 0; i < temptable.Rows.Count; i++)
            {
                if (i == 0)
                    keys = temptable.Rows[i]["KeywordText"].ToString();
                else
                    keys = keys + "," + temptable.Rows[i]["KeywordText"].ToString();
            }
            return keys;
        }

        // 获得第一级节点ID
        public int GetFriestNode(int NodeID)
        {
            GetNo(NodeID);
            return FNodeID;
        }
        protected int FNodeID = 0;
        public void GetNo(int NodeID)
        {
            M_Node nodeinfo = bnode.GetNodeXML(NodeID);
            int ParentID = nodeinfo.ParentID;
            if (DataConverter.CLng(nodeinfo.ParentID) > 0)
            {
                GetNo(nodeinfo.ParentID);
            }
            else
            {
                FNodeID = nodeinfo.NodeID;
            }
        }
        //-----Tools
        public string GetFileName(string fileName)
        {
            return function.GetFileName() + "." + fileName.Split('.')[(fileName.Split('.').Length - 1)] + "$" + fileName;
        }
        //保存上传的文件,如果是上传,则加上已有的
        public string SaveFile()
        {
            oaVPath += DateTime.Now.ToString("yyyyMMddHHmmss") + "/";
            string oaPPath = Server.MapPath(oaVPath);
            string result = "";
            if (Request.Files.Count > 0)
            {
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    if (Request.Files[i].ContentLength < 1)
                        continue;//为空则不处理
                    if (!Directory.Exists(oaPPath))
                    {
                        Directory.CreateDirectory(oaPPath);
                    }
                    SafeSC.SaveFile(oaPPath + Path.GetFileName(Request.Files[i].FileName), Request.Files[i]);
                    result += oaVPath + Path.GetFileName(Request.Files[i].FileName) + ",";
                }
                if (!string.IsNullOrEmpty(Request.QueryString["Gid"]))
                {
                    result += hasFileData.Value;
                }
            }
            return result;
        }
        /// <summary>
        /// 是否有该节点的添加权限
        /// </summary>
        /// <returns></returns>
        public bool AuthCheck(int nid)
        {
            string nodeIDS = "," + promBll.GetNodeIDS(buser.GetLogin().GroupID);
            return nodeIDS.Contains("," + nid + ",");
        }
        public string GetEditor(string name)
        {
            if (SiteConfig.SiteOption.EditVer == "1")
                return "";
            if (SiteConfig.SiteOption.EditVer == "3")
                return "var editor; setTimeout(function () { editor = UE.getEditor('" + name + "',{" + BLLCommon.ueditorMid + "});}, 300);";
            else
                return "";
        }
    }
}