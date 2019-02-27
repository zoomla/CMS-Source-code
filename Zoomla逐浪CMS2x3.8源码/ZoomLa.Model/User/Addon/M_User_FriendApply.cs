using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace ZoomLa.Model.User
{
    public class M_User_FriendApply : M_Base
    {
        public int ID { get; set; }
        /// <summary>
        /// 好友申请人
        /// </summary>
        public int UserID { get; set; }
        /// <summary>
        /// 好友受邀人
        /// </summary>
        public int TUserID { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remind { get; set; }
        /// <summary>
        /// 申请状态
        /// </summary>
        public int ZStatus { get; set; }
        public DateTime CDate { get; set; }

        public override string TbName { get { return "ZL_User_FriendApply"; } }
        public override string PK { get { return "ID"; } }

        public override string[,] FieldList()
        {
            string[,] Tablelist = {
        		        		{"ID","Int","4"},
        		        		{"UserID","Int","4"},
        		        		{"TUserID","Int","4"},
        		        		{"Remind","NVarChar","500"},
        		        		{"ZStatus","Int","4"},
        		        		{"CDate","DateTime","8"}
        };
            return Tablelist;
        }

        public override SqlParameter[] GetParameters()
        {
            if (CDate <= DateTime.MinValue) { CDate = DateTime.Now; }
            M_User_FriendApply model = this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.UserID;
            sp[2].Value = model.TUserID;
            sp[3].Value = model.Remind;
            sp[4].Value = model.ZStatus;
            sp[5].Value = model.CDate;
            return sp;
        }
        public M_User_FriendApply GetModelFromReader(DbDataReader rdr)
        {
            M_User_FriendApply model = new M_User_FriendApply();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.UserID = Convert.ToInt32(rdr["UserID"]);
            model.TUserID = ConvertToInt(rdr["TUserID"]);
            model.Remind = ConverToStr(rdr["Remind"]);
            model.ZStatus = ConvertToInt(rdr["ZStatus"]);
            model.CDate = ConvertToDate(rdr["CDate"]);
            rdr.Close();
            return model;
        }
    }
}