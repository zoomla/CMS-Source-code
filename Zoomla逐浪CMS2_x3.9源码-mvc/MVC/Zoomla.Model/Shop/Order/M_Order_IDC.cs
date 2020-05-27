using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ZoomLa.Model.Shop
{
    /*
     * 用于存储扩展的IDC订单信息,与Order关联
     */
    public class M_Order_IDC : M_Base
    {
        public int ID { get; set; }
        public string OrderNo { get; set; }
        /// <summary>
        /// 绑定域名
        /// </summary>
        public string Domain { get; set; }
        /// <summary>
        /// 商品信息
        /// </summary>
        public string ProInfo { get; set; }
        /// <summary>
        /// 起始时间
        /// </summary>
        public DateTime STime { get; set; }
        /// <summary>
        /// 到期时间
        /// </summary>
        public DateTime ETime { get; set; }
        /// <summary>
        /// 订单状态预留
        /// </summary>
        public int ZStatus { get; set; }
        /// <summary>
        /// 订单类型
        /// </summary>
        public int ZType { get; set; }
        public string Remind { get; set; }
        /// <summary>
        /// 管理名称
        /// </summary>
        public string AdminName { get; set; }
        /// <summary>
        /// 管理密码
        /// </summary>
        public string AdminPwd { get; set; }
        /// <summary>
        /// 所购商品ID,续费时读取
        /// </summary>
        public int ProID { get; set; }

        public override string TbName { get { return "ZL_Order_IDC"; } }
        public override string PK { get { return "ID"; } }

        public override string[,] FieldList()
        {
            string[,] Tablelist = {
        		        		{"ID","Int","4"},
        		        		{"OrderNo","NVarChar","400"},
        		        		{"Domain","NVarChar","500"},
        		        		{"ProInfo","NVarChar","4000"},
        		        		{"STime","DateTime","8"},
        		        		{"ETime","DateTime","8"},
        		        		{"ZStatus","Int","4"},
        		        		{"ZType","Int","4"},
        		        		{"Remind","NVarChar","500"},
        		        		{"AdminName","NVarChar","500"},
        		        		{"AdminPwd","NVarChar","500"},
                                {"ProID","Int","4"}
        };
            return Tablelist;
        }

        public override SqlParameter[] GetParameters()
        {
            M_Order_IDC model = this;
            SqlParameter[] sp = GetSP();
            if (model.STime <= DateTime.MinValue) { model.STime = DateTime.Now; }
            if (model.ETime <= DateTime.MinValue) { model.ETime = DateTime.Now; }
            sp[0].Value = model.ID;
            sp[1].Value = model.OrderNo;
            sp[2].Value = model.Domain;
            sp[3].Value = model.ProInfo;
            sp[4].Value = model.STime;
            sp[5].Value = model.ETime;
            sp[6].Value = model.ZStatus;
            sp[7].Value = model.ZType;
            sp[8].Value = model.Remind;
            sp[9].Value = model.AdminName;
            sp[10].Value = model.AdminPwd;
            sp[11].Value = model.ProID;
            return sp;
        }
        public M_Order_IDC GetModelFromReader(DbDataReader rdr)
        {
            M_Order_IDC model = new M_Order_IDC();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.OrderNo = ConverToStr(rdr["OrderNo"]);
            model.Domain = ConverToStr(rdr["Domain"]);
            model.ProInfo = ConverToStr(rdr["ProInfo"]);
            model.STime = ConvertToDate(rdr["STime"]);
            model.ETime = ConvertToDate(rdr["ETime"]);
            model.ZStatus = ConvertToInt(rdr["ZStatus"]);
            model.ZType = ConvertToInt(rdr["ZType"]);
            model.Remind = ConverToStr(rdr["Remind"]);
            model.AdminName = ConverToStr(rdr["AdminName"]);
            model.AdminPwd = ConverToStr(rdr["AdminPwd"]);
            model.ProID = Convert.ToInt32(rdr["ProID"]);
            rdr.Close();
            return model;
        }
    }
}
