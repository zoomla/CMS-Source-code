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

namespace User
{
    public partial class RoleManage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                B_Admin badmin = new B_Admin();
                badmin.CheckMulitLogin();
                bind();
            }
        }
        private void bind()
        {
            this.GridView1.DataSource = B_Role.GetRoleInfo();
            this.GridView1.DataKeyNames = new string[] { "RoleID" };
            this.GridView1.DataBind();
        }
        //绑定分页
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            bind();
        }
        //取消
        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            bind();
        }

        protected void LnkModify_Click(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ModifyRole")
                Page.Response.Redirect("AddRole.aspx?RoleID=" + e.CommandArgument.ToString());
            if (e.CommandName == "Del")
            {
                B_Role.DelRoleByID(DataConverter.CLng(e.CommandArgument.ToString()));
                bind();
            }
            if (e.CommandName == "ModifyPower")
            {
                Page.Response.Redirect("RoleManager.aspx?id=" + e.CommandArgument.ToString());
            }
        }
    }
}