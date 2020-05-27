using System;
using System.Collections.Generic;
using System.Web;
using System.Net;
using System.IO;
using System.Text;
using System.Net.Security;    
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using ZoomLa.Model;
using ZoomLa.Common;

namespace ZoomLa.BLL.WxPayAPI
{
    /// <summary>
    /// http连接基础类，负责底层的http通信
    /// </summary>
    public class HttpService
    {
        public static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            //直接确认，否则打不开    
            return true;
        }
        public static string Post(string xml, string url, bool isUseCert, int timeout,M_WX_APPID appMod)
        {
            System.GC.Collect();//垃圾回收，回收没有正常关闭的http连接

            string result = "";//返回结果

            HttpWebRequest request = null;
            HttpWebResponse response = null;
            Stream reqStream = null;

            try
            {
                //设置最大连接数
                ServicePointManager.DefaultConnectionLimit = 200;
                //设置https验证方式
                if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
                {
                    ServicePointManager.ServerCertificateValidationCallback =
                            new RemoteCertificateValidationCallback(CheckValidationResult);
                }

                /***************************************************************
                * 下面设置HttpWebRequest的相关属性
                * ************************************************************/
                request = (HttpWebRequest)WebRequest.Create(url);

                request.Method = "POST";
                request.Timeout = timeout * 1000;

                //设置代理服务器
                //WebProxy proxy = new WebProxy();                          //定义一个网关对象
                //proxy.Address = new Uri(WxPayConfig.PROXY_URL);              //网关服务器端口:端口
                //request.Proxy = proxy;

                //设置POST的数据类型和长度
                request.ContentType = "text/xml";
                byte[] data = System.Text.Encoding.UTF8.GetBytes(xml);
                request.ContentLength = data.Length;

                //是否使用证书
                if (isUseCert)
                {
                    //X509Certificate2 cert = new X509Certificate2(function.VToP(appMod.Pay_SSLPath), appMod.Pay_SSLPassword);
                    X509Certificate2 cert = Cert_GetByName(appMod.Pay_SSLPath);
                    request.ClientCertificates.Add(cert);
                }

                //往服务器写入数据
                reqStream = request.GetRequestStream();
                reqStream.Write(data, 0, data.Length);
                reqStream.Close();

                //获取服务端返回
                response = (HttpWebResponse)request.GetResponse();

                //获取服务端返回数据
                StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                result = sr.ReadToEnd().Trim();
                sr.Close();
            }
            catch (System.Threading.ThreadAbortException)
            {
                System.Threading.Thread.ResetAbort();
            }
            catch (WebException e)
            {
                if (e.Status == WebExceptionStatus.ProtocolError)
                {
                }
                throw new WxPayException(e.ToString());
            }
            catch (Exception e)
            {
                throw new WxPayException(e.ToString());
            }
            finally
            {
                //关闭连接和流
                if (response != null)
                {
                    response.Close();
                }
                if(request != null)
                {
                    request.Abort();
                }
            }
            return result;
        }
        /// <summary>
        /// 处理http GET请求，返回数据
        /// </summary>
        /// <param name="url">请求的url地址</param>
        /// <returns>http GET成功后返回的数据，失败抛WebException异常</returns>
        public static string Get(string url)
        {
            System.GC.Collect();
            string result = "";

            HttpWebRequest request = null;
            HttpWebResponse response = null;

            //请求url以获取数据
            try
            {
                //设置最大连接数
                ServicePointManager.DefaultConnectionLimit = 200;
                //设置https验证方式
                if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
                {
                    ServicePointManager.ServerCertificateValidationCallback =
                            new RemoteCertificateValidationCallback(CheckValidationResult);
                }

                /***************************************************************
                * 下面设置HttpWebRequest的相关属性
                * ************************************************************/
                request = (HttpWebRequest)WebRequest.Create(url);

                request.Method = "GET";

                //设置代理
                //WebProxy proxy = new WebProxy();
                //proxy.Address = new Uri(WxPayConfig.PROXY_URL);
                //request.Proxy = proxy;

                //获取服务器返回
                response = (HttpWebResponse)request.GetResponse();

                //获取HTTP返回数据
                StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                result = sr.ReadToEnd().Trim();
                sr.Close();
            }
            catch (System.Threading.ThreadAbortException)
            {
                System.Threading.Thread.ResetAbort();
            }
            catch (WebException e)
            {
                throw new WxPayException(e.ToString());
            }
            catch (Exception e)
            {
                throw new WxPayException(e.ToString());
            }
            finally
            {
                //关闭连接和流
                if (response != null)
                {
                    response.Close();
                }
                if (request != null)
                {
                    request.Abort();
                }
            }
            return result;
        }
        /// <summary>
        /// 根据name获取绑定在my下的证书,不需要password
        /// </summary>
        /// <param name="name">示例:浙江丰酿贸易有限公司</param>
        /// <returns></returns>
        public static X509Certificate2 Cert_GetByName(string name)
        {
            X509Store store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
            store.Open(OpenFlags.ReadOnly);
            X509Certificate2 result = null;
            //轮询存储区中的所有证书  
            foreach (X509Certificate2 myX509Certificate2 in store.Certificates)
            {
                if (myX509Certificate2.Subject.Contains(name))
                {
                    result = myX509Certificate2; break;
                }
                //将证书的名称跟要导出的证书MyTestCert比较,找到要导出的证书  
                //if (myX509Certificate2.Subject == "CN=TestCert")
                //{
                //    //证书导出到byte[]中，password为私钥保护密码  
                //    byte[] CertByte = myX509Certificate2.Export(X509ContentType.Pfx, "password");
                //    //将证书的字节流写入到证书文件  
                //    FileStream fStream = new FileStream(
                //    @"C:\Samples\PartnerAEncryptMsg\MyTestCert_Exp.pfx",
                //    FileMode.Create,
                //    FileAccess.Write);
                //    fStream.Write(CertByte, 0, CertByte.Length);
                //    fStream.Close();
                //}

            }
            store.Close();
            return result;
        }
    }
}