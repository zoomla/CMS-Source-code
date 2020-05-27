using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ZoomLa.Model
{
    public class M_OA_UserConfig
    {
        string _leftChk, _mainChk, _popChk;
        public int ID { get; set; }
        public int UserID { get; set; }
        /// <summary>
        /// 左边栏
        /// </summary>
        public string LeftChk
        {
            get { _leftChk = string.IsNullOrEmpty(_leftChk) ? "" : "," + (_leftChk.Trim(',')) + ","; return _leftChk; }
            set { _leftChk = value; }
        }
        /// <summary>
        /// 主界面
        /// </summary>
        public string MainChk
        {
            get { _mainChk = string.IsNullOrEmpty(_mainChk) ? "" : "," + (_mainChk.Trim(',')) + ","; return _mainChk; }
            set { _mainChk = value; }
        }
        /// <summary>
        /// 弹窗
        /// </summary>
        public string PopChk
        {
            get { _popChk = string.IsNullOrEmpty(_popChk) ? "" : "," + (_popChk.Trim(',')) + ","; return _popChk; }
            set { _popChk = value; }
        }
        public int Status { get; set; }
        public string Remind { get; set; }
        public bool IsNull { get; set; }
        public bool HasAuth(string s1,string s2) 
        {
            return s1.IndexOf("," + s2 + ",") > -1;
        }
        public M_OA_UserConfig()
        {

        }

        public string TbName = "ZL_OA_UserConfig";
        public string PK = "ID";

        public string[,] FieldList()
        {
            string[,] Tablelist = {
        	            {"ID","Int","4"},            
                        {"UserID","Int","4"},            
                        {"LeftChk","NVarChar","300"},            
                        {"MainChk","NVarChar","300"},            
                        {"PopChk","NVarChar","300"},            
                        {"Status","Int","4"},            
                        {"Remind","NVarChar","100"}            
              
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
        public SqlParameter[] GetParameters(M_OA_UserConfig model)
        {
            string[,] strArr = FieldList();
            SqlParameter[] sp = new SqlParameter[strArr.Length];

            for (int i = 0; i < strArr.GetLength(0); i++)
            {
                sp[i] = new SqlParameter("@" + strArr[i, 0], (SqlDbType)Enum.Parse(typeof(SqlDbType), strArr[i, 1]), Convert.ToInt32(strArr[i, 2]));
            }
            sp[0].Value = model.ID;
            sp[1].Value = model.UserID;
            sp[2].Value = model.LeftChk;
            sp[3].Value = model.MainChk;
            sp[4].Value = model.PopChk;
            sp[5].Value = model.Status;
            sp[6].Value = model.Remind;
            return sp;
        }
        public M_OA_UserConfig GetModelFromReader(SqlDataReader rdr)
        {
            M_OA_UserConfig model = new M_OA_UserConfig();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.UserID = Convert.ToInt32(rdr["UserID"]);
            model.LeftChk = rdr["LeftChk"].ToString();
            model.MainChk = rdr["MainChk"].ToString();
            model.PopChk = rdr["PopChk"].ToString();
            model.Status = Convert.ToInt32(rdr["Status"]);
            model.Remind = rdr["Remind"].ToString();
            rdr.Close();
            rdr.Dispose();
            return model;
        }
    }
}
