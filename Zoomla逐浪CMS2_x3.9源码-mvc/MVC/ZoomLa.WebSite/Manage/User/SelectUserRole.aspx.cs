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

namespace ZoomLaCMS.Manage.User
{
    public partial class SelectUserRole : System.Web.UI.Page
    {
        protected B_User buser = new B_User();
        protected B_Group bgp = new B_Group();
        protected void Page_Load(object sender, EventArgs e)
        {
            B_Admin.CheckIsLogged();
            if (!IsPostBack)
            {
                DataBind();
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='PermissionInfo.aspx'>用户角色</a></li><li>选择会员</li>");
            }
        }
        public new void DataBind()
        {
            DataTable dt = buser.Sel();
            EGV.DataSource = dt;
            EGV.DataBind();
        }
        protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            EGV.PageIndex = e.NewPageIndex;
            DataBind();
        }
        protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
        {
        }
        public string GetGroupName(string GroupID)
        {
            return bgp.GetByID(DataConverter.CLng(GroupID)).GroupName;
        }
        public string GetStatus(string Status)
        {
            switch (Status)
            {
                case "0":
                    return "正常";
                case "1":
                    return "锁定";
                case "2":
                    return "待认证";
                case "3":
                    return "双认证";
                case "4":
                    return "邮件认证";
                case "5":
                    return "待认证";
            }
            return "正常";
        }
        protected void SaveBtn_Click(object sender, EventArgs e)
        {
            string role = Request.QueryString["id"];
            for (int i = 0; i <= EGV.Rows.Count - 1; i++)
            {
                HtmlControl cbox = (HtmlControl)EGV.Rows[i].FindControl("Btchk");
                HtmlInputCheckBox cbx = cbox as HtmlInputCheckBox;
                if (cbx.Checked == true)
                {
                    M_UserInfo dd = buser.GetUserByUserID(DataConverter.CLng(EGV.DataKeys[i].Value));
                    if (dd.UserRole.IndexOf("," + role + ",") == -1)
                    {
                        dd.UserRole += role;
                        buser.UpDateUser(dd);
                    }
                }
                else
                {
                    M_UserInfo dd1 = buser.GetUserByUserID(DataConverter.CLng(EGV.DataKeys[i].Value));
                    if (dd1.UserRole.IndexOf("," + role + ",") > -1)
                    {
                        dd1.UserRole = dd1.UserRole.Replace("," + role + ",", "");
                        buser.UpDateUser(dd1);
                    }
                }
            }
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('修改成功');window.location.href = 'PermissionInfo.aspx';", true);

        }
        //获取所有属于当前的用户ID
        public string GetUserRole()
        {
            DataTable dt = buser.Sel();
            string userIDs = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["UserRole"].ToString().IndexOf("," + Request.QueryString["id"] + ",") > -1)
                {
                    userIDs += dt.Rows[i]["UserID"] + ",";
                }
            }
            userIDs = userIDs.Trim(',');
            return userIDs;
        }
    }
}