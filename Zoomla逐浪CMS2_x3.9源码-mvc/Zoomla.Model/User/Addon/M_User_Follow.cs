using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ZoomLa.Model.User
{
    public class M_User_Follow : M_Base
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        /// <summary>
        /// 被关注用户
        /// </summary>
        public int TUserID { get; set; }
        public DateTime CDate { get; set; }
        /// <summary>
        /// 关注的平台(暂时不分)
        /// </summary>
        public string ZSource { get; set; }
        public override string TbName { get { return "ZL_User_Follow"; } }
        public override string PK { get { return "ID"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
        		        		{"ID","Int","4"},
        		        		{"UserID","Int","4"},
        		        		{"TUserID","Int","4"},
        		        		{"CDate","DateTime","8"},
        		        		{"ZSource","NVarChar","50"}
        };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_User_Follow model = this;
            if (model.CDate <= DateTime.MinValue) { model.CDate = DateTime.Now; }
            if (string.IsNullOrEmpty(ZSource)) { ZSource = "user"; }
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.UserID;
            sp[2].Value = model.TUserID;
            sp[3].Value = model.CDate;
            sp[4].Value = model.ZSource;
            return sp;
        }
        public M_User_Follow GetModelFromReader(DbDataReader rdr)
        {
            M_User_Follow model = new M_User_Follow();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.UserID = ConvertToInt(rdr["UserID"]);
            model.TUserID = ConvertToInt(rdr["TUserID"]);
            model.CDate = ConvertToDate(rdr["CDate"]);
            model.ZSource = ConverToStr(rdr["ZSource"]);
            rdr.Close();
            return model;
        }
    }
}
