using System;
using System.Data.SqlClient;
using System.Data;
namespace ZoomLa.Model
{
    [Serializable]
    public class M_ServiceSeat:M_Base
    {
        public int S_ID
        {
            get;
            set;
        }
        /// <summary>
        /// 席位名称
        /// </summary>
        public string S_Name
        {
            get;
            set;
        }
        /// <summary>
        /// 客服UserID
        /// </summary>
        public int S_AdminID
        {
            get;
            set;
        }
        /// <summary>
        /// 设置时间
        /// </summary>
        public string S_DateTime
        {
            get;
            set;
        }
        /// <summary>
        /// 显示索引
        /// </summary>
        public int S_Index
        {
            get;
            set;
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string S_Remrk
        {
            get;
            set;
        }

        /// <summary>
        /// 是否为默认客服
        /// </summary>
        public int S_Default
        {
            get;
            set;
        }
        public string S_FaceImg { get; set; }
        public override string PK { get { return "S_ID"; } }
        public override string TbName { get { return "ZL_ServiceSeat"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"S_ID","Int","4"},
                                  {"S_Name","NVarChar","50"},
                                  {"S_AdminID","Int","4"},
                                  {"S_DateTime","NVarChar","50"}, 
                                  {"S_Index","Int","4"}, 
                                  {"S_Remrk","NVarChar","500"}, 
                                  {"S_Default","Int","4"},
                                  {"S_FaceImg","NVarChar","200"}
                                 };
            return Tablelist;
        }
        public SqlParameter[] GetParameters(M_ServiceSeat model)
        {
            string[,] strArr = FieldList();
            SqlParameter[] sp = new SqlParameter[strArr.Length];

            for (int i = 0; i < strArr.GetLength(0); i++)
            {
                sp[i] = new SqlParameter("@" + strArr[i, 0], (SqlDbType)Enum.Parse(typeof(SqlDbType), strArr[i, 1]), Convert.ToInt32(strArr[i, 2]));
            }
            sp[0].Value = model.S_ID;
            sp[1].Value = model.S_Name;
            sp[2].Value = model.S_AdminID;
            sp[3].Value = model.S_DateTime;
            sp[4].Value = model.S_Index;
            sp[5].Value = model.S_Remrk;
            sp[6].Value = model.S_Default;
            sp[7].Value = model.S_FaceImg ;
            return sp;
        }

        public M_ServiceSeat GetModelFromReader(SqlDataReader rdr)
        {
            M_ServiceSeat model = new M_ServiceSeat();
            model.S_ID = Convert.ToInt32(rdr["S_ID"]);
            model.S_Name = rdr["S_Name"].ToString();
            model.S_AdminID = Convert.ToInt32(rdr["S_AdminID"]);
            model.S_DateTime = ConverToStr(rdr["S_DateTime"]);
            model.S_Index = Convert.ToInt32(rdr["S_Index"]);
            model.S_Remrk = ConverToStr(rdr["S_Remrk"]);
            model.S_Default = Convert.ToInt32(rdr["S_Default"]);
            model.S_FaceImg = ConverToStr(rdr["S_FaceImg"]);
            rdr.Dispose();
            return model;
        }
    }
}


