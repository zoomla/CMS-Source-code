using System;
using System.Web;
using System.Data;
using ZoomLa.BLL;
using ZoomLa.BLL.Plat;
using ZoomLa.BLL.API;
using ZoomLa.Model;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZoomLaCMS.Plat.Common
{
    /// <summary>
    /// Common 的摘要说明
    /// </summary>
    public class Common : API_Base, IHttpHandler
    {
        B_User_Plat upBll = new B_User_Plat();
        B_Blog_Msg msgBll = new B_Blog_Msg();
        //统一状态码  -1:失败,99:成功
        //以下都限制为只能获取用户所在的公司
        public void ProcessRequest(HttpContext context)
        {
            M_APIResult retMod = new M_APIResult();
            retMod.retcode = M_APIResult.Failed;
            M_User_Plat upMod = null;
            string value = context.Request.Form["value"];
            string result = "";
            switch (Action)
            {
                case "plat_compuser"://获取公司中成员,用于@功能
                    {
                        upMod = B_User_Plat.GetLogin();
                        DataTable dt = upBll.SelByCompWithAT(upMod.CompID);
                        result = JsonHelper.JsonSerialDataTable(dt);
                        OldRep(result);
                    }
                    break;
                case "getuinfo"://获取单个用户信息(只允许获取本公司),返回的信息存入Json,避免重复检测,后期将服务端也缓存化
                    {
                        upMod = B_User_Plat.GetLogin();
                        int uid = Convert.ToInt32(value);
                        M_User_Plat model = upBll.SelReturnModel(uid, upMod.CompID);
                        if (model != null)
                            result = "{\"id\":\"" + model.UserID + "\",\"UserID\":\"" + model.UserID + "\",\"UserName\":\"" + model.TrueName + "\",\"Mobile\":\"" + model.Mobile + "\",\"GroupName\":\"" + model.GroupName.Trim(',') + "\",\"UserFace\":\"" + model.UserFace + "\"}";
                        OldRep(result);
                    }
                    break;
                case "getnotify"://获取提醒
                    {
                        B_Notify notBll = new B_Notify();
                        if (B_Notify.NotifyList.Count < 1) { retMod.retmsg = "none"; }
                        else
                        {
                            notBll.RemoveExpire();//去除超时的
                            M_UserInfo mu = new B_User().GetLogin();
                            List<M_Notify> list = notBll.GetNotfiyByUid(mu.UserID);
                            DataTable retdt = new DataTable();
                            retdt.Columns.Add(new DataColumn("title", typeof(string)));
                            retdt.Columns.Add(new DataColumn("content", typeof(string)));
                            retdt.Columns.Add(new DataColumn("cuname", typeof(string)));
                            if (list.Count > 0)
                            {
                                foreach (M_Notify model in list)//有多个就发多条
                                {
                                    notBll.AddReader(model, mu.UserID);
                                    DataRow dr = retdt.NewRow();
                                    dr["title"] = model.Title;
                                    dr["content"] = model.Content;
                                    dr["cuname"] = model.CUName;
                                    retdt.Rows.Add(dr);
                                }
                            }
                            retMod.retcode = M_APIResult.Success;
                            retMod.result = JsonConvert.SerializeObject(retdt);
                        }
                        RepToClient(retMod);
                    }
                    break;
                case "newblog"://自己公司有无新的信息
                    {
                        upMod = B_User_Plat.GetLogin();
                        result = msgBll.SelByDateForNotify(Req("date"), upMod).ToString();
                        OldRep(result);
                    }
                    break;
                case "privatesend"://私信功能,走邮件模块
                    {
                        upMod = B_User_Plat.GetLogin();
                        if (upMod != null)
                        {
                            string msg = context.Request.Form["msg"];
                            string receuser = context.Request.Form["receuser"];
                            if (!string.IsNullOrWhiteSpace(msg) && !string.IsNullOrWhiteSpace(receuser) && SafeSC.CheckIDS(receuser))
                            {
                                //过滤非用户公司的同事,后期处理
                                M_Message msgMod = new M_Message();
                                B_Message msgBll = new B_Message();
                                msgMod.Incept = receuser;
                                msgMod.Sender = upMod.UserID.ToString();
                                msgMod.Title = upMod.TrueName + "的私信";
                                msgMod.PostDate = DateTime.Now;
                                msgMod.Content = msg;
                                msgMod.Savedata = 0;
                                msgMod.Receipt = "";
                                msgMod.CCUser = "";
                                msgMod.Attachment = "";
                                msgBll.GetInsert(msgMod);
                                result = "99";
                                //添加一条新提醒
                                B_Notify.AddNotify(upMod.UserName, "你收到一封私信", msgMod.Title, msgMod.Incept);
                            }
                            else result = "-1";
                        }
                        else { result = "0"; }//未登录
                        OldRep(result);
                        break;
                    }
                case "addread"://阅读量统计
                    {
                        string ids = context.Request.Form["ids"];
                        msgBll.AddRead(ids.Trim(','));
                        OldRep("1");
                    }
                    break;
            }
        }
        //兼容
        private void OldRep(string result)
        {
            HttpResponse Response = HttpContext.Current.Response;
            Response.Clear();
            Response.Write(result); Response.Flush(); Response.End();
        }
        public bool IsReusable { get { return false; } }

    }
}