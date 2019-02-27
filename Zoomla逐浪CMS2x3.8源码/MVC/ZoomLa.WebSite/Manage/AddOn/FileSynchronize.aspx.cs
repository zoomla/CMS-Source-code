using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Common;
using ZoomLa.BLL;

namespace ZoomLaCMS.Manage.AddOn
{
    public partial class FileSynchronize : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='" + CustomerPageAction.customPath2 + "plus/ADManage.aspx'>扩展功能</a></li><li><a href='" + CustomerPageAction.customPath2 + "iplook/IPManage.aspx'>其他功能</a></li><li><a href='FileSynchronize.aspx'>文件同步管理</a></li>");
            }
        }
    }
}