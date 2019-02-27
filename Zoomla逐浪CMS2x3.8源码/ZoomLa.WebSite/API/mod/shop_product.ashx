<%@ WebHandler Language="C#" Class="product" %>

using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.BLL.API;
using ZoomLa.SQLDAL;
using ZoomLa.BLL.User;
using Newtonsoft.Json;
//商品接口,配合cart,order
public class product : API_Base, IHttpHandler
{
    B_User buser = new B_User();
    B_Product proBll = new B_Product();
    B_User_API buapi = new B_User_API();
    private string OpenID { get { return Req("openid") ?? ""; } }
    public void ProcessRequest(HttpContext context)
    {
        throw new Exception("closed");
        M_UserInfo mu = B_User_API.GetLogin(OpenID);
        retMod.retcode = M_APIResult.Failed;
        retMod.callback = CallBack;//暂不开放JsonP
        if (mu.IsNull) { retMod.retmsg = "用户未登录"; RepToClient(retMod); }
        try
        {
            switch (Action)
            {
                case "get":
                    {
                        int id = DataConvert.CLng(Req("id"));
                        M_Product proMod = proBll.GetproductByid(id);
                        if (null == proMod) { retMod.retmsg = "商品不存在!"; }
                        else if (proMod.UserID != mu.UserID) { retMod.retmsg = "你无权访问该信息!"; }
                        else
                        {
                            retMod.result = JsonConvert.SerializeObject(proMod);
                            retMod.retcode = M_APIResult.Success;
                        }
                    }
                    break;
                case "add":
                    {
                        string product_json = Req("model");
                        M_Product product = JsonConvert.DeserializeObject<M_Product>(product_json);
                        product.UserID = mu.UserID;
                        retMod.result = proBll.Insert(product).ToString();
                        retMod.retcode = M_APIResult.Success;
                    }
                    break;
                case "edit":
                    {
                        string product_json = Req("model");
                        M_Product proMod = JsonConvert.DeserializeObject<M_Product>(product_json);
                        if (proMod.UserID != mu.UserID) { retMod.retmsg = ""; }
                        else
                        {
                            M_Product proMod2 = proBll.GetproductByid(proMod.ID);
                            //限定只能修改以下参数，如有需要可在bll扩展一个新方法
                            proMod2.Proname = proMod.Proname;
                            proMod2.ShiPrice = proMod.ShiPrice;
                            proMod2.LinPrice = proMod.LinPrice;
                            proMod2.Kayword = proMod.Kayword;
                            proMod2.Proinfo = proMod.Proinfo;
                            proMod2.Procontent = proMod.Procontent;
                            proMod2.Procontent = proMod.Clearimg;
                            proBll.updateinfo(proMod2);
                            retMod.retcode = M_APIResult.Success;
                        }

                    }
                    break;
                case "del":
                    {
                        string ids = Req("ids");
                        string items = Req("items");
                        if (proBll.RealDelByIDS(ids, items)) { retMod.retcode = M_APIResult.Success; }
                    }
                    break;
                case "list":
                    {
                        int pageSize = DataConvert.CLng(Req("psize"));
                        int pageIndex = DataConvert.CLng(Req("cpage"));
                        DataTable dt = proBll.SelPage(pageSize, pageIndex);
                        retMod.result = JsonConvert.SerializeObject(dt);
                        retMod.retcode = M_APIResult.Success;
                    }
                    break;
                case "search":
                    {
                        int psize = DataConvert.CLng(Req("psize"));
                        int cpage = DataConvert.CLng(Req("cpage"));
                        int uid = Req("uid") == "" ? 0 : DataConvert.CLng(Req("uid"));
                        int nodeId = Req("nodeid") == "" ? 0 : DataConvert.CLng(Req("nodeid"));
                        string proname = Req("proname");
                        string addUser = Req("addUser");
                        DataTable dt = proBll.SelPage(psize, cpage, uid, proname, nodeId, addUser);
                        retMod.result = JsonConvert.SerializeObject(dt);
                        retMod.retcode = M_APIResult.Success;
                    }
                    break;
                default:
                    {
                        retMod.retmsg = "[" + Action + "]接口不存在";
                    }
                    break;
            }
        }
        catch (Exception ex) { retMod.retmsg = ex.Message; }
        RepToClient(retMod);
    }
    public bool IsReusable { get { return false; } }
}