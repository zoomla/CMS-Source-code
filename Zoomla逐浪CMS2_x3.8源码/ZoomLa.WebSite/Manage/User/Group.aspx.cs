namespace ZoomLa.WebSite.Manage.User
{
    using System;
    using System.Data;
    using System.Configuration;
    using System.Collections;
    using System.Web;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Web.UI.WebControls.WebParts;
    using System.Web.UI.HtmlControls;
    using ZoomLa.BLL;
    using ZoomLa.Model;
    using ZoomLa.Common;
    using System.Xml;

    public partial class Group : CustomerPageAction
    {
        private B_Group bll = new B_Group();
        private B_Model mll = new B_Model();
        private B_Structure strBll = new B_Structure();
        private M_Structure strMod = new M_Structure();

        protected void Page_Load(object sender, EventArgs e)
        {
            Call.SetBreadCrumb(Master, "<li>"+Resources.L.后台管理 + "</li><li>" + Resources.L.会员管理 + "</li><li><a href=\"GroupManage.aspx\">" + Resources.L.会员组管理 + "</a></li><li><asp:Literal Text='" + Resources.L.添加会员组 + "'>" + LNav_Hid.Value + "</asp:Literal></li>");
            B_Admin badmin = new B_Admin();
            if (!this.Page.IsPostBack)
            {
                if (!B_ARoleAuth.Check(ZLEnum.Auth.user, "UserGroup"))
                {
                    function.WriteErrMsg(Resources.L.没有权限进行此项操作);
                }
                string id = base.Request.QueryString["id"];
                if (!string.IsNullOrEmpty(id))
                {
                    this.HdnGroupID.Value = id;
                }
                else
                {
                    this.HdnGroupID.Value = "0";
                    id = "0";
                }
                if (id != "0")
                {
                    M_Group info = this.bll.GetByID(DataConverter.CLng(id));
                    if (info.ParentGroupID != 0)
                    {
                        M_Group infoP = bll.GetByID(info.ParentGroupID);
                        Label1.Text = infoP.GroupName;
                    }
                    else
                        Label1.Text = Resources.L.系统会员组;
                    this.TxtGroupName.Text = info.GroupName;
                    this.TxtDescription.Text = info.Description;
                    this.RBLReg.Checked = info.RegSelect;
                    this.UpGradeMoney.Text = info.UpGradeMoney.ToString();
                    UpSIcon_T.Text = info.UpSIcon.ToString();
                    UpPoint_T.Text = info.UpPoint.ToString();
                    this.RBcompanyG.Checked = info.CompanyGroup == 1 ? true : false;
                    this.RBVipG.Checked = info.VIPGroup==1?true:false;
                    this.txtVIPNum.Text = info.VIPNum.ToString();
                    this.OtherName.Text = info.OtherName;
                    this.txtRebateRate.Text = info.RebateRate.ToString();
                    this.txtCreit.Text = info.Credit.ToString();
                    this.txt_Enroll.Checked = info.Enroll.Contains("isenroll") ? true : false;
                    foreach (ListItem item in ClassEnroll_Radio.Items)
                    {
                        if (!string.IsNullOrEmpty(item.Value)&&info.Enroll.Contains(item.Value))
                        {
                            item.Selected = true;
                            break;
                        }
                    }
                }
                if (!string.IsNullOrEmpty(Request.QueryString["ParentID"]))
                {
                    M_Group info = this.bll.GetByID(DataConverter.CLng(Request.QueryString["ParentID"]));
                    Label1.Text = info.GroupName;
                }
                else
                    Label1.Text = Resources.L.系统会员组;
            }
            int sid = DataConverter.CLng(base.Request.QueryString["id"]);
            int ruleid = DataConverter.CLng(base.Request.QueryString["ruleid"]);//ruleid
            string menu = Request.QueryString["menu"];
            if (menu == "ruledel")
            {
                DeleteUserGroup(ruleid.ToString(), sid);
                Response.Redirect("Group.aspx?id=" + sid + "UserGroups");
            }
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>"+ Resources.L.工作台 + "</a></li><li><a href='" + customPath2 + "User/UserManage.aspx'>" + Resources.L.会员管理 + "</a></li><li><a href='GroupManage.aspx'>" + Resources.L.会员组管理 + "</a></li><li class='active'>" + Resources.L.添加会员组 + "</li>");
        }
        protected void EBtnSubmit_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                int GroupID = DataConverter.CLng(this.HdnGroupID.Value);
                M_Group info = new M_Group();
                if (GroupID == 0)
                {
                    info.GroupID = 0;
                    info.GroupName = this.TxtGroupName.Text.Trim();
                    info.Description = this.TxtDescription.Text.Trim();
                    info.RegSelect = RBLReg.Checked;
                    info.UpGradeMoney = DataConverter.CDouble(UpGradeMoney.Text);
                    info.UpSIcon = DataConverter.CDouble(UpSIcon_T.Text);
                    info.UpPoint = DataConverter.CDouble(UpPoint_T.Text);
                    info.OtherName = this.OtherName.Text;
                    info.CompanyGroup = RBcompanyG.Checked ? 1 : 0;
                    info.RebateRate=DataConverter.CFloat(this.txtRebateRate.Text.Trim());
                    info.Credit = DataConverter.CLng(txtCreit.Text);
                    info.Enroll = "";
                    if (txt_Enroll.Checked)
                        info.Enroll = "isenroll";
                    if (!string.IsNullOrEmpty(ClassEnroll_Radio.SelectedValue))
                        info.Enroll += ","+ ClassEnroll_Radio.SelectedValue;
                    info.Enroll = info.Enroll.Trim(',');
                    if (!string.IsNullOrEmpty(Request.QueryString["ParentID"]))
                    {
                        info.ParentGroupID = DataConverter.CLng(Request.QueryString["ParentID"]);
                    }
                    else
                    {
                        info.ParentGroupID = 0;
                    }
                    if (!this.bll.HasGroup())
                        info.IsDefault = true;
                    else
                        info.IsDefault = false;
                    this.bll.Add(info);
                    info.VIPGroup = RBVipG.Checked ? 1 : 0;
                    info.VIPNum = Convert.ToInt32(this.txtVIPNum.Text.ToString());
                }
                else
                {
                    info = this.bll.GetByID(GroupID);
                    info.GroupName = this.TxtGroupName.Text.Trim();
                    info.Description = this.TxtDescription.Text.Trim();
                    info.OtherName = this.OtherName.Text;
                    info.RegSelect = RBLReg.Checked;
                    info.UpGradeMoney = DataConverter.CDouble(this.UpGradeMoney.Text);
                    info.UpSIcon = DataConverter.CDouble(UpSIcon_T.Text);
                    info.UpPoint = DataConverter.CDouble(UpPoint_T.Text);
                    info.CompanyGroup = RBcompanyG.Checked ? 1 : 0;
                    info.VIPGroup = RBVipG.Checked ? 1 : 0;
                    info.VIPNum = Convert.ToInt32(this.txtVIPNum.Text.ToString());
                    info.RebateRate = DataConverter.CFloat(this.txtRebateRate.Text.Trim());
                    info.Credit = DataConverter.CLng(this.txtCreit.Text);
                    info.Enroll = "";
                    if (txt_Enroll.Checked)
                        info.Enroll = "isenroll";
                    if (!string.IsNullOrEmpty(ClassEnroll_Radio.SelectedValue))
                        info.Enroll += "," + ClassEnroll_Radio.SelectedValue;
                    info.Enroll = info.Enroll.Trim(',');
                    this.bll.Update(info);
                }
                function.WriteSuccessMsg(Resources.L.修改成功, "GroupManage.aspx");
            }
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            int sid = DataConverter.CLng(base.Request.QueryString["id"]);
            string Item = Request.Form["Item"] == null ? "" : Request.Form["Item"];
            DeleteUserGroup(Item, sid);
            Response.Redirect("Group.aspx?id=" + sid);
        }
        #region 返回权限
        /// <summary>
        /// 返回权限
        /// </summary>
        /// <param name="pagelist"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        protected string Getpagepre(string pagelist, string type)
        {
            DataSet ds = function.XmlToTable(pagelist);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (DataConverter.CLng(ds.Tables[0].Rows[0][type]) == 1)
                {
                    return "<font color=green>"+ Resources.L.开放 + "</font>";
                }
                else
                {
                    return "<font color=red>" + Resources.L.关闭 + "</font>";
                }

            }
            return "";

        }
        #endregion
        #region 角色中删除用户组
        /// <summary>
        /// 角色中删除用户组
        /// </summary>
        /// <param name="ruleid">角色组</param>
        /// <param name="UserGroup">用户组ID</param>
        /// <returns></returns>
        protected void DeleteUserGroup(string ruleid, int UserGroup)
        {
            string Grouplist = "";
            if (ruleid != "")//权限组不为空
            {
                ruleid = ruleid.TrimEnd(new char[] { ',' });
                ruleid = ruleid.TrimStart(new char[] { ',' });

                if (ruleid.IndexOf(",") > -1)//多组
                {
                    string[] Rgroups = ruleid.Split(new string[] { "," }, StringSplitOptions.None);//角色
                    for (int i = 0; i < Rgroups.Length; i++)
                    {
                        strMod= strBll.SelReturnModel(DataConverter.CLng(Rgroups[i]));

                        //Grouplist = psinfo.UserGroup;

                        if (Grouplist.IndexOf("," + UserGroup + ",") > -1)
                        {
                            Grouplist = Grouplist.Replace("," + UserGroup + ",", ",");
                        }

                        if (Grouplist.Substring(0, 1) == ",")
                        {
                            Grouplist = Grouplist.Substring(1, Grouplist.Length - 1);
                        }

                        if (Grouplist != "")
                        {
                            if (Grouplist.Substring(Grouplist.Length - 1, 1) == ",")
                            {
                                Grouplist = Grouplist.Substring(0, Grouplist.Length - 1);
                            }
                        }
                        Grouplist = "," + Grouplist + ",";
                        //psinfo.UserGroup = Grouplist;
                        //pll.GetUpdate(psinfo);
                    }


                }
                else//单个
                {
                    strMod = strBll.SelReturnModel(DataConverter.CLng(ruleid));

                    //Grouplist = psinfo.UserGroup;

                    Grouplist = "," + Grouplist + ",";
                    if (Grouplist.IndexOf("," + UserGroup + ",") > -1)
                    {
                        Grouplist = Grouplist.Replace("," + UserGroup + ",", ",");
                        if (Grouplist == ",") { Grouplist = ""; }
                    }

                    Grouplist = Grouplist.TrimEnd(new char[] { ',' });
                    Grouplist = Grouplist.TrimStart(new char[] { ',' });


                    //psinfo.UserGroup = Grouplist;
                    //pll.GetUpdate(psinfo);
                }


            }
        }
        #endregion
    }
}