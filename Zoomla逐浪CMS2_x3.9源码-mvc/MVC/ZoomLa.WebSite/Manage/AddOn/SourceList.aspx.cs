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
using ZoomLa.Common;
using ZoomLa.BLL;


namespace ZoomLaCMS.Manage.AddOn
{
    public partial class SourceList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            function.AccessRulo();
            B_Admin badmin = new B_Admin();
            badmin.CheckIsLogin();
            string str = "";
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["flag"] != null)
                {
                    str = Request.QueryString["flag"].Trim();
                    InitPage(str);
                }
            }

        }
        public void InitPage(string str)
        {
            if ("source" == str)
            {
                f1.Visible = true;
                f2.Visible = false;
            }
            else if ("author" == str)
            {
                f1.Visible = false;
                f2.Visible = true;
            }
            else if ("keyword" == str)
            {
                f3.Visible = true;
            }
        }
    }
}