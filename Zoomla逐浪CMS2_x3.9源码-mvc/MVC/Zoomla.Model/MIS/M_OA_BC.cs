using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ZoomLa.Model
{
    public class M_OA_BC
    {
        public int ID { get; set; }
        public string BCName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Remind { get; set; }
        public DateTime AddTime { get; set; }
        public string BackColor { get; set; }
        public M_OA_BC()
        {

        }

        public string TbName = "ZL_OA_BC";
        public string PK = "ID";

        public string[,] FieldList()
        {
            string[,] Tablelist = {
        	            {"ID","Int","4"},            
                        {"BCName","NVarChar","50"},            
                        {"StartTime","DateTime","8"},            
                        {"EndTime","DateTime","8"},            
                        {"Remind","NVarChar","200"},            
                        {"AddTime","DateTime","8"},
                        {"BackColor","NVarChar","50"}
              
        };
            return Tablelist;
        }
        public string GetFields()
        {
            string str = string.Empty;
            string[,] strArr = FieldList();
            for (int i = 0; i < strArr.GetLength(0); i++)
            {
                if (strArr[i, 0] != PK)
                {
                    str += "[" + strArr[i, 0] + "],";
                }
            }
            return str.Substring(0, str.Length - 1);
        }
        public string GetParas()
        {
            string str = string.Empty;
            string[,] strArr = FieldList();
            for (int i = 0; i < strArr.GetLength(0); i++)
            {
                if (strArr[i, 0] != PK)
                {
                    str += "@" + strArr[i, 0] + ",";
                }
            }
            return str.Substring(0, str.Length - 1);
        }
        public string GetFieldAndPara()
        {
            string str = string.Empty;
            string[,] strArr = FieldList();
            for (int i = 0; i < strArr.GetLength(0); i++)
            {
                if (strArr[i, 0] != PK)
                {
                    str += "[" + strArr[i, 0] + "]=@" + strArr[i, 0] + ",";
                }
            }
            return str.Substring(0, str.Length - 1);
        }
        public SqlParameter[] GetParameters(M_OA_BC model)
        {
            string[,] strArr = FieldList();
            SqlParameter[] sp = new SqlParameter[strArr.Length];

            for (int i = 0; i < strArr.GetLength(0); i++)
            {
                sp[i] = new SqlParameter("@" + strArr[i, 0], (SqlDbType)Enum.Parse(typeof(SqlDbType), strArr[i, 1]), Convert.ToInt32(strArr[i, 2]));
            }
            sp[0].Value = model.ID;
            sp[1].Value = model.BCName;
            sp[2].Value = model.StartTime;
            sp[3].Value = model.EndTime;
            sp[4].Value = model.Remind;
            sp[5].Value = model.AddTime;
            sp[6].Value = model.BackColor;
            return sp;
        }
        public M_OA_BC GetModelFromReader(SqlDataReader rdr)
        {
            M_OA_BC model = new M_OA_BC();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.BCName = rdr["BCName"].ToString();
            model.StartTime = Convert.ToDateTime(rdr["StartTime"]);
            model.EndTime = Convert.ToDateTime(rdr["EndTime"]);
            model.Remind = rdr["Remind"].ToString();
            model.AddTime = Convert.ToDateTime(rdr["AddTime"]);
            model.BackColor = rdr["BackColor"].ToString();
            rdr.Close();
            rdr.Dispose();
            return model;
        }
    }
}
