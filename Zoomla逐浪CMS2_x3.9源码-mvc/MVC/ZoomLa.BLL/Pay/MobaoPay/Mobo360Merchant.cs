using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Xml;
namespace ZoomLa.BLL.MobaoPay
{
    public sealed class Mobo360Merchant
    {
        private static readonly Mobo360Merchant instance = new Mobo360Merchant();
        public static Mobo360Merchant Instance
        {
            get { return instance; }
        }

        private Mobo360Merchant()
        { }

        /// <summary>
        /// 商户交易取消
        /// </summary>
        /// <param name="sourceData"></param>
        /// <returns></returns>
        private string getTranCancelBody(Dictionary<string, string> sourceData)
        {
            if (!sourceData.ContainsKey("apiName") || string.IsNullOrEmpty(sourceData["apiName"]))
            {
                throw new Exception("apiName不能为空");
            }
            if (!sourceData.ContainsKey("apiVersion") || string.IsNullOrEmpty(sourceData["apiVersion"]))
            {
                throw new Exception("apiVersion不能为空");
            }
            if (!sourceData.ContainsKey("platformID") || string.IsNullOrEmpty(sourceData["platformID"]))
            {
                throw new Exception("platformID不能为空");
            }
            if (!sourceData.ContainsKey("merchNo") || string.IsNullOrEmpty(sourceData["merchNo"]))
            {
                throw new Exception("merchNo不能为空");
            }
            if (!sourceData.ContainsKey("orderNo") || string.IsNullOrEmpty(sourceData["orderNo"]))
            {
                throw new Exception("orderNo不能为空");
            }
            if (!sourceData.ContainsKey("tradeDate") || string.IsNullOrEmpty(sourceData["tradeDate"]))
            {
                throw new Exception("tradeDate不能为空");
            }
            if (!sourceData.ContainsKey("amt") || string.IsNullOrEmpty(sourceData["amt"]))
            {
                throw new Exception("amt不能为空");
            }
            if (!sourceData.ContainsKey("tradeSummary") || string.IsNullOrEmpty(sourceData["tradeSummary"]))
            {
                throw new Exception("tradeSummary不能为空");
            }
            if (!sourceData.ContainsKey("signMsg") || string.IsNullOrEmpty(sourceData["signMsg"]))
            {
                throw new Exception("signMsg不能为空");
            }

            string apiName = sourceData["apiName"];
            string apiVersion = sourceData["apiVersion"];
            string platformID = sourceData["platformID"];
            string merchNo = sourceData["merchNo"];
            string orderNo = sourceData["orderNo"];
            string tradeDate = sourceData["tradeDate"];
            string amt = sourceData["amt"];
            string tradeSummary = sourceData["tradeSummary"];
            string signMsg = sourceData["signMsg"];
            if (!apiVersion.Equals("1.0.0.0"))
            {
                throw new Exception("apiVersion错误！");
            }

            string result = string.Format("apiName={0}&apiVersion={1}&platformID={2}&merchNo={3}&orderNo={4}&tradeDate={5}&amt={6}&tradeSummary={7}&signMsg={8}",
                apiName, apiVersion, platformID, merchNo, orderNo, tradeDate, amt, tradeSummary, signMsg);
            return result;
        }

