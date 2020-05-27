<%@ WebHandler Language="C#" Class="Server" %>

using System;
using System.Web;
using ZoomLa.Common;
using System.Collections.Generic;
using System.Linq;
/*
 * 接受客户端信息,同步给用户
 */ 
public class Server : IHttpHandler {
    
       public void ProcessRequest (HttpContext context) {
            string action = context.Request["action"].Trim();
            string value = context.Request["value"].Trim();
            string result = "",json = "[",name="";
            switch (action)
            {
                //case "Login"://添加一个新用户
                //    //用户名不存在时才写入
                //    if (Call.PlayerList.Count < 1 || Call.PlayerList.Where(p => p.PlayerName.Equals(value)).ToArray().Length<1)
                //    {
                //        Player p1 = new Player();
                //        p1.PlayerName = value;
                //        Call.PlayerList.Add(p1);
                //    }
                //    result = "1";
                //    break;
                //case "SyncSpirt"://同步所有玩家信息
                //    if (Call.PlayerList.Count < 2) break;
                //    for (int i = 0; i < Call.PlayerList.Count; i++)
                //    {
                //        Player p = Call.PlayerList[i];
                //        if (p.PlayerName.Equals(value)) continue;
                //        json += "{name:\"" + p.PlayerName + "\",X:\"" + p.XPos + "\",Y:\"" +p.YPos + "\"},";//x与y是目标的x与y置
                //    }
                //    json = json.TrimEnd(',') + "]";
                //    result = json;
                //    break;
                //case "SendPos":
                //    name = value.Split(',')[0];
                //    int x = Convert.ToInt32(value.Split(',')[1]);
                //    int y = Convert.ToInt32(value.Split(',')[2]);
                //    var c3= Call.PlayerList.First(p=>p.PlayerName==name);
                //    c3.XPos = x;
                //    c3.YPos = y;
                //    break;
                //case "SendInfo":
                //    name = value.Split(',')[0];
                //    string chatInfo = value.Split(',')[1] + "\r\n";
                //    //每个玩家自己知道其他所有玩家的事件,不维护自己的
                //    var ie3 = Call.PlayerList.Where(p => !p.PlayerName.Equals(name));
                //    foreach (var i in ie3)
                //    {
                //        i.AddAction(name, chatInfo);
                //    }
                //    break; 
                //case "GetInfo":
                //    var pp = Call.PlayerList.First(p => p.PlayerName.Equals(value));//其他用户的事件
                //    result = pp.PopAction();
                //    break;
                //default:
                //    break;
            }
            context.Response.Clear(); 
            context.Response.Write(result); 
            context.Response.End();
    }
    public bool IsReusable {
        get {
            return false;
        }
    }
}
