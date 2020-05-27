namespace ZoomLaCMS.Cart
{
    using System;
    using System.Web;
    using ZoomLa.Common;
    using ZoomLa.BLL;
    using ZoomLa.BLL.API;
    using ZoomLa.Model;
    /*
     * 仅允许Form提交
     * 用于价格计算,增减商品
     * -1:失败,1:成功或直接返回值
     */
    public class OrderCom : API_Base, IHttpHandler
    {
        B_Cart cartBll = new B_Cart();
        M_Cart cartMod = new M_Cart();
        B_User buser = new B_User();
        public void ProcessRequest(HttpContext context)
        {
            M_UserInfo mu = buser.GetLogin();
            retMod.retcode = M_APIResult.Failed;
            string cartCookID = GetCartCookID(context);
            //retMod.callback = CallBack;//暂不开放JsonP
            try
            {
                switch (Action)
                {
                    case "deladdress":
                        {
                            B_UserRecei receBll = new B_UserRecei();
                            int id = DataConverter.CLng(Req("id"));
                            if (mu == null || mu.UserID == 0 || id < 1) { OldRep("-1"); }
                            else
                            {
                                receBll.U_DelByID(id, mu.UserID); OldRep("1");
                            }
                        }
                        break;
                    case "setnum"://ID,数量,Cookies,可不登录,数量不能小于1
                        {
                            int id = DataConverter.CLng(Req("mid"));
                            int pronum = DataConverter.CLng(Req("pronum"));
                            if (id < 1 || pronum < 1 || string.IsNullOrEmpty(cartCookID))
                            {
                                OldRep("-1");
                            }
                            else
                            {
                                cartBll.UpdateProNum(cartCookID,mu.UserID, id, pronum);
                                OldRep("1");
                            }
                        }
                        break;
                    case "arrive":
                        {
                            B_Arrive avBll = new B_Arrive();
                            string flow = Req("flow");
                            double money = double.Parse(Req("money"));
                            string err = "";
                            M_Arrive avMod = avBll.SelModelByFlow(flow, mu.UserID);
                            if (avBll.U_CheckArrive(avMod, mu.UserID, ref money, ref err))
                            {
                                retMod.retcode = M_APIResult.Success;
                                //已优惠金额,优惠后金额
                                retMod.result = "{\"amount\":\"" + avMod.Amount + "\",\"money\":\"" + money + "\"}";
                            }
                            else { retMod.retmsg = err; }
                        }
                        break;
                    default:
                        retMod.retmsg = "[" + Action + "]接口不存在";
                        break;
                }
            }
            catch (Exception ex) { retMod.retmsg = ex.Message; }
            RepToClient(retMod);
        }
        public void OldRep(string result) { HttpResponse rep = HttpContext.Current.Response; rep.Clear(); rep.Write(result); rep.Flush(); rep.End(); }
        public bool IsReusable { get { return false; } }
        public string GetCartCookID(HttpContext context)
        {
            if (context.Request.Cookies["Shopby"] == null || context.Request.Cookies["Shopby"]["OrderNo"] == null) { return ""; }
            else { return context.Request.Cookies["Shopby"]["OrderNo"]; }
        }
    }
}