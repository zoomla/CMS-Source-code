using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace ZoomLa.Model.Site
{
   public class M_IDC_DomPrice:M_Base
    {
        public int ID { get; set; }
        public string DomName { get; set; }
        public decimal DomPrice { get; set; }
       /// <summary>
       /// 商品类型，域名还是其他
       /// </summary>
        public int DomType { get; set; }
        public DateTime CreateDate { get; set; }
        public int Status { get; set; }
        public string ProDetail { get; set; }
       /// <summary>
       /// 0:未选定,1:新网,2:万网
       /// </summary>
        public int IFSupplier { get; set; }

        public override string TbName { get { return "ZL_IDC_DomainPrice"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"ID","Int","4"},
                                  {"DomName","VarChar","50"},
                                  {"DomPrice","Decimal","10"},
                                  {"DomType","Int","4"}, 
                                  {"CreateDate","DateTime","20"}, 
                                  {"Status","Int","20"}, 
                                  {"ProDetail","NVarChar","200"},
                                  {"IFSupplier","Int","10"}
                                 };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_IDC_DomPrice model = this;
            if(model.CreateDate <= DateTime.MinValue) { model.CreateDate = DateTime.Now; }
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.DomName;
            sp[2].Value = model.DomPrice;
            sp[3].Value = model.DomType;
            sp[4].Value = model.CreateDate;
            sp[5].Value = model.Status;
            sp[6].Value = model.ProDetail;
            sp[7].Value = model.IFSupplier;
            return sp;
        }
        public M_IDC_DomPrice GetModelFromReader(SqlDataReader rdr)
        {
            M_IDC_DomPrice model = new M_IDC_DomPrice();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.DomName = ConverToStr(rdr["DomName"]);
            model.DomPrice = ConverToDec(rdr["DomPrice"]);
            model.DomType = ConvertToInt(rdr["DomType"]);
            model.CreateDate = ConvertToDate(rdr["CreateDate"] == DBNull.Value ? DateTime.Now : rdr["CreateDate"]);
            model.Status = ConvertToInt(rdr["Status"]==DBNull.Value?"0":"1");
            model.ProDetail = ConverToStr(rdr["ProDetail"]);
            model.IFSupplier = ConvertToInt(rdr["IFSupplier"] == DBNull.Value ? "0" : "1");
            rdr.Close();
            return model;
        }
    }
}
