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

namespace ZoomLaCMS.Manage.User
{
    public partial class AddRole : CustomerPageAction
    {
        M_RoleInfo role = new M_RoleInfo();
        B_Role roleBll = new B_Role();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (function.isAjax())
            {
                string action = Request.Form["action"];
                string result = "";
                switch (action)
                {
                    case "checkname":
                        if (B_Role.IsExit(Request.Form["name"]))
                            result = "-1";
                        else
                            result = "1";
                        break;
                    default:
                        break;
                }
                Response.Write(result); Response.Flush(); Response.End();
            }
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='UserManage.aspx'>用户管理</a></li><li> <a href='AdminManage.aspx'>管理员管理</a></li><li><a href=\"RoleManage.aspx\">角色管理</a></li><li><asp:Literal Text='添加角色'>" + Literal1_Hid.Value + "</asp:Literal></li>");
            B_Admin badmin = new B_Admin();
            if (!B_ARoleAuth.Check(ZoomLa.Model.ZLEnum.Auth.user, "RoleEdit"))
            {
                function.WriteErrMsg("没有权限进行此项操作");
            }
            if (!Page.IsPostBack)
            {
                ViewState["RoleID"] = Request.QueryString["RoleID"];

                //判断当前角色是否存在
                if (B_Role.IsExit(DataConverter.CLng(ViewState["RoleID"])))
                {
                    this.LblTitle.Text = "修改角色";
                    this.Literal1_Hid.Value = "修改角色";
                    role = B_Role.GetRoleById(DataConverter.CLng(ViewState["RoleID"]));
                    this.txbRoleName.Text = role.RoleName;
                    this.tbRoleInfo.Text = role.Description;
                    EditRoleName_Hid.Value = role.RoleName;
                }
                else
                {
                    this.LblTitle.Text = "添加角色";
                    this.Literal1_Hid.Value = "添加角色";
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
            B_Admin badmin = new B_Admin();
            if (Page.IsValid)
            {
                switch (this.LblTitle.Text)
                {
                    case ("添加角色"):
                        role.RoleName = this.txbRoleName.Text;
                        role.Description = this.tbRoleInfo.Text;
                        role.NodeID = 0;
                        if (B_Role.Add(role))
                        {
                            function.WriteSuccessMsg("添加成功!", "RoleManage.aspx");
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
                        if (roleBll.Update(role))
                        {
                            function.WriteSuccessMsg("修改成功!", "RoleManage.aspx");
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
                this.txbRoleName.Text = "sdfsdf";
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