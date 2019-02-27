using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Components;
using ZoomLa.SQLDAL;

public partial class User_Profile_OrderManage : Page
{
    B_User buser = new B_User();
    //B_Shopsite bshop = new B_Shopsite();
    int currentUser = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        currentUser = buser.GetLogin().UserID;
        if (!IsPostBack)
        {
            string url = Request.CurrentExecutionFilePath;
            DataTable list = buser.SelByUserID(currentUser);
            if (list.Rows.Count != 0)
            {

                string str = list.Rows[0]["seturl"].ToString();
                string[] strarr = str.Split(',');

                for (int i = 0; i <= strarr.Length - 1; i++)
                {
                    if (strarr[i].ToLower() == url.ToLower())
                    {


                        DV_show.Visible = false;
                        Login.Visible = true;
                        
                    }

                }

            }

            Bind();
        }
        int state = DataConverter.CLng(Request.QueryString["state"]);
        if (state == 0){ orderlist.InnerHtml="<font color='red'>下单后被跟踪到即为待返订单，去商户完成交易且无退换货后可获得返利</font>";}
        if (state == 1) { orderlist.InnerHtml = "<font color='red'>成功完成支付且未发生退换货的订单，在核对无误并将返利打给您后，该笔订单为已返利订单</font>"; }
        if (state == 2) { orderlist.InnerHtml = "<font color='red'>在商户取消订单、超时未付款或是退换货等情况，在核对后该笔订单为失效订单</font>"; }
    }

    #region private functin

    private void Bind()
    {
        //int state = 0;
        //if (!string.IsNullOrEmpty(Request.QueryString["state"]))
        //{
        //    state = DataConverter.CLng(Request.QueryString["state"]);
        //}
        //List<M_RebateOrder> orebas = brebate.GetSeleByUserAndOrderState(buser.GetLogin().UserID, state);
        //if (orebas == null || orebas.Count <= 0)
        //{
        //    if (state == 0)
        //    {
        //        lblOrderTip.InnerText = "暂无待返订单";
        //        tips.InnerText = "您当前有0个订单处于待返状态，确认订单后预计最高可返0元。";
        //    }
        //    if (state == 1)
        //    {
        //        lblOrderTip.InnerText = "暂无已返订单";
        //        tips.InnerText = "您当前有0个订单处于已返状态，确认订单后预计最高可返0元。";
        //    }
        //    if (state == 2)
        //    {
        //        lblOrderTip.InnerText = "暂无失效订单";
        //        tips.InnerText = "您当前有0个订单处于失效状态";
        //    }
        //    lblOrderTip.Visible = true;
        //    repf.Visible = false;
        //}
        //else
        //{
        //    lblOrderTip.Visible = false;
        //    repf.Visible = true;
        //}
        //count.Value = orebas.Count.ToString();
        //repf.DataSource = orebas;
        //repf.DataBind();
    }

    #endregion

    //double money = 0;
    protected void sure_Click(object sender, EventArgs e)
    {
        M_UserInfo info = buser.GetLogin();
        string PWD = Second.Text;

        if (StringHelper.MD5(PWD) == info.PayPassWord)
        {


            DV_show.Visible = true;
            Login.Visible = false;
        }
        else
        {
            Response.Write("<script>alert('密码错误,请重新输入！');</script>");
            DV_show.Visible = false;
            Login.Visible = true;
            ;
        }
    }
    protected void repf_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            HiddenField hfId = e.Item.FindControl("hfId") as HiddenField;
            Label lblShop = e.Item.FindControl("lblShop") as Label;  //商家
            Label lblProData = e.Item.FindControl("lblProData") as Label;  //订单反馈日期
            Label lblFM = e.Item.FindControl("lblFM") as Label;   //订单返利金额
            Label lblState = e.Item.FindControl("lblState") as Label;
            Label lblTime = e.Item.FindControl("lblTime") as Label;  //预计返利时间
         
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
            //    money = money + mrebate.ProfileMoney;
            //    lblProData.Text = mrebate.OrderFbackData.ToShortDateString();
            //    lblTime.Text = mrebate.OrderData.AddMonths(2).Year.ToString() + "年" + mrebate.OrderData.AddMonths(2).Month + "月";
            //    if (mrebate.OrderState == 0)
            //    {
            //        lblState.Text = "待返订单";
            //        tips.InnerText = "您当前有"+count.Value+"个订单处于待返状态，确认订单后预计最高可返"+money.ToString("0.00")+"元。";
            //        lblFM.Text = "-";
            //    }
            //    if (mrebate.OrderState == 1)
            //    {
            //        lblState.Text = "已返订单";
            //        lblFM.Text = mrebate.ProfileMoney.ToString("0.00");
            //        tips.InnerText = "您当前有" + count.Value + "个订单处于已返状态，确认订单后预计最高可返" + money.ToString("0.00") + "元。";
            //    }
            //    if (mrebate.OrderState == 2)
            //    {
            //        lblState.Text = "失效订单";
            //        lblFM.Text = "-";
            //        tips.InnerText = "您当前有" + count.Value + "个订单处于失效状态";
            //    }
            //}
        }
    }
    protected void txtPage_TextChanged(object sender, EventArgs e)
    {
        Bind();
    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        Bind();
    }
}
