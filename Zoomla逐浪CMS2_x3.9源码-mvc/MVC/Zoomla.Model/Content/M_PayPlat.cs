/// <summary>
/// 支付平台模型
/// </summary>
using System.Data.SqlClient;
using System.Data;
using System;
namespace ZoomLa.Model
{
    public class M_PayPlat:M_Base
    {
        #region 字段定义
        /// <summary>
        /// 平台分类
        /// </summary>
        public int PayClass { get; set; }
        /// <summary>
        /// 平台ID
        /// </summary>
        public int PayPlatID { get; set; }
        /// <summary>
        /// 平台名
        /// </summary>
        public string PayPlatName { get; set; }
        /// <summary>
        /// 商户账号ID
        /// </summary>
        public string AccountID { get; set; }
        /// <summary>
        /// 平台安全校验码
        /// </summary>
        public string MD5Key { get; set; }
        /// <summary>
        /// 手续费
        /// </summary>
        public float Rate { get; set; }
        /// <summary>
        /// 排序序号
        /// </summary>
        public int OrderID { get; set; }
        /// <summary>
        /// 是否禁用
        /// </summary>
        public bool IsDisabled { get; set; }
        /// <summary>
        /// 是否默认平台
        /// </summary>
        public bool IsDefault { get; set; }
        /// <summary>
        /// 卖家账号Email 支付宝
        /// </summary>
        public string SellerEmail { get; set; }
        /// <summary>
        /// 是否为空
        /// </summary>
        public bool IsNull { get; private set; }
        public string payType { get; set; }
        public string leadtoGroup { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string PayPlatinfo { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int UID { get; set; }
        /// <summary>
        /// 私钥文件路径
        /// </summary>
        public string PrivateKey { get; set; }
        /// <summary>
        /// 公钥文件路径
        /// </summary>
        public string PublicKey { get; set; }
        /// <summary>
        /// 现用于银行方证书
        /// </summary>
        public string Other { get; set; }
        #endregion
        public override string PK { get { return "PayPlatID"; } }
        public override string TbName { get { return "ZL_PayPlat"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"PayPlatID","Int","4"},
                                  {"UID","Int","4"},
                                  {"PayClass","Int","4"},
                                  {"PayPlatName","NVarChar","255"},
                                  {"AccountID","NVarChar","255"}, 
                                  {"MD5Key","NVarChar","255"},
                                  {"SellerEmail","NVarChar","255"},
                                  {"IsDisabled","Bit","4"},
                                  {"IsDefault","Bit","4"}, 
                                  {"Rate","Float","8"},
                                  {"OrderID","Int","4"},
                                  {"payType","NVarChar","50"}, 
                                  {"leadtoGroup","NVarChar","500"}, 
                                  {"PayPlatinfo","NVarChar","1000"},
                                  {"PrivateKey","NVarChar","200"},
                                  {"PublicKey","NVarChar","200"},
                                  {"Other","NVarChar","3000"}
                                 };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_PayPlat model = this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.PayPlatID;
            sp[1].Value = model.UID;
            sp[2].Value = model.PayClass;
            sp[3].Value = model.PayPlatName;
            sp[4].Value = model.AccountID;
            sp[5].Value = model.MD5Key;
            sp[6].Value = model.SellerEmail;
            sp[7].Value = model.IsDisabled;
            sp[8].Value = model.IsDefault;
            sp[9].Value = model.Rate;
            sp[10].Value = model.OrderID;
            sp[11].Value = model.payType;
            sp[12].Value = model.leadtoGroup;
            sp[13].Value = model.PayPlatinfo;
            sp[14].Value = model.PrivateKey;
            sp[15].Value = model.PublicKey;
            sp[16].Value = model.Other;
            return sp;
        }
        public M_PayPlat GetModelFromReader(SqlDataReader rdr)
        {
            M_PayPlat model = new M_PayPlat();
            model.PayPlatID = Convert.ToInt32(rdr["PayPlatID"]);
            model.UID = Convert.ToInt32(rdr["UID"]);
            model.PayClass = Convert.ToInt32(rdr["PayClass"]);
            model.PayPlatName = rdr["PayPlatName"].ToString();
            model.AccountID = rdr["AccountID"].ToString();
            model.MD5Key = rdr["MD5Key"].ToString();
            model.SellerEmail = rdr["SellerEmail"].ToString();
            model.IsDisabled = Convert.ToBoolean(rdr["IsDisabled"]);
            model.IsDefault = Convert.ToBoolean(rdr["IsDefault"]);
            model.Rate = Convert.ToInt64(rdr["Rate"]);
            model.OrderID = Convert.ToInt32(rdr["OrderID"]);
            model.payType = rdr["payType"].ToString();
            model.leadtoGroup = rdr["leadtoGroup"].ToString();
            model.PayPlatinfo = rdr["PayPlatinfo"].ToString();
            model.PrivateKey = ConverToStr(rdr["PrivateKey"]);
            model.PublicKey = ConverToStr(rdr["PublicKey"]);
            model.Other = ConverToStr(rdr["Other"]);
            rdr.Dispose();
            return model;
        }
        //其他的支付平台除虚拟币外不支持
        //3:银联,12:支付宝[即时到账],15:支付宝单网银,16:摩宝,21:微信支付,22:宝付,23:南昌工商银行,27:汇潮支付,99:线下支付,100:货到付款
        public enum Plat {UnionPay=3, Alipay_Instant = 12, Alipay_Bank = 15, Mobo = 16, WXPay = 21, BaoFo = 22, ICBC_NC = 23,EPay95=24,Ebatong=25,CCB=26, ECPSS=27, Offline = 99, CashOnDelivery = 100 }
    }
}