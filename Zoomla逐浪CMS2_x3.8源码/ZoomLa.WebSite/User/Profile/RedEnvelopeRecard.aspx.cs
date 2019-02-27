using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ZoomLa.Model;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Components;

public partial class User_Profile_RedEnvelopeRecard : System.Web.UI.Page
{
    B_RedEnvelope bredenvel = new B_RedEnvelope();
    B_User buser = new B_User();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Bind();
        }
    }

    private void Bind()
    {
        DataTable dt = bredenvel.GetSelectByUserId(buser.GetLogin().UserID);
        repf.DataSource = dt;
        repf.DataBind();
    }

    //红包申请记录
    protected void repf_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            HiddenField hfid = e.Item.FindControl("hfId") as HiddenField;
            Label lblRedE = e.Item.FindControl("lblRedE") as Label;
            Label lblState = e.Item.FindControl("lblState") as Label;
            Label lblRemark = e.Item.FindControl("lblRemark") as Label;

            M_RedEnvelope mredenv = bredenvel.GetSelect(DataConverter.CLng(hfid.Value));
            if (mredenv != null && mredenv.id > 0)
            {
                if (mredenv.type == 0)
                {
                    lblRedE.Text = (DataConverter.CDouble(mredenv.RedEnvelope)+mredenv.DeducFee).ToString() + "元";
                }
                else
                {
                    lblRedE.Text = mredenv.RedEnvelope;
                }
                lblState.Text = mredenv.OrderState == 0 ? "申请中" : "已完成申请";
                if (mredenv.Remark.Length > 50)
                {
                    lblRemark.Text = mredenv.Remark.Substring(0, 50);
                }
                else
                {
                    lblRemark.Text = mredenv.Remark;
                }
            }
        }
    }

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
