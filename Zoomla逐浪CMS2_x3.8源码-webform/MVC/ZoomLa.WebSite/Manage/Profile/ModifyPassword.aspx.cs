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
using ZoomLa.Web;
using System.Text;
using ZoomLa.Common;

namespace ZoomLaCMS.Manage.Profile
{
    public partial class ModifyPassword : CustomerPageAction
    {
        B_Admin badmin = new B_Admin();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>" + Resources.L.工作台 + "</a></li><li><a href='../User/UserManage.aspx'>" + Resources.L.用户管理 + "</a></li><li>" + Resources.L.修改密码 + "</li>");
                M_AdminInfo info = B_Admin.GetLogin();
                AdminName_L.Text = info.AdminName;
                if (!info.EnableModifyPassword)
                {
                    function.WriteErrMsg("您的帐户不允许修改密码！");
                }
            }
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            M_AdminInfo info = badmin.GetAdminLogin();
            if (info.AdminPassword != StringHelper.MD5(this.TxtOldPassword.Text.Trim()))
            {
                function.WriteErrMsg("输入的原密码与登录密码不相同！");
            }
            info.AdminPassword = StringHelper.MD5(this.TxtPassword.Text.Trim());
            info.LastModifyPasswordTime = DateTime.Now;
            B_Admin.Update(info);
            if (info.AdminPassword != StringHelper.MD5(this.TxtOldPassword.Text.Trim()))
            {
                function.WriteSuccessMsg("密码修改成功,系统现在自动注销，请使用新密码登录管理!", CustomerPageAction.customPath2 + "/SignOut.aspx");
            }
            else
            {
                Response.Write("<script type=\"text/javascript\">location.href='../User/AdminDetail.aspx?id=" + info.AdminId + "&roleid=" + info.NodeRole + "';</script>");
            }
        }
        protected void BtnCancle_Click(object sender, EventArgs e)
        {
            this.TxtOldPassword.Text = "";
            this.TxtPassword.Text = "";
            this.TxtPassword2.Text = "";
        }
    }
}