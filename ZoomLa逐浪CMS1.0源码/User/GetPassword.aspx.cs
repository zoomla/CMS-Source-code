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
using ZoomLa.Model;
using ZoomLa.BLL;

using ZoomLa.Common;
using ZoomLa.Components;
namespace ZoomLa.WebSite.User
{
public partial class User_GetPassword : System.Web.UI.Page
{
    private B_User buser = new B_User();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Page.IsPostBack)
        {
            this.PnlStep1.Visible = true;
        }
    }
    protected void BtnStep1_Click(object sender, EventArgs e)
        {
            this.PnlStep1.Visible = false;
            M_UserInfo users = buser.GetUserByName(this.TxtUserName.Text);
            if (!users.IsNull)
            {
                this.LitQuestion.Text = users.Question;
            }
            this.PnlStep2.Visible = true;
            if (SiteConfig.UserConfig.UserGetPasswordType == 0)
            {
                this.BtnStep2.Text = "下一步";
            }
        }

        protected void BtnStep2_Click(object sender, EventArgs e)
        {
            this.PnlStep1.Visible = false;
            this.PnlStep2.Visible = false;
            string vCode = this.Session["ValidateCode"].ToString();
            if (string.IsNullOrEmpty(vCode))
            {
                function.WriteErrMsg("<li>验证码无效，请刷新验证码重新登录</li>", "GetPassword.aspx");
            }
            if (string.Compare(this.TxtValidateCode.Text.Trim(), vCode, true) != 0)
            {
                function.WriteErrMsg("<li>验证码不正确</li>", "GetPassword.aspx");
            }
            M_UserInfo users = buser.GetUserByName(this.TxtUserName.Text);
            if (!users.IsNull)
            {
                string str = StringHelper.MD5(this.TxtAnswer.Text);
                if (StringHelper.ValidateMD5(users.Answer, str))
                {
                    this.PnlStep3.Visible = true;
                }
            }
            else
            {
                function.WriteErrMsg("<li>密码提示答案不正确</li>", "GetPassword.aspx");
            }
            
        }
        protected void BtnStep3_Click(object sender, EventArgs e)
        {
            M_UserInfo usersByUserName = buser.GetUserByName(this.TxtUserName.Text);
            usersByUserName.UserPwd = StringHelper.MD5(this.TxtPassword.Text);
            buser.UpDateUser(usersByUserName);
        }
    }
}