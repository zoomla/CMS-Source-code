using System;
using System.Data.SqlClient;
using System.Data;
namespace ZoomLa.Model
{
    [Serializable]
    public class M_Scheme:M_Base
    {
        #region 定义字段
        public int ID { get; set; }
        /// <summary>
        /// 方案名称
        /// </summary>
        public string SName { get; set; }
        /// <summary>
        /// 方案有效期开始时间
        /// </summary>
        public DateTime SStartTime { get; set; }
        /// <summary>
        /// 方案有效期结束时间
        /// </summary>
        public DateTime SEndTime { get; set; }
        /// <summary>
        /// 方案商品列表
        /// </summary>
        public string SList { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime SAddTime { get; set; }
        /// <summary>
        /// 打折类型，1：按商品分类，2：按商品类型分类
        /// </summary>
        public int SType { get; set; }
        #endregion

        #region 构造函数
        public M_Scheme()
        {
            this.SName = string.Empty;
            this.SStartTime = DateTime.Now;
            this.SEndTime = DateTime.Now;
            this.SList = string.Empty;
            this.SAddTime = DateTime.Now;
        }
        public M_Scheme
        (
            int ID,
            string SName,
            DateTime SStartTime,
            DateTime SEndTime,
            string SList,
            DateTime SAddTime,
            int SType
        )
        {
            this.ID = ID;
            this.SName = SName;
            this.SStartTime = SStartTime;
            this.SEndTime = SEndTime;
            this.SList = SList;
            this.SAddTime = SAddTime;
            this.SType = SType;
        }
        /// <summary>
        /// 返回实体列表数组
        /// </summary>
        /// <returns>String[]</returns>
        public string[] SchemeList()
        {
            string[] Tablelist = { "ID", "SName", "SStartTime", "SEndTime", "SList", "SAddTime", "SType" };
            return Tablelist;
        }
        #endregion


        public override string TbName { get { return "ZL_Scheme"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"ID","Int","4"},
                                  {"SName","NVarChar","255"},
                                  {"SStartTime","DateTime","8"},
                                  {"SEndTime","DateTime","8"},
                                  {"SList","NVarChar","50"},
                                  {"SAddTime","DateTime","8"},
                                  {"SType","Int","4"} 
                                 };
            return Tablelist;
        }
        
        public override SqlParameter[] GetParameters()
        {
            M_Scheme model = this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.SName;
            sp[2].Value = model.SStartTime;
            sp[3].Value = model.SEndTime;
            sp[4].Value = model.SList;
            sp[5].Value = model.SAddTime;
            sp[6].Value = model.SType;
            return sp;
        }

        public M_Scheme GetModelFromReader(SqlDataReader rdr)
        {
            M_Scheme model = new M_Scheme();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.SName = rdr["SName"].ToString();
            model.SStartTime = Convert.ToDateTime(rdr["SStartTime"]);
            model.SEndTime = Convert.ToDateTime(rdr["SEndTime"]);
            model.SList = rdr["SList"].ToString();
            model.SAddTime = Convert.ToDateTime(rdr["SAddTime"]);
            model.SType = Convert.ToInt32(rdr["SType"]);
            rdr.Close();
            rdr.Dispose();
            return model;
        }
    }
}