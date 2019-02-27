using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Data;
using System.Text.RegularExpressions;
using ZoomLa.SQLDAL;

/*
 * 使用: string s = JsonHelper.JsonSerializer<M_Site_SiteList>(siteModel);
 * 
 * 
 */
/// <summary>
/// JsonHelper 的摘要说明
/// </summary>
public class JsonHelper
{
    public JsonHelper()
    { }
    /// <summary>
    /// JSON序列化,日期必须有赋值
    /// </summary>
    public static string JsonSerializer<T>(T t)
    {
        return JsonConvert.SerializeObject(t);
    }

    /// <summary>
    /// JSON反序列化
    /// </summary>
    public static T JsonDeserialize<T>(string jsonString)
    {
        return JsonConvert.DeserializeObject<T>(jsonString);
    }
    /// <summary>
    /// 将DataTable序列化为Json
    /// </summary>
    public static string JsonSerialDataTable(DataTable dt)
    {
        return JsonConvert.SerializeObject(dt);
    }
    /// <summary>
    /// Json转化为DataTable 格式:[{"id":"54","oid":"304"},{"id":"53","oid":"305"}]
    /// </summary>
    public static DataTable JsonToDT(string json)
    {
        return JsonConvert.DeserializeObject<DataTable>(json);
    }
    /// <summary>
    /// 将Json序列化的时间由/Date(1294499956278+0800)转为字符串
    /// </summary>
    private static string ConvertJsonDateToDateString(Match m)
    {
        string result = string.Empty;
        DateTime dt = new DateTime(1970, 1, 1);
        dt = dt.AddMilliseconds(long.Parse(m.Groups[1].Value));
        dt = dt.ToLocalTime();
        result = dt.ToString("yyyy-MM-dd HH:mm:ss");
        return result;
    }

    /// <summary>
    /// 将时间字符串转为Json时间
    /// </summary>
    private static string ConvertDateStringToJsonDate(Match m)
    {
        string result = string.Empty;
        DateTime dt = DateTime.Parse(m.Groups[0].Value);
        dt = dt.ToUniversalTime();
        TimeSpan ts = dt - DateTime.Parse("1970-01-01");
        result = string.Format("\\/Date({0}+0800)\\/", ts.TotalMilliseconds);
        return result;
    }
    //-------------与JObject配合
    public static string AddVal(string[] name, params string[] value)
    {
        JObject obj = new JObject();
        for (int i = 0; i < name.Length; i++)
        {
            obj.Add(name[i], value[i]);
        }
        return obj.ToString();
    }
    public static string GetVal(string json, string name)
    {
        if (string.IsNullOrEmpty(json)) return "";
        JObject obj = (JObject)JsonConvert.DeserializeObject(json);
        return obj[name].ToString();
    }
    public static double GetDBVal(string json,string name)
    {
        return DataConvert.CDouble(GetVal(json, name));
    }
}
