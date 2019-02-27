using MSXML2;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Web;
using woxpcn;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;

/*
 * 
 * 短信发送类,根据选择发送短信
 * 注意短信内容要多些，否则会被当作垃圾短信过滤
 *备注:北京网通与深圳电信命名反了,请注意
 */
public class SendWebSMS
{
    WebClient webClient = new WebClient();
    B_Safe_Mobile mobileBll = new B_Safe_Mobile();
    /// <summary>
    /// 依据后台配置，使用不同的接口发出短信.1:深圳电信,2:北京网通,3:亿美
    /// </summary>
    public static string SendMessage(string phone,string msg)
    {
        SendWebSMS client = new SendWebSMS();
        string result = "";
        switch (SiteConfig.SiteOption.DefaultSMS)
        {
            case "0"://关闭
                result = "";
                break;
            case "1"://深圳电信
                result = client.SendByBJWT(phone, msg);
                break;
            case "2"://北京网通(东时方)
                result = client.SendByEast(phone, msg).ToString();
                break;
            case "3"://亿美
                result = client.SendByYM(phone, msg);
                break;
            case "4"://云通讯
                result = client.SendMsgByCCP(phone, msg);
                break;
            default:
                result = "运营商选择错误";
                break;
        }
        return result;
    }
   
    /// <summary>
    /// 查询余额
    /// </summary>
    public static string GetBalance()
    {
        return GetBalance(SiteConfig.SiteOption.DefaultSMS);
    }
    public static string GetBalance(string flag)
    {
        string result="";
        SendWebSMS client = new SendWebSMS();
        switch (flag)
        {
            case "1"://深圳电信
                result = client.GetBJWTBalance();
                break;
            case "2"://北京网通(东时方)
                //result = client.GetBJWTBalance();
                break;
            case "3"://亿美
                result = client.GetYMBalance();
                break;
            default:
                function.WriteErrMsg("短接口配置不正确");
                break;
        }
        return result;
    }
    //------深圳电信
    public string SendByBJWT(string phone, string msg)
    {
        string res = "";
        if (string.IsNullOrEmpty(phone))
        {
            res = "手机号码为空,无法发送!";
            return res;
        }
        string req = this.SendMsg(SiteConfig.SiteOption.MssUser, SiteConfig.SiteOption.MssPsw, phone, msg);
        string[] reqs = req.Split(new char[] { '/' });

        switch (reqs[0])
        {
            case "000":
                res = "1";//发送成功！
                //res += "发送条数:" + reqs[1].Split(new char[] { ':' })[1] + "<br/>";
                //res += "当次消费金额" + reqs[2].Split(new char[] { ':' })[1] + "<br/>";
                //res += "总体余额:" + reqs[3].Split(new char[] { ':' })[1] + "<br/>";
                //res += "短信编号:" + reqs[4];
                break;
            case "-01":
                res = "当前账号余额不足！";
                break;
            case "-02":
                res = "当前用户ID错误！";
                break;
            case "-03":
                res = "当前密码错误！";
                break;
            case "-04":
                res = "参数不够或参数内容的类型错误！";
                break;
            case "-05":
                res = "手机号码格式不对！";
                break;
            case "-06":
                res = "短信内容编码不对！";
                break;
            case "-07":
                res = "短信内容含有敏感字符！";
                break;
            case "-8":
                res = "无接收数据";
                break;
            case "-09":
                res = "系统维护中..";
                break;
            case "-10":
                res = "手机号码数量超长！（100个/次 超100个请自行做循环）";
                break;
            case "-11":
                res = "短信内容超长！（70个字符）";
                break;
            case "-12":
                res = "其它错误！";
                break;
            default:
                res = req;
                break;
        }
        return res;
    }
    private string SendMsg(string uid, string pwd, string mob, string msg)
    {
        switch (SiteConfig.SiteOption.DefaultSMS)
        {
            case "3":
                return "";
            default:
                string Send_URL = "http://service.winic.org/sys_port/gateway/?id=" + uid + "&pwd=" + pwd + "&to=" + mob + "&content=" + msg + "&time=";
                MSXML2.XMLHTTP xmlhttp = new MSXML2.XMLHTTP();
                xmlhttp.open("GET", Send_URL, false, null, null);
                xmlhttp.send("");
                MSXML2.XMLDocument dom = new XMLDocument();
                Byte[] b = (Byte[])xmlhttp.responseBody;
                //string Flag = System.Text.ASCIIEncoding.UTF8.GetString(b, 0, b.Length);
                string andy = System.Text.Encoding.GetEncoding("GB2312").GetString(b).Trim();
                return andy;
        }
    }
    /// <summary>
    /// 云通讯
    /// </summary>
    /// <param name="phone"></param>
    /// <param name="msg">以内容参数以','隔开</param>
    /// <returns></returns>
    public string SendMsgByCCP(string phone,string msg)
    {
        CCPRestSDK.CCPRestSDK api = new CCPRestSDK.CCPRestSDK();
        //初始化发送短信
        bool isInit = api.init("sandboxapp.cloopen.com", "8883");
        api.setAccount(SiteConfig.SiteOption.CCPAccount_SID, SiteConfig.SiteOption.CCPToken);//aaf98f8952a572be0152aa32e1b606e4,595fc398965a433a9a57290fa7179fc4
        api.setAppId(SiteConfig.SiteOption.CCPAppID);//"8a48b55152a56fc20152aa33f3d60673"
        string ret = "";
        if (isInit)
        {
            //云通讯短信发送操作
            //【云通讯】您使用的是云通讯短信模板，您的验证码是{1}，请于{2}分钟内正确输入
            Dictionary<string, object> retData = api.SendTemplateSMS(phone,SiteConfig.SiteOption.CCPMsgTempID.ToString(), msg.Split(new char[] {','},StringSplitOptions.RemoveEmptyEntries));
            ret = getDictionaryData(retData);
        }
        else
        {
            ret = "初始化失败";
        }
        if (ret.Contains("statusMsg=成功"))
        {
            return "1";
        }
        else { return ret; }
    }
    //得到云通讯返回结果处理
    private string getDictionaryData(Dictionary<string, object> data)
    {
        string ret = null;
        foreach (KeyValuePair<string, object> item in data)
        {
            if (item.Value != null && item.Value.GetType() == typeof(Dictionary<string, object>))
            {
                ret += item.Key.ToString() + "={";
                ret += getDictionaryData((Dictionary<string, object>)item.Value);
                ret += "};";
            }
            else
            {
                ret += item.Key.ToString() + "=" + (item.Value == null ? "null" : item.Value.ToString()) + ";";
            }
        }
        return ret;
    }
    public string GetBJWTBalance() 
    {
        return GetBJWTBalance(SiteConfig.SiteOption.MssUser, SiteConfig.SiteOption.MssPsw);
    }
    public string GetBJWTBalance(string uid, string pwd)
    {
        string Send_URL = "http://service.winic.org/webservice/public/remoney.asp?uid=" + uid + "&pwd=" + pwd + "";
        MSXML2.XMLHTTP xmlhttp = new MSXML2.XMLHTTP();
        xmlhttp.open("GET", Send_URL, false, null, null);
        xmlhttp.send("");
        MSXML2.XMLDocument dom = new XMLDocument();
        Byte[] b = (Byte[])xmlhttp.responseBody;
        string andy = System.Text.Encoding.GetEncoding("GB2312").GetString(b).Trim();
        return andy;
    }

