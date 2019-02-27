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
    
    using ZoomLa.Model;
    using ZoomLa.Components;

    public partial class MyComment : System.Web.UI.Page
    {
        private B_Content bll = new B_Content();
        private B_User buser = new B_User();
        private B_Comment bfav = new B_Comment();
        public int NodeID;        
        public M_UserInfo UserInfo;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                buser.CheckIsLogin();
                this.LblSiteName.Text = SiteConfig.SiteInfo.SiteName;
                string uname = HttpContext.Current.Request.Cookies["UserState"]["LoginName"];
                this.UserInfo = buser.GetUserByName(uname);
                if (string.IsNullOrEmpty(base.Request.QueryString["NodeID"]))
                {
                    this.NodeID = 0;
                }
                else
                {
                    this.NodeID = DataConverter.CLng(base.Request.QueryString["NodeID"]);
                }
                RepNodeBind();
            }
        }
        /// <summary>
        /// 绑定数据到GridView
        /// </summary>
        private void RepNodeBind()
        {
            this.Egv.DataSource = this.bfav.SearchByUser(this.UserInfo.UserID, this.NodeID);
            this.Egv.DataKeyNames = new string[] { "CommentID" };
            this.Egv.DataBind();
        }
        /// <summary>
        /// GridView 翻页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Egv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.Egv.PageIndex = e.NewPageIndex;
            this.RepNodeBind();
        }
        public string GetUrl(string infoid)
        {
            int p = DataConverter.CLng(infoid);
            M_CommonData cinfo = this.bll.GetCommonData(p);
            if (cinfo.IsCreate == 1)
                return SiteConfig.SiteInfo.SiteUrl + cinfo.HtmlLink;
            else
                return "~/Content.aspx?ItemID=" + p;
        }
        public string GetTitle(string infoid)
        {
            int p = DataConverter.CLng(infoid);
            M_CommonData cinfo = this.bll.GetCommonData(p);
            return cinfo.Title;
        }
    }
}