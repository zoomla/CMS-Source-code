using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace ZoomLa.Model
{
   public class M_MisSign:M_Base
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID{set;get;}
        /// <summary>
        /// 工作日
        /// </summary>
        public string WorkDate{set;get;}
        /// <summary>
        /// 上班时间
        /// </summary>
        public string WorkBegin{set;get;}
        /// <summary>
        /// 下班时间
        /// </summary>
        public string WorkEnd{set;get;}
        /// <summary>
        /// 状态
        /// </summary>
        public int Status{set;get;}
        /// <summary>
        /// 开始执行时间
        /// </summary>
        public DateTime BeginDate{set;get;}
        public override string TbName { get { return "ZL_MisSign"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"ID","Int","4"},
                                  {"WorkDate","NVarChar","50"},
                                  {"WorkBegin","NVarChar","50"},
                                  {"WorkEnd","NVarChar","50"},
                                  {"Status","Int","4"},
                                  {"BeginDate","DateTime","8"}
                              };
            return Tablelist;
        }

        /// <summary>
        /// 获取字段窜
        /// </summary>
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

        /// <summary>
        /// 获取参数串
        /// </summary>
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

        /// <summary>
        /// 获取字段=参数
        /// </summary>
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

        public override SqlParameter[] GetParameters()
        {
            M_MisSign model = this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.WorkDate;
            sp[2].Value = model.WorkBegin;
            sp[3].Value = model.WorkEnd;
            sp[4].Value = model.Status;
            sp[5].Value = model.BeginDate;
            return sp;
        }
        public M_MisSign GetModelFromReader(SqlDataReader rdr)
        {
            M_MisSign model = new M_MisSign();
            model.ID = ConvertToInt(rdr["ID"]);
            model.WorkDate = ConverToStr(rdr["WorkDate"]);
            model.WorkBegin = ConverToStr(rdr["WorkBegin"]);
            model.WorkEnd = ConverToStr(rdr["WorkEnd"]);
            model.Status = ConvertToInt(rdr["Status"]);
            model.BeginDate =ConvertToDate(rdr["MID"]);
            if (!rdr.HasRows)
            {
                rdr.Close();
                rdr.Dispose();
            }
            return model;
        }
    }
}
