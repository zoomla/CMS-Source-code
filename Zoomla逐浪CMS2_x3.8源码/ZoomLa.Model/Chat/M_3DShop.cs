using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ZoomLa.Model.Chat
{
    public class M_3DShop:M_Base
    {
        public int ID { get; set; }
        /// <summary>
        /// 店铺名称
        /// </summary>
        public string ShopName { get; set; }
        /// <summary>
        /// 店铺图片
        /// </summary>
        public string ShopImg { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        /// <summary>
        /// X轴坐标
        /// </summary>
        public string posX { get; set; }
        /// <summary>
        /// Y轴坐标
        /// </summary>
        public string posY { get; set; }
        /// <summary>
        /// 店铺状态
        /// </summary>
        public int ShopStatus { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remind { get; set; }
        public M_3DShop()
        {

        }
        public override string TbName { get { return "ZL_3DShop"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
        	            {"ShopName","NVarChar","50"},            
                        {"ShopImg","NVarChar","100"},            
                        {"UserID","Int","4"},            
                        {"UserName","NVarChar","50"},            
                        {"posX","NVarChar","50"},            
                        {"posY","NVarChar","50"},            
                        {"ShopStatus","Int","4"},            
                        {"Remind","NVarChar","50"}            
              
        };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_3DShop model = this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ShopName;
            sp[1].Value = model.ShopImg;
            sp[2].Value = model.UserID;
            sp[3].Value = model.UserName;
            sp[4].Value = model.posX;
            sp[5].Value = model.posY;
            sp[6].Value = model.ShopStatus;
            sp[7].Value = model.Remind;
            return sp;
        }
        public M_3DShop GetModelFromReader(DbDataReader rdr)
        {
            M_3DShop model = new M_3DShop();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.ShopName = ConverToStr(rdr["ShopName"]);
            model.ShopImg = ConverToStr(rdr["ShopImg"]);
            model.UserID = ConvertToInt(rdr["UserID"]);
            model.UserName = ConverToStr(rdr["UserName"]);
            model.posX = ConverToStr(rdr["posX"]);
            model.posY = ConverToStr(rdr["posY"]);
            model.ShopStatus = ConvertToInt(rdr["ShopStatus"]);
            model.Remind = ConverToStr(rdr["Remind"]);
            rdr.Close();
            return model;
        }
    }
}
