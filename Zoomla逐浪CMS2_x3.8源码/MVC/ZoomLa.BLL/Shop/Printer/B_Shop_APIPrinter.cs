using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Xml;
using ZoomLa.Common;
using ZoomLa.Model.Shop;

namespace ZoomLa.BLL.Shop
{
    public class B_Shop_APIPrinter
    {
        private readonly string apiurl = "http://my.feyin.net/api/sendMsg";
        public B_Shop_APIPrinter()
        {
        }
        public B_Shop_APIPrinter(string memberCode, string securityKey)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ids">设备编码</param>
        /// <param name="message">要打印的消息</param>
        /// <returns></returns>
        public int Print(string ids, string message)
        {
            int successNum = 0;
            //SafeSC.CheckIDSEx(ids);
            string[] nos = ids.Split(',');
            foreach (string no in nos)
            {
                M_Shop_PrintMessage MsgMod = new M_Shop_PrintMessage();
                MsgMod.Mode = 2;
                MsgMod.Detail = message;
                MsgMod.ReqTime = DateTime.Now;
                MsgMod.ReqStatus = SendFreeMessage(MsgMod, no);
                if (MsgMod.ReqStatus.Equals("0")) { successNum += 1; }
                //将MsgMod存入数据库
            }
            return successNum;
        }
        /// <summary>
        /// 获取用户所有打印设备的编码
        /// </summary>
        /// <returns></returns>
        //public string GetAllDeviceNo()
        //{
        //    string ids = string.Empty;
        //    List<M_Shop_PrintDevice> Devices = ListDevice();
        //    foreach (M_Shop_PrintDevice device in Devices)
        //    {
        //        ids += device.DeviceNo + ",";
        //    }
        //    if (!string.IsNullOrEmpty(ids)) { ids.TrimEnd(','); }
        //    return ids;
        //}

