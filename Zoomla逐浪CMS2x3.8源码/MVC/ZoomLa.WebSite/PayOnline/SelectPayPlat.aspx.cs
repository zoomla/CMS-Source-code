namespace ZoomLaCMS.PayOnline
{
    using System;
    using System.Data;
    using System.Configuration;
    using System.Collections;
    using System.Web;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Web.UI.WebControls.WebParts;
    using System.Web.UI.HtmlControls;
    using ZoomLa.BLL;
    using ZoomLa.Common;
    using ZoomLa.Model;
    public partial class SelectPayPlat : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            B_User.CheckIsLogged(Request.RawUrl);
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            double money = DataConverter.CDouble(Money_T.Text.Trim());
            //if (money < 1) { function.WriteErrMsg("充值金额过小"); return; }
            Response.Redirect("~/PayOnline/OrderPay.aspx?Money=" + money);
        }
    }
}