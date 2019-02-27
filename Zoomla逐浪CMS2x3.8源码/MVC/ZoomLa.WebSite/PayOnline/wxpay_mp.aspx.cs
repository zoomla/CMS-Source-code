namespace ZoomLaCMS.PayOnline
{
    using System;
    using ZoomLa.SQLDAL;
    public partial class wxpay_mp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Server.Transfer("/API/pay/wxpay_mp.aspx",true);
        }
    }
}