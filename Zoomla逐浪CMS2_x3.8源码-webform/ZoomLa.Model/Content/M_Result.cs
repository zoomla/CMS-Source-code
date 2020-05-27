using System;
using System.Data.SqlClient;
using System.Data;
namespace ZoomLa.Model
{
    public class M_Result : M_Base
    {
        #region 构造函数
        public M_Result()
        {
        }

        public M_Result
        (
            int ID,
            string Result,
            DateTime ResultTime,
            int ProblemID,
            int UserID
        )
        {
            this.ID = ID;
            this.Result = Result;
            this.ResultTime = ResultTime;
            this.ProblemID = ProblemID;
            this.UserID = UserID;
        }
        /// <summary>
        /// 返回实体列表数组
        /// </summary>
        /// <returns>String[]</returns>
        public string[] ResultList()
        {
            string[] Tablelist = { "ID", "Result", "ResultTime", "ProblemID", "UserID" };
            return Tablelist;
        }
        #endregion

        #region 属性定义
        /// <summary>
        /// 
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 答案
        /// </summary>
        public string Result { get; set; }
        /// <summary>
        /// 回答时间
        /// </summary>
        public DateTime ResultTime { get; set; }
        /// <summary>
        /// 问题ID
        /// </summary>
        public int ProblemID { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID { get; set; }
        #endregion
        public override string TbName { get { return "ZL_Result"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"ID","Int","4"},
                                  {"Result","VarChar","2000"},
                                  {"ResultTime","DateTime","8"},
                                  {"ProblemID","Int","4"},
                                  {"UserID","Int","4"}
                                 };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_Result model = this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.Result;
            sp[2].Value = model.ResultTime;
            sp[3].Value = model.ProblemID;
            sp[4].Value = model.UserID;
            return sp;
        }

        public M_Result GetModelFromReader(SqlDataReader rdr)
        {
            M_Result model = new M_Result();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.Result = rdr["Result"].ToString();
            model.ResultTime = Convert.ToDateTime(rdr["ResultTime"]);
            model.ProblemID = Convert.ToInt32(rdr["ProblemID"]);
            model.UserID = Convert.ToInt32(rdr["UserID"]);
            rdr.Close();
            rdr.Dispose();
            return model;
        }
    }
}