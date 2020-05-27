using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL.CreateJS;
using ZoomLa.BLL.Shop;


namespace ZoomLaCMS.Manage.User.Addon
{
    public partial class RegularList : System.Web.UI.Page
    {
        B_Shop_MoneyRegular regularBll = new B_Shop_MoneyRegular();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MyBind();
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='../User/UserManage.aspx'>用户管理</a></li><li class='active'>规则列表 [<a href='AddRegular.aspx'>添加规则</a>]</li>");
            }
        }
        private void MyBind()
        {
            EGV.DataSource = regularBll.Sel();
            EGV.DataBind();
        }
        protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            EGV.PageIndex = e.NewPageIndex;
            MyBind();
        }
        protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "del2":
                    int id = Convert.ToInt32(e.CommandArgument);
                    regularBll.Del(id);
                    break;
            }
            MyBind();
        }
        protected void EGV_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row != null && e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView dr = e.Row.DataItem as DataRowView;
                e.Row.Attributes.Add("ondblclick", "location='AddRegular.aspx?ID=" + dr["ID"] + "'");
            }
        }
    }
}