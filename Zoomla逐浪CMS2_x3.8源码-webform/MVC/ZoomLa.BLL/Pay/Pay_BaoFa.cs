using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZoomLa.Common;

namespace ZoomLa.BLL.API
{
    public class Pay_BaoFa
    {
        //strMemberID = ConfigurationManager.AppSettings["MemberID"];//商户号
        //strPayID = Request.Params["PayID"];//招商银行是1001,为空则是宝付网关页
        //strTradeDate = DateTime.Now.ToString("yyyyMMddHHmmss");
        //strTransID = strMemberID + DateTime.Now.ToString("MMddHHmmss");//商户订单号（交易流水号）(建议使用商户订单号加上贵方的唯一标识号)
        //strOrderMoney = (float.Parse(Request.Params["OrderMoney"]) * 100).ToString();//订单金额，需要和卡面额一致(此处以分为单位)
        //strProductName = "";//商品名称
        //strAmount = "1";//商品数量，为1
        //strUsername = "";
        //strAdditionalInfo = "";
        //strPageUrl = ConfigurationManager.AppSettings["PageUrl"]; //客户端跳转地址
        //strReturnUrl = ConfigurationManager.AppSettings["ReturnUrl"];//服务器端返回地址
        //strNoticeType = "1";//0 不跳转 1 会跳转
        //strMd5Key = ConfigurationManager.AppSettings["Md5key"];//密钥 双方约定
        //strTerminalID = ConfigurationManager.AppSettings["TerminalID"];//终端
        //strInterfaceVersion = ConfigurationManager.AppSettings["InterfaceVersion"];//版本 当前为4.0请勿修改 
        //strKeyType = ConfigurationManager.AppSettings["KeyType"]; //加密方式默认1 MD5

        public string GetMd5Sign(string _MerchantID, string _PayID, string _TradeDate, string _TransID,
       string _OrderMoney, string _Page_url, string _Return_url, string _NoticeType, string _Md5Key)
        {
            string mark = "|";
            string str = _MerchantID + mark
                        + _PayID + mark
                        + _TradeDate + mark
                        + _TransID + mark
                        + _OrderMoney + mark
                        + _Page_url + mark
                        + _Return_url + mark
                        + _NoticeType + mark
                        + _Md5Key;
            return StringHelper.MD5(str);
        }
        public string BuildForm(string url, Dictionary<string, string> dics, string method = "post", string taraget = "_blank")
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<form id='payform' name='payform' action='" + url + "' method=\"" + method + "\" target=\"" + taraget + "\">");
            if (dics != null)//有些支付要求用链接直接跳转
            {
                foreach (var item in dics)
                {
                    builder.Append("<input type=\"hidden\" name=\"" + item.Key + "\" value=\"" + item.Value + "\"/>");
                }
            }
            builder.Append("<input type='submit' value='确认支付'></form>");
            return builder.ToString();
        }
        /// <summary>
        /// 通过两个状态获取订单结果的中文描述
        /// </summary>
        /// <param name="result"></param>
        /// <param name="resultDesc"></param>
        /// <returns></returns>
        public string GetErrorInfo(string result, string resultDesc)
        {
            string retInfo = "";
            if (result == "1")
                return "支付成功";
            else
            {
                switch (resultDesc)
                {
                    case "0000":
                        retInfo = "充值失败";
                        break;
                    case "0001":
                        retInfo = "系统错误";
                        break;
                    case "0002":
                        retInfo = "订单超时";
                        break;
                    case "0003":
                        retInfo = "订单状态异常";
                        break;
                    case "0004":
                        retInfo = "无效商户";
                        break;
                    case "0015":
                        retInfo = "卡号或卡密错误";
                        break;
                    case "0016":
                        retInfo = "不合法的IP地址";
                        break;
                    case "0018":
                        retInfo = "卡密已被使用";
                        break;
                    case "0019":
                        retInfo = "订单金额错误";
                        break;
                    case "0020":
                        retInfo = "支付的类型错误";
                        break;
                    case "0021":
                        retInfo = "卡类型有误";
                        break;
                    case "0022":
                        retInfo = "卡信息不完整";
                        break;
                    case "0023":
                        retInfo = "卡号，卡密，金额不正确";
                        break;
                    case "0024":
                        retInfo = "不能用此卡继续做交易";
                        break;
                    case "0025":
                        retInfo = "订单无效";
                        break;
                    default:
                        retInfo = "支付失败";
                        break;
                }
                return retInfo;
            }
        }
    }
}
