using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.User;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;
using ZoomLa.Model.User;

namespace ZoomLaCMS.App
{
    public partial class Step2 : System.Web.UI.Page
    {
        B_App appBll = new B_App();
        B_User buser = new B_User();
        B_App_AppTlp tlpBll = new B_App_AppTlp();
        private int Mid { get { return DataConverter.CLng(Request.QueryString["ID"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            B_User.CheckIsLogged(Request.RawUrl);
            if (!IsPostBack)
            {
                string host = Request.Url.Host.ToLower();
                if (DomainCheck())
                {
                    remind_sp.Visible = false;
                }
                else
                {
                    bool result = APPAuthCheck();
                    if (!result)
                    {
                        hasauth_div.Visible = false;
                        noauth_div.Visible = true;
                    }
                }
                M_App appMod = appBll.Select(Mid);
                M_UserInfo mu = buser.GetLogin();
                if (appMod == null || Convert.ToInt32(appMod.UserID) != mu.UserID) { function.WriteErrMsg("APP不存在,或你无权访问该APP"); }
                APPName_T.Text = SiteConfig.SiteInfo.SiteName;
                switch (appMod.APKMode)
                {
                    case 0:
                        url_ifr.Attributes.Add("src", appMod.Furl);
                        break;
                }
            }
            MyBind();
        }
        protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            EGV.PageIndex = e.NewPageIndex;
            MyBind();
        }
        private void MyBind()
        {
            M_UserInfo mu = buser.GetLogin();
            EGV.DataSource = tlpBll.SelByUid(mu.UserID);
            EGV.DataBind();
        }
        //创建APP
        protected void Create_Btn_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(@"C:\APPTlp\")) { function.WriteErrMsg("环境未配置,APP模板目录不存在!"); }
            string xml = SafeSC.ReadFileStr("/APP/Res/config.xml");
            M_UserInfo mu = buser.GetLogin();
            M_App appMod = appBll.Select(Mid);
            if (appMod == null || Convert.ToInt32(appMod.UserID) != mu.UserID) { function.WriteErrMsg("APP不存在,或你无权访问该APP"); }
            //------------------------------------------------------------------
            appMod.AppName = APPName_T.Text.Trim();
            appMod.Author = Author_T.Text;
            appMod.Description = Description_T.Text;
            appMod.Template = "";
            int tlpID = DataConverter.CLng(Request.Form["idrad"]);
            if (tlpID > 0)
            {
                M_APP_APPTlp tlpMod = tlpBll.SelReturnModel(tlpID);
                appMod.Template = Server.MapPath(tlpMod.TlpUrl);
            }
            //appMod.Thumbnails = "";
            //appMod.Clearimg = "";
            //appMod.Colors="";
            //appMod.Feature = "";
            SaveFile(appMod.APPDir + "\\icon.png", APPIcon_F, "/APP/Res/icon.png");//安装时显示的图标
            SaveFile(appMod.APPDir + "\\screen.png", Splash_F, "/APP/Res/screen.png");//程序图标
            switch (appMod.APKMode)
            {
                case 0:
                    xml = xml.Replace("@launchUrl", appMod.Furl);
                    break;
                case 1:
                    xml = xml.Replace("@launchUrl", "file:///android_asset/www/index.html");
                    break;
            }
            xml = xml.Replace("@APPName", appMod.AppName)
                .Replace("@author", appMod.Author).Replace("@description", appMod.Description);
            File.WriteAllBytes(appMod.APPDir + "\\config.xml", Encoding.UTF8.GetBytes(xml));
            appBll.Update(appMod);
            function.WriteSuccessMsg("已提交APP生成申请,正在审核生成,请稍等一分钟左右", "APPList.aspx");
        }
        //物理路径,上传控件,如无文件时默认文件虚拟路径
        private void SaveFile(string path, FileUpload file, string defVPath)
        {
            path = path.Replace(@"\\", @"\");
            string ext = Path.GetExtension(file.FileName).ToLower();
            string dir = Path.GetDirectoryName(path) + "\\";
            //if (!ext.Equals(".png")) { function.WriteErrMsg("图片仅支持png格式"); }
            //if (!Directory.Exists(dir)) { Directory.CreateDirectory(dir); }
            if (File.Exists(path)) { File.Delete(path); }
            if (file.FileContent.Length > 100)
            {
                SafeSC.SaveFile(function.PToV(path), file);
            }
            else
            {
                if (!string.IsNullOrEmpty(defVPath))
                { File.Copy(Server.MapPath(defVPath), path); }
            }
        }
        //授权检测
        private bool APPAuthCheck()
        {
            string asmx = "http://www.z01.com/Api/Center.asmx";
            //asmx = "http://win01:86/Api/Center.asmx";
            string cert = SiteConfig.SiteOption.WxAppID;
            if (string.IsNullOrEmpty(cert)) { return false; }
            var result = new WSHelper().InvokeWS(asmx, "Center", "Center", "APPCertCheck", new object[] { cert });
            return Convert.ToBoolean(result);
        }
        //通过返回True
        private bool DomainCheck()
        {
            string[] urlArr = { "app.z01.com", "www.z01.com", "z01.com", "demo.z01.com" };
            string host = Request.Url.Host.ToLower();
            foreach (string url in urlArr)
            {
                if (host.Equals(url)) { return true; }
            }
            return false;
        }
    }
}