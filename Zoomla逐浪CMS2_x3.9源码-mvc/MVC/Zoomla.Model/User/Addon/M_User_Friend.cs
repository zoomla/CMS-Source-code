using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace ZoomLa.Model.User
{
    public class M_User_Friend : M_Base
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public int TUserID { get; set; }
        /// <summary>
        /// 好友组
        /// </summary>
        public int FGroupID { get; set; }
        /// <summary>
        /// 好友类型
        /// </summary>
        public int FType { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int ZStatus { get; set; }
        /// <summary>
        /// 申请备注
        /// </summary>
        public string Remind { get; set; }
        public DateTime CDate { get; set; }

        public override string TbName { get { return "ZL_User_Friend"; } }
        public override string PK { get { return "ID"; } }

        public override string[,] FieldList()
        {
            string[,] Tablelist = {
        		        		{"ID","Int","4"},
        		        		{"UserID","Int","4"},
        		        		{"TUserID","Int","4"},
        		        		{"FGroupID","Int","4"},
        		        		{"FType","Int","4"},
        		        		{"ZStatus","Int","4"},
        		        		{"Remind","NVarChar","500"},
        		        		{"CDate","DateTime","8"}
        };
            return Tablelist;
        }

        public override SqlParameter[] GetParameters()
        {
            M_User_Friend model = this;
            if (CDate <= DateTime.MinValue) { CDate = DateTime.Now; }
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.UserID;
            sp[2].Value = model.TUserID;
            sp[3].Value = model.FGroupID;
            sp[4].Value = model.FType;
            sp[5].Value = model.ZStatus;
            sp[6].Value = model.Remind;
            sp[7].Value = model.CDate;
            return sp;
        }
        public M_User_Friend GetModelFromReader(DbDataReader rdr)
        {
            M_User_Friend model = new M_User_Friend();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.UserID = ConvertToInt(rdr["UserID"]);
            model.TUserID = ConvertToInt(rdr["TUserID"]);
            model.FGroupID = ConvertToInt(rdr["FGroupID"]);
            model.FType = ConvertToInt(rdr["FType"]);
            model.ZStatus = ConvertToInt(rdr["ZStatus"]);
            model.Remind = ConverToStr(rdr["Remind"]);
            model.CDate = ConvertToDate(rdr["CDate"]);
            rdr.Close();
            return model;
        }
    }
}
