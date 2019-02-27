namespace ZoomLaCMS.Design.ask.server
{
    using System;
    using System.Web;
    using System.Data;
    using System.Collections.Generic;
    using ZoomLa.BLL;
    using ZoomLa.BLL.API;
    using ZoomLa.BLL.Design;
    using ZoomLa.BLL.Helper;
    using ZoomLa.Model;
    using ZoomLa.Model.Design;
    using Newtonsoft.Json;
    public class answer : API_Base, IHttpHandler
    {
        B_User buser = new B_User();
        B_Design_Ask askBll = new B_Design_Ask();
        B_Design_Answer ansBll = new B_Design_Answer();
        B_Design_AnsDetail ansdeBll = new B_Design_AnsDetail();
        public void ProcessRequest(HttpContext context)
        {
            M_UserInfo mu = buser.GetLogin();
            retMod.retcode = M_APIResult.Failed;
            try
            {
                switch (Action)
                {
                    case "submit":
                        {
                            M_Design_Answer ansMod = new M_Design_Answer();
                            ansMod.AskID = Convert.ToInt32(Req("askid"));
                            //ansMod.Answer = Req("answer");//{qid:1,answer:'is answer'}
                            ansMod.Answer = Req("answer");
                            ansMod.UserID = mu.UserID;
                            ansMod.IP = IPScaner.GetUserIP();
                            ansMod.Source = DeviceHelper.GetBrower().ToString();
                            ansMod.ID = ansBll.Insert(ansMod);
                            //-----------单独写入表中,便于后期分析(后期优化为批量插入)
                            List<M_SubOption> ansList = JsonConvert.DeserializeObject<List<M_SubOption>>(Req("answer"));
                            foreach (M_SubOption ans in ansList)
                            {
                                M_Design_AnsDetail ansdeMod = new M_Design_AnsDetail();
                                ansdeMod.AskID = ansMod.AskID;
                                ansdeMod.AnsID = ansMod.ID;
                                ansdeMod.Qid = ans.qid;
                                ansdeMod.Answer = ans.answer;
                                ansdeMod.UserID = ansMod.UserID;
                                ansdeBll.Insert(ansdeMod);
                            }
                            retMod.retcode = M_APIResult.Success;
                            retMod.result = ansMod.ID.ToString();
                        }
                        break;
                    case "list":
                        {
                            DataTable dt = ansBll.Sel(mu.UserID, -100);
                            retMod.retcode = M_APIResult.Success;
                            retMod.result = JsonConvert.SerializeObject(dt);
                        }
                        break;
                    case "listbyqid"://仅用于查看详情
                        {
                            int qid = Convert.ToInt32(Req("qid"));
                            DataTable dt = ansdeBll.Sel(qid.ToString());
                            retMod.retcode = M_APIResult.Success;
                            retMod.result = JsonConvert.SerializeObject(dt);
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
        public class M_SubOption
        {
            public int qid = 0;
            public string answer = "";
        }
    }
}