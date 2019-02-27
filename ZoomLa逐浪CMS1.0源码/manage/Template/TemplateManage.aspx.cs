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
namespace ZoomLa.WebSite.Manage.Template
{
    public partial class TemplateManage : System.Web.UI.Page
    {
        protected string AbsPath = string.Empty;
        protected string DirPath = string.Empty;
        protected string ParentDir = string.Empty;
        protected string m_UrlReferrer;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                B_Admin badmin = new B_Admin();
                badmin.CheckMulitLogin();
                if (!badmin.ChkPermissions("TemplateManage"))
                {
                    function.WriteErrMsg("没有权限进行此项操作");
                }
                string str2 = base.Request.QueryString["Dir"];
                string TemplateDir = SiteConfig.SiteOption.TemplateDir;
                this.m_UrlReferrer = "TemplateManage.aspx?Dir=" + base.Server.UrlEncode(base.Request.QueryString["Dir"]);
                if (string.IsNullOrEmpty(str2))
                {
                    this.lblDir.Text = "/";
                    this.DirPath = TemplateDir;
                    this.LitParentDirLink.Text = "<a disabled='disabled'>返回上一级</a>";
                }
                else
                {
                    this.lblDir.Text = str2;
                    this.DirPath = TemplateDir + "/" + str2;
                    if (str2.LastIndexOf("/") > 0)
                    {
                        this.ParentDir = str2.Remove(str2.LastIndexOf("/"), str2.Length - str2.LastIndexOf("/"));
                    }
                    this.LitParentDirLink.Text = "<a href=\"TemplateManage.aspx?Dir=" + this.ParentDir + "\">返回上一级</a>";
                }
                this.AbsPath = base.Request.PhysicalApplicationPath + TemplateDir + str2 + "/";
                this.AbsPath = this.AbsPath.Replace("/", @"\");
                this.AbsPath = this.AbsPath.Replace(@"\\", @"\");
                this.HdnPath.Value = this.AbsPath;
                try
                {
                    DataTable fileList = FileSystemObject.GetDirectoryInfos(this.AbsPath, FsoMethod.All);
                    this.repFile.DataSource = fileList;
                    this.repFile.DataBind();
                    this.Page.DataBind();
                }
                catch (Exception exception)
                {
                    string message = exception.Message;
                }
            }
        }
        protected string GetSize(string size)
        {
            return FileSystemObject.ConvertSizeToShow(DataConverter.CLng(size));
        }
        protected void repFileReName_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            this.AbsPath = this.HdnPath.Value;
            if (e.CommandName == "DelFiles")
            {
                FileSystemObject.Delete(this.AbsPath + e.CommandArgument.ToString(), FsoMethod.File);
                function.WriteSuccessMsg("<li>模板文件已成功删除！</li>", this.m_UrlReferrer);
            }
            if (e.CommandName == "DelDir")
            {
                FileSystemObject.Delete(this.AbsPath + e.CommandArgument.ToString(), FsoMethod.Folder);
                function.WriteSuccessMsg("<li>该模板文件夹已成功删除！</li>", this.m_UrlReferrer);
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("TemplateEdit.aspx?filepath=" + base.Server.UrlEncode(base.Request.QueryString["Dir"]));
        }
        protected void btnCreateFolder_Click(object sender, EventArgs e)
        {
            //string pattern = "^[a-zA-Z0-9_]+$";
            //if (!Regex.IsMatch(this.txtForderName.Text.Trim(), pattern, RegexOptions.IgnoreCase))
            //{
            //   function.WriteErrMsg("<li>文件夹名称不能有特殊字符!</li><li><a href='javascript:window.history.back(-1)'>返回</a></li>");
            //}
            this.AbsPath = this.HdnPath.Value;
            if (this.Page.IsValid)
            {
                try
                {
                    DirectoryInfo info = new DirectoryInfo(this.AbsPath + @"\" + this.txtForderName.Text.Trim());
                    if (info.Exists)
                    {
                        function.WriteErrMsg("当前目录下已存在同名的文件夹", this.m_UrlReferrer);
                    }
                    FileSystemObject.Create(this.AbsPath + @"\" + this.txtForderName.Text.Trim(), FsoMethod.Folder);
                    function.WriteSuccessMsg("文件夹创建成功！", this.m_UrlReferrer);
                }
                catch (FileNotFoundException)
                {
                    function.WriteErrMsg("无法在目标位置创建文件夹！", this.m_UrlReferrer);
                }
                catch (UnauthorizedAccessException)
                {
                    function.WriteErrMsg("<li>在目标位置没有权限创建文件夹！</li>", this.m_UrlReferrer);
                }
            }

        }
        protected void btnTemplateUpLoad_Click(object sender, EventArgs e)
        {
            bool flag = false;
            this.AbsPath = this.HdnPath.Value;
            string fileName = this.fileUploadTemplate.PostedFile.FileName;
            string str2 = Path.GetFileName(fileName).ToLower();
            string str3 = Path.GetExtension(fileName).ToLower();
            string[] TemplateAllowExtName = ".html|.htm|.txt|.config|.Config".Split(new char[] { '|' });
            foreach (string str4 in TemplateAllowExtName)
            {
                if (str3 == str4)
                {
                    flag = true;
                    break;
                }
            }
            if (!flag)
            {
                base.Response.Write("<script>alert('请上传正确的扩展名的模板文件')</script>");
            }
            else
            {
                string filename = this.AbsPath + @"\" + str2;
                this.fileUploadTemplate.PostedFile.SaveAs(filename);
                Response.Redirect("TemplateManage.aspx?Dir=" + base.Server.UrlEncode(base.Request.QueryString["Dir"]));
            }
        }
    }
}