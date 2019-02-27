using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;

public partial class User_MobileReg : System.Web.UI.Page
{
    B_User buser = new B_User();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //function.WriteErrMsg("手机注册功能默认关闭");
            step1_div.Visible = true;
        }
    }
    protected void Sure_Btn_Click(object sender, EventArgs e)
    {
        M_UserInfo mu = new M_UserInfo();
        M_Uinfo basemu = new M_Uinfo();
        mu.UserName = UName_T.Text.Trim();
        mu.UserPwd = StringHelper.MD5(Passwd_T.Text.Replace(" ", ""));
        basemu.Mobile = Session["Mobile_Number"].ToString();
        if (string.IsNullOrEmpty(Email_T.Text)) { mu.Email = mu.UserName + function.GetRandomString(3, 2) + "@random.com"; }
        else
        {
            mu.Email = Email_T.Text;
            if (buser.IsExistMail(mu.Email)) { function.WriteErrMsg("[" + mu.Email + "]已存在,取消注册"); }
        }
        if (buser.IsExistUName(mu.UserName)) { function.WriteErrMsg("[" + mu.UserName + "]已存在,取消注册"); }
        if (buser.IsExist("ume", basemu.Mobile)) { function.WriteErrMsg("[" + basemu.Mobile + "]已存在,取消注册"); }
        mu.UserID = buser.Add(mu);
        basemu.UserId = mu.UserID;
        buser.AddBase(basemu);
        buser.SetLoginState(mu);
        function.WriteSuccessMsg("注册成功", "/User/Default.aspx");
    }
    protected void Next_Btn_Click(object sender, EventArgs e)
    {
        string code = Session["Mobile_SafeCode"] == null ? "" : Session["Mobile_SafeCode"].ToString();
        if (string.IsNullOrEmpty(code)) { function.WriteErrMsg("验证码不存在"); }
        if (!code.Equals(VCode_T.Text)) { function.WriteErrMsg("验证码不正确"); }
        Session["Mobile_SafeCode"] = null;
        Mobile_L.Text = Session["Mobile_Number"].ToString();
        step1_div.Visible = false;
        step2_div.Visible = true;
    }
}