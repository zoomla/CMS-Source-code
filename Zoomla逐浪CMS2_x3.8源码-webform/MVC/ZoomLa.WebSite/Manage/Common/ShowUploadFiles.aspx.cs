using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Web;
using System.Web;

namespace ZoomLaCMS.Manage.Common
{
    public partial class ShowUploadFiles : CustomerPageAction
    {

        protected string m_ConfigUploadDir;
        protected int m_ItemIndex;
        protected string m_ParentDir;
        protected void BindData()
        {
            string str2 = this.m_ConfigUploadDir + base.Request.QueryString["Dir"];
            if (string.IsNullOrEmpty(this.TxtSearchKeyword.Text))
            {
                this.RptFiles.DataSource = FileSystemObject.GetDirectoryInfos(base.Request.PhysicalApplicationPath + str2, FsoMethod.All);
            }
            else
            {
                this.RptFiles.DataSource = FileSystemObject.SearchFiles(base.Request.PhysicalApplicationPath + str2, this.TxtSearchKeyword.Text);
            }
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
        protected string GetFileContent(string fileName, string extension)
        {
            //string basePath = GetBasePath(base.Request);
            string configUploadDir = this.m_ConfigUploadDir;
            string str3 = base.Request.QueryString["Dir"];
            str3 = configUploadDir + str3 + "/";
            string str4 = str3 + fileName;
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
            dictionary.Add("jpeg", "<img src='/App_Themes/AdminDefaultTheme/Images/Folder/img.gif'>");
            dictionary.Add("jpe", "<img src='/App_Themes/AdminDefaultTheme/Images/Folder/img.gif'>");
            dictionary.Add("bmp", "<img src='/App_Themes/AdminDefaultTheme/Images/Folder/img.gif'>");
            dictionary.Add("png", "<img src='/App_Themes/AdminDefaultTheme/Images/Folder/img.gif'>");
            dictionary.Add("swf", "<img src='/App_Themes/AdminDefaultTheme/Images/Folder/Filetype_flash.gif'>");
            dictionary.Add("dll", "<img src='/App_Themes/AdminDefaultTheme/Images/Folder/img.gif'>");
            dictionary.Add("vbp", "<img src='/App_Themes/AdminDefaultTheme/Images/Folder/sys.gif'>");
            dictionary.Add("wmv", "<img src='/App_Themes/AdminDefaultTheme/Images/Folder/Filetype_media.gif'>");
            dictionary.Add("avi", "<img src='/App_Themes/AdminDefaultTheme/Images/Folder/Filetype_media.gif'>");
            dictionary.Add("asf", "<img src='/App_Themes/AdminDefaultTheme/Images/Folder/Filetype_media.gif'>");
            dictionary.Add("mpg", "<img src='/App_Themes/AdminDefaultTheme/Images/Folder/Filetype_media.gif'>");
            dictionary.Add("rm", "<img src='/App_Themes/AdminDefaultTheme/Images/Folder/Filetype_rm.gif'>");
            dictionary.Add("ra", "<img src='/App_Themes/AdminDefaultTheme/Images/Folder/Filetype_rm.gif'>");
            dictionary.Add("ram", "<img src='/App_Themes/AdminDefaultTheme/Images/Folder/Filetype_rm.gif'>");
            dictionary.Add("rar", "<img src='/App_Themes/AdminDefaultTheme/Images/Folder/zip.gif'>");
            dictionary.Add("zip", "<img src='/App_Themes/AdminDefaultTheme/Images/Folder/zip.gif'>");
            dictionary.Add("xml", "<img src='/App_Themes/AdminDefaultTheme/Images/Folder/xml.gif'>");
            dictionary.Add("txt", "<img src='/App_Themes/AdminDefaultTheme/Images/Folder/txt.gif'>");
            dictionary.Add("exe", "<img src='/App_Themes/AdminDefaultTheme/Images/Folder/exe.gif'>");
            dictionary.Add("doc", "<img src='/App_Themes/AdminDefaultTheme/Images/Folder/doc.gif'>");
            dictionary.Add("html", "<img src='/App_Themes/AdminDefaultTheme/Images/Folder/html.gif'>");
            dictionary.Add("htm", "<img src='/App_Themes/AdminDefaultTheme/Images/Folder/htm.gif'>");
            dictionary.Add("jpg", "<img src='/App_Themes/AdminDefaultTheme/Images/Folder/jpg.gif'>");
            dictionary.Add("gif", "<img src='/App_Themes/AdminDefaultTheme/Images/Folder/gif.gif'>");
            dictionary.Add("xls", "<img src='/App_Themes/AdminDefaultTheme/Images/Folder/xls.gif'>");
            dictionary.Add("asp", "<img src='/App_Themes/AdminDefaultTheme/Images/Folder/asp.gif'>");
            if (dictionary.ContainsKey(extension))
            {
                return dictionary[extension];
            }
            return "<img src='/App_Themes/AdminDefaultTheme/Images/Folder/other.gif'>";
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
            B_Admin.CheckIsLogged();
            this.m_ConfigUploadDir = SiteConfig.SiteOption.UploadDir;
            if (!string.IsNullOrEmpty(this.m_ConfigUploadDir))
            {
                string configUploadDir = this.m_ConfigUploadDir;
                string str2 = base.Request.QueryString["Dir"];
                if (!string.IsNullOrEmpty(str2) && (str2.LastIndexOf("/") > 0))
                {
                    this.m_ParentDir = str2.Remove(str2.LastIndexOf("/"), str2.Length - str2.LastIndexOf("/"));
                }
                str2 = configUploadDir + str2 + "/";
                DirectoryInfo info = new DirectoryInfo(base.Request.PhysicalApplicationPath + str2);
                if (info.Exists)
                {
                    this.BindData();
                }
                this.LblCurrentDir.Text = str2;
            }
        }
        protected void RptFiles_ItemCommand(object source, CommandEventArgs e)
        {
            string str2 = this.m_ConfigUploadDir + base.Request.QueryString["Dir"] + "/" + ((string)e.CommandArgument);
            if (e.CommandName == "DelFiles")
            {
                FileSystemObject.Delete(base.Request.PhysicalApplicationPath + str2.Replace("/", @"\"), FsoMethod.File);
                function.WriteSuccessMsg("文件成功删除", "FileManage.aspx?Dir=" + base.Server.UrlEncode(base.Request.QueryString["Dir"]));
            }
            if (e.CommandName == "DelDir")
            {
                base.Response.Write("<script type='text/javascript'>parent.frames[\"left\"].location.reload();</script>");
                FileSystemObject.Delete(base.Request.PhysicalApplicationPath + str2.Replace("/", @"\"), FsoMethod.Folder);
                function.WriteSuccessMsg("<li>目录成功删除</li>", "FileManage.aspx?Dir=" + base.Server.UrlEncode(base.Request.QueryString["Dir"]));
            }
        }
    }
}