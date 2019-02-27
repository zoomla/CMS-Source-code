namespace ZoomLa.WebSite.Manage.Template
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
    using System.IO;
    using ZoomLa.BLL;

    public partial class CssEdit : System.Web.UI.Page
    {
        protected string FilePath = string.Empty;
        protected string FileName = string.Empty;
        protected string ShowPath = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                B_Admin badmin = new B_Admin();
                badmin.CheckMulitLogin();
                if (!badmin.ChkPermissions("CssEdit"))
                {
                    function.WriteErrMsg("没有权限进行此项操作");
                }
                string strPath = base.Request.QueryString["filepath"];                
                string TemplateDir = "/Skin";
                if (string.IsNullOrEmpty(strPath))
                {
                    strPath = "";
                    this.ShowPath = "/";
                }
                else
                {
                    this.ShowPath = strPath;
                }
                strPath = base.Request.PhysicalApplicationPath + TemplateDir + strPath;
                strPath = strPath.Replace("/", @"\");

                this.FilePath = strPath;
                string FileExp = Path.GetExtension(this.FilePath);
                if (!string.IsNullOrEmpty(FileExp))
                {
                    this.FileName = Path.GetFileName(this.FilePath);
                    this.lblFielName.Text = this.FileName;
                    this.TxtFilename.Text = this.FileName;
                    this.TxtFilename.Visible = false;
                    this.textContent.Text = FileSystemObject.ReadFile(this.FilePath);
                    this.ShowPath = this.ShowPath.Replace(this.FileName, "");
                    this.Hdnmethod.Value = "append";
                }
                else
                {
                    this.FileName = "";
                    this.TxtFilename.Text = "";
                    this.TxtFilename.Visible = true;
                    this.lblFielName.Text = "";
                    this.lblFielName.Visible = false;
                    this.Hdnmethod.Value = "add";
                }                
                this.HdnFilePath.Value = this.FilePath;
                this.HdnShowPath.Value = this.ShowPath;                
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                if (this.Hdnmethod.Value == "add")
                {
                    this.FilePath = this.HdnFilePath.Value + @"\" + this.TxtFilename.Text;
                    FileSystemObject.WriteFile(this.FilePath, this.textContent.Text);
                }
                if (this.Hdnmethod.Value == "append")
                {
                    this.FilePath = this.HdnFilePath.Value;
                    FileSystemObject.WriteFile(this.FilePath, this.textContent.Text);
                }
                string sPath = this.HdnShowPath.Value.Replace("/", "");
                base.Response.Redirect("CssManage.aspx?Dir=" + sPath);
            }
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            if (this.Hdnmethod.Value == "add")
            {
                this.textContent.Text = "";
            }
            if (this.Hdnmethod.Value == "append")
            {
                string str = FileSystemObject.ReadFile(this.HdnFilePath.Value);
                this.textContent.Text = str;
            }
        }
}
}