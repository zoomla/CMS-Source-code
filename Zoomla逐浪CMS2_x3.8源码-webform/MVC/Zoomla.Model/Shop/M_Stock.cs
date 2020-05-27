using System;
using System.Data.SqlClient;
using System.Data;
namespace ZoomLa.Model
{
    public class M_Stock:M_Base
    {
        public int id { get; set; }
        /// <summary>
        /// 单据号
        /// </summary>
        public string danju { get; set; }
        /// <summary>
        /// 0:入库|1:出库
        /// </summary>
        public int stocktype { get; set; }
        public DateTime addtime { get; set; }
        /// <summary>
        /// 添加用户
        /// </summary>
        public string adduser { get; set; }
        /// <summary>
        /// 详情备注
        /// </summary>
        public string content { get; set; }
        /// <summary>
        /// 商品ID
        /// </summary>
        public int proid { get; set; }
        /// <summary>
        /// 商品数量
        /// </summary>
        public int Pronum { get; set; }
        /// <summary>
        /// 商品名
        /// </summary>
        public string proname { get; set; }
        public int UserID { get; set; }
        public int AdminID { get; set; }
        public int StoreID{get;set;}
        public M_Stock()
        {
            this.danju = string.Empty;
            this.proname = string.Empty;
            this.adduser = string.Empty;
            this.content = string.Empty;
            this.UserID = 0;
            this.AdminID = 0;
            this.StoreID = 0;
        }
        public override string TbName { get { return "ZL_Stock"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"id","Int","4"},
                                  {"stocktype","Int","4"},
                                  {"danju","NVarChar","500"},
                                  {"proname","NVarChar","500"}, 
                                  {"addtime","DateTime","8"},
                                  {"adduser","NVarChar","50"},
                                  {"content","NText","1000"},
                                  {"proid","Int","4"},
                                  {"pronum","Int","4"},
                                  {"UserID","Int","4"} ,
                                  {"AdminID","Int","4"} ,
                                  {"StoreID","Int","4"} 
                                 };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_Stock model = this;
            if (model.addtime <= DateTime.MinValue) { model.addtime = DateTime.Now; }
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.id;
            sp[1].Value = model.stocktype;
            sp[2].Value = model.danju;
            sp[3].Value = model.proname;
            sp[4].Value = model.addtime;
            sp[5].Value = model.adduser;
            sp[6].Value = model.content;
            sp[7].Value = model.proid;
            sp[8].Value = model.Pronum;
            sp[9].Value = model.UserID;
            sp[10].Value = model.AdminID;
            sp[11].Value = model.StoreID;
            return sp;
        }
        public M_Stock GetModelFromReader(SqlDataReader rdr)
        {
            M_Stock model = new M_Stock();
            model.id = Convert.ToInt32(rdr["id"]);
            model.stocktype = ConvertToInt(rdr["stocktype"]);
            model.danju = ConverToStr(rdr["danju"]);
            model.proname = ConverToStr(rdr["proname"]);
            model.addtime = ConvertToDate(rdr["addtime"]);
            model.adduser = ConverToStr(rdr["adduser"]);
            model.content = ConverToStr(rdr["content"]);
            model.proid = ConvertToInt(rdr["proid"]);
            model.Pronum = ConvertToInt(rdr["pronum"]);
            model.UserID = ConvertToInt(rdr["UserID"]);
            model.AdminID = ConvertToInt(rdr["AdminID"]);
            model.StoreID = ConvertToInt(rdr["StoreID"]);
            rdr.Close();
            rdr.Dispose();
            return model;
        }
    }
}