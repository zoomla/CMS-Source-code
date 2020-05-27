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
    using ZoomLa.Model;
    using ZoomLa.Common;
    using ZoomLa.Components;
    using ZoomLa.BLL.Page;
    using ZoomLa.Model.Page;

    public partial class _Default : System.Web.UI.Page
    {
        private int m_t;
        private B_ModelField mll = new B_ModelField();
        public B_PageReg b_PageReg = new B_PageReg();
        public M_PageReg m_PageReg = new M_PageReg();
        public B_User buser = new B_User();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.m_t = DataConverter.CLng(Request.QueryString["t"]);
                if (this.m_t == 0)
                {
                    this.m_t = 1;
                }
                M_UserInfo mu = buser.GetLogin();
                DataTable cmdinfo = mll.SelectTableName("ZL_Pagereg", "TableName like 'ZL_Reg_%' and UserName='" + mu.UserName + "'");
                // 判断注册状态，根据状态跳转
                m_PageReg = b_PageReg.GetSelectByUserID(mu.UserID);
                if (m_PageReg == null||m_PageReg.Status == 0)
                {
                    Response.Redirect("PageInfo.aspx");
                }
            }
        }
        public int Tnum
        {
            get { return this.m_t; }
        }
    }
}