using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Common;
using System.IO;
using ZoomLa.Components;
using ZoomLa.Web;
using System.Text;
using ZoomLa.BLL;
using System.Data;

namespace ZoomLaCMS.Edit
{
    public partial class ShowEdit : System.Web.UI.Page
    {
        protected string m_ConfigUploadDir;
        protected int m_ItemIndex;
        protected string m_ParentDir;
        protected string m_UserInput;
        B_Admin admin = new B_Admin();
        B_User b_User = new B_User();

        //只允许DocTemp目录下
        protected void BindData()
        {
            string str2 = "";
            DataTable dt = new DataTable();
            try
            {
                if (!string.IsNullOrEmpty(base.Request.QueryString["Dir"])) { str2 = this.m_ConfigUploadDir + "/DocTemp/" + base.Request.QueryString["Dir"]; }
                else
                {
                    str2 = this.m_ConfigUploadDir + "/DocTemp";
                }
                if (string.IsNullOrEmpty(this.TxtSearchKeyword.Text))
                {
                    //为空并且显示目录

                    dt = FileSystemObject.GetDirectoryInfos(base.Request.PhysicalApplicationPath + str2, FsoMethod.All);
                    dt.DefaultView.RowFilter = "content_type not in ('xml')";
                    this.RptFiles.DataSource = dt.DefaultView;
                }
                else
                {
                    //如果不为空则搜索
                    dt = FileSystemObject.SearchFiles(base.Request.PhysicalApplicationPath + str2 + "/DocTemp", this.TxtSearchKeyword.Text);
                    dt.DefaultView.RowFilter = "content_type not in ('xml')";
                    this.RptFiles.DataSource = dt.DefaultView;
                }
                this.RptFiles.DataBind();
            }
            catch { function.WriteErrMsg(str2); }
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
            string str2 = base.Request.QueryString["Dir"];
            this.m_UserInput = base.Request.QueryString["OpenWords"];
            this.m_ConfigUploadDir = SiteConfig.SiteOption.UploadDir;
            if (!string.IsNullOrEmpty(this.m_ConfigUploadDir))
            {
                string configUploadDir = this.m_ConfigUploadDir;
                if (!string.IsNullOrEmpty(str2) && (str2.LastIndexOf("/") > 0))
                {
                    this.m_ParentDir = str2.Remove(str2.LastIndexOf("/"), str2.Length - str2.LastIndexOf("/"));
                }
                if (!string.IsNullOrEmpty(str2)) { str2 = configUploadDir + "/DocTemp/" + str2; }
                else
                {
                    str2 = configUploadDir + "/DocTemp";
                }
                string dirPath = Server.MapPath(str2);
                //回朔Bug处理,1,只能访问上传目录 2,替换../
                if (!dirPath.ToLower().Contains(configUploadDir.ToLower().Replace("/", @"\")))
                {
                    function.WriteErrMsg("路径错误!");
                }
                DirectoryInfo info = new DirectoryInfo(dirPath);

                if (info.Exists)
                {
                    this.BindData();
                }
                else
                    function.WriteErrMsg(info.FullName);
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
                function.WriteSuccessMsg("目录成功删除", "FileManage.aspx?Dir=" + base.Server.UrlEncode(base.Request.QueryString["Dir"]));
            }
        }
    }
}