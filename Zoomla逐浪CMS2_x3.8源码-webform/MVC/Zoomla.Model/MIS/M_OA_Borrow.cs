using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ZoomLa.Model.MIS
{
    [Serializable]
    //ZL_OA_Borrow
    public class M_OA_Borrow : M_Base
    {

        public int ID { get; set; }
        public int UserID { get; set; }
        public string Uids { get; set; }
        public string UNames { get; set; }
        public string DocIDS { get; set; }
        public string DocTitles { get; set; }
        public DateTime CDate { get; set; }
        /// <summary>
        /// 起始时间,默认为当前时间
        /// </summary>
        public DateTime SDate { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EDate { get; set; }
        public string Remind { get; set; }
        public override string TbName { get { return "ZL_OA_Borrow"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
        		        		{"ID","Int","4"},
        		        		{"UserID","Int","4"},
        		        		{"Uids","VarChar","4000"},
        		        		{"UNames","NVarChar","4000"},
        		        		{"DocIDS","VarChar","4000"},
        		        		{"DocTitles","NVarChar","4000"},
        		        		{"CDate","DateTime","8"},
        		        		{"SDate","DateTime","8"},
        		        		{"EDate","DateTime","8"},
                                {"Remind","NVarChar","4000"}
        };
            return Tablelist;
        }
        public SqlParameter[] GetParameters(M_OA_Borrow model)
        {
            if (CDate <= DateTime.MinValue) { CDate = DateTime.Now; }
            if (SDate <= DateTime.MinValue) { SDate = DateTime.Now; }
            string[,] strArr = FieldList();
            SqlParameter[] sp = new SqlParameter[strArr.Length];

            for (int i = 0; i < strArr.GetLength(0); i++)
            {
                sp[i] = new SqlParameter("@" + strArr[i, 0], (SqlDbType)Enum.Parse(typeof(SqlDbType), strArr[i, 1]), Convert.ToInt32(strArr[i, 2]));
            }
            sp[0].Value = model.ID;
            sp[1].Value = model.UserID;
            sp[2].Value = model.Uids;
            sp[3].Value = model.UNames;
            sp[4].Value = model.DocIDS;
            sp[5].Value = model.DocTitles;
            sp[6].Value = model.CDate;
            sp[7].Value = model.SDate;
            sp[8].Value = model.EDate;
            sp[9].Value = model.Remind;
            return sp;
        }
        public M_OA_Borrow GetModelFromReader(SqlDataReader rdr)
        {
            M_OA_Borrow model = new M_OA_Borrow();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.UserID = Convert.ToInt32(rdr["UserID"]);
            model.Uids = rdr["Uids"].ToString();
            model.UNames = rdr["UNames"].ToString();
            model.DocIDS = rdr["DocIDS"].ToString();
            model.DocTitles = rdr["DocTitles"].ToString();
            model.CDate = Convert.ToDateTime(rdr["CDate"]);
            model.SDate = Convert.ToDateTime(rdr["SDate"]);
            model.EDate = Convert.ToDateTime(rdr["EDate"]);
            model.Remind = ConverToStr(rdr["Remind"]);
            rdr.Close();
            return model;
        }
    }
}
