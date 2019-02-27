using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.SQLDAL;
using ZoomLa.SQLDAL.SQL;

namespace ZoomLaCMS.Areas.User.Models
{
    public class VM_OrderPro
    {
        B_OrderList orderBll = new B_OrderList();
        B_CartPro cartProBll = new B_CartPro();
        B_Cart cartBll = new B_Cart();
        B_User buser = new B_User();
        OrderCommon orderCom = new OrderCommon();
        //CartID,OrderID,OrderNo
        public string OrderNo="";
        public int SType = 0;
        //-------------------
        public M_UserInfo mu = null;
        public PageSetting setting = new PageSetting();
        public M_OrderList orderMod = new M_OrderList();
        //购物车|订单下商品列表
        public DataTable proDT = new DataTable();
        public List<M_Cart_Contract> clientList = null;
        public VM_OrderPro(M_UserInfo mu,HttpRequestBase Request) 
        {
            this.SType = DataConvert.CLng(Request.QueryString["SType"]);
            this.mu = mu;
            this.OrderNo = Request.QueryString["OrderNo"];
            switch (SType)
            {
                case 0://订单
                    orderMod = orderBll.SelModelByOrderNo(OrderNo);
                    if (orderMod == null || orderMod.id == 0) function.WriteErrMsg("订单不存在");
                    if (orderMod.Userid != mu.UserID) function.WriteErrMsg("该订单不属于你,无法查看");
                    proDT = cartProBll.SelByOrderID(orderMod.id);
                    break;
                case 1://购物车
                    proDT = cartBll.GetCarProList(OrderNo);
                    break;
            }
            if (proDT.Rows.Count > 0 && !string.IsNullOrEmpty(proDT.Rows[0]["Additional"].ToString()))
            {
                M_Cart_Travel model = JsonConvert.DeserializeObject<M_Cart_Travel>(proDT.Rows[0]["Additional"].ToString());
                clientList = new List<M_Cart_Contract>();
                clientList.AddRange(model.Guest);
                clientList.AddRange(model.Contract);
            }
        }
    }
}