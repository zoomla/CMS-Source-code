using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ZoomLa.Model
{
    public class M_Plat_TaskMsg : M_Base
    {
        public int ID { get; set; }
        public int TaskID { get; set; }
        public string MsgContent { get; set; }
        public int MsgType { get; set; }
        public DateTime CreateTime { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public int Status { get; set; }
        public string Attach { get; set; }

        public override string TbName { get { return "ZL_Plat_TaskMsg"; } }

        public override string[,] FieldList()
        {
            string[,] Tablelist = {
        		        		{"ID","Int","4"},
        		        		{"TaskID","Int","4"},
        		        		{"MsgContent","NVarChar","200"},
        		        		{"MsgType","Int","4"},
        		        		{"CreateTime","DateTime","8"},
        		        		{"UserID","Int","4"},
        		        		{"UserName","NVarChar","50"},
        		        		{"Status","Int","4"},
        		        		{"Attach","NVarChar","200"}
        };
            return Tablelist;
        }

        public SqlParameter[] GetParameters(M_Plat_TaskMsg model)
        {
            string[,] strArr = FieldList();
            SqlParameter[] sp = new SqlParameter[strArr.Length];

            for (int i = 0; i < strArr.GetLength(0); i++)
            {
                sp[i] = new SqlParameter("@" + strArr[i, 0], (SqlDbType)Enum.Parse(typeof(SqlDbType), strArr[i, 1]), Convert.ToInt32(strArr[i, 2]));
            }
            sp[0].Value = model.ID;
            sp[1].Value = model.TaskID;
            sp[2].Value = model.MsgContent;
            sp[3].Value = model.MsgType;
            sp[4].Value = model.CreateTime;
            sp[5].Value = model.UserID;
            sp[6].Value = model.UserName;
            sp[7].Value = model.Status;
            sp[8].Value = model.Attach;
            return sp;
        }
        public M_Plat_TaskMsg GetModelFromReader(SqlDataReader rdr)
        {
            M_Plat_TaskMsg model = new M_Plat_TaskMsg();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.TaskID = Convert.ToInt32(rdr["TaskID"]);
            model.MsgContent = rdr["MsgContent"].ToString();
            model.MsgType = Convert.ToInt32(rdr["MsgType"]);
            model.CreateTime = Convert.ToDateTime(rdr["CreateTime"]);
            model.UserID = Convert.ToInt32(rdr["UserID"]);
            model.UserName = rdr["UserName"].ToString();
            model.Status = Convert.ToInt32(rdr["Status"]);
            model.Attach = rdr["Attach"].ToString();
            rdr.Close();
            return model;
        }
    }
}
