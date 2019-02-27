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
    using System.Text;

public partial class PaypalSuccess : System.Web.UI.Page
{
    private B_OrderList bll = new B_OrderList();
    private B_Cart ACl = new B_Cart();
    private B_CartPro bc = new B_CartPro();
    private B_MoneyManage mm = new B_MoneyManage();
    protected void Page_Load(object sender, EventArgs e)
    {
        //string success = Request.QueryString["st"];
        //if (success.Equals("Completed"))
        //{
        //    string ppItem = Request.QueryString["item_number"];
        //    string money_code = Request.QueryString["cc"].ToString();
        //    if (mm.GetMoney(money_code).Tables.Count > 0)
        //    {
        //        DataTable dt = mm.GetMoney(money_code).Tables[0];
        //        if (dt != null && dt.Rows.Count > 0)
        //        {
        //            double money = DataConverter.CDouble(dt.Rows[0]["Money_rate"]);
        //            bll.UpOrderNo(ppItem, DataConverter.CDouble(Request.QueryString["amt"]), money, money_code);//更新订单状态
        //            DataTable Oid = bll.GetOrderbyOrderNo(ppItem);
        //            if (Oid != null && Oid.Rows.Count > 0)
        //            {
        //                DataTable Cid = bc.GetCartProOrderID(DataConverter.CLng(Oid.Rows[0]["id"]));
        //                if (Cid.Rows.Count > 0)
        //                    ACl.DeleteByID(DataConverter.CLng(Cid.Rows[0]["Cartid"]));//删除购物车ＩＤ
        //            }
        //        }
        //    }
        //}
    }
}
