namespace ZoomLa.WebSite.Manage.Template
{
    using System;
    using System.Data;
    using System.IO;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using ZoomLa.Common;
    using ZoomLa.Components;
    using ZoomLa.BLL;
    using ZoomLa.Model;
    using System.Text.RegularExpressions;
    public partial class TemplateList : System.Web.UI.Page
    {
        //当前所处的虚拟目录
        public string VDir
        {
            get
            {
                string _vdir = (Request.QueryString["VDir"] ?? "").TrimStart('/');
                return SiteConfig.SiteOption.TemplateDir + "/" + _vdir;
            }
        }
        public string OpenerText { get { return (Request.QueryString["OpenerText"] ?? "").Trim(); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!Directory.Exists(function.VToP(VDir))) { function.WriteErrMsg("[" + VDir + "]目录不存在"); }
                MyBind();
                //Call.HideBread(Master);
            }
        }
        protected void MyBind()
        {
            string pdir = function.VToP(VDir);
            DataTable filesDT = FileSystemObject.GetDirectoryInfos(pdir, FsoMethod.All);
            filesDT.DefaultView.RowFilter = "name<>'配置库' and name<>'标签库' and name<>'节点库' and name<>'模型库' and name <>'style' and (name like '%.htm%' OR ExName='FileFolder')";
            filesDT = filesDT.DefaultView.ToTable();
            RPT.DataSource = filesDT;
            RPT.DataBind();
            GetBread();
        }
        public string GetLink()
        {
            string optxt = "&OpenerText="+OpenerText;
            string name = Eval("Name").ToString();
            string path = GetVPath(Eval("FullPath").ToString()).TrimStart('/');
            string reuslt = GetExtIcon(Eval("ExName").ToString().ToLower());
            switch (Eval("ExName").ToString().ToLower())
            {
                case "filefolder":
                    return reuslt + "<a href='TemplateList.aspx?vdir=" + path + optxt + "'>" + name + "</a>";
                case ".htm":
                case ".html":
                case ".shtml":
                    return reuslt + "<a href='javascript:;' onclick=\"SelHtmlFile('" + path + "');\">" + name + "</a>";
                default:
                    return reuslt + name;
            }
        }
        //物理-->虚拟-->去除模板目录
        public string GetVPath(string path)
        {
            string _vpath = function.PToV(path);
            _vpath = Regex.Replace(_vpath, SiteConfig.SiteOption.TemplateDir, "", RegexOptions.IgnoreCase);
            return _vpath;
        }
        public void GetBread()
        {
            string vdir = Request.QueryString["VDir"] ?? "";
            if (string.IsNullOrEmpty(vdir)) { Bread_L.Text = "根目录"; }
            else
            {
                string url = "TemplateList.aspx?OpenerText=" + OpenerText + "&vdir=";
                string[] dirArr = vdir.Split("/".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                Bread_L.Text += "<a href='" + url + "'>全部文件</a>";
                for (int i = 0; i < dirArr.Length; i++)
                {
                    //上一级目录链接
                    url += dirArr[i] + "/";
                    Bread_L.Text += "<i class='fa fa-angle-right spanr'></i>";
                    if (i == (dirArr.Length - 1)) { Bread_L.Text += "<span>" + dirArr[i] + "</span>"; }
                    else { Bread_L.Text += "<a href='" + url + "'>" + dirArr[i] + "</a>"; }
                    //设置返回上一级
                    if (dirArr.Length == 1) { Bread_L.Text = "<a href='TemplateList.aspx?OpenerText=" + OpenerText+"'>返回上一级</a> | " + Bread_L.Text; }
                    else if (i == (dirArr.Length - 2))
                    {
                        Bread_L.Text = "<a href='" + url + "'>返回上一级</a> | " + Bread_L.Text;
                    }
                }
            }
        }
        private string GetExtIcon(string ext)
        {
            switch (ext)
            {
                case "filefolder":
                    return "<i class='fa fa-folder' style='color:#4586BD;'></i> ";
                case ".htm":
                case ".html":
                case ".shtml":
                    return "<i class='fa fa-code' style='color:#4586BD;'></i> ";
                default:
                    return "";
            }
        }
    }
}