using System;
using System.Data.SqlClient;
using System.Data;
namespace ZoomLa.Model
{
    public class M_Promotions : M_Base
    {
        #region 构造函数
        public M_Promotions()
        {
        }

        public M_Promotions
        (
            int Id,
            string Promoname,
            DateTime Promostart,
            DateTime Promoend,
            double Pricetop,
            double Priceend,
            string AddUser,
            int Integral,
            int GetPresent,
            double Presentmoney,
            int IntegralTure,
            string PromoProlist,
            DateTime Addtime,
            string TureUser
        )
        {
            this.Id = Id;
            this.Promoname = Promoname;
            this.Promostart = Promostart;
            this.Promoend = Promoend;
            this.Pricetop = Pricetop;
            this.Priceend = Priceend;
            this.AddUser = AddUser;
            this.Integral = Integral;
            this.GetPresent = GetPresent;
            this.Presentmoney = Presentmoney;
            this.IntegralTure = IntegralTure;
            this.PromoProlist = PromoProlist;
            this.Addtime = Addtime;
            this.TureUser = TureUser;
        }
        /// <summary>
        /// 返回实体列表数组
        /// </summary>
        /// <returns>String[]</returns>
        public string[] PromotionsList()
        {
            string[] Tablelist = { "Id", "Promoname", "Promostart", "Promoend", "Pricetop", "Priceend", "AddUser", "Integral", "GetPresent", "Presentmoney", "IntegralTure", "PromoProlist", "Addtime", "TureUser" };
            return Tablelist;
        }
        #endregion

        #region 属性定义
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 促销名称
        /// </summary>
        public string Promoname { get; set; }
        /// <summary>
        /// 起始时间
        /// </summary>
        public DateTime Promostart { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime Promoend { get; set; }
        /// <summary>
        /// 价格上限
        /// </summary>
        public double Pricetop { get; set; }
        /// <summary>
        /// 价格下限
        /// </summary>
        public double Priceend { get; set; }
        /// <summary>
        /// 添加人
        /// </summary>
        public string AddUser { get; set; }
        /// <summary>
        /// 积分
        /// </summary>
        public int Integral { get; set; }
        /// <summary>
        /// 可以得到礼品
        /// </summary>
        public int GetPresent { get; set; }
        /// <summary>
        /// 兑换金钱数
        /// </summary>
        public double Presentmoney { get; set; }
        /// <summary>
        /// 可以得到积分
        /// </summary>
        public int IntegralTure { get; set; }
        /// <summary>
        /// 礼品名称列表
        /// </summary>
        public string PromoProlist { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime Addtime { get; set; }
        /// <summary>
        /// 添加作者
        /// </summary>
        public string TureUser { get; set; }
        #endregion
        public override string TbName { get { return "ZL_Promotions"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"id","Int","4"},
                                  {"Promoname","NVarChar","4"},
                                  {"Promostart","DateTime","4"},
                                  {"Promoend","DateTime","4"},
                                  {"Pricetop","Money","4"},
                                  {"Priceend","Money","4"},
                                  {"AddUser","VarChar","4"},
                                  {"Integral","Int","4"},
                                  {"GetPresent","Int","4"},
                                  {"Presentmoney","Money","4"},
                                  {"IntegralTure","Int","4"},
                                  {"PromoProlist","VarChar","4"},
                                  {"Addtime","DateTime","8"},
                                  {"TureUser","VarChar","4"},
                                 };
            return Tablelist;
        }
        public string GetFieldAndPara()
        {
            string str = string.Empty;
            string[,] strArr = FieldList();
            for (int i = 0; i < strArr.GetLength(0); i++)
            {
                if (strArr[i, 0] != PK)
                {
                    str += "[" + strArr[i, 0] + "]=@" + strArr[i, 0] + ",";
                }
            }
            return str.Substring(0, str.Length - 1);
        }
        public SqlParameter[] GetParameters(M_Promotions model)
        {
            string[,] strArr = FieldList();
            SqlParameter[] sp = new SqlParameter[strArr.Length];

            for (int i = 0; i < strArr.GetLength(0); i++)
            {
                sp[i] = new SqlParameter("@" + strArr[i, 0], (SqlDbType)Enum.Parse(typeof(SqlDbType), strArr[i, 1]), Convert.ToInt32(strArr[i, 2]));
            }
            sp[0].Value = model.Id;
            sp[1].Value = model.Promoname;
            sp[2].Value = model.Promostart;
            sp[3].Value = model.Promoend;
            sp[4].Value = model.Pricetop;
            sp[5].Value = model.Priceend;
            sp[6].Value = model.AddUser;
            sp[7].Value = model.Integral;
            sp[8].Value = model.GetPresent;
            sp[9].Value = model.Presentmoney;
            sp[10].Value = model.IntegralTure;
            sp[11].Value = model.PromoProlist;
            sp[12].Value = model.Addtime;
            sp[13].Value = model.TureUser;
            return sp;
        }
        public M_Promotions GetModelFromReader(SqlDataReader rdr)
        {
            M_Promotions model = new M_Promotions();
            model.Id = Convert.ToInt32(rdr["Id"]);
            model.Promoname = rdr["Promoname"].ToString();
            model.Promostart = Convert.ToDateTime(rdr["Promostart"]);
            model.Promoend = Convert.ToDateTime(rdr["Promoend"]);
            model.Pricetop = Convert.ToInt32(rdr["Pricetop"]);
            model.Priceend = Convert.ToInt32(rdr["Priceend"]);
            model.AddUser = rdr["AddUser"].ToString();
            model.Integral = Convert.ToInt32(rdr["Integral"]);
            model.GetPresent = Convert.ToInt32(rdr["GetPresent"]);
            model.Presentmoney = Convert.ToInt32(rdr["Presentmoney"]);
            model.IntegralTure = Convert.ToInt32(rdr["IntegralTure"]);
            model.PromoProlist = rdr["PromoProlist"].ToString();
            model.Addtime = Convert.ToDateTime(rdr["Addtime"]);
            model.TureUser = rdr["TureUser"].ToString();
            rdr.Close();
            rdr.Dispose();
            return model;
        }
    }
}


