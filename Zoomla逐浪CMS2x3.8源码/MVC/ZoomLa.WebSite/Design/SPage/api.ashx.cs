using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZoomLa.BLL;
using ZoomLa.BLL.API;
using ZoomLa.Common;

namespace ZoomLaCMS.Design.SPage
{
    public class api :API_Base,IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            retMod.retcode = M_APIResult.Failed;
            //retMod.callback = CallBack;//暂不开放JsonP
            try
            {
                switch (Action)
                {
                    case "base64":
                        {
                            string dslabel = Req("dslabel");
                            string base64 = Req("base64");
                            string html = EncryptHelper.Base64Decrypt(dslabel) + HttpUtility.HtmlDecode(EncryptHelper.Base64Decrypt(base64));
                            //使用来源地址,否则无法获取页面传参
                            //ZLLog.L(context.Request.UrlReferrer.PathAndQuery+"|"+context.Request.UrlReferrer.AbsoluteUri);
                            //context.Request.RawUrl+"|"+context.Request.Url
                            string rawurl = context.Request.UrlReferrer.PathAndQuery;
                            string url = context.Request.UrlReferrer.AbsoluteUri;
                            html = new B_CreateHtml(rawurl, url).CreateHtml(html, DataConverter.CLng(Req("cpage")), DataConverter.CLng(Req("itemid")));
                            HttpResponse Response = context.Response;
                            Response.Clear(); Response.Write(html); Response.Flush(); Response.End();
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

        public bool IsReusable
        {
            get{return false;}
        }
    }
}