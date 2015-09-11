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

    public partial class Group : System.Web.UI.Page
    {
        private B_Group bll = new B_Group();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                B_Admin badmin = new B_Admin();
                badmin.CheckMulitLogin();
                if (!badmin.ChkPermissions("UserGroup"))
                {
                    function.WriteErrMsg("没有权限进行此项操作");
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
                    this.TxtGroupName.Text = info.GroupName;
                    this.TxtDescription.Text = info.Description;
                    this.RBLReg.SelectedValue = DataConverter.CLng(info.RegSelect).ToString();
                }
            }
        }
        protected void EBtnSubmit_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                M_Group info = new M_Group();
                info.GroupID = DataConverter.CLng(this.HdnGroupID.Value);
                info.GroupName = this.TxtGroupName.Text.Trim();
                info.Description = this.TxtDescription.Text.Trim();
                info.RegSelect = DataConverter.CBool(this.RBLReg.SelectedValue);
                if (info.GroupID == 0)
                    this.bll.Add(info);
                else
                    this.bll.Update(info);
                Response.Redirect("GroupManage.aspx");
            }
        }
}
}