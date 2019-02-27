using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;

namespace ZoomLaCMS.Plat.Common
{
    public partial class Apply : System.Web.UI.Page
    {
        B_User_Plat upBll = new B_User_Plat();
        B_Common_UserApply alyBll = new B_Common_UserApply();
        B_User buser = new B_User();
        protected void Page_Load(object sender, EventArgs e)
        {
            B_User.CheckIsLogged(Request.RawUrl);
            if (!IsPostBack)
            {
                M_UserInfo mu = buser.GetLogin();
                M_Uinfo basemu = buser.GetUserBaseByuserid(mu.UserID);
                if (basemu != null)
                {
                    Mobile_T.Text = basemu.Mobile;
                    Email_T.Text = mu.Email;
                    CompName_T.Text = mu.CompanyName;
                    QQ_T.Text = basemu.QQ;
                }
                M_User_Plat upMod = upBll.SelReturnModel(mu.UserID);
                if (upMod != null && upMod.Status != -1)
                {
                    ShowRemind("你已开通能力中心,不需要再申请");
                }
                else if (alyBll.IsExist("plat_applyopen", mu.UserID))
                {
                    ShowRemind("你提交的审请正在审核中...");
                }
            }
        }
        protected void Save_Btn_Click(object sender, EventArgs e)
        {
            M_UserInfo mu = buser.GetLogin();
            M_Common_UserApply alyMod = new M_Common_UserApply("plat_applyopen");
            alyMod.UserID = mu.UserID;
            alyMod.UserName = mu.UserName;
            alyMod.Remind = UserRemind_T.Text;
            alyMod.CompName = CompName_T.Text;
            alyMod.Info1 = CompShort_T.Text;
            alyMod.Contact = Contact_T.Text;
            alyMod.Email = Email_T.Text;
            alyMod.Mobile = Mobile_T.Text;
            alyMod.QQ = QQ_T.Text;
            alyBll.Insert(alyMod);
            function.WriteSuccessMsg("您已经成功申请，系统审核后会以电子邮件通知您");
        }
        private void ShowRemind(string msg)
        {
            remind_div.InnerText = msg;
            Form_Div.Visible = false;
            Tip_Div.Visible = true;
        }
    }
}