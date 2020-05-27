using System;
using System.Data;
using System.Data.SqlClient;
namespace ZoomLa.Model
{
    [Serializable]
    public class M_Exroom : M_Base
    {
        #region 构造函数
        public M_Exroom()
        {
        }

        public M_Exroom
        (
            int ExrID,
            string RoomName,
            DateTime Starttime,
            DateTime Endtime,
            int ExaID,
            DateTime AddTime,
            string Stuidlist
        )
        {
            this.ExrID = ExrID;
            this.RoomName = RoomName;
            this.Starttime = Starttime;
            this.Endtime = Endtime;
            this.ExaID = ExaID;
            this.AddTime = AddTime;
            this.Stuidlist = Stuidlist;
        }
        /// <summary>
        /// 返回实体列表数组
        /// </summary>
        /// <returns>String[]</returns>
        public string[] ExroomList()
        {
            string[] Tablelist = { "ExrID", "RoomName", "Starttime", "Endtime", "ExaID", "AddTime", "Stuidlist" };
            return Tablelist;
        }
        #endregion

        #region 属性定义
        /// <summary>
        /// 考场ID
        /// </summary>
        public int ExrID { get; set; }
        public string RoomName { get; set; }
        /// <summary>
        /// 开始考试时间
        /// </summary>
        public DateTime Starttime { get; set; }
        /// <summary>
        /// 结束考试时间
        /// </summary>
        public DateTime Endtime { get; set; }
        /// <summary>
        /// 模板试卷ID
        /// </summary>
        public int ExaID { get; set; }
        /// <summary>
        /// 安排时间
        /// </summary>
        public DateTime AddTime { get; set; }
        /// <summary>
        /// 考生ID列表
        /// </summary>
        public string Stuidlist { get; set; }
        #endregion
        public override string PK { get { return "ExrID"; } }
        public override string TbName { get { return "ZL_Exroom"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"ExrID","Int","4"},
                                  {"RoomName","NChar","255"},
                                  {"Starttime","DateTime","8"},
                                  {"Endtime","DateTime","8"},
                                  {"ExaID","Int","4"},
                                  {"AddTime","DateTime","8"},
                                  {"Stuidlist","NText","400"}
                              };
            return Tablelist;
        }
        public SqlParameter[] GetParameters(M_Exroom model)
        {
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ExrID;
            sp[1].Value = model.RoomName;
            sp[2].Value = model.Starttime;
            sp[3].Value = model.Endtime;
            sp[4].Value = model.ExaID;
            sp[5].Value = model.AddTime;
            sp[6].Value = model.Endtime;
            sp[7].Value = model.Stuidlist;
            return sp;
        }
        public M_Exroom GetModelFromReader(SqlDataReader rdr)
        {
            M_Exroom model = new M_Exroom();
            model.ExrID = Convert.ToInt32(rdr["ExrID"]);
            model.RoomName = ConverToStr(rdr["RoomName"]);
            model.Starttime = ConvertToDate(rdr["Starttime"]);
            model.Endtime = ConvertToDate(rdr["Endtime"]);
            model.ExaID = ConvertToInt(rdr["ExaID"]);
            model.AddTime = ConvertToDate(rdr["AddTime"]);
            model.Endtime = ConvertToDate(rdr["Endtime"]);
            model.Stuidlist = ConverToStr(rdr["Stuidlist"]);
            rdr.Close();
            rdr.Dispose();
            return model;
        }
    }
}