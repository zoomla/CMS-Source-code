using System;
using System.Data.SqlClient;
using System.Data;
namespace ZoomLa.Model
{
    /// <summary>
    ///ZL_UserStoreTable业务实体
    /// </summary>
    [Serializable]
    public class M_UserStoreTable:M_Base
    {
        #region 构造函数
        public M_UserStoreTable()
        {
            this.StoreName = string.Empty;
            this.StoreContent = string.Empty;
            this.StoreType = string.Empty;
            this.StoreProvince = string.Empty;
            this.StoreCity = string.Empty;
        }
        #endregion
        #region 属性定义
        public int ID { get; set; }
        ///<summary>
        ///用户ID
        ///</summary>
        public int UserID { get; set; }
        ///<summary>
        ///商铺名
        ///</summary>
        public string StoreName { get; set; }
        ///<summary>
        ///商铺简介
        ///</summary>
        public string StoreContent { get; set; }
        ///<summary>
        ///信用
        ///</summary>
        public int StoreCredit { get; set; }
        ///<summary>
        ///商铺VIP级别
        ///</summary>
        public int StoreVip { get; set; }
        ///<summary>
        ///商铺类型
        ///</summary>
        public string StoreType { get; set; }
        ///<summary>
        ///商铺余额
        ///</summary>
        public decimal StoreCash { get; set; }
        ///<summary>
        ///商铺状态
        ///</summary>
        public int StoreState { get; set; }
        ///<summary>
        ///申请时间
        ///</summary>
        public DateTime AddTime { get; set; }
        ///<summary>
        ///所在省
        ///</summary>
        public string StoreProvince { get; set; }
        ///<summary>
        ///所在市
        ///</summary>
        public string StoreCity { get; set; }
        #endregion
     
        public override string TbName { get { return "ZL_UserStoreTable"; } }
        /// <summary>
        /// 返回实体列表数组
        /// </summary>
        /// <returns>String[]</returns>
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"ID","Int","4"},
                                  {"UserID","Int","4"},
                                  {"StoreName","NVarChar","100"},
                                  {"StoreContent","NText","400"}, 
                                  {"StoreCredit","Int","4"},
                                  {"StoreVip","Int","4"},
                                  {"StoreType","NVarChar","100"},
                                  {"StoreCash","Decimal","8,2"}, 
                                  {"StoreState","Int","4"},
                                  {"AddTime","DateTime","8"},
                                  {"StoreProvince","NVarChar","100"},
                                  {"StoreCity","NVarChar","100"}
                                 };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_UserStoreTable model = this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.UserID;
            sp[2].Value = model.StoreName;
            sp[3].Value = model.StoreContent;
            sp[4].Value = model.StoreCredit;
            sp[5].Value = model.StoreVip;
            sp[6].Value = model.StoreType;
            sp[7].Value = model.StoreCash;
            sp[8].Value = model.StoreState;
            sp[9].Value = model.AddTime;
            sp[10].Value = model.StoreProvince;
            sp[11].Value = model.StoreCity;
            return sp;
        }

        public  M_UserStoreTable GetModelFromReader(SqlDataReader rdr)
        {
            M_UserStoreTable model = new M_UserStoreTable();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.UserID = Convert.ToInt32(rdr["UserID"]);
            model.StoreName = ConverToStr(rdr["StoreName"]);
            model.StoreContent = ConverToStr(rdr["StoreContent"]);
            model.StoreCredit = ConvertToInt(rdr["StoreCredit"]);
            model.StoreVip = ConvertToInt(rdr["StoreVip"]);
            model.StoreType = ConverToStr(rdr["StoreType"]);
            model.StoreCash = ConvertToInt(rdr["StoreCash"]);
            model.StoreState = ConvertToInt(rdr["StoreState"]);
            model.AddTime = ConvertToDate(rdr["AddTime"]);
            model.StoreProvince = ConverToStr(rdr["StoreProvince"]);
            model.StoreCity = ConverToStr(rdr["StoreCity"]);
            rdr.Close();
            return model;
        }
    }
}