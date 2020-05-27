using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Collections.Generic;
using ZoomLa.BLL;
using ZoomLa.BLL.API;
using ZoomLa.BLL.Design;
using ZoomLa.Model;
using ZoomLa.Model.Design;
using ZoomLa.Components;
using ZoomLa.SQLDAL;
using ZoomLa.SQLDAL.SQL;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ZoomLaCMS.Design.ppt
{
    /// <summary>
    /// ppt 的摘要说明
    /// </summary>
    public class ppt : API_Base, IHttpHandler
    {
        B_User buser = new B_User();
        B_Design_PPT pptBll = new B_Design_PPT();
        B_Design_Tlp tlpBll = new B_Design_Tlp();
        public void ProcessRequest(HttpContext context)
        {
            M_UserInfo mu = buser.GetLogin();
            retMod.retcode = M_APIResult.Failed;
            int siteID = DataConvert.CLng(Req("SiteID"));
            try
            {
                switch (Action)
                {
                    case "save"://保存或更新
                        {
                            M_Design_Page model = JsonConvert.DeserializeObject<M_Design_Page>(Req("model"));
                            M_Design_Page pageMod = pptBll.SelModelByGuid(model.guid);
                            pageMod.comp = model.comp;
                            pageMod.page = model.page;
                            pageMod.scence = model.scence;
                            pageMod.labelArr = model.labelArr;
                            pageMod.UPDate = DateTime.Now;
                            pptBll.UpdateByID(pageMod);
                            retMod.result = model.guid;
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
}