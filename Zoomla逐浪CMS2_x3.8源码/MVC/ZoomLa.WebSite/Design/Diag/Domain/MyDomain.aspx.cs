namespace ZoomLaCMS.Design.Diag.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using ZoomLa.BLL;
    using ZoomLa.BLL.Site;
    using ZoomLa.Common;
    using ZoomLa.Components;
    using ZoomLa.Model;
    public partial class MyDomain : System.Web.UI.Page
    {
        B_User buser = new B_User();
        B_IDC_DomainList domBll = new B_IDC_DomainList();
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!SiteConfig.SiteOption.DomainRoute.Equals("1")) { function.WriteErrMsg("管理员未开启域名路由功能"); }
            B_User.CheckIsLogged("/Design/");
            if (!IsPostBack)
            {
                MyBind();
            }
        }
        private void MyBind()
        {
            M_UserInfo mu = buser.GetLogin();
            DataTable dt = domBll.SelWithUserByID(mu.UserID);
            if (dt.Rows.Count < 1) { Response.Redirect("AddDomain.aspx"); return; }
            EGV.DataSource = dt;
            EGV.DataBind();
        }
        protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            EGV.PageIndex = e.NewPageIndex;
            MyBind();
        }
        protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "del2":
                    int id = Convert.ToInt32(e.CommandArgument);
                    break;
            }
            MyBind();
        }
    }
}