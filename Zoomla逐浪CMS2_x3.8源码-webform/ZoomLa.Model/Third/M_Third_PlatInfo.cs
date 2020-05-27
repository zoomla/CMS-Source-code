using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ZoomLa.Model.Third
{
    public class M_Third_PlatInfo : M_Base
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string APPID { get; set; }
        public string APPKey { get; set; }
        public string APPSecret { get; set; }
        /// <summary>
        /// 对应平台的用户名与密码,有些平台需要这些换取信息
        /// </summary>
        public string UserName { get; set; }
        public string UserPwd { get; set; }
        public string CallBack { get; set; }
        /// <summary>
        /// 标识,根据标识取出对应的Key与Secret,标识不可重复
        /// </summary>
        public string Flag { get; set; }
        public int ZStatus { get; set; }
        public DateTime CDate { get; set; }
        public string Remind { get; set; }
        public override string TbName { get { return "ZL_Third_PlatInfo"; } }
        public override string PK { get { return "ID"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
        		        		{"ID","Int","4"},
        		        		{"Name","NVarChar","100"},
        		        		{"APPKey","NVarChar","500"},
        		        		{"APPSecret","NVarChar","500"},
        		        		{"CallBack","NVarChar","2000"},
        		        		{"Flag","NVarChar","100"},
        		        		{"ZStatus","Int","4"},
        		        		{"CDate","DateTime","8"},
        		        		{"Remind","NVarChar","200"},
                                {"UserName","NVarChar","500"},
                                {"UserPwd","NVarChar","500"},
                                {"APPID","NVarChar" ,"50" }
        };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_Third_PlatInfo model = this;
            if (model.CDate <= DateTime.MinValue) { model.CDate = DateTime.Now; }
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.Name;
            sp[2].Value = model.APPKey;
            sp[3].Value = model.APPSecret;
            sp[4].Value = model.CallBack;
            sp[5].Value = model.Flag;
            sp[6].Value = model.ZStatus;
            sp[7].Value = model.CDate;
            sp[8].Value = model.Remind;
            sp[9].Value = model.UserName;
            sp[10].Value = model.UserPwd;
            sp[11].Value = model.APPID;
            return sp;
        }
        public M_Third_PlatInfo GetModelFromReader(DbDataReader rdr)
        {
            M_Third_PlatInfo model = new M_Third_PlatInfo();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.Name = ConverToStr(rdr["Name"]);
            model.APPKey = ConverToStr(rdr["APPKey"]);
            model.APPSecret = ConverToStr(rdr["APPSecret"]);
            model.CallBack = ConverToStr(rdr["CallBack"]);
            model.Flag = ConverToStr(rdr["Flag"]);
            model.ZStatus = ConvertToInt(rdr["ZStatus"]);
            model.CDate = ConvertToDate(rdr["CDate"]);
            model.Remind = ConverToStr(rdr["Remind"]);
            model.UserName = ConverToStr(rdr["UserName"]);
            model.UserPwd = ConverToStr(rdr["UserPwd"]);
            model.APPID = ConverToStr(rdr["APPID"]);
            rdr.Close();
            return model;
        }
    }
}
