namespace ZoomLa.WebSite.Manage.Template
{
    using System;
    using System.Data;
    using System.IO;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using ZoomLa.Common;
    using ZoomLa.Components;
    using ZoomLa.BLL;

    public partial class SelectTemplate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                B_Admin badmin = new B_Admin();
                badmin.CheckMulitLogin();
                string templateDir = SiteConfig.SiteOption.TemplateDir;
                string str2 = base.Request.QueryString["FilesDir"];
                string str3 = "";
                if (!string.IsNullOrEmpty(str2))
                {
                    str3 = str2.Replace("/", @"\");
                }
                string str4 = "";
                if (!string.IsNullOrEmpty(templateDir))
                {
                    str4 = templateDir.Replace("/", @"\");
                }
                this.HdnFileText.Value = str2;
                DirectoryInfo info = new DirectoryInfo(base.Request.PhysicalApplicationPath + str4 + str3);
                if (info.Exists)
                {
                    this.BindData(str4 + str3);
                }
            }
        }
        protected void BindData(string dir)
        {
            this.RepFiles.DataSource = FileSystemObject.GetDirectoryInfos(base.Request.PhysicalApplicationPath + dir, FsoMethod.All);
            this.RepFiles.DataBind();
        }
        protected void RepFiles_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                DataRowView dataItem = (DataRowView)e.Item.DataItem;
                if ((dataItem["name"].ToString() == "") || (dataItem["name"].ToString() == ""))
                {
                    e.Item.Visible = false;
                }
            }
        }
    }
}