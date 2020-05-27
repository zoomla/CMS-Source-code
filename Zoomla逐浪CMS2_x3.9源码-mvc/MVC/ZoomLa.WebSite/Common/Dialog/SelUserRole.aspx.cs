namespace ZoomLaCMS.Common.Dialog
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using ZoomLa.BLL;
    public partial class SelUserRole : System.Web.UI.Page
    {
        B_Admin badmin = new B_Admin();
        B_Permission perBll = new B_Permission();
        protected void Page_Load(object sender, EventArgs e)
        {
            B_Admin.CheckIsLogged();
            if (!IsPostBack)
            {
                MyBind();
            }
        }
        private void MyBind()
        {
            DataTable dt = perBll.Select_All();
            EGV.DataSource = dt;
            EGV.DataBind();
        }
        protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
        protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            EGV.PageIndex = e.NewPageIndex;
            DataBind();
        }
    }
}