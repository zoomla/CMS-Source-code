using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Common;
using ZoomLa.Components;

public partial class User_Profile_ExChangeRecord : System.Web.UI.Page
{
    B_User buser = new B_User();
    B_GiftCard_User bgcUser = new B_GiftCard_User();
    B_GiftCard_shop bgcShop = new B_GiftCard_shop();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Bind();
        }
    }

    #region 内部方法

    private void Bind()
    {
        DataTable dt = bgcUser.GetSelectByUserid(buser.GetLogin().UserID);
        repf.DataSource = dt;
        repf.DataBind();
    }

    public string GetRemark(string remark)
    {
        if (remark.Length > 50)
        {
            return remark.Substring(0, 50);
        }
        else
        {
            return remark;
        }
    }

    public string GetCardinfo(string shopid)
    {
        M_GiftCard_shop scard = bgcShop.GetSelect(DataConverter.CLng(shopid));
        if (scard != null && scard.id > 0)
        {
            return scard.Cardinfo;
        }
        else
        {
            return "";
        }
    }

    public string GetCardType(string cardtype)
    {
        if (cardtype == "1")
        {
            return "用积分兑换";
        }
        else
        {
            return "用返利兑换";
        }
    }

    public string GetConfirmData(string confState,string data)
    {
        if (confState == "0")  //未确认
        {
            return "未确认";
        }
        else
        {
            return DataConverter.CDate(data).ToShortDateString();
        }
    }

    public string GetState(string state)
    {
        if (state == "0")
        {
            return "未兑换";
        }
        else
        {
            return "已兑换";
        }
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
