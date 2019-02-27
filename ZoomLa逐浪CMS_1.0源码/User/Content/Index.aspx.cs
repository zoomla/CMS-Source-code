namespace ZoomLa.WebSite.User.Content
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
    using ZoomLa.Common;
    public partial class _Index : System.Web.UI.Page
    {
        private int m_t;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                B_User buser = new B_User();
                buser.CheckIsLogin();
                this.m_t = DataConverter.CLng(Request.QueryString["t"]);
                if (this.m_t == 0)
                    this.m_t = 1;                
            }
        }
        public int Tnum
        {
            get { return this.m_t; }
        }
    }
}