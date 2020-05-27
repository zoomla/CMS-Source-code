using System;
using System.Web;
using System.Data;
using ZoomLa.BLL;
using ZoomLa.BLL.API;
using ZoomLa.BLL.Design;
using ZoomLa.Model;
using ZoomLa.Model.Design;
using Newtonsoft.Json;

namespace ZoomLaCMS.Design.server
{
    /// <summary>
    /// ask 的摘要说明
    /// </summary>
    public class ask : API_Base, IHttpHandler
    {

        B_User buser = new B_User();
        B_Design_Ask askBll = new B_Design_Ask();
        public void ProcessRequest(HttpContext context)
        {
            M_UserInfo mu = buser.GetLogin();
            retMod.retcode = M_APIResult.Failed;
            try
            {
                switch (Action)
                {
                    case "get":
                        {
                            int id = Convert.ToInt32(Req("id"));
                            M_Design_Ask model = askBll.SelReturnModel(id);
                            retMod.result = JsonConvert.SerializeObject(model);
                            retMod.retcode = M_APIResult.Success;
                        }
                        break;
                    case "add"://添加或修改
                        {
                            M_Design_Ask model = JsonConvert.DeserializeObject<M_Design_Ask>(Req("model"));
                            if (model.ID > 0)
                            {
                                M_Design_Ask askMod = askBll.SelReturnModel(model.ID);
                                if (askMod.CUser != mu.UserID) { retMod.retmsg = "你无权修改该问题"; RepToClient(retMod); }
                                askMod.Title = model.Title;
                                askMod.Remind = model.Remind;
                                askMod.PreViewImg = model.PreViewImg;
                                askMod.EndDate = model.EndDate;
                                askBll.UpdateByID(askMod);
                            }
                            else
                            {
                                model.CUser = mu.UserID;
                                model.ID = askBll.Insert(model);
                            }
                            retMod.retcode = M_APIResult.Success;
                            retMod.result = model.ID.ToString();
                        }
                        break;
                    case "list":
                        {
                            DataTable dt = askBll.Sel(mu.UserID);
                            retMod.retcode = M_APIResult.Success;
                            retMod.result = JsonConvert.SerializeObject(dt);
                        }
                        break;
                    case "del":
                        {
                            int id = Convert.ToInt32(Req("id"));
                            askBll.U_Del(mu.UserID, id);
                            retMod.retcode = M_APIResult.Success;
                            retMod.result = "true";
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
}