using System.Data;
using System;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;
using System.Xml;
using ZoomLa.Components;
using Newtonsoft.Json;

namespace ZoomLaCMS.Manage.I.ASCX
{
    public partial class BatchNode : CustomerPageAction
    {
        B_Admin badmin = new B_Admin();
        B_Group gpBll = new B_Group();
        B_Node nodeBll = new B_Node();
        B_Model bllmodel = new B_Model();
        B_NodeRole bnll = new B_NodeRole();
        B_UserPromotions psll = new B_UserPromotions();
        public DataTable UPromoDT
        {
            get
            {
                return ViewState["UPromoDT"] as DataTable;
            }
            set
            {
                ViewState["UPromoDT"] = value;
            }
        }
        public int NodeID { get { return DataConverter.CLng(Request.QueryString["NodeID"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                B_ARoleAuth.CheckEx(ZLEnum.Auth.model, "NodeEdit");
                Call.SetBreadCrumb(Master, "<li><a href='../Config/SiteOption.aspx'>系统设置</a></li><li><a href='NodeManage.aspx'>节点管理</a></li><li class=\"active\">节点批量设置</li>" + Call.GetHelp(16));
                UPromoDT = psll.Select_All();
                //绑定模型
                DataTable dt = this.bllmodel.GetList();
                this.Model_RPT.DataSource = dt;
                this.Model_RPT.DataBind();

                //绑定节点
                this.LstNodes.DataSource = nodeBll.CreateForListBox();
                this.LstNodes.DataTextField = "NodeName";
                this.LstNodes.DataValueField = "NodeID";
                this.LstNodes.DataBind();

                //绑定权限
                DataTable Rt = B_Role.GetRoleName();
                this.Egv.DataSource = Rt;
                this.Egv.DataBind();

                this.GridView1.DataSource = gpBll.GetGroupList();
                this.GridView1.DataBind();

                //用户组
                DataTable tinfo = gpBll.GetGroupList();
                for (int i = 0; i < tinfo.Rows.Count; i++)
                {
                    ViewGroup.Items.Add(new ListItem(tinfo.Rows[i]["GroupName"].ToString(), tinfo.Rows[i]["GroupID"].ToString()));
                    ViewGroup2.Items.Add(new ListItem(tinfo.Rows[i]["GroupName"].ToString(), tinfo.Rows[i]["GroupID"].ToString()));
                    ViewGroup3.Items.Add(new ListItem(tinfo.Rows[i]["GroupName"].ToString(), tinfo.Rows[i]["GroupID"].ToString()));
                }
            }

        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            UPromoDT = null;
        }
        protected void EBtnBacthSet_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.LstNodes.Items.Count; i++)
            {
                if (!this.LstNodes.Items[i].Selected) continue;
                int nodetype = 0;
                //节点信息
                int nodeid = DataConverter.CLng(this.LstNodes.Items[i].Value);
                M_Node node = nodeBll.GetNodeXML(nodeid);
                if (this.ChkOpenType.Checked)
                {
                    node.OpenTypeTrue = this.RBLOpenType.SelectedValue;
                }
                if (this.ChkItemOpen.Checked)
                {
                    node.ItemOpenTypeTrue = this.RBLItemOpenType.SelectedValue;
                }
                if (this.ChkPurview.Checked)
                {
                    node.PurviewType = DataConverter.CBool(this.RBLPurviewType.SelectedValue);
                }
                if (this.ChkComment.Checked)
                {
                    node.CommentType = this.RBLCommentType.SelectedValue;
                }
                if (this.ChkHits.Checked)
                {
                    node.HitsOfHot = DataConverter.CLng(this.TxtHitsOfHot.Text);
                }
                if (this.Bat_isSimple_Chk.Checked)
                {
                    node.Contribute = isSimple_CH.Checked ? 1 : 0;
                }
                if (this.Bat_SafeGuard_Chk.Checked)
                {
                    node.SafeGuard = SafeGuard.Checked ? 1 : 0;
                }
                if (this.ChkClickTimeout.Checked)
                {
                    node.ClickTimeout = DataConverter.CLng(this.ClickTimeout.Text);
                }
                if (this.ChkTemp.Checked)
                {
                    node.ListTemplateFile = TxtTemplate_hid.Value;
                }
                if (this.ChkITemp.Checked)
                {
                    node.IndexTemplate = TxtIndexTemplate_hid.Value;
                }
                if (this.Chk1Temp.Checked)
                {
                    node.LastinfoTemplate = LastinfoTemplate_hid.Value;
                }
                if (this.Chk2Temp.Checked)
                {
                    node.HotinfoTemplate = HotinfoTemplate_hid.Value;
                }
                if (this.Chk3Temp.Checked)
                {
                    node.ProposeTemplate = ProposeTemplate_hid.Value;
                }
                if (this.ChkModelID.Checked)
                {
                    string modellist = this.Page.Request.Form["ChkModel"];
                    node.ContentModel = modellist;
                    string[] ModelArr = modellist.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    this.nodeBll.DelModelTemplate(node.NodeID, modellist);
                    for (int j = 0; j < ModelArr.Length; j++)
                    {
                        if (bllmodel.GetModelById(DataConverter.CLng(ModelArr[j])).ModelType == 2)
                        {
                            if (nodetype == 1)
                            {
                                nodetype = 3;
                            }
                            else if (nodetype == 2)
                            {
                                nodetype = 2;
                            }
                            else
                            {
                                nodetype = 2;
                            }
                            if (nodetype == 5)
                            {
                                nodetype = 5;
                            }
                        }
                        else if (bllmodel.GetModelById(DataConverter.CLng(ModelArr[j])).ModelType == 1)
                        {
                            if (nodetype == 5)
                            {
                                nodetype = 5;
                            }
                            else if (nodetype == 1)
                            {
                                nodetype = 1;
                            }
                            else if (nodetype == 2)
                            {
                                nodetype = 3;
                            }
                            else
                            {
                                nodetype = 1;
                            }
                        }
                        else if (bllmodel.GetModelById(DataConverter.CLng(ModelArr[j])).ModelType == 5)
                        {
                            nodetype = 5;
                        }

                        if (!string.IsNullOrEmpty(this.Page.Request.Form["TxtModelTemplate_" + ModelArr[j]].Trim()))
                        {
                            //将模型模板设定写入数据库
                            string temp = this.Page.Request.Form["TxtModelTemplate_" + ModelArr[j]].Trim();
                            if (this.nodeBll.IsExistTemplate(node.NodeID, DataConverter.CLng(ModelArr[j])))
                            {
                                this.nodeBll.UpdateModelTemplate(node.NodeID, DataConverter.CLng(ModelArr[j]), temp);
                            }
                            else
                            {
                                this.nodeBll.AddModelTemplate(node.NodeID, DataConverter.CLng(ModelArr[j]), temp);
                            }
                        }
                    }
                    node.NodeListType = nodetype;
                }
                if (this.ChkListEx.Checked)
                {
                    node.ListPageHtmlEx = DataConverter.CLng(this.RBLListEx.SelectedValue);
                }
                if (this.ChkListPageEx.Checked)
                {
                    node.ListPageEx = DataConverter.CLng(this.ListPageEx.SelectedValue);
                }
                if (this.ChkLastinfoPageEx.Checked)
                {
                    node.LastinfoPageEx = DataConverter.CLng(this.LastinfoPageEx.SelectedValue);
                }
                if (this.ChkHotinfoPageEx.Checked)
                {
                    node.HotinfoPageEx = DataConverter.CLng(this.HotinfoPageEx.SelectedValue);
                }
                if (this.ChkProposePageEx.Checked)
                {
                    node.ProposePageEx = DataConverter.CLng(this.ProposePageEx.SelectedValue);
                }

                if (this.ChkContentEx.Checked)
                {
                    node.ContentFileEx = DataConverter.CLng(this.RBLContentEx.SelectedValue);
                }
                if (this.ChkPosition.Checked)
                {
                    node.HtmlPosition = DataConverter.CLng(this.RBLPosition.SelectedValue);
                }
                if (this.ChkContentRule.Checked)
                {
                    node.ContentPageHtmlRule = DataConverter.CLng(this.DDLContentRule.SelectedValue);
                }
                if (this.ChkConsumePoint.Checked)
                {
                    node.ConsumePoint = DataConverter.CLng(this.TxtConsumePoint.Text);
                }
                if (this.ChkPay.Checked)
                {
                    this.TxtAddMoney.Text = node.AddMoney.ToString();
                    this.TxtAddPoint.Text = node.AddPoint.ToString();
                    this.txtAddExp.Text = node.AddUserExp.ToString();
                }
                if (this.ChkDeducExp.Checked)
                {
                    this.txtDeducExp.Text = node.DeducUserExp.ToString();
                }
                if (this.ChConsumeType.Checked)
                {
                    switch (node.ConsumeType)
                    {
                        case 0:
                            this.Radio1.Checked = true;
                            break;
                        case 1:
                            this.Radio2.Checked = true;
                            break;
                        case 2:
                            this.Radio3.Checked = true;
                            break;
                        case 3:
                            this.Radio4.Checked = true;
                            break;
                        case 4:
                            this.Radio5.Checked = true;
                            break;
                        default:
                            this.Radio6.Checked = true;
                            break;
                    }
                    this.TxtConsumeCount.Text = node.ConsumeCount.ToString();
                    this.TxtConsumeTime.Text = node.ConsumeTime.ToString();
                }
                if (this.ChkBGroup.Checked)
                {
                    BGroup.DataSource = gpBll.GetGroupList();
                    BGroup.DataBind();
                }
                if (this.ChkShares.Checked)
                {
                    this.TxtShares.Text = node.Shares.ToString();
                }
                #region 前台权限,需整理
                if (this.ChkUserView.Checked)
                {
                    DataTable auitdt = nodeBll.GetNodeAuitDT();
                    DataRow auitdr = auitdt.Rows[0];
                    auitdr["View"] = UserAuit_RadioList.SelectedValue;
                    string ViewGroupvalue = "";
                    foreach (ListItem dd in this.ViewGroup.Items)
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
                    auitdr["ViewGroup"] = ViewGroupvalue;
                    string ViewGroup2value = "";
                    foreach (ListItem dd in this.ViewGroup2.Items)
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
                    auitdr["ViewSunGroup"] = ViewGroup2value;
                    string ViewGroup3value = "";
                    foreach (ListItem dd in this.ViewGroup3.Items)
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
                    auitdr["input"] = ViewGroup3value;
                    string forumvalue = "";
                    foreach (ListItem dd in this.forum.Items)
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
                    auitdr["forum"] = forumvalue;
                    node.Purview = JsonConvert.SerializeObject(auitdt);
                }
                #endregion
                this.nodeBll.UpdateNode(node);
                #region 角色
                if (NodeRole.Checked)
                {
                    DataTable nrDT = B_Role.SelectNodeRoleNode(nodeid);
                    for (int ii = 0; ii <= Egv.Rows.Count - 1; ii++)
                    {
                        CheckBox cbox1 = (CheckBox)Egv.Rows[ii].FindControl("chkSel1");
                        CheckBox cbox2 = (CheckBox)Egv.Rows[ii].FindControl("chkSel2");
                        CheckBox cbox3 = (CheckBox)Egv.Rows[ii].FindControl("chkSel3");
                        CheckBox cbox4 = (CheckBox)Egv.Rows[ii].FindControl("chkSel4");
                        CheckBox cbox5 = (CheckBox)Egv.Rows[ii].FindControl("chkSel5");
                        CheckBox cbox6 = (CheckBox)Egv.Rows[ii].FindControl("chkSel6");
                        if (cbox1.Checked || cbox2.Checked || cbox3.Checked || cbox4.Checked || cbox5.Checked || cbox6.Checked)
                        {
                            M_NodeRole nr = bnll.SelModelByRidAndNid(nodeid, Convert.ToInt32(Egv.DataKeys[ii].Value));
                            if (nr == null) { nr = new M_NodeRole(); nr.NID = nodeid; nr.RID = Convert.ToInt32(Egv.DataKeys[ii].Value); }
                            if (cbox1.Checked)
                                nr.look = 1;
                            else
                                nr.look = 0;
                            if (cbox2.Checked)
                                nr.Modify = 1;
                            else
                                nr.Modify = 0;
                            if (cbox3.Checked)
                                nr.State = 1;
                            else
                                nr.State = 0;
                            if (cbox4.Checked)
                                nr.addTo = 1;
                            else
                                nr.addTo = 0;
                            if (cbox5.Checked)
                                nr.Columns = 1;
                            else
                                nr.Columns = 0;
                            if (cbox6.Checked)
                                nr.Comments = 1;
                            else
                                nr.Comments = 0;
                            DataRow[] drs = nrDT.Select("Rid='" + nr.RID + "' And Nid='" + nodeid + "'");
                            if (drs != null && drs.Length > 0)
                                nr.RN_ID = Convert.ToInt32(drs[0]["RN_ID"]);
                            else nr.RN_ID = 0;
                            bnll.InsertUpdate(nr);
                        }
                        else
                        {
                            DataTable nn = bnll.GetSelectNodeANDRid(nodeid, Egv.DataKeys[ii].Value + "");
                            if (nn != null && nn.Rows.Count > 0)
                            {
                                bnll.GetDelete(DataConverter.CLng(nn.Rows[0]["RN_ID"]));
                            }
                        }
                    }
                }

                if (this.NodeURole.Checked)
                {
                    for (int ij = 0; ij <= GridView1.Rows.Count - 1; ij++)
                    {
                        CheckBox cbox1 = (CheckBox)GridView1.Rows[ij].FindControl("chkSel1");
                        CheckBox cbox2 = (CheckBox)GridView1.Rows[ij].FindControl("chkSel2");
                        CheckBox cbox3 = (CheckBox)GridView1.Rows[ij].FindControl("chkSel3");
                        CheckBox cbox4 = (CheckBox)GridView1.Rows[ij].FindControl("chkSel4");
                        CheckBox cbox5 = (CheckBox)GridView1.Rows[ij].FindControl("chkSel5");
                        CheckBox cbox6 = (CheckBox)GridView1.Rows[ij].FindControl("chkSel6");
                        CheckBox cbox7 = (CheckBox)GridView1.Rows[ij].FindControl("chkSel11");
                        CheckBox cbox8 = (CheckBox)GridView1.Rows[ij].FindControl("chkSel12");

                        //HiddenField hd = (HiddenField)GridView1.Rows[i].FindControl("HiddenGroupID");
                        //if (cbox1.Checked || cbox2.Checked || cbox3.Checked || cbox4.Checked || cbox5.Checked || cbox6.Checked)
                        //{
                        M_UserPromotions ups = new M_UserPromotions();

                        ups.GroupID = Convert.ToInt32(GridView1.DataKeys[ij].Value);

                        if (cbox1.Checked)
                            ups.look = 1;
                        else
                            ups.look = 0;

                        if (cbox2.Checked)
                            ups.addTo = 1;
                        else
                            ups.addTo = 0;

                        if (cbox3.Checked)
                            ups.Modify = 1;
                        else
                            ups.Modify = 0;

                        if (cbox4.Checked)
                            ups.Deleted = 1;
                        else
                            ups.Deleted = 0;

                        if (cbox5.Checked)
                            ups.Columns = 1;
                        else
                            ups.Columns = 0;

                        if (cbox6.Checked)
                            ups.Comments = 1;
                        else
                            ups.Comments = 0;

                        if (cbox7.Checked)
                            ups.Down = 1;
                        else
                            ups.Down = 0;

                        if (cbox8.Checked)
                            ups.quote = 1;
                        else
                            ups.quote = 0;
                        ups.NodeID = DataConverter.CLng(nodeid);
                        //ups.NodeID = DataConverter.CLng(this.HdnNodeID.Value);
                        psll.GetInsertOrUpdate(ups);
                    }
                }
                #endregion
            }
            function.WriteSuccessMsg("批量设置成功", customPath2 + "Content/NodeManage.aspx");
        }
        public string GetTemplate(string mid)
        {
            string result = "";
            result = this.bllmodel.GetModelById(DataConverter.CLng(mid)).ContentModule;
            return result;
        }
        public string GetViewVl(string Id)
        {
            M_Node node = this.nodeBll.GetNodeXML(NodeID);
            string[] vimi = { };
            try
            {
                vimi = node.Viewinglimit.Split('|');
            }
            catch (Exception)
            {
                return "0";
            }
            string[] Valu = { };
            for (int i = 0; i < vimi.Length; i++)
            {
                Valu = vimi[i].Split('=');
                if (Id == Valu[0])
                {
                    return Valu[1];
                }
            }
            return "0";
        }
        protected bool GetGrouplook(string GroupID)
        {
            return UPromoDT.Select("NodeID=" + NodeID + " And GroupID=" + DataConverter.CLng(GroupID) + " And look=1").Length == 0 ? false : true;
        }
        protected bool GetGroupDown(string GroupID)
        {
            return UPromoDT.Select("NodeID=" + NodeID + " And GroupID=" + DataConverter.CLng(GroupID) + " And Down=1").Length == 0 ? false : true;
        }
        protected bool GetGroupquote(string GroupID)
        {
            return UPromoDT.Select("NodeID=" + NodeID + " And GroupID=" + DataConverter.CLng(GroupID) + " And quote=1").Length == 0 ? false : true;
        }
        protected bool GetGroupAddto(string GroupID)
        {
            return UPromoDT.Select("NodeID=" + NodeID + " And GroupID=" + DataConverter.CLng(GroupID) + " And addTo=1").Length == 0 ? false : true;
        }
        protected bool GetGroupModify(string GroupID)
        {
            return UPromoDT.Select("NodeID=" + NodeID + " And GroupID=" + DataConverter.CLng(GroupID) + " And Modify=1").Length == 0 ? false : true;
        }
        protected bool GetGroupDelete(string GroupID)
        {
            return UPromoDT.Select("NodeID=" + NodeID + " And GroupID=" + DataConverter.CLng(GroupID) + " And Deleted=1").Length == 0 ? false : true;
        }
        protected bool GetGroupColumns(string GroupID)
        {
            return UPromoDT.Select("NodeID=" + NodeID + " And GroupID=" + DataConverter.CLng(GroupID) + " And Columns=1").Length == 0 ? false : true;
        }
        protected bool GetGroupComments(string GroupID)
        {
            return UPromoDT.Select("NodeID=" + NodeID + " And GroupID=" + DataConverter.CLng(GroupID) + " And Comments=1").Length == 0 ? false : true;
        }
    }
}