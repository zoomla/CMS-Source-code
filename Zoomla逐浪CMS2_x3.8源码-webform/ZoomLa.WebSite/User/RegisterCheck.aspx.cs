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
using ZoomLa.Common;
using ZoomLa.BLL;
using ZoomLa.Components;

namespace ZoomLa.WebSite.User
{
    public partial class RegisterCheck : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.TxtUserName.Text = Request.QueryString["UserName"];
                this.TxtCheckNum.Text = Request.QueryString["CheckNum"];
            }
        }

        protected void BtnRegCheck_Click(object sender, EventArgs e)
        {
            B_User bll = new B_User();
            M_UserInfo usersByUserName = bll.GetUserByName(this.TxtUserName.Text);
            if (!usersByUserName.IsNull)
            {
                if (StringHelper.ValidateMD5(usersByUserName.UserPwd, StringHelper.MD5(this.TxtPassword.Text)))
                {
                    if (usersByUserName.Status  != 0)
                    {
                        if (usersByUserName.CheckNum == this.TxtCheckNum.Text)
                        {
                            usersByUserName.Status = usersByUserName.Status==3 ? 2 : 0; 
                            bll.UpDateUser(usersByUserName);
                            bll.GetLogin(false);
                            if (usersByUserName.Status == 0)
                            {
                                function.WriteSuccessMsg("恭喜你正式成为本站的一员，请返回会员中心登录。", "/User/default.aspx");
                            }
                            else
                            {
                                function.WriteSuccessMsg("恭喜你通过了Email验证。请等待管理开通你的帐号。开通后，你就正式正为本站的一员了。", "/User/Login.aspx");
                            }
                        }
                        else
                        {
                            function.WriteErrMsg("验证码不正确", "/User/Login.aspx");
                        }
                    }
                    else
                    {
                        function.WriteErrMsg("你已通过验证，此链接已无效，请返回会员中心登录。", "/User/Login.aspx");
                    }
                }
                else
                {
                    function.WriteErrMsg("密码不正确", "/User/Login.aspx");
                }
            }
            else
            {
                function.WriteErrMsg("不存在此用户", "/User/Login.aspx");
            }
        }
}
}