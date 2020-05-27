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

    public partial class CssEdit : CustomerPageAction
    {
        protected string FilePath = string.Empty;
        protected string FileName = string.Empty;
        protected string ShowPath = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                B_Admin badmin = new B_Admin();
                if (!B_ARoleAuth.Check(ZoomLa.Model.ZLEnum.Auth.label, "CssEdit"))
                {
                    function.WriteErrMsg("没有权限进行此项操作");
                }
                string strPath = base.Request.QueryString["filepath"];
                string TemplateDir = (!string.IsNullOrEmpty(SiteConfig.SiteOption.CssDir)) ? SiteConfig.SiteOption.CssDir : @"\Skin";
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

                    string fname = this.FileName.Contains(".") ? this.FileName.Split('.')[0] : this.FileName;
                    string exname = this.FileName.Contains(".") ? this.FileName.Split('.')[1] : "css";

                    if (exname.ToLower().Equals("css"))
                    {
                        this.TxtFilename.Value = fname;
                        TxtFilename.Attributes.Add("disabled", "disabled");
                        name_L.Text = "." + exname;
                    }
                    else { function.WriteErrMsg("无权修改.css以外的文件！"); }
               
                    this.textContent.Text = FileSystemObject.ReadFile(this.FilePath);
                    this.ShowPath = this.ShowPath.Replace(this.FileName, "").Trim();
                    this.Hdnmethod.Value = "append";
                }
                else
                {
                    this.FileName = "";
                    this.TxtFilename.Value = "";
                    this.TxtFilename.Visible = true;
                    this.Hdnmethod.Value = "add";
                }                
                this.HdnFilePath.Value = this.FilePath;
                this.HdnShowPath.Value = this.ShowPath;
            }
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='TemplateSet.aspx'>模板风格</a></li><li><a href='CssManage.aspx'>风格管理</a></li><li class='active'><a href='" + Request.RawUrl.ToString() + "'>样式编辑</a></li>");
        }
        public bool CheckIsOk(string exName)
        {
            exName = exName.ToLower();
            bool flag = false;
            string[] allowEx = new string[] { ".html", ".htm",".xml", ".css", ".js", ".txt" };
            foreach (string s in allowEx)
            {
                if (exName == s) flag = true;
            }
            return flag;
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            int num = TxtFilename.Value.IndexOf(".");
            if (num > 0)
            {
                TxtFilename.Value = TxtFilename.Value.Substring(0, num);

            }
            TxtFilename.Value = TxtFilename.Value + ".css";
            string fileExtension = System.IO.Path.GetExtension(TxtFilename.Value.Trim()).ToLower();
          
            if (this.Page.IsValid)
            {
                if (this.Hdnmethod.Value == "add"||this.Hdnmethod.Value == "append")
                {
                    this.FilePath = this.HdnFilePath.Value;
                    SafeSC.WriteFile(function.PToV(FilePath), textContent.Text);
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
            Response.Redirect("CSSManage.aspx");
            
        }

        /// <summary>
        /// 重置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Reset_Click(object sender, EventArgs e)
        {
            this.textContent.Text = "";
        }
        
}
}