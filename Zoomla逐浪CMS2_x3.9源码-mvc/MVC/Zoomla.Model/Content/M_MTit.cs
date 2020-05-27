using System;
using System.Data.SqlClient;
using System.Data;
namespace ZoomLa.Model
{
    [Serializable]
    public class M_MTit : M_Base
    {
        #region 构造函数
        public M_MTit()
        {
        }

        public M_MTit
        (
            int I_id,
            string Iinfo,
            string Iurl,
            int Itype,
            int Stype,
            int orderID
        )
        {
            this.I_id = I_id;
            this.Iinfo = Iinfo;
            this.Iurl = Iurl;
            this.Itype = Itype;
            this.Stype = Stype;
            this.orderID = orderID;
        }
        /// <summary>
        /// 返回实体列表数组
        /// </summary>
        /// <returns>String[]</returns>
        public string[] MTitList()
        {
            string[] Tablelist = { "I_id", "Iinfo", "Iurl", "Itype", "Stype", "orderID" };
            return Tablelist;
        }
        #endregion

        #region 属性定义
        /// <summary>
        /// 
        /// </summary>
        public int I_id { get; set; }
        public int orderID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Iinfo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Iurl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Itype { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Stype { get; set; }
        #endregion
        public override string PK { get { return "I_id"; } }
        public override string TbName { get { return "ZL_MTit"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"I_id","Int","4"},
                                  {"Iinfo","NVarChar","500"},
                                  {"Iurl","NVarChar","500"},
                                  {"Itype","Int","4"},
                                  {"Stype","Int","4"},
                                  {"orderid","Int","4"}
                                 };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_MTit model = this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.I_id;
            sp[1].Value = model.Iinfo;
            sp[2].Value = model.Iurl;
            sp[3].Value = model.Itype;
            sp[4].Value = model.Stype;
            sp[5].Value = model.orderID;
            return sp;
        }
        public M_MTit GetModelFromReader(SqlDataReader rdr)
        {
            M_MTit model = new M_MTit();
            model.I_id = Convert.ToInt32(rdr["I_id"]);
            model.Iinfo = rdr["Iinfo"].ToString();
            model.Iurl = rdr["Iurl"].ToString();
            model.Itype = Convert.ToInt32(rdr["Itype"]);
            model.Stype = Convert.ToInt32(rdr["Stype"]);
            model.orderID = Convert.ToInt32(rdr["orderid"]);
            rdr.Close();
            rdr.Dispose();
            return model;
        }
    }
}