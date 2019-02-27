using System;
using System.Data.SqlClient;
using System.Data;
namespace ZoomLa.Model
{
    public class M_ZL_GroupBuy : M_Base
    {
        #region 定义字段
        public int ID { get; set; }
        public double Price { get; set; }
        public int Number { get; set; }
        public int ShopID { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public double Count { get; set; }
        #endregion

        public override string TbName { get { return "ZL_GroupBuy"; } }
        /// <summary>
        /// 返回实体列表数组
        /// </summary>
        /// <returns>String[]</returns>
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"ID","Int","4"},
                                  {"price","Money","8"},
                                  {"number","Int","4"},
                                  {"ShopID","Int","4"}, 
                                  {"count","Money","8"}
                                 };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_ZL_GroupBuy model = this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.Price;
            sp[2].Value = model.Number;
            sp[3].Value = model.ShopID;
            sp[4].Value = model.Count;
            return sp;
        }

        public M_ZL_GroupBuy GetModelFromReader(SqlDataReader rdr)
        {
            M_ZL_GroupBuy model = new M_ZL_GroupBuy();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.Price = Convert.ToDouble(rdr["price"]);
            model.Number = ConvertToInt(rdr["number"]);
            model.ShopID = Convert.ToInt32(rdr["ShopID"]);
            model.Count = ConverToDouble(rdr["count"]);
            rdr.Close();
            return model;
        }
    }
}