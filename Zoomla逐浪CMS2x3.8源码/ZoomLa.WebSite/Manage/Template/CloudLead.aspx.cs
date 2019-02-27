using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;

public partial class Manage_I_Template_CloudLead : System.Web.UI.Page
{
    protected B_Admin badmin = new B_Admin();
    protected void Page_Load(object sender, EventArgs e)
    {
        Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='../Config/SiteOption.aspx'>系统设置</a></li><li><a href='TemplateSet.aspx'>模板风格</a></li><li class='active'>从云端免费获取全站方案</li>" + Call.GetHelp(23));
    }
}