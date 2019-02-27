using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using ZoomLa.SQLDAL;
using System.Data;
using System.Data.SqlClient;
using ZoomLa.Common;
using System.Text;
using ZoomLa.BLL;
using ZoomLa.Components;
using ZoomLa.Model;

public partial class manage_File_BackupManage : CustomerPageAction
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
        B_Admin badmin = new B_Admin();
        if (!B_ARoleAuth.Check(ZLEnum.Auth.other, "FileManage"))
        {
            function.WriteErrMsg("没有权限进行此项操作");
        }
        this.m_CurrentDir = base.Request.QueryString["Dir"];
        string SysUp = "/temp/";
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
            if (this.m_ParentDir.Substring(this.m_ParentDir.Length-1,1)=="/")
            {
                this.m_ParentDir = this.m_ParentDir.Substring(0, this.m_ParentDir.Length - 1);//判断BackupManage.aspx.cs页面里的返回上一页里的链接的最后一个字符是不是"/"，如果是则去掉。
            }
        }
        this.m_CurrentDir = this.m_CurrentDir + "/";
        this.m_CurrentDir = this.m_CurrentDir.Replace("//", "/");
        DirectoryInfo info = new DirectoryInfo(base.Request.PhysicalApplicationPath + this.m_CurrentDir);
        if (info.Exists)
        {
            this.BindData();
        }
        this.LblCurrentDir.Text = this.m_CurrentDir;
        Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='" + CustomerPageAction.customPath2 + "plus/ADManage.aspx'>扩展功能</a></li><li><a href='UploadFile.aspx'>文件管理</a></li><li><a href='BackupManage.aspx'>备份管理</a></li>");
    }

    protected void RptFiles_ItemCommand(object source, CommandEventArgs e)
    {
        string p = this.HdnPath.Value;
        string str2 = p + "/" + ((string)e.CommandArgument);
        if (e.CommandName == "DelFiles")
        {
            FileSystemObject.Delete(base.Request.PhysicalApplicationPath + str2.Replace("/", @"\"), FsoMethod.File);
            function.WriteSuccessMsg("文件成功删除", "../File/BackupManage.aspx?Dir=" + base.Server.UrlEncode(p));
        }
        if (e.CommandName == "DelDir")
        {
            base.Response.Write("<script type='text/javascript'>parent.frames[\"left\"].location.reload();</script>");
            FileSystemObject.Delete(base.Request.PhysicalApplicationPath + str2.Replace("/", @"\"), FsoMethod.Folder);
            function.WriteSuccessMsg("目录成功删除", "../File/BackupManage.aspx?Dir=" + base.Server.UrlEncode(p));
        }
        if (e.CommandName == "Down") //下载功能
        {
            string ste = e.CommandArgument.ToString();//文件全名
            string filetype = Path.GetExtension(ste);//文件扩展名
            string ttname = Path.GetFileNameWithoutExtension(ste);//文件名
            SafeSC.DownFile(str2, HttpUtility.UrlEncode(ttname + filetype, System.Text.Encoding.UTF8));
        }

    }
    //protected void Backup_Click1(object sender, EventArgs e)
    //{
    //    ZipClass zip = new ZipClass();
    //    string dir = "/temp/";//m_CurrentDir.Substring(1);//文件夹
    //    string LjFile = Request.PhysicalApplicationPath.ToString() + dir;
    //    string FileToZip = LjFile.Replace("/", @"\");
    //    string dirNmae = dir.Replace("/", "");
    //    dirNmae = DateTime.Now.ToString("yyyyMMdd") + m_CurrentDir.Substring(1).Replace("/", ""); //+ dirNmae;
    //    //function.WriteSuccessMsg(dirNmae, "../Plus/UploadFile.aspx");
    //    string ZipedFile = LjFile + dirNmae + ".rar";
    //    if (zip.Zip(FileToZip, ZipedFile, ""))
    //    {
    //        function.WriteSuccessMsg("<li>文件备份失败</li>", "../Plus/UploadFile.aspx");
    //    }
    //    else
    //    {
    //        function.WriteSuccessMsg("<li>恭喜：备份成功！本备份创建的文件包存于系统根目录Temp文件夹下，您还可以点此<a>[下载当前备份]</a>或进入<a>[备份管理]</a>哦~</li>", "../File/BackupManage.aspx");
    //    }
    //}
}
