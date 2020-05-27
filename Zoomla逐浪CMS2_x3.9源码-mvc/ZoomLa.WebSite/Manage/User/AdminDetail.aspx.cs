using System;
using System.Text;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using ZoomLa.Components;
using ZoomLa.SQLDAL;
using ZoomLa.Model;
using ZoomLa.Web;
using System.Data.SqlClient;
using ZoomLa.BLL;
using ZoomLa.Common;


namespace ZoomLaCMS.Manage.User
{
    public partial class AdminDetail : CustomerPageAction
    {

        #region
        B_Admin badmin = new B_Admin();
        B_User buser = new B_User();
        B_Role b_role = new B_Role();
        public int Mid { get { return DataConvert.CLng(Request.QueryString["ID"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!B_ARoleAuth.Check(ZLEnum.Auth.user, "AdminEdit"))
                {
                    function.WriteErrMsg("没有权限进行此项操作");
                }
                M_AdminInfo adminMod = B_Admin.GetAdminByID(Mid);
                if (adminMod == null) { function.WriteErrMsg("指定的管理员不存在"); }
                tbdName.Text = adminMod.AdminName;
                txtAdminTrueName.Text = adminMod.AdminTrueName;
                tbdName.Enabled = false;
                DefaultStart1.Text = B_Content.GetStatusStr(adminMod.DefaultStart);
                string[] arr = adminMod.RoleList.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                string roleName = "";
                for (int i = 0; i < arr.Length; i++)
                {
                    int roleId = DataConverter.CLng(arr[i]);
                    M_RoleInfo m_role = B_Role.GetRoleById(roleId);
                    roleName += "  " + m_role.RoleName;
                }
                cblRoleList1.Text = roleName;
                //单选框设置
                if (adminMod.EnableMultiLogin)
                {
                    cb1.Text = "允许多人同时使用此帐号登录";
                    cb1.Visible = true;
                }
                else
                {
                    cb1.Visible = false;
                }
                if (adminMod.EnableModifyPassword)
                {
                    this.cb2.Text = "允许管理员修改密码";
                    cb2.Visible = true;
                }
                else
                {
                    this.cb2.Visible = false;
                }
                if (adminMod.IsLock)
                {
                    this.cb3.Text = "是否锁定";
                    cb3.Visible = true;
                }
                else
                {
                    this.cb3.Visible = false;
                }
                if (adminMod.PubRole == 1)
                {
                    this.CheckBox1.Text = "是";
                }
                else
                {
                    this.CheckBox1.Text = "否";
                }
                int roId = adminMod.NodeRole;
                if (roId == 0)
                {
                    DropDownList11.Text = "管理所有节点";
                }
                else
                {
                    M_RoleInfo role = B_Role.GetRoleById(roId);
                    DropDownList11.Text = role.RoleName;
                }
                Call.SetBreadCrumb(Master, "<li>后台管理</li><li><a href='AdminManage.aspx'>管理员管理</a></li><li>管理员预览</li>");
            }
        }
        #endregion
        // 验证管理员是否已存在
        protected bool Manager_Validator()
        {
            string adminName = this.tbdName.Text;
            bool re = false;
            if (B_Admin.IsExist(adminName))
            {
                re = false;
            }
            else
            {
                re = true;
            }
            return re;
        }

    }
}