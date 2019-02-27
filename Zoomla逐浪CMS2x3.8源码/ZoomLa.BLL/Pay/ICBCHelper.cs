using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ZoomLa.Common;
using ZoomLa.Model;
/*
 * 用于工商网银支付
 */
namespace ZoomLa.BLL.API
{
    public class ICBCHelper
    {
        /// <summary>
        /// 注意格式顺序,大小写,不能有多余空格和换行
        /// </summary>
        /// <param name="model"></param>
        /// <param name="merid">商户ID</param>
        /// <param name="merAcct">商户帐号</param>
        /// <param name="siteurl">根域名,本地为localhost</param>
        /// <param name="notifyurl">接受通知的地址</param>
        /// <returns>XML明文</returns>
        public string SpliceTranData(M_Payment model, string merid, string merAcct, string siteurl, string notifyurl)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<?xml version=\"1.0\" encoding=\"GBK\" standalone=\"no\"?>");
            sb.Append("<B2CReq>");
            sb.Append("<interfaceName>ICBC_PERBANK_B2C</interfaceName>");
            sb.Append("<interfaceVersion>1.0.0.11</interfaceVersion>");
            sb.Append("<orderInfo>");
            sb.Append("<orderDate>" + DateTime.Now.ToString("yyyyMMddHHmmss") + "</orderDate>");//因为不能与其服务器差太多时间,最好实时生成
            sb.Append("<curType>001</curType>");
            sb.Append("<merID>"+merid+"</merID>");//商户ID,1502EC24375838
            sb.Append("<subOrderInfoList>");
            sb.Append("<subOrderInfo>");
            sb.Append("<orderid>" + model.PayNo.Trim() + "</orderid>");
            sb.Append("<amount>" + (model.MoneyReal * 100).ToString("f0") + "</amount>");//以分为单位,所以必须*100,无小数位
            sb.Append("<installmentTimes>1</installmentTimes>");
            sb.Append("<merAcct>" + merAcct + "</merAcct>");//帐户:1502200129300005002
            sb.Append("<goodsID>776453</goodsID>");
            sb.Append("<goodsName>Goods</goodsName>");
            sb.Append("<goodsNum>1</goodsNum>");
            sb.Append("<carriageAmt>0</carriageAmt>");//已含运费金额
            sb.Append("</subOrderInfo>");
            sb.Append("</subOrderInfoList>");
            sb.Append("</orderInfo>");
            sb.Append("<custom>");
            sb.Append("<verifyJoinFlag>0</verifyJoinFlag>");
            sb.Append("<Language>ZH_CN</Language>");
            sb.Append("</custom>");
            sb.Append("<message>");
            sb.Append("<creditType>2</creditType>");
            sb.Append("<notifyType>HS</notifyType>");
            sb.Append("<resultType>1</resultType>");//1只接受成功消息
            sb.Append("<merReference>" + siteurl.Trim() + "</merReference>");//域名必须与用户付款前一页面的根域名一致,不带http,如*.z01.com
            sb.Append("<merCustomIp></merCustomIp>");
            sb.Append("<goodsType>1</goodsType>");
            sb.Append("<merCustomID>166</merCustomID>");
            sb.Append("<merCustomPhone></merCustomPhone>");
            sb.Append("<goodsAddress>hereisnot</goodsAddress>");
            sb.Append("<merOrderRemark></merOrderRemark>");
            sb.Append("<merHint></merHint>");//商城提示
            sb.Append("<remark1></remark1>");
            sb.Append("<remark2></remark2>");
            sb.Append("<merURL>"+notifyurl.Trim()+"</merURL>");//需要http,80,不能使用https,看是否异步回调,http://localhost:86/pay/paynotify.aspx
            sb.Append("<merVAR></merVAR>");
            sb.Append("</message>");
            sb.Append("</B2CReq>");
            return sb.ToString();
        }
        /// <summary>
        /// 二进制读取证书,并转为Base64(用于公钥)
        /// </summary>
        public string ReadCertToBase64(string cervpath)
        {
            string ppath = function.VToP(cervpath);
            if (!File.Exists(ppath)) { throw new Exception(cervpath + ",证书文件不存在"); }
            FileStream fsMyfile = new FileStream(ppath, FileMode.Open, FileAccess.Read);
            BinaryReader brMyfile = new BinaryReader(fsMyfile);
            byte[] filebyte = new byte[brMyfile.BaseStream.Length];
            brMyfile.BaseStream.Read(filebyte, 0, filebyte.Length);
            fsMyfile.Close(); fsMyfile.Dispose(); brMyfile.Dispose();
            return Convert.ToBase64String(filebyte);
        }
        //int ret = obj.verifySign(obj.base64dec(trandata.Replace("%2B", "+").Replace("%3D", "=")), Server.MapPath("admin.crt"), signmsg.Replace("%2B", "+").Replace("%3D", "=").Replace("%2F", "/"));
        //T1.Text = ret.ToString();
        ////string detext = obj.base64dec(trandata);
        ////T1.Text= obj.base64dec(trandata.Replace("%2B","+").Replace("%3D","=")); ;
        ////T1.Text = obj.base64dec(signmsg.Replace("%2B", "+").Replace("%3D", "=").Replace("%2F","/"));
    }
}
