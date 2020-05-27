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
using ZoomLa.Web;
using ZoomLa.Components;
using ZoomLa.Common;
using System.Text.RegularExpressions;
using System.IO;
using ZoomLa.BLL;
using System.Text;
using ZoomLa.Model;


namespace ZoomLaCMS.Manage.Template
{
    public partial class CssManage : CustomerPageAction
    {
        protected string ParentDir = string.Empty;
        //当前目录
        public string Dir { get { return (Request.QueryString["Dir"] ?? ""); } }
        //当前模板目录
        public string TemplateDir
        {
            get
            {
                return SiteConfig.SiteOption.CssDir.TrimEnd(',') + "/";
            }
        }
        //当前虚拟路径,模板+Dir
        public string CurDirPath { get { return (TemplateDir + Dir).Replace("//", "/") + "/"; } }
        public string UrlReferrer { get { return "CssManage.aspx?Dir=" + base.Server.UrlEncode(base.Request.QueryString["Dir"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            B_Admin badmin = new B_Admin();
            if (!IsPostBack)
            {
                if (!B_ARoleAuth.Check(ZLEnum.Auth.label, "CssManage"))
                {
                    function.WriteErrMsg(Resources.L.没有权限进行此项操作);
                }
                //string dir = base.Request.QueryString["Dir"];
                //TemplateDir = (!string.IsNullOrEmpty(SiteConfig.SiteOption.CssDir)) ? SiteConfig.SiteOption.CssDir : @"\Skin";
                //this.m_UrlReferrer = "CssManage.aspx?Dir=" + base.Server.UrlEncode(base.Request.QueryString["Dir"]);
                GetBread();
                //if (string.IsNullOrEmpty(Dir))
                //{
                //    this.lblDir.Text = "/";
                //    this.LitParentDirLink.Text = "<a disabled='disabled'>↑"+ Resources.L.返回上一级 + "</a>";
                //}
                //else
                //{
                //    this.lblDir.Text = Dir;
                //    //this.DirPath = TemplateDir + "/" + Dir;
                //    this.LitParentDirLink.Text = "<a href=\"CssManage.aspx?Dir=" + this.ParentDir + "\">↑"+ Resources.L.返回上一级 + "</a>";
                //}
                try
                {
                    DataTable fileList = FileSystemObject.GetDirectoryInfos(function.VToP(CurDirPath), FsoMethod.All);
                    this.repFile.DataSource = fileList;
                    this.repFile.DataBind();
                    this.Page.DataBind();
                }
                catch (Exception exception)
                {
                    string message = exception.Message;
                }
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>" + Resources.L.工作台 + "</a></li><li><a href='TemplateSet.aspx'>" + Resources.L.模板风格 + "</a></li><li class='active'><a href='CssManage.aspx'>" + Resources.L.风格管理 + "</a></li><li>[<a href='CssEdit.aspx' style='color:red;'>" + Resources.L.新建风格 + "</a>]</li>" + Call.GetHelp(22));
            }
        }
        public void GetBread()
        {
            string vdir = Dir;
            if (string.IsNullOrEmpty(vdir)) { lblDir.Text = "全部文件"; }
            else
            {
                string url = "CssManage.aspx?Dir=" + Server.UrlEncode(ParentDir);
                string[] dirArr = vdir.Split("/".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                lblDir.Text += "<a href='" + url + "'>全部文件</a>";
                for (int i = 0; i < dirArr.Length; i++)
                {
                    //上一级目录链接
                    url += dirArr[i] + "/";
                    lblDir.Text += "<i class='fa fa-angle-right spanr'></i>";
                    if (i == (dirArr.Length - 1)) { lblDir.Text += "<span>" + dirArr[i] + "</span>"; }
                    else { lblDir.Text += "<a href='" + url + "'>" + dirArr[i] + "</a>"; }
                    //设置返回上一级
                    if (dirArr.Length == 1) { lblDir.Text = "<a href='CssManage.aspx?Dir=" + Server.UrlEncode(ParentDir) + "'>返回上一级</a> | " + lblDir.Text; }
                    else if (i == (dirArr.Length - 2))
                    {
                        lblDir.Text = "<a href='" + url + "'>返回上一级</a> | " + lblDir.Text;
                    }
                }
            }
        }
        protected string GetSize(string size)
        {
            return FileSystemObject.ConvertSizeToShow(DataConverter.CLng(size));
        }
        protected void repFileReName_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            string pdir = function.VToP(CurDirPath);
            string ppath = pdir + e.CommandArgument.ToString();
            string fname = Path.GetFileNameWithoutExtension(e.CommandArgument.ToString());
            string ext = Path.GetExtension(e.CommandArgument.ToString());
            if (e.CommandName == "DelFiles")
            {
                FileSystemObject.Delete(ppath, FsoMethod.File);
                Response.Redirect(Request.RawUrl);
            }
            if (e.CommandName == "DelDir")
            {
                FileSystemObject.Delete(ppath, FsoMethod.Folder);
                Response.Redirect(Request.RawUrl);
            }
            if (e.CommandName == "CopyDir")
            {
                FileSystemObject.CopyDirectory(ppath, pdir + fname + "_" + Resources.L.复制 + ext);//"复制_" + ppath
                Response.Redirect(Request.RawUrl);
            }
            if (e.CommandName == "CopyFiles")
            {
                FileSystemObject.CopyFile(ppath, pdir + fname + "_" + Resources.L.复制 + ext);
                Response.Redirect(Request.RawUrl);
            }
            if (e.CommandName == "DownFiles")
            {
                SafeSC.DownFile(CurDirPath + e.CommandArgument.ToString(), e.CommandArgument.ToString());
            }
        }
        protected void btnCreateFolder_Click(object sender, EventArgs e)
        {
            try
            {
                string ppath = function.VToP(TemplateDir);
                DirectoryInfo info = new DirectoryInfo(ppath + "\\" + this.txtForderName.Text.Trim());
                if (info.Exists)
                {
                    function.WriteErrMsg(Resources.L.当前目录下已存在同名的文件夹);
                }
                FileSystemObject.Create(ppath + @"\" + this.txtForderName.Text.Trim(), FsoMethod.Folder);
                function.WriteSuccessMsg(Resources.L.文件夹创建成功 + "!");
            }
            catch (FileNotFoundException)
            {
                function.WriteSuccessMsg(Resources.L.文件夹创建成功 + "!");
            }
            catch (UnauthorizedAccessException)
            {
                function.WriteSuccessMsg(Resources.L.没有权限创键文件夹 + "!");
            }

        }
        protected void btnTemplateUpLoad_Click(object sender, EventArgs e)
        {
            SFile_Up.SaveUrl = CurDirPath;
            SFile_Up.SaveFile();
            Response.Redirect(Request.RawUrl);
        }

        protected string GetFileContent(string fileName, string extension)
        {
            string vpath = CurDirPath + fileName;
            StringBuilder builder = new StringBuilder();
            switch (extension)
            {
                case "jpeg":
                case "jpe":
                case "bmp":
                case "png":
                case "jpg":
                case "gif":
                    builder.Append(@"<img src=\'" + vpath + @"\'");
                    builder.Append(@" width=\'200px\'");
                    builder.Append(@" height=\'120px\'");
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
                    builder.Append(@"><param name=\'movie\' value=\'" + vpath + @"\'>");
                    builder.Append(@"<param name=\'wmode\' value=\'transparent\'>");
                    builder.Append(@"<param name=\'quality\' value=\'autohigh\'>");
                    builder.Append(@"<embed src=\'" + vpath + @"\' quality=\'autohigh\'");
                    builder.Append(@" pluginspage=\'http://www.macromedia.com/shockwave/download/index.cgi?P1_Prod_Version=ShockwaveFlash\' type=\'application/x-shockwave-flash\'");
                    builder.Append(@" wmode=\'transparent\'");
                    builder.Append(@" width=\'200\'");
                    builder.Append(@" height=\'120\'");
                    builder.Append("></embed></object>");
                    break;

                default:
                    builder.Append("&nbsp;" + Resources.L.其他文件 + "&nbsp;");
                    break;
            }
            return builder.ToString();
        }
        public bool isimg(string name)
        {
            if (name.Trim() == "jpg" || name.Trim() == "jpeg" || name.Trim() == "bmp" || name.Trim() == "png" || name.Trim() == "gif")
            {
                return true;
            }
            return false;
        }

        public bool isvideo(string name)
        {
            if (name.Trim() == "swf")
            {
                return true;
            }
            return false;
        }
        protected void btnCSSBackup_Click(object sender, EventArgs e)
        {
            ZipClass zip = new ZipClass();
            int index = CurDirPath.LastIndexOf("/");
            string fielname = CurDirPath.Substring(index + 1);
            string LjFile = Request.PhysicalApplicationPath.ToString() + CurDirPath;
            string FileToZip = LjFile.Replace("/", @"\");
            string zipdirName = DateTime.Now.ToString("yyyyMMdd") + "_" + fielname;
            string ZipedFile = FileToZip + @"\" + zipdirName + Resources.L.风格集备份 + ".rar";
            string path1 = ZipedFile;
            string sPath = Request.PhysicalApplicationPath.ToString() + @"temp\";
            if (Directory.Exists(sPath))//判断是否存在这个目录
            {
            }
            else
            {
                Directory.CreateDirectory(sPath);//不存在则创建这个目录
            }
            string path2 = sPath + zipdirName + Resources.L.风格集备份 + ".rar";

            if (zip.Zip(FileToZip, ZipedFile, ""))
            {
                function.WriteSuccessMsg(Resources.L.风格方案 + "(" + fielname + ")" + Resources.L.备份失败 + "!", "../Template/CssManage.aspx");
            }
            else
            {
                File.Delete(path2);//如果不删除则会出现文件已存在,无法创建该文件的错误。
                File.Move(path1, path2);//因为生成的ZIP文件名和文件的存放位置一样，所以要在生成以后移动到temp目录下面（temp目录是用来存放备份文件的）
                function.WriteSuccessMsg(Resources.L.风格方案 + "(" + fielname + ")" + Resources.L.备份成功 + "!", "../Template/CssManage.aspx");
            }
        }
    }
}