using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Components;

public partial class Manage_Page_PageConfig : CustomerPageAction
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            IsAudit_Chk.Checked = SiteConfig.YPage.IsAudit;
            UserCanNode_Chk.Checked = SiteConfig.YPage.UserCanNode;
            Call.SetBreadCrumb(Master, "<li><a href='" + customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='PageManage.aspx'>企业黄页</a></li><li class='active'>黄页配置</li>");
        }
    }
    protected void Save_Btn_Click(object sender, EventArgs e)
    {
        SiteConfig.YPage.IsAudit = IsAudit_Chk.Checked;
        SiteConfig.YPage.UserCanNode = UserCanNode_Chk.Checked;
        SiteConfig.Update();
        function.WriteSuccessMsg("配置保存成功");
    }
}