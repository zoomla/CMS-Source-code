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
using ZoomLa.Model;
using ZoomLa.Web;
using ZoomLa.BLL;
using ZoomLa.Common;

namespace ZoomLa.WebSite.Manage.User
{
    public partial class AddRole : System.Web.UI.Page
    {
        M_RoleInfo role = new M_RoleInfo();
        protected void Page_Load(object sender, EventArgs e)
        {
            B_Admin badmin = new B_Admin();
            badmin.CheckMulitLogin();
            if (!badmin.ChkPermissions("RoleEdit"))
            {
                function.WriteErrMsg("没有权限进行此项操作");
            }
            if (!Page.IsPostBack)
            {
                ViewState["RoleID"] = Request.QueryString["RoleID"];
                //判断当前管理员是否存在
                if (B_Admin.IsExist(DataConverter.CLng(ViewState["RoleID"])))
                {
                    this.LblTitle.Text = "修改角色";
                    this.Literal1.Text = "修改角色";
                    role = B_Role.GetRoleById(DataConverter.CLng(ViewState["RoleID"]));
                    this.txbRoleName.Text = role.RoleName;
                    this.tbRoleInfo.Text = role.Description;
                }
                else
                {
                    this.LblTitle.Text = "添加角色";
                    this.Literal1.Text = "添加角色";
                }
            }

        }
        /// <summary>
        /// 增加角色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region

        protected void Button2_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                switch (this.LblTitle.Text)
                {
                    case ("添加角色"):
                        role.RoleName = this.txbRoleName.Text;
                        role.Description = this.tbRoleInfo.Text;
                        if (B_Role.Add(role))
                        {
                            Response.Redirect("RoleManage.aspx");
                        }
                        else
                        {
                            function.WriteErrMsg("添加失败！");
                        }
                        break;
                    case ("修改角色"):
                        role.RoleID = DataConverter.CLng(ViewState["RoleID"]);
                        role.RoleName = this.txbRoleName.Text;
                        role.Description = this.tbRoleInfo.Text;
                        if (B_Role.Update(role))
                        {
                            Response.Redirect("RoleManage.aspx");
                        }
                        else
                        {
                            function.WriteErrMsg("修改失败！");
                        }
                        break;
                }

            }
        }
        #endregion

        /// <summary>
        /// 验证角色名称是否已经存在
        /// </summary>
        /// <param name="source"></param>
        /// <param name="args"></param>
        #region

        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            string roleName = this.txbRoleName.Text;
            if (B_Role.IsExit(roleName))
            {
                this.cvRole.Visible = true;
                args.IsValid = false;
                this.txbRoleName.Text = "";
            }
            else
            {
                args.IsValid = true;
            }
        }
        #endregion


        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("RoleManage.aspx");
        }
    }
}
