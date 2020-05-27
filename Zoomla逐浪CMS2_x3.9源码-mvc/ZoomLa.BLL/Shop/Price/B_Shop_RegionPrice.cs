using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ZoomLa.Model;
using ZoomLa.Model.Shop;
using ZoomLa.SQLDAL;

namespace ZoomLa.BLL.Shop
{
    public class B_Shop_RegionPrice
    {
        private M_Shop_RegionPrice initMod = new M_Shop_RegionPrice();
        private string TbName, PK; 
        public B_Shop_RegionPrice() 
        {
            TbName = initMod.TbName;
            PK = initMod.PK;
        }
        public bool Del(int ID)
        {
            return DBCenter.Del(TbName, PK, ID);
        }
        public int Insert(M_Shop_RegionPrice model)
        {
            return DBCenter.Insert(model);
        }
        public DataTable Sel()
        {
            return DBCenter.Sel(TbName);
        }
        public M_Shop_RegionPrice SelReturnModel(int ID)
        {
            using (DbDataReader reader = DBCenter.SelReturnReader(TbName, PK, ID))
            {
                if (reader.Read())
                {
                    return initMod.GetModelFromReader(reader);
                }
                else
                {
                    return null;
                }
            }
        }
        /// <summary>
        /// 支持传入Guid或ProID的方式获取
        /// </summary>
        public M_Shop_RegionPrice SelModelByGuid(string guid)
        {
            if (string.IsNullOrEmpty(guid)) { return null; }
            string where = "Guid=@guid ";
            if (DataConvert.CLng(guid) > 0) { where += " OR ProID=" + guid; }
            List<SqlParameter> sp = new List<SqlParameter>() { new SqlParameter("guid", guid) };
            using (DbDataReader reader = DBCenter.SelReturnReader(TbName, where, "", sp))
            {
                if (reader.Read())
                {
                    return initMod.GetModelFromReader(reader);
                }
                else
                {
                    return null;
                }
            }
        }
        public M_Shop_RegionPrice SelModelByProID(int proid) 
        {
            return SelModelByGuid(proid.ToString());
        }
        public bool UpdateByID(M_Shop_RegionPrice model)
        {
            return DBCenter.UpdateByID(model, model.ID);
        }
        /// <summary>
        /// 根据商品信息,获取其区域价格
        /// 先匹配县,如无,再匹配-->市-->省数据
        /// </summary>
        /// <param name="region">省|市|县</param>
        public double GetRegionPrice(int proID, double linPrice, string region, int gid)
        {
            if (string.IsNullOrWhiteSpace(region) || region.Equals("none") || gid < 1) { return linPrice; }
            string[] arr = region.Replace(" ", "").Split('|');
            string province = arr[0];
            string city = arr.Length > 1 ? arr[1] : "";
            string county = arr.Length > 2 ? arr[2] : "";
            M_Shop_RegionPrice regionMod = SelModelByProID(proID);
            if (regionMod == null || string.IsNullOrEmpty(regionMod.Info)) { return linPrice; }
            //---------------------------------------------
            List<M_RegionPrice_Price> infoList = JsonConvert.DeserializeObject<List<M_RegionPrice_Price>>(regionMod.Info);
            M_RegionPrice_Price priceMod = infoList.FirstOrDefault(p => p.region.Equals(province + city + county));
            if (priceMod == null) { priceMod = infoList.FirstOrDefault(p => p.region.Equals(province + city)); }
            if (priceMod == null) { priceMod = infoList.FirstOrDefault(p => p.region.Equals(province)); }
            if (priceMod == null) { return linPrice; }
            else { return GetPrice(priceMod.price, linPrice, gid); }
        }
        /// <summary>
        /// 从已筛选出的地区中,获取售价
        /// </summary>
        /// <param name="priceDT">售价表</param>
        /// <param name="linPrice">默认售价(为0时使用其)</param>
        /// <param name="gid">组ID</param>
        /// <returns></returns>
        public double GetPrice(DataTable priceDT, double linPrice, int gid)
        {
            if (priceDT == null || priceDT.Rows.Count < 1 || gid < 1) { return linPrice; }
            DataRow[] drs = priceDT.Select("gid='" + gid + "'");
            //不存在,或售价等于0则按原价
            if (drs.Length < 1 || DataConvert.CDouble(drs[0]["Price"]) <= 0) { return linPrice; }
            else { return DataConvert.CDouble(drs[0]["Price"]); }
        }
        public void P_Remove(M_Shop_RegionPrice model, string region)
        {
            if (string.IsNullOrEmpty(model.Info) || string.IsNullOrWhiteSpace(region)) { return; }
            region = region.Trim();
            List<M_RegionPrice_Price> infoList = JsonConvert.DeserializeObject<List<M_RegionPrice_Price>>(model.Info);//原有数据
            M_RegionPrice_Price priceMod = infoList.FirstOrDefault(p => p.region.Equals(region));
            if (priceMod != null) { infoList.Remove(priceMod);}
            model.Info = JsonConvert.SerializeObject(infoList);
        }
    }
}
