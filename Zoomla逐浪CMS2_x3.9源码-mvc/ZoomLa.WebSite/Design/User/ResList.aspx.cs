using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Design;
using ZoomLa.BLL.Plat;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;
using ZoomLa.Model.Design;
using ZoomLa.Model.Plat;

namespace ZoomLaCMS.Design.User
{
    public partial class ResList : System.Web.UI.Page
    {
        //只给其一级目录
        B_Design_SiteInfo sfBll = new B_Design_SiteInfo();
        B_User buser = new B_User();
        public int SiteID { get { return DataConverter.CLng(Request.QueryString["SiteID"]); } }
        //当前所处的虚拟目录(不含根路径)
        public string VDir
        {
            get
            {
                string _vdir = (Request.QueryString["VDir"] ?? "").TrimStart('/');
                return B_Design_SiteInfo.GetSiteUpDir(SiteID) + _vdir;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MyBind();
                //Call.HideBread(Master);
            }
        }
        protected void MyBind()
        {

            M_UserInfo mu = buser.GetLogin();
            M_Design_SiteInfo sfMod = sfBll.SelReturnModel(SiteID);
            sfBll.CheckAuthEx(sfMod, mu);
            string pdir = function.VToP(VDir);
            if (!Directory.Exists(pdir)) { function.WriteErrMsg("[" + VDir + "]目录不存在"); }
            DataTable filesDT = FileSystemObject.GetDirectoryInfos(pdir, FsoMethod.All);
            RPT.DataSource = filesDT;
            RPT.DataBind();
            GetBread();
        }
        public string GetLink()
        {
            string name = Eval("Name").ToString();
            string path = GetVPath(Eval("FullPath").ToString()).TrimStart('/');

            string reuslt = GroupPic.GetExtNameMini(Eval("ExName").ToString());
            switch (Eval("ExName").ToString().ToLower())
            {
                case "filefolder":
                    return reuslt + "<a href='ResList.aspx?vdir=" + path + "'>" + name + "</a>";
                default://支持预览
                    return reuslt + "<span>" + name + "</span>";
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
                string url = "ResList.aspx?vdir=";
                string[] dirArr = vdir.Split("/".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                Bread_L.Text += "<a href='" + url + "'>根目录</a>";
                for (int i = 0; i < dirArr.Length; i++)
                {
                    //上一级目录链接
                    url += dirArr[i] + "/";
                    Bread_L.Text += "<i class='fa fa-angle-right spanr'></i>";
                    if (i == (dirArr.Length - 1)) { Bread_L.Text += "<span>" + dirArr[i] + "</span>"; }
                    else { Bread_L.Text += "<a href='" + url + "'>" + dirArr[i] + "</a>"; }
                    //设置返回上一级
                    if (dirArr.Length == 1) { Bread_L.Text = "<a href='ResList.aspx'>返回上一级</a> | " + Bread_L.Text; }
                    else if (i == (dirArr.Length - 2))
                    {
                        Bread_L.Text = "<a href='" + url + "'>返回上一级</a> | " + Bread_L.Text;
                    }
                }
            }
        }
        protected void RPT_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            string fname = e.CommandArgument.ToString();
            string fvpath = VDir + fname;
            switch (e.CommandName)
            {
                case "del":
                    SafeSC.DelFile(fvpath);
                    break;
            }
            Response.Redirect(Request.RawUrl);
        }
    }
}