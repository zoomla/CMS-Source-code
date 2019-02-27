<%@ WebHandler Language="C#" Class="cart" %>

using System;
using System.Data;
using System.Web;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.BLL;
using ZoomLa.BLL.User;
using ZoomLa.BLL.API;
using Newtonsoft.Json;
//用于对接APP,用户必须登录
public class cart : API_Base, IHttpHandler
{
    B_User buser = new B_User();
    B_User_API buapi = new B_User_API();
    B_Product proBll = new B_Product();
    B_Cart cartBll = new B_Cart();
    private string OpenID { get { return Req("openid"); } }
    public void ProcessRequest(HttpContext context)
    {

        //function.WriteErrMsg("接口默认关闭,请联系管理员开启");
        M_UserInfo mu = B_User_API.GetLogin(OpenID);
        //M_UserInfo mu = buser.GetLogin();
        retMod.retcode = M_APIResult.Failed;
        retMod.callback = CallBack;
        if (mu.IsNull) { retMod.retmsg = "用户未登录"; RepToClient(retMod); }
        try
        {
            switch (Action)
            {
                case "add":
                    int proID = DataConverter.CLng(Req("proid"));
                    M_Product proMod = proBll.GetproductByid(proID);
                    if (proMod == null) { retMod.retmsg = "商品不存在"; }
                    else
                    {
                        //检测购物车中是否有该用户的该商品,有则叠加
                        Insert(mu, proMod, GetProNum());
                        retMod.retcode = M_APIResult.Success;
                    }
                    break;
                case "del":
                    {
                        string ids = Req("ids");
                        cartBll.U_DelByIDS(ids, mu.UserID);
                        retMod.retcode = M_APIResult.Success;
                    }
                    break;
                case "setnum"://修改商品数量,直接将数值提交
                    {
                        int id = DataConverter.CLng(Req("id"));
                        cartBll.U_SetNum(mu.UserID, id, GetProNum());
                        retMod.retcode = M_APIResult.Success;
                    }
                    break;
                case "list":
                    {
                        DataTable dt = cartBll.SelByCartID(mu.UserID.ToString(), mu.UserID, -100);
                        retMod.result = JsonConvert.SerializeObject(dt);
                        retMod.retcode = M_APIResult.Success;
                    }
                    break;
                case "get":
                    {
                        int id = DataConverter.CLng(Req("id"));
                        M_Cart cartMod = cartBll.SelReturnModel(id);
                        if (null == cartMod) { retMod.retmsg = "找不到对应ID的数据!"; }
                        else if (cartMod.userid != mu.UserID) { retMod.retmsg = "你无权访问该数据!"; }
                        else
                        {
                            retMod.result = JsonConvert.SerializeObject(cartMod);
                            retMod.retcode = M_APIResult.Success;
                        }
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

    public bool IsReusable { get { return false; } }
    //以用户ID作为CookiesID,方便多平台共用
    private void Insert(M_UserInfo mu, M_Product proMod, int pronum)
    {
        M_Cart cartMod = cartBll.SelModelByWhere(mu.UserID, proMod.ID);
        if (cartMod == null) { cartMod = new M_Cart(); }
        cartMod.ProID = proMod.ID;
        cartMod.Proname = proMod.Proname;
        cartMod.Pronum = cartMod.Pronum + pronum;
        cartMod.AllMoney = cartMod.Pronum * proMod.LinPrice;
        if (cartMod.ID > 0) { cartBll.UpdateByID(cartMod); }
        else
        {
            cartMod.Cartid = mu.UserID.ToString();
            cartMod.userid = mu.UserID;
            cartMod.Username = mu.UserName;
            cartBll.insert(cartMod);
        }
    }
    private int GetProNum()
    {
        int pronum = DataConverter.CLng(Req("pronum"));
        pronum = pronum < 1 ? 1 : pronum;
        return pronum;
    }
}