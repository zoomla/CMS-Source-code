namespace ZoomLaCMS.Plugins.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using ZoomLa.BLL;
    public partial class ViewUserLog : System.Web.UI.Page
    {
        B_Site_Log logBll = new B_Site_Log();
        B_User buser = new B_User();
        protected void Page_Load(object sender, EventArgs e)
        {
            B_User.CheckIsLogged();
            if (!IsPostBack)
            {
                DataBind();
            }
        }
        private void DataBind(string key = "")
        {
            EGV.DataSource = logBll.SelAllByUserID(buser.GetLogin().UserID, B_Site_Log.UserType.OwnUserID);
            EGV.DataBind();
        }
    }
}