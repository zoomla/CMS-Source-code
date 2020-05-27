using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Common;
using ZoomLa.Components;

public partial class User_Profile_AddProfile : System.Web.UI.Page
{
    //B_Shopsite bshop = new B_Shopsite();
    B_OrderList border = new B_OrderList();
    B_User buser = new B_User();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["wid"]))
            {
                int wid = DataConverter.CLng(Request.QueryString["wid"]);
                ViewState["wid"] = wid;
                if (wid > 0)
                {
                    //M_Shopsite site = bshop.GetSelectById(wid);
                    //if (site != null && site.id > 0)
                    //{
                    //    txtwname.Text = site.Sname;
                    //}
                }
            }
            buser.CheckIsLogin();
        }
    }

    //添加返利订单
    protected void Button1_Click(object sender, EventArgs e)
    {

    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Write("OrderManage.aspx?state=0");
    }
}
