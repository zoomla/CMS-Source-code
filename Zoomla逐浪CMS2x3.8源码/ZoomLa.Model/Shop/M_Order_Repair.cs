using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ZoomLa.Model.Shop
{
    public class M_Order_Repair : M_Base
    {
        public int ID { get; set; }
        /// <summary>
        /// 提交数量
        /// </summary>
        public int ProNum { get; set; }
        /// <summary>
        /// 问题描述
        /// </summary>
        public string Deailt { get; set; }
        /// <summary>
        /// 商品图片信息
        /// </summary>
        public string ProImgs { get; set; }
        /// <summary>
        /// 凭据类型
        /// </summary>
        public string CretType { get; set; }
        /// <summary>
        /// 收货类型
        /// </summary>
        public int ReProType { get; set; }
        /// <summary>
        /// 取货省市
        /// </summary>
        public string TakeProCounty { get; set; }
        /// <summary>
        /// 取货详细地址
        /// </summary>
        public string TakeProAddress { get; set; }
        /// <summary>
        /// 收货省市
        /// </summary>
        public string ReProCounty { get; set; }
        /// <summary>
        /// 收货详细地址
        /// </summary>
        public string ReProAddress { get; set; }
        /// <summary>
        /// 用户姓名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 商品id
        /// </summary>
        public int ProID { get; set; }
        /// <summary>
        /// 申请时间
        /// </summary>
        public DateTime CDate { get; set; }
        /// <summary>
        /// 取货时间
        /// </summary>
        public DateTime TakeTime { get; set; }
        /// <summary>
        /// 返修单状态 1,审核  2,未审核
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 用户id
        /// </summary>
        public int UserID { get; set; }
        /// <summary>
        /// 服务类型  1,退货  2,换货  3,维修
        /// </summary>
        public string ServiceType { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNO { get; set; }
        /// <summary>
        /// CartProID
        /// </summary>
        public int CartID { get; set; }
        /// <summary>
        /// 0:cart,1:order
        /// </summary>
        public int ApplyType { get; set; }
        public M_Order_Repair()
        {
            this.CDate = DateTime.Now;
            this.TakeTime = DateTime.Now;
        }

        public override string TbName { get { return "ZL_Order_Repair"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist ={
                                    {"ID","Int","4"},
                                    {"ProNum","Int","4" },
                                    {"Deailt","NVarChar","1000" },
                                    {"ProImgs","NVarChar","2000" },
                                    {"CretType","NVarChar","100" },
                                    {"ReProType","Int","4" },
                                    {"TakeProCounty","NVarChar","500" },
                                    {"TakeProAddress","NVarChar","1000" },
                                    {"ReProCounty","NVarChar","500" },
                                    {"ReProAddress","NVarChar","1000" },
                                    {"UserName","NVarChar","500" },
                                    {"Phone","NVarChar","100" },
                                    {"ProID","Int","4" },
                                    {"CDate","DateTime","8" },
                                    {"TakeTime","DateTime","8" },
                                    {"Status","Int","4" },
                                    {"UserID","Int","4" },
                                    {"ServiceType","NVarChar","500" },
                                    {"OrderNO","NVarChar","500" },
                                    { "CartID","Int","4"},
                                    {"ApplyType","Int","4" }
                                 };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_Order_Repair model = this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.ProNum;
            sp[2].Value = model.Deailt;
            sp[3].Value = model.ProImgs;
            sp[4].Value = model.CretType;
            sp[5].Value = model.ReProType;
            sp[6].Value = model.TakeProCounty;
            sp[7].Value = model.TakeProAddress;
            sp[8].Value = model.ReProCounty;
            sp[9].Value = model.ReProAddress;
            sp[10].Value = model.UserName;
            sp[11].Value = model.Phone;
            sp[12].Value = model.ProID;
            sp[13].Value = model.CDate;
            sp[14].Value = model.TakeTime;
            sp[15].Value = model.Status;
            sp[16].Value = model.UserID;
            sp[17].Value = model.ServiceType;
            sp[18].Value = model.OrderNO;
            sp[19].Value = model.CartID;
            sp[20].Value = model.ApplyType;
            return sp;
        }
        public M_Order_Repair GetModelFromReader(SqlDataReader rdr)
        {
            M_Order_Repair model = new M_Order_Repair();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.ProNum = ConvertToInt(rdr["ProNum"]);
            model.Deailt = ConverToStr(rdr["Deailt"]);
            model.ProImgs = ConverToStr(rdr["ProImgs"]);
            model.CretType = ConverToStr(rdr["CretType"]);
            model.ReProType = ConvertToInt(rdr["ReProType"]);
            model.TakeProCounty = ConverToStr(rdr["TakeProCounty"]);
            model.TakeProAddress = ConverToStr(rdr["TakeProAddress"]);
            model.ReProCounty = ConverToStr(rdr["ReProCounty"]);
            model.ReProAddress = ConverToStr(rdr["ReProAddress"]);
            model.UserName = ConverToStr(rdr["UserName"]);
            model.Phone = ConverToStr(rdr["Phone"]);
            model.ProID = ConvertToInt(rdr["ProID"]);
            model.CDate = ConvertToDate(rdr["CDate"]);
            model.TakeTime = ConvertToDate(rdr["TakeTime"]);
            model.Status = ConvertToInt(rdr["Status"]);
            model.UserID = ConvertToInt(rdr["UserID"]);
            model.ServiceType = ConverToStr(rdr["ServiceType"]);
            model.OrderNO = ConverToStr(rdr["OrderNO"]);
            model.CartID = ConvertToInt(rdr["CartID"]);
            model.ApplyType = ConvertToInt(rdr["ApplyType"]);
            rdr.Close();
            return model;
        }
    }
}
