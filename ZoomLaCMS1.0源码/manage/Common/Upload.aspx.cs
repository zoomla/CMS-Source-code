namespace ZoomLa.WebSite.Manage.Common
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
    using ZoomLa.Components;
    using ZoomLa.Common;
    using System.Text;
    using System.IO;

    public partial class Upload : System.Web.UI.Page
    {
        private string m_ShowPath;
        private string m_FileExtArr;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.HdnCID.Value = base.Request.QueryString["CID"];
            this.HdnType.Value = base.Request.QueryString["ftype"];
        }
        private string FileSavePath()
        {
            string str = SiteConfig.SiteOption.UploadDir+"/"+SiteConfig.SiteOption.AdvertisementDir+"/{$Year}{$Month}/";
            str = str.Replace("{$Year}", DateTime.Now.Year.ToString()).Replace("{$Month}", DateTime.Now.Month.ToString()).Replace("\\", "/");
            this.m_ShowPath = str;            
            return str;
        }
        protected void BtnUpload_Click(object sender, EventArgs e)
        {
            if (!this.FupFile.HasFile)
            {
                this.ReturnManage("请指定一个上传文件");
            }
            else
            {
                int ftype = DataConverter.CLng(this.HdnType.Value);
                if(ftype==1)
                    this.m_FileExtArr = "gif|jpg|png";
                else
                    this.m_FileExtArr = "swf";
                string str2 = Path.GetExtension(this.FupFile.FileName).ToLower();
                if (!this.CheckFilePostfix(str2.Replace(".", "")))
                    this.ReturnManage("上传的文件不是符合扩展名" + this.m_FileExtArr + "的文件");
                else
                {
                    string str3 = DataSecurity.MakeFileRndName();
                    string foldername = base.Request.PhysicalApplicationPath + this.FileSavePath().Replace("/", "\\");
                    string filename = FileSystemObject.CreateFileFolder(foldername, HttpContext.Current) + str3 + str2;                    
                    this.FupFile.SaveAs(filename);
                    string thumbnailPath = this.m_ShowPath + str3 + str2;
                    this.GetScriptByModuleName(thumbnailPath);
                    this.ReturnManage("文件上传成功");
                }
            }
        }
        private void GetScriptByModuleName(string thumbnailPath)
        {
            string id = this.HdnCID.Value;
            StringBuilder builder = new StringBuilder();
            builder.Append("<script language=\"javascript\" type=\"text/javascript\">");
            builder.Append("  parent.DealwithUploadPic(\"" + thumbnailPath + "\",\"txt" + id + "\");");            
            builder.Append("</script>");
            this.Page.ClientScript.RegisterStartupScript(base.GetType(), "UpdateParent", builder.ToString());
        }
        private bool CheckFilePostfix(string fileExtension)
        {
            return StringHelper.FoundCharInArr(this.m_FileExtArr.ToLower(), fileExtension.ToLower(), "|");
        }
        private void ReturnManage(string manage)
        {
            if (!string.IsNullOrEmpty(manage))
            {
                StringBuilder builder = new StringBuilder();
                builder.Append("<script language=\"javascript\" type=\"text/javascript\">");
                builder.Append("   parent.DealwithUploadErrMessage(\"" + manage + "\");");
                builder.Append("</script>");
                this.Page.ClientScript.RegisterStartupScript(base.GetType(), "UpdateParent", builder.ToString());
            }
        }
}
}