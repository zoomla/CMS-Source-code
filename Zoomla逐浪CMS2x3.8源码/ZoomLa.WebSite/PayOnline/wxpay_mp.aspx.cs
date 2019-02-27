using System;

public partial class PayOnline_wxpay_mp : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Server.Transfer("/API/pay/wxpay_mp.aspx", true);
    }
}