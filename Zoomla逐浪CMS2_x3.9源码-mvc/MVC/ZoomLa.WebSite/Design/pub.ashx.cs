using System;
using System.Data;
using System.Web;
using ZoomLa.Common;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Model.Design;
using ZoomLa.BLL.API;
using ZoomLa.BLL.Design;
using Newtonsoft.Json;

namespace ZoomLaCMS.Design
{
    /// <summary>
    /// pub 的摘要说明
    /// </summary>
    public class pub : API_Base, IHttpHandler
    {

        B_User buser = new B_User();
        B_Design_Pub pubBll = new B_Design_Pub();
        public void ProcessRequest(HttpContext context)
        {
            M_UserInfo mu = buser.GetLogin();
            retMod.retcode = M_APIResult.Failed;
            //retMod.callback = CallBack;//暂不开放JsonP
            try
            {
                switch (Action)
                {
                    case "add"://收集用户回复(每个用户只能一次)
                        {
                            UserAnswer answer = JsonConvert.DeserializeObject<UserAnswer>(Req("model"));
                            M_Design_Pub pubMod = new M_Design_Pub();
                            pubMod.H5ID = answer.guid;
                            pubMod.FormName = answer.fname;
                            pubMod.FormContent = answer.content;
                            if (!mu.IsNull)
                            {
                                pubMod.UserID = mu.UserID.ToString();
                                pubMod.UserName = mu.UserName.ToString();
                            }
                            pubBll.Insert(pubMod);
                            retMod.retcode = M_APIResult.Success;
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

        public class UserAnswer
        {
            public string guid = "";//场景ID
            public string fname = "";
            public string cdate = "";
            public string content = "";
        }

    }
}