using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Common;

public partial class Prompt_ShopCart_SelCustomer : System.Web.UI.Page
{
    B_Client_Penson pensonBll = new B_Client_Penson();
    public string Codes
    {
        get { return Request.QueryString["codes"]; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MyBind();
        }
    }
    public void MyBind()
    {
        EGV.DataSource = pensonBll.Select_All();
        EGV.DataBind();
        if(!string.IsNullOrEmpty(Codes))
            Codes_Hid.Value = Codes;
    }
    protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        EGV.PageIndex= e.NewPageIndex;
        MyBind();
    }
}