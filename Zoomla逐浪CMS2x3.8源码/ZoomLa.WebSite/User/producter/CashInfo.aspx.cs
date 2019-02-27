using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;
using ZoomLa.SQLDAL;

public partial class User_producter_CashInfo : System.Web.UI.Page
{
    private B_User buser = new B_User();
    private B_Cash bc = new B_Cash();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) 
        {
            MyBind();
        }
        
    }
    private void MyBind()
    {
        M_UserInfo mu = buser.GetLogin();
        EGV.DataSource = SqlHelper.ExecuteTable("SELECT Y_ID,Bank,Account,money,yState  FROM ZL_Cash");
        EGV.DataBind();
    }
    protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        EGV.PageIndex = e.NewPageIndex;
        MyBind();
    }
    
}