        /// <summary>
        /// 商户退款
        /// </summary>
        /// <param name="sourceData"></param>
        /// <returns></returns>
        private string getTranReturn(Dictionary<string, string> sourceData)
        {
            if (!sourceData.ContainsKey("apiName") || string.IsNullOrEmpty(sourceData["apiName"]))
            {
                throw new Exception("apiName不能为空");
            }
            if (!sourceData.ContainsKey("apiVersion") || string.IsNullOrEmpty(sourceData["apiVersion"]))
            {
                throw new Exception("apiVersion不能为空");
            }
            if (!sourceData.ContainsKey("platformID") || string.IsNullOrEmpty(sourceData["platformID"]))
            {
                throw new Exception("platformID不能为空");
            }
            if (!sourceData.ContainsKey("merchNo") || string.IsNullOrEmpty(sourceData["merchNo"]))
            {
                throw new Exception("merchNo不能为空");
            }
            if (!sourceData.ContainsKey("orderNo") || string.IsNullOrEmpty(sourceData["orderNo"]))
            {
                throw new Exception("orderNo不能为空");
            }
            if (!sourceData.ContainsKey("tradeDate") || string.IsNullOrEmpty(sourceData["tradeDate"]))
            {
                throw new Exception("tradeDate不能为空");
            }
            if (!sourceData.ContainsKey("amt") || string.IsNullOrEmpty(sourceData["amt"]))
            {
                throw new Exception("amt不能为空");
            }
            if (!sourceData.ContainsKey("tradeSummary") || string.IsNullOrEmpty(sourceData["tradeSummary"]))
            {
                throw new Exception("tradeSummary不能为空");
            }
            if (!sourceData.ContainsKey("signMsg") || string.IsNullOrEmpty(sourceData["signMsg"]))
            {
                throw new Exception("signMsg不能为空");
            }

            string apiName = sourceData["apiName"];
            string apiVersion = sourceData["apiVersion"];
            string platformID = sourceData["platformID"];
            string merchNo = sourceData["merchNo"];
            string orderNo = sourceData["orderNo"];
            string tradeDate = sourceData["tradeDate"];
            string amt = sourceData["amt"];
            string tradeSummary = sourceData["tradeSummary"];
            string signMsg = sourceData["signMsg"];
            if (!apiVersion.Equals("1.0.0.0"))
            {
                throw new Exception("apiVersion错误！");
            }

            string body = string.Format("apiName={0}&apiVersion={1}&platformID={2}&merchNo={3}&orderNo={4}&tradeDate={5}&amt={6}&tradeSummary={7}&signMsg={8}",
                apiName, apiVersion, platformID, merchNo, orderNo, tradeDate, amt, tradeSummary, signMsg);
            return body;
        }

        /// <summary>
        /// 商户交易查询
        /// </summary>
        /// <param name="sourceData"></param>
        /// <returns></returns>
        private string getTranQuery(Dictionary<string, string> sourceData)
        {
            if (!sourceData.ContainsKey("apiName") || string.IsNullOrEmpty(sourceData["apiName"]))
            {
                throw new Exception("apiName不能为空");
            }
            if (!sourceData.ContainsKey("apiVersion") || string.IsNullOrEmpty(sourceData["apiVersion"]))
            {
                throw new Exception("apiVersion不能为空");
            }
            if (!sourceData.ContainsKey("platformID") || string.IsNullOrEmpty(sourceData["platformID"]))
            {
                throw new Exception("platformID不能为空");
            }
            if (!sourceData.ContainsKey("merchNo") || string.IsNullOrEmpty(sourceData["merchNo"]))
            {
                throw new Exception("merchNo不能为空");
            }
            if (!sourceData.ContainsKey("orderNo") || string.IsNullOrEmpty(sourceData["orderNo"]))
            {
                throw new Exception("orderNo不能为空");
            }
            if (!sourceData.ContainsKey("tradeDate") || string.IsNullOrEmpty(sourceData["tradeDate"]))
            {
                throw new Exception("tradeDate不能为空");
            }
            if (!sourceData.ContainsKey("amt") || string.IsNullOrEmpty(sourceData["amt"]))
            {
                throw new Exception("amt不能为空");
            }
            if (!sourceData.ContainsKey("signMsg") || string.IsNullOrEmpty(sourceData["signMsg"]))
            {
                throw new Exception("signMsg不能为空");
            }

            string apiName = sourceData["apiName"];
            string apiVersion = sourceData["apiVersion"];
            string platformID = sourceData["platformID"];
            string merchNo = sourceData["merchNo"];
            string orderNo = sourceData["orderNo"];
            string tradeDate = sourceData["tradeDate"];
            string amt = sourceData["amt"];
            string signMsg = sourceData["signMsg"];
            if (!apiVersion.Equals("1.0.0.0"))
            {
                throw new Exception("apiVersion错误！");
            }

            string result = string.Format("apiName={0}&apiVersion={1}&platformID={2}&merchNo={3}&orderNo={4}&tradeDate={5}&amt={6}&signMsg={7}",
                apiName, apiVersion, platformID, merchNo, orderNo, tradeDate, amt, signMsg);
            return result;
        }

