namespace ZoomLa.WebSite.Manage.AddOn
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
    using System.IO;

    public partial class ShowJSCode : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ZoomLa.Common.function.AccessRulo();
            if (!this.Page.IsPostBack)
            {
                B_Admin badmin = new B_Admin();
                if (!B_ARoleAuth.Check(ZLEnum.Auth.other, "ADManage"))
                {
                    function.WriteErrMsg("没有权限进行此项操作");
                }
                M_Adzone adZoneById = B_ADZone.getAdzoneByZoneId(DataConverter.CLng(base.Request.QueryString["ZoneId"]));
                if ((adZoneById.IsNull) || string.IsNullOrEmpty(adZoneById.ZoneJSName))
                {
                    this.TxtZoneCode.Text = "版位调用代码不存在!";
                }
                else
                {
                    this.TxtZoneCode.Text = "<script src=\"{$AdDir/}/" + adZoneById.ZoneJSName.Trim() + "\"></script>";
                }
            }
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='ADManage.aspx'>广告管理</a></li><li class='active'>获取广告代码</li>");
        }        
    }
}