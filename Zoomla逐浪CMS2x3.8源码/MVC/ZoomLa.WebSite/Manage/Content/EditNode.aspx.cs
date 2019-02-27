using System;
using System.Text;
using System.IO;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;
using System.Xml;
using ZoomLa.Components;
using System.Web;
using ZoomLa.BLL.API;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZoomLa.Model.CreateJS;
using ZoomLa.BLL.CreateJS;

namespace ZoomLaCMS.Manage.Content
{
    public partial class EditNode : CustomerPageAction
    {
        B_Admin badmin = new B_Admin();
        B_Group bGll = new B_Group();
        B_Model bllmodel = new B_Model();
        B_Node nodeBll = new B_Node();
        B_NodeRole bnll = new B_NodeRole();
        B_Pub pll = new B_Pub();
        B_UserPromotions psll = new B_UserPromotions();
        string ModelArr = "";
        /*文档工作流*/
        private B_Flow bf = new B_Flow();
        private B_NodeBindDroit droBll = new B_NodeBindDroit();
        private M_NodeBindDroit droMod = new M_NodeBindDroit();
        public int ParentID { get { return DataConverter.CLng(Request.QueryString["ParentID"]); } }
        public int NodeID { get { return DataConverter.CLng(Request.QueryString["NodeID"]); } }
        //会员组权限表,修改时才取值,并且验证
        public DataTable GroupAuthDT { get { return ViewState["GroupAuthDT"] as DataTable; } set { ViewState["GroupAuthDT"] = value; ; } }
        public string ViewMode { get { return Request.QueryString["view"] ?? ""; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            B_ARoleAuth.CheckEx(ZLEnum.Auth.model, "NodeEdit");
            if (function.isAjax())
            {
                #region AJAX
                M_Node nodeMod = nodeBll.SelReturnModel(NodeID);
                if (NodeID < 1) { nodeMod.ParentID = ParentID; }
                string action = Request.Form["action"];
                string value = Request.Form["value"];
                int result = M_APIResult.Success;
                switch (action)
                {
                    case "nodename":
                        {
                            nodeMod.NodeName = value;
                            result = nodeBll.CheckNodeName(nodeMod) ? M_APIResult.Success : M_APIResult.Failed;
                        }
                        break;
                    case "nodedir":
                        {
                            nodeMod.NodeDir = value;
                            result = nodeBll.CheckNodeDir(nodeMod) ? M_APIResult.Success : M_APIResult.Failed;
                        }
                        break;
                }
                Response.Write(result); Response.Flush(); Response.End();
                #endregion
            }
            if (!IsPostBack)
            {
                M_AdminInfo adminMod = B_Admin.GetLogin();
                M_Node nodeMod = nodeBll.SelReturnModel(NodeID);
                M_Node parentMod = nodeBll.SelReturnModel(ParentID > 0 ? ParentID : nodeMod.ParentID);
                LblNodeName.Text = parentMod.NodeName;
                if (NodeID < 1)
                {
                    Title_T.Text = "添加节点";
                    CDate_T.Text = DateTime.Now.ToString();
                    CUser_T.Text = adminMod.AdminName;
                    ModelArr += parentMod.ContentModel;
                    function.Script(this, "BindPY();");
                }
                else
                {
                    Title_T.Text = "修改节点";
                    Release_Btn.Visible = true;
                    #region 节点信息填充
                    if (nodeMod.IsNull) { function.WriteErrMsg("指定要编辑的节点不存在"); }
                    TxtNodeName.Text = nodeMod.NodeName;
                    TxtNodeDir.Text = nodeMod.NodeDir;
                    TxtNodePicUrl.Text = nodeMod.NodePic;
                    TxtTips.Text = nodeMod.Tips;
                    TxtMetaKeywords.Text = nodeMod.Meta_Keywords;
                    TxtMetaDescription.Text = nodeMod.Meta_Description;
                    TxtDescription.Text = nodeMod.Description;
                    //RBLOpenType.SelectedValue = DataConverter.CLng(node.OpenNew).ToString();
                    RBLPurviewType.SelectedValue = nodeMod.PurviewType ? "1" : "0";
                    SiteContentAudit_Rad.SelectedValue = nodeMod.SiteContentAudit.ToString();
                    RBLCommentType.SelectedValue = nodeMod.CommentType;
                    TxtHitsOfHot.Text = nodeMod.HitsOfHot.ToString();

                    TxtTemplate_hid.Value = nodeMod.ListTemplateFile;
                    IndexTemplate_hid.Value = nodeMod.IndexTemplate;
                    LastinfoTemplate_hid.Value = nodeMod.LastinfoTemplate;
                    ProposeTemplate_hid.Value = nodeMod.ProposeTemplate;
                    HotinfoTemplate_hid.Value = nodeMod.HotinfoTemplate;

                    ListPageHtmlEx_Rad.SelectedValue = nodeMod.ListPageHtmlEx.ToString();
                    ContentFileEx_Rad.SelectedValue = nodeMod.ContentFileEx.ToString();
                    ListPageEx_Rad.SelectedValue = nodeMod.ListPageEx.ToString();
                    LastinfoPageEx_Rad.SelectedValue = nodeMod.LastinfoPageEx.ToString();
                    HotinfoPageEx.SelectedValue = nodeMod.HotinfoPageEx.ToString();
                    ProposePageEx.SelectedValue = nodeMod.ProposePageEx.ToString();
                    DDLContentRule.SelectedValue = nodeMod.ContentPageHtmlRule.ToString();
                    RBLPosition.SelectedValue = nodeMod.HtmlPosition.ToString();
                    RBLItemOpenType.SelectedValue = nodeMod.ItemOpenType.ToString();
                    function.Script(this, "SetRadVal('UrlFormat_Rad','" + nodeMod.ItemOpenTypeTrue + "');");
                    RBLOpenType.SelectedValue = nodeMod.OpenTypeTrue.ToString();
                    TxtAddMoney.Text = nodeMod.AddMoney.ToString();
                    TxtAddPoint.Text = nodeMod.AddPoint.ToString();
                    ClickTimeout.SelectedValue = nodeMod.ClickTimeout.ToString();
                    txtAddExp.Text = nodeMod.AddUserExp.ToString();
                    txtDeducExp.Text = nodeMod.DeducUserExp.ToString();
                    ConsumeType_Hid.Value = nodeMod.ConsumeType.ToString();
                    CDate_T.Text = nodeMod.CDate.ToString();
                    CUser_T.Text = nodeMod.CUName;
                    DataTable auitDt = nodeBll.GetNodeAuitDT(nodeMod.Purview);
                    //节点权限
                    if (auitDt != null && auitDt.Rows.Count > 0)
                    {
                        DataRow auitdr = auitDt.Rows[0];
                        SelCheck_RadioList.SelectedValue = auitdr["View"].ToString();
                        string ViewGroup = auitdr["ViewGroup"].ToString();
                        string ViewSunGroup = auitdr["ViewSunGroup"].ToString();
                        string input = auitdr["input"].ToString();
                        string forum = auitdr["forum"].ToString();
                        foreach (ListItem item in ViewGroup_Chk.Items)
                        {
                            if (("," + ViewGroup + ",").Contains("," + item.Value + ","))
                            { item.Selected = true; }
                        }
                        foreach (ListItem item in ViewGroup2_Chk.Items)
                        {
                            if (("," + ViewSunGroup + ",").Contains("," + item.Value + ","))
                            { item.Selected = true; }
                        }
                        foreach (ListItem item in input_Chk.Items)
                        {
                            if (("," + input + ",").Contains("," + item.Value + ","))
                            { item.Selected = true; }
                        }
                        foreach (ListItem item in forum_Chk.Items)
                        {
                            if (("," + forum + ",").Contains("," + item.Value + ","))
                            { item.Selected = true; }
                        }

                    }
                    TxtConsumeCount.Text = nodeMod.ConsumeCount.ToString();
                    TxtConsumePoint.Text = nodeMod.ConsumePoint.ToString();
                    TxtConsumeTime.Text = nodeMod.ConsumeTime.ToString();
                    TxtShares.Text = nodeMod.Shares.ToString();
                    SetCustom(nodeMod.Custom);
                    ModelArr = nodeMod.ContentModel;
                    isSimple_CH.Checked = nodeMod.Contribute == 1;
                    SafeGuard.Checked = nodeMod.SafeGuard == 1 ? true : false;
                    #endregion
                }
                //添加工作流
                ddlState.DataSource = bf.GetFlowAll();
                ddlState.DataTextField = "flowName";
                ddlState.DataValueField = "id";
                ddlState.DataBind();
                ddlState.Items.Insert(0, new ListItem("不指定", "0"));
                droMod = droBll.SelByNodeID(NodeID);
                ddlState.SelectedValue = droMod == null ? "0" : droMod.FID.ToString();
                BGroup.DataSource = bGll.GetGroupList();
                BGroup.DataBind();
                //内容模型列表
                DataTable dt = bllmodel.GetList();
                Model_RPT.DataSource = dt;
                Model_RPT.DataBind();
                //角色列表
                DataTable Rt = B_Role.SelectNodeRoleNode(NodeID);
                AdminRole_EGV.DataSource = Rt;
                AdminRole_EGV.DataBind();
                //组权限绑定
                GroupAuthDT = psll.SelByNid(NodeID);
                DataTable gpauthDT = bGll.GetGroupList();
                for (int i = 0; i < gpauthDT.Rows.Count; i++)
                {
                    ViewGroup_Chk.Items.Add(new ListItem(gpauthDT.Rows[i]["GroupName"].ToString(), gpauthDT.Rows[i]["GroupID"].ToString()));
                    ViewGroup2_Chk.Items.Add(new ListItem(gpauthDT.Rows[i]["GroupName"].ToString(), gpauthDT.Rows[i]["GroupID"].ToString()));
                    input_Chk.Items.Add(new ListItem(gpauthDT.Rows[i]["GroupName"].ToString(), gpauthDT.Rows[i]["GroupID"].ToString()));
                }
                GroupAuth_EGV.DataSource = gpauthDT;
                GroupAuth_EGV.DataBind();
                #region 互动
                DataTable dp = pll.GetPubModelPublic();
                DropDownList1.DataSource = dp;
                DropDownList1.DataTextField = "PubName";
                DropDownList1.DataValueField = "Pubid";
                DropDownList1.DataBind();
                DropDownList1.Items.Insert(0, new ListItem("选择绑定", "0"));
                M_Pub pp = pll.GetSelectNode(NodeID.ToString());
                if (pp.Pubid > 0)
                {
                    DropDownList1.SelectedValue = pp.Pubid.ToString();
                }
                #endregion
                string bread = "<li><a href='" + customPath2 + "Config/SiteInfo.aspx'>系统设置</a></li><li>" + (ViewMode.Equals("design") || ViewMode.Equals("child") ? "<a href='DesignNodeManage.aspx'>动力节点</a>" : "<a href='NodeManage.aspx'>节点管理</a>") + "</li>";
                bread += "<li class='active'><a href='" + Request.RawUrl + "'>" + Title_T.Text + "</a></li>";
                bread += Call.GetHelp(103);
                bread += "<div class='pull-right' style='margin-right:10px;'><a href='" + Request.RawUrl + "' title='刷新'><i class='fa fa-refresh'></i></a></div>";
                Call.SetBreadCrumb(Master, bread);
            }
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            GroupAuthDT = null;
        }
        protected void EBtnSubmit_Click(object sender, EventArgs e)
        {
            M_Node node = new M_Node();
            if (NodeID > 0) { node = nodeBll.SelReturnModel(NodeID); }
            else
            {
                node.ParentID = ParentID;
                node.NodeUrl = "";
                node.NodeListUrl = "";
            }
            node.NodeType = 1;
            node.NodeDir = TxtNodeDir.Text;
            node.NodeName = TxtNodeName.Text;
            if (!nodeBll.CheckCanSave(node)) { function.WriteErrMsg("节发现同栏目下栏目名或标识名重复,请点击确定重新修改节点"); }
            #region 修改
            //node.Viewinglimit = EDHid.Value.TrimEnd('|');
            node.NodePic = TxtNodePicUrl.Text;
            node.Tips = TxtTips.Text;
            node.Description = TxtDescription.Text;
            node.Meta_Keywords = TxtMetaKeywords.Text;
            node.Meta_Description = TxtMetaDescription.Text;
            node.OpenNew = DataConverter.CBool(RBLOpenType.SelectedValue);
            node.ItemOpenType = DataConverter.CBool(RBLItemOpenType.SelectedValue);
            node.ItemOpenTypeTrue = Request.Form["UrlFormat_Rad"];
            node.PurviewType = DataConverter.CBool(RBLPurviewType.SelectedValue);
            node.CommentType = RBLCommentType.SelectedValue;
            node.HitsOfHot = DataConverter.CLng(TxtHitsOfHot.Text);
            node.ListTemplateFile = TxtTemplate_hid.Value;
            node.IndexTemplate = IndexTemplate_hid.Value;// Request.Form[TxtIndexTemplate.UniqueID];
            node.LastinfoTemplate = LastinfoTemplate_hid.Value;
            node.ProposeTemplate = ProposeTemplate_hid.Value;
            node.HotinfoTemplate = HotinfoTemplate_hid.Value;
            node.ConsumePoint = DataConverter.CLng(TxtConsumePoint.Text);
            node.ConsumeType = DataConverter.CLng(Page.Request.Form["ConsumeType"]);
            node.ConsumeTime = DataConverter.CLng(TxtConsumeTime.Text);
            node.ConsumeCount = DataConverter.CLng(TxtConsumeCount.Text);
            node.Shares = DataConverter.CFloat(TxtShares.Text);
            node.OpenTypeTrue = RBLOpenType.SelectedValue;
            node.Custom = GetCustom();
            node.AddPoint = DataConverter.CLng(TxtAddPoint.Text);
            node.AddMoney = DataConverter.CDouble(TxtAddMoney.Text);
            node.ClickTimeout = DataConverter.CLng(ClickTimeout.SelectedValue);
            node.AddUserExp = DataConverter.CLng(txtAddExp.Text);
            node.DeducUserExp = DataConverter.CLng(txtDeducExp.Text);
            node.CDate = DataConverter.CDate(CDate_T.Text);
            node.CUser = badmin.GetAdminLogin().AdminId;
            node.CUName = CUser_T.Text;
            node.ContentModel = (Request.Form["ChkModel"] ?? "");
            string modellist = node.ContentModel;
            Int32 Nodetype = 0;
            StringBuilder NodeListTypeall = new StringBuilder();
            NodeListTypeall.Append("");

            DataTable dt = bllmodel.GetModels(node.ContentModel);
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    NodeListTypeall.Append("," + dr["ModelType"]);
                }
            }
            NodeListTypeall.Append(",");
            if (Convert.ToInt32(NodeListTypeall.ToString().IndexOf(",1,")) > -1 && Convert.ToInt32(NodeListTypeall.ToString().IndexOf(",2,")) > -1)
            {
                Nodetype = 3;
            }
            else if (Convert.ToInt32(NodeListTypeall.ToString().IndexOf(",2,")) > -1 && Convert.ToInt32(NodeListTypeall.ToString().IndexOf(",1,")) == -1)
            {
                Nodetype = 2;
            }
            else if (Convert.ToInt32(NodeListTypeall.ToString().IndexOf(",1,")) > -1 && Convert.ToInt32(NodeListTypeall.ToString().IndexOf(",2,")) == -1)
            {
                Nodetype = 1;
            }
            else if (Convert.ToInt32(NodeListTypeall.ToString().IndexOf(",5,")) > -1)
            {
                Nodetype = 5;
            }
            //else if (DropDownList1.SelectedValue != "0")
            //{
            //    M_Pub pm = pll.GetSelect(DataConverter.CLng(DropDownList1.SelectedValue));
            //    Nodetype = (pm.Public == 1) ? 7 : 6;
            //}
            node.ListPageHtmlEx = DataConverter.CLng(ListPageHtmlEx_Rad.SelectedValue);
            node.ContentFileEx = DataConverter.CLng(ContentFileEx_Rad.SelectedValue);
            node.ListPageEx = DataConverter.CLng(ListPageEx_Rad.SelectedValue);
            node.LastinfoPageEx = DataConverter.CLng(LastinfoPageEx_Rad.SelectedValue);//LastinfoTemplate.UniqueID
            node.HotinfoPageEx = DataConverter.CLng(HotinfoPageEx.SelectedValue);
            node.ProposePageEx = DataConverter.CLng(ProposePageEx.SelectedValue);
            node.ContentPageHtmlRule = DataConverter.CLng(DDLContentRule.SelectedValue);
            node.SafeGuard = SafeGuard.Checked ? 1 : 0;
            node.HtmlPosition = DataConverter.CLng(RBLPosition.SelectedValue);
            node.NodeListType = DataConverter.CLng(Nodetype);
            node.Contribute = isSimple_CH.Checked ? 1 : 0;
            node.SiteContentAudit = DataConverter.CLng(SiteContentAudit_Rad.SelectedValue);
            DataTable auitdt = nodeBll.GetNodeAuitDT();
            DataRow auitDr = auitdt.Rows[0];
            auitDr["View"] = SelCheck_RadioList.SelectedValue;
            string ViewGroupvalue = "";
            foreach (ListItem dd in ViewGroup_Chk.Items)
            {
                if (dd.Selected)
                {
                    if (ViewGroupvalue != "")
                    {
                        ViewGroupvalue += ",";
                    }
                    ViewGroupvalue += dd.Value;
                }
            }
            auitDr["ViewGroup"] = ViewGroupvalue;
            string ViewGroup2value = "";
            foreach (ListItem dd in ViewGroup2_Chk.Items)
            {
                if (dd.Selected)
                {
                    if (ViewGroup2value != "")
                    {
                        ViewGroup2value += ",";
                    }
                    ViewGroup2value += dd.Value;
                }
            }
            auitDr["ViewSunGroup"] = ViewGroup2value;
            string ViewGroup3value = "";
            foreach (ListItem dd in input_Chk.Items)
            {
                if (dd.Selected)
                {
                    if (ViewGroup3value != "")
                    {
                        ViewGroup3value += ",";
                    }
                    ViewGroup3value += dd.Value;
                }
            }
            auitDr["input"] = ViewGroup3value;
            string forumvalue = "";
            foreach (ListItem dd in forum_Chk.Items)
            {
                if (dd.Selected)
                {
                    if (forumvalue != "")
                    {
                        forumvalue += ",";
                    }
                    forumvalue += dd.Value;
                }
            }
            auitDr["forum"] = forumvalue;
            node.Purview = JsonConvert.SerializeObject(auitdt);
            if (node.NodeID > 0) { nodeBll.UpdateByID(node); }
            else { node.NodeID = nodeBll.Insert(node); }
            //----------ZL_Node_Template
            string[] ModelArr = modellist.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            nodeBll.DelModelTemplate(node.NodeID, modellist);
            for (int i = 0; i < ModelArr.Length; i++)
            {
                if (!string.IsNullOrEmpty(Page.Request.Form["ModelTemplate_" + ModelArr[i]].Trim()))
                {
                    //将模型模板设定写入数据库
                    string temp = Page.Request.Form["ModelTemplate_" + ModelArr[i]].Trim();
                    if (nodeBll.IsExistTemplate(node.NodeID, DataConverter.CLng(ModelArr[i])))
                    {
                        nodeBll.UpdateModelTemplate(node.NodeID, DataConverter.CLng(ModelArr[i]), temp);
                    }
                    else
                    {
                        nodeBll.AddModelTemplate(node.NodeID, DataConverter.CLng(ModelArr[i]), temp);
                    }
                }
            }
            //--工作流
            droMod = droBll.SelByNodeID(node.NodeID);
            if (droMod == null)//添加
            {
                droMod = new M_NodeBindDroit();
                droMod.NodeId = node.NodeID;
                droMod.FID = DataConverter.CLng(ddlState.SelectedValue);
                droBll.AddNodeBinDroit(droMod);
            }
            else
            {
                droMod.FID = DataConverter.CLng(ddlState.SelectedValue);
                droBll.UpdateByID(droMod);
            }
            for (int i = 0; i <= AdminRole_EGV.Rows.Count - 1; i++)
            {
                GridViewRow row = AdminRole_EGV.Rows[i];
                CheckBox cbox1 = (CheckBox)row.FindControl("chkSel1");
                CheckBox cbox2 = (CheckBox)row.FindControl("chkSel2");
                CheckBox cbox3 = (CheckBox)row.FindControl("chkSel3");
                CheckBox cbox4 = (CheckBox)row.FindControl("chkSel4");
                CheckBox cbox5 = (CheckBox)row.FindControl("chkSel5");
                CheckBox cbox6 = (CheckBox)row.FindControl("chkSel6");
                HiddenField hd = (HiddenField)row.FindControl("HiddenRID");

                M_NodeRole nr = bnll.SelModelByRidAndNid(node.NodeID, Convert.ToInt32(hd.Value));
                if (nr == null) { nr = new M_NodeRole(); nr.NID = node.NodeID; nr.RID = Convert.ToInt32(hd.Value); }
                nr.look = cbox1.Checked ? 1 : 0;
                nr.addTo = cbox2.Checked ? 1 : 0;
                nr.State = cbox3.Checked ? 1 : 0;
                nr.Modify = cbox4.Checked ? 1 : 0;
                nr.Columns = cbox5.Checked ? 1 : 0;
                nr.Comments = cbox6.Checked ? 1 : 0;
                bnll.InsertUpdate(nr);
            }

