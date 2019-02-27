using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ZoomLa.Model.User
{
    public class M_User_InviteCode : M_Base
    {
        public int ID { get; set; }
        /// <summary>
        /// 邀请码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 所属用户
        /// </summary>
        public int UserID { get; set; }
        public string UserName { get; set; }
        /// <summary>
        /// 通过该邀请码加入的会员组
        /// </summary>
        public int JoinGroup { get; set; }
        /// <summary>
        /// 如果是用户前台创建,该值不为空
        /// </summary>
        public int CUser { get; set; }
        /// <summary>
        /// 如果是管理员后台创建,该值不为空
        /// </summary>
        public int CAdmin { get; set; }
        public DateTime CDate { get; set; }
        public string Remind { get; set; }
        /// <summary>
        /// 0:未使用,99:已使用
        /// </summary>
        public int ZStatus { get; set; }
        /// <summary>
        /// 使用该邀请码的人
        /// </summary>
        public string UsedUserName { get; set; }
        public string UsedDate { get; set; }
        public int UsedUserID { get; set; }
        /// <summary>
        /// 流水号,用于记录批次
        /// </summary>
        public string Flow { get; set; }
        public M_User_InviteCode() { ZStatus = 0; UsedUserName = ""; }
        public override string TbName { get { return "ZL_User_InviteCode"; } }
        public override string PK { get { return "ID"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
        		        		{"ID","Int","4"},
        		        		{"Code","NVarChar","500"},
        		        		{"UserID","Int","4"},
        		        		{"UserName","NVarChar","200"},
        		        		{"JoinGroup","Int","4"},
        		        		{"CUser","Int","4"},
        		        		{"CAdmin","Int","4"},
        		        		{"CDate","DateTime","8"},
        		        		{"Remind","NVarChar","500"},
                                {"ZStatus","Int","4"},
                                {"UsedUserName","NVarChar","500"},
                                {"Flow","NVarChar","100"},
                                {"UsedDate","NVarChar","100"},
                                {"UsedUserID","Int","4"}
        };
            return Tablelist;
        }

        public override SqlParameter[] GetParameters()
        {
            M_User_InviteCode model = this;
            if (model.CDate <= DateTime.MinValue) { model.CDate = DateTime.Now; }
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.Code;
            sp[2].Value = model.UserID;
            sp[3].Value = model.UserName;
            sp[4].Value = model.JoinGroup;
            sp[5].Value = model.CUser;
            sp[6].Value = model.CAdmin;
            sp[7].Value = model.CDate;
            sp[8].Value = model.Remind;
            sp[9].Value = model.ZStatus;
            sp[10].Value = model.UsedUserName;
            sp[11].Value = model.Flow;
            sp[12].Value = model.UsedDate;
            sp[13].Value = model.UsedUserID;
            return sp;
        }
        public M_User_InviteCode GetModelFromReader(DbDataReader rdr)
        {
            M_User_InviteCode model = new M_User_InviteCode();
            model.ID = ConvertToInt(rdr["ID"]);
            model.Code = ConverToStr(rdr["Code"]);
            model.UserID = ConvertToInt(rdr["UserID"]);
            model.UserName = ConverToStr(rdr["UserName"]);
            model.JoinGroup = ConvertToInt(rdr["JoinGroup"]);
            model.CUser = ConvertToInt(rdr["CUser"]);
            model.CAdmin = ConvertToInt(rdr["CAdmin"]);
            model.CDate = ConvertToDate(rdr["CDate"]);
            model.Remind = ConverToStr(rdr["Remind"]);
            model.ZStatus = ConvertToInt(rdr["ZStatus"]);
            model.UsedUserName = ConverToStr(rdr["UsedUserName"]);
            model.Flow = ConverToStr(rdr["Flow"]);
            model.UsedDate = ConverToStr(rdr["UsedDate"]);
            model.UsedUserID = ConvertToInt(rdr["UsedUserID"]);
            rdr.Close();
            return model;
        }
    }
}
