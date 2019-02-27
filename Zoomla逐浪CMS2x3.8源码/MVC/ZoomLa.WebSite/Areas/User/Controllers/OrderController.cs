using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZoomLa.BLL;
using ZoomLa.BLL.Helper;
using ZoomLa.BLL.Shop;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;
using ZoomLa.Model.Shop;
using ZoomLa.SQLDAL;
using ZoomLa.SQLDAL.SQL;
using ZoomLaCMS.Areas.User.Models;

namespace ZoomLaCMS.Areas.User.Controllers
{
    public class OrderController : ZLCtrl
    {
        OrderViewModel viewMod = null;
        B_OrderList orderBll = new B_OrderList();
        B_Order_Repair repBll = new B_Order_Repair();
        B_Order_Share shareBll = new B_Order_Share();
        B_CartPro cartProBll = new B_CartPro();
        B_Group groupBll = new B_Group();
        B_Admin badmin = new B_Admin();
        OrderCommon orderCom = new OrderCommon();
        public void Index() { Response.Redirect("OrderList");}
        public ActionResult OrderList()
        {
            viewMod = new OrderViewModel(CPage, PSize, mu, Request);
            return View(viewMod);
        }
        public PartialViewResult Order_Data()
        {
            viewMod = new OrderViewModel(CPage, PSize, mu, Request);
            return PartialView("OrderList_List", viewMod);
        }
        public ActionResult TripOrder()
        {
            PageSetting setting = orderBll.U_SelPage(CPage, PSize, mu.UserID, "", "", (int)M_OrderList.OrderEnum.Trval);
            return View(setting);
        }
        public PartialViewResult Trip_Data()
        {
            PageSetting setting = orderBll.U_SelPage(CPage, PSize, mu.UserID, "", "", (int)M_OrderList.OrderEnum.Trval);
            return PartialView("TripOrder_List", setting);
        }
        public ActionResult HotelOrder()
        {
            PageSetting setting = orderBll.U_SelPage(CPage, PSize, mu.UserID, "", "", (int)M_OrderList.OrderEnum.Hotel);
            return View(setting);
        }
        public PartialViewResult Hotel_Data()
        {
            PageSetting setting = orderBll.U_SelPage(CPage, PSize, mu.UserID, "", "", (int)M_OrderList.OrderEnum.Hotel);
            return PartialView("HotelOrder_List", setting);
        }
        public ActionResult OrderProList()
        {
            return View(new VM_OrderPro(mu, Request));
        }
        public int Order_API() 
        {
            int oid = Convert.ToInt32(Request.Form["oid"]);
            string action = Request.Form["action"];
            int result = Failed;
            //-----
            M_OrderList orderMod = orderBll.SelReturnModel(oid);
            if (mu.UserID != orderMod.Userid) { return result; }
            switch (action)
            {
                case "del":
                    {
                        orderBll.DelByIDS_U(oid.ToString(), mu.UserID);
                        result = Success;
                    }
                    break;
                case "receive":
                    {
                        if (orderMod.Paymentstatus < (int)M_OrderList.PayEnum.HasPayed) break;
                        orderBll.UpdateByField("StateLogistics", "2", oid);
                        result = Success;
                    }
                    break;
                case "reconver"://还原
                    orderBll.UpdateByField("Aside", "0", oid);
                    result = Success;
                    break;
                case "realdel"://彻底删除
                    orderBll.UpdateByField("Aside", "2", oid);
                    result = Success;
                    break;
                default:
                    break;
            }
            return result;
        }
        public int Order_Del(string ids) 
        {
            orderBll.DelByIDS_U(ids, mu.UserID);
            return Success;
        }
        //----------------------------Share
        private int ProID { get { return DataConvert.CLng(Request.QueryString["ProID"]); } }
        //admin
        public string Mode { get { return Request.QueryString["Mode"] ?? ""; } }
        public ActionResult AddShare()
        {
            int OrderID = DataConvert.CLng(Request.QueryString["OrderID"]);
            M_OrderList orderMod = orderBll.SelReturnModel(OrderID);
            if (orderMod.Userid != mu.UserID) { function.WriteErrMsg("你无权访问该订单"); return Content(""); }
            if (orderMod.OrderStatus < (int)M_OrderList.StatusEnum.OrderFinish) { function.WriteErrMsg("订单未完结,不允许评价"); return null; }
            ViewBag.dt = cartProBll.SelForRPT(OrderID, "comment");
            return View(orderMod);
        }
        public ActionResult ShareList()
        {
            if (Mode.ToLower().Equals("admin")) { B_Admin.CheckIsLogged(); }
            PageSetting setting = shareBll.SelPage(CPage, PSize, 0, ProID);
            return View(setting);
        }
        public PartialViewResult Share_Data()
        {
            int pid = DataConvert.CLng(Request["pid"]);
            int proid = DataConvert.CLng(Request["proid"]);
            PageSetting setting = shareBll.SelPage(CPage, PSize, pid, proid);
            if (pid == 0) { return PartialView("ShareList_List", setting); }
            else {return PartialView("ShareList_Reply", setting); }
        }
        [HttpPost]
        public string Share_API()
        {
            string action = Request.Form["action"];
            string result = "-1";
            switch (action)
            {
                case "reply"://回复,不需要购买也可,但必须登录
                    {
                        string msg = Request.Form["msg"];
                        int pid = DataConvert.CLng(Request.Form["ID"]);
                        int rid = DataConvert.CLng(Request.Form["rid"]);
                        int proid = DataConvert.CLng(Request.Form["proid"]);
                        if (pid < 1 || rid < 1 || string.IsNullOrEmpty(msg)) break;
                        M_Order_Share replyMod = shareBll.SelReturnModel(rid);
                        M_Order_Share shareMod = new M_Order_Share();
                        shareMod.UserID = mu.UserID;
                        shareMod.Pid = pid;
                        shareMod.MsgContent = msg;
                        shareMod.ReplyID = rid;
                        shareMod.ProID = proid;
                        shareMod.ReplyUid = replyMod.UserID;
                        shareBll.Insert(shareMod);
                        result = "1";
                    }
                    break;
                case "del":
                    {
                        int id = Convert.ToInt32(Request.Form["id"]);
                        shareBll.Del(id);
                        result = "1";
                    }
                    break;
                default:
                    break;
            }
            return result;
        }
        [HttpPost]
        [ValidateInput(false)]
        public void Share_Add()
        {
            int OrderID = DataConvert.CLng(Request.QueryString["OrderID"]);
            int cartid = DataConverter.CLng(Request.Form["cart_rad"]);
            //购买时间与商品信息也需要写入
            M_CartPro cartProMod = cartProBll.SelReturnModel(cartid);
            cartProMod.AddStatus = StrHelper.AddToIDS(cartProMod.AddStatus, "comment");//换为枚举
            M_Order_Share shareMod = new M_Order_Share();
            shareMod.Title = Request.Form["Title_T"];
            shareMod.MsgContent = Request.Form["MsgContent_T"];
            shareMod.UserID = mu.UserID;
            shareMod.IsAnonymous =string.IsNullOrEmpty(Request.Form["IsHideName"])? 1 : 0;
            shareMod.Score = DataConverter.CLng(Request.Form["star_hid"]);
            if (shareMod.Score > 5) { shareMod.Score = 5; }
            shareMod.Imgs = Request.Form["Attach_Hid"];
            shareMod.Labels = "";
            shareMod.OrderID = cartProMod.Orderlistid;
            shareMod.ProID = cartProMod.ProID;
            shareMod.OrderDate = cartProMod.Addtime;
            shareBll.Insert(shareMod);
            cartProBll.UpdateByID(cartProMod);
            DataTable dt = cartProBll.SelForRPT(OrderID, "comment");
            if (dt.Rows.Count < 1)
            {
                function.WriteSuccessMsg("评价成功,将跳转至商品页", orderCom.GetShopUrl(cartProMod.StoreID, cartProMod.ProID)); return; //返回商品页,或对应的商品页
            }
            else { function.WriteErrMsg("评价成功"); }
        }
        //----------------------------申请退款|返修
        public ActionResult DrawBack() 
        {
            M_OrderList orderMod = orderBll.SelReturnModel(Mid);
            if (orderMod == null) { function.WriteErrMsg("订单不存在"); return Content(""); }
            if (orderMod.Userid != mu.UserID) { function.WriteErrMsg("订单不属于你,拒绝操作"); return Content(""); }
            //只有已付款订单,并且未退款才可申请
            if (orderMod.Paymentstatus != (int)M_OrderList.PayEnum.HasPayed) { function.WriteErrMsg("该订单当前支付状态无法退款"); return Content(""); }
            if (orderMod.OrderStatus != 99 && orderMod.OrderStatus != 1 && orderMod.OrderStatus != 0) { function.WriteErrMsg("订单属于不可退款状态"); return Content(""); }
            if (SiteConfig.SiteOption.THDate != 0 && (DateTime.Now - orderMod.AddTime).TotalDays > SiteConfig.SiteOption.THDate) { function.WriteErrMsg("订单已超过" + SiteConfig.SiteOption.THDate + "天,无法申请退款"); return Content(""); }
            if (orderMod.OrderStatus != 99 && orderMod.OrderStatus!=1) { function.WriteErrMsg("订单属于不可退款状态"); return Content(""); }
            return View(orderMod);
        }
        public ActionResult DrawBack_Add()
        {
            string text = Request["Back_T"];
            if (text.Length < 10) { function.WriteErrMsg("退款说明最少需十个字符"); return null; }
            M_OrderList orderMod = orderBll.SelReturnModel(Mid);
            orderMod.Settle = orderMod.OrderStatus;
            orderMod.OrderStatus = -1;
            orderMod.Merchandiser = text;
            orderBll.UpdateByID(orderMod);
            return Content("<script>alert('操作成功!');top.CloseComDiag();</script>");
        }
        public ActionResult MyOrderRepair()
        {
            PageSetting setting = repBll.U_SelAll(CPage, PSize, mu.UserID);
            return View(setting);
        }
    }
}
