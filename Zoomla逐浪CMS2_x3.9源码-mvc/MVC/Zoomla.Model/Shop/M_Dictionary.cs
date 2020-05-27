using System;
using System.Data.SqlClient;
using System.Data;
namespace ZoomLa.Model
{
    /// <summary>
    /// 数据字典模型
    /// </summary>
    public class M_Dictionary:M_Base
    {
        public M_Dictionary()
        {
            this.DicName = string.Empty;
        }
        public M_Dictionary(bool value)
        {
            this.IsNull = value;
        }
        /// <summary>
        /// 数据字典ID
        /// </summary>
        public int DicID { get; set; }
        /// <summary>
        /// 数据字典分类ID
        /// </summary>
        public int DicCate { get; set; }
        /// <summary>
        /// 数据字典项名称
        /// </summary>
        public string DicName { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsUsed { get; set; }
        /// <summary>
        /// 是否空实例
        /// </summary>
        public bool IsNull { get; private set; }
        public override string PK { get { return "DicID"; } }
        public override string TbName { get { return "ZL_Datadic"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = { 
                                  {"DicID","Int","4"},
                                  {"DicCate","Int","4"},
                                  {"DicName","NVarChar","50"},
                                  {"IsUsed","Bit","50"}
                                 };
            return Tablelist;
        }
        public static string[,] FieldList2() { return new M_Dictionary().FieldList(); }
        public override SqlParameter[] GetParameters()
        {
            M_Dictionary model = this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.DicID;
            sp[1].Value = model.DicCate;
            sp[2].Value = model.DicName;
            sp[3].Value = model.IsUsed;
            return sp;
        }
        public M_Dictionary GetModelFromReader(SqlDataReader rdr)
        {

            M_Dictionary model = new M_Dictionary();
            model.DicID = Convert.ToInt32(rdr["DicID"]);
            model.DicCate = Convert.ToInt32(rdr["DicCate"]);
            model.DicName = rdr["DicName"].ToString();
            model.IsUsed = Convert.ToBoolean(rdr["IsUsed"]);
            rdr.Close();
            rdr.Dispose();
            return model;
        }
    }
}