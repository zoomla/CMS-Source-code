using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using ZoomLa.Common;
using System.Xml;
using ZoomLa.BLL;
using ZoomLa.Model;
using Newtonsoft.Json;
using System.Web.Security;
using ZoomLa.Components;
using ZoomLa.Model.Plat;
using ZoomLa.BLL.Plat;
using ZoomLa.BLL.Other;

public partial class API_wxpage : System.Web.UI.Page
{
    B_WX_User wxuserBll = new B_WX_User();
    B_WX_ReplyMsg rpBll = new B_WX_ReplyMsg();
    B_WX_APPID appBll = new B_WX_APPID();
    B_User buser = null;
    WxAPI api = null;
    string baseUrl = SiteConfig.SiteInfo.SiteUrl;
    //-----------------用于debug
    string errmsg = "";
    string requesdata = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        string echoString = HttpContext.Current.Request.QueryString["echoStr"];
        string signature = HttpContext.Current.Request.QueryString["signature"];
        string timestamp = HttpContext.Current.Request.QueryString["timestamp"];
        string nonce = HttpContext.Current.Request.QueryString["nonce"];
        if (Request.HttpMethod == "GET") { Auth(); return; }
        try
        {
            buser = new B_User(HttpContext.Current);
            requesdata = GetXml();
            //requesdata = "<xml><ToUserName><![CDATA[gh_33273dafc0e4]]></ToUserName> <FromUserName><![CDATA[olwfpsvje_OHogJ8rOANahcqSijk]]></FromUserName> <CreateTime>1434081760</CreateTime> <MsgType><![CDATA[text]]></MsgType> <Content><![CDATA[pic]]></Content> <MsgId>6159334259197323209</MsgId> </xml>";
            if (string.IsNullOrEmpty(requesdata)) { return; }
            M_WxTextMsg reqMod = new M_WxTextMsg(requesdata);
            //获取需要返回的公众号
            M_WX_APPID appmod = appBll.GetAppByWxNo(reqMod.ToUserName);
            if (appmod == null) { throw new Exception("目标公众号[" + reqMod.ToUserName + "]不存在"); }
            api = WxAPI.Code_Get(appmod);
            errmsg += "动作:" + reqMod.MsgType;
            switch (reqMod.MsgType)
            {
                case "event"://事件--关注处理,后期扩展单击等
                    {
                        //M_WxImgMsg msgMod = JsonConvert.DeserializeObject<M_WxImgMsg>(appmod.WelStr);
                        M_WxImgMsg msgMod = new M_WxImgMsg();
                        msgMod.ToUserName = reqMod.FromUserName;
                        msgMod.FromUserName = reqMod.ToUserName;
                        WxEventHandler(reqMod);//系统事件处理
                        //登录检测,可按需取消或修改位置
                        M_UserInfo mu = UserBindCheck(reqMod);
                        //if (mu.IsNull)
                        //{
                        //    msgMod.Articles.Add(new M_WXImgItem()
                        //    {
                        //        Title = "请先关联用户",
                        //        Description = "你尚未关联用户,点击登录关联用户",
                        //        Url = baseUrl + "/User/Login.aspx?WXOpenID=" + reqMod.FromUserName
                        //    });
                        //    RepToClient(msgMod.ToXML());
                        //}
                        WxMenuBtnHandler(reqMod, msgMod, mu);
                    }
                    break;
                case "text"://接收文本消息
                    {
                        string xml = UserTextDeal(reqMod);
                        RepToClient(xml);
                    }
                    break;
            }
        }
        catch (Exception ex) { ZLLog.L("微信报错," + errmsg + ",数据:" + requesdata + ",原因:" + ex.Message); }
    }
    private void RepToClient(string xml)
    {
        //自返回,用于避免线程中断报错
        Response.Clear();
        Response.Write(xml);
        Response.Flush();
        //Response.End();
    }
    //检测用户是否关联用户,未关联用户则直接返回,否则返回用户信息
    //帐户在所有公众号通用
    public M_UserInfo UserBindCheck(M_WxTextMsg reqMod)
    {
        B_User_Token tokenBll = new B_User_Token();
        M_User_Token tokenMod = tokenBll.SelByOpenID(reqMod.FromUserName, "WX");
        M_UserInfo mu = new M_UserInfo(true);
        if (tokenMod == null)
        {

        }
        else { mu = buser.SelReturnModel(tokenMod.uid); }
        return mu;
    }
    /// <summary>
    /// 系统事件,如订阅等处理
    /// </summary>
    public void WxEventHandler(M_WxTextMsg reqMod)
    {
        M_WX_User userMod = null;
        errmsg += ",事件:" + reqMod.Event;
        switch (reqMod.Event.ToLower())
        {
            case "subscribe":
                userMod = api.GetWxUserModel(reqMod.FromUserName);
                userMod.CDate = DateTime.Now;
                userMod.AppId = api.AppId.ID;
                wxuserBll.Insert(userMod);
                M_WxImgMsg msgmod = JsonConvert.DeserializeObject<M_WxImgMsg>(api.AppId.WelStr);
                msgmod.ToUserName = reqMod.FromUserName;
                msgmod.FromUserName = reqMod.ToUserName;
                if (string.IsNullOrEmpty(msgmod.Articles[0].PicUrl)) //如未设置图片则以纯文本格式发送,纯文本支持内置超链接
                {
                    M_WxTextMsg textMod = ImageToText(msgmod);
                    RepToClient(textMod.ToXML());
                }
                else
                {
                    RepToClient(msgmod.ToXML());
                }
                //关注时发送多条信息
                //System.Threading.ThreadPool.QueueUserWorkItem(new System.Threading.WaitCallback(SendMsg), reqMod);
                break;
            case "unsubscribe":
                wxuserBll.DelByOpenid(api.AppId.ID, reqMod.FromUserName);
                break;
            case "location"://用户进入公众号(包含定位信息)
                break;
            default:
                break;
        }
    }
    //private void SendMsg(Object info)
    //{
    //    try
    //    {
    //        M_WxTextMsg reqMod = (M_WxTextMsg)info;
    //        System.Threading.Thread.Sleep(1000);//延迟1秒,避免先于欢迎消息
    //        M_WX_APPID appmod = new B_WX_APPID().GetAppByWxNo(reqMod.ToUserName);
    //        if (appmod == null) { throw new Exception("目标公众号[" + reqMod.ToUserName + "]不存在"); }
    //        WxAPI.Code_Get(appmod).SendMsg(reqMod.FromUserName, "test");
    //    }
    //    catch (Exception ex) { ZLLog.L("微信多信息出错,原因:" + ex.Message); }
    //}
    /// <summary>
    /// 自定义单击事件处理,按需扩展
    /// </summary>
    public void WxMenuBtnHandler(M_WxTextMsg reqMod, M_WxImgMsg msgMod, M_UserInfo mu)
    {
        //switch (reqMod.Event)
        //{
        //    case "CLICK":
        //        break;
        //}
        //<xml><ToUserName><![CDATA[gh_02d7c9c8ec54]]></ToUserName>
        //<FromUserName><![CDATA[obaF_jigkjUCPDpFeCMx25TV7qQk]]></FromUserName>
        //<CreateTime>1450326669</CreateTime>
        //<MsgType><![CDATA[event]]></MsgType>
        //<Event><![CDATA[CLICK]]></Event>
        //<EventKey><![CDATA[menu_0_btn_0]]></EventKey>
        //</xml>
        switch (reqMod.EventKey)
        {
            case "menu_0_btn_0"://项目进度
                {
                    msgMod.Articles.Add(new M_WXImgItem()
                    {
                        Title = mu.UserName + "[项目进度]的信息1",
                        Description = "点击访问[项目进度]信息",
                        //PicUrl=baseUrl+"/Template/Ke/style/images/login1.png",
                        Url = baseUrl + "/User/Default.aspx"
                    });
                }
                break;
            case "menu_0_btn_1":
                {
                    M_WxTextMsg textMod = new M_WxTextMsg()
                    {
                        MsgType = "text",
                        CreateTime = reqMod.CreateTime,
                        Content = "点击访问[<a href='" + baseUrl + "/User/Default.aspx'>发布项目</a>]信息",
                        FromUserName = msgMod.FromUserName,
                        ToUserName = msgMod.ToUserName
                    };
                    RepToClient(textMod.ToXML());
                }
                break;
        }
        RepToClient(msgMod.ToXML());
    }
    //用户文件信息处理
    public string UserTextDeal(M_WxTextMsg reqMod)
    {
        M_WX_ReplyMsg rpMod = rpBll.SelByFileAndDef(appBll.GetAppByWxNo(reqMod.ToUserName).ID, reqMod.Content);
        M_WxImgMsg msgMod = new M_WxImgMsg();
        msgMod.ToUserName = reqMod.FromUserName;
        msgMod.FromUserName = reqMod.ToUserName;
        //如果未匹配到则直接返回
        string result = "";
        if (rpMod == null)
        {
            msgMod.Articles.Add(new M_WXImgItem() { Title = "未找到相关联的操作" });
            result = msgMod.ToXML();
        }
        else//如果匹配到
        {
            M_WXImgItem item = JsonConvert.DeserializeObject<M_WXImgItem>(rpMod.Content);
            msgMod.Articles.Add(item);
            switch (rpMod.MsgType)
            {
                case "0":
                    M_WxTextMsg textMod = ImageToText(msgMod);
                    result = textMod.ToXML();
                    break;
                case "1":
                    result = msgMod.ToXML();
                    break;
            }
        }
        return result;
    }
    //用于处理微信初次验证
    private void Auth()
    {
        string echoString = HttpContext.Current.Request.QueryString["echoStr"];
        string signature = HttpContext.Current.Request.QueryString["signature"];
        string timestamp = HttpContext.Current.Request.QueryString["timestamp"];
        string nonce = HttpContext.Current.Request.QueryString["nonce"];
        ZLLog.L(echoString + "|||" + signature + "|||" + timestamp + "|||" + nonce);
        Response.Clear();
        Response.Write(echoString); Response.Flush(); Response.End();
        //string token = "demo";
        //if (CheckSignature(token, signature, timestamp, nonce))
        //{
        //    if (!string.IsNullOrEmpty(echoString))
        //    {
        //        HttpContext.Current.Response.Write(echoString);
        //        HttpContext.Current.Response.End();
        //    }
        //}
    }
    //------------------Tools
    private string GetXml()
    {
        byte[] inputdata = new byte[Request.InputStream.Length];
        Request.InputStream.Read(inputdata, 0, inputdata.Length);
        return System.Text.Encoding.UTF8.GetString(inputdata);
    }
    // 验证微信签名
    private bool CheckSignature(string token, string signature, string timestamp, string nonce)
    {
        string[] ArrTmp = { token, timestamp, nonce };
        Array.Sort(ArrTmp);
        string tmpStr = string.Join("", ArrTmp);

        tmpStr = FormsAuthentication.HashPasswordForStoringInConfigFile(tmpStr, "SHA1");
        tmpStr = tmpStr.ToLower();

        if (tmpStr == signature)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    //图文消息转文本(存储时以图片消息为准)
    //文本的优点在于,可以内置超链接
    private M_WxTextMsg ImageToText(M_WxImgMsg msgmod)
    {
        M_WxTextMsg textMod = new M_WxTextMsg();
        textMod.ToUserName = msgmod.ToUserName;
        textMod.FromUserName = msgmod.FromUserName;
        textMod.Content = msgmod.Articles[0].Description;
        textMod.CreateTime = DateTime.Now.Millisecond;
        textMod.MsgType = "text";
        return textMod;
    }
}