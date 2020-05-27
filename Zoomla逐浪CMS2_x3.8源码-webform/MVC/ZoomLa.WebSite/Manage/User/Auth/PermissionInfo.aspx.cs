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
    public partial class PermissionInfo : CustomerPageAction
    {
        B_Permission pll = new B_Permission();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (!B_ARoleAuth.Check(ZLEnum.Auth.user, "UserManage")) { function.WriteErrMsg("没有权限进行此项操作"); }
                MyBind();
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='UserManage.aspx'>用户管理</a></li><li>角色管理 <a href='Permissionadd.aspx'>[添加角色]</a></li>");
            }
        }
        private void MyBind()
        {
            DataTable dt = pll.Select_All();
            EGV.DataSource = dt;
            EGV.DataBind();
        }
        protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "del2":
                    pll.Del(Convert.ToInt32(e.CommandArgument));
                    break;
            }
            MyBind();
        }
        protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            EGV.PageIndex = e.NewPageIndex;
            MyBind();
        }
        protected void EGV_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["style"] = "cursor:pointer;";
                e.Row.Attributes["ondblclick"] = "javascript:Modify('" + EGV.DataKeys[e.Row.RowIndex].Value + "')";
                e.Row.Attributes["title"] = "双击修改";
            }
        }
        protected void BatDel_Btn_Click(object sender, EventArgs e)
        {

        }
    }
}