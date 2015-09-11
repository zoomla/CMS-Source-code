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
    using ZoomLa.Common;
    using ZoomLa.BLL;

    public partial class EditJSTemplate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                B_Admin badmin = new B_Admin();
                badmin.CheckMulitLogin();
                if (!badmin.ChkPermissions("ADManage"))
                {
                    function.WriteErrMsg("没有权限进行此项操作");
                }
                int num = DataConverter.CLng(base.Request.QueryString["ZoneType"]);
                this.HdnZoneType.Value = num.ToString();
                this.TxtADTemplate.Text = new B_ADZoneJs().GetADZoneJSTemplateContent(num);
            }
        }
        protected void EBtnSaverTemplate_Click(object sender, EventArgs e)
        {
            B_ADZoneJs ejs = new B_ADZoneJs();
            if (ejs.SaveJSTemplate(this.TxtADTemplate.Text.Trim(), DataConverter.CLng(this.HdnZoneType.Value)))
            {
                function.WriteSuccessMsg("<li>保存JS模板成功</li>", "../Plus/JSTemplate.aspx");
            }
            else
            {
                function.WriteErrMsg("保存JS模板出现异常，可能是没有操作权限", "../Plus/JSTemplate.aspx");
            }
        }
    }
}