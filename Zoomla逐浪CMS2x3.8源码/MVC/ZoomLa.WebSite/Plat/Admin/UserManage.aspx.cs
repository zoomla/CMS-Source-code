using System;
using System.Collections.Generic;
using System.Data;
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


/*
 * Admin下页面只有用户管理员可访问
 * 创建者默认有管理员权限,但可移除
 */

namespace ZoomLaCMS.Plat.Admin
{
    public partial class UserManage : System.Web.UI.Page
    {
        B_User_Plat upBll = new B_User_Plat();
        B_User buser = new B_User();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!B_User_Plat.IsAdmin())
            {
                function.WriteErrMsg("非管理员,无权限访问该页!!");
            }
            if (!IsPostBack)
            {
                MyBind();
                ULink_L.Text = GetULink();
            }
        }
        public void MyBind(string search = "")
        {
            int compid = B_User_Plat.GetLogin().CompID;
            DataTable dt = upBll.SelByCompany(compid);
            EGV.DataSource = dt;
            string wherestr = "1=1";
            if (!string.IsNullOrEmpty(search.Trim()))
                wherestr += " AND UserName LIKE '%" + search + "%'";
            dt.DefaultView.RowFilter = wherestr;
            EGV.DataBind();
        }
        protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            EGV.PageIndex = e.NewPageIndex;
            MyBind();
        }
        protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName.ToLower())
            {
                case "del2":
                    //删除记录，同时删除目标数据库
                    break;
            }
        }
        //正常, 未审核,离职
        public string GetStatus()
        {
            M_UserInfo mu = buser.SelReturnModel(DataConverter.CLng(Eval("UserID")));
            int status = mu.Status;
            string result = "";
            switch (status)
            {
                case 0:
                    result = "正常";
                    break;
                case 1:
                    result = "禁用";
                    break;
                default:
                    result = status + "状态码未知";
                    break;
            }
            return result;
        }
        //批量发送邮件
        protected void BatEmail_Btn_Click(object sender, EventArgs e)
        {
            BatEmail_T.Text = BatEmail_T.Text.Replace(" ", "");
            if (!string.IsNullOrEmpty(BatEmail_T.Text))
            {
                string result = "", url = GetULink();
                foreach (string email in BatEmail_T.Text.Split(','))
                {
                    if (string.IsNullOrEmpty(email)) continue;
                    result += SendEMail(email, url) + "<br />";
                }
                SendResult_Lit.Text = result;
                result_div.Visible = true;
            }
        }
        private string SendEMail(string email, string url)
        {
            string result = "";
            string emailTlp = SiteConfig.UserConfig.EmailPlatReg;
            MailInfo mailInfo = new MailInfo();
            MailAddress adMod = new MailAddress(email);
            mailInfo.ToAddress = adMod;
            mailInfo.IsBodyHtml = true;
            mailInfo.FromName = SiteConfig.SiteInfo.SiteName;
            mailInfo.Subject = SiteConfig.SiteInfo.SiteName + "注册邮件";
            mailInfo.MailBody = emailTlp.Replace("{Url}", url).Replace("{Email}", email);
            if (buser.IsExistMail(email)) { result = email + "发送失败,原因：该帐号已存在!"; }
            else if (SendMail.Send(mailInfo) == SendMail.MailState.Ok)//发送成功,生成用户
            {
                M_UserInfo mu = new M_UserInfo();
                mu.Email = email;
                mu.UserName = email;
                mu.UserPwd = function.GetRandomString(6);
                mu.Question = "尚未定义问题";
                mu.Answer = function.GetRandomString(6);
                mu.RegTime = DateTime.Now;
                mu.Status = 0;
                mu.Remark = B_Plat_Common.GetTimeStamp(email);
                buser.AddModel(mu);
                result = email + "发送成功";
            }
            else
            {
                result = email + "发送失败,原因：Email发送失败!";
            }
            return result;
        }
        //产生邀请链接
        public string GetULink()
        {
            M_User_Plat upMod = B_User_Plat.GetLogin();
            string code = B_Plat_Common.GetTimeStamp(upMod.CompID.ToString());
            string ulink = SiteConfig.SiteInfo.SiteUrl + "/User/RegPlat?Invite=" + code;
            return ulink;
        }
        //激活
        protected void Audit_Btn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.Form["idChk"]))
            {
                buser.BatAudit(Request.Form["idChk"], 0);
                function.WriteSuccessMsg("激活选定用户成功!");
            }
            else
            {
                function.Script(this, "alert('未选定用户');");
            }
        }
        //禁用
        protected void UnAudit_Btn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.Form["idchk"]))
            {
                buser.BatAudit(Request.Form["idchk"], 1);
                function.WriteSuccessMsg("禁止选定用户成功!");
            }
            else
            {
                function.Script(this, "alert('未选定用户');");
            }
        }
        //移除
        protected void BatDel_Btn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.Form["idchk"]))
            {
                upBll.DelByIDS(Request.Form["idchk"]);
                MyBind();
            }
        }
        protected void Search_Btn_Click(object sender, EventArgs e)
        {
            MyBind(Search_T.Text);
        }
    }
}