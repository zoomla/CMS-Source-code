using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL.User;

public partial class Manage_User_CodeList : System.Web.UI.Page
{
    B_Temp tempBll = new B_Temp();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MyBind();
            Call.SetBreadCrumb(Master, "<li>后台管理</li><li><a href='ServiceSeat.aspx'>客服管理</a></li><li class='active'><a href='CodeList.aspx'>引用管理</a>[<a href='ServiceCode.aspx'>在线生成</a>]</li>");
        }
    }
    private void MyBind() 
    {
        DataTable dt = tempBll.SelByType(12);
        EGV.DataSource = dt;
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
                tempBll.Del(id);
                break;
        }
        MyBind();
    }
}