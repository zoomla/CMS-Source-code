using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;


namespace ZoomLa.Model.Plat
{
    public class M_Plat_Like : M_Base
    {
        public int ID { get; set; }
        /// <summary>
        /// 能力中心消息id
        /// </summary>
        public int MsgID { get; set; }
        /// <summary>
        /// 点赞人
        /// </summary>
        public int CUser { get; set; }
        public DateTime CDate { get; set; }
        /// <summary>
        /// 受赞用户
        /// </summary>
        public int TagUser { get; set; }
        public string Source { get; set; }
        public override string TbName
        {
            get
            {
                return "ZL_Plat_Like";
            }
        }

        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                     {"ID","Int","4"},
                                     {"MsgID","Int","4" },
                                     {"CUser","Int","4" },
                                     {"CDate","DateTime","8" },
                                     {"TagUser","Int","4" },
                                     {"Source","NVarChar","100" }
                                  };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            var model = this;
            if (model.CDate <= DateTime.MinValue) { model.CDate = DateTime.Now; }
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.MsgID;
            sp[2].Value = model.CUser;
            sp[3].Value = model.CDate;
            sp[4].Value = model.TagUser;
            sp[5].Value = model.Source;
            return sp;
        }
        public M_Plat_Like GetModelFromReader(DbDataReader rdr)
        {
            M_Plat_Like model = new M_Plat_Like();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.MsgID = ConvertToInt(rdr["MsgID"]);
            model.CUser = ConvertToInt(rdr["CUser"]);
            model.CDate =ConvertToDate(rdr["CDate"]);
            model.TagUser = ConvertToInt(rdr["TagUser"]);
            model.Source = ConverToStr(rdr["Source"]);
            rdr.Close();
            return model;
        }
    }
}
