using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace ZoomLa.Model
{
    public class M_WX_APPID:M_Base
    {
        public int ID { get; set; }
        /// <summary>
        /// 别名
        /// </summary>
        public string  Alias { get; set; }
        public string APPID { get; set; }
        public string Secret { get; set; }
        public DateTime CDate { get; set; }
        public int Status { get; set; }
        //是否默认
        public int IsDefault { get; set; }
        /// <summary>
        /// 微信公众号(登录邮箱|手机号),公众号设置--注册信息
        /// </summary>
        public string WxNo { get; set; }
        /// <summary>
        /// 原始ID(用于客户提交的信息ToUserName),公众号设置--注册信息
        /// </summary>
        public string OrginID { get; set; }
        public string WelStr { get; set; }
        /// <summary>
        /// Token缓存中,不置入数据库
        /// </summary>
        public string Token { get; set; }
        //Token生效时间
        public DateTime TokenDate { get; set; }
        /// <summary>
        /// JSAPI授权,给予WXAPI使用,请勿直接调用
        /// </summary>
        public string JSAPITicket { get; set; }
        /// <summary>
        /// JSAPI的获取时间,7200秒后过期
        /// </summary>
        public DateTime JSAPIDate { get; set; }
        /// <summary>
        /// 支付APPID(同于APPID,不需要配置)
        /// </summary>
        public string Pay_APPID { get { return APPID; } set { APPID = value; } }
        /// <summary>
        /// 支付Secret(同于Secret,不需要配置)
        /// </summary>
        public string Pay_Secret { get { return Secret; } set { Secret = value; } }
        /// <summary>
        /// 商户号MCH_ID
        /// </summary>
        public string Pay_AccountID { get; set; }
        /// <summary>
        /// 商户密钥
        /// </summary>
        public string Pay_Key { get; set; }
        /// <summary>
        /// 证书虚拟路径
        /// </summary>
        public string Pay_SSLPath { get; set; }
        /// <summary>
        /// 证书密钥
        /// </summary>
        public string Pay_SSLPassword { get; set; }
        public override string TbName { get { return "ZL_WX_APPID"; } }
        public M_WX_APPID() { EmptyDeal(); }
        public override string[,] FieldList()
        {
            string[,] fields = { 
                                  {"ID","Int","4"},
                                  {"Alias","NVarChar","100"},
                                  {"APPID","NVarChar","200"},
                                  {"Secret","NVarChar","200"},
                                  {"Token","NVarChar","500"},
                                  {"CDate","DateTime","8"},
                                  {"Status","Int","4"},
                                  {"IsDefault","Int","4"},
                                  {"TokenDate","DateTime","8"},
                                  {"WxNo","NVarChar","100"},
                                  {"WelStr","NVarChar","4000"},
                                  {"OrginID","NVarChar","100"},
                                  {"Pay_AccountID","NVarChar","200"},
                                  {"Pay_Key","NVarChar","200"},
                                  {"Pay_SSLPath","NVarChar","500"},
                                  {"Pay_SSLPassword","NVarChar","500"},
                                  
                               };
            return fields;
        }
        public SqlParameter[] GetParameters(M_WX_APPID model)
        {
            EmptyDeal();
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.Alias;
            sp[2].Value = model.APPID;
            sp[3].Value = model.Secret;
            sp[4].Value = "";//token
            sp[5].Value = model.CDate;
            sp[6].Value = model.Status;
            sp[7].Value = model.IsDefault;
            sp[8].Value = model.TokenDate;
            sp[9].Value = model.WxNo;
            sp[10].Value = model.WelStr;
            sp[11].Value = model.OrginID;
            sp[12].Value = model.Pay_AccountID;
            sp[13].Value = model.Pay_Key;
            sp[14].Value=model.Pay_SSLPath;
            sp[15].Value=model.Pay_SSLPassword;
            return sp;
        }
        public M_WX_APPID GetModelFromReader(SqlDataReader rdr)
        {
            M_WX_APPID model = new M_WX_APPID();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.Alias = ConverToStr(rdr["Alias"]);
            model.APPID = ConverToStr(rdr["APPID"]);
            model.Secret = ConverToStr(rdr["Secret"]);
            model.Token = "";
            model.CDate = ConvertToDate(rdr["CDate"]);
            model.Status = ConvertToInt(rdr["Status"]);
            model.IsDefault = ConvertToInt(rdr["IsDefault"]);
            model.TokenDate = ConvertToDate(rdr["TokenDate"]);
            model.WxNo = ConverToStr(rdr["WxNo"]);
            model.WelStr = ConverToStr(rdr["WelStr"]);
            model.OrginID = ConverToStr(rdr["OrginID"]);
            model.Pay_AccountID = ConverToStr(rdr["Pay_AccountID"]);
            model.Pay_Key = ConverToStr(rdr["Pay_Key"]);
            model.Pay_SSLPath = ConverToStr(rdr["Pay_SSLPath"]);
            model.Pay_SSLPassword = ConverToStr(rdr["Pay_SSLPassword"]);
            rdr.Close();
            return model;
        }
        public void EmptyDeal()
        {
            if (CDate <= DateTime.MinValue) CDate = DateTime.Now;
            if (TokenDate <= DateTime.MinValue) TokenDate = DateTime.Now;
        }
    }
}
