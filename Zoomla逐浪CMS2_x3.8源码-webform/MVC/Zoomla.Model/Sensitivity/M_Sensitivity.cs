using System;
using System.Data.SqlClient;
using System.Data;
namespace ZoomLa.Model
{
    [Serializable]
    public class M_Sensitivity : M_Base
    {
        #region 构造函数
        public M_Sensitivity()
        {
        }

        public M_Sensitivity
        (
            int id,
            string keyname,
            int istrue
        )
        {
            this.id = id;
            this.keyname = keyname;
            this.istrue = istrue;
        }
        /// <summary>
        /// 返回实体列表数组
        /// </summary>
        /// <returns>String[]</returns>
        public string[] SensitivityList()
        {
            string[] Tablelist = { "id", "keyname", "istrue" };
            return Tablelist;
        }
        #endregion

        #region 属性定义
        /// <summary>
        /// 
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 关键字
        /// </summary>
        public string keyname { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public int istrue { get; set; }
        #endregion
        public override string TbName { get { return "ZL_Sensitivity"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"id","Int","4"},
                                  {"keyname","NVarChar","255"},
                                  {"istrue","Int","4"}
                              };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_Sensitivity model = this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.id;
            sp[1].Value = model.keyname;
            sp[2].Value = model.istrue;
            return sp;
        }
        public M_Sensitivity GetModelFromReader(SqlDataReader rdr)
        {
            M_Sensitivity model = new M_Sensitivity();
            model.id = Convert.ToInt32(rdr["id"]);
            model.keyname = ConverToStr(rdr["keyname"]);
            model.istrue = ConvertToInt(rdr["istrue"]);
            rdr.Close();
            return model;
        }
    }
}