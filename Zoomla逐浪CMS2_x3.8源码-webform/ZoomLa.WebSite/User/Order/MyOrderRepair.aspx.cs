using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Shop;
using ZoomLa.Model;
using ZoomLa.Model.Shop;
using ZoomLa.Common;


public partial class User_Order_MyOrderRepair : System.Web.UI.Page
{
    B_Order_Repair repailBll = new B_Order_Repair();
    B_User buser = new B_User();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MyBind();
        }
    }
    public void MyBind()
    {
        EGV.DataSource = repailBll.U_SelAll(buser.GetLogin().UserID);
        EGV.DataBind();
    }

    protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        EGV.PageIndex = e.NewPageIndex;
        MyBind();
    }
}