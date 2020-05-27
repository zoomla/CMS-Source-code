using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ZoomLa.Model
{
    public class M_OA_Sign
    {
        public int ID { get; set; }
        /// <summary>
        /// 签章名
        /// </summary>
        public string SignName { get; set; }
        /// <summary>
        /// 虚拟路径
        /// </summary>
        public string VPath { get; set; }
        /// <summary>
        /// 绑定用户ID
        /// </summary>
        public int OwnUserID { get; set; }
        /// <summary>
        /// 密钥,未实现(用于标识用户身份)
        /// </summary>
        public string SignKey { get; set; }
        public DateTime CreateTime { get; set; }
        public int CreateMan { get; set; }
        public string Remind { get; set; }
        /// <summary>
        /// 1为启用,0:不启用
        /// </summary>
        public int Status { get; set; }
        public string SignPwd { get; set; }
        public M_OA_Sign()
        {

        }

        public string TbName = "ZL_OA_Sign";
        public string PK = "ID";

        public string[,] FieldList()
        {
            string[,] Tablelist = {
                        {"ID","Int","4"},   
        	            {"SignName","NVarChar","50"},            
                        {"VPath","NVarChar","100"},            
                        {"OwnUserID","Int","4"},            
                        {"SignKey","NVarChar","50"},            
                        {"CreateTime","DateTime","8"},            
                        {"CreateMan","Int","4"},            
                        {"Remind","NVarChar","255"},            
                        {"Status","Int","4"},
                        {"SignPwd","NVarChar","50"} 
              
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
        public SqlParameter[] GetParameters(M_OA_Sign model)
        {
            string[,] strArr = FieldList();
            SqlParameter[] sp = new SqlParameter[strArr.Length];

            for (int i = 0; i < strArr.GetLength(0); i++)
            {
                sp[i] = new SqlParameter("@" + strArr[i, 0], (SqlDbType)Enum.Parse(typeof(SqlDbType), strArr[i, 1]), Convert.ToInt32(strArr[i, 2]));
            }
            sp[0].Value = model.ID;
            sp[1].Value = model.SignName;
            sp[2].Value = model.VPath;
            sp[3].Value = model.OwnUserID;
            sp[4].Value = model.SignKey;
            sp[5].Value = model.CreateTime;
            sp[6].Value = model.CreateMan;
            sp[7].Value = model.Remind;
            sp[8].Value = model.Status;
            sp[9].Value = model.SignPwd;
            return sp;
        }
        public M_OA_Sign GetModelFromReader(SqlDataReader rdr)
        {
            M_OA_Sign model = new M_OA_Sign();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.SignName = rdr["SignName"].ToString();
            model.VPath = rdr["VPath"].ToString();
            model.OwnUserID = Convert.ToInt32(rdr["OwnUserID"]);
            model.SignKey = rdr["SignKey"].ToString();
            model.CreateTime = Convert.ToDateTime(rdr["CreateTime"]);
            model.CreateMan = Convert.ToInt32(rdr["CreateMan"]);
            model.Remind = rdr["Remind"].ToString();
            model.Status = Convert.ToInt32(rdr["Status"]);
            model.SignPwd = rdr["SignPwd"].ToString();
            rdr.Close();
            return model;
        }
    }
}
