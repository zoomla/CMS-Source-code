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
using ZoomLa.Sns;

public partial class Store_index : System.Web.UI.Page
{
    protected B_CreateShopHtml shll = new B_CreateShopHtml();
    protected B_CreateHtml bll = new B_CreateHtml();

    protected void Page_Load(object sender, EventArgs e)
    {
        CheckInstalled();
        string IndexDir = SiteConfig.SiteOption.ShopTemplate;

        IndexDir = base.Request.PhysicalApplicationPath + SiteConfig.SiteOption.TemplateDir + "/" + IndexDir;
        IndexDir = IndexDir.Replace("/", @"\");
        if (!FileSystemObject.IsExist(IndexDir, FsoMethod.File))
            Response.Write("[产生错误的可能原因：内容信息不存在或未开放！]");
        else
        {
            string IndexHtml = FileSystemObject.ReadFile(IndexDir);
            //IndexHtml = this.shll.CreateShopHtml(IndexHtml, 0, 0);
            IndexHtml = this.bll.CreateHtml(IndexHtml, 0, 0, "0");

            if (IndexHtml.IndexOf("$Zone_") > -1)
            {
                IndexHtml = ZoneFun.MessageReplace(IndexHtml, 0, Guid.Empty, Guid.Empty);
            }
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
