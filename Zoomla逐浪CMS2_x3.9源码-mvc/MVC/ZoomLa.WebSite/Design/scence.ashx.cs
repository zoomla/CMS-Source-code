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

namespace ZoomLaCMS.Design
{
    /// <summary>
    /// scence 的摘要说明
    /// </summary>
    public class scence : API_Base, IHttpHandler
    {

        B_User buser = new B_User();
        B_Design_Scence pageBll = new B_Design_Scence();
        B_Design_SiteInfo sfBll = new B_Design_SiteInfo();
        B_Design_Tlp tlpBll = new B_Design_Tlp();
        public void ProcessRequest(HttpContext context)
        {
            //throw new Exception("该接口默认不开放,请联系管理员");
            M_UserInfo mu = buser.GetLogin();
            retMod.retcode = M_APIResult.Failed;
            //if (mu.IsNull) { retMod.retmsg = "用户未登录"; RepToClient(retMod); }
            //retMod.callback = CallBack;//暂不开放JsonP
            int siteID = DataConvert.CLng(Req("SiteID"));
            try
            {
                switch (Action)
                {
                    case "save"://保存或更新
                        {
                            M_Design_Page model = JsonConvert.DeserializeObject<M_Design_Page>(Req("model"));
                            M_Design_Page pageMod = pageBll.SelModelByGuid(model.guid);
                            pageMod.comp = model.comp;
                            pageMod.page = model.page;
                            pageMod.scence = model.scence;
                            pageMod.labelArr = model.labelArr;
                            pageMod.UPDate = DateTime.Now;
                            pageBll.UpdateByID(pageMod);
                            retMod.result = model.guid;
                            retMod.retcode = M_APIResult.Success;
                        }
                        break;
                    case "loadtlp"://只取第一页的数据
                        {
                            int tlpid = DataConvert.CLng(Req("tlpid"));
                            M_Design_Tlp tlpMod = tlpBll.SelReturnModel(tlpid);
                            if (tlpMod == null) { retMod.retmsg = "模板不存在"; }
                            else if (tlpMod.ZType != 1) { retMod.retmsg = "并非有效的场景模板"; }
                            else
                            {
                                M_Design_Page pageMod = pageBll.SelModelByTlp(tlpMod.ID);
                                JObject jobj = new JObject();
                                jobj.Add("page", pageMod.page);
                                jobj.Add("comp", pageMod.comp);
                                retMod.result = JsonConvert.SerializeObject(jobj);
                                retMod.retcode = M_APIResult.Success;
                            }
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
        public bool IsReusable
        {
            get { return false; }
        }
    }
}