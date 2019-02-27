using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.BLL;
using System.Data;

public partial class User_Shopfee_paypal : System.Web.UI.Page
{
    protected B_Cart ACl = new B_Cart();
    protected B_OrderList oll = new B_OrderList();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["orderno"] != null)
        {
            string orderno = Request.QueryString["orderno"];   //订单号
            DataTable ordertable = oll.GetOrderbyOrderNo(orderno);  //订单
            string Money_rate = Request.QueryString["Money_rate"];   //汇率
            string ordersamount = "";        //总价
            if (ordertable != null && ordertable.Rows.Count > 0)
            {
                ordersamount = ordertable.Rows[0]["ordersamount"].ToString();
            }
            string money = ((DataConverter.CFloat(ordersamount) + DataConverter.CFloat(ordersamount) * 0.05) * DataConverter.CFloat(Money_rate)).ToString("F2"); //转换后的货币
            if (Session["randoms" + orderno] != null)
            {
                string radom = Session["randoms" + orderno].ToString();

                if (StringHelper.MD5(radom) == Request.QueryString["safecode"])  //判断安全码是否相同
                {
                    StringBuilder bull = new StringBuilder();
                    bull.AppendLine("<form name=\"paypal\" id=\"paypal\" action=\"https://www.paypal.com/cgi-bin/webscr\"  method=\"post\"> ");
                    bull.AppendLine("<input type=\"hidden\" name=\"cmd\" value=\"_xclick\">  ");
                    bull.AppendLine("<input type=\"hidden\" name=\"business\" value=\"" + Request.QueryString["bus"] + "\"><!--这里填写你的paypal账户email-->  ");
                    bull.AppendLine("<input type=\"hidden\" name=\"item_name\" value=\"" + orderno + "\"><!--这里填写客户订单的一些相关信息，当客户连到paypal网站付款的时候将看到这些信息-->  ");
                    bull.AppendLine("<input type=\"hidden\" name=\"amount\" value=\"" + money+ "\"><!--订单的总金额信息-->  ");
                    bull.AppendLine("<input type=\"hidden\" name=\"currency_code\" value=\"" + Request.QueryString["currency_code"] + "\"><!--订单总金额对应的货币类型 ,客户可以用其他币种来付款,比如这里订单币种是美元USD,客户可以用欧元EUR来付款,由paypal根据当前汇率自动实现币种之间的换算-->     ");


                    bull.AppendLine("<input type=\"hidden\" name=\"cancel_return\" value=\"" + SiteConfig.SiteInfo.SiteUrl + "\">");
                    bull.AppendLine("<input type=\"hidden\" name=\"upload\" value=\"1\">  ");
                    bull.AppendLine("<input type=\"hidden\" name=\"item_number\" value=\"" +orderno + "\">  ");
                    bull.AppendLine("<input type=\"hidden\" name=\"no_shipping\" value=\"2\">  ");
                    bull.AppendLine("<input type=\"hidden\" name=\"notify_url\" value=\"" + SiteConfig.SiteInfo.SiteUrl + "/User/Shopfee/PaypalSuccess.aspx\"><!--这里告诉paypal付款的通信url,即当客户付款后调用这个url通知系统-->");
                    bull.AppendLine("</form>");

                    bull.AppendLine("<script language=\"javascript\" >");
                    bull.AppendLine("window.onload = function () {");
                    bull.AppendLine("paypal.submit(); ");   //提交表单
                    bull.AppendLine(" }</script>   ");
                    Response.Write(bull.ToString());
                }
            }
        }
    }
}