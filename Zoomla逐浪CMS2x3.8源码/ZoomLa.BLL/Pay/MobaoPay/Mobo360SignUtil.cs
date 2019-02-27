using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
//using Org.BouncyCastle.Crypto;
//using Org.BouncyCastle.Crypto.Parameters;
//using Org.BouncyCastle.Pkcs;
//using Org.BouncyCastle.Security;
//using Org.BouncyCastle.X509;
using System.Diagnostics;

namespace ZoomLa.BLL.MobaoPay
{
    //移除了crypt.dll,有需要的话将其恢复5M大小
    public sealed class Mobo360SignUtil
    {
        private static readonly Mobo360SignUtil instance = new Mobo360SignUtil();
        //private RsaKeyParameters privateKey = null;
        //private X509Certificate certificate = null;

        public static Mobo360SignUtil Instance
        {
            get { return instance; }
        }

        private Mobo360SignUtil()
        { }

        /// <summary>
        /// 获取需要签名的数据
        /// </summary>
        /// <param name="sourceData"></param>
        /// <returns></returns>
        private string getSigMsg(Dictionary<string, string> sourceData)
        {
            string signMsg = string.Empty;
            string apiName = string.Empty;

            if (sourceData.TryGetValue("apiName", out apiName))
            {
                if (apiName.Length == 0)
                    throw new Exception("apiName为空！");
            }
            else
            {
                throw new Exception("apiName为空！");
            }

            switch (apiName)
            {
                case "MOBO_TRAN_QUERY":
                    signMsg = getQueryData(sourceData);
                    break;
                case "MOBO_TRAN_RETURN":
                    signMsg = getReturnData(sourceData);
                    break;
                case "WAP_PAY_B2C":
                    signMsg = getPayData(sourceData);
                    break;
                case "WEB_PAY_B2C":
                    signMsg = getPayData(sourceData);
                    break;
                default:
                    throw new Exception("apiName错误！");
                //break;
            }

            Debug.Print(signMsg);
            return signMsg;
        }

        /// <summary>
        /// 获取支付请求签名数据
        /// </summary>
        /// <param name="sourceData"></param>
        /// <returns></returns>
        private string getPayData(Dictionary<string, string> sourceData)
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
            if (!sourceData.ContainsKey("merchUrl") || string.IsNullOrEmpty(sourceData["merchUrl"]))
            {
                throw new Exception("merchUrl不能为空");
            }
            if (!sourceData.ContainsKey("merchParam"))
            {
                throw new Exception("merchParam可以为空，但必须存在！");
            }
            if (!sourceData.ContainsKey("tradeSummary") || string.IsNullOrEmpty(sourceData["tradeSummary"]))
            {
                throw new Exception("tradeSummary不能为空");
            }

            string apiName = sourceData["apiName"];
            string apiVersion = sourceData["apiVersion"];
            string platformID = sourceData["platformID"];
            string merchNo = sourceData["merchNo"];
            string orderNo = sourceData["orderNo"];
            string tradeDate = sourceData["tradeDate"];
            string amt = sourceData["amt"];
            string merchUrl = sourceData["merchUrl"];
            string merchParam = sourceData["merchParam"];
            string tradeSummary = sourceData["tradeSummary"];
            if (!apiVersion.Equals("1.0.0.0"))
            {
                throw new Exception("apiVersion错误！");
            }

            string result = string.Format("apiName={0}&apiVersion={1}&platformID={2}&merchNo={3}&orderNo={4}&tradeDate={5}&amt={6}&merchUrl={7}&merchParam={8}&tradeSummary={9}",
                apiName, apiVersion, platformID, merchNo, orderNo, tradeDate, amt, merchUrl, merchParam, tradeSummary);
            return result;
        }

        /// <summary>
        /// 获取商户查询数据
        /// </summary>
        /// <param name="sourceData"></param>
        /// <returns></returns>
        private string getQueryData(Dictionary<string, string> sourceData)
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

            string apiName = sourceData["apiName"];
            string apiVersion = sourceData["apiVersion"];
            string platformID = sourceData["platformID"];
            string merchNo = sourceData["merchNo"];
            string orderNo = sourceData["orderNo"];
            string tradeDate = sourceData["tradeDate"];
            string amt = sourceData["amt"];
            if (!apiVersion.Equals("1.0.0.0"))
            {
                throw new Exception("apiVersion错误！");
            }

