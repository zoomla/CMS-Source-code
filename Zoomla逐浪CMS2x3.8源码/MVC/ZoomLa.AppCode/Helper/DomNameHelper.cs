using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.SQLDAL;

/// <summary>
/// DomainNameHelper 的摘要说明
/// </summary>
public class DomNameHelper
{
    //默认代理编号
    private Encoding _WebEncode = Encoding.GetEncoding("UTF-8");
    private string _RequestString;
    private string _Result;
    private string _RequestUrl;
    private WebClient _EWebClient = new WebClient();

    /// <summary>
    /// 处理编码
    /// </summary>
    public Encoding WebEncode
    {
        get { return _WebEncode; }
        set { _WebEncode = value; }
    }

    /// <summary>
    /// 请求的查询参数
    /// </summary>
    public string RequestString
    {
        get { return _RequestString; }
        set { _RequestString = value; }
    }
    /// <summary>
    /// 请求返回的结果
    /// </summary>
    public string Result
    {
        get { return _Result; }
        set { _Result = value; }
    }
    public Hashtable HashtableResult { get { return ResultToHashTable(Result); } }
    /// <summary>
    /// 请求的URL地址
    /// </summary>
    public string RequestUrl
    {
        get { return _RequestUrl; }
        set { _RequestUrl = value; }
    }

    /// <summary>
    /// 客户端与服务器通信类
    /// </summary>
    public WebClient EWebClient
    {
        get { return _EWebClient; }
        set { _EWebClient = value; }
    }

    public DomNameHelper(ApiType _apitype, List<QueryParam> _list)
    {
        this.RequestUrl = GetApiUrl(_apitype);
        this.RequestString = GetRequestString(_list);
        this.GetHttp();
    }

    public DomNameHelper(string _ApiUrl, List<QueryParam> _list)
    {
        this.RequestUrl = _ApiUrl;
        this.RequestString = GetRequestString(_list);
    }

    public string GetRequestString(List<QueryParam> list)
    {
        string requestUrl = "";
        foreach (QueryParam q in list)
        {
            requestUrl += q.QueryName + "=" + HttpUtility.UrlEncode(q.QueryValue, this.WebEncode) + "&";
        }
        return requestUrl.TrimEnd('&');
    }
    /// <summary>
    /// Post提交数据,并获取返回结果
    /// </summary>
    public string GetHttp()
    {
        this.EWebClient.Headers.Add(HttpRequestHeader.ContentType, "application/x-www-form-urlencoded");
        this.EWebClient.Encoding = this.WebEncode;
        //http://api.xinnet.com/domain/api.gb?method=check&charset=utf-8
        this.Result = this.EWebClient.UploadString(new Uri(this.RequestUrl), "POST", this.RequestString);
        return this.Result;
    }

    public string GetApiUrl(ApiType _ApiType)
    {
        string _Url = "";
        switch (_ApiType)
        {
            case ApiType.Check:
                _Url = "http://api.xinnet.com/domain/api.gb?method=check&charset=utf-8";
                break;
            case ApiType.Register:
                _Url = "http://api.xinnet.com/domain/api.gb?method=Register&charset=utf-8";
                break;
            case ApiType.Status:
                _Url = "http://api.xinnet.com/domain/api.gb?method=Status&charset=utf-8";
                break;
            case ApiType.ModDns:
                _Url = "http://api.xinnet.com/domain/api.gb?method=ModDns&charset=utf-8";
                break;
            case ApiType.domain:
                _Url = "http://api.xinnet.com/domain/api.gb?method=DomainRenew&charset=utf-8";
                break;
            case ApiType.GetProductKey:
                _Url = "http://api.xinnet.com/domain/api.gb?method=GetProductKey&charset=utf-8";
                break;
            case ApiType.ChangeProductKey:
                _Url = "http://api.xinnet.com/domain/api.gb?method=ChangeProductKey&charset=utf-8";
                break;
            case ApiType.ModifyContactor:
                _Url = "http://api.xinnet.com/domain/api.gb?method=ModifyContactor&charset=utf-8";
                break;
            default:
                _Url = "http://api.xinnet.com/domain/api.gb?method=testmd5&charset=utf-8";
                break;
        }

        return _Url;
    }

