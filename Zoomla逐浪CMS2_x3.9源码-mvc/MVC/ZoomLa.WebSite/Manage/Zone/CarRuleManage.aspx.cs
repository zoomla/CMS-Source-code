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
using ZoomLa.Sns;
using ZoomLa.BLL;
using ZoomLa.Common;

namespace ZoomLaCMS.Manage.Zone
{
    public partial class CarRuleManage : CustomerPageAction
    {
        Parking_BLL pl = new Parking_BLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            ZoomLa.Common.function.AccessRulo();
            B_Admin admin = new B_Admin();
            admin.CheckIsLogin();
            if (!IsPostBack)
                MyBind();
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='" + customPath2 + "User/UserManage.aspx'>会员管理</a></li><li><a href='ZoneManage.aspx'>会员空间管理</a></li><li><a href='CarManage.aspx'>车辆列表</a></li><li class='active'>抢车位规则管理<a href='CarAdd.aspx'>[添加车辆]</a></li>");
        }
        private void MyBind()
        {
            this.EGV.DataSource = pl.GetCarConfig();
            this.EGV.DataBind();
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.EGV.EditIndex = e.NewEditIndex;
            MyBind();
        }
        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            pl.UpdateCarConfig(((TextBox)EGV.Rows[e.RowIndex].FindControl("TextBox1")).Text, int.Parse(EGV.DataKeys[e.RowIndex].Value.ToString()));
            EGV.EditIndex = -1;
            MyBind();
        }
        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.EGV.EditIndex = -1;
            MyBind();
        }
        protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            EGV.PageIndex = e.NewPageIndex;
            DataBind();
        }
    }
}