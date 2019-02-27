using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.User;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;
using ZoomLa.Model.User;

namespace ZoomLaCMS.Manage.User.Promo
{
    public partial class InviteCodeAdd : System.Web.UI.Page
    {
        B_User buser = new B_User();
        B_User_InviteCode inviteBll = new B_User_InviteCode();
        B_Group gpBll = new B_Group();
        private int Mid { get { return DataConverter.CLng(Request.QueryString); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='" + CustomerPageAction.customPath2 + "user/UserManage.aspx'>用户管理</a></li><li><a href='InviteCodeList.aspx'>邀请码列表</a></li><li>添加邀请码</li>");
                Format_T.Text = SiteConfig.UserConfig.InviteFormat;
                Group_Rad.DataSource = gpBll.Sel();
                Group_Rad.DataBind();
                Group_Rad.SelectedValue = SiteConfig.UserConfig.InviteJoinGroup.ToString();
            }
        }

        protected void Create_Btn_Click(object sender, EventArgs e)
        {
            M_AdminInfo adminMod = B_Admin.GetLogin();
            M_UserInfo mu = buser.SelReturnModel(DataConverter.CLng(User_Hid.Value));
            int count = DataConverter.CLng(Count_T.Text);
            M_User_InviteCode inviteMod = new M_User_InviteCode();
            inviteMod.CAdmin = adminMod.AdminId;
            inviteMod.CUser = 0;
            inviteMod.UserID = mu.UserID;
            inviteMod.UserName = mu.UserName;
            inviteMod.JoinGroup = DataConverter.CLng(Group_Rad.SelectedValue);
            inviteBll.Code_Create(count, SiteConfig.UserConfig.InviteFormat, inviteMod);
            function.WriteSuccessMsg("邀请码创建成功","InviteCodeList.aspx");
        }
    }
}