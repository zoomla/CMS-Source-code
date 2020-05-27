using System;
using System.Data.SqlClient;
using System.Data;
namespace ZoomLa.Model
{
    [Serializable]
    public class M_ExAttendance : M_Base
    {
        #region 构造函数
        public M_ExAttendance()
        {
        }

        public M_ExAttendance
        (
            int AttID,
            int Stuid,
            DateTime LogTime,
            int Logtimeout,
            string Location,
            string StuName
        )
        {
            this.AttID = AttID;
            this.Stuid = Stuid;
            this.LogTime = LogTime;
            this.Logtimeout = Logtimeout;
            this.Location = Location;
            this.StuName = StuName;
        }
        /// <summary>
        /// 返回实体列表数组
        /// </summary>
        /// <returns>String[]</returns>
        public string[] ExAttendanceList()
        {
            string[] Tablelist = { "AttID", "Stuid", "LogTime", "Logtimeout", "Location", "StuName" };
            return Tablelist;
        }
        #endregion

        #region 属性定义
        /// <summary>
        /// 考勤ID
        /// </summary>
        public int AttID { get; set; }
        /// <summary>
        /// 学员ID
        /// </summary>
        public int Stuid { get; set; }
        /// <summary>
        /// 登录时间
        /// </summary>
        public DateTime LogTime { get; set; }
        /// <summary>
        /// 登录时长
        /// </summary>
        public int Logtimeout { get; set; }
        /// <summary>
        /// 当前位置
        /// </summary>
        public string Location { get; set; }
        /// <summary>
        /// 用户名称
        /// </summary>
        public string StuName { get; set; }
        #endregion

        public override string PK { get { return "AttID"; } }
        public override string TbName { get { return "ZL_ExAttendance"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"AttID","Int","4"},
                                  {"Stuid","Int","4"},
                                  {"LogTime","DateTime","8"},
                                  {"Logtimeout","Int","4"},
                                  {"Location","NChar","255"},
                                  {"StuName","NChar","255"}
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

        public SqlParameter[] GetParameters(M_ExAttendance model)
        {
            string[,] strArr = FieldList();
            SqlParameter[] sp = new SqlParameter[strArr.Length];

            for (int i = 0; i < strArr.GetLength(0); i++)
            {
                sp[i] = new SqlParameter("@" + strArr[i, 0], (SqlDbType)Enum.Parse(typeof(SqlDbType), strArr[i, 1]), Convert.ToInt32(strArr[i, 2]));
            }
            sp[0].Value = model.AttID;
            sp[1].Value = model.Stuid;
            sp[2].Value = model.LogTime;
            sp[3].Value = model.Logtimeout;
            sp[4].Value = model.Location;
            sp[5].Value = model.StuName;

            return sp;
        }
        public M_ExAttendance GetModelFromReader(SqlDataReader rdr)
        {
            M_ExAttendance model = new M_ExAttendance();
            model.AttID = Convert.ToInt32(rdr["AttID"]);
            model.Stuid = ConvertToInt(rdr["Stuid"]);
            model.LogTime = ConvertToDate(rdr["LogTime"]);
            model.Logtimeout = ConvertToInt(rdr["Logtimeout"]);
            model.Location = ConverToStr(rdr["Location"]);
            model.StuName = ConverToStr(rdr["StuName"]);
            rdr.Close();
            rdr.Dispose();
            return model;
        }
    }
}