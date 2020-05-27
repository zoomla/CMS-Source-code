using System;
using System.Data.SqlClient;
using System.Data;
namespace ZoomLa.Model
{
    public class M_RoomActive:M_Base
    {

        #region 构造函数
        public M_RoomActive()
        {
        }

        public M_RoomActive
        (
            int AID,
            int ActiveUserID,
            string ActiveTitle,
            string ActiveContext,
            DateTime ActiveStateTime,
            DateTime ActiveEndTime,
            DateTime ActiveAddTime,
            string ActivePic,
            int RoomID
        )
        {
            this.AID = AID;
            this.ActiveUserID = ActiveUserID;
            this.ActiveTitle = ActiveTitle;
            this.ActiveContext = ActiveContext;
            this.ActiveStateTime = ActiveStateTime;
            this.ActiveEndTime = ActiveEndTime;
            this.ActiveAddTime = ActiveAddTime;
            this.ActivePic = ActivePic;
            this.RoomID = RoomID;
        }
        /// <summary>
        /// 返回实体列表数组
        /// </summary>
        /// <returns>String[]</returns>
        public string[] RoomActiveList()
        {
            string[] Tablelist = { "AID", "ActiveUserID", "ActiveTitle", "ActiveContext", "ActiveStateTime", "ActiveEndTime", "ActiveAddTime", "ActivePic", "RoomID" };
            return Tablelist;
        }
        #endregion

        #region 属性定义
        /// <summary>
        /// 
        /// </summary>
        public int AID { get; set; }
        /// <summary>
        /// 活动发起人
        /// </summary>
        public int ActiveUserID { get; set; }
        /// <summary>
        /// 活动标题
        /// </summary>
        public string ActiveTitle { get; set; }
        /// <summary>
        /// 活动内容
        /// </summary>
        public string ActiveContext { get; set; }
        /// <summary>
        /// 活动开始时间
        /// </summary>
        public DateTime ActiveStateTime { get; set; }
        /// <summary>
        /// 活动结束时间
        /// </summary>
        public DateTime ActiveEndTime { get; set; }
        /// <summary>
        /// 活动添加时间
        /// </summary>
        public DateTime ActiveAddTime { get; set; }
        /// <summary>
        /// 活动图片
        /// </summary>
        public string ActivePic { get; set; }
        /// <summary>
        /// 班级ID
        /// </summary>
        public int RoomID { get; set; }
        #endregion

        public override string PK { get { return "AID"; } }
        public override string TbName { get { return "ZL_RoomActive"; } }
        public override string[,] FieldList()
        {

            string[,] Tablelist = {
                                  {"AID","Int","4"},
                                  {"ActiveUserID","Int","4"},
                                  {"ActiveTitle","NChar","255"}, 
                                  {"ActiveContext","NText","4000"},
                                  {"ActiveStateTime","DateTime","8"},
                                  {"ActiveEndTime","DateTime","8"}, 
                                  {"ActiveAddTime","DateTime","8"},
                                  {"ActivePic","NChar","200"},
                                  {"RoomID","Int","4"},
                                 };
            return Tablelist;
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

        public override SqlParameter[] GetParameters()
        {
            M_RoomActive model = this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.AID;
            sp[1].Value = model.ActiveUserID;
            sp[2].Value = model.ActiveTitle;
            sp[3].Value = model.ActiveContext;
            sp[4].Value = model.ActiveStateTime;
            sp[5].Value = model.ActiveEndTime;
            sp[6].Value = model.ActiveAddTime;
            sp[7].Value = model.ActivePic;
            sp[8].Value = model.RoomID;


            return sp;
        }
        public M_RoomActive GetModelFromReader(SqlDataReader rdr)
        {
            M_RoomActive model = new M_RoomActive();
            model.AID = Convert.ToInt32(rdr["AID"]);
            model.ActiveUserID = ConvertToInt(rdr["ActiveUserID"]);
            model.ActiveTitle = ConverToStr(rdr["ActiveTitle"]);
            model.ActiveContext = ConverToStr(rdr["ActiveContext"]);
            model.ActiveStateTime = ConvertToDate(rdr["ActiveStateTime"]);
            model.ActiveEndTime = ConvertToDate(rdr["ActiveEndTime"]);
            model.ActiveAddTime = ConvertToDate(rdr["ActiveAddTime"]);
            model.ActivePic = ConverToStr(rdr["ActivePic"]);
            model.RoomID = ConvertToInt(rdr["RoomID"]);
            rdr.Close();
            rdr.Dispose();
            return model;
        }
    }
}