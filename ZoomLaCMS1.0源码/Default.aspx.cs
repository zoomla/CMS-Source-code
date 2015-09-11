namespace ZoomLa.WebSite
{
    using System;
    using System.Data;
    using System.Configuration;
    using System.Web;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Web.UI.WebControls.WebParts;
    using System.Web.UI.HtmlControls;
    using ZoomLa.Components;
    using ZoomLa.BLL;
    using ZoomLa.Common;
    using System.Web.Configuration;
    public partial class _Default : System.Web.UI.Page
    {
        protected B_CreateHtml bll = new B_CreateHtml();
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckInstalled();
            string IndexDir = SiteConfig.SiteOption.IndexTemplate;
            IndexDir = base.Request.PhysicalApplicationPath +SiteConfig.SiteOption.TemplateDir+ IndexDir;
            IndexDir = IndexDir.Replace("/", @"\");
            if (!FileSystemObject.IsExist(IndexDir, FsoMethod.File))
                Response.Write("[产生错误的可能原因：您访问的内容信息不存在！]");
            else
            {
                string IndexHtml = this.bll.CreateHtml(FileSystemObject.ReadFile(IndexDir), 0, 0);
                Response.Write(IndexHtml);
            }
        }
        private void CheckInstalled()
        {
            string str = WebConfigurationManager.AppSettings["Installed"].ToLower();
            if (!Convert.ToBoolean(str))
            {
                Page.Response.Redirect("Install/Default.aspx");
            }
        }
    }
}