            string result = string.Format("apiName={0}&apiVersion={1}&platformID={2}&merchNo={3}&orderNo={4}&tradeDate={5}&amt={6}",
                apiName, apiVersion, platformID, merchNo, orderNo, tradeDate, amt);
            return result;
        }

        /// <summary>
        /// 获取商户退款签名的数据
        /// </summary>
        /// <param name="sourceData"></param>
        /// <returns></returns>
        private string getReturnData(Dictionary<string, string> sourceData)
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

            string apiName = sourceData["apiName"];
            string apiVersion = sourceData["apiVersion"];
            string platformID = sourceData["platformID"];
            string merchNo = sourceData["merchNo"];
            string orderNo = sourceData["orderNo"];
            string tradeDate = sourceData["tradeDate"];
            string amt = sourceData["amt"];
            string tradeSummary = sourceData["tradeSummary"];
            if (!apiVersion.Equals("1.0.0.0"))
            {
                throw new Exception("apiVersion错误！");
            }

            string result = string.Format("apiName={0}&apiVersion={1}&platformID={2}&merchNo={3}&orderNo={4}&tradeDate={5}&amt={6}&tradeSummary={7}",
                apiName, apiVersion, platformID, merchNo, orderNo, tradeDate, amt, tradeSummary);
            return result;
        }

        /// <summary>
        /// 获取查询验签原数据
        /// </summary>
        /// <param name="sourceData"></param>
        /// <returns></returns>
        private string getQueryVerifyData(Dictionary<string, string> sourceData)
        {
            string result = string.Empty;

            if (!sourceData.TryGetValue("respData", out result))
            {
                throw new Exception("返回数据为空！");
            }
            else if (string.IsNullOrEmpty(result))
            {
                throw new Exception("返回数据为空！");
            }
            else
            {
                return result;
            }
        }

        /// <summary>
        /// 获取退款验签原数据
        /// </summary>
        /// <param name="sourceData"></param>
        /// <returns></returns>
        private string getReturnVerifyData(Dictionary<string, string> sourceData)
        {
            string result = string.Empty;

            if (!sourceData.TryGetValue("respData", out result))
            {
                throw new Exception("返回数据为空！");
            }
            else if (string.IsNullOrEmpty(result))
            {
                throw new Exception("返回数据为空！");
            }
            else
            {
                return result;
            }
        }

        /// <summary>
        /// 签名初始化
        /// </summary>
        /// <param name="pfxFile">签名私钥文件的绝对路径</param>
        /// <param name="certFile">验证签名文件的绝对路径</param>
        /// <param name="pfxPwd">私钥文件的密码</param>
        public void init(string pfxFile, string certFile, string pfxPwd)
        {
            //if (null == privateKey || null == certificate)
            //{
            //    // 根据指定的文件名和密码获取私钥，用于签名数据。
            //    using (FileStream fs = File.OpenRead(pfxFile))
            //    {
            //        char[] passwd = pfxPwd.ToCharArray();

                    //Pkcs12Store store = new Pkcs12StoreBuilder().Build();
                    //store.Load(fs, passwd);

                    //string alias = null;
                    //foreach (string str in store.Aliases)
                    //{
                    //    if (store.IsKeyEntry(str))
                    //        alias = str;
                    //}
                    //if (null == alias)
                    //{
                    //    throw new Exception("alias 为空");
                    //}
                    //Debug.Print("alias=" + alias);

                    //AsymmetricKeyEntry keyEntry = store.GetKey(alias);
                    //privateKey = (RsaKeyParameters)keyEntry.Key;
                //    Debug.Print("获取私钥成功！");
                //}

                //// 从文件中读取证书
                //using (FileStream fs = File.OpenRead(certFile))
                //{
                //    //certificate = new X509CertificateParser().ReadCertificate(fs);
                //    //Debug.Print("获取公钥证书成功！");
                //}
            //}
        }

        /// <summary>
        /// 生成签名：针对查询和退货
        /// </summary>
        /// <param name="sourceData">签名源数据</param>
        /// <returns></returns>
        public string sign(Dictionary<string, string> sourceData)
        {
            //if (null == privateKey)
            //{
            //    throw new Exception("签名尚未初始化！");
            //}
            //if (0 == sourceData.Count)
            //{
            //    throw new Exception("签名数据为空！");
            //}

            //// 签名
            //var data = Encoding.UTF8.GetBytes(getSigMsg(sourceData));
            //ISigner signer = SignerUtilities.GetSigner("MD5WITHRSA");
            //signer.Init(true, privateKey);
            //signer.BlockUpdate(data, 0, data.Length);
            //data = signer.GenerateSignature();

            //string result = Convert.ToBase64String(data).Replace("\r", "").Replace("\n", "");
            //return result;
            return "";
        }

        /// <summary>
        /// 验证签名
        /// </summary>
        /// <param name="signData">签名数据</param>
        /// <param name="sourceData">源数据</param>
        /// <returns></returns>
        public bool verify(string signData, Dictionary<string, string> sourceData)
        {
            return this.verifyData(signData, sourceData["respData"]);
        }

        /// <summary>
        /// 验证签名
        /// </summary>
        /// <param name="signData">签名数据</param>
        /// <param name="srcData">源数据</param>
        /// <returns></returns>
        public bool verifyData(string signData, string srcData)
        {
            //if (null == certificate)
            //{
            //    throw new Exception("签名尚未初始化！");
            //}
            //if (string.IsNullOrEmpty(signData) || string.IsNullOrEmpty(srcData))
            //{
            //    throw new Exception("系统效验时，签名数据或原数据为空！");
            //}

            var srcBytes = Encoding.UTF8.GetBytes(srcData);
            //ISigner signer = SignerUtilities.GetSigner("MD5WITHRSA");
            //signer.Init(false, certificate.GetPublicKey());
            //signer.BlockUpdate(srcBytes, 0, srcBytes.Length);
            //return signer.VerifySignature(Convert.FromBase64String(signData));
            return false;
        }
    }
}
