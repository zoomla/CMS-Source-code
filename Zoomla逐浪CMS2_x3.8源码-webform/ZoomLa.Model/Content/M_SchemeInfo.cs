using System;
using System.Data;
using System.Data.SqlClient;
namespace ZoomLa.Model
{
    [Serializable]
    public class M_SchemeInfo:M_Base
    {
        #region 构造函数
        public M_SchemeInfo()
        {
        }

        public M_SchemeInfo
        (
            int ID,
            int SIULimit,
            int SILLimit,
            int SIAgio,
            int SID,
            DateTime SIAddTime
        )
        {
            this.ID = ID;
            this.SIULimit = SIULimit;
            this.SILLimit = SILLimit;
            this.SIAgio = SIAgio;
            this.SID = SID;
            this.SIAddTime = SIAddTime;
        }
        /// <summary>
        /// 返回实体列表数组
        /// </summary>
        /// <returns>String[]</returns>
        public string[] SchemeInfoList()
        {
            string[] Tablelist = { "ID", "SIULimit", "SILLimit", "SIAgio", "SID", "SIAddTime" };
            return Tablelist;
        }
        #endregion

        #region 属性定义
        /// <summary>
        /// 
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 数量上限
        /// </summary>
        public int SIULimit { get; set; }
        /// <summary>
        /// 数量下限
        /// </summary>
        public int SILLimit { get; set; }
        /// <summary>
        /// 折扣
        /// </summary>
        public int SIAgio { get; set; }
        /// <summary>
        /// 方案ID
        /// </summary>
        public int SID { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime SIAddTime { get; set; }
        #endregion
        public override string TbName { get { return "ZL_SchemeInfo"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"ID","Int","4"},
                                  {"SIULimit","Int","4"},
                                  {"SILLimit","Int","4"},
                                  {"SIAgio","Int","4"}, 
                                  {"SID","Int","4"}, 
                                  {"SIAddTime","DateTime","8"}
                                 };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_SchemeInfo model = this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.SIULimit;
            sp[2].Value = model.SILLimit;
            sp[3].Value = model.SIAgio;
            sp[4].Value = model.SID;
            sp[5].Value = model.SIAddTime;
            return sp;
        }
        public M_SchemeInfo GetModelFromReader(SqlDataReader rdr)
        {
            M_SchemeInfo model = new M_SchemeInfo();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.SIULimit = Convert.ToInt32(rdr["SIULimit"]);
            model.SILLimit = Convert.ToInt32(rdr["SILLimit"]);
            model.SIAgio = Convert.ToInt32(rdr["SIAgio"]);
            model.SID = Convert.ToInt32(rdr["SID"]);
            model.SIAddTime = Convert.ToDateTime(rdr["SIAddTime"]);
            rdr.Close();
            rdr.Dispose();
            return model;
        }
    }
}