<%@ WebHandler Language="C#" Class="JSServe" %>

using System;
using System.Web;
using System.Data;
using ZoomLa.SQLDAL;
using ZoomLa.Common;
using System.Linq;

/*
 * JS方面后台支持,排序
 * 
 * 合法检测:
 * 1,通过Refer来检测是否合法
 * 2,必须为Ajax提交
 * 3,只允许部分表,并且在服务端设好主键与OrderID的关系
 */
public class JSServe : IHttpHandler {
    public string[] AllowTable = new string[] { "ZL_Pub,PubID,OrderID", "ZL_CommonModel,GeneralID,OrderID" };
    public void ProcessRequest (HttpContext context) {
        string action = context.Request.Form["action"];
        string value = context.Request.Form["value"];
        string info = context.Request.Form["info"];//附加信息,如表名等
        string sql = "";
        if (!function.isAjax()||!IsAllow(info)) { return; }//仅允许其支持AJAX
        switch (action)
        {
            //[{"id":"54","oid":"304"},{"id":"53","oid":"305"},{"id":"52","oid":"303"},{"id":"51","oid":"302"},{"id":"50","oid":"301"},{"id":"49","oid":"300"},{"id":"46","oid":"297"},{"id":"43","oid":"294"},{"id":"42","oid":"293"},{"id":"36","oid":"287"}]
            //case "TableOrder"://整张表提交,暂注释,需要时回复
            //    DataTable dt = JsonHelper.JsonToDT(value);
            //    string[] valArr = value.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            //    for (int i = 0; i < dt.Rows.Count; i++)
            //    {
            //        int id = Convert.ToInt32(dt.Rows[i]["id"]);
            //        int oid = Convert.ToInt32(dt.Rows[i]["oid"]);
            //        sql = "Update " + info + " Set OrderID=" + oid + " Where GeneralID=" + id;
            //        SqlHelper.ExecuteSql(sql);
            //    }
            //    break;
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }
    //True通过检测
    private bool IsAllow(string info) 
    {
        bool flag = false;
        foreach (string v in AllowTable)
        {
            if (v.Split(',')[0].Equals(info, StringComparison.CurrentCultureIgnoreCase)) { info=v.Split(',')[0]; flag = true; break; }
        }
        return flag;
    }
}