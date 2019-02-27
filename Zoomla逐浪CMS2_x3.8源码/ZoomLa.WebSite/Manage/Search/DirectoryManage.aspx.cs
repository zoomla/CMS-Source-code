using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Components;
using ZoomLa.Common;
using ZoomLa.BLL;

public partial class manage_Search_DirectoryManage : System.Web.UI.Page
{
    protected string SiteName = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='" + CustomerPageAction.customPath2 + "plus/ADManage.aspx'>扩展功能</a></li><li><a href='" + CustomerPageAction.customPath2 + "I/iplook/IPManage.aspx'>其他功能</a></li> <li><a href='DirectoryManage.aspx'>定义全文检索</a>     <a href='AddIndexSeach.aspx'>[添加检索]</a></li>");
        }
        SiteName = SiteConfig.SiteInfo.SiteName;
    }
}
