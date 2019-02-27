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

    public partial class GroupManage : System.Web.UI.Page
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
                GdvBind();
            }
        }
        private void GdvBind()
        {
            DataTable dt = this.bll.GetGroupList();
            if (dt.Rows.Count == 0)
            {
                this.nocontent.Visible = true;
            }
            else
            {
                this.nocontent.Visible = false;
                this.Gdv.DataSource = dt;
                this.Gdv.DataKeyNames = new string[] { "GroupID" };
                this.Gdv.DataBind();
            }
        }
        //绑定分页
        protected void Gdv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.Gdv.PageIndex = e.NewPageIndex;
            GdvBind();
        }
        public string GetRegStatus(string flag)
        {
            string re="";
            if (DataConverter.CBool(flag))
            {
                re = "<font color=green>√</font>";
            }
            else
            {
                re = "<font color=red>×</font>";

            }
            return re;
        }
        protected void Lnk_Click(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
                Response.Redirect("Group.aspx?id=" + e.CommandArgument.ToString());
            if (e.CommandName == "Del")
            {
                this.bll.Del(DataConverter.CLng(e.CommandArgument.ToString()));
                GdvBind();
            }
            if (e.CommandName == "Set")
            {
                Response.Redirect("GroupConfig.aspx?id=" + e.CommandArgument.ToString());
            }
        }
    }
}