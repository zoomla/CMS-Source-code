using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ZoomLa.Model.Shop
{
    public class M_Shop_MoneyRegular:M_Base
    {
        public int ID { get; set; }
        /// <summary>
        /// 充值金额,必须大于该值才可获取该条优惠
        /// </summary>
        public double Min { get; set; }
        /// <summary>
        /// 赠送余额
        /// </summary>
        public double Purse { get; set; }
        /// <summary>
        /// 赠送银币
        /// </summary>
        public double Sicon { get; set; }
        /// <summary>
        /// 赠送积分
        /// </summary>
        public double Point { get; set; }
        /// <summary>
        /// 用户备注
        /// </summary>
        public string UserRemind { get; set; }
        /// <summary>
        /// 管理员备注
        /// </summary>
        public string AdminRemind { get; set; }
        /// <summary>
        /// 充值规则管理员ID
        /// </summary>
        public int AdminID { get; set; }
        /// <summary>
        /// 修改日期
        /// </summary>
        public DateTime EditDate { get; set; }
        public DateTime CDate { get; set; }

        public override string TbName { get { return "ZL_User_MoneyRegular"; } }
        public override string PK { get { return "ID"; } }

        public override string[,] FieldList()
        {
            string[,] Tablelist = {
        		        		{"ID","Int","4"},
        		        		{"Min","Money","8"},
        		        		{"Purse","Money","8"},
        		        		{"Sicon","Money","8"},
        		        		{"Point","Money","8"},
        		        		{"UserRemind","NVarChar","500"},
        		        		{"AdminRemind","NVarChar","500"},
        		        		{"AdminID","Int","4"},
        		        		{"EditDate","DateTime","8"},
        		        		{"CDate","DateTime","8"}
        };
            return Tablelist;
        }

        public override SqlParameter[] GetParameters()
        {
            M_Shop_MoneyRegular model = this;
            if (model.CDate <= DateTime.MinValue) { model.CDate = DateTime.Now; }
            if (model.EditDate <= DateTime.MinValue) { model.EditDate = DateTime.Now; }
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.Min;
            sp[2].Value = model.Purse;
            sp[3].Value = model.Sicon;
            sp[4].Value = model.Point;
            sp[5].Value = model.UserRemind;
            sp[6].Value = model.AdminRemind;
            sp[7].Value = model.AdminID;
            sp[8].Value = model.EditDate;
            sp[9].Value = model.CDate;
            return sp;
        }
        public M_Shop_MoneyRegular GetModelFromReader(DbDataReader rdr)
        {
            M_Shop_MoneyRegular model = new M_Shop_MoneyRegular();
            model.ID = ConvertToInt(rdr["ID"]);
            model.Min = ConverToDouble(rdr["Min"]);
            model.Purse = ConverToDouble(rdr["Purse"]);
            model.Sicon = ConverToDouble(rdr["Sicon"]);
            model.Point = ConverToDouble(rdr["Point"]);
            model.UserRemind = ConverToStr(rdr["UserRemind"]);
            model.AdminRemind = ConverToStr(rdr["AdminRemind"]);
            model.AdminID = ConvertToInt(rdr["AdminID"]);
            model.EditDate = ConvertToDate(rdr["EditDate"]);
            model.CDate = ConvertToDate(rdr["CDate"]);
            rdr.Close();
            return model;
        }
    }
}
