using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.Configuration;
using System.Text.RegularExpressions;
using System.Collections;
using ZoomLa.Components;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.BLL;
using ZoomLa.Sns;
using System.Xml;

namespace ZoomLaCMS
{
    public partial class Default : FrontPage
    {
        B_CreateHtml bll = new B_CreateHtml();
        B_Sensitivity sll = new B_Sensitivity();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CheckInstalled();
            }
            string vpath = SiteConfig.SiteOption.TemplateDir + "/" + SiteConfig.SiteOption.IndexTemplate.TrimStart('/');
            string TemplateDir = function.VToP(vpath);
            string fileex = ".html";
            switch (SiteConfig.SiteOption.IndexEx)
            {
                case "0":
                    fileex = ".html";
                    break;
                case "1":
                    fileex = ".htm";
                    break;
                case "2":
                    fileex = ".shtml";
                    break;
                case "3":
                    fileex = ".aspx";
                    break;
            }
            if (FileSystemObject.IsExist(Server.MapPath("/index" + fileex), FsoMethod.File)) { Response.Redirect("index" + fileex); }
            if (!FileSystemObject.IsExist(TemplateDir, FsoMethod.File)) { ErrToClient("[产生错误的可能原因：(" + vpath + ")不存在或未开放!]"); }
            else
            {
                string readfile = FileSystemObject.ReadFile(TemplateDir);
                string IndexHtml = this.bll.CreateHtml(readfile);
                if (SiteConfig.SiteOption.IsSensitivity == 1)
                {
                    IndexHtml = sll.ProcessSen(IndexHtml);
                }
                Response.Write(IndexHtml);
            }
        }
        // 检测网站是否安装
        private void CheckInstalled()
        {
            string requesturl = Request.Url.AbsoluteUri;
            if (requesturl.IndexOf("://") > -1)
            {
                string[] rqurl = requesturl.Split(new string[] { "://" }, StringSplitOptions.None);
                int flt = rqurl[1].Split('/').Length;
                if (flt <= 2)
                {
                    string str = WebConfigurationManager.AppSettings["Installed"].ToLower();
                    if (!Convert.ToBoolean(str))
                    {
                        Page.Response.Redirect("Install/Default.aspx");
                    }
                }
                else
                {
                    string str = WebConfigurationManager.AppSettings["Installed"].ToLower();
                    if (!Convert.ToBoolean(str))
                    {
                        function.WriteErrMsg("Error!请在根目录下执行本系统安装。");
                    }
                }
            }
        }
    }
}