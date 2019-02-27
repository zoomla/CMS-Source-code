using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Http;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;

namespace ZoomLa.Ajax.WebAPI
{
    public class CartController:ApiController
    {
        B_Cart cartBll = new B_Cart();
        B_User buser = new B_User();
        HttpRequest curReq = HttpContext.Current.Request;

        [HttpPost]
        //ProID,数量,Cookies,可不登录,数量不能小于1
        public string SetNum(int mid, int pronum)
        {
            if (mid < 1 || pronum < 1 || string.IsNullOrEmpty(GetCartCookID()))
            {
                return "-1";
            }
            else
            {
                cartBll.UpdateProNum(GetCartCookID(), mid, pronum);
                return "1";
            }
        }
        [HttpPost]
        //删除收货地址
        public string DelAddress(int id)
        {
            B_UserRecei receBll = new B_UserRecei();
            M_UserInfo mu = buser.GetLogin();
            if (mu.UserID < 1 || id < 1) { return "-1"; }
            else
            {
                receBll.U_DelByID(id, mu.UserID);
            }
            return "1";
            //string r = "";
            //if (HttpContext.Current.Session == null) { r ="1"; }
            //else if (string.IsNullOrEmpty(HttpContext.Current.Session.SessionID)) { r = "2"; }
            //else { r = HttpContext.Current.Session.SessionID; }
            //return r;
        }
        //使用优惠券
        public string Arrive(JObject jobj)
        {
            //B_Arrive arriveBll = new B_Arrive();
            //string code = jobj["code"].ToString();
            //string pwd = jobj["pwd"].ToString();
            //double money = Convert.ToDouble(jobj["money"].ToString());
            //string remind = "";
            //var amount = arriveBll.GetArriveByaa(code, pwd, money, out remind);
            //string result = "{\"amount\":\"" + amount + "\",\"remind\":\"" + remind + "\"}";
            //return result;
            return "";
        }
        //-----------Tools
        private string GetCartCookID()
        {
            if (curReq.Cookies["Shopby"] == null || curReq.Cookies["Shopby"]["OrderNo"] == null)
            {
                return "";
            }
            else
                return curReq.Cookies["Shopby"]["OrderNo"];
        }
    }
}
