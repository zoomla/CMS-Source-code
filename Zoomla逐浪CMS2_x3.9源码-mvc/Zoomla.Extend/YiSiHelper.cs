using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;
using System.Xml;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.SQLDAL;

//Extend:帐号:产品编号
//其外部订单号不一定与我们的对应,根据生成的ys内部订单号查询
namespace ZoomLa.Extend
{
    //水电煤:在易赛后台可查订单,(需要用户充值,并能否获取到真实的数据,以便测试缴费)
    //话费  :仍报IP错误,是否其捆绑了单独IP
    //流量  :使用测试接口充值成功(需要用户充值,并开通对应的流量产品编号,以便进一步测试)
    //油卡  :其文档中未提供测试接口地址(联调时确定)
    //缴费流程
    //1,用户通过APP提交缴费申请
    //2,在APP中通过支付宝|微信,将费用缴至可点可赞CMS
    //3,CMS后台获悉付款成功后--根据用户信息,向易赛发起缴费申请(专门的表用于记录)
    //4,专用的结果回调页,获悉易赛的缴费是否成功,根据成功与失败,对缴费操作进行标注


    //提交给易赛后,需要几十秒后才可以通过接口获取是否充值成功
    //注意其返回只是提交是否成功(水电煤也是如此),所以提交后,需要用返回的订单号再查询(每种都需要实现两种方法)
    //流量包 缴费充值 接口只会返回你的订单是否已经提交 接口只会返回你的订单是否已经提交 接口只会返回你的订单是否已经提交 接口只会返回你的订单是否已经提交 接口只会返回你的订单是否已经提交 接口只会返回你的订单是否已经提交 接口只会返回你的订单是否已经提交 接口只会返回你的订单是否已经提交 接口只会返回你的订单是否已经提交 接口只会返回你的订单是否已经提交 接口只会返回你的订单是否已经提交 接口只会返回你的订单是否已经提交 接口只会返回你的订单是否已经提交 接口只会返回你的订单是否已经提交 接口只会返回你的订单是否已经提交 接口只会返回你的订单是否已经提交 ,查询充值结果请使用流量包 查询充值结果请使用流量包 查询充值结果请使用流量包 查询充值结果请使用流量包 查询充值结果请使用流量包 查询充值结果请使用流量包 查询充值结果请使用流量包 查询充值结果请使用流量包 查询充值结果请使用流量包 查询充值结果请使用流量包 查询充值结果请使用流量包 查询充值结果请使用流量包 订单结果查询 订单结果查询 订单结果查询 订单结果查询 订单结果查询 订单结果查询 接口 ，请合理搭配两个接口的使用逻辑。 请合理搭配两个接口的使用逻辑。 请合理搭配两个接口的使用逻辑。 请合理搭配两个接口的使用逻辑。 请合理搭配两个接口的使用逻辑。 请合理搭配两个接口的使用逻辑。
    //ZL_YiSi_Order,ZL_YiSi_Log
    public class YiSiHelper
    {
        B_OrderList orderBll = new B_OrderList();
        B_Yisi_Order ysBll = new B_Yisi_Order();
        //=================================================================水电煤
        private string life_user = "7000616";
        private string life_sign = "1475c4afa9b9f42b7c8b3913fc54df85";
        private string life_api = "http://lifeapi.salerwise.com/IWEC";
        public M_Yisi_Result Life_QueryBill(int proid, string account)
        {
            //成功:<?xml version='1.0' encoding='GB2312'?><billsquery><result>success</result><time>2016-06-14 15:20:00</time><proid>1065</proid><account>4050034644</account><yearmonth></yearmonth><username>张三</username><bills>7520</bills></billsquery>
            //错误:<?xml version='1.0' encoding='GB2312'?><billsquery><result>fail</result><time>2016-07-01 10:45:37</time><proid>1098</proid><account>69240005</account><yearmonth></yearmonth><username>账期号不能为空</username><bills>0</bills></billsquery>
            string yearmonth = "";
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string queryattr1 = "";
            string queryattr2 = "";
            string recordkey = MD5Make(life_user + proid + account + yearmonth + timestamp + queryattr1 + queryattr2, life_sign);
            string url = life_api + "/BillsQuery";//BillsQuery_test
            IDictionary<string, string> Parem = new Dictionary<string, string>();
            Parem.Add("UserNumber", life_user);
            Parem.Add("ProId", proid.ToString());
            Parem.Add("Account", account);
            Parem.Add("Yearmonth", yearmonth);
            Parem.Add("Timestamp", timestamp);
            Parem.Add("Queryattr1", queryattr1);
            Parem.Add("Queryattr2", queryattr2);
            Parem.Add("Recordkey", recordkey);
            WebUtils utilBLL = new WebUtils();
            string result = utilBLL.DoPost(url, Parem);
            M_Yisi_Result retMod =new  M_Yisi_Result();
            retMod.account = account;
            retMod.proid = proid;
            XmlHelper xmldoc = new XmlHelper(result, "//billsquery/");
            retMod.success = xmldoc.GetValue("result").Equals("success");
            if (retMod.success)
            {
                retMod.username = xmldoc.GetValue("username");
                string bill = xmldoc.GetValue("bills");
                retMod.money = (Convert.ToDouble(bill) / 100);
            }
            else
            {
                retMod.err = xmldoc.GetValue("username");
            }
            return retMod;
        }
        /// <summary>
        /// 付款充值
        /// </summary>
        public M_Yisi_Order Life_Charge(M_OrderList orderMod)
        {
            //成功:<esaipay><result>success</result><inOrderNumber>100001200912231009224321</inOrderNumber><outOrderNumber>out_2011_6_20_1234</outOrderNumber> <queryType>WEC</queryType> <payResult>2 </payResult> <remark>查询备注</remark> <recordKey>xxxx</recordKey> </esaipay>
            //fail:<esaipay><result>attrerr</result><time>2012-5-23 10:11:12</time><msg>参数错误</msg> </ esaipay > 

            string proid = orderMod.Extend.Split(':')[0], account = orderMod.Extend.Split(':')[1];
            double paymoney = orderMod.Ordersamount;
            //先用IRechargeList_WEC_test测试，暂时先不要用IRechargeList_WEC，它会真实缴费的
            string inordernumber = "IWEC" + life_user + DateTime.Now.ToString("yyyyMMddHHmmss") + function.GetRandomString(4, 2);
            string outordernumber = orderMod.OrderNo;
            string yearmonth = "";
            string starttime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            int timeout = 500;
            string attr1 = "";
            string attr2 = "";
            string recordkey = MD5Make(life_user + inordernumber + outordernumber + proid + account + paymoney + yearmonth + starttime + timeout + attr1 + attr2, life_sign);
            //string url = life_api+"/IRechargeList_WEC";
            string url = life_api + "/IRechargeList_WEC";//IRechargeList_WEC_test
            IDictionary<string, string> Parem = new Dictionary<string, string>();
            Parem.Add("UserNumber", life_user);
            Parem.Add("InOrderNumber", inordernumber);
            Parem.Add("OutOrderNumber", outordernumber);
            Parem.Add("ProId", proid);
            Parem.Add("Account", account);
            Parem.Add("PayMoney", paymoney.ToString());
            Parem.Add("YearMonth", yearmonth);
            Parem.Add("StartTime", starttime);
            Parem.Add("TimeOut", timeout.ToString());
            Parem.Add("Attr1", attr1);
            Parem.Add("Attr2", attr2);
            Parem.Add("RecordKey", recordkey);
            Parem.Add("Remark", "");
            WebUtils utilBLL = new WebUtils();
            string result = utilBLL.DoPost(url, Parem);
            //写入订单日志
            M_Yisi_Order ysMod = new M_Yisi_Order(orderMod);
            ysMod.PostResult = result;
            ysMod.ID = ysBll.Insert(ysMod);
            if (ysMod.PostResult.Contains("<result>success</result>"))
            {
                ysMod.Status = 1;
            }
            else { ysMod.Status = -1; }
            return ysMod;
        }
        /// <summary>
        /// 根据易赛订单号查询信息
        /// </summary>
        public string Life_ViewOrder(string yisiOrderNo) 
        {
            //<?xml version='1.0' encoding='GB2312'?><esaipay><result>success</result><inOrderNumber>IWEC7000616201606201622340577</inOrderNumber><outOrderNumber>DD20160620162234714</outOrderNumber><queryType>WEC</queryType><payMoney>3520</payMoney><payResult>4</payResult><remark>--</remark><recordKey>E1A3C0FC2F21C054</recordKey></esaipay>
            string inordernumber = yisiOrderNo;//tbxInNumber.Text.Trim();
            string outordernumber = "None";
            string queryType = "WEC";//固定为此
            string recordkey = MD5Make(life_user + inordernumber + outordernumber + queryType, life_sign); //ESaiPay.Common.DEncrypt.DESEncrypt.ESaiAes(usernumber + inordernumber + outordernumber + queryType + sign);

            //string url = m_APIUrl+"/IRechargeResult";
            string url = life_api + "/IRechargeResult";
            IDictionary<string, string> Parem = new Dictionary<string, string>();
            Parem.Add("UserNumber", life_user);
            Parem.Add("InOrderNumber", inordernumber);
            Parem.Add("OutOrderNumber", outordernumber);
            Parem.Add("queryType", queryType);
            Parem.Add("Remark", "");
            Parem.Add("RecordKey", recordkey);
            WebUtils utilBLL = new WebUtils();
            string result = utilBLL.DoPost(url, Parem);
            return result;
        }
        //=================================================================话费
        private string phone_user = "5000978";
        private string phone_sign = "c542a304bf55b7fa6153480b7c5ce861";
        private string phone_api = "http://www.esaipai.com/IF/EIFPHONE/IRechargeList_Phone";
        /// <summary>
        /// 话费充值无proid,但需要咨询其开通了哪些充值,我们自建ProID
        /// </summary>
        public M_Yisi_Order Phone_Charge(M_OrderList orderMod)
        {
            M_Yisi_Order ysMod = new M_Yisi_Order(orderMod);
            string phoneNumber = ysMod.account;
            int phoneMoney = (int)ysMod.paymoney;
            #region 参数设置
            /*用户编号,Esaipay支付平台用户编号. 如100001*/
            string userNumber = phone_user;
            /*4位随即数*/
            Random rd = new Random(unchecked((int)DateTime.Now.Ticks));
            string randomnum1 = "";
            for (int i = 0; i < 4; i++) { randomnum1 += rd.Next(0, 9).ToString(); }
            /*注意,外部订单号请根据业务逻辑生成,同一个订单请使用相同订单号提交,以免重复提交出现.*/
            //string outOrderNumber = userNumber + DateTime.Now.ToString("yyyyMMddHHmmss") + randomnum1;
            string outOrderNumber = orderMod.OrderNo;
            /*送单类型+用户编号+当前时间(yyyyMMddHHmmss格式) +4位随即数的字符串 绝对不能错...*/
            string inOrderNumber = "IP" + outOrderNumber;
            /*充值手机号码*/
            //string phoneNumber = "18897918710";
            /*号码所属省,直辖市.如果需要接口自动判断请填写 Auto*/
            string province = "Auto";
            /*号码所属城市.如果需要接口自动判断请填写 Auto.*/
            string city = "Auto";
            /*充值号码类型. 移动,电信,联通.如果需要接口自动判断请填写 Auto.*/
            string phoneClass = "Auto";
            /*销售价格,用户软件中统计利润,格式为小数点后保留4位.如果不需要此功能请填入.None.*/
            string sellPrice = "None";
            if (sellPrice != "None")
            {
                sellPrice = Math.Round(((Convert.ToDecimal(sellPrice)) + (Convert.ToDecimal("0.000001")))).ToString();
            }
            /*充值面额(正整数),具体支持面额请软件中查询*/
            //string phoneMoney = "50";
            /*StartTime订单提交时间格式(yyyy-MM-dd HH:mm:ss)*/
            string startTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            /*TimeOut超时设定,(300秒-7200秒),如果此字段字段传递参数异常,如低于或者高于限定值则按默认600秒处理*/
            string timeOut = "100000";
            /*Remark备注  明文(16字节),系统不会做任何处理,将会原样返回接口客户端.并且此参数不参与数据密匙验证*/
            string remark = "phone charge";
            #endregion
            string Parameter = (CreatIRechargeListParameter(userNumber, inOrderNumber, outOrderNumber, phoneNumber, province, city, phoneClass, phoneMoney.ToString(), sellPrice, startTime, timeOut, remark));
            ysMod.PostResult = new WebUtils().DoGet(phone_api + "?" + Parameter, null);
            ysMod.ID = ysBll.Insert(ysMod);
            return ysMod;
        }
        public string Phone_ViewOrder(string yisiOrderNo) 
        {
            return "";
        }
        //=================================================================流量
        private string flow_user = "8722015";
        private string flow_sign = "b0ae35c518cb7c46861b9ffc05ae9122";
        private string flow_api = "http://llbchongzhi.esaipai.com/";
        public M_Yisi_Order Flow_Charge(M_OrderList orderMod)
        {
            M_Yisi_Order ysMod = new M_Yisi_Order(orderMod);
            StringBuilder sb = new StringBuilder();
            var parameters = new Dictionary<string, string>();
            parameters.Add("UserNumber", flow_user);
            parameters.Add("OutOrderNumber",orderMod.OrderNo);//"ZL" + DateTime.Now.ToString("yyyyMMddHHmmssffff")
            parameters.Add("ProId", ysMod.ProID);
            parameters.Add("PhoneNumber", ysMod.account);
            parameters.Add("PayAmount", "1");//充值数据,默认为1
            parameters.Add("StartTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            parameters.Add("TimeOut", "1200");
            parameters.Add("Remark", "");
            var sign = SignTopRequest(parameters, flow_sign).ToLower();
            parameters.Add("RecordKey", sign);
            WebUtils webutils = new WebUtils();
            var url = flow_api + "/IRecharge_Flow";
            //sb.Append("提交请求数据：\r\n" + webutils.BuildGetUrl(url, parameters));
            var result = webutils.DoPost(url, parameters);
            ysMod.PostResult = result;
            //sb.Append("\r\n\r\n返回结果：" + result);
            if (!string.IsNullOrEmpty(result))
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(result);
                var inOrderNumberNode = doc.SelectSingleNode("/Esaipay/InOrderNumber/node()");
                //var outOrderNumberNode = doc.SelectSingleNode("/Esaipay/OutOrderNumber/node()");
                //var RresultNode = doc.SelectSingleNode("/Esaipay/Result/node()");
                //var remarkNode = doc.SelectSingleNode("/Esaipay/Remark/node()");
                //var recordKeyNode = doc.SelectSingleNode("/Esaipay/RecordKey/node()");
                ysMod.Status = 1;
                ysMod.inOrder = inOrderNumberNode != null ? inOrderNumberNode.Value : "";
                //ysMod.outOrder = outOrderNumberNode != null ? outOrderNumberNode.Value : "";
                //var Rresult = RresultNode != null ? RresultNode.Value : "";
                //var remark = remarkNode != null ? remarkNode.Value : "";
                //var recordKey = recordKeyNode != null ? recordKeyNode.Value : "";

                //txtInorderNumber.Text = inOrderNumber;

                //var checkdata = new Dictionary<string, string>();
                //checkdata.Add("InOrderNumber", inOrderNumber);
                //checkdata.Add("OutOrderNumber", outOrderNumber);
                //checkdata.Add("Result", Rresult);
                //checkdata.Add("Remark", remark);
                //var checkSign = SignTopRequest(checkdata, flow_sign);
                //if (checkSign != recordKey)
                //{
                //    //sb.Append("\r\n\r\n--------------返回结果:" + Rresult + "-----签名：正确----------------\r\n");
                //}
                //else
                //{
                //    //sb.Append("\r\n\r\n-------------返回结果:" + Rresult + "-----签名：错误-----------------\r\n");
                //}
            }
            else { ysMod.Status = -1; }
            ysMod.ID = ysBll.Insert(ysMod);
            return ysMod;
        }
        public string Flow_OrderView(string ysOrderNo)
        {
            var parameters = new Dictionary<string, string>();
            parameters.Add("UserNumber", flow_user);
            parameters.Add("InOrderNumber", ysOrderNo);//易赛订单号
            parameters.Add("OutOrderNumber", "");//我们的订单号
            parameters.Add("Remark", "");
            var sign = SignTopRequest(parameters, flow_sign).ToLower();
            parameters.Add("RecordKey", sign);
            var url = flow_api + "/IRechargeResult_Flow";
            WebUtils webutils = new WebUtils();
            return webutils.DoPost(url, parameters);
        }
        //=================================================================油卡
        private string oil_user = "50000027";
        private string oil_sign = "3761ad0eb69a22be5afee3e239d8fc50";
        private string oil_api = "";
        //---------通用
        private string CreatIRechargeListParameter(string userNumber, string inOrderNumber, string outOrderNumber, string phoneNumber, string province, string city, string phoneClass, string phoneMoney, string sellPrice, string startTime, string timeOut, string remark)
        {
            //UserNumber+ InOrderNumber + OutOrderNumber + PhoneNumber+ Province + City + PhoneClass + PhoneMoney(小数点后保留4位小数)+ SellPrice(小数点后保留4位小数) + StartTime+ TimeOut + Sign
            /*数据密匙32位,请注意保管*/
            /*记录字符串*/
            string Info = userNumber + inOrderNumber + outOrderNumber + phoneNumber + province + city + phoneClass + phoneMoney + sellPrice + startTime + timeOut;
            ///*数据验证密匙*/
            string RecordKey = MD5Make(Info, phone_sign);
            string url = "UserNumber=" + userNumber + "&InOrderNumber=" + inOrderNumber + "&OutOrderNumber=" + outOrderNumber + "&PhoneNumber=" + phoneNumber + "&Province=" + province + "&City=" + city + "&PhoneClass=" + phoneClass + "&PhoneMoney=" + phoneMoney + "&SellPrice=" + sellPrice + "&StartTime=" + startTime + "&TimeOut=" + timeOut + "&RecordKey=" + RecordKey + "&Remark=" + remark;
            return url;

            //Dictionary<string, string> Parem = new Dictionary<string, string>();
            //Parem.Add("UserNumber", UserNumber);
            //Parem.Add("InOrderNumber",inOrderNumber);
        }
        //流量使用
        private string SignTopRequest(IDictionary<string, string> parameters, string secret)
        {
            IEnumerator<KeyValuePair<string, string>> dem = parameters.GetEnumerator();

            StringBuilder query = new StringBuilder();
            string parameter = string.Empty;
            while (dem.MoveNext())
            {
                string value = dem.Current.Value;
                query.Append(value);
            }
            query.Append(secret);
            parameter = query.ToString();

            MD5 md5 = MD5.Create();
            byte[] bytes = md5.ComputeHash(Encoding.GetEncoding("GB2312").GetBytes(parameter));

            StringBuilder result = new StringBuilder(32);
            for (int i = 0; i < bytes.Length; i++)
                result.Append(bytes[i].ToString("x").PadLeft(2, '0'));
            //result.Length = 16;

            return result.ToString().Substring(0, 16);
        }
        private string MD5Make(string Info, string Sign)
        {
            string retStr;
            MD5CryptoServiceProvider m5 = new MD5CryptoServiceProvider();
            //创建md5对象
            byte[] inputBye;
            byte[] outputBye;
            inputBye = Encoding.GetEncoding("GB2312").GetBytes(Info + Sign);
            outputBye = m5.ComputeHash(inputBye);
            retStr = System.BitConverter.ToString(outputBye);
            retStr = retStr.Replace("-", "").ToUpper();
            return retStr.Substring(0, 16);
        }
    }
    public sealed class WebUtils
    {
        private int _timeout = 100000;
        /** 查询申请应答参数 **/
        protected static Hashtable esaipayQueryparameters = new Hashtable();
        /// <summary>
        /// 请求与响应的超时时间
        /// </summary>
        public int Timeout
        {
            get { return this._timeout; }
            set { this._timeout = value; }
        }
        /** 获取参数值 */
        public static string getParameter(string parameter)
        {

            string s = (string)esaipayQueryparameters[parameter];
            return (null == s) ? "" : s;
        }
        /** 设置参数值 */
        public static void setParameter(string parameter, string parameterValue)
        {
            esaipayQueryparameters = new Hashtable();
            if (parameter != null && parameter != "")
            {
                if (esaipayQueryparameters.Contains(parameter))
                {
                    esaipayQueryparameters.Remove(parameter);
                }

                esaipayQueryparameters.Add(parameter, parameterValue);
            }
        }
        #region 获取查询结果参数
        /** 获取参数值 */
        public static string getQueryParameter(string parameter)
        {
            string s = (string)esaipayQueryparameters[parameter];
            return (null == s) ? "" : s;
        }

