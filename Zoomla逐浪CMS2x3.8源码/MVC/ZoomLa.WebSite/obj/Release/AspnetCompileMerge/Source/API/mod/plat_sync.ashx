<%@ WebHandler Language="C#" Class="plat_sync" %>

using System;
using System.IO;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Web;
using ZoomLa.Model;
using ZoomLa.Model.Plat;
using ZoomLa.BLL;
using ZoomLa.BLL.API;
using ZoomLa.BLL.Plat;
using ZoomLa.BLL.Helper;
using ZoomLa.Common;
using ZoomLa.SQLDAL;
using ZoomLa.PdoApi.SinaWeiBo;
//用户必须先绑定好用户,才可使用该功能,否则忽略
//同步内容至第三方平台
public class plat_sync : API_Base, IHttpHandler
{
    //支持以,切割  sina,qqblog
    public string SyncPlat { get { return Req("plat"); } }
    B_User buser = new B_User();
    B_User_Token tokenBll = new B_User_Token();
    public void ProcessRequest(HttpContext context)
    {
        M_UserInfo mu = buser.GetLogin();
        retMod.retcode = M_APIResult.Failed;
        if (mu.IsNull) { retMod.retmsg = "请登录后再操作"; RepToClient(retMod); }
        //retMod.callback = CallBack;//暂不开放JsonP
        try
        {
            switch (Action)
            {
                case "add"://发布一条分享
                    M_User_Token tokenMod = tokenBll.SelModelByUid(mu.UserID);
                    string content = StringHelper.SubStr(Req("content"), 140);
                    string picurl = Req("pic");
                    if (!string.IsNullOrEmpty(picurl)) { picurl = function.VToP(picurl); }
                    if (tokenMod == null) { retMod.retmsg = "未绑定账号"; }
                    else if (string.IsNullOrEmpty(SyncPlat)) { retMod.retmsg = "未指定发送平台"; }
                    else if (string.IsNullOrEmpty(content) && string.IsNullOrEmpty(picurl)) { retMod.retmsg = "内容不能为空"; }
                    else
                    {
                        string qqret = "", sinaret = "";
                        if (SyncPlat.Contains("sina"))
                        {
                            if (string.IsNullOrEmpty(tokenMod.SinaToken)) { sinaret = "未绑定新浪账号"; }
                            else
                            {
                                SinaHelper sinaBll = new SinaHelper(tokenMod.SinaToken);
                                sinaret = sinaBll.PostStatus(content, picurl);
                            }
                        }
                        if (SyncPlat.Contains("qqblog"))
                        {
                            if (string.IsNullOrEmpty(tokenMod.QQToken)) { qqret = "未绑定QQ帐号"; }
                            else
                            {
                                QQHelper qq = new QQHelper(tokenMod.QQToken, tokenMod.QQOpenID);
                                if (string.IsNullOrEmpty(picurl)) { qqret = qq.AddBlog(content); }
                                else { qqret = qq.AddBlog(content, picurl); }
                            }
                        }
                        //新浪Success,腾迅:{"data":{"id":"493362015379761","time":1461145881},"errcode":0,"imgurl":"http:\/\/t2.qpic.cn\/mblogpic\/d0dc687c6f72d4ab0548","msg":"ok","ret":0,"seqid":6275573769292488611}
                        retMod.retcode = M_APIResult.Success;
                        retMod.retmsg = "新浪" + sinaret + ",腾迅:" + qqret;
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
}