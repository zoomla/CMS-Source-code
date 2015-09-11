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
    using ZoomLa.BLL;
    using ZoomLa.Common;
    using ZoomLa.Components;
    using System.Text;

    public partial class UploadMultiPicEditor : System.Web.UI.Page
    {
        private B_UpLoadFile BUpLoadFile = new B_UpLoadFile();
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Literal1.Text = "";
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            string str3 = "";
            StringBuilder builder = new StringBuilder();
            if (this.File1.Value != "")
            {
                str3 = this.BUpLoadFile.GetUpLoadFile(this.File1, "" + function.ApplicationRootPath + "/UploadFiles/pic/", 100);
                str3 = SiteConfig.SiteOption.UploadDir + "/pic/" + str3;
                builder.Append("<script type=\"text/javascript\">setImg('" + str3 + "');</script>");
            }
            if (this.File2.Value != "")
            {
                str3 = this.BUpLoadFile.GetUpLoadFile(this.File2, "" + function.ApplicationRootPath + "/UploadFiles/pic/", 100);
                str3 = SiteConfig.SiteOption.UploadDir + "/pic/" + str3;
                builder.Append("<script type=\"text/javascript\">setImg('" + str3 + "');</script>");
            }
            if (this.File3.Value != "")
            {
                str3 = this.BUpLoadFile.GetUpLoadFile(this.File3, "" + function.ApplicationRootPath + "/UploadFiles/pic/", 100);
                str3 = SiteConfig.SiteOption.UploadDir + "/pic/" + str3;
                builder.Append("<script type=\"text/javascript\">setImg('" + str3 + "');</script>");
            }
            if (this.File4.Value != "")
            {
                str3 = this.BUpLoadFile.GetUpLoadFile(this.File4, "" + function.ApplicationRootPath + "/UploadFiles/pic/", 100);
                str3 = SiteConfig.SiteOption.UploadDir + "/pic/" + str3;
                builder.Append("<script type=\"text/javascript\">setImg('" + str3 + "');</script>");
            }
            if (this.File5.Value != "")
            {
                str3 = this.BUpLoadFile.GetUpLoadFile(this.File5, "" + function.ApplicationRootPath + "/UploadFiles/pic/", 100);
                str3 = SiteConfig.SiteOption.UploadDir + "/pic/" + str3;
                builder.Append("<script type=\"text/javascript\">setImg('" + str3 + "');</script>");
            }
            if (this.File6.Value != "")
            {
                str3 = this.BUpLoadFile.GetUpLoadFile(this.File6, "" + function.ApplicationRootPath + "/UploadFiles/pic/", 100);
                str3 = SiteConfig.SiteOption.UploadDir + "/pic/" + str3;
                builder.Append("<script type=\"text/javascript\">setImg('" + str3 + "');</script>");
            }
            this.Page.ClientScript.RegisterStartupScript(base.GetType(), "InsertImg", builder.ToString());
        }
    }
}