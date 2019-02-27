using System;
using System.Data.SqlClient;
using System.Data;
namespace ZoomLa.Model
{
    public class M_CartPro:M_Base
    {
        public string ProSeller { get; set; }
        /// <summary>
        /// 类型 0为普通 1:酒店订单 2:航班订单 3:域名订单
        /// </summary>
        public int type { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        public string PerID { get; set; }
        /// <summary>
        /// 城市
        /// </summary>
        public string city { get; set; }
        public int ID { get; set; }
        public int Orderlistid { get; set; }
        public int CartID { get; set; }
        public int ProID { get; set; }
        public int Pronum { get; set; }
        public string Username { get; set; }
        public string Proname { get; set; }
        public string Danwei { get; set; }
        public double Shijia { get; set; }
        public double AllMoney { get; set; }
        private DateTime _addTime;
        public DateTime Addtime
        {
            get { return _addTime.Year == 1 ? DateTime.Now : _addTime; }
            set { _addTime = value; }
        }
        public string Bindpro { get; set; }
        public string FarePrice { get; set; }
        /// <summary>
        /// 商品定制信息
        /// </summary>
        public string Attribute { get; set; }
        private DateTime _endTime { get; set; }
        public DateTime EndTime
        {
            get { return _endTime.Year == 1 ? DateTime.Now : _endTime; }
            set { _endTime = value; }
        }
        /// <summary>
        /// 附加信息,存Json
        /// </summary>
        public string Additional { get; set; }
        /// <summary>
        /// 附加状态,是否已返修,退货,评价 repair,return,comment
        /// </summary>
        public string AddStatus { get; set; }
        /// <summary>
        /// 店铺ID
        /// </summary>
        public int StoreID { get; set; }
        /// <summary>
        /// 附加虚拟币信息
        /// </summary>
        public string AllMoney_Json { get; set; }
        /// <summary>
        /// 是否已被晒单等
        /// </summary>
        public string cartStatus { get; set; }
        /// <summary>
        /// 多价格编号,或IDC期限,用于标识商品价格
        /// </summary>
        public string code { get; set; }

        public M_CartPro() 
        {
            this.ProSeller = string.Empty;
            this.PerID = string.Empty;
            this.city = string.Empty;
            this.Username = string.Empty;
            this.Proname = string.Empty;
            this.Danwei = string.Empty;
            this.Bindpro = string.Empty;
            this.FarePrice = string.Empty;
        }
        public override string TbName { get { return "ZL_CartPro"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"ID","Int","4"},
                                  {"Orderlistid","Int","4"},
                                  {"CartID","Int","4"},
                                  {"ProID","Int","4"}, 
                                  {"Pronum","Int","4"},
                                  {"Username","NVarChar","255"},
                                  {"Proname","NVarChar","255"},
                                  {"Danwei","NVarChar","50"},
                                  {"Shijia","Money","8"}, 
                                  {"AllMoney","Money","8"},
                                  {"Addtime","DateTime","8"}, 
                                  {"Bindpro","NVarChar","50"},
                                  {"ProSeller","NVarChar","255"},
                                  {"PerID","NVarChar","50"},
                                  {"city","NVarChar","50"},
                                  {"type","Int","4"}, 
                                  {"Attribute","NVarChar","1000"},
                                  {"FarePrice","NVarChar","400"},
                                  {"EndTime","DateTime","8"},
                                  {"Additional","NText","4000"},
                                  {"StoreID","Int","4"},
                                  {"AllMoney_Json","NVarChar","500"},
                                  {"AddStatus","NVarChar","1000"},
                                  {"code","VarChar","500"}
                                 };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_CartPro model = this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.Orderlistid;
            sp[2].Value = model.CartID;
            sp[3].Value = model.ProID;
            sp[4].Value = model.Pronum;
            sp[5].Value = model.Username;
            sp[6].Value = model.Proname;
            sp[7].Value = model.Danwei;
            sp[8].Value = model.Shijia;
            sp[9].Value = model.AllMoney;
            sp[10].Value = model.Addtime;
            sp[11].Value = model.Bindpro;
            sp[12].Value = model.ProSeller;
            sp[13].Value = model.PerID;
            sp[14].Value = model.city;
            sp[15].Value = model.type;
            sp[16].Value = model.Attribute;
            sp[17].Value = model.FarePrice;
            sp[18].Value = model.EndTime;
            sp[19].Value = model.Additional;
            sp[20].Value = model.StoreID;
            sp[21].Value = model.AllMoney_Json;
            sp[22].Value = model.AddStatus;
            sp[23].Value = model.code;
            return sp;
        }
        public M_CartPro GetModelFromReader(DataRow rdr)
        {
            M_CartPro model = new M_CartPro();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.Orderlistid = ConvertToInt(rdr["Orderlistid"]);
            model.CartID = Convert.ToInt32(rdr["CartID"]);
            model.ProID = Convert.ToInt32(rdr["ProID"]);
            model.Pronum = ConvertToInt(rdr["Pronum"]);
            model.Username = ConverToStr(rdr["Username"]);
            model.Proname = ConverToStr(rdr["Proname"]);
            model.Danwei = ConverToStr(rdr["Danwei"]);
            model.Shijia = ConverToDouble(rdr["Shijia"]);
            model.AllMoney = ConverToDouble(rdr["AllMoney"]);
            model.Addtime = Convert.ToDateTime(rdr["Addtime"]);
            model.Bindpro = ConverToStr(rdr["Bindpro"]);
            model.ProSeller = ConverToStr(rdr["ProSeller"]);
            model.PerID = ConverToStr(rdr["PerID"]);
            model.city = ConverToStr(rdr["city"]);
            model.type = ConvertToInt(rdr["type"]);
            model.Attribute = ConverToStr(rdr["Attribute"]);
            model.FarePrice = ConverToStr(rdr["FarePrice"]);
            model.EndTime = ConvertToDate(rdr["EndTime"]);
            model.Additional = ConverToStr(rdr["Additional"]);
            model.StoreID = ConvertToInt(rdr["StoreID"]);
            model.AllMoney_Json = ConverToStr(rdr["AllMoney_Json"]);
            model.AddStatus = ConverToStr(rdr["AddStatus"]);
            model.code = ConverToStr(rdr["code"]);
            return model;
        }
        public M_CartPro GetModelFromReader(SqlDataReader rdr)
        {
            M_CartPro model = new M_CartPro();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.Orderlistid = Convert.ToInt32(rdr["Orderlistid"]);
            model.CartID = Convert.ToInt32(rdr["CartID"]);
            model.ProID = Convert.ToInt32(rdr["ProID"]);
            model.Pronum = Convert.ToInt32(rdr["Pronum"]);
            model.Username = rdr["Username"].ToString();
            model.Proname = ConverToStr(rdr["Proname"]);
            model.Danwei = ConverToStr(rdr["Danwei"]);
            model.Shijia = Convert.ToDouble(rdr["Shijia"]);
            model.AllMoney = Convert.ToDouble(rdr["AllMoney"]);
            model.Addtime = Convert.ToDateTime(rdr["Addtime"]);
            model.Bindpro = ConverToStr(rdr["Bindpro"]);
            model.ProSeller = ConverToStr(rdr["ProSeller"]);
            model.PerID = ConverToStr(rdr["PerID"]);
            model.city = ConverToStr(rdr["city"]);
            model.type = ConvertToInt(rdr["type"]);
            model.Attribute = rdr["Attribute"].ToString();
            model.FarePrice = rdr["FarePrice"].ToString();
            model.EndTime = ConvertToDate(rdr["EndTime"]);
            model.Additional = ConverToStr(rdr["Additional"]);
            model.StoreID = ConvertToInt(rdr["StoreID"]);
            model.AllMoney_Json = ConverToStr(rdr["AllMoney_Json"]);
            model.AddStatus = ConverToStr(rdr["AddStatus"]);
            model.code = ConverToStr(rdr["code"]);
            rdr.Dispose();
            return model;
        }
    }
}