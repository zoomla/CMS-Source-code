namespace ZoomLa.WebSite.Manage.User
{
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
    using ZoomLa.Common;
    using ZoomLa.Model;
    using ZoomLa.BLL;

    public partial class UserPassModify : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                string UserID = base.Request.QueryString["UserID"];
                if (string.IsNullOrEmpty(UserID))
                    function.WriteErrMsg("请指定会员ID","../User/UserManage.aspx");
                this.HdnUserID.Value = UserID;
            }
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            if (this.IsValid)
            {
                B_User bll = new B_User();
                int UserID = DataConverter.CLng(this.HdnUserID.Value);
                M_UserInfo info = bll.SeachByID(UserID);                
                info.UserPwd = StringHelper.MD5(this.TxtPassword.Text.Trim());
                bll.UpDateUser(info);
                function.WriteSuccessMsg("修改密码成功！", "../User/UserManage.aspx");
            }
        }
    }
}