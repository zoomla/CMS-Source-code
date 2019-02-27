using System;
using System.Data.SqlClient;
using System.Data;
namespace ZoomLa.Model
{
    /// <summary>
    /// 数据字典分类模型
    /// </summary>
    public class M_DicCategory:M_Base
    {

        public M_DicCategory()
        {
            this.CategoryName = String.Empty;
        }
        public M_DicCategory(bool value)
        {
            this.IsNull = value;
        }
        /// <summary>
        /// 数据字典分类ID
        /// </summary>
        public int DicCateID { get; set; }
        /// <summary>
        /// 数据字典分类名
        /// </summary>
        public string CategoryName { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsUsed { get; set; }
        /// <summary>
        /// 是否空实例
        /// </summary>
        public bool IsNull { get; private set; }
        public override string PK { get { return "DicCateID"; } }
        public override string TbName { get { return "ZL_Datadiccategory"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"DicCateID","Int","4"},
                                  {"CategoryName","NVarChar","50"},
                                  {"IsUsed","Bit","50"}
                                 };
            return Tablelist;
        }
        public static string[,] FieldList2() { return new M_DicCategory().FieldList(); }
        public override SqlParameter[] GetParameters()
        {
            M_DicCategory model = this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.DicCateID;
            sp[1].Value = model.CategoryName;
            sp[2].Value = model.IsUsed;
            return sp;
        }
        public M_DicCategory GetModelFromReader(SqlDataReader rdr)
        {

            M_DicCategory model = new M_DicCategory();
            model.DicCateID = Convert.ToInt32(rdr["DicCateID"]);
            model.CategoryName = ConverToStr(rdr["CategoryName"]);
            model.IsUsed = ConverToBool(rdr["IsUsed"]);
            rdr.Close();
            rdr.Dispose();
            return model;
        }
    }
}