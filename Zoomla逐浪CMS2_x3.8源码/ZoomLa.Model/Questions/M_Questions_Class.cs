using System;
using System.Data.SqlClient;
using System.Data;
namespace ZoomLa.Model
{
    [Serializable]
    public class M_Questions_Class : M_Base
    {

        #region 构造函数
        public M_Questions_Class()
        {
        }

        public M_Questions_Class
        (
            int C_id,
            string C_ClassName,
            int C_Classid,
            int C_OrderBy
        )
        {
            this.C_id = C_id;
            this.C_ClassName = C_ClassName;
            this.C_Classid = C_Classid;
            this.C_OrderBy = C_OrderBy;
        }
        /// <summary>
        /// 返回实体列表数组
        /// </summary>
        /// <returns>String[]</returns>
        public string[] Questions_ClassList()
        {
            string[] Tablelist = { "C_id", "C_ClassName", "C_Classid", "C_OrderBy" };
            return Tablelist;
        }
        #endregion

        #region 属性定义
        /// <summary>
        /// 分类ID
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
        #endregion

        public override string PK { get { return "C_id"; } }
        public override string TbName { get { return "ZL_Questions_Class"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"C_id","Int","4"},
                                  {"C_ClassName","NChar","255"},
                                  {"C_Classid","Int","4"},
                                   {"C_OrderBy","Int","4"}
                              };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
          M_Questions_Class model=this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.C_id;
            sp[1].Value = model.C_ClassName;
            sp[2].Value = model.C_Classid;
            sp[3].Value = model.C_OrderBy;
            return sp;
        }
        public M_Questions_Class GetModelFromReader(SqlDataReader rdr)
        {
            M_Questions_Class model = new M_Questions_Class();
            model.C_id = Convert.ToInt32(rdr["C_id"]);
            model.C_ClassName = rdr["C_ClassName"].ToString();
            model.C_OrderBy = Convert.ToInt32(rdr["C_OrderBy"]);
            rdr.Close();
            return model;
        }
    }
}