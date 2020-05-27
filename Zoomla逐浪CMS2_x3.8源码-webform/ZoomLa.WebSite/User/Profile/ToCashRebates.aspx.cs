using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Common;
using ZoomLa.Components;

public partial class User_Profile_ToCashRebates : System.Web.UI.Page
{
    //B_Shopsite bshop = new B_Shopsite();
    B_User buser = new B_User();
    //B_Honor bhonor = new B_Honor();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Bind();
        }
    }

    public string GetStatus(string status)
    {
        int s = DataConverter.CLng(status);

        if (s == 0)
            return "申请中";
        else if (s == 1)
            return "已兑现";
        else
            return "其他";
    }

    public string GetDataBypay(string state,string paydata)
    {
        if (state=="0")
        {
            return "等待支付";
        }
        else
        {
            return DataConverter.CDate(paydata).ToShortDateString();
        }
    }

    #region private functin

    private void Bind()
    {
        //List<M_Honor> honor = bhonor.GetSelectByUserid(buser.GetLogin().UserID);
        //repf.DataSource = honor;
        //repf.DataBind();
    }

    #endregion

    protected void txtPage_TextChanged(object sender, EventArgs e)
    {
        ViewState["page"] = "1";
        Bind();
    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        Bind();
    }
}
