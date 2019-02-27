using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ZoomLa.Model.Shop
{
    public class M_Shop_RegionPrice : M_Base
    {
        public int ID { get; set; }
        /// <summary>
        /// 添加完成后附加入ProID
        /// </summary>
        public int ProID { get; set; }
        /// <summary>
        /// 存于FarePrice
        /// </summary>
        public string Guid { get; set; }
        /// <summary>
        /// 价格与区域详情
        /// </summary>
        public string Info { get; set; }
        public string Remind { get; set; }
        public override string TbName { get { return "ZL_Shop_RegionPrice"; } }
        public override string PK { get { return "ID"; } }

        public override string[,] FieldList()
        {
            string[,] Tablelist = {
        		        		{"ID","Int","4"},
        		        		{"ProID","Int","4"},
        		        		{"Guid","NVarChar","200"},
        		        		{"Info","NText","50000"},
        		        		{"Remind","NVarChar","1000"}
        };
            return Tablelist;
        }

        public override SqlParameter[] GetParameters()
        {
            M_Shop_RegionPrice model = this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.ProID;
            sp[2].Value = model.Guid;
            sp[3].Value = model.Info;
            sp[4].Value = model.Remind;
            return sp;
        }
        public M_Shop_RegionPrice GetModelFromReader(DbDataReader rdr)
        {
            M_Shop_RegionPrice model = new M_Shop_RegionPrice();
            model.ID = ConvertToInt(rdr["ID"]);
            model.ProID = ConvertToInt(rdr["ProID"]);
            model.Guid = ConverToStr(rdr["Guid"]);
            model.Info = ConverToStr(rdr["Info"]);
            model.Remind = ConverToStr(rdr["Remind"]);
            rdr.Close();
            return model;
        }
    }
    public class M_RegionPrice_Price
    {
        public int id = 0;
        public string region = "";
        public DataTable price = new DataTable();
    }
}
