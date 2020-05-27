using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
 * 酒店,旅游,机票等需要提交信息的订单模型
 */ 
namespace ZoomLa.Model
{
    public abstract class M_Cart_Base 
    {
        public int UserID { get; set; }
        //商品信息(ID,数量,备注)
        public List<ProModel> ProList = new List<ProModel>();
        //入住人信息
        public List<M_Cart_Contract> Guest = new List<M_Cart_Contract>();
        //联系人信息
        public List<M_Cart_Contract> Contract = new List<M_Cart_Contract>();
        public DateTime CDate = DateTime.Now;
    }
    //酒店
    public class M_Cart_Hotel:M_Cart_Base
    {
        //酒店名
        public string HotelName { get; set; }
        //入住人数
        public int PeopleNum { get; set; }
        //所在城市
        public string CityName { get; set; }
        public string GoDate { get; set; }
        public string OutDate { get; set; }
    }
    //旅游
    public class M_Cart_Travel:M_Cart_Base
    {
        public int PeopleNum { get; set; }
    }
    //联系人模型
    public class M_Cart_Contract
    {
        public M_Cart_Contract(string n, string m, string e, string a)
        {
            Name = n; Mobile = m; Email = e; Address = a;
        }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        //证件类型
        public string CertType { get; set; }
        //证件号码
        public string CertCode { get; set; }
    }
    //购物车商品购买模型
    public class ProModel
    {
        //0:自营商城
        public int StoreID { get; set; }
        public int ProID { get; set; }
        //SKU
        public string SkuID { get; set; }
        public string ProName { get; set; }
        public int Pronum { get; set; }
        /// <summary>
        /// 价格编号,用于支持多价格
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// 总金额,数量*价格,后期逻辑运算得出
        /// </summary>
        public double AllMoney { get; set; }
        /// <summary>
        /// 旅游,出发日期或酒店入住日期
        /// </summary>
        public DateTime GoDate = DateTime.Now;
        //酒店留开日期
        public DateTime OutDate = DateTime.Now;
        //备注
        public string Remind { get; set; }
    }
}
