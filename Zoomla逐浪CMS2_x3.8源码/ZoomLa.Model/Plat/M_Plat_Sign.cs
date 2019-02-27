using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ZoomLa.Model.User
{
    public class M_Plat_Sign : M_Base
    {
        public int ID { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID { get; set; }
        /// <summary>
        /// IP地址
        /// </summary>
        public string IP { get; set; }
        public string IPLocation { get; set; }
        /// <summary>
        /// 类别,0:签到,1:签退
        /// </summary>
        public int ZType { get; set; }
        /// <summary>
        /// 状态,0:正常,1:迟到,2:早退
        /// </summary>
        public int State { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CDate { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remind { get; set; }

        public override string TbName { get { return "ZL_Plat_Sign"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                {"ID","Int","4"},
                                {"UserID","Int","4"},
                                {"IP","NVarChar","200" },
                                {"IPLocation","NVarChar","300" },
                                {"ZType","Int","4" },
                                {"State","Int","4" },
                                {"CDate","DateTime","8"},
                                {"Remind","NVarChar","200"}
        };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_Plat_Sign model = this;
            if (CDate <= DateTime.MinValue) { CDate = DateTime.Now; }
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.UserID;
            sp[2].Value = model.IP;
            sp[3].Value = model.IPLocation;
            sp[4].Value = model.ZType;
            sp[5].Value = model.State;
            sp[6].Value = model.CDate;
            sp[7].Value = model.Remind;
            return sp;
        }
        public M_Plat_Sign GetModelFromReader(DbDataReader rdr)
        {
            M_Plat_Sign model = new M_Plat_Sign();
            model.ID = ConvertToInt(rdr["ID"]);
            model.UserID = ConvertToInt(rdr["UserID"]);
            model.CDate = ConvertToDate(rdr["CDate"]);
            model.ZType = ConvertToInt(rdr["ZType"]);
            model.State = Convert.ToInt32(rdr["State"]);
            model.IP = rdr["IP"].ToString();
            model.Remind = rdr["Remind"].ToString();
            model.IPLocation = rdr["IPLocation"].ToString();
            rdr.Close();
            return model;
        }
    }
}
