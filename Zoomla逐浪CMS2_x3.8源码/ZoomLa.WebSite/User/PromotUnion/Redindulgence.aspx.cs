using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Net.Mail;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Common;
using ZoomLa.Components;


public partial class PromotUnion_Redindulgence : System.Web.UI.Page
{
    B_User buser = new B_User();
    B_Redindulgence bred = new B_Redindulgence();

    protected void Page_Load(object sender, EventArgs e)
    {
        buser.CheckIsLogin(GetPath());
        if (!IsPostBack)
        {
            M_UserInfo userinfo = buser.GetLogin();
            DataTable red = bred.SelectByUse(0,userinfo.UserID);
            if (red != null && red.Rows.Count > 0)
            {
                hfRid.Value = red.Rows[0]["id"].ToString();
                lblNumChance.Text = red.Rows.Count.ToString();
                hfNum.Value = red.Rows.Count.ToString();
            }
            else
            {
                lblNumChance.Text = "0";
                hfNum.Value = "0";
            }
            hfUserId.Value = userinfo.UserID.ToString();
        }
    }

    //发送红包
    protected void btn_Click(object sender, EventArgs e)
    {
        if (CheckEmail())
        {
            if (DataConverter.CLng(hfNum.Value) <= 0 || DataConverter.CLng(hfRid.Value) <= 0)
            {
                Response.Write("<script>alert('对不起,您还没有推荐机会!');</script>");
                return;
            }
            M_Redindulgence red = bred.GetSelect(DataConverter.CLng(hfRid.Value));
            red.mail = email_name.Text.Trim();
            red.UserId = DataConverter.CLng(hfUserId.Value);
            red.isUse = 1;
            red.InvitePeople = r_name.Text.Trim();
            red.FriendName = f_name.Text.Trim();
            string siteurls = SiteConfig.SiteInfo.SiteUrl;
            if (siteurls.Substring(siteurls.Length - 1) != "/")
            {
                siteurls = siteurls + "/";
            }
            red.Url =siteurls + "User/Register.aspx?ruid=" + hfUserId.Value + "&rname=" + email_name.Text.Trim();
            bool result = bred.GetUpdate(red);
            if (result)
            {
                MailInfo mailInfo = new MailInfo();
                mailInfo.IsBodyHtml = true;
                mailInfo.FromName = SiteConfig.SiteInfo.SiteName;
                MailAddress address = new MailAddress(email_name.Text.Trim());
                mailInfo.ToAddress = address;
              
                string EmailContent = @" 您好,{$userName}！<br />您的朋友{$Name}邀请您注册tt返利,帮您省钱<br />
                    请点击下面的地址,注册通过后，您就可以获取5元现金返利！<br />
                    <a href='{$CheckUrl}' target='_blank'>{$CheckUrl}</a> ";

                mailInfo.MailBody = EmailContent.Replace("{$userName}", r_name.Text).Replace("{$Name}", f_name.Text).Replace
                    ("{$CheckUrl}", red.Url);
                mailInfo.Subject = SiteConfig.SiteInfo.SiteName + "_邀请注册";
                if (SendMail.Send(mailInfo) == SendMail.MailState.Ok)
                {
                    Response.Write("<script>alert('恭喜您,您已成功完成推荐');location.href='Redindulgence.aspx'</script>");
                }
            }
            else
            {
                Response.Write("<script>alert('推荐失败!');</script>");
            }
        }
    }

    /// <summary>
    /// 检验邮件Email是否重复
    /// </summary>
    private bool CheckEmail()
    {
        if (!SiteConfig.UserConfig.EnableMultiRegPerEmail && buser.IsExistMail(this.email_name.Text))
        {
            Response.Write("<script>alert('您邀请的邮箱已经注册返利网,请重新填写');</script>");
            return false;
        }
        return true;
    }

    private string GetPath()
    {
        string strPath = "http://" + Request.ServerVariables["HTTP_HOST"] + Request.ServerVariables["PATH_INFO"] + "?" + Request.ServerVariables["QUERY_STRING"];
        if (strPath.EndsWith("?"))
        {
            strPath = strPath.Substring(0, strPath.Length - 1);
        }
        return strPath;
    }
}
