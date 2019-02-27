using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Common;
using ZoomLa.Components;

public partial class Manage_Counter_StatisticalCode : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='SiteInfo.aspx'>系统设置</a></li><li><a href='Counter.aspx'>访问统计</a></li><li class=\"active\">统计代码</li>");
            Code_T.Text= SiteConfig.SiteInfo.AllSiteJS;
        }
    }

    protected void SaveCode_T_Click(object sender, EventArgs e)
    {
        SiteConfig.SiteInfo.AllSiteJS = Code_T.Text;
        SiteConfig.Update();
        function.WriteSuccessMsg("网站信息配置保存成功！");
    }
}