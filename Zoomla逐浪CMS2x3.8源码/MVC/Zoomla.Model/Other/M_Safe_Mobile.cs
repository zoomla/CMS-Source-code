using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;

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
        /// <summary>
        /// 来源,对应页面处理
        /// </summary>
        public string Source { get; set; }
        /// <summary>
        /// 校验码
        /// </summary>
        public string VCode { get; set; }
        /// <summary>
        /// 0未使用,99已使用
        /// </summary>
        public int ZStatus { get; set; }
        public DateTime CDate { get; set; }
        public M_Safe_Mobile() { ZStatus = 0; VCode = ""; }
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
                                {"CDate","DateTime","8" },
                                {"Source","NVarChar","100" },
                                {"VCode","NVarChar","50" },
                                {"ZStatus","Int","8" }
            };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_Safe_Mobile model = this;
            if (model.CDate <= DateTime.MinValue) { model.CDate = DateTime.Now; }
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.Phone;
            sp[2].Value = model.IP;
            sp[3].Value = model.CDate;
            sp[4].Value = model.Source;
            sp[5].Value = model.VCode;
            sp[6].Value = model.ZStatus;
            return sp;
        }
        public M_Safe_Mobile GetModelFromReader(DbDataReader rdr)
        {
            M_Safe_Mobile model = new M_Safe_Mobile();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.Phone = ConverToStr(rdr["Phone"]);
            model.IP = ConverToStr(rdr["IP"]);
            model.CDate = ConvertToDate(rdr["CDate"]);
            model.Source=ConverToStr(rdr["Source"]);
            model.VCode=ConverToStr(rdr["VCode"]);
            model.ZStatus=ConvertToInt(rdr["ZStatus"]);
            rdr.Close();
            return model;
        }

    }
}
