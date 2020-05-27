using System;
using System.Data.SqlClient;
using System.Data;
namespace ZoomLa.Model
{
    [Serializable]
    public class M_Defray : M_Base
    {

        #region 构造函数
        public M_Defray()
        {
        }

        public M_Defray
        (
            int flow,
            string Pay_name,
            string Pay_intf,
            string Client_id,
            string Title,
            string money_code,
            string number
        )
        {
            this.flow = flow;
            this.Pay_name = Pay_name;
            this.Pay_intf = Pay_intf;
            this.Client_id = Client_id;
            this.Title = Title;
            this.money_code = money_code;
            this.number = number;
        }
        /// <summary>
        /// 返回实体列表数组
        /// </summary>
        /// <returns>String[]</returns>
        public string[] DefrayList()
        {
            string[] Tablelist = { "flow", "Pay_name", "Pay_intf", "Client_id", "Title", "money_code", "number" };
            return Tablelist;
        }
        #endregion

        #region 属性定义
        /// <summary>
        /// 
        /// </summary>
        public int flow { get; set; }
        /// <summary>
        /// 方式名称
        /// </summary>
        public string Pay_name { get; set; }
        /// <summary>
        /// 接品类型
        /// </summary>
        public string Pay_intf { get; set; }
        /// <summary>
        /// 客户号
        /// </summary>
        public string Client_id { get; set; }
        /// <summary>
        /// 身份标记
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 支持交易货币
        /// </summary>
        public string money_code { get; set; }
        /// <summary>
        /// 支付手续费
        /// </summary>
        public string number { get; set; }
        #endregion

        public override string PK { get { return "flow"; } }
        public override string TbName { get { return "ZL_Defray"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"flow","Int","4"},
                                  {"Pay_name","VarChar","100"},
                                  {"Pay_intf","VarChar","100"},
                                  {"Client_id","VarChar","50"}, 
                                  {"Title","VarChar","100"}, 
                                  {"money_code","VarChar","100"}, 
                                  {"number","VarChar","50"}
                                 };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_Defray model = this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.flow;
            sp[1].Value = model.Pay_name;
            sp[2].Value = model.Pay_intf;
            sp[3].Value = model.Client_id;
            sp[4].Value = model.Title;
            sp[5].Value = model.money_code;
            sp[6].Value = model.number;
            return sp;
        }

        public M_Defray GetModelFromReader(SqlDataReader rdr)
        {
            M_Defray model = new M_Defray();
            model.flow = Convert.ToInt32(rdr["flow"]);
            model.Pay_name = rdr["Pay_name"].ToString();
            model.Pay_intf = rdr["Pay_intf"].ToString();
            model.Client_id = rdr["Client_id"].ToString();
            model.Title = rdr["Title"].ToString();
            model.money_code = rdr["money_code"].ToString();
            model.number = rdr["number"].ToString();
            rdr.Close();
            return model;
        }
    }
}