        /** 设置参数值 */
        public static void setQueryParameter(string parameter, string parameterValue)
        {
            if (parameter != null && parameter != "")
            {
                if (esaipayQueryparameters.Contains(parameter))
                {
                    esaipayQueryparameters.Remove(parameter);
                }

                esaipayQueryparameters.Add(parameter, parameterValue);
            }
        }
        #endregion
        /// <summary>
        /// Base64解码
        /// </summary>
        /// <param name="Message"></param>
        /// <returns></returns>
        public static string Base64Decode(string Message)
        {
            if ((Message.Length % 4) != 0)
            {
                throw new ArgumentException("不是正确的BASE64编码，请检查。", "Message");
            }
            if (!System.Text.RegularExpressions.Regex.IsMatch(Message, "^[A-Z0-9/+=]*$", System.Text.RegularExpressions.RegexOptions.IgnoreCase))
            {
                throw new ArgumentException("包含不正确的BASE64编码，请检查。", "Message");
            }
            string Base64Code = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=";
            int page = Message.Length / 4;
            System.Collections.ArrayList outMessage = new System.Collections.ArrayList(page * 3);
            char[] message = Message.ToCharArray();
            for (int i = 0; i < page; i++)
            {
                byte[] instr = new byte[4];
                instr[0] = (byte)Base64Code.IndexOf(message[i * 4]);
                instr[1] = (byte)Base64Code.IndexOf(message[i * 4 + 1]);
                instr[2] = (byte)Base64Code.IndexOf(message[i * 4 + 2]);
                instr[3] = (byte)Base64Code.IndexOf(message[i * 4 + 3]);
                byte[] outstr = new byte[3];
                outstr[0] = (byte)((instr[0] << 2) ^ ((instr[1] & 0x30) >> 4));
                if (instr[2] != 64)
                {
                    outstr[1] = (byte)((instr[1] << 4) ^ ((instr[2] & 0x3c) >> 2));
                }
                else
                {
                    outstr[2] = 0;
                }
                if (instr[3] != 64)
                {
                    outstr[2] = (byte)((instr[2] << 6) ^ instr[3]);
                }
                else
                {
                    outstr[2] = 0;
                }
                outMessage.Add(outstr[0]);
                if (outstr[1] != 0)
                    outMessage.Add(outstr[1]);
                if (outstr[2] != 0)
                    outMessage.Add(outstr[2]);
            }
            byte[] outbyte = (byte[])outMessage.ToArray(Type.GetType("System.Byte"));
            return System.Text.Encoding.Default.GetString(outbyte);
        }
        /// <summary>
        /// 执行post请求
        /// </summary>
        /// <param name="Parameter"></param>
        /// <param name="timeout"></param>
        /// <param name="input_charset"></param>
        /// <param name="floorkey"></param>
        /// <returns></returns>
        public static string ToPost(string Parameter, int timeout, string input_charset, string floorkey)
        {
            string flg = "";
            try
            {
                #region 设置接口参数
                /*充值申请接口*/
                string esaiPayInterfaceUrl = "http://10.0.0.153:8080/Order/TaoBaoOpen";
                string valpairs = Parameter;
                UTF8Encoding encoding = new UTF8Encoding();
                byte[] b = encoding.GetBytes(valpairs);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(esaiPayInterfaceUrl);
                request.Timeout = timeout;
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = b.Length;
                request.Proxy = WebProxy.GetDefaultProxy();
                request.Proxy.Credentials = CredentialCache.DefaultCredentials;
                System.IO.Stream sw = request.GetRequestStream();
                sw.Write(b, 0, b.Length);
                sw.Close();
                #endregion
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        Stream resStream = response.GetResponseStream();
                        System.IO.StreamReader streamReader = new StreamReader(resStream, System.Text.Encoding.GetEncoding("UTF-8"));
                        string content = streamReader.ReadToEnd();
                        //textBox1.Text = content; 
                        streamReader.Close();
                        resStream.Close();

                        #region 处理返回参数
                        esaipayQueryparameters = new Hashtable();//初始化哈希
                        XmlDocument xmlDoc = new XmlDocument();
                        xmlDoc.LoadXml(content);
                        //<?xml version='1.0' encoding='GB2312'?><newesaipay><orderStatus>error</orderStatus><returnInfo >非法用户</returnInfo ><timeStamp >2012-07-25 14:18:51</timeStamp ></newesaipay>
                        XmlNode root = xmlDoc.SelectSingleNode("newesaipay");
                        XmlNodeList xnl = root.ChildNodes;
                        foreach (XmlNode xnf in xnl)
                        {
                            setQueryParameter(xnf.Name, xnf.InnerXml);
                        }
                        #endregion

                        #region 在这里处理查询返回结果

                        string result = getQueryParameter("orderStatus");//返回处理结果详见文档枚举
                        string msg = getQueryParameter("returnInfo");
                        string time = getQueryParameter("timeStamp");


                        if (result.Equals("success"))
                        {
                            flg = msg;

                            //根据自己公司内部业务进行处理
                            //txtfundmsg.Text = content;
                        }
                        else
                        {

                            flg = msg;

                            //根据自己公司内部业务进行处理
                            // txtfundmsg.Text = content;
                        }
                        #endregion

                    }

                    return flg;
                }

            }
            catch (Exception err)
            {

                return err.Message.ToString();


            }


        }
        /// <summary>
        /// 执行HTTP POST请求。
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="parameters">请求参数</param>
        /// <returns>HTTP响应</returns>
        public string DoPost(string url, IDictionary<string, string> parameters)
        {
            try
            {
                HttpWebRequest req = GetWebRequest(url, "POST");

                //string html="url="+url+"<br />";
                //foreach (var param in parameters)
                //{
                //    html += param.Key + "=" + param.Value + "</br>";
                //}

                //function.WriteErrMsg(html);

                req.ContentType = "application/x-www-form-urlencoded;charset=utf-8";

                byte[] postData = Encoding.UTF8.GetBytes(BuildQuery(parameters, "utf8"));
                System.IO.Stream reqStream = req.GetRequestStream();
                reqStream.Write(postData, 0, postData.Length);
                reqStream.Close();

                HttpWebResponse rsp = (HttpWebResponse)req.GetResponse();
                Encoding encoding = Encoding.GetEncoding(rsp.CharacterSet);
                return GetResponseAsString(rsp, encoding);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        /// <summary>
        /// 执行HTTP GET请求。
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="parameters">请求参数</param>
        /// <returns>HTTP响应</returns>
        public string DoGet(string url, IDictionary<string, string> parameters)
        {
            if (parameters != null && parameters.Count > 0)
            {
                if (url.Contains("?"))
                {
                    url = url + "&" + BuildQuery(parameters, "utf8");
                }
                else
                {
                    url = url + "?" + BuildQuery(parameters, "utf8");
                }
            }

            HttpWebRequest req = GetWebRequest(url, "GET");
            req.ContentType = "application/x-www-form-urlencoded;charset=utf-8";

            HttpWebResponse rsp = (HttpWebResponse)req.GetResponse();
            Encoding encoding = Encoding.GetEncoding(rsp.CharacterSet);
            return GetResponseAsString(rsp, encoding);
        }
        public bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        { //直接确认，否则打不开
            return true;
        }
        public HttpWebRequest GetWebRequest(string url, string method)
        {
            HttpWebRequest req = null;
            if (url.Contains("https"))
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                req = (HttpWebRequest)WebRequest.CreateDefault(new Uri(url));
            }
            else
            {
                req = (HttpWebRequest)WebRequest.Create(url);
            }

            req.ServicePoint.Expect100Continue = false;
            req.Method = method;
            req.KeepAlive = true;
            req.UserAgent = "Top4Net";
            req.Timeout = this._timeout;

            return req;
        }
        /// <summary>
        /// 把响应流转换为文本。
        /// </summary>
        /// <param name="rsp">响应流对象</param>
        /// <param name="encoding">编码方式</param>
        /// <returns>响应文本</returns>
        public string GetResponseAsString(HttpWebResponse rsp, Encoding encoding)
        {
            System.IO.Stream stream = null;
            StreamReader reader = null;

            try
            {
                // 以字符流的方式读取HTTP响应
                stream = rsp.GetResponseStream();
                reader = new StreamReader(stream, encoding);
                return reader.ReadToEnd();
            }
            finally
            {
                // 释放资源
                if (reader != null) reader.Close();
                if (stream != null) stream.Close();
                if (rsp != null) rsp.Close();
            }
        }
        /// <summary>
        /// 组装GET请求URL。
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="parameters">请求参数</param>
        /// <returns>带参数的GET请求URL</returns>
        public string BuildGetUrl(string url, IDictionary<string, string> parameters)
        {
            if (parameters != null && parameters.Count > 0)
            {
                if (url.Contains("?"))
                {
                    url = url + "&" + BuildQuery(parameters, "utf8");
                }
                else
                {
                    url = url + "?" + BuildQuery(parameters, "utf8");
                }
            }
            return url;
        }
        /// <summary>
        /// 验证时间戳验证
        /// </summary>
        /// <param name="Time"></param>
        /// <returns></returns>
        public static bool ValiTime(string Time)
        {
            bool pass = true;
            try
            {
                DateTime Valitime = Convert.ToDateTime(Time);
                System.TimeSpan ND = DateTime.Now - Valitime;
                int minutes = ND.Seconds;//时间差/分钟
                int hour = ND.Hours;//时间差/小时
                if (DateTime.Now > Valitime.AddMinutes(120))//时间戳效验 设定120分钟
                {
                    pass = false;
                }
            }
            catch (Exception)
            {
                pass = false;
            }
            return pass;
        }
        /// <summary>
        /// 组装普通文本请求参数。
        /// </summary>
        /// <param name="parameters">Key-Value形式请求参数字典</param>
        /// <returns>URL编码后的请求数据</returns>
        public static string BuildQuery(IDictionary<string, string> parameters, string encode)
        {
            StringBuilder postData = new StringBuilder();
            bool hasParam = false;

            IEnumerator<KeyValuePair<string, string>> dem = parameters.GetEnumerator();
            while (dem.MoveNext())
            {
                string name = dem.Current.Key;
                string value = dem.Current.Value;
                // 忽略参数名或参数值为空的参数
                if (!string.IsNullOrEmpty(name))//&& !string.IsNullOrEmpty(value)
                {
                    if (hasParam)
                    {
                        postData.Append("&");
                    }

                    postData.Append(name);
                    postData.Append("=");
                    if (encode == "gb2312")
                    {
                        postData.Append(HttpUtility.UrlEncode(value, Encoding.GetEncoding("gb2312")));
                    }
                    else if (encode == "utf8")
                    {
                        postData.Append(HttpUtility.UrlEncode(value, Encoding.UTF8));
                    }
                    else
                    {
                        postData.Append(value);
                    }
                    hasParam = true;
                }
            }

            return postData.ToString();
        }
    }
    public class XmlHelper
    {
        XmlDocument xmldoc = new XmlDocument();
        public string Root = "//";
        public XmlHelper(string xml, string root) { xmldoc.LoadXml(xml); Root = root; }
        public string GetValue(string name)
        {
            XmlNode node = xmldoc.SelectSingleNode(Root + name);
            if (node != null) { return node.InnerText; }
            else { return ""; }
        }
    }
    public class M_Yisi_Result 
    {
        //是否成功
        public bool success = false;
        public string orderno = "";//CMS订单号
        public string username = "";//户主名,用于水煤电
        public string account = "";
        public int proid = 0;
        public double money = 0;//需付金额 //从分转为实付金额
        public string err = "";//错误信息
    }
    public class M_Yisi_Order : M_Base
    {
        public int ID { get; set; }
        /// <summary>
        /// ZL订单号
        /// </summary>
        public string outOrder { get; set; }
        /// <summary>
        /// 易赛订单号
        /// </summary>
        public string inOrder { get; set; }
        /// <summary>
        /// 产品ID
        /// </summary>
        public string ProID { get; set; }
        /// <summary>
        /// 用户账户|手机号码
        /// </summary>
        public string account { get; set; }
        /// <summary>
        /// 付费金额
        /// </summary>
        public double paymoney { get; set; }
        /// <summary>
        /// 提交结果,是否正确提交入易赛
        /// </summary>
        public string PostResult { get; set; }
        /// <summary>
        /// 处理结果,提交后,再根据inOrder查询是否为成功状态
        /// </summary>
        public string Result { get; set; }
        /// <summary>
        /// 订单状态 0:待处理,1:提交成功,99易赛充值成功,-1:提交订单失败,-2:易赛充值失败
        /// </summary>
        public int Status { get; set; }
        public string Remind { get; set; }
        public DateTime CDate { get; set; }
        /// <summary>
        /// life,phone,oil,flow
        /// </summary>
        public string ZType { get; set; }
        public int UserID { get; set; }
        public override string TbName { get { return "ZL_Yisi_Order"; } }
        public override string PK { get { return "ID"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
        		        		{"ID","Int","4"},
        		        		{"outOrder","NVarChar","500"},
        		        		{"inOrder","NVarChar","500"},
        		        		{"ProID","NVarChar","100"},
        		        		{"account","NVarChar","500"},
        		        		{"paymoney","Money","8"},
        		        		{"Result","NVarChar","4000"},
        		        		{"Status","Int","4"},
        		        		{"Remind","NVarChar","1000"},
        		        		{"CDate","DateTime","8"},
                                {"PostResult","NVarChar","4000"},
                                {"ZType","NVarChar","50"},
                                {"UserID","Int","4"}
        };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_Yisi_Order model = this;
            if (model.CDate <= DateTime.MinValue) { model.CDate = DateTime.Now; }
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.outOrder;
            sp[2].Value = model.inOrder;
            sp[3].Value = model.ProID;
            sp[4].Value = model.account;
            sp[5].Value = model.paymoney;
            sp[6].Value = model.Result;
            sp[7].Value = model.Status;
            sp[8].Value = model.Remind;
            sp[9].Value = model.CDate;
            sp[10].Value = model.PostResult;
            sp[11].Value = model.ZType;
            sp[12].Value = model.UserID;
            return sp;
        }
        public M_Yisi_Order GetModelFromReader(DbDataReader rdr)
        {
            M_Yisi_Order model = new M_Yisi_Order();
            model.ID = ConvertToInt(rdr["ID"]);
            model.outOrder = ConverToStr(rdr["outOrder"]);
            model.inOrder = ConverToStr(rdr["inOrder"]);
            model.ProID = ConverToStr(rdr["ProID"]);
            model.account = ConverToStr(rdr["account"]);
            model.paymoney = ConverToDouble(rdr["paymoney"]);
            model.Result = ConverToStr(rdr["Result"]);
            model.Status = ConvertToInt(rdr["Status"]);
            model.Remind = ConverToStr(rdr["Remind"]);
            model.CDate = ConvertToDate(rdr["CDate"]);
            model.PostResult = ConverToStr(rdr["PostResult"]);
            model.ZType = ConverToStr(rdr["ZType"]);
            model.UserID = ConvertToInt(rdr["UserID"]);
            rdr.Close();
            return model;
        }
        public M_Yisi_Order(M_OrderList orderMod)
        {
            outOrder = orderMod.OrderNo;
            paymoney = orderMod.Ordersamount;
            account = orderMod.Extend.Split(':')[0];
            ProID = orderMod.Extend.Split(':')[1];
            ZType = orderMod.Extend.Split(':')[2];
            UserID = orderMod.Userid;
            //-------------------------------
            CDate = DateTime.Now;
            PostResult = "";
            Result = "";
            Status = 0;
        }
        public M_Yisi_Order() { }
    }
    public class B_Yisi_Order
    {
        private string PK, TbName = "";
        private M_Yisi_Order initMod = new M_Yisi_Order();
        public B_Yisi_Order()
        {
            PK = initMod.PK;
            TbName = initMod.TbName;
        }
        public int Insert(M_Yisi_Order model)
        {
            return DBCenter.Insert(model);
        }
        public bool UpdateByID(M_Yisi_Order model)
        {
            return DBCenter.UpdateByID(model, model.ID);
        }
        public bool Del(int ID)
        {
            return DBCenter.Del(TbName, PK, ID);
        }
        public M_Yisi_Order SelReturnModel(int ID)
        {
            if (ID < 1) { return null; }
            using (DbDataReader reader = DBCenter.SelReturnReader(TbName, PK, ID))
            {
                if (reader.Read())
                {
                    return initMod.GetModelFromReader(reader);
                }
                else
                {
                    return null;
                }
            }
        }
        //根据易赛订单查询对应的模型
        public M_Yisi_Order SelModelByInOrder(string order) 
        {
            if (!string.IsNullOrEmpty(order)) { return null; }
            return null;
        }
        public M_Yisi_Order SelModelByOutOrder(string order)
        {
            if (!string.IsNullOrEmpty(order)) { return null; }
            string where = "outOrder=@order";
            List<SqlParameter> sp = new List<SqlParameter>() { new SqlParameter("order", order.Trim()) };
            using (DbDataReader reader = DBCenter.SelReturnReader(TbName, where, sp.ToArray()))
            {
                if (reader.Read())
                {
                    return initMod.GetModelFromReader(reader);
                }
                else
                {
                    return null;
                }
            }
        }
        public DataTable Sel()
        {
            return DBCenter.Sel(TbName, "", PK + " DESC");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ztype">类型</param>
        /// <param name="uid">用户ID,为0全部</param>
        /// <param name="order">订单号</param>
        /// <returns></returns>
        public DataTable Sel(string ztype, int uid, string order)
        {
            string where = " 1=1 ";
            List<SqlParameter> sp = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(order))
            {
                sp.Add(new SqlParameter("order", "%" + order + "%"));
                where = " AND (A.inOrder LIKE @order OR A.outOrder LIKE @order)";
            }
            if (uid > 0) { where += " AND A.UserID=" + uid; }
            if (!ztype.Equals("all")) { sp.Add(new SqlParameter("ztype", ztype)); where += " AND A.ZType=@ztype"; }
            return DBCenter.JoinQuery("A.*", TbName, "ZL_Orderinfo", "A.outOrder=B.OrderNo", where, "A.ID DESC", sp.ToArray());
        }
        //----------------------Tools
        public string GetStatusStr(int status)
        {
            switch (status)
            {
                case 0:
                    return "未处理";
                default:
                    return status.ToString();
            }
        }
    }
}
