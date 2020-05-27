using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace ZoomLa.Model
{
    public class M_Safe_Mobile : M_Base
    {
        public int ID { get; set; }
        /// <summary>
        /// 接收验证码的手机
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 请求验证码的IP
        /// </summary>
        public string IP { get; set; }
        public DateTime CDate { get; set; }

        public override string TbName
        {
            get
            { return "ZL_Safe_Mobile"; }
        }

        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                {"ID","Int","4" },
                                {"Phone","NVarChar","100" },
                                {"IP","NVarChar","500" },
                                {"CDate","DateTime","8" }
            };
            return Tablelist;
        }
        public SqlParameter[] GetParameters(M_Safe_Mobile model)
        {
            if (model.CDate <= DateTime.MinValue) { model.CDate = DateTime.Now; }
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.Phone;
            sp[2].Value = model.IP;
            sp[3].Value = model.CDate;
            return sp;
        }
        public M_Safe_Mobile GetModelFromReader(SqlDataReader rdr)
        {
            M_Safe_Mobile model = new M_Safe_Mobile();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.Phone = ConverToStr(rdr["Phone"]);
            model.IP = ConverToStr(rdr["IP"]);
            model.CDate = ConvertToDate(rdr["CDate"]);
            rdr.Close();
            return model;
        }

    }
}
