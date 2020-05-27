using Newtonsoft.Json;
using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZoomLa.BLL;
using ZoomLa.BLL.API;
using ZoomLa.BLL.Helper;
using ZoomLa.Model;

namespace ZoomLa.Ajax.WebAPI
{
    /*
     * 用户相关API
     */
    public class UserController : ApiController
    {
        M_APIResult result = new M_APIResult();
        //M_Ucenter ucMod = null;
        //B_User buser = new B_User();
        //private string Action { get { return HttpContext.Current.Request["Action"]; } }
        //private string Key { get { return HttpContext.Current.Request["Key"]; } }
        //private int PSize { get { return DataConvert.CLng(HttpContext.Current.Request["psize"]); } }
        //private int CPage { get { return DataConvert.CLng(HttpContext.Current.Request["cpage"]); } }
        //private string fields { get { return HttpContext.Current.Request["fields"] ?? ""; } }
        ////如有传值,则为JsonP
        //private 

        //// POST api/<controller>
        //public void Post([FromBody]string value)
        //{
        //}
        // POST api/<controller>/id
        //public string test(M_UserInfo mu) { return mu.UserName; }

        //public string test22(JObject obj) { return Request.Headers.GetValues("Authorization").First(); }
        [HttpGet]
        public string Login(string uname, string upwd, string callback)
        {
            //Request.GetQueryNameValuePairs().FirstOrDefault(item => item.Key == "callback").Value;
            //HttpContext.Current.Request.QueryString["callback"];
            //result.retcode = -1;
            //result.callback = callback;
            //try
            //{
            //    if (StrHelper.StrNullCheck(uname, upwd)) { return ""; }
            //    //M_UserInfo mu = apiBll.Login(uname, upwd);
            //    //if (mu.IsNull) return "";
            //    //M_AJAXUser ajaxMod = new M_AJAXUser(mu);
            //    //result.result = ajaxMod.ToJson();
            //    return result.ToString();
            //}
            //catch (Exception ex) { result.retmsg = ex.Message; return result.ToString(); }
            return "";
        }
    }
}