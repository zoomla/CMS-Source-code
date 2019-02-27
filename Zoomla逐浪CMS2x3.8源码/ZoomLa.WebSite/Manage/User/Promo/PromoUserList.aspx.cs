using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Common;

public partial class Manage_User_Promo_PromoUserList : System.Web.UI.Page
{
    B_User buser = new B_User();
    public int PUserID { get { return DataConverter.CLng(Request.QueryString["PUserID"]); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MyBind();
        }
    }
    public void MyBind(string search="",string startdate="",string enddate="")
    {
        EGV.DataSource = buser.SelPromoUser(PUserID, search, startdate, enddate);
        EGV.DataBind();
    }

    protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        EGV.PageIndex = e.NewPageIndex;
        MyBind();
    }

    protected void Search_Btn_Click(object sender, EventArgs e)
    {
        MyBind(Search_T.Text,StartDate_T.Text,EndDate_T.Text);
    }
}