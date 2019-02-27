<%@ WebHandler Language="C#" Class="collect" %>

using System;
using System.Web;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ZoomLa.BLL;
using ZoomLa.BLL.API;
using ZoomLa.BLL.User;
using ZoomLa.BLL.Helper;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Model.User;
using ZoomLa.SQLDAL;
using Newtonsoft.Json;
//用于支持用户--收藏

public class collect : API_Base, IHttpHandler
{
    B_Favorite favBll = new B_Favorite();
    B_User buser = new B_User();
    M_Favorite model = new M_Favorite();
    private int InfoID { get { return DataConvert.CLng(Req("InfoID")); } }
    private int FavType { get { return DataConvert.CLng(Req("type")); } }
    public void ProcessRequest(HttpContext context)
    {
        retMod.retcode = M_APIResult.Success;
        try
        {
            M_UserInfo mu = buser.GetLogin();
            if (mu.UserID <= 0) { retMod.retcode = M_APIResult.Failed;retMod.retmsg = "您还没有登录";RepToClient(retMod); return; }
            switch (Action)
            {
                case "add":
                    {
                        //检测重复
                        if (favBll.CheckFavoData(mu.UserID, FavType, InfoID))
                        {
                            retMod.retcode = M_APIResult.Failed;retMod.retmsg = "您已添加该收藏";
                            break;
                        }
                        model.Title = Req("title");
                        model.Owner = mu.UserID;
                        model.InfoID = InfoID;
                        model.FavUrl = Req("favurl");
                        model.FavoriType = FavType;
                        favBll.insert(model);
                    }
                    break;
                case "list":
                    {
                        DataTable dt = favBll.GetFavByUserID(mu.UserID);
                        retMod.result = JsonConvert.SerializeObject(dt);
                    }
                    break;
                case "del":
                    {
                        favBll.U_Del(mu.UserID,InfoID,FavType);
                    }
                    break;
                case "has"://检测是否已收藏
                    {
                        retMod.result = favBll.CheckFavoData(mu.UserID, FavType, InfoID).ToString();
                    }
                    break;
            }
        }
        catch (Exception ex) { retMod.retmsg = ex.Message; retMod.retcode = M_APIResult.Failed; }
        RepToClient(retMod);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}