            if (DropDownList1.SelectedValue != "0")
            {
                M_Pub pm = pll.GetSelect(DataConverter.CLng(DropDownList1.SelectedValue));
                if (!("," + pm.PubNodeID + ",").Contains("," + node.NodeID + ","))//判断互动模型是否绑定该节点
                {
                    string[] strarr = pm.PubNodeID.Split(',');
                    StringBuilder sb = new StringBuilder();
                    for (int x = 0; x < strarr.Length; x++)
                    {
                        StringHelper.AppendString(sb, strarr[x], ",");
                    }
                    StringHelper.AppendString(sb, node.NodeID.ToString(), ",");
                    pm.PubNodeID = sb.ToString();
                }
                pll.GetUpdate(pm);
            }
            else
            {
                M_Pub pp = pll.GetSelectNode(node.NodeID.ToString());
                if (pp.Pubid > 0)
                {
                    string[] strarr = pp.PubNodeID.Split(',');
                    StringBuilder sb = new StringBuilder();
                    for (int x = 0; x < strarr.Length; x++)
                    {
                        if (!strarr[x].Equals(node.NodeID.ToString()))
                            StringHelper.AppendString(sb, strarr[x], ",");
                    }
                    pp.PubNodeID = sb.ToString();
                    pll.GetUpdate(pp);
                }
            }