        /// <summary>
        /// 如果数据被篡改 则抛出异常
        /// </summary>
        /// <param name="result"></param>
        private void checkResult(string result)
        {
            if (string.IsNullOrEmpty(result))
            {
                throw new Exception("返回数据为空");
            }

            XmlDocument resultDoc = new XmlDocument();
            resultDoc.LoadXml(result);

            XmlElement rootEl = resultDoc.DocumentElement;
            if (null == rootEl)
            {
                throw new Exception("回复数据格式不正确");
            }

            XmlElement respDataEl = rootEl["respData"];
            if (null == respDataEl)
            {
                throw new Exception("回复数据格式不正确");
            }
            string respData = respDataEl.OuterXml;

            XmlElement signMsgEl = rootEl["signMsg"];
            if (null == signMsgEl)
            {
                throw new Exception("回复数据格式不正确");
            }
            string signMsg = signMsgEl.InnerText;

            if (string.IsNullOrEmpty(respData) || string.IsNullOrEmpty(signMsg))
            {
                throw new Exception("解析返回验签或原数据错误");
            }

            if (!Mobo360SignUtil.Instance.verifyData(signMsg, respData))
            {
                throw new Exception("系统校验返回数据失败！");
            }
        }


        private bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            //直接确认，否则打不开
            return true;
        }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="sourceData">待发送源数据</param>
        /// <param name="serverUrl">服务地址</param>
        /// <returns></returns>
        public string transact(Dictionary<string, string> sourceData, string serverUrl)
        {
            if (string.IsNullOrEmpty(serverUrl) || null == sourceData)
            {
                throw new Exception("请求地址或请求数据为空");
            }

            string checkUrl = serverUrl.ToLower();
            bool isHttps = true;
            if (!checkUrl.StartsWith("https"))
            {
                //throw new Exception("URL地址必须以https开头");
                isHttps = false;
            }

            // 获取接口名称
            string apiName = string.Empty;
            if (!sourceData.TryGetValue("apiName", out apiName))
            {
                throw new Exception("apiName不存在！");
            }
            else if (string.IsNullOrEmpty(apiName))
            {
                throw new Exception("apiName不能为空！");
            }

            // 根据接口名称获取待发送数据
            string body = string.Empty;
            switch (apiName)
            {
                case "MOBO_CANCEL_APPLY":
                    body = getTranCancelBody(sourceData);
                    break;
                case "MOBO_TRAN_RETURN":
                    body = getTranReturn(sourceData);
                    break;
                case "MOBO_TRAN_QUERY":
                    body = getTranQuery(sourceData);
                    break;
                default:
                    throw new Exception("apiName错误");
            }

            body = body.Replace("\\+", "%2B");
            var postData = Encoding.UTF8.GetBytes(body);

            // 收发数据
            if (isHttps)
            {
                ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(CheckValidationResult);
            }
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serverUrl);
            request.Method = "POST";
            request.Timeout = 5000;
            request.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US; rv:1.8.1.1) Gecko/20061204 Firefox/2.0.0.3";
            request.ContentType = "application/x-www-form-urlencoded; charset=utf-8";
            request.KeepAlive = false;
            request.ContentLength = postData.Length;
            request.ServicePoint.Expect100Continue = false;
            using (var requestStream = request.GetRequestStream())
            {
                requestStream.Write(postData, 0, postData.Length);
                requestStream.Flush();
            }

            string result = string.Empty;
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                Stream streamReceive = response.GetResponseStream();
                using (StreamReader reader = new StreamReader(streamReceive, Encoding.UTF8))
                {
                    result = reader.ReadToEnd();
                }
            }

            // 验证收到的数据
            if (string.IsNullOrEmpty(result))
            {
                throw new Exception("返回参数错误！");
            }
            checkResult(result);
            return result;
        }

        public string generatePayUrl(Dictionary<string, string> paramsDict, string payReqUrl)
        {
            string myParams = string.Format("?apiName={0}&apiVersion={1}&platformID={2}&merchNo={3}&orderNo={4}&tradeDate={5}&amt={6}&merchUrl={7}&merchParam={8}&tradeSummary={9}&signMsg={10}",
                paramsDict["apiName"],
                paramsDict["apiVersion"],
                paramsDict["platformID"],
                paramsDict["merchNo"],
                paramsDict["orderNo"],
                paramsDict["tradeDate"],
                paramsDict["amt"],
                System.Web.HttpUtility.UrlEncode(Encoding.UTF8.GetBytes(paramsDict["merchUrl"])),
                System.Web.HttpUtility.UrlEncode(Encoding.UTF8.GetBytes(paramsDict["merchParam"])),
                System.Web.HttpUtility.UrlEncode(Encoding.UTF8.GetBytes(paramsDict["tradeSummary"])),
                paramsDict["signMsg"]);
            return payReqUrl + myParams;
        }
    }
}
