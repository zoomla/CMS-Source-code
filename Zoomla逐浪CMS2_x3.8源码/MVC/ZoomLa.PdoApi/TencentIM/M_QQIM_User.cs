using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ZoomLa.Model;

namespace ZoomLa.PdoApi.TencentIM
{
    public class M_QQIM_User : M_Base
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        /// <summary>
        /// IM用户名
        /// </summary>
        public string IM_Identity { get; set; }
        /// <summary>
        /// IM用户签名,用于登录,有效期180天
        /// </summary>
        public string Sign { get; set; }
        /// <summary>
        /// 用户签名生成日期
        /// </summary>
        public DateTime SignDate { get; set; }
        public DateTime CDate { get; set; }
        public override string TbName { get { return "ZL_API_QQIMUser"; } }
        public override string PK { get { return "ID"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
        		        		{"ID","Int","4"},
        		        		{"UserID","Int","4"},
        		        		{"IM_Identity","NVarChar","500"},
        		        		{"Sign","VarChar","4000"},
        		        		{"SignDate","DateTime","8"},
        		        		{"CDate","DateTime","8"}
        };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_QQIM_User model = this;
            SqlParameter[] sp = GetSP();
            if (model.SignDate <= DateTime.MinValue) { model.SignDate = DateTime.Now; }
            if (model.CDate <= DateTime.MinValue) { model.CDate = DateTime.Now; }
            sp[0].Value = model.ID;
            sp[1].Value = model.UserID;
            sp[2].Value = model.IM_Identity;
            sp[3].Value = model.Sign;
            sp[4].Value = model.SignDate;
            sp[5].Value = model.CDate;
            return sp;
        }
        public M_QQIM_User GetModelFromReader(DbDataReader rdr)
        {
            M_QQIM_User model = new M_QQIM_User();
            model.ID = ConvertToInt(rdr["ID"]);
            model.UserID = ConvertToInt(rdr["UserID"]);
            model.IM_Identity = ConverToStr(rdr["IM_Identity"]);
            model.Sign = ConverToStr(rdr["Sign"]);
            model.SignDate = ConvertToDate(rdr["SignDate"]);
            model.CDate = ConvertToDate(rdr["CDate"]);
            rdr.Close();
            return model;
        }
    }
}
