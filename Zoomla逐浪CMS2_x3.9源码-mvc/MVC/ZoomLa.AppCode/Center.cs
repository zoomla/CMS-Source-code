using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using ZoomLa.SQLDAL;
using ZoomLa.Model;
using ZoomLa.BLL;
using ZoomLa.BLL.User;
using ZoomLa.Model.User;
using ZoomLa.Model.CreateJS;
using ZoomLa.BLL.CreateJS;
using Newtonsoft.Json;
using ZoomLa.BLL.Helper;

[WebService(Namespace = "Center")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class Center : System.Web.Services.WebService {
    M_Ucenter ucMod = null;
    B_Ucenter ucBll = new B_Ucenter();
    B_Ask askBll = new B_Ask();
    B_Temp tempBll = new B_Temp();
    private string connstr = "";
    public Center()
    {
        connstr = "Data Source=" + StrHelper.GetAttrByStr(SqlHelper.ConnectionString, "Data Source") + ";Initial Catalog=" + StrHelper.GetAttrByStr(SqlHelper.ConnectionString, "Initial Catalog") + ";User ID={0};Password={1}";
    }
    [WebMethod]
    public bool Insert(string key, string tbname, string fields, string param, SqlParameter[] sp = null)
    {
        CheckKey(key); if (ucMod == null) { return false; }
        string sql = "INSERT INTO " + tbname + " (" + fields + ") VALUES(" + param + ")";
        return SqlHelper.ExecuteNonQuery(connstr, CommandType.Text, sql, sp) > 0;
    }
    [WebMethod]
    public string Select(string key, string tbname, string fields, string where, string order = "", SqlParameter[] sp = null)
    {
        CheckKey(key); if (ucMod == null) { return "Key Error"; }
        string sql = "SELECT " + fields + " FROM " + tbname, result = "";
        if (!string.IsNullOrEmpty(where))
        {
            sql += " WHERE " + where;
        }
        if (!string.IsNullOrEmpty(order))
        {
            sql += " ORDER BY " + order;
        }
        DataTable dt = SqlHelper.ExecuteTable(connstr, CommandType.Text, sql, sp);
        if (dt != null && dt.Rows.Count > 0)
        { result = JsonConvert.SerializeObject(dt); }
        return result;
    }
    [WebMethod]
    public bool Update(string key, string tbname, string set, string where, SqlParameter[] sp = null)
    {
        CheckKey(key); if (ucMod == null) { return false; }
        string sql = "UPDATE " + tbname + " SET " + set + " WHERE " + where;
        return SqlHelper.ExecuteNonQuery(connstr, CommandType.Text, sql, sp) > 0;
    }
    [WebMethod]
    public bool Del(string key, string tbname, string where, SqlParameter[] sp = null)
    {
        M_Ucenter ucMod = CheckKey(key);if (ucMod == null) { return false; }
        string sql = "DELETE FROM " + tbname + " WHERE " + where;
        return SqlHelper.ExecuteNonQuery(connstr, CommandType.Text, sql, sp) > 0;
    }
    //------问答
    [WebMethod]
    public int AddAsk(string key, M_Ask askMod)
    {
        M_Ucenter ucMod = CheckKey(key); if (ucMod == null) { return 0; }
        return askBll.insert(askMod);
    }
    private M_Ucenter CheckKey(string key)//稍后增加5分钟一次的缓存
    {
        ucMod = ucBll.SelByKey(key);
        if (ucMod != null)
        {
            connstr = string.Format(connstr, ucMod.DBUName, ucMod.DBPwd);
        }
        return ucMod;
    }
    //--------------------其他检测
    [WebMethod]
    public bool APPCertCheck(string key) 
    {
        //用于APP授权检测
        M_APP_Auth authMod = new B_APP_Auth().SelModelByKey(key);
        return !(authMod == null);
    }
}
