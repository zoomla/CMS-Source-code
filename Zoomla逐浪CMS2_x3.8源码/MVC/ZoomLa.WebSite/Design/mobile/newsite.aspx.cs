namespace ZoomLaCMS.Design.mobile
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using ZoomLa.BLL;
    using ZoomLa.BLL.Design;
    using ZoomLa.Common;
    using ZoomLa.Model;
    using ZoomLa.Model.Design;
    using ZoomLa.SQLDAL;
    public partial class newsite : System.Web.UI.Page
    {
        B_User buser = new B_User();
        B_Design_MBSite mbBll = new B_Design_MBSite();
        public int TlpID { get { return DataConvert.CLng(Request.QueryString["TlpID"]); } }
        public string SiteName { get { return Request.QueryString["SiteName"] ?? ""; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Redirect("addsite.aspx?tlpid=" + TlpID);
        }
    }
}