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
        /// 支付APPID
        /// </summary>
        public string Pay_APPID { get; set; }
        /// <summary>
        /// 支付Secret
        /// </summary>
        public string Pay_Secret { get; set; }
        /// <summary>
        /// 商户号
        /// </summary>
        public string Pay_AccountID { get; set; }
        /// <summary>
        /// 商户密钥
        /// </summary>
        public string Pay_Key { get; set; }
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
                                  {"Pay_APPID","NVarChar","200"},
                                  {"Pay_Secret","NVarChar","200"},
                                  {"Pay_AccountID","NVarChar","200"},
                                  {"Pay_Key","NVarChar","200"}
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
            sp[12].Value = model.Pay_APPID;
            sp[13].Value = model.Pay_Secret;
            sp[14].Value = model.Pay_AccountID;
            sp[15].Value = model.Pay_Key;
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
            model.Pay_APPID = ConverToStr(rdr["Pay_APPID"]);
            model.Pay_Secret = ConverToStr(rdr["Pay_Secret"]);
            model.Pay_AccountID = ConverToStr(rdr["Pay_AccountID"]);
            model.Pay_Key = ConverToStr(rdr["Pay_Key"]);
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
