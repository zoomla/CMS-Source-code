using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace ZoomLa.Model
{
    public class M_MisType : M_Base
    {
        /// <summary>
        /// 类型ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 类型名称
        /// </summary>
        public string TypeName { get; set; }
        /// <summary>
        /// 类型描述
        /// </summary>	
        public string TypeDescribe { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>	
        public DateTime CreateTime { get; set; }
        public M_MisType()
        {
        }
        public override string TbName { get { return "ZL_MisType"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"ID","Int","4"},
                                  {"TypeName","NVarChar","50"},
                                  {"TypeDescribe","NVarChar","255"},
                                  {"CreateTime","DateTime","8"}
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
            M_MisType MType = this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = MType.ID;
            sp[1].Value = MType.TypeName;
            sp[2].Value = MType.TypeDescribe;
            sp[3].Value = MType.CreateTime;
            return sp;
        }

        public M_MisType GetModelFromReader(SqlDataReader rdr)
        {
            M_MisType MType = new M_MisType();
            MType.ID = Convert.ToInt32(rdr["ID"]);
            MType.TypeName = ConverToStr(rdr["TypeName"]);
            MType.TypeDescribe = ConverToStr(rdr["TypeDescribe"]);
            MType.CreateTime = ConvertToDate(rdr["CreateTime"]);
            if (!rdr.HasRows)
            {
                rdr.Close();
                rdr.Dispose();
            }
            return MType;
        }
    }
}
