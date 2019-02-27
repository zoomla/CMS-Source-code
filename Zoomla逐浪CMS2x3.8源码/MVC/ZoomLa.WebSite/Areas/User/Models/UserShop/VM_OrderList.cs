using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.SQLDAL.SQL;

namespace ZoomLaCMS.Areas.User.Models.UserShop
{
    public class VM_OrderList
    {
        B_Content conBll = new B_Content();
        B_OrderList orderBll = new B_OrderList();
        public PageSetting setting = null;
        public M_UserInfo mu = null;
        public M_CommonData storeMod = null;
        public double total = 0;
        public string OrderStatus, PayStatus, OrderType;
        public VM_OrderList(int cpage, int psize, M_UserInfo mu, HttpRequestBase Request, ref string err)
        {
            OrderStatus = Request.QueryString["orderstatus"] ?? "";
            PayStatus = Request.QueryString["PayStatus"] ?? "";
            OrderType = Request.QueryString["ordertype"] ?? "";
            //int quick = Convert.ToInt32(Request.Form["QuickSearch_DP"]);
            //int skeyType = Convert.ToInt32(Request.Form["SkeyType_DP"]);
            storeMod = conBll.SelMyStore_Ex(ref err);
            if (!string.IsNullOrEmpty(err)) { function.WriteErrMsg(err); }
            else { setting = orderBll.SelPage(cpage, psize, out total, storeMod.GeneralID, OrderType, OrderStatus, PayStatus); }
        }
    }
}