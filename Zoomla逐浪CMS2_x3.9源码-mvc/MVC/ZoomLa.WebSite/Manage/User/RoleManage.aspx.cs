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
using ZoomLa.Web;
using ZoomLa.Model;
using ZoomLa.BLL;
using ZoomLa.Common;
using System.Data.SqlClient;

namespace ZoomLaCMS.Manage.User
{
    public partial class RoleManage :CustomerPageAction
    {
        B_Admin badmin = new B_Admin();
        protected void Page_Load(object sender, EventArgs e)
        {
            ZoomLa.Common.function.AccessRulo();
            B_Admin badmin = new B_Admin();
            badmin.CheckIsLogin();
            if (!Page.IsPostBack)
            {
                if (!B_ARoleAuth.Check(ZLEnum.Auth.user, "AdminManage"))
                {
                    function.WriteErrMsg("没有权限进行此项操作");
                }
                myBind();
            }
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='UserManage.aspx'>用户管理</a></li><li> <a href='AdminManage.aspx'>管理员管理</a></li><li>角色管理<a href='AddRole.aspx'>[添加角色]</a></li>" + Call.GetHelp(104));
        }
        private void myBind()
        {
            DataView dt = B_Role.GetRoleInfo();
            this.EGV.DataSource = dt;//.FindRows(" AddUser=" + badmin.GetAdminLogin().AdminId);
            this.EGV.DataKeyNames = new string[] { "RoleID" };
            this.EGV.DataBind();

        }
        //绑定分页
        protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            EGV.PageIndex = e.NewPageIndex;
            myBind();
        }
        //取消
        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            EGV.EditIndex = -1;
            myBind();
        }
        protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ModifyRole")
                Page.Response.Redirect("AddRole.aspx?RoleID=" + e.CommandArgument.ToString());
            if (e.CommandName == "Del")
            {
                B_Role.DelRoleByID(DataConverter.CLng(e.CommandArgument.ToString()));
                myBind();
            }
            if (e.CommandName == "ModifyPower")
            {
                Page.Response.Redirect("RoleAuthList.aspx?id=" + e.CommandArgument.ToString());
            }
            if (e.CommandName == "ListRolelist")
            {
                Page.Response.Redirect("AdminManage.aspx?id=" + e.CommandArgument.ToString());
            }
        }
        // 鼠标滑动变色
        protected void GridView1_RowDataBound(Object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView dr = (DataRowView)e.Row.DataItem;
                if (dr["RoleID"].ToString().Equals("1"))
                {
                    (e.Row.FindControl("LnkDel") as LinkButton).Visible = false;
                    (e.Row.FindControl("AuthEdit") as LinkButton).Visible = false;
                }
            }
        }
        // 鼠标双击查看
        protected override void Render(HtmlTextWriter writer)
        {
            foreach (GridViewRow row in this.EGV.Rows)
            {
                if (row.RowState == DataControlRowState.Edit)
                {
                    row.Attributes.Remove("ondblclick");
                    row.Attributes.Remove("style");
                    row.Attributes["title"] = "编辑行";
                    continue;
                }
                if (row.RowType == DataControlRowType.DataRow)
                {

                    row.Attributes["ondblclick"] = String.Format("javascript:location.href='AddRole.aspx?RoleID={0}'", this.EGV.DataKeys[row.RowIndex].Value.ToString());
                    row.Attributes["style"] = "cursor:pointer";
                    row.Attributes["title"] = "双击修改角色信息";
                }
            }
            base.Render(writer);
        }
    }
}