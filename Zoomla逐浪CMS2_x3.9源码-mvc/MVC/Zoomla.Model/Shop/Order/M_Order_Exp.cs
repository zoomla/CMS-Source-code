using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ZoomLa.Model.Shop
{
    public class M_Order_Exp : M_Base
    {

        public int ID { get; set; }
        public int OrderID { get; set; }
        /// <summary>
        /// 快递公司类型
        /// </summary>
        public string CompType { get; set; }
        /// <summary>
        /// 快递公司名称
        /// </summary>
        public string ExpComp { get; set; }
        /// <summary>
        /// 快递单号
        /// </summary>
        public string ExpNo { get; set; }
        /// <summary>
        /// 快递状态(同于Order中)
        /// </summary>
        public string ExpStatus { get; set; }
        /// <summary>
        /// 快递备注
        /// </summary>
        public string ExpRemind { get; set; }
        /// <summary>
        /// 发货时间
        /// </summary>
        public string SendDate { get; set; }
        /// <summary>
        /// 签收时间
        /// </summary>
        public string SignDate { get; set; }
        /// <summary>
        /// 退货时间
        /// </summary>
        public string BackDate { get; set; }
        /// <summary>
        /// 用户备注
        /// </summary>
        public string UserRemind { get; set; }
        /// <summary>
        /// 管理员备注
        /// </summary>
        public string AdminRemind { get; set; }
        public int UserID { get; set; }
        public DateTime CDate { get; set; }

        public override string TbName { get { return "ZL_Order_Exp"; } }
        public override string PK { get { return "ID"; } }

        public override string[,] FieldList()
        {
            string[,] Tablelist = {
        		        		{"ID","Int","4"},
        		        		{"OrderID","Int","4"},
        		        		{"CompType","NVarChar","500"},
        		        		{"ExpComp","NVarChar","500"},
        		        		{"ExpNo","NVarChar","500"},
        		        		{"ExpStatus","NVarChar","500"},
        		        		{"ExpRemind","NVarChar","500"},
        		        		{"SendDate","NVarChar","500"},
        		        		{"SignDate","NVarChar","500"},
        		        		{"BackDate","NVarChar","500"},
        		        		{"UserRemind","NVarChar","500"},
        		        		{"AdminRemind","NVarChar","500"},
        		        		{"UserID","Int","4"},
        		        		{"CDate","DateTime","8"}
        };
            return Tablelist;
        }

        public override SqlParameter[] GetParameters()
        {
            M_Order_Exp model = this;
            if (model.CDate <= DateTime.MinValue) { model.CDate = DateTime.Now; }
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.OrderID;
            sp[2].Value = model.CompType;
            sp[3].Value = model.ExpComp;
            sp[4].Value = model.ExpNo;
            sp[5].Value = model.ExpStatus;
            sp[6].Value = model.ExpRemind;
            sp[7].Value = model.SendDate;
            sp[8].Value = model.SignDate;
            sp[9].Value = model.BackDate;
            sp[10].Value = model.UserRemind;
            sp[11].Value = model.AdminRemind;
            sp[12].Value = model.UserID;
            sp[13].Value = model.CDate;
            return sp;
        }
        public M_Order_Exp GetModelFromReader(DbDataReader rdr)
        {
            M_Order_Exp model = new M_Order_Exp();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.OrderID = ConvertToInt(rdr["OrderID"]);
            model.CompType = ConverToStr(rdr["CompType"]);
            model.ExpComp = ConverToStr(rdr["ExpComp"]);
            model.ExpNo = ConverToStr(rdr["ExpNo"]);
            model.ExpStatus = ConverToStr(rdr["ExpStatus"]);
            model.ExpRemind = ConverToStr(rdr["ExpRemind"]);
            model.SendDate = ConverToStr(rdr["SendDate"]);
            model.SignDate = ConverToStr(rdr["SignDate"]);
            model.BackDate = ConverToStr(rdr["BackDate"]);
            model.UserRemind = ConverToStr(rdr["UserRemind"]);
            model.AdminRemind = ConverToStr(rdr["AdminRemind"]);
            model.UserID = ConvertToInt(rdr["UserID"]);
            model.CDate = ConvertToDate(rdr["CDate"]);
            rdr.Close();
            return model;
        }
    }
}
