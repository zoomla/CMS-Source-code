using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ZoomLa.Model.Message
{
    public class M_Guest_Medals : M_Base
    {
        public int ID { get; set; }
        /// <summary>
        /// 勋章种类
        /// 1:网友勋章;2:版主勋章;3:系统勋章
        /// </summary>
        public int MedalID { get; set; }
        public int UserID { get; set; }
        public int BarID { get; set; }
        /// <summary>
        /// 勋章颁发者id
        /// -1:系统管理员
        /// </summary>
        public int Sender { get; set; }
        public DateTime CDate { get; set; }
        public override string TbName { get { return "ZL_Guest_Medals"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                {"ID","Int","4"},
                                {"medalid","Int","4" },
                                {"UserID","Int","4" },
                                {"BarID","Int","4" },
                                {"CDate","DateTime","8" },
                                {"Sender","Int","4" }
            };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            var model = this;
            if (model.CDate <= DateTime.MinValue) { model.CDate = DateTime.Now; }
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.MedalID;
            sp[2].Value = model.UserID;
            sp[3].Value = model.BarID;
            sp[4].Value = model.CDate;
            sp[5].Value = model.Sender;
            return sp;
        }
        public M_Guest_Medals GetModelFromReader(DbDataReader rdr)
        {
            M_Guest_Medals model = new M_Guest_Medals();
            model.ID = ConvertToInt(rdr["ID"]);
            model.MedalID = ConvertToInt(rdr["medalid"]);
            model.UserID = ConvertToInt(rdr["UserID"]);
            model.BarID = ConvertToInt(rdr["BarID"]);
            model.CDate = ConvertToDate(rdr["CDate"]);
            model.Sender = ConvertToInt(rdr["Sender"]);
            rdr.Close();
            return model;
        }
    }
}
