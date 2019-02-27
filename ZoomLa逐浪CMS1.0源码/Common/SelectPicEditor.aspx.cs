namespace ZoomLa.WebSite.Common
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
    using ZoomLa.Components;

    public partial class SelectPicEditor : System.Web.UI.Page
    {
        protected string ControlId = "";
        private B_UpLoadFile BUpLoadFile = new B_UpLoadFile();

        protected void Page_Load(object sender, EventArgs e)
        {
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            string str3 = this.BUpLoadFile.GetUpLoadFile(this.File1, "" + function.ApplicationRootPath + "/UploadFiles/pic/", 100);
            str3 = SiteConfig.SiteOption.UploadDir + "/pic/" + str3;
            this.FilePicPath.Text = str3;
        }
    }
}