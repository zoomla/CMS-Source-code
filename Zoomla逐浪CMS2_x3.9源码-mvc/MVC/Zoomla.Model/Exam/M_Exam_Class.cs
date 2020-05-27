using System;
using System.Data.SqlClient;
using System.Data;
namespace ZoomLa.Model
{
    [Serializable]
    public class M_Exam_Class:M_Base
    {
        #region 定义字段
        /// <summary>
        /// 
        /// </summary>
        public int C_id { get; set; }
        /// <summary>
        /// 分类名称
        /// </summary>
        public string C_ClassName { get; set; }
        /// <summary>
        /// 所属ID
        /// </summary>
        public int C_Classid { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int C_OrderBy { get; set; }
        /// <summary>
        /// 类别类型 1-答题类型 2-视频操作类型
        /// </summary>
        public int C_ClassType { get; set; }
        #endregion

        #region 构造函数
        public M_Exam_Class()
        {
        }

        public M_Exam_Class
        (
            int C_id,
            string C_ClassName,
            int C_Classid,
            int C_OrderBy,
            int C_ClassType
        )
        {
            this.C_id = C_id;
            this.C_ClassName = C_ClassName;
            this.C_Classid = C_Classid;
            this.C_OrderBy = C_OrderBy;
            this.C_ClassType = C_ClassType;
        }
        /// <summary>
        /// 返回实体列表数组
        /// </summary>
        /// <returns>String[]</returns>
        public string[] Questions_ClassList()
        {
            string[] Tablelist = { "C_id", "C_ClassName", "C_Classid", "C_OrderBy", "C_ClassType" };
            return Tablelist;
        }
        #endregion

        public override string PK { get { return "C_id"; } }
        public override string TbName { get { return "ZL_Exam_Class"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"C_id","Int","4"},
                                  {"C_ClassName","NVarChar","255"},
                                  {"C_Classid","Int","4"}, 
                                  {"C_OrderBy","Int","4"},
                                  {"C_ClassType","Int","4"},
                                 };
         
            return Tablelist;
        }
        public  SqlParameter[] GetParameters(M_Exam_Class model)
        {
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.C_id;
            sp[1].Value = model.C_ClassName;
            sp[2].Value = model.C_Classid;
            sp[3].Value = model.C_OrderBy;
            sp[4].Value = model.C_ClassType;   
            return sp;
        }
        public M_Exam_Class GetModelFromReader(SqlDataReader rdr)
        {
            M_Exam_Class model = new M_Exam_Class();
            model.C_id = Convert.ToInt32(rdr["C_id"]);
            model.C_ClassName = ConverToStr(rdr["C_ClassName"]);
            model.C_Classid =ConvertToInt(rdr["C_Classid"]);
            model.C_OrderBy = ConvertToInt(rdr["C_OrderBy"]);
            model.C_ClassType = ConvertToInt(rdr["C_ClassType"]);
            rdr.Close();
            return model;
        }
    }
   
}


