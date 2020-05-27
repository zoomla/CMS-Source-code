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
using ZoomLa.BLL.Helper;
using ZoomLa.BLL.User;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;
using ZoomLa.Model.User;

namespace ZoomLaCMS.App
{
    public partial class Default : CustomerPageAction
    {
        B_App appBll = new B_App();
        B_User buser = new B_User();
        private int APKMode { get { return DataConverter.CLng(Request.QueryString["APKMode"]); } }
        private void SyncUser()
        {
            B_Admin badmin = new B_Admin();
            if (badmin.CheckLogin() && !buser.CheckLogin())
            {
                M_AdminInfo adminMod = B_Admin.GetLogin();
                M_UserInfo mu = new M_UserInfo();
                if (adminMod.AddUserID > 0) { mu = buser.SelReturnModel(adminMod.AddUserID); }
                if (mu.UserID < 1)
                {
                    mu = buser.AuthenticateUser(adminMod.AdminName, adminMod.AdminPassword, true);
                }
                if (mu.UserID > 0)
                {
                    buser.SetLoginState(mu);
                }
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            SyncUser();
            B_User.CheckIsLogged(Request.RawUrl);
            if (!IsPostBack)
            {
                Url_T.Text = "www.z01.com";
                remind_sp.Visible = (!DomainCheck());
                auth_sp.InnerHtml = SafeSC.ReadFileStr("/APP/Other/auth.html");
                switch (APKMode)
                {
                    case 0:
                    default:
                        apkmode0_div.Visible = true;
                        break;
                    case 1:
                        apkmode1_div.Visible = true;
                        break;
                }
            }
        }
        protected void SetUrl_Btn_Click(object sender, EventArgs e)
        {
            M_App appMod = InitModel();
            appMod.ID = appBll.Insert(appMod);
            appMod.APKMode = 0;
            Response.Redirect("step2.aspx?ID=" + appMod.ID);
        }
        protected void SetAPP_Btn_Click(object sender, EventArgs e)
        {
            M_App appMod = InitModel();
            //SafeSC.SaveFile(function.PToV(appMod.APPDir), Zip_F, "www.zip");
            if (!Zip_F.SaveAs(function.PToV(appMod.APPDir) + "www.zip")) { function.WriteErrMsg(Zip_F.ErrorMsg); }
            appMod.ZipFile = "www.zip";
            appMod.APKMode = 1;
            appMod.ID = appBll.Insert(appMod);
            Response.Redirect("step2.aspx?ID=" + appMod.ID);
        }
        private M_App InitModel()
        {
            //if (!DomainCheck())
            //{
            //    bool result = APPAuthCheck();
            //    if (!result)//未授权则跳转至授权页
            //    {
            //        Response.Redirect("NoAuth.aspx"); Response.End(); return null;
            //    }
            //}
            M_UserInfo mu = buser.GetLogin();
            M_App appMod = new M_App();
            appMod.AppName = "temp";
            appMod.Furl = StrHelper.UrlDeal(Url_T.Text.Trim());
            appMod.MyStatus = 0;
            appMod.UserID = mu.UserID.ToString();
            appMod.APPDir = Server.MapPath("/APP/APP/" + mu.UserName + mu.UserID + "/" + appMod.AppName + DateTime.Now.ToString("yyyyMMddHHmm") + "/");
            if (!Directory.Exists(appMod.APPDir)) { Directory.CreateDirectory(appMod.APPDir); }
            return appMod;
        }
        //授权检测
        private bool APPAuthCheck()
        {
            string asmx = "http://www.zoomal.cn/Api/Center.asmx";
            //asmx = "http://win01:86/Api/Center.asmx";
            string cert = SiteConfig.SiteOption.WxAppID;
            if (string.IsNullOrEmpty(cert)) { return false; }
            var result = new WSHelper().InvokeWS(asmx, "Center", "Center", "APPCertCheck", new object[] { cert });
            return Convert.ToBoolean(result);
        }
        //通过返回True
        private bool DomainCheck()
        {
            string[] urlArr = { "app.z01.com", "app.z01.com", "www.z01.com", "z01.com" };
            string host = Request.Url.Host.ToLower();
            foreach (string url in urlArr)
            {
                if (host.Equals(url)) { return true; }
            }
            return false;
        }
    }
}