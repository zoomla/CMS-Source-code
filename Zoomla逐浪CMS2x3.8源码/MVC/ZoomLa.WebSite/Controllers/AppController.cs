using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using ZoomLa.BLL;
using ZoomLa.BLL.CreateJS;
using ZoomLa.BLL.Helper;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;
using ZoomLa.Model.CreateJS;
using ZoomLa.SQLDAL;
using ZoomLa.SQLDAL.SQL;

namespace ZoomLaCMS.Controllers
{
    public class AppController : Controller
    {
        B_User buser = new B_User();
        B_App appBll = new B_App();
        B_APP_Auth authBll = new B_APP_Auth();
        B_QrCode codeBll = new B_QrCode();
        B_App_AppTlp tlpBll = new B_App_AppTlp();
        B_CreateHtml chtmlBll = new B_CreateHtml();
        public int PSize { get { return DataConverter.CLng(Request["psize"]); } }
        public int CPage
        {
            get { int _cpage = DataConverter.CLng(Request["cpage"]); if (_cpage < 1) { _cpage = 1; } return _cpage; }
        }
        //为防止用户任意修改模板,限定为必须以 /App/AppTlp/开头
        public string VPath
        {
            get
            {
                ///App/AppTlp/
                string _vpath = (Request.QueryString["vpath"] ?? "").ToLower();
                _vpath = SafeSC.PathDeal(_vpath);
                if (string.IsNullOrEmpty(_vpath)) { function.WriteErrMsg("未指定模板"); }
                if (!_vpath.StartsWith("/app/apptlp/")) { function.WriteErrMsg("根路径不正确,只允许修改APP模板"); }
                return _vpath;
            }
        }
        public ActionResult Default()
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
            B_User.CheckIsLogged(Request.RawUrl);
            ViewBag.url = "www.z01.com";
            ViewBag.showmsg = !DomainCheck();
            ViewBag.authsp = SafeSC.ReadFileStr("/APP/Other/auth.html");
            int APKMode = DataConverter.CLng(Request["APKMode"]);
            ViewBag.apkmode = APKMode;
            return View();
        }
        public void CreateAppByUrl()
        {
            M_App appMod = InitModel(Request.Form["url_t"].Trim(' '));
            appMod.ID = appBll.Insert(appMod);
            appMod.APKMode = 0;
            Response.Redirect("step2?ID=" + appMod.ID);
        }
        public void CreateAppByZip()
        {
            M_App appMod = InitModel("");
            //if (!Zip_F.SaveAs(function.PToV(appMod.APPDir) + "www.zip")) { function.WriteErrMsg(Zip_F.ErrorMsg); }
            appMod.ZipFile = "www.zip";
            appMod.APKMode = 1;
            appMod.ID = appBll.Insert(appMod);
            Response.Redirect("step2?ID=" + appMod.ID);
        }
        private M_App InitModel(string url)
        {
            M_UserInfo mu = buser.GetLogin();
            M_App appMod = new M_App();
            appMod.AppName = "temp";
            appMod.Furl = StrHelper.UrlDeal(url);
            appMod.MyStatus = 0;
            appMod.UserID = mu.UserID.ToString();
            appMod.APPDir = Server.MapPath("/APP/APP/" + mu.UserName + mu.UserID + "/" + appMod.AppName + DateTime.Now.ToString("yyyyMMddHHmm") + "/");
            if (!Directory.Exists(appMod.APPDir)) { Directory.CreateDirectory(appMod.APPDir); }
            return appMod;
        }
        private bool DomainCheck()
        {
            string[] urlArr = { "app.z01.com", "demo.z01.com", "www.z01.com", "z01.com" };
            string host = Request.Url.Host.ToLower();
            foreach (string url in urlArr)
            {
                if (host.Equals(url)) { return true; }
            }
            return false;
        }
        public ActionResult APPList()
        {
            B_User.CheckIsLogged(Request.RawUrl);
            M_UserInfo mu = buser.GetLogin();
            DataTable dt = appBll.SelBySite(mu.UserID.ToString());
            return View(dt);
        }
        public void APPList_Down()
        {
            M_UserInfo mu = buser.GetLogin();
            M_App appMod = appBll.Select(DataConverter.CLng(Request["ID"]));
            if (!mu.UserID.ToString().Equals(appMod.UserID)) { function.WriteErrMsg("你无权下载该APK"); return; }
            string fpath = appMod.APPDir + "\\" + appMod.AppName + ".apk";
            if (!System.IO.File.Exists(fpath)) { function.WriteErrMsg("文件不存在"); }
            SafeSC.DownFile(function.PToV(fpath));
        }
        //管理端处理用户的授权申请
        public ActionResult AuthList()
        {
            if (!B_Admin.CheckIsLogged(Request.RawUrl)) { return null; }
            int filter = DataConverter.CLng(Request["Filter"]);
            PageSetting setting = authBll.SelPage(CPage, PSize, filter);
            ViewBag.navLabel = "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='" + CustomerPageAction.customPath2 + "WeiXin/Home.aspx'>移动微信</a></li><li class='active'>授权审核</li>";
            return View(setting);
        }
        public int Auth_Audit(string ids)
        {
            ids = authBll.AuditApply(ids);
            if (Request["sendmail"] != "no") { SendMailByIDS(ids); }
            return 1;
        }
        public int Auth_UNAudit(string ids)
        {
            authBll.UnAuditApply(ids);
            return 1;
        }
        public int Auth_Del(string ids)
        {
            authBll.DelByIDS(ids);
            return 1;
        }
        #region 发送邮件
        private void SendMailByIDS(string ids)
        {
            if (!string.IsNullOrEmpty(ids))//发送邮件
            {
                string emailTlp = SafeSC.ReadFileStr("/APP/Other/mail.html");
                DataTable dt = authBll.SelByIDS(ids);
                foreach (string tid in ids.Split(','))
                {
                    dt.DefaultView.RowFilter = "ID=" + tid;
                    SendAuthedMail(emailTlp, dt.DefaultView.ToTable());
                }
            }
        }
        private void SendAuthedMail(string emailTlp, DataTable dt)
        {
            if (dt.Rows.Count < 1) { function.WriteErrMsg("授权申请不存在"); }
            MailAddress adMod = new MailAddress(dt.Rows[0]["Email"].ToString());
            MailInfo mailInfo = new MailInfo() { ToAddress = adMod, IsBodyHtml = true };
            mailInfo.FromName = SiteConfig.SiteInfo.SiteName;
            mailInfo.Subject = SiteConfig.SiteInfo.SiteName + "APP授权审核邮件";
            mailInfo.MailBody = new OrderCommon().TlpDeal(emailTlp, dt);
            if (SendMail.Send(mailInfo) == ZoomLa.Components.SendMail.MailState.Ok)//发送成功,生成用户,显示下一步提示
            {
                function.WriteErrMsg("发送成功!");
            }
            else
            {
                ZLLog.L(ZLEnum.Log.exception, adMod.Address + "的邮件发送失败,请检测邮箱地址是否正确!");
            }
        }
        #endregion
        public ActionResult CL()
        {
            if (!B_Admin.CheckIsLogged(Request.RawUrl)) { return null; }
            M_QrCode qrcodeMod = null;
            int Mid = DataConverter.CLng(Request["ID"]);
            if (Mid > 0)
            {
                qrcodeMod = codeBll.SelReturnModel(Mid);
            }
            if (qrcodeMod != null)
            {
                ViewBag.alias = qrcodeMod.UserName;
                ViewBag.android = codeBll.GetUrlByAgent(DeviceHelper.Agent.Android, qrcodeMod);
                ViewBag.iphone = codeBll.GetUrlByAgent(DeviceHelper.Agent.iPhone, qrcodeMod);
                ViewBag.ipad = codeBll.GetUrlByAgent(DeviceHelper.Agent.iPad, qrcodeMod);
                ViewBag.wphone = codeBll.GetUrlByAgent(DeviceHelper.Agent.WindowsPhone, qrcodeMod);
                ViewBag.pc = codeBll.GetUrlByAgent(DeviceHelper.Agent.PC, qrcodeMod);
                string url = StrHelper.UrlDeal(SiteConfig.SiteInfo.SiteUrl + "/app/appurl?id=" + qrcodeMod.ID);
                ViewBag.link = codeBll.GetUrl(qrcodeMod.ID);
                ViewBag.code = "<img src='/Common/Common.ashx?url=" + url + "' class='codeimg'  />";
            }
            ViewBag.navLabel = "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='/APP/Default'>移动应用</a></li><li><a href='CLList'>APP颁发</a></li><li class='active'>颁发管理</li>";
            return View();
        }
        public void CreateLink()
        {
            int Mid = DataConverter.CLng(Request["ID"]);
            M_QrCode codeMod = null;
            if (Mid > 0) { codeMod = codeBll.SelReturnModel(Mid); }
            if (codeMod == null) { codeMod = new M_QrCode(); }
            codeMod.UserName = Request.Form["alias_t"].Trim();
            codeMod.Urls = DeviceHelper.Agent.Android + "$" + StrHelper.UrlDeal(Request.Form["android_t"].Trim()) + ","
                            + DeviceHelper.Agent.iPhone + "$" + StrHelper.UrlDeal(Request.Form["iphone_t"].Trim()) + ","
                            + DeviceHelper.Agent.iPad + "$" + StrHelper.UrlDeal(Request.Form["ipad_t"].Trim()) + ","
                            + DeviceHelper.Agent.WindowsPhone + "$" + StrHelper.UrlDeal(Request.Form["wphone_t"].Trim()) + ","
                            + DeviceHelper.Agent.PC + "$" + StrHelper.UrlDeal(Request.Form["pc_t"].Trim());
            if (codeMod.ID <= 0)
            {
                codeMod.AppID = DataConverter.CLng(function.GetRandomString(6, 2));
                codeMod.ID = Sql.insertID(codeBll.strTableName, codeMod.GetParameters(codeMod), BLLCommon.GetParas(codeMod), BLLCommon.GetFields(codeMod));
            }
            else { codeBll.UpdateByID(codeMod); }
            Response.Redirect("CL?ID=" + codeMod.ID);
        }
        public ActionResult CLList()
        {
            if (!B_Admin.CheckIsLogged(Request.RawUrl)) { return null; }
            PageSetting setting = codeBll.SelPage(CPage, PSize);
            ViewBag.navLabel = "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='/APP/Default'>移动应用</a></li><li><a href='" + Request.RawUrl + "'>APP颁发</a> [<a href='CL'>添加颁发</a>]</li>";
            return View(setting);
        }
        public int CL_Del(string ids)
        {
            codeBll.DelByIDS(ids);
            return 1;
        }
        public ActionResult NoAuth()
        {
            return View();
        }
        public ActionResult Step2()
        {
            B_User.CheckIsLogged(Request.RawUrl);
            int Mid = DataConverter.CLng(Request["ID"]);
            M_App appMod = appBll.Select(Mid);
            M_UserInfo mu = buser.GetLogin();
            if (appMod == null || Convert.ToInt32(appMod.UserID) != mu.UserID) { function.WriteErrMsg("APP不存在,或你无权访问该APP"); }
            if (appMod.APKMode == 0) { ViewBag.ifrsrc = appMod.Furl; }
            PageSetting setting = tlpBll.SelPage(CPage, PSize, mu.UserID);
            ViewBag.chkdomain = DomainCheck();
            ViewBag.chkauth = APPAuthCheck();
            return View(setting);
        }
        public PartialViewResult AppTlp_Data()
        {
            M_UserInfo mu = buser.GetLogin();
            PageSetting setting = tlpBll.SelPage(CPage, PSize, mu.UserID);
            return PartialView("Step2_List", setting);
        }
        public void App_Create()
        {
            if (!Directory.Exists(@"C:\APPTlp\")) { function.WriteErrMsg("环境未配置,APP模板目录不存在!"); }
            string xml = SafeSC.ReadFileStr("/APP/Res/config.xml");
            int Mid = DataConverter.CLng(Request["ID"]);
            M_UserInfo mu = buser.GetLogin();
            M_App appMod = appBll.Select(Mid);
            if (appMod == null || DataConverter.CLng(appMod.UserID) != mu.UserID) { function.WriteErrMsg("APP不存在,或你无权访问该APP"); }
            //------------------------------------------------------------------
            appMod.AppName = Request.Form["appname"].Trim();
            appMod.Author = Request.Form["author"].Trim();
            appMod.Description = Request.Form["description"];
            appMod.Template = "";
            int tlpID = DataConverter.CLng(Request.Form["idrad"]);
            if (tlpID > 0)
            {
                M_APP_APPTlp tlpMod = tlpBll.SelReturnModel(tlpID);
                appMod.Template = Server.MapPath(tlpMod.TlpUrl);
            }
            string appicon = Request.Form["appicon"];
            string splash = Request.Form["splash"];
            SaveFile(appMod.APPDir + "\\icon.png", appicon, "/APP/Res/icon.png");//安装时显示的图标
            SaveFile(appMod.APPDir + "\\screen.png", splash, "/APP/Res/screen.png");//程序图标
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
            System.IO.File.WriteAllBytes(appMod.APPDir + "\\config.xml", System.Text.Encoding.UTF8.GetBytes(xml));
            appBll.Update(appMod);
            function.WriteSuccessMsg("已提交APP生成申请,正在审核生成,请稍等一分钟左右", "APPList");
        }
        //物理路径,上传控件,如无文件时默认文件虚拟路径
        private void SaveFile(string path, string file, string defVPath)
        {
            path = path.Replace(@"\\", @"\");
            if (System.IO.File.Exists(path)) { System.IO.File.Delete(path); }
            string filepath = function.VToP(file);
            if (!string.IsNullOrEmpty(file) && System.IO.File.Exists(filepath))
            {
                byte[] data = System.IO.File.ReadAllBytes(filepath);
                SafeSC.SaveFile(function.PToV(path), "", data);
            }
            else
            {
                if (!string.IsNullOrEmpty(defVPath))
                { System.IO.File.Copy(Server.MapPath(defVPath), path); }
            }
        }
        //授权检测
        private bool APPAuthCheck()
        {
            string asmx = "http://www.z01.com/Api/Center.asmx";
            string cert = SiteConfig.SiteOption.WxAppID;
            if (string.IsNullOrEmpty(cert)) { return false; }
            var result = new WSHelper().InvokeWS(asmx, "Center", "Center", "APPCertCheck", new object[] { cert });
            return Convert.ToBoolean(result);
        }
        public void AppUrl()
        {
            int appId = DataConverter.CLng(Request["appid"]);
            int Mid = DataConverter.CLng(Request["ID"]);
            string agent = Request.UserAgent;
            M_QrCode qrcodeMod = null;
            if (Mid > 0) { qrcodeMod = codeBll.SelReturnModel(Mid); }
            else if (appId > 0) { qrcodeMod = codeBll.SelModelByAppID(appId); }
            else { function.WriteErrMsg("参数不能为空"); }
            if (qrcodeMod == null) { function.WriteErrMsg("参数错误,该链接不存在"); }
            string url = GetAppUrl(qrcodeMod, DeviceHelper.GetAgent(agent), DeviceHelper.GetBrower(agent));
            url = string.IsNullOrEmpty(url) ? GetAppUrl(qrcodeMod, DeviceHelper.Agent.Android, DeviceHelper.GetBrower(agent)) : url;
            if (string.IsNullOrEmpty(url)) { function.WriteErrMsg("未为该设备指定链接"); }
            Response.Redirect(url);
        }
        private string GetAppUrl(M_QrCode model, DeviceHelper.Agent agent, DeviceHelper.Brower brower)
        {
            string url = "";
            if (string.IsNullOrEmpty(model.Urls)) return url;
            url = codeBll.GetUrlByAgent(agent, model);
            switch (agent)
            {
                case DeviceHelper.Agent.iPhone:
                case DeviceHelper.Agent.iPad:
                    switch (brower)
                    {
                        case DeviceHelper.Brower.Micro://如果是微信,并且是分发市场的Url,则提示其用外置浏览器打开
                            string html = SafeSC.ReadFileStr("/APP/AppStore.html");
                            //html = html.Replace("@Device", "");
                            Response.Clear();
                            Response.Write(html); Response.Flush(); Response.End();
                            break;
                    }
                    break;
            }
            return url;
        }
        public ActionResult Design()
        {
            B_User.CheckIsLogged();
            int Mid = DataConverter.CLng(Request["ID"]);
            if (Mid > 0)
            {
                M_UserInfo mu = buser.GetLogin();
                M_APP_APPTlp tlpMod = tlpBll.SelReturnModel(Mid);
                if (tlpMod.UserID != mu.UserID) { function.WriteErrMsg("你无权编辑该模板!"); }
            }
            ViewBag.vpath = VPath;
            return View();
        }
        public void Design_Save()
        {
            M_UserInfo mu = buser.GetLogin();
            M_APP_APPTlp tlpMod = new M_APP_APPTlp();
            string saveurl = "", fname = "";
            int Mid = DataConverter.CLng(Request["ID"]);
            //--------------------------------
            string head = HttpUtility.UrlDecode(Request.Form["Head_Hid"]);
            string html = Request.Form["AllHtml_Hid"];
            int start = html.IndexOf(Call.Boundary) + Call.Boundary.Length;
            int len = html.Length - start;
            html = "<body>" + html.Substring(start, len);//处理iframe中标签错位Bug
            head = head.Replace(Call.Boundary, html);
            if (Mid > 0)//更新
            {
                tlpMod = tlpBll.SelReturnModel(Mid);
                tlpMod.Alias = Request.Form["TlpName_T"];
                tlpBll.UpdateByID(tlpMod);
                SafeSC.WriteFile(tlpMod.TlpUrl, head);
            }
            else
            {
                saveurl = "/App/AppTlp/Users/" + mu.UserName + mu.UserID + "/";//存储模板路径
                fname = DateTime.Now.ToString("yyyyMMddhhmm") + function.GetRandomString(4) + Path.GetExtension(VPath);
                tlpMod.CDate = DateTime.Now;
                tlpMod.Alias = Request.Form["TlpName_T"];
                tlpMod.TlpUrl = saveurl + fname;
                tlpMod.UserID = mu.UserID;
                tlpBll.Insert(tlpMod);
                SafeSC.WriteFile(saveurl + fname, head);
            }
            function.WriteSuccessMsg("模板保存成功", "/App//MyTlpList");
        }
        public ActionResult MyTlpList()
        {
            B_User.CheckIsLogged(Request.RawUrl);
            PageSetting setting = tlpBll.SelPage(CPage,PSize,buser.GetLogin().UserID);
            return View(setting);
        }
        public void Tlp_Down(int id)
        {
            M_APP_APPTlp tlpMod = tlpBll.SelReturnModel(id);
            if (tlpMod.UserID != buser.GetLogin().UserID) { function.WriteErrMsg("你没有下载该模板的权限"); }
            SafeSC.DownFile(tlpMod.TlpUrl);
        }
        public int Tlp_Del(string ids)
        {
            tlpBll.DelByIDS(ids);
            return 1;
        }
        public ActionResult TlpList()
        {
            string xmlPath = function.VToP("/APP/APPTlp/APPTlp.config");
            DataSet ds = new DataSet();
            ds.ReadXml(xmlPath);
            DataTable dt = ds.Tables[0];
            return View(dt);
        }
        public ActionResult TlpShow()
        {
            string vpath = Request["vpath"];
            if (string.IsNullOrEmpty(vpath)) { function.WriteErrMsg("路径不能为空"); }
            string html = SafeSC.ReadFileStr(VPath);
            return Content(chtmlBll.CreateHtml(html));
        }
        public ActionResult Client()
        {
            int site = DataConvert.CLng(Request.QueryString["s"]);
            string baseurl = "http://app.z01.com/APP/";
            if (site == 1) { baseurl = "http://www.z01.com/APP/"; }
            ViewBag.ifrsrc = baseurl + Request.QueryString["U"];
            ViewBag.title = Request.QueryString["T"];
            return View();
        }
        public ActionResult AuthApply()
        {
            return View();
        }
        public void AuthApply_Submit()
        {
            M_APP_Auth authMod = new M_APP_Auth();
            authMod.SiteUrl = StrHelper.UrlDeal(Request.Form["siteurl"]);
            authMod.Contact = Request.Form["contact"];
            authMod.MPhone = Request.Form["mphone"];
            authMod.Email = Request.Form["email"];
            authMod.QQCode = Request.Form["qqcode"];
            authBll.Insert(authMod);
            function.WriteSuccessMsg("授权申请提交成功,请等待管理员审核", "AuthApply");
        }
    }
}