            for (int i = 0; i <= GroupAuth_EGV.Rows.Count - 1; i++)
            {
                CheckBox cbox1 = (CheckBox)GroupAuth_EGV.Rows[i].FindControl("chkSel1");
                CheckBox cbox2 = (CheckBox)GroupAuth_EGV.Rows[i].FindControl("chkSel2");
                CheckBox cbox3 = (CheckBox)GroupAuth_EGV.Rows[i].FindControl("chkSel3");
                CheckBox cbox4 = (CheckBox)GroupAuth_EGV.Rows[i].FindControl("chkSel4");
                CheckBox cbox5 = (CheckBox)GroupAuth_EGV.Rows[i].FindControl("chkSel5");
                CheckBox cbox6 = (CheckBox)GroupAuth_EGV.Rows[i].FindControl("chkSel6");
                CheckBox cbox7 = (CheckBox)GroupAuth_EGV.Rows[i].FindControl("chkSel11");
                CheckBox cbox8 = (CheckBox)GroupAuth_EGV.Rows[i].FindControl("chkSel12");
                M_UserPromotions ups = new M_UserPromotions();
                ups.GroupID = Convert.ToInt32(GroupAuth_EGV.DataKeys[i].Value);
                ups.look = cbox1.Checked ? 1 : 0;
                ups.addTo = cbox2.Checked ? 1 : 0;
                ups.Modify = cbox3.Checked ? 1 : 0;
                ups.Deleted = cbox4.Checked ? 1 : 0;
                ups.Columns = cbox5.Checked ? 1 : 0;
                ups.Comments = cbox6.Checked ? 1 : 0;
                ups.Down = cbox7.Checked ? 1 : 0;
                ups.quote = cbox8.Checked ? 1 : 0;
                ups.NodeID = node.NodeID;
                IsChild(ups);
                psll.GetInsertOrUpdate(ups);
            }
            #endregion
            //if (ViewMode.Equals("design")) { function.WriteSuccessMsg("操作成功", "DesignNodeManage.aspx"); }
            //else if (ViewMode.Equals("child")) { function.WriteSuccessMsg("操作成功", "DesignNodeList.aspx?pid=" + Request.QueryString["pid"]); }
            //else { function.WriteSuccessMsg("操作成功", "NodeManage.aspx"); }
            function.WriteSuccessMsg("操作成功", "NodeManage.aspx");
        }

