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
    using ZoomLa.Components;
    using System.Text;
    using ZoomLa.Common;
    using System.IO;
    using System.Collections.Generic;
    using ZoomLa.BLL;

    public partial class UploadFile : System.Web.UI.Page
    {
        protected string m_CurrentDir;
        protected string m_ParentDir;
        protected string m_UrlReferrer;        

        protected void BindData()
        {
            this.RptFiles.DataSource = FileSystemObject.GetDirectoryInfos(base.Request.PhysicalApplicationPath + this.m_CurrentDir, FsoMethod.All);
            this.RptFiles.DataBind();
        }
        protected string GetBasePath(HttpRequest request)
        {
            if (request == null)
            {
                return "/";
            }
            return VirtualPathUtility.AppendTrailingSlash(request.ApplicationPath);
        }
        public string CurrentDir
        {
            get { return this.m_CurrentDir; }
        }
        protected string GetFileContent(string fileName, string extension)
        {
            //string basePath = GetBasePath(base.Request);            
            //string str3 = base.Request.QueryString["Dir"];
            //if (string.IsNullOrEmpty(str3))
            //    str3 = SiteConfig.SiteOption.UploadDir;
            //str3 = str3 + "/";
            string str4 = this.m_CurrentDir + fileName;
            StringBuilder builder = new StringBuilder();
            switch (extension)
            {
                case "jpeg":
                case "jpe":
                case "bmp":
                case "png":
                case "jpg":
                case "gif":
                    builder.Append(@"<img src=\'" + str4 + @"\'");
                    builder.Append(@" width=\'200\'");
                    builder.Append(@" height=\'120\'");
                    builder.Append(@" border=\'0\' />");
                    break;

                case "wmv":
                case "avi":
                case "asf":
                case "mpg":
                case "rm":
                case "ra":
                case "ram":
                case "swf":
                    builder.Append(@"<object classid=\'clsid:D27CDB6E-AE6D-11cf-96B8-444553540000\' codebase=\'http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,0,0\'");
                    builder.Append(@" width=\'200\'");
                    builder.Append(@" height=\'120\'");
                    builder.Append(@"><param name=\'movie\' value=\'" + str4 + @"\'>");
                    builder.Append(@"<param name=\'wmode\' value=\'transparent\'>");
                    builder.Append(@"<param name=\'quality\' value=\'autohigh\'>");
                    builder.Append(@"<embed src=\'" + str4 + @"\' quality=\'autohigh\'");
                    builder.Append(@" pluginspage=\'http://www.macromedia.com/shockwave/download/index.cgi?P1_Prod_Version=ShockwaveFlash\' type=\'application/x-shockwave-flash\'");
                    builder.Append(@" wmode=\'transparent\'");
                    builder.Append(@" width=\'200\'");
                    builder.Append(@" height=\'120\'");
                    builder.Append("></embed></object>");
                    break;

                default:
                    builder.Append("&nbsp;其他文件&nbsp;");
                    break;
            }
            return builder.ToString();
        }

        protected string GetShowExtension(string extension)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add("jpeg", "<img src='../images/Folder/img.gif'>");
            dictionary.Add("jpe", "<img src='../images/Folder/img.gif'>");
            dictionary.Add("bmp", "<img src='../images/Folder/img.gif'>");
            dictionary.Add("png", "<img src='../images/Folder/img.gif'>");
            dictionary.Add("swf", "<img src='../images/Folder/Filetype_flash.gif'>");
            dictionary.Add("dll", "<img src='../images/Folder/img.gif'>");
            dictionary.Add("vbp", "<img src='../images/Folder/sys.gif'>");
            dictionary.Add("wmv", "<img src='../images/Folder/Filetype_media.gif'>");
            dictionary.Add("avi", "<img src='../images/Folder/Filetype_media.gif'>");
            dictionary.Add("asf", "<img src='../images/Folder/Filetype_media.gif'>");
            dictionary.Add("mpg", "<img src='../images/Folder/Filetype_media.gif'>");
            dictionary.Add("rm", "<img src='../images/Folder/Filetype_rm.gif'>");
            dictionary.Add("ra", "<img src='../images/Folder/Filetype_rm.gif'>");
            dictionary.Add("ram", "<img src='../images/Folder/Filetype_rm.gif'>");
            dictionary.Add("rar", "<img src='../images/Folder/zip.gif'>");
            dictionary.Add("zip", "<img src='../images/Folder/zip.gif'>");
            dictionary.Add("xml", "<img src='../images/Folder/xml.gif'>");
            dictionary.Add("txt", "<img src='../images/Folder/txt.gif'>");
            dictionary.Add("exe", "<img src='../images/Folder/exe.gif'>");
            dictionary.Add("doc", "<img src='../images/Folder/doc.gif'>");
            dictionary.Add("html", "<img src='../images/Folder/html.gif'>");
            dictionary.Add("htm", "<img src='../images/Folder/htm.gif'>");
            dictionary.Add("jpg", "<img src='../images/Folder/jpg.gif'>");
            dictionary.Add("gif", "<img src='../images/Folder/gif.gif'>");
            dictionary.Add("xls", "<img src='../images/Folder/xls.gif'>");
            dictionary.Add("asp", "<img src='../images/Folder/asp.gif'>");
            if (dictionary.ContainsKey(extension))
            {
                return dictionary[extension];
            }
            return "<img src='../images/Folder/other.gif'>";
        }

        protected string GetSize(string size)
        {
            if (string.IsNullOrEmpty(size))
            {
                return string.Empty;
            }
            int num = DataConverter.CLng(size);
            int num2 = num / 0x400;
            if (num2 < 1)
            {
                return (num.ToString() + "B");
            }
            if (num2 < 0x400)
            {
                return (num2.ToString() + "KB");
            }
            int num3 = num2 / 0x400;
            if (num3 < 1)
            {
                return (num2.ToString() + "KB");
            }
            if (num3 >= 0x400)
            {
                num3 /= 0x400;
                return (num3.ToString() + "GB");
            }
            return (num3.ToString() + "MB");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            B_Admin badmin = new B_Admin();
            badmin.CheckMulitLogin();
            if (!badmin.ChkPermissions("FileManage"))
            {
                function.WriteErrMsg("没有权限进行此项操作");
            }
            this.m_CurrentDir = base.Request.QueryString["Dir"];
            if (string.IsNullOrEmpty(this.m_CurrentDir))
                this.m_CurrentDir = SiteConfig.SiteOption.UploadDir;
            this.HdnPath.Value = this.m_CurrentDir;
            if (!string.IsNullOrEmpty(this.m_CurrentDir) && (this.m_CurrentDir.LastIndexOf("/") > 0))
            {
                this.m_ParentDir = this.m_CurrentDir.Remove(this.m_CurrentDir.LastIndexOf("/"), this.m_CurrentDir.Length - this.m_CurrentDir.LastIndexOf("/"));
            }
            this.m_CurrentDir = this.m_CurrentDir + "/";
            DirectoryInfo info = new DirectoryInfo(base.Request.PhysicalApplicationPath + this.m_CurrentDir);
            if (info.Exists)
            {
                this.BindData();
            }
            this.LblCurrentDir.Text = this.m_CurrentDir;            
        }

        protected void RptFiles_ItemCommand(object source, CommandEventArgs e)
        {
            string p = this.HdnPath.Value;
            string str2 = p + "/" + ((string)e.CommandArgument);
            if (e.CommandName == "DelFiles")
            {
                FileSystemObject.Delete(base.Request.PhysicalApplicationPath + str2.Replace("/", @"\"), FsoMethod.File);
                function.WriteSuccessMsg("文件成功删除", "../Plus/UploadFile.aspx?Dir=" + base.Server.UrlEncode(p));
            }
            if (e.CommandName == "DelDir")
            {
                base.Response.Write("<script type='text/javascript'>parent.frames[\"left\"].location.reload();</script>");
                FileSystemObject.Delete(base.Request.PhysicalApplicationPath + str2.Replace("/", @"\"), FsoMethod.Folder);
                function.WriteSuccessMsg("<li>目录成功删除</li>", "../Plus/UploadFile.aspx?Dir=" + base.Server.UrlEncode(p));
            }
        }
    }
}