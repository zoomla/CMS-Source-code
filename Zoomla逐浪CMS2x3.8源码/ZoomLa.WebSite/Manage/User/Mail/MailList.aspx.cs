using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ZoomLa.Components;
using System.Net.Mail;

using System.Collections.Generic;
using ZoomLa.Common;
using ZoomLa.BLL;

using System.Text.RegularExpressions;
using ZoomLa.SQLDAL;
using ZoomLa.Model;
namespace ZoomLa.WebSite.Manage.User
{
    public partial class MailList : CustomerPageAction
    {
        B_User buser = new B_User();
        B_MailTemp tlpBll = new B_MailTemp();
        B_MailHtml bm = new B_MailHtml();
        B_CreateShopHtml bc = new B_CreateShopHtml();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!B_ARoleAuth.Check(ZoomLa.Model.ZLEnum.Auth.user, "EmailManage"))
            {
                function.WriteErrMsg("没有权限进行此项操作");
            }
            if (!IsPostBack)
            {
                TxtSenderName.Text = SiteConfig.SiteInfo.Webmaster;
                TxtSenderEmail.Text = SiteConfig.SiteInfo.WebmasterEmail;
                BindTlpDP();
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='" + customPath2 + "User/UserManage.aspx'>用户管理</a><li><a href='SendMailList.aspx'>发送邮件列表</a></li><li class='active'>邮件发送</li>" + Call.GetHelp(110));
            }
        }
        protected void BtnSend_Click(object sender, EventArgs e)
        {
            DataTable dt = buser.Sel();
            MailInfo model = new MailInfo();
            model.Subject = Subject_T.Text;
            foreach (DataRow dr in dt.Rows)
            {
                model.ToAddress = new MailAddress(dr["Email"].ToString());
                SendMail.Send(model);
            }
            function.WriteSuccessMsg("发送完成");
        }
        protected void BindTlpDP()
        {
            DataTable dt = tlpBll.Sel();
            if (dt.Rows.Count > 0)
            {
                this.TxtContent.Text = dt.Rows[0]["Content"].ToString();
                MailTemp_DP.DataSource = dt;
                MailTemp_DP.DataTextField = "TempName";
                MailTemp_DP.DataValueField = "ID";
                MailTemp_DP.DataBind();
            }
            else
            {
                MailTemp_DP.Items.Insert(0, "");
                MailTemp_DP.Items[0].Value = "0";
                MailTemp_DP.Items[0].Text = "暂无邮件模板";
            }
        }
        // 转换标签
        public string GetContent(string Result)
        {
            string mat = "";
            bool flag = false;
            #region 替换系统标签
            string pattern = @"{\$([\s\S])*?\/}";
            string tit = this.Subject_T.Text;
            do
            {
                flag = false;
                MatchCollection matchs = Regex.Matches(Result, pattern, RegexOptions.IgnoreCase);
                if (matchs.Count > 0)
                {
                    foreach (Match match in matchs)
                    {
                        Result = bc.CreateShopHtml(Result);
                        mat = match.Value;
                        Result = Result.Replace(mat, bm.SysLabelProc(mat, tit));
                        flag = true;
                    }
                }
            }
            while (flag);
            #endregion
            Result = bc.CreateShopHtml(Result);
            return Result;
        }
        protected void MailTemp_DP_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(MailTemp_DP.SelectedValue);
            if (id > 0)
            {
                M_MailTemp mailMod = tlpBll.SelReturnModel(id);
                TxtContent.Text = mailMod.Content;
            }
        }
    }
}