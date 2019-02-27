using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Common;
using ZoomLa.Components;

public partial class User_Profile_Profile : System.Web.UI.Page
{
    //B_Shopsite bshop = new B_Shopsite();
    B_User buser = new B_User();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Bind();
        }
    }

    //行绑定
    protected void repf_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            HiddenField hfId = e.Item.FindControl("hfId") as HiddenField;
            Label lblShop = e.Item.FindControl("lblShop") as Label;
            Label lblAuditData = e.Item.FindControl("lblAuditData") as Label;
            Label lblProSate = e.Item.FindControl("lblProSate") as Label;

            //M_RebateOrder mrebate = brebate.GetSelectByid(DataConverter.CLng(hfId.Value));
            //if (mrebate != null && mrebate.Id > 0)
            //{
            //    M_Shopsite site = bshop.GetSelectById(DataConverter.CLng(mrebate.Shopsite));
            //    if (site != null && site.id > 0)
            //    {
            //        lblShop.Text = site.Sname;
            //    }
            //    else
            //    {
            //        lblShop.Text = mrebate.Shopsite;
            //    }
            //    lblProSate.Text = mrebate.OrderState == 0 ? "未确认" : "已确认";
            //}
        }
    }

    #region private functin

    private void Bind()
    {
        //int state = 0;
        //if (!string.IsNullOrEmpty(Request.QueryString["state"]))
        //{
        //    state = DataConverter.CLng(Request.QueryString["state"]);
        //}
        //List<M_RebateOrder> orebas = brebate.GetSeleByUserAndOrderState(buser.GetLogin().UserID,state);
        //repf.DataSource = orebas;
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
