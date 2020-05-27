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
    /// question 的摘要说明
    /// </summary>
    public class question : API_Base, IHttpHandler
    {
        B_User buser = new B_User();
        B_Design_Ask askBll = new B_Design_Ask();
        B_Design_Question questBll = new B_Design_Question();
        public void ProcessRequest(HttpContext context)
        {
            M_UserInfo mu = buser.GetLogin();
            retMod.retcode = M_APIResult.Failed;
            try
            {
                switch (Action)
                {
                    case "list":
                        {
                            int askid = Convert.ToInt32(Req("askid"));
                            DataTable dt = questBll.Sel(askid, "qlist");
                            retMod.retcode = M_APIResult.Success;
                            retMod.result = JsonConvert.SerializeObject(dt);
                        }
                        break;
                    case "get":
                        {
                            int id = Convert.ToInt32(Req("id"));
                            M_Design_Question questMod = questBll.U_SelModel(mu.UserID, id);
                            retMod.retcode = M_APIResult.Success;
                            retMod.result = JsonConvert.SerializeObject(questMod);
                        }
                        break;
                    case "add":
                        {
                            M_Design_Question model = JsonConvert.DeserializeObject<M_Design_Question>(Req("model"));
                            if (model.ID > 0)
                            {
                                //哪些选项不允许修改
                                M_Design_Question questMod = questBll.U_SelModel(mu.UserID, model.ID);
                                model.CUser = questMod.CUser;
                                model.CDate = questMod.CDate;
                                model.AskID = questMod.AskID;
                                questBll.UpdateByID(model);
                            }
                            else
                            {
                                model.CUser = mu.UserID;
                                model.ID = questBll.Insert(model);
                            }
                            retMod.retcode = M_APIResult.Success;
                            retMod.result = model.ID.ToString();
                        }
                        break;
                    case "del":
                        {
                            int id = Convert.ToInt32(Req("id"));
                            questBll.U_Del(mu.UserID, id);
                            retMod.retcode = M_APIResult.Success;
                            retMod.result = "success";
                        }
                        break;
                    case "move":
                        {
                            int from = Convert.ToInt32(Req("from"));
                            int target = Convert.ToInt32(Req("target"));
                            retMod.result = questBll.Move(from, target).ToString();
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
    }
}