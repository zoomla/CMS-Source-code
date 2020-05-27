using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ZoomLa.Model.MIS
{
    /// <summary>
    /// 签章日志
    /// </summary>
    public class M_OA_DocSigned : M_Base
    {
        public int ID { get; set; }
        /// <summary>
        /// 签章的流程
        /// </summary>
        public int ProID { get; set; }
        public int Step { get; set; }
        public int SignID { get; set; }
        public string VPath { get; set; }
        public int UserID { get; set; }
        public DateTime CDate { get; set; }
        public int AppID { get; set; }
        //同样存SignName,UserName和VPath,方便后期验证
        public string SignName { get; set; }
        public string CUName { get; set; }

        public override string TbName { get { return "ZL_OA_DocSigned"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
        		        		{"ID","Int","4"},
        		        		{"ProID","Int","4"},
        		        		{"Step","Int","4"},
        		        		{"SignID","Int","4"},
        		        		{"VPath","NVarChar","300"},
        		        		{"UserID","Int","4"},
        		        		{"CDate","DateTime","8"},
                                {"AppID","Int","4"},
                                {"SignName","NVarChar","100"},
                                {"CUName","NVarChar","100"}
        };
            return Tablelist;
        }
        public SqlParameter[] GetParameters(M_OA_DocSigned model)
        {
            if (CDate <= DateTime.MinValue) CDate = DateTime.Now;
            string[,] strArr = FieldList();
            SqlParameter[] sp = new SqlParameter[strArr.Length];

            for (int i = 0; i < strArr.GetLength(0); i++)
            {
                sp[i] = new SqlParameter("@" + strArr[i, 0], (SqlDbType)Enum.Parse(typeof(SqlDbType), strArr[i, 1]), Convert.ToInt32(strArr[i, 2]));
            }
            sp[0].Value = model.ID;
            sp[1].Value = model.ProID;
            sp[2].Value = model.Step;
            sp[3].Value = model.SignID;
            sp[4].Value = model.VPath;
            sp[5].Value = model.UserID;
            sp[6].Value = model.CDate;
            sp[7].Value = model.AppID;
            sp[8].Value = model.SignName;
            sp[9].Value = model.CUName;
            return sp;
        }
        public M_OA_DocSigned GetModelFromReader(SqlDataReader rdr)
        {
            M_OA_DocSigned model = new M_OA_DocSigned();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.ProID = Convert.ToInt32(rdr["ProID"]);
            model.Step = ConvertToInt(rdr["Step"]);
            model.SignID = Convert.ToInt32(rdr["SignID"]);
            model.VPath = rdr["VPath"].ToString();
            model.UserID = Convert.ToInt32(rdr["UserID"]);
            model.CDate = Convert.ToDateTime(rdr["CDate"]);
            model.AppID = ConvertToInt(rdr["AppID"]);
            model.SignName = ConverToStr(rdr["SignName"]);
            model.CUName = ConverToStr(rdr["CUName"]);
            rdr.Close();
            return model;
        }
    }
}