    //------北京网通(东时方)
    public int SendByEast(string t_sendNo, string t_sendMemo)
    {
        WebSMS wsm = new WebSMS();
        string g_uid = SiteConfig.SiteOption.G_uid;
        string g_eid = SiteConfig.SiteOption.G_eid;
        string g_pwd = SiteConfig.SiteOption.G_pwd;
        string g_gate_id = SiteConfig.SiteOption.G_gate_id;

        string strIdentity = wsm.GetIdentityMark(Int32.Parse(g_eid), g_uid, g_pwd, Int32.Parse(g_gate_id));
        if (wsm.GetMoney(strIdentity) > 0)
        {
            if (wsm == null)
            {
                //Response.Write("<script>alert('网关初始化失败!');window.location='Default.aspx';</script>");
                return 1;
            }
            //string strIdentity = wsm.GetIdentityMark(Int32.Parse(g_eid), g_uid, g_pwd, Int32.Parse(g_gate_id));
            if (strIdentity == null || strIdentity == "")
            {
                //Response.Write("<script>alert('获取标识串失败!');window.location='Default.aspx';</script>");
                return 2;
            }
            if (t_sendNo.Trim() == "" || t_sendMemo.Trim() == "")
            {
                //Response.Write("<script>alert('接收号码或者短信内容没有输入!');window.location='Default.aspx';</script>");
                return 3;
            }

            //快速发送.直接提交到运营商网关
            SendResult status = wsm.FastSend(strIdentity, t_sendNo.Trim(), t_sendMemo.Trim().Replace(",", ""), "", "");
            //System.Web.HttpContext.Current.Response.Write(t_sendMemo.Trim());
            //System.Web.HttpContext.Current.Response.End();
            //发送成功
            if (status.RetCode > 0)
            {
                //js = "发送成功,共发送:" + status.RetCode.ToString() + "条";
                return 99;
                //this.Label1.Text = "当前余额:" + wsm.GetMoney(strIdentity).ToString("0.00");
            }
               
            else //发送失败
            {
                //js = "发送失败,错误代码:" + status.RetCode.ToString() + ",原因:" + status.ErrorDesc;
                //System.Web.HttpContext.Current.Response.Write(js);
                return 4;
            }

            // Response.Write("<script>alert('" + js + "');window.location='MessageInfo.aspx';</script>");
        }
        return 0;
    }
    public string GetSZDXBalance()
    {
        WebSMS wsm = new WebSMS();
        string g_uid = SiteConfig.SiteOption.G_uid;
        string g_eid = SiteConfig.SiteOption.G_eid;
        string g_pwd = SiteConfig.SiteOption.G_pwd;
        string g_gate_id = SiteConfig.SiteOption.G_gate_id;

        string strIdentity = wsm.GetIdentityMark(Int32.Parse(g_eid), g_uid, g_pwd, Int32.Parse(g_gate_id));
        return wsm.GetMoney(strIdentity).ToString("0.00");
    }
    //------亿美短信
    public string SendByYM(string phone, string message) 
    {
        return SendByYM(phone, message, SiteConfig.SiteOption.sms_key, SiteConfig.SiteOption.sms_pwd);
    }
    private string SendByYM(string phone, string message, string sms_key, string sms_pwd)
    {
        string url = "http://sdk229ws.eucp.b2m.cn:8080/sdkproxy/sendsms.action?cdkey=" + sms_key + "&password=" + sms_pwd;
        string urlParam = "&phone={0}&message={1}&addserial=";
        urlParam = string.Format(urlParam, phone, message);
        return SendUrl(url+urlParam);
    }
    private string SendUrl(string url)
    {
        webClient.Headers.Add(HttpRequestHeader.ContentType, "application/x-www-form-urlencoded");
        webClient.Encoding = Encoding.GetEncoding("UTF-8");
        //string result = webClient.UploadString(new Uri(url), "POST", "client_id=" + clientID + "&sign=" + GenerateSign.GetSign(key, signUser));
        string result = webClient.UploadString(new Uri(url), "POST", "");
        return result;
    }
    public string GetYMBalance() 
    {
        return GetYMBalance(SiteConfig.SiteOption.sms_key, SiteConfig.SiteOption.sms_pwd);
    }
    public string GetYMBalance(string sms_key, string sms_pwd) 
    {
        SMSMessage1.SDKService client = new SMSMessage1.SDKService();
        return client.getBalance(sms_key, sms_pwd).ToString();
    }
}