using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Plat;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;
using ZoomLa.Model.Plat;
using System.Data;


public partial class User_RegPlat : System.Web.UI.Page
{
    B_User buser = new B_User();
    B_User_Plat upBll = new B_User_Plat();
    B_Plat_Comp compBll = new B_Plat_Comp();
    B_MailManage mailBll = new B_MailManage();
    protected void Page_Init() 
    {
        if (!IsPostBack)
        {
            s2.ValidationExpression = RegexHelper.S_Email;
            m1.ValidationExpression = RegexHelper.S_Mobile;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["code"]))
            {
                Step2(Request.QueryString["code"]);
            }
            else
            {
                Step1_Div.Visible = true;
            }
        }
    }
    public DataTable GetPlatRegDt(string url,string email)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("Url");
        dt.Columns.Add("Email");
        dt.Rows.Add(dt.NewRow());
        dt.Rows[0]["Url"] = url;
        dt.Rows[0]["Email"] = email;
        return dt;
    }
    //创建用户,并发送邮件,email:compid:时间戮
    protected void Step1_Btn_Click(object sender, EventArgs e)
    {
        int compid = 0;
        if (!string.IsNullOrEmpty(Request.QueryString["Invite"]))
        {
            string invite = Request.QueryString["Invite"];
            compid = DataConverter.CLng(EncryptHelper.AESDecrypt(invite).Split(':')[0]);
        }
        Email_T.Text = Email_T.Text.Trim();
        string code = B_Plat_Common.GetTimeStamp(Email_T.Text + ":" + compid);
        string url = SiteConfig.SiteInfo.SiteUrl.TrimEnd('/') + "/User/RegPlat.aspx?code=" + code;
        string emailTlp = mailBll.SelByType(B_MailManage.MailType.PlatReg);  //SiteConfig.UserConfig.EmailPlatReg;
        MailAddress adMod = new MailAddress(Email_T.Text);
        MailInfo mailInfo = new MailInfo() { ToAddress = adMod, IsBodyHtml = true };
        mailInfo.FromName = SiteConfig.SiteInfo.SiteName;
        mailInfo.Subject = SiteConfig.SiteInfo.SiteName + "注册邮件";
        mailInfo.MailBody = new OrderCommon().TlpDeal(emailTlp, GetPlatRegDt(url, Email_T.Text));
        if (SendMail.Send(mailInfo) == SendMail.MailState.Ok)//发送成功,生成用户,显示下一步提示
        {
            M_UserInfo mu = new M_UserInfo();
            if (buser.IsExistMail(Email_T.Text)) { function.WriteErrMsg("帐号错误请检查注册选项或联系官方!!"); }
            mu.Email = Email_T.Text;
            mu.UserName = Email_T.Text;//OK后再更改用户名
            mu.UserPwd = function.GetRandomString(6);
            mu.Question = "尚未定义问题";
            mu.Answer = function.GetRandomString(6);
            mu.Status = 0;
            mu.Remark = code;
            buser.AddModel(mu);
            MailSite_A.NavigateUrl = B_Plat_Common.GetMailSite(mu.Email);
            function.Script(this, "showme();");
        }
        else
        {
            function.WriteErrMsg("邮件发送失败,请检测邮箱地址是否正确!!");
        }
    }
    //解析参数,如正确,则显示div让其填充信息
    private void Step2(string code)
    {
        code = code.Trim();
        string decode = EncryptHelper.AESDecrypt(code);
        string email = decode.Split(':')[0];
        int compid = DataConverter.CLng(decode.Split(':')[1]);
        M_UserInfo mu = buser.GetSelectByEmail(email);
        if (mu.Remark.Equals(code))//验证通过
        {
            Step3_Div.Visible = true;
            //if (compid > 0)//被邀请注册
            //{
            //    compli.Visible = true;
            //    CompName_T.ReadOnly = true;
            //    CompName_T.Text = compBll.SelReturnModel(compid).CompName;
            //}
            //else if (compBll.SelCompByMail(email) == 0)
            //{
            //    compli.Visible = true;
            //}
            UserName_L.Text = email;
            TrueName_T.Focus();
        }
        else
        { function.WriteErrMsg("校验失败,请检测该验证码是否在有效期内!!"); }
    }
    //最终步
    protected void Sub_Btn_Click(object sender, EventArgs e)
    {
        string decode = EncryptHelper.AESDecrypt(Request.QueryString["code"]);
        int compid = DataConverter.CLng(decode.Split(':')[1]);
        M_UserInfo mu = buser.GetSelectByRemark(Request.QueryString["code"]);
        M_User_Plat upMod = new M_User_Plat();
        mu.UserPwd = StringHelper.MD5(Pwd_T.Text);
        mu.Remark = "";
        //----能力中心信息
        upMod.UserID = mu.UserID;
        upMod.TrueName = TrueName_T.Text;
        upMod.Post = Post_T.Text;
        upMod.Mobile = Mobile_T.Text;
        upMod.Status = 1;//需审核才可进行具体操作
        upMod.CompID = compid == 0 ? compBll.SelCompByMail(mu.Email) : compid;//被邀请,或通过企业邮箱分类
        upMod.UserFace = "/Images/userface/noface.png";
        if (upMod.CompID == 0)//非企业邮箱或未匹配任何企业的邮箱,则创建新公司
        {
            M_Plat_Comp compMod = new M_Plat_Comp();
            compMod.CompName = B_User.GetUserName(upMod.TrueName, upMod.UserName) + "的";
            compMod.CreateUser = mu.UserID;
            compMod.UPPath = compBll.CreateUPPath(compMod);
            if (compBll.IsEnterMail(mu.Email))
                compMod.Mails = mu.Email.Split('@')[1];
            upMod.CompID = compBll.Insert(compMod);
            //------创建公司网络管理员角色
            M_Plat_UserRole urMod = new M_Plat_UserRole();
            B_Plat_UserRole urBll = new B_Plat_UserRole();
            urMod.IsSuper = 1;
            urMod.RoleAuth = "";
            urMod.CompID = upMod.CompID;
            urMod.RoleName = "网络管理员";
            urMod.RoleDesc = "公司网络管理员,拥有全部权限,该角色只允许存在一个";
            urMod.UserID = upMod.UserID;
            upMod.Plat_Role = "," + urBll.Insert(urMod) + ",";
        }
        upBll.Insert(upMod);
        buser.UpDateUser(mu);
        buser.SetLoginState(mu, "Month");
        Response.Redirect("/Plat/");
    }
}