        /// <summary>
        /// 打印自定义格式消息
        /// </summary>
        /// <param name="Message">要打印的消息对象</param>
        /// <returns></returns>
        public string SendFreeMessage(M_Shop_PrintMessage msgMod, string DeviceNo)
        {
            B_Shop_PrintDevice printBll = new B_Shop_PrintDevice();
            M_Shop_PrintDevice printMod = printBll.SelModeByDevice(DeviceNo);
            if (string.IsNullOrEmpty(DeviceNo)) { throw new Exception("设备编号不能为空"); }
            if (string.IsNullOrEmpty(msgMod.Detail)) { throw new Exception("内容不能为空"); }
            long reqTime = GetCurrentMilli();
            string qstr = "memberCode=" + HttpUtility.UrlEncode(printMod.MemberCode, Encoding.UTF8);
            qstr += "&deviceNo=" + HttpUtility.UrlEncode(DeviceNo, Encoding.UTF8);
            qstr += "&msgNo=" + HttpUtility.UrlEncode(msgMod.MsgNo, Encoding.UTF8);
            qstr += "&msgDetail=" + HttpUtility.UrlEncode(msgMod.Detail, Encoding.UTF8);
            qstr += "&mode=" + HttpUtility.UrlEncode(msgMod.Mode.ToString(), Encoding.UTF8); ;
            qstr += "&reqTime=" + reqTime;
            qstr += "&securityCode=" + StringHelper.MD5(printMod.MemberCode + msgMod.Detail + DeviceNo + msgMod.MsgNo + reqTime + printMod.SecurityKey);
            return SendMessage(qstr);
        }
        ///// <summary>
        ///// 打印格式化消息
        ///// </summary>
        ///// <param name="Message">要打印的消息对象</param>
        ///// <returns></returns>
        //public string SendFormatedMessage(M_Shop_PrintMessage Message, string DeviceNo)
        //{
        //    int mode = 1;
        //    long reqTime = GetCurrentMilli();
        //    string qstr = "memberCode=" + HttpUtility.UrlEncode(MemberCode, Encoding.UTF8);
        //    qstr += "&customerName=" + HttpUtility.UrlEncode("xiaoming", Encoding.UTF8);
        //    qstr += "&customerPhone=" + HttpUtility.UrlEncode("13104259386", Encoding.UTF8);
        //    qstr += "&customerAddress=" + HttpUtility.UrlEncode("江西南昌", Encoding.UTF8);
        //    qstr += "&msgDetail=" + HttpUtility.UrlEncode(Message.MsgDetail, Encoding.UTF8);
        //    qstr += "&deviceNo=" + HttpUtility.UrlEncode(DeviceNo, Encoding.UTF8);
        //    qstr += "&msgNo=" + HttpUtility.UrlEncode(Message.MsgNo, Encoding.UTF8);
        //    qstr += "&mode=" + mode;
        //    qstr += "&reqTime=" + reqTime;
        //    qstr += "&securityCode=" + StringHelper.MD5(MemberCode + "xiaoming" + "13104259386" + "江西南昌"
        //                                        + "十万火急" + Message.MsgDetail + DeviceNo + Message.MsgNo + reqTime + SecurityKey);
        //    return SendMessage(qstr);
        //}
        /// <summary>
        /// 公用的发送消息函数
        /// </summary>
        /// <param name="qstr"></param>
        /// <returns></returns>
        private string SendMessage(string qstr)
        {
            try
            {
                HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(apiurl);
                req.Method = "POST";
                req.ContentType = "application/x-www-form-urlencoded";
                req.ContentLength = qstr.Length;

                StreamWriter writer = new StreamWriter(req.GetRequestStream(), Encoding.ASCII);
                writer.Write(qstr);
                writer.Flush();
                HttpWebResponse response = (HttpWebResponse)req.GetResponse();
                string encoding = response.ContentEncoding;
                if (encoding == null || encoding.Length < 1)
                {
                    encoding = "UTF-8"; //默认编码
                }
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(encoding));
                string data = reader.ReadToEnd();
                response.Close();
                return data;
            }
            catch (Exception ex) { return ex.Message; }
        }
        ///// <summary>
        ///// 查询打印状态
        ///// </summary>
        ///// <param name="msgNo"></param>
        //public string QueryState(string msgNo)
        //{
        //    long reqTime = GetCurrentMilli();
        //    return APIHelper.GetWebResult("http://my.feyin.net/api/queryState?memberCode=" + MemberCode + "&reqTime=" + reqTime + "&msgNo=" + msgNo + "&securityCode" + StringHelper.MD5(MemberCode + reqTime + SecurityKey + msgNo));
        //}
        ///// <summary>
        ///// 查询设备列表
        ///// </summary>
        //public List<M_Shop_PrintDevice> ListDevice()
        //{
        //    List<M_Shop_PrintDevice> Devices = new List<M_Shop_PrintDevice>();
        //    long reqTime = GetCurrentMilli();
        //    string result = APIHelper.GetWebResult("http://my.feyin.net/api/listDevice?memberCode=" + MemberCode + "&reqTime=" + reqTime + "&securityCode=" + StringHelper.MD5(MemberCode + reqTime + SecurityKey));
        //    XmlDocument xmlDoc = new XmlDocument();
        //    xmlDoc.LoadXml(result);
        //    XmlNodeList xnl = xmlDoc.GetElementsByTagName("device");
        //    for (int i = 0; i < xnl.Count; i++)
        //    {
        //        M_Shop_PrintDevice deviceMod = new M_Shop_PrintDevice();
        //        deviceMod.DeviceNo = xnl[i].Attributes["id"].Value;
        //        XmlNode xnAdr = xnl[i].SelectSingleNode("address");
        //        if (xnAdr != null) { }
        //        XmlNode xnSe = xnl[i].SelectSingleNode("since");
        //        if (xnSe != null) { deviceMod.Since = DataConverter.CDate(xnSe.InnerText); }
        //        //XmlNode xnSc = xnl[i].SelectSingleNode("simCode");
        //        //if (xnSc != null) { deviceMod.SimCode = xnSc.InnerText; }
        //        XmlNode xnLc = xnl[i].SelectSingleNode("lastConnected");
        //        if (xnLc != null) { deviceMod.LastConnected = DataConverter.CDate(xnLc.InnerText); }
        //        XmlNode xnDs = xnl[i].SelectSingleNode("deviceStatus");
        //        if (xnDs != null) { deviceMod.DeviceStatus = xnDs.InnerText; }
        //        XmlNode xnPs = xnl[i].SelectSingleNode("paperStatus");
        //        if (xnPs != null) { deviceMod.PaperStatus = xnPs.InnerText; }
        //        Devices.Add(deviceMod);
        //    }
        //    return Devices;
        //}
        /// <summary>
        /// 查询异常列表
        /// </summary>
        public string ListException(M_Shop_PrintDevice printMod)
        {
            long reqTime = GetCurrentMilli();
            return APIHelper.GetWebResult("http://my.feyin.net/api/listException?memberCode=" + printMod.MemberCode + "&reqTime=" + reqTime + "&securityCode" + StringHelper.MD5(printMod.MemberCode + reqTime + printMod.SecurityKey));
        }

        /// <summary>
        /// 取得时间戳，类似于 java中的 System.currentTimeMillis()
        /// </summary>
        public long GetCurrentMilli()
        {
            DateTime Jan1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            TimeSpan javaSpan = DateTime.UtcNow - Jan1970;
            return (long)javaSpan.TotalMilliseconds;
        }
    }
}
