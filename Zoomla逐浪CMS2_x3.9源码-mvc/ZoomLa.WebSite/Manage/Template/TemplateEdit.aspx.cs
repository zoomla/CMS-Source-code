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
using ZoomLa.Model;
using System.Xml;
using ZoomLa.SQLDAL;
using ZoomLa.Model.Content;
using ZoomLa.BLL.Content;


namespace ZoomLaCMS.Manage.Template
{
    public partial class TemplateEdit : CustomerPageAction
    {
        protected string FileName = string.Empty;
        protected string ShowPath = string.Empty;
        public string TemplateDir = "";
        private B_Label bll = new B_Label();
        private B_FunLabel bfun = new B_FunLabel();
        protected M_DataSource dsModel = new M_DataSource();
        protected B_DataSource dsBll = new B_DataSource();
        protected DataTable dsTable = new DataTable();
        protected B_Admin badmin = new B_Admin();
        public string FileVPath
        {
            get
            {
                if (ViewState["FilePath"] == null)
                {
                    ViewState["FilePath"] = PathDeal(Request.QueryString["filepath"]);
                }
                return ViewState["FilePath"].ToString();
            }
            set { ViewState["FilePath"] = value; }
        }
        private void DPBind()
        {
            CustomLabel_DP.DataSource = bll.GetLabelCateListXML();
            CustomLabel_DP.DataTextField = "name";
            CustomLabel_DP.DataValueField = "name";
            CustomLabel_DP.DataBind();
            CustomLabel_DP.Items.Insert(0, new ListItem("选择标签类型", ""));
            Field_DP.DataSource = bll.GetSourceLabelXML();//LabelType
            Field_DP.DataTextField = "LabelName";
            Field_DP.DataValueField = "LabelID";
            Field_DP.DataBind();
            Field_DP.Items.Insert(0, new ListItem("选择数据源标签", ""));
        }
        //返回物理路径
        private string PathDeal(string strPath)
        {
            //if (strPath.Trim().Contains(" ")) { function.WriteErrMsg("提示:模板名不能包含空格,为了安全和规范请将模板名改为无空格方式"); }
            TemplateDir = SiteConfig.SiteOption.TemplateDir;//   /Template/V3
            if (!string.IsNullOrEmpty(Request.QueryString["setTemplate"]))//带有指定目录的,如方案设置处跳转
            {
                TemplateDir = Request.QueryString["setTemplate"];
            }
            strPath = TemplateDir + strPath;
            strPath = "/" + strPath.Trim().TrimStart('/').Replace("//", "/");
            if (strPath.Replace(" ", "").Contains("./")) { function.WriteErrMsg("模板文件名格式不正确"); }
            return strPath;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!B_ARoleAuth.Check(ZLEnum.Auth.label, "TemplateEdit"))
            {
                function.WriteErrMsg("没有权限进行此项操作");
            }
            if (!IsPostBack)
            {
                if (FileVPath.Contains(" ")) { whitespace_sp.Visible = true; }
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>" + Resources.L.工作台 + "</a></li><li><a href='TemplateSet.aspx'>" + Resources.L.模板风格 + "</a></li><li><a href='TemplateManage.aspx'>模板列表</a></li><li><a href='" + Request.RawUrl.ToString() + "'>模板编辑</a></li>" + Call.GetHelp(26));
                dsTable = dsBll.Sel();
                ViewState["dsTable"] = dsTable;
                DPBind();
                if (string.IsNullOrEmpty(Request.QueryString["filepath"]))
                    ShowPath = "/";
                else
                    ShowPath = Request.QueryString["filepath"];
                string FileExp = Path.GetExtension(FileVPath);
                if (!string.IsNullOrEmpty(FileExp))
                {
                    FileName = Path.GetFileName(FileVPath);
                    ShowPath = ShowPath.Replace(FileName, "").Replace("//", "/");
                    TempUrl_L.Text = ShowPath + FileName;
                    TempUrl_L.Attributes.Add("disabled", "disabled");

                    string fname = FileName.Contains(".") ? FileName.Split('.')[0] : FileName;
                    string exname = FileName.Contains(".") ? FileName.Split('.')[1] : "html";
                    if (exname.ToLower().Equals("html"))
                    {
                        TxtFilename.Value = fname;
                        name_L.Text = "." + exname;
                    }
                    else { function.WriteErrMsg("无权修改.html以外的文件！"); }
                    textContent.Text = SafeSC.ReadFileStr(FileVPath);
                    Hdnmethod.Value = "append";
                    edit_div.Visible = true;
                    if (Path.GetExtension(FileVPath).ToLower().Equals(".html"))
                    {
                        ViewEdit_A.Visible = true;
                        ViewEdit_A.HRef = customPath2 + "Template/Design.aspx?vpath=" + HttpUtility.UrlEncode(FileVPath);
                    }
                }
                else
                {
                    FileName = "";
                    TxtFilename.Value = "";
                    TxtFilename.Visible = true;
                    Hdnmethod.Value = "add";
                    add_div.Visible = true;
                }
                lblSys.Text = bfun.GetSysLabel();//系统标签列表
                lblFun.Text = bfun.GetFunLabel();//扩展函数列表
                HdnShowPath.Value = ShowPath;
            }
        }
        public bool CheckIsOk(string exName)
        {
            bool flag = false;
            string[] allowEx = new string[] { ".html", ".htm", ".config", ".xml", ".css", ".js", ".txt" };
            foreach (string s in allowEx)
            {
                if (exName == s) flag = true;
            }
            return flag;
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            int num = TxtFilename.Value.IndexOf(".");
            if (num > 0)
            {
                TxtFilename.Value = TxtFilename.Value.Substring(0, num);
            }
            TxtFilename.Value = TxtFilename.Value + ".html";
            string fileExtension = System.IO.Path.GetExtension(TxtFilename.Value.Trim()).ToLower();
            if (Hdnmethod.Value == "add")
            {
                SafeSC.WriteFile(FileVPath + "/" + TxtFilename.Value, textContent.Text);
            }
            else if (Hdnmethod.Value == "append")
            {
                SafeSC.WriteFile(FileVPath, textContent.Text);
            }
            string sPath = HdnShowPath.Value;//.Replace("", "");
            function.WriteSuccessMsg("操作成功", "TemplateManage.aspx?setTemplate=" + Server.UrlEncode(TemplateDir) + "&Dir=" + sPath);
        }

        //protected void btnReset_Click(object sender, EventArgs e)
        //{
        //    if (Hdnmethod.Value == "add")
        //    {
        //        textContent.Text = "";
        //    }
        //    if (Hdnmethod.Value == "append")
        //    {
        //        string str = SafeSC.ReadFileStr(FileVPath);
        //        textContent.Text = str;
        //    }
        //}

        protected void btn_Click(object sender, EventArgs e)
        {
            string filepath = function.GetFileName() + ".txt";
            FileSystemObject.WriteFile(SiteConfig.SiteMapath() + "Template\\" + filepath, textContent.Text);
            function.Script(this, "opentitle('Default.aspx?filename=" + filepath + "','可视化窗口[按ESC键关闭窗口]');");
        }

        protected string getExternalConnStr(M_Label label)
        {
            try
            {
                string id = label.DataSourceType;
                dsTable = ViewState["dsTable"] == null ? dsBll.Sel() : ViewState["dsTable"] as DataTable;
                return dsTable.Select("ID='" + id + "'")[0][dsTable.Columns.IndexOf("ConnectionString")] as string;
            }
            catch { return SqlHelper.ConnectionString; }
        }
    }
}