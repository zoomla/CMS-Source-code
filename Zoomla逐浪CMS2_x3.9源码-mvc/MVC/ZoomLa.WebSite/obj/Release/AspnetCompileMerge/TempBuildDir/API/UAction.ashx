<%@ WebHandler Language="C#" Class="UAction" %>

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL.Auth;
using ZoomLa.BLL.Chat;
using ZoomLa.Model.Chat;

public class UAction : System.Web.SessionState.IReadOnlySessionState, IHttpHandler
{
    B_UAction uaBll = new B_UAction();
    private string Action { get { return HttpContext.Current.Request["Action"] ?? ""; } }
    public void ProcessRequest (HttpContext context) {
        string result = "";
        switch (Action)
        {
            case "add"://添加行为记录进入数据库
                {
                    List<M_UAction> list = JsonConvert.DeserializeObject<List<M_UAction>>(HttpContext.Current.Request.Form["data"], new JsonSerializerSettings()
                    {
                        NullValueHandling = NullValueHandling.Ignore
                    });
                    if (list.Count > 0)
                    {
                        uaBll.Insert(list);
                    }
                }
                break;
            case "event"://根据cookie标志,返回队列中的事件
                {
                    string idflag = HttpContext.Current.Request["idflag"];
                    if (string.IsNullOrEmpty(idflag)) return;
                    List<M_ZLEvent> eventList = ZLEvent.SelByFlag(ZLEvent.EventT.UAction, idflag);
                    if (eventList.Count < 1) { return; }
                    foreach (M_ZLEvent model in eventList)
                    {
                        result += model.Value + ",";
                    }
                    result = result.TrimEnd(',');
                }
                break;
            default:
                break;
        }
        HttpContext.Current.Response.Write(result); HttpContext.Current.Response.Flush(); HttpContext.Current.Response.End();
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}