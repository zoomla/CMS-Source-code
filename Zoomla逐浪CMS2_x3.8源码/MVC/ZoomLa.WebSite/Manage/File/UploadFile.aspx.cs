namespace ZoomLaCMS.Manage.FtpFile
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
            dictionary.Add("jpeg", "<i class='fa  fa-file-photo-o'></i>");
            dictionary.Add("jpe", "<i class='fa  fa-file-photo-o'></i>");
            dictionary.Add("bmp", "<i class='fa  fa-file-photo-o'></i>");
            dictionary.Add("png", "<i class='fa  fa-file-photo-o'></i>");
            dictionary.Add("swf", "<i class='fa  fa-file-movie-o'></i>");
            dictionary.Add("dll", "<i class='fa  fa-file-movie-o'></i>");
            dictionary.Add("vbp", "<i class='fa  fa-file-movie-o'></i>");
            dictionary.Add("wmv", "<i class='fa  fa-file-movie-o'></i>");
            dictionary.Add("avi", "<i class='fa  fa-file-movie-o'></i>");
            dictionary.Add("asf", "<i class='fa  fa-file-fa-file'></i>");
            dictionary.Add("mpg", "<i class='fa  fa-file-fa-file'></i>");
            dictionary.Add("rm", "<i class='fa  fa-file-fa-file'></i>");
            dictionary.Add("ra", "<i class='fa  fa-file-fa-file'></i>");
            dictionary.Add("ram", "<i class='fa  fa-file-zip-o'></i>");
            dictionary.Add("rar", "<i class='fa  fa-file-zip-o'></i>");
            dictionary.Add("zip", "<i class='fa  fa-file-zip-o'></i>");
            dictionary.Add("xml", "<i class='fa  fa-file-excel-o'></i>");
            dictionary.Add("txt", "<i class='fa  fa-file-code-o'></i>");
            dictionary.Add("exe", "<i class='fa  fa-file-zip-o'></i>");
            dictionary.Add("doc", "<i class='fa  fa-file-word-o'></i>");
            dictionary.Add("html", "<i class='fa  fa-file-code-o'></i>");
            dictionary.Add("htm", "<i class='fa  fa-file-code-o'></i>");
            dictionary.Add("jpg", "<i class='fa  fa-file-photo-o'></i>");
            dictionary.Add("gif", "<i class='fa  fa-file-photo-o'></i>");
            dictionary.Add("xls", "<i class='fa  fa-file-excel-o'></i>");
            dictionary.Add("asp", "<i class='fa  fa-file-code-o'></i>");
            if (dictionary.ContainsKey(extension))
            {
                return dictionary[extension];
            }
            return "<i class='fa  fa-file-fa-file'><i>";
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
            if (!B_ARoleAuth.Check(ZoomLa.Model.ZLEnum.Auth.other, "FileManage"))
            {
                function.WriteErrMsg("没有权限进行此项操作");
            }
            this.m_CurrentDir = base.Request.QueryString["Dir"];
            string SysUp = SiteConfig.SiteOption.UploadDir;
            if (string.IsNullOrEmpty(this.m_CurrentDir))
                this.m_CurrentDir = SysUp;
            else
            {
                if (this.m_CurrentDir.IndexOf(SysUp) != 0)
                    this.m_CurrentDir = SysUp;
            }
            this.HdnPath.Value = this.m_CurrentDir;

            if (!string.IsNullOrEmpty(this.m_CurrentDir) && (this.m_CurrentDir.LastIndexOf("/") > 0))
            {
                this.m_ParentDir = this.m_CurrentDir.Remove(this.m_CurrentDir.LastIndexOf("/"), this.m_CurrentDir.Length - this.m_CurrentDir.LastIndexOf("/"));
                this.m_ParentDir = this.m_ParentDir.Replace("//", "/");//把BackupManage.aspx.cs页面里的返回上一页链接里多余的"/"去掉。
                if (this.m_ParentDir.Substring(this.m_ParentDir.Length - 1, 1) == "/")
                {
                    this.m_ParentDir = this.m_ParentDir.Substring(0, this.m_ParentDir.Length - 1);//判断BackupManage.aspx.cs页面里的返回上一页里的链接的最后一个字符是不是"/"，如果是则去掉。
                }
            }
            this.m_CurrentDir = this.m_CurrentDir + "/";
            //string m_CurrentDir2;
            //m_CurrentDir2 = this.m_CurrentDir.Replace("//", "/");//用m_CurrentDir2来避免Backup_Click1方法中的ZipedFile字符串中的"_"消失
            DirectoryInfo info = new DirectoryInfo(base.Request.PhysicalApplicationPath + this.m_CurrentDir);
            if (info.Exists)
            {
                this.BindData();
            }
            this.LblCurrentDir.Text = m_CurrentDir;
            this.LblCurrentDir.Text = this.LblCurrentDir.Text.Replace("//", "/");
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='" + CustomerPageAction.customPath2 + "Config/DatalistProfile.aspx'>扩展功能</a></li><li><a href='UploadFile.aspx'>文件管理</a></li><li><a href='UploadFile.aspx'>本地文件</a></li>");
        }

        protected void RptFiles_ItemCommand(object source, CommandEventArgs e)
        {
            string p = this.HdnPath.Value;
            string str2 = p + "/" + ((string)e.CommandArgument);
            if (e.CommandName == "DelFiles")
            {
                FileSystemObject.Delete(base.Request.PhysicalApplicationPath + str2.Replace("/", @"\"), FsoMethod.File);
                function.WriteSuccessMsg("文件成功删除", "../File/UploadFile.aspx?Dir=" + base.Server.UrlEncode(p));
            }
            if (e.CommandName == "DelDir")
            {
                base.Response.Write("<script type='text/javascript'>parent.frames[\"left\"].location.reload();</script>");
                FileSystemObject.Delete(base.Request.PhysicalApplicationPath + str2.Replace("/", @"\"), FsoMethod.Folder);
                function.WriteSuccessMsg("目录成功删除", "../File/UploadFile.aspx?Dir=" + base.Server.UrlEncode(p));
            }
        }

        protected void Backup_Click1(object sender, EventArgs e)
        {
            //ZipClass zip = new ZipClass();
            //string dir = m_CurrentDir.Substring(1);//文件夹
            //string LjFile = Request.PhysicalApplicationPath.ToString() + dir;
            //string FileToZip = LjFile.Replace("/", @"\");
            //string dirNmae = dir.Replace("/", "");
            //dirNmae = DateTime.Now.ToString("yyyyMMdd") + m_CurrentDir.Substring(1).Replace("/", "_"); //+ dirNmae;
            ////function.WriteSuccessMsg(dirNmae, "../Plus/UploadFile.aspx");
            //string ZipedFile = LjFile + dirNmae + "备份" + ".rar";
            //ZipedFile = ZipedFile.Replace("__", "|");//先将ZipedFile中的"__"转化成"|" 
            //ZipedFile = ZipedFile.Replace("_", "");//在将ZipedFile中的"_"转化为空
            //ZipedFile = ZipedFile.Replace("|", "_");//最后将ZipedFile中的"|"转化为"_"
            //string a1 = dirNmae + "备份" + ".rar";
            //a1 = a1.Replace("__", "|");//先将a1中的"__"转化成"|" 
            //a1 = a1.Replace("_", "");//在将a1中的"_"转化为空
            //a1 = a1.Replace("|", "_");//最后将a1中的"|"转化为"_"
            //string path1 = ZipedFile;
            //string sPath = Request.PhysicalApplicationPath.ToString() + @"temp\";
            //if (Directory.Exists(sPath))//判断是否存在这个目录
            //{
            //}
            //else
            //{
            //    Directory.CreateDirectory(sPath);//不存在则创建这个目录
            //}
            //string path2 = Request.PhysicalApplicationPath.ToString() + @"temp\" + a1;


            //string path3 = dirNmae.Replace("__", "_");
            //path3 = path3.Substring(0, path3.Length - 1) + "备份" + ".rar";
            //FileToZip = FileToZip.Substring(0, FileToZip.Length - 1);
            //if (zip.Zip(FileToZip, ZipedFile, ""))
            //{
            //    function.WriteSuccessMsg("文件备份失败", "../Plus/UploadFile.aspx");
            //}
            //else
            //{
            //    File.Delete(path2);//如果不删除则会出现文件已存在,无法创建该文件的错误。
            //    File.Move(path1, path2);//因为生成的ZIP文件名和文件的存放位置一样，所以要在生成以后移动到temp目录下面（temp目录是用来存放备份文件的）
            //    function.WriteSuccessMsg("恭喜：备份成功！本备份创建的文件包存于系统根目录Temp文件夹下，您还可以点此 <a href=\"/temp/" + "/" + Server.UrlEncode(path3) + "\")'>[下载当前备份]</a> 或 [<a href=\"BackupManage.aspx?Dir=" + m_ParentDir + "\">备份管理</a>] 进入哦~", "../File/UploadFile.aspx");
            //}
        }
    }
}