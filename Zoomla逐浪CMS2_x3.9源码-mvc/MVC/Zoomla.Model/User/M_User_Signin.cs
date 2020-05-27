using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ZoomLa.Model
{
    public class M_User_Signin : M_Base
    {
        public int ID { get; set; }
        /// <summary>
        /// 签到用户
        /// </summary>
        public int UserID { get; set; }
        /// <summary>
        /// 签到用户备注
        /// </summary>
        public string UserText { get; set; }
        public int Status { get; set; }
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 系统备注
        /// </summary>
        public string Remind { get; set; }

        public override string TbName { get { return "ZL_User_SignIn"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
        		        		{"ID","Int","4"},
        		        		{"UserID","Int","4"},
        		        		{"UserText","NVarChar","200"},
        		        		{"Status","Int","4"},
        		        		{"CreateTime","DateTime","8"},
        		        		{"Remind","NVarChar","200"}
        };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_User_Signin model = this;
            SqlParameter[] sp = GetSP();
            if (model.CreateTime <= DateTime.MinValue) { model.CreateTime = DateTime.Now; }
            sp[0].Value = model.ID;
            sp[1].Value = model.UserID;
            sp[2].Value = model.UserText;
            sp[3].Value = model.Status;
            sp[4].Value = model.CreateTime;
            sp[5].Value = model.Remind;
            return sp;
        }
        public M_User_Signin GetModelFromReader(SqlDataReader rdr)
        {
            M_User_Signin model = new M_User_Signin();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.UserID = Convert.ToInt32(rdr["UserID"]);
            model.UserText = ConverToStr(rdr["UserText"]);
            model.Status = ConvertToInt(rdr["Status"]);
            model.CreateTime = ConvertToDate(rdr["CreateTime"]);
            model.Remind = ConverToStr(rdr["Remind"]);
            rdr.Close();
            return model;
        }
    }
}
