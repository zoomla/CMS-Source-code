using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace ZoomLa.Model
{
    public class M_MisAttendance : M_Base
    {
        /// <summary>
        /// ID
        /// </summary>
        public string DepartMent { set; get; }
        /// <summary>
        /// 部门
        /// </summary>
        public int ID { set; get; }
        /// <summary>
        /// 签到时间
        /// </summary>
        public string BeginTime { set; get; }
        /// <summary>
        /// 签退时间
        /// </summary>
        public string EndTime { set; get; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string UserName { set; get; }
        /// <summary>
        /// 备注信息
        /// </summary>
        public string Comment { set; get; }
        /// <summary>
        /// 签到状态
        /// </summary>
        public int BeginStatus { set; get; }
        /// <summary>
        /// 签退状态
        /// </summary>
        public int EndStatus { set; get; }
        public override string TbName { get { return "ZL_MisAttendance"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"ID","Int","4"},
                                  {"Department","NVarChar","255"},
                                  {"BeginTime","NVarChar","100"},
                                  {"EndTime","NVarChar","100"},
                                  {"UserName","NVarChar","50"},
                                  {"Comment","NVarChar","500"},
                                  {"BeginStatus","Int","4"},
                                  {"EndStatus","Int","4"}
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
            M_MisAttendance model = this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.DepartMent;
            sp[2].Value = model.BeginTime;
            sp[3].Value = model.EndTime;
            sp[4].Value = model.UserName;
            sp[5].Value = model.Comment;
            sp[6].Value = model.BeginStatus;
            sp[7].Value = model.EndStatus;
            return sp;
        }
        public M_MisAttendance GetModelFromReader(SqlDataReader rdr)
        {
            M_MisAttendance model = new M_MisAttendance();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.DepartMent = ConverToStr(rdr["Department"]);
            model.BeginTime = ConverToStr(rdr["BeginTime"]);
            model.EndTime = ConverToStr(rdr["EndTime"]);
            model.UserName = ConverToStr(rdr["UserName"]);
            model.Comment = ConverToStr(rdr["Comment"]);
            model.BeginStatus = ConvertToInt(rdr["BeginStatus"]);
            model.EndStatus = ConvertToInt(rdr["EndStatus"]);
            if (!rdr.HasRows)
            {
                rdr.Close();
                rdr.Dispose();
            }
            return model;
        }
    }
}
