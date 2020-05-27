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
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Common;
using ZoomLa.Components;
namespace ZoomLa.WebSite.User
{
    public partial class ChangAnswer : System.Web.UI.Page
    {
        private B_User buser = new B_User();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                buser.CheckIsLogin();
                M_UserInfo info = buser.GetLogin();
                if (info.IsNull)
                {
                    Response.Redirect("Login.aspx");
                }
            }
        }
        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            M_UserInfo info = buser.GetLogin();
            info.Question = this.TxtQuestion.Text.Trim();
            if (!string.IsNullOrEmpty(this.TxtNewAnswer.Text.Trim()))
                info.Answer = this.TxtNewAnswer.Text.Trim();
            buser.UpDateUser(info);
            function.WriteSuccessMsg("修改密码提示成功!", "/user/Info/UserInfo.aspx");
        }
        protected void BtnCancle_Click(object sender, EventArgs e)
        {
            this.TxtQuestion.Text = "";
            this.TxtNewAnswer.Text = "";
            this.TxtNewAnswer2.Text = "";
        }
    }
}