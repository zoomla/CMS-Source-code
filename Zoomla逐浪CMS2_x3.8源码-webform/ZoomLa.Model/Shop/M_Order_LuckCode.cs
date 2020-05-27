using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ZoomLa.Model
{
    public class M_Order_LuckCode : M_Base
    {
        public int ID { get; set; }
        public int Code { get; set; }
        public int UserID { get; set; }
        public int OrderID { get; set; }
        public string OrderNO { get; set; }
        public string CreateTime { get; set; }
        public string CreateTime2 { get; set; }
        public int ProID { get; set; }
        public M_Order_LuckCode()
        {

        }
        public override string TbName { get { return "ZL_Order_LuckCode"; } }

        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                        {"ID","Int","4"},
        	            {"Code","Int","4"},            
                        {"UserID","Int","4"},            
                        {"OrderID","Int","4"},            
                        {"OrderNO","NVarChar","50"},            
                        {"CreateTime","NVarChar","50"},            
                        {"ProID","Int","4"},
                        {"CreateTime2","NVarChar","50"}   
              
        };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_Order_LuckCode model = this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.Code;
            sp[2].Value = model.UserID;
            sp[3].Value = model.OrderID;
            sp[4].Value = model.OrderNO;
            sp[5].Value = model.CreateTime;
            sp[6].Value = model.ProID;
            sp[7].Value = model.CreateTime2;
            return sp;
        }
        public M_Order_LuckCode GetModelFromReader(SqlDataReader rdr)
        {
            M_Order_LuckCode model = new M_Order_LuckCode();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.Code = ConvertToInt(rdr["Code"]);
            model.UserID = Convert.ToInt32(rdr["UserID"]);
            model.OrderID = ConvertToInt(rdr["OrderID"]);
            model.OrderNO = ConverToStr(rdr["OrderNO"]);
            model.CreateTime = ConverToStr(rdr["CreateTime"]);
            model.ProID = ConvertToInt(rdr["ProID"]);
            model.CreateTime2 = ConverToStr(rdr["CreateTime2"]);
            rdr.Close();
            return model;
        }
    }
}
