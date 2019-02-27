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
    using System.IO;
    using System.Text;

    public partial class SelectPicEditor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {            
            this.Button2.Disabled = true;
            this.span1.InnerText = "图片大小不能超过" + SiteConfig.SiteOption.UploadPicMaxSize.ToString() + "K";
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (!SiteConfig.SiteOption.EnableUploadFiles)
            {
                ReturnManage("本站不允许上传文件！");
            }
            else
            {
                if (!this.FupFile.HasFile)
                {
                    ReturnManage("请指定一个上传附件");
                }
                else
                {
                    string str2 = Path.GetExtension(this.FupFile.FileName).ToLower();
                    if (string.IsNullOrEmpty(SiteConfig.SiteOption.UploadPicExts))
                    {
                        ReturnManage("在线编辑器上传图片文件类型没有设定，不允许上传");
                    }
                    else if (!this.CheckFilePostfix(str2.Replace(".", "")))
                    {
                        ReturnManage("上传的图片不是符合扩展名" + SiteConfig.SiteOption.UploadPicExts + "的文件");
                    }
                    else
                    {
                        if (((int)this.FupFile.FileContent.Length) > (SiteConfig.SiteOption.UploadPicMaxSize * 0x400))
                        {
                            ReturnManage("上传的图片超过" + SiteConfig.SiteOption.UploadPicMaxSize.ToString() + "KB大小");
                        }
                        else
                        {
                            string str4 = DateTime.Now.ToString("yyyyMM");
                            string str3 = DataSecurity.MakeFileRndName();

                            string str = VirtualPathUtility.AppendTrailingSlash(SiteConfig.SiteOption.UploadDir) + "FCKeditor/pic/" + str4 + "/";
                            string path = HttpContext.Current.Server.MapPath(function.ApplicationRootPath + str);

                            if (!Directory.Exists(path))
                            {
                                Directory.CreateDirectory(path);
                            }
                            string filename = path + str3 + str2;
                            SafeSC.SaveFile(filename, FupFile);
                            this.FilePicPath.Text = str + str3 + str2;
                            this.Button2.Disabled = false;
                            this.Button2.Attributes.Add("onclick", "setImg();");
                        }
                    }
                }
            }            
        }
        private bool CheckFilePostfix(string fileExtension)
        {
            return StringHelper.FoundCharInArr(SiteConfig.SiteOption.UploadPicExts.ToLower(), fileExtension.ToLower(), "|");
        }
        private void ReturnManage(string manage)
        {
            if (!string.IsNullOrEmpty(manage))
            {
                StringBuilder builder = new StringBuilder();
                builder.Append("<script language=\"javascript\" type=\"text/javascript\">");
                builder.Append("alert(\"" + manage + "\");");
                builder.Append("</script>");
                this.Page.ClientScript.RegisterStartupScript(base.GetType(), "ShowErr", builder.ToString());
            }
        }  

    }
}