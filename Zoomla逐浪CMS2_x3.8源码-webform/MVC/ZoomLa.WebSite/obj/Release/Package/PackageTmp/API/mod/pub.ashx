<%@ WebHandler Language="C#" Class="pub" %>

using System;
using System.Web;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ZoomLa.BLL;
using ZoomLa.BLL.API;
using ZoomLa.BLL.User;
using ZoomLa.BLL.Helper;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Model.User;
using ZoomLa.SQLDAL;
using Newtonsoft.Json;
/*
 * 互动模块接口,以DataTable接收存储和修改
 */
public class pub : API_Base, System.Web.SessionState.IReadOnlySessionState, IHttpHandler
{
    B_User buser = new B_User();
    //$.post("/API/Mod/Pub.ashx?action=add&pubid=1&id=1",{data:json},function(data){});
    //-----
    int PubID { get { return DataConvert.CLng(Req("pubid")); } }
    int Mid { get { return DataConvert.CLng(Req("id")); } }
    string Mdata { get { return "[" + Req("data") + "]"; } }//添加与修改时传递的数据,需要转DataTable,所以外置[]
    B_Pub pubBll = new B_Pub();
    M_Pub pubMod = new M_Pub();
    M_APIResult retMod = new M_APIResult();
    public void ProcessRequest(HttpContext context)
    {
        //function.WriteErrMsg("接口默认关闭,请联系管理员开启");
        M_UserInfo mu = buser.GetLogin();
        retMod.retcode = M_APIResult.Failed;
        //retMod.callback = CallBack;//暂不开放JsonP
        pubMod = pubBll.SelReturnModel(PubID);
        if (pubMod == null) { retMod.retmsg = "[" + PubID + "]互动模块不存在"; RepToClient(retMod); }
        try
        {
            switch (Action)
            {
                case "add":
                    {
                        DataTable dt = JsonConvert.DeserializeObject<DataTable>(Mdata);
                        ForDataColumn(pubMod,dt);
                        DataRow dr = dt.Rows[0];
                        retMod.result = DBCenter.Insert(pubMod.PubTableName, BLLCommon.GetFields(dr), BLLCommon.GetParas(dr), BLLCommon.GetParameters(dr)).ToString();
                        retMod.retcode = M_APIResult.Success;
                    }
                    break;
                case "edit":
                    {
                        //修改对用户ID校验
                        DataTable dt = JsonConvert.DeserializeObject<DataTable>(Mdata);
                        ForDataColumn(pubMod, dt);
                        DataRow dr = dt.Rows[0];
                        List<SqlParameter> splist = new List<SqlParameter>();
                        splist.AddRange(BLLCommon.GetParameters(dr));
                        DBCenter.UpdateSQL(pubMod.PubTableName, BLLCommon.GetFieldAndPara(dr), "ID=" + Mid + " AND PubUserID=" + mu.UserID, splist);
                        retMod.retcode = M_APIResult.Success;
                    }
                    break;
                case "del":
                    {
                        DBCenter.DelByWhere(pubMod.PubTableName, "ID=" + Mid + " AND PubUserID=" + mu.UserID);
                        retMod.retcode = M_APIResult.Success;
                    }
                    break;
                case "list":
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
    private void ForDataColumn(M_Pub pubMod,DataTable dt)
    {
        M_UserInfo mu = buser.GetLogin();
        string[] fields = "PubAddTime,PubUserName,PubUserID,PubIP,Pubupid,Pubstart".Split(',');
        foreach (string field in fields)
        {
            if (!dt.Columns.Contains(field)) { dt.Columns.Add(new DataColumn(field, typeof(string))); }
        }
        DataRow dr = dt.Rows[0];
        dr["PubAddTime"] = DateTime.Now;
        dr["PubUserName"] = mu.UserName;
        dr["PubUserID"] = mu.UserID;
        dr["PubIP"] = IPScaner.GetUserIP();
        dr["Pubupid"] = PubID;
        dr["Pubstart"] = pubMod.PubIsTrue == 1 ? 0 : 1;//是否审核
    }
    public bool IsReusable { get { return false; } }
}