using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Common;
using System.Net.Mail;
using ZoomLa.Components;
using System.Data;

public partial class Common_SubScriptCheck : System.Web.UI.Page
{
    B_Mail_BookRead readBll = new B_Mail_BookRead();
    B_MailManage mailBll = new B_MailManage();
    B_User buser = new B_User();
    //验证代码
    public string AuthCode { get {return Request.QueryString["authcode"]; } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (function.isAjax())
        {
            string action = Request.Form["action"];
            string result = "";
            switch (action)
            {
                case "addsub"://订阅邮箱处理
                    {
                        M_UserInfo mu = buser.GetLogin();
                        int userid = mu == null ? 0 : mu.UserID;
                        string uname = userid > 0 ? mu.UserName : "匿名用户";
                        M_Mail_BookRead readMod = readBll.SelByEMail(Request.Form["EMail"]);
                        if (readMod != null && readMod.IsAudit == 1) { result = "-1"; break; }//邮箱已订阅
                        if (readMod != null) { result = SendSubEmail(uname, readMod.AuthCode, readMod.EMail); break; }
                        readMod = new M_Mail_BookRead();
                        readMod.UserID = userid;
                        readMod.Source = Request.UrlReferrer.ToString();
                        readMod.IP = Request.UserHostAddress;
                        readMod.EMail = Request.Form["EMail"];
                        readMod.Browser = Request.Browser.Type;
                        readMod.CDate = DateTime.Now;
                        readMod.IsAudit = 0;//并未验证
                        readMod.AuthCode = function.GetRandomString(10);
                        readBll.GetInsert(readMod);
                        result = SendSubEmail(uname, readMod.AuthCode, readMod.EMail);
                    }
                    break;
                default:
                    break;
            }
            Response.Write(result); Response.Flush(); Response.End();
        }
        if (!IsPostBack)
        {
            //验证订阅用户邮件是否有效
            if (string.IsNullOrEmpty(AuthCode)) { function.WriteErrMsg("参数错误!"); }
            M_Mail_BookRead readMod = readBll.SelByCode(AuthCode);
            if (readMod == null) { function.WriteErrMsg("验证错误!"); }
            readMod.IsAudit = 1;//已验证
            readMod.AuthCode = function.GetRandomString(8);
            readBll.GetUpdata(readMod);
            function.WriteSuccessMsg("验证成功!","/");
        }
    }
    //发送确认订阅邮件
    public string SendSubEmail(string uname, string authcode, string email)
    {
        MailAddress adMod = new MailAddress(email);
        MailInfo mailInfo = new MailInfo() { ToAddress = adMod, IsBodyHtml = true };
        mailInfo.FromName = SiteConfig.SiteInfo.SiteName;
        mailInfo.Subject = SiteConfig.SiteInfo.SiteName + "订阅邮件";
        string mailtlp = mailBll.SelByType(B_MailManage.MailType.SubMailVerification);
        string url = SiteConfig.SiteInfo.SiteUrl + "/common/SubScriptCheck.aspx?authcode=" + authcode;
        DataTable tempDt = GetDtModel();
        DataRow tempDr = tempDt.Rows[0];
        tempDr["CDate"] = DateTime.Now.ToString();
        tempDr["SiteName"] = SiteConfig.SiteInfo.SiteName;
        tempDr["url"] = "<a href='" + url + "' target='_blank'>" + url + "</a>";
        tempDr["UserName"] = uname;
        mailtlp = new OrderCommon().TlpDeal(mailtlp, tempDt);
        mailInfo.MailBody = mailtlp;
        if (SendMail.Send(mailInfo)==SendMail.MailState.Ok)
        {
            return "1";
        }
        else
        {
            return "-2";
        }
    }
    public DataTable GetDtModel()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("CDate");
        dt.Columns.Add("SiteName");
        dt.Columns.Add("url");
        dt.Columns.Add("UserName");
        dt.Rows.Add(dt.NewRow());
        return dt;
    }
}