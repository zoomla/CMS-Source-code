using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using ZoomLa.BLL.API;
using ZoomLa.BLL;
using ZoomLa.BLL.Plat;
using ZoomLa.Model;

namespace ZoomLaCMS.Plat.EMail
{
    /// <summary>
    /// email 的摘要说明
    /// </summary>
    public class email : API_Base, IHttpHandler
    {
        B_Plat_Mail mailBll = new B_Plat_Mail();
        B_Plat_MailConfig configBll = new B_Plat_MailConfig();
        B_User buser = new B_User();
        public void ProcessRequest(HttpContext context)
        {
            M_UserInfo mu = buser.GetLogin();
            retMod.retcode = M_APIResult.Failed;
            //retMod.callback = CallBack;//暂不开放JsonP
            try
            {
                switch (Action)
                {
                    case "del":
                        string ids = Req("ids");
                        mailBll.DelByUid(ids, mu.UserID);
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
}