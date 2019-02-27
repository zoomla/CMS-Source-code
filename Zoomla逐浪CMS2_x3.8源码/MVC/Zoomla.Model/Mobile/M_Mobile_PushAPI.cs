using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ZoomLa.Model.Mobile
{       
    //是否扩展为会局缓存
    public class M_Mobile_PushAPI : M_Base
    {
        public int ID { get; set; }
        /// <summary>
        /// 别名
        /// </summary>
        public string Alias { get; set; }
        /// <summary>
        /// 接口Key
        /// </summary>
        public string APPKey { get; set; }
        /// <summary>
        /// 接口密钥
        /// </summary>
        public string APPSecret { get; set; }
        public DateTime CDate { get; set; }
        /// <summary>
        /// 1:极光,2:百度推送
        /// </summary>
        public int Plat { get; set; }
        public override string TbName { get { return "ZL_Mobile_PushAPI"; } }
        public override string PK { get { return "ID"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
        		        		{"ID","Int","4"},
        		        		{"Alias","NVarChar","50"},
        		        		{"APPKey","NVarChar","1000"},
        		        		{"APPSecret","NVarChar","1000"},
        		        		{"CDate","DateTime","8"},
        		        		{"Plat","Int","4"}
        };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_Mobile_PushAPI model = this;
            if (model.CDate <= DateTime.MinValue) { model.CDate = DateTime.Now; }
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.Alias;
            sp[2].Value = model.APPKey;
            sp[3].Value = model.APPSecret;
            sp[4].Value = model.CDate;
            sp[5].Value = model.Plat;
            return sp;
        }
        public M_Mobile_PushAPI GetModelFromReader(DbDataReader rdr)
        {
            M_Mobile_PushAPI model = new M_Mobile_PushAPI();
            model.ID = ConvertToInt(rdr["ID"]);
            model.Alias = ConverToStr(rdr["Alias"]);
            model.APPKey = ConverToStr(rdr["APPKey"]);
            model.APPSecret = ConverToStr(rdr["APPSecret"]);
            model.CDate = ConvertToDate(rdr["CDate"]);
            model.Plat = ConvertToInt(rdr["Plat"]);
            rdr.Close();
            return model;
        }
    }
}