    /// <summary>
    /// MD5加密
    /// </summary>
    /// <param name="_ConvertString">加密的内容</param>
    /// <param name="_Len">16或32 表示16位或32位</param>
    /// <returns></returns>
    public static string MD5(string _ConvertString, int _Len)
    {
        using (System.Security.Cryptography.MD5CryptoServiceProvider provider = new System.Security.Cryptography.MD5CryptoServiceProvider())
        {
            switch (_Len)
            {
                case 16:
                    return BitConverter.ToString(provider.ComputeHash(Encoding.UTF8.GetBytes(_ConvertString)), 4, 8).Replace("-", string.Empty).ToUpper(System.Globalization.CultureInfo.CurrentCulture);

                case 32:
                    return BitConverter.ToString(provider.ComputeHash(Encoding.UTF8.GetBytes(_ConvertString))).Replace("-", string.Empty).ToUpper(System.Globalization.CultureInfo.CurrentCulture);
                default:
                    return BitConverter.ToString(provider.ComputeHash(Encoding.UTF8.GetBytes(_ConvertString))).Replace("-", string.Empty).ToUpper(System.Globalization.CultureInfo.CurrentCulture);

            }
        }
    }
    /// <summary>
    /// 将回传的参数HashTable化
    /// </summary>
    public Hashtable ResultToHashTable(string result)
    {
        string[] resultArr = result.Split('&');//避免参数中出现空格
        Hashtable ht = new Hashtable();
        try
        {
            for (int i = 0; i < resultArr.Length; i++)
            {
                string key = resultArr[i].Split('=')[0];
                string val = resultArr[i].Split('=')[1];
                ht.Add(key, val);
            }
        }
        catch { }
        return ht;
    }
    /// <summary>
    /// 获取到期日期
    /// </summary>
    public static string GetEndDate(string url,string format)
    {
        string result="";
          //到期日等查询服务
        string checksum = DomNameHelper.MD5("GetProductKey" + StationGroup.newNetClientID + StationGroup.newNetApiPasswd + url + "E", 32);//以32位
        List<QueryParam> param = new List<QueryParam>();
        param.Add(new QueryParam("name", url));//域名
        param.Add(new QueryParam("enc", "E"));
        param.Add(new QueryParam("client", StationGroup.newNetClientID));
        param.Add(new QueryParam("keyname", "ExpireDate"));//到期日期等,为空则为PassWord
        param.Add(new QueryParam("checksum", checksum));
        //返回
        //ret=100&name=tc001.com&key=2014-01-10 00:00:00.0
        DomNameHelper XinNet = new DomNameHelper(ApiType.GetProductKey, param);
        Hashtable ht = XinNet.HashtableResult;
        if (ht["err"] != null)
        {
            switch (ht["err"].ToString())
            {
                case "name-notexist":
                    result = "域名不存在";
                    break;
            }
        }
        else
        {
                DateTime dTime = new DateTime();
                if(DateTime.TryParse(ht["key"].ToString(), out dTime))
                   result = dTime.ToString(format);
        }
        return result;
    }
    public static string GetEndDate(string url)
    {
        string format = "yyyy年M月dd日";
        return GetEndDate(url,format);
    }
    /// <summary>
    /// 根据返回结果，判断是否成功
    /// </summary>
    /// <returns></returns>
    public bool IsSuccess() 
    {
        bool flag = false;
        if (HashtableResult["ret"].Equals("100"))
        {
            flag = true;
        }
        return flag;
    }
    //将模板Value转为注册时用的信息
    protected List<QueryParam> ConvertToList(string value)
    {
        List<QueryParam> param = new List<QueryParam>();
        string[] temp, tempValue = value.Split(',');
        for (int i = 0; i < tempValue.Length; i++)
        {
            temp = tempValue[i].Split(':');
            param.Add(new QueryParam(temp[0], temp[1]));
        }
        //param.Where(p => p.QueryName.Equals("uname1")).ToList()[0].QueryValue;
        return param;
    }
    //将value转为词典
    public static Dictionary<string, string> ConvertToHashMap(string tempValue)
    {
        Dictionary<string, string> dic = new Dictionary<string, string>();
        try
        {
            string[] arr = tempValue.Split(',');
            for (int i = 0; i < arr.Length; i++)
            {
                dic.Add(arr[i].Split(':')[0], arr[i].Split(':')[1]);
            }
        }
        catch { }
        return dic;
    }
}
public class QueryParam
{
    private string _QueryName;
    private string _QueryValue;

    public string QueryName
    {
        set { this._QueryName = value; }
        get { return this._QueryName; }
    }

    public string QueryValue
    {
        set { this._QueryValue = value; }
        get { return this._QueryValue; }
    }

    public QueryParam(string _Name, string _Value)
    {
        this.QueryName = _Name;
        this.QueryValue = _Value;
    }

}
public enum ApiType
{
    Check,
    Register,
    Status,
    ModDns,
    domain,
    GetProductKey,
    ChangeProductKey,
    ModifyContactor
}