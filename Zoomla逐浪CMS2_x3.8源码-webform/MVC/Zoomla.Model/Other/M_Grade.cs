using System;
using System.Data.SqlClient;
using System.Data;

namespace ZoomLa.Model
{
    /// <summary>
    /// 分级选项
    /// </summary>
    public class M_Grade:M_Base
    {
        public M_Grade()
        {
            this.GradeID = 0;
            this.Cate = 0;
            this.ParentID = 0;
            this.GradeName = "";
            this.Grade = 0;
        }
        /// <summary>
        /// 分级选项ID
        /// </summary>
        public int GradeID { get; set; }
        /// <summary>
        /// 分级类别ID 4,题型 5,试题难度 6,年级 7,教材版本
        /// </summary>
        public int Cate { get; set; }
        /// <summary>
        /// 父选项ID 一级选项父ID为0
        /// </summary>
        public int ParentID { get; set; }
        /// <summary>
        /// 选项名称
        /// </summary>
        public string GradeName { get; set; }
        /// <summary>
        /// 当前选项级别
        /// </summary>
        public int Grade { get; set; }

        public override string PK { get { return "GradeID"; } }
        public override string TbName { get { return "ZL_Grade"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"GradeID","Int","4"},
                                  {"Cate","Int","4"},
                                  {"ParentID","Int","4"},
                                  {"GradeName","NVarChar","50"}, 
                                  {"Grade","Int","4"}
                                 };
            return Tablelist;
        }
        public static string[,] FieldList2() 
        {
            return new M_Grade().FieldList();
        }
      
        public SqlParameter[] GetParameters(M_Grade model)
        {
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.GradeID;
            sp[1].Value = model.Cate;
            sp[2].Value = model.ParentID;
            sp[3].Value = model.GradeName;
            sp[4].Value = model.Grade;
            return sp;
        }

        public M_Grade GetModelFromReader(SqlDataReader rdr)
        {
            M_Grade model = new M_Grade();
            model.GradeID = Convert.ToInt32(rdr["GradeID"]);
            model.Cate = Convert.ToInt32(rdr["Cate"]);
            model.ParentID = Convert.ToInt32(rdr["ParentID"]);
            model.GradeName = rdr["GradeName"].ToString();
            model.Grade = Convert.ToInt32(rdr["Grade"]);
            rdr.Close();
            rdr.Dispose();
            return model;
        }

    }
}