        //-------------------------------------------
        public string GetFileInfo()
        {
            if (Eval("type").Equals("1"))
                return "<a href='javascript:;' onclick='toggleChild(this," + Eval("id") + ")' ><span class='fa fa-folder-open'></span> " + Eval("name") + "</a>";
            else
                return "<a href='javascript:;' data-pid='" + Eval("pid") + "' onclick=\"setVal(this,'" + Eval("rname") + "')\"><img src='/Images/TreeLineImages/t.gif' /> <span class='fa fa-file'></span> " + Eval("name") + "</a>";
        }
        //过滤掉没有文件的文件夹
        public DataTable FiterHasFile(DataTable filedt)
        {
            DataTable newfiledt = filedt.Clone();
            foreach (DataRow row in filedt.Rows)
            {
                if (row["type"].Equals("1") && filedt.AsEnumerable().Where((t => (t.Field<string>("type").Equals("2")) && t.Field<string>("rname").Contains(row["rname"].ToString()))).AsDataView().Count <= 0)
                {

                    continue;
                }
                DataRow dr = newfiledt.NewRow();
                dr["type"] = row["type"];
                dr["path"] = row["path"];
                dr["rname"] = row["rname"].ToString();
                dr["name"] = row["name"];
                newfiledt.Rows.Add(dr);

            }
            return newfiledt;
        }
        private void SetCustom(string Custom)
        {
            if (!string.IsNullOrEmpty(Custom))
            {
                string[] CustArr = Custom.Split(new string[] { "{SplitCustom}" }, StringSplitOptions.RemoveEmptyEntries);
                if (CustArr.Length > 0)
                {
                    SelCount.SelectedValue = CustArr.Length.ToString();
                    for (int i = 1; i <= CustArr.Length; i++)
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), i.ToString(), "$('#TextBox" + i + "').val('" + CustArr[i - 1] + "');$('#Custom" + i + "').show();", true);
                    }
                }
            }
        }
        private string GetCustom()
        {
            int index = DataConverter.CLng(SelCount.SelectedValue);
            string Cust = "";
            for (int i = 1; i <= index; i++)
            {
                TextBox tbox = (TextBox)Page.FindControl("ctl00$Content$TextBox" + i.ToString());
                string con = tbox.Text.Trim();
                if (!string.IsNullOrEmpty(con))
                {
                    Cust = Cust + con + "{SplitCustom}";
                }
            }
            return Cust;
        }
        //模型,是否选中
        public string GetChk(string mid)
        {
            string result = "";
            string[] arr = ModelArr.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (IsInModel(mid, arr))
            {
                result = "<input type=\"checkbox\" id=\"ChkModel\" name=\"ChkModel\" value=\"" + mid + "\" checked />";
            }
            else
            {
                result = "<input type=\"checkbox\" id=\"ChkModel\" name=\"ChkModel\" value=\"" + mid + "\" />";
            }
            return result;
        }
        public string GetTemplate(string mid)
        {
            string result = "";
            string[] arr = ModelArr.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (IsInModel(mid, arr))
            {
                result = nodeBll.GetModelTemplate(NodeID, DataConverter.CLng(mid));
                if (string.IsNullOrEmpty(result))
                    result = bllmodel.GetModelById(DataConverter.CLng(mid)).ContentModule;
            }
            else
            {
                result = bllmodel.GetModelById(DataConverter.CLng(mid)).ContentModule;
            }
            return result.Substring(0);
        }
        public bool IsInModel(string modelid, string[] array)
        {
            bool flag = false;
            for (int i = 0; i < array.Length; i++)
            {
                if (modelid == array[i])
                {
                    flag = true;
                    break;
                }
            }
            return flag;
        }
        public string GetModelIcon()
        {
            string iconstr = Eval("ItemIcon").ToString();
            return StringHelper.GetItemIcon(iconstr);
        }
        //可浏览篇数切割
        public string GetViewVl(string Id)
        {
            //M_Node node = nodeBll.GetNodeXML(NodeID);
            //string[] vimi = { };
            //try
            //{
            //    vimi = node.Viewinglimit.Split('|');
            //}
            //catch (Exception)
            //{
            //    return "0";
            //}
            //string[] Valu = { };
            //for (int i = 0; i < vimi.Length; i++)
            //{
            //    Valu = vimi[i].Split('=');
            //    if (Id == Valu[0])
            //    {
            //        return Valu[1];
            //    }
            //}
            //return "0";
            return "0";
        }
        public bool getbool(object ss)
        {
            int sss = DataConverter.CLng(ss);
            if (sss == 1)
                return true;
            else
                return false;
        }
        //用户组权限验证
        public bool GroupAuth(string authName)
        {
            int gid = DataConverter.CLng(Eval("GroupID"));
            if (NodeID < 1 || GroupAuthDT.Rows.Count < 1) { return false; }
            return GroupAuthDT.Select("GroupID=" + gid + " AND " + authName + "=1").Length > 0;
        }
        //将其父节点的浏览权限添加进去
        protected void IsChild(M_UserPromotions ups)
        {
            M_UserPromotions upsMod = new M_UserPromotions();
            int pid = nodeBll.SelReturnModel(ups.NodeID).ParentID;
            if (pid > 0)
            {
                upsMod = psll.GetSelect(pid, ups.GroupID);
                if (upsMod == null) return;
                if (upsMod.look == 0) { upsMod.look = 1; upsMod.addTo = 1; psll.GetUpdate(upsMod); }
                IsChild(upsMod);
            }
        }

        protected void Cancel_B_Click(object sender, EventArgs e)
        {
            if (ViewMode.Equals("design")) { Response.Redirect("DesignNodeManage.aspx"); }
            else if (ViewMode.Equals("child")) { Response.Redirect("DesignNodeList.aspx?pid=" + Request.QueryString["pid"]); }
            else { Response.Redirect("NodeManage.aspx"); }
        }
    }
}