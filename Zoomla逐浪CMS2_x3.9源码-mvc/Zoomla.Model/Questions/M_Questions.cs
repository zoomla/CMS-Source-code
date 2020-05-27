using System;
using System.Data.SqlClient;
using System.Data;
namespace ZoomLa.Model
{
    [Serializable]
    public class M_Questions : M_Base
    {
        #region 构造函数
        public M_Questions()
        {
        }

        public M_Questions
        (
            int p_id,
            string p_title,
            int p_Difficulty,
            int p_Class,
            int p_Views,
            string p_Inputer,
            int p_Type,
            int p_Knowledge,
            string p_Answer,
            string p_Content,
            DateTime p_CreateTime,
            int p_Order
        )
        {
            this.p_id = p_id;
            this.p_title = p_title;
            this.p_Difficulty = p_Difficulty;
            this.p_Class = p_Class;
            this.p_Views = p_Views;
            this.p_Inputer = p_Inputer;
            this.p_Type = p_Type;
            this.p_Knowledge = p_Knowledge;
            this.p_Answer = p_Answer;
            this.p_Content = p_Content;
            this.p_CreateTime = p_CreateTime;
            this.p_Order = p_Order;
        }
        /// <summary>
        /// 返回实体列表数组
        /// </summary>
        /// <returns>String[]</returns>
        public string[] QuestionsList()
        {
            string[] Tablelist = { "p_id", "p_title", "p_Difficulty", "p_Class", "p_Views", "p_Inputer", "p_Type", "p_Knowledge", "p_Answer", "p_Content", "p_CreateTime", "p_Order" };
            return Tablelist;
        }
        #endregion

        #region 属性定义
        /// <summary>
        /// 试卷题目ID
        /// </summary>
        public int p_id { get; set; }
        /// <summary>
        /// 试卷标题
        /// </summary>
        public string p_title { get; set; }
        /// <summary>
        /// 难度
        /// </summary>
        public int p_Difficulty { get; set; }
        /// <summary>
        /// 分类
        /// </summary>
        public int p_Class { get; set; }
        /// <summary>
        /// 使用次数
        /// </summary>
        public int p_Views { get; set; }
        /// <summary>
        /// 作者
        /// </summary>
        public string p_Inputer { get; set; }
        /// <summary>
        /// 题型
        /// </summary>
        public int p_Type { get; set; }
        /// <summary>
        /// 知识点
        /// </summary>
        public int p_Knowledge { get; set; }
        /// <summary>
        /// 答案
        /// </summary>
        public string p_Answer { get; set; }
        /// <summary>
        /// 试题内容
        /// </summary>
        public string p_Content { get; set; }
        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime p_CreateTime { get; set; }

        /// <summary>
        /// 所属试卷ID
        /// </summary>
        public int Paper_Id { get; set; }

        /// <summary>
        /// 试题排序
        /// </summary>
        public int p_Order { get; set; }
        #endregion

        public override string PK { get { return "p_id"; } }
        public override string TbName { get { return "ZL_Questions"; } }
        public override string[,] FieldList()
        {

            string[,] Tablelist = {
                                  {"p_id","Int","4"},
                                  {"p_title","NChar","255"},
                                  {"p_Difficulty","Int","4"}, 
                                  {"p_Class","Int","4"},
                                  {"p_Views","Int","4"},
                                  {"p_Inputer","NChar","255"}, 
                                  {"p_Type","Int","4"},
                                  {"p_Knowledge","Int","4"},
                                  {"p_Answer","NText","400"}, 
                                  {"p_Content","NText","400"},
                                  {"p_CreateTime","DateTime","255"},
                                  {"Paper_Id","Int","4"}, 
                                  {"p_Order","Int","4"}
                                 };

            return Tablelist;

        }
        public SqlParameter[] GetParameters(M_Questions model)
        {
            string[,] strArr = FieldList();
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.p_id;
            sp[1].Value = model.p_title;
            sp[2].Value = model.p_Difficulty;
            sp[3].Value = model.p_Class;
            sp[4].Value = model.p_Views;
            sp[5].Value = model.p_Inputer;
            sp[6].Value = model.p_Type;
            sp[7].Value = model.p_Knowledge;
            sp[8].Value = model.p_Answer;
            sp[9].Value = model.p_Content;
            sp[10].Value = model.p_CreateTime;
            sp[11].Value = model.Paper_Id;
            sp[12].Value = model.p_Order;
            return sp;
        }
        public M_Questions GetModelFromReader(SqlDataReader rdr)
        {
            M_Questions model = new M_Questions();
            model.p_id = Convert.ToInt32(rdr["p_id"]);
            model.p_title = rdr["p_title"].ToString();
            model.p_Difficulty = Convert.ToInt32(rdr["p_Difficulty"]);
            model.p_Class = Convert.ToInt32(rdr["p_Class"]);
            model.p_Views = Convert.ToInt32(rdr["p_Views"]);
            model.p_Inputer = rdr["p_Inputer"].ToString();
            model.p_Type = Convert.ToInt32(rdr["p_Type"]);
            model.p_Knowledge = Convert.ToInt32(rdr["p_Knowledge"]);
            model.p_Answer = rdr["p_Answer"].ToString();
            model.p_Content = rdr["p_Content"].ToString();
            model.p_CreateTime = Convert.ToDateTime(rdr["p_CreateTime"]);
            model.Paper_Id = Convert.ToInt32(rdr["Paper_Id"]);
            model.p_Order = Convert.ToInt32(rdr["p_Order"]);
            rdr.Close();
            return model;
        }
    }
}