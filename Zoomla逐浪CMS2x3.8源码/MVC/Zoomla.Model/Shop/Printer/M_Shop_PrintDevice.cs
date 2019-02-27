using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ZoomLa.Model.Shop
{
    public class M_Shop_PrintDevice : M_Base
    {
        public int ID { get; set; }
        /// <summary>
        /// 设备编码
        /// </summary>
        public string DeviceNo { get; set; }
        /// <summary>
        /// 商户编码
        /// </summary>
        public string MemberCode { get; set; }
        /// <summary>
        /// API密钥
        /// </summary>
        public string SecurityKey { get; set; }
        /// <summary>
        /// 激活日期
        /// </summary>
        public DateTime Since { get; set; }
        /// <summary>
        /// 最后一次通信时刻
        /// </summary>
        public DateTime LastConnected { get; set; }
        //---------------------------------
        /// <summary>
        /// 连接状态，离线/在线
        /// </summary>
        public string DeviceStatus { get; set; }
        /// <summary>
        /// 纸张状态，正常/缺纸
        /// </summary>
        public string PaperStatus { get; set; }
        /// <summary>
        /// 安全校检字符串，不存入数据库
        /// </summary>
        public string SecurityCode { get; set; }
        //---------------------------------
        /// <summary>
        /// 别名
        /// </summary>
        public string Alias { get; set; }
        /// <summary>
        /// 店铺名称
        /// </summary>
        public string ShopName { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remind { get; set; }
        /// <summary>
        /// 是否是默认打印机
        /// </summary>
        public int IsDefault { get; set; }
        public override string TbName { get { return "ZL_Shop_PrintDevice"; } }
        public override string[,] FieldList()
        {
            string[,] fields = {
                                  {"ID","Int","4"},
                                  {"DeviceNo","NVarChar","200"},
                                  {"MemberCode","NVarChar","200"},
                                  {"SecurityKey","NVarChar","200"},
                                  {"Since","DateTime","8" },
                                  {"LastConnected","DateTime","8"},
                                  {"Alias","NVarChar","50" },
                                  {"ShopName","NVarChar","50" },
                                  {"Remind","NVarChar","200" },
                                  {"IsDefault","Int","4"}
                               };
            return fields;
        }
        public override SqlParameter[] GetParameters()
        {
            M_Shop_PrintDevice model = this;
            if (Since <= DateTime.MinValue) { Since = DateTime.Now; }
            if (LastConnected <= DateTime.MinValue) { LastConnected = DateTime.Now; }
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.DeviceNo;
            sp[2].Value = model.MemberCode;
            sp[3].Value = model.SecurityKey;
            sp[4].Value = model.Since;
            sp[5].Value = model.LastConnected;
            sp[6].Value = model.Alias;
            sp[7].Value = model.ShopName;
            sp[8].Value = model.Remind;
            sp[9].Value = model.IsDefault;
            return sp;
        }
        public M_Shop_PrintDevice GetModelFromReader(DbDataReader rdr)
        {
            M_Shop_PrintDevice model = new M_Shop_PrintDevice();
            model.ID = ConvertToInt(rdr["ID"]);
            model.DeviceNo = ConverToStr(rdr["DeviceNo"]);
            model.MemberCode = ConverToStr(rdr["MemberCode"]);
            model.SecurityKey = ConverToStr(rdr["SecurityKey"]);
            model.Since = ConvertToDate(rdr["Since"]);
            model.LastConnected = ConvertToDate(rdr["LastConnected"]);
            model.Alias = ConverToStr(rdr["Alias"]);
            model.ShopName = ConverToStr(rdr["ShopName"]);
            model.Remind = ConverToStr(rdr["Remind"]);
            model.IsDefault = ConvertToInt(rdr["IsDefault"] ?? 0);
            rdr.Close();
            return model;
        }
    }
}
