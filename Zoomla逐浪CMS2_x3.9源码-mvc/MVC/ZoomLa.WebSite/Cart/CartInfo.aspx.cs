using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.SQLDAL;

namespace ZoomLaCMS.Cart
{
    public partial class CartInfo : System.Web.UI.Page
    {
        B_User buser = new B_User();
        B_Cart cartBll = new B_Cart();
        B_Product proBll = new B_Product();
        //类型,其值同于订单类型
        public int SType { get { return DataConvert.CLng(Request["SType"]); } }
        public string CartCookID
        {
            get
            {
                if (Request.Cookies["Shopby"] == null || Request.Cookies["Shopby"]["OrderNo"] == null)
                    Response.Cookies["Shopby"]["OrderNo"] = cartBll.GenerateCookieID();
                return Request.Cookies["Shopby"]["OrderNo"];
            }
        }
        public string Pros_Hid { get { return Request["Pros_Hid"]; } }
        public string Guest_Hid { get { return Request["Guest_Hid"]; } }
        public string Contract_Hid { get { return Request["Contract_Hid"]; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            B_User.CheckIsLogged(Request.RawUrl);
            if (!IsPostBack)
            {
                M_UserInfo mu = buser.GetLogin(false);
                switch (SType)
                {
                    case 1://酒店
                           //{
                           //    M_Cart_Hotel model = new M_Cart_Hotel();
                           //    model.UserID = buser.GetLogin().UserID;
                           //    //model.HotelName = Request["HotelName"];
                           //    model.GoDate = Convert.ToDateTime(Request["GoDate"]);
                           //    model.OutDate = Convert.ToDateTime(Request["OutDate"]);
                           //    //-------------联系人(全部都进行一定的规范)
                           //    model.Guest.Add(new M_Cart_Contract(Server.UrlDecode(Request["GuestName"]), Request["GuestMobile"], "", ""));
                           //    for (int i = 0; i < Request["ContractName"].Split(',').Length; i++)
                           //    {
                           //        string name = Request["ContractName"].Split(',')[i];
                           //        string mobile = Request["ContractMobile"].Split(',')[i];
                           //        model.Contract.Add(new M_Cart_Contract(Server.UrlDecode(name), Request["ContractMobile"], "", ""));
                           //    }
                           //    //-------------购物车
                           //    ProModel pro = new ProModel() { ProID = DataConvert.CLng(Request["ProID"]), Pronum = DataConvert.CLng(Request["Pronum"]) };
                           //    string addition = JsonConvert.SerializeObject(model);
                           //    int cartid = AddCart(pro, mu, addition);
                           //    Response.Redirect("/Cart/GetOrderInfo.aspx?ProClass=8&ids=" + cartid);
                           //}
                           //break;

                    case 2://旅游
                        {
                            M_Cart_Travel model = new M_Cart_Travel();
                            JArray proArr = (JArray)JsonConvert.DeserializeObject(Pros_Hid);
                            JArray guestArr = (JArray)JsonConvert.DeserializeObject(Guest_Hid);
                            JArray contractArr = (JArray)JsonConvert.DeserializeObject(Contract_Hid);
                            if (string.IsNullOrEmpty(Pros_Hid) || string.IsNullOrEmpty(Guest_Hid) || string.IsNullOrEmpty(Contract_Hid))
                            {
                                function.WriteErrMsg("生成订单失败,提交的信息不完全!");
                            }
                            foreach (JObject pro in proArr)//赋值店铺信息
                            {
                                ProModel proMod = JsonConvert.DeserializeObject<ProModel>(pro.ToString());
                                M_Product proInfo = proBll.GetproductByid(proMod.ProID);
                                proMod.ProName = proInfo.Proname;
                                proMod.StoreID = proInfo.UserShopID;
                                double price = proInfo.LinPrice;
                                DataRow priceDR = proBll.GetPriceByCode(proMod.code, proInfo.Wholesalesinfo, ref price);
                                if (priceDR != null) { proMod.ProName += "(" + priceDR["Proname"] + ")"; }
                                model.ProList.Add(proMod);
                            }
                            foreach (JObject guest in guestArr)
                            {
                                model.Guest.Add(JsonConvert.DeserializeObject<M_Cart_Contract>(guest.ToString()));
                            }
                            foreach (JObject contract in contractArr)
                            {
                                model.Contract.Add(JsonConvert.DeserializeObject<M_Cart_Contract>(contract.ToString()));
                            }
                            //-------------购物车
                            string addition = JsonConvert.SerializeObject(model);
                            if (!string.IsNullOrEmpty(Request.Form["ctl00$Content$IDS_Hid"]))
                            { cartBll.DelByIDS(CartCookID, Request.Form["ctl00$Content$IDS_Hid"]); }
                            //-------------跳转
                            string ids = "";
                            foreach (ProModel pro in model.ProList)
                            {
                                ids += AddCart(pro, mu, addition) + ",";
                            }
                            ids = ids.TrimEnd(',');
                            int proclass = (SType == 1 ? 8 : 7);
                            Response.Redirect("/Cart/GetOrderInfo.aspx?ProClass=" + proclass + "&ids=" + ids);
                        }
                        break;
                    case 3://航班
                           //Response.Redirect("/Cart/GetOrderInfo.aspx?ProClass=&ids=" + ids);
                        break;
                    default:
                        function.WriteErrMsg("未知的提交类型");
                        break;
                }
            }
        }
        //将购买信息,存入购物车
        private int AddCart(ProModel pro, M_UserInfo mu, string addition)
        {
            int proid = pro.ProID;
            int pronum = pro.Pronum;
            if (proid < 1 || pronum < 1) function.WriteErrMsg("商品ID或数量异常");
            M_Cart cartMod = new M_Cart();
            cartMod.Cartid = CartCookID;
            cartMod.StoreID = pro.StoreID;
            cartMod.ProID = proid;
            cartMod.Pronum = pronum;
            cartMod.userid = mu.UserID;
            cartMod.Username = mu.UserName;
            cartMod.Getip = EnviorHelper.GetUserIP();
            cartMod.Addtime = DateTime.Now;
            cartMod.Additional = addition;
            cartMod.code = pro.code;
            cartMod.Proname = pro.ProName;
            cartMod.ID = cartBll.insert(cartMod);
            return cartMod.ID;
        }
    }
}