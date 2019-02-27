using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Model.Shop;
using ZoomLa.SQLDAL;
using ZoomLa.SQLDAL.SQL;

namespace ZoomLa.BLL.Shop
{
    public class B_Order_IDC : ZL_Bll_InterFace<M_Order_IDC>
    {
        private M_Order_IDC initMod = new M_Order_IDC();
        private string TbName, PK;
        public B_Order_IDC()
        {
            TbName = initMod.TbName;
            PK = initMod.PK;
        }
        public bool Del(int ID)
        {
            return DBCenter.Del(TbName, PK, ID);
        }
        public int Insert(M_Order_IDC model)
        {
            return DBCenter.Insert(model);
        }
        public DataTable Sel()
        {
            return DBCenter.Sel(TbName);
        }
        public PageSetting SelPage(int cpage, int psize)
        {
            PageSetting setting = PageSetting.Single(cpage, psize, TbName, PK, "");
            DBCenter.SelPage(setting);
            return setting;
        }
        public M_Order_IDC SelModelByOrderNo(string orderno)
        {
            List<SqlParameter> sp = new List<SqlParameter>() { new SqlParameter("OrderNo", orderno) };
            using (DbDataReader reader = DBCenter.SelReturnReader(TbName, "OrderNo=@OrderNo", "", sp))
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
        public M_Order_IDC SelReturnModel(int ID)
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
        public bool UpdateByID(M_Order_IDC model)
        {
            return DBCenter.UpdateByID(model, model.ID);
        }
        public bool DelByIDS(string ids)
        {
            SafeSC.CheckIDSEx(ids);
            return DBCenter.DelByIDS(TbName, PK, ids);
        }
        /// <summary>
        /// 更新到期时间,仅用于初次购买
        /// </summary>
        /// <param name="oid"></param>
        public void UpdateEndTimeByNo(string orderNo)
        {
            M_Order_IDC idcMod = SelModelByOrderNo(orderNo);
            JObject timeMod = JsonConvert.DeserializeObject<JObject>(idcMod.ProInfo);
            idcMod.ETime = GetEndTime(idcMod.ETime, timeMod["time"].ToString());
            UpdateByID(idcMod);
        }
        /// <summary>
        /// 订单续费,延长使用时间
        /// </summary>
        public void RennewTime(M_OrderList mod)
        {
            M_Order_IDC idcMod = SelReturnModel(mod.Promoter);
            JObject timeMod = JsonConvert.DeserializeObject<JObject>(mod.Ordermessage);
            idcMod.ETime = GetEndTime(idcMod.ETime, timeMod["time"].ToString());
            UpdateByID(idcMod);
        }
        //-------------Tools
        /// <summary>
        /// 转换为table后,筛选有效的价格>0且期限不为空
        /// </summary>
        public DataTable P_GetValid(string info)
        {
            if (string.IsNullOrEmpty(info)) { return null; }
            DataTable dt = JsonConvert.DeserializeObject<DataTable>(info);
            dt.Columns.Add("enable", typeof(int));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                if (!TimeIsValid(DataConvert.CStr(dr["time"]))) { }
                else if (DataConvert.CLng(dr["price"]) <= 0) { }
                else { dr["enable"] = 1; }
            }
            dt.DefaultView.RowFilter = "enable='1'";
            return dt.DefaultView.ToTable();
        }
        /// <summary>
        /// 将数据转化为json,用于存入订单字段
        /// </summary>
        public string ToProInfoStr(DataRow dr)
        {
            JObject jobj = new JObject();
            foreach (DataColumn col in dr.Table.Columns)
            {
                jobj.Add(col.ColumnName, dr[col.ColumnName].ToString());
            }
            return JsonConvert.SerializeObject(jobj);
        }
        /// <summary>
        /// IDC专用,根据到时间计算新到期日
        /// </summary>
        public DateTime GetEndTime(DateTime stime, string time)
        {
            DateTime etime = stime;
            switch (time.Replace(" ", ""))
            {
                case "半年":
                    etime = stime.AddMonths(6);
                    break;
                case "季度":
                    etime = stime.AddMonths(3);
                    break;
                case "无限期":
                    etime = DateTime.MaxValue;
                    break;
                default:
                    if (time.Contains("天") || DataConvert.CLng(time) > 0)
                    {
                        int days = DataConvert.CLng(time.Replace("天", ""));
                        etime = stime.AddDays(days);
                    }
                    else if (time.Contains("月"))
                    {
                        int months = DataConvert.CLng(time.Replace("月", ""));
                        etime = stime.AddMonths(months);
                    }
                    else if (time.Contains("年"))
                    {
                        int years = DataConvert.CLng(time.Replace("年", ""));
                        etime = stime.AddYears(years);
                    }
                    break;
            }
            return etime;
        }
        /// <summary>
        /// 返回所选定的价格方案
        /// </summary>
        public DataRow GetSelTime(M_Product proMod, string time)
        {
            DataTable timedt = JsonConvert.DeserializeObject<DataTable>(proMod.IDCPrice);
            timedt.DefaultView.RowFilter = "time='" + time + "'";
            timedt = timedt.DefaultView.ToTable();
            if (timedt.Rows.Count < 1) { function.WriteErrMsg("IDC商品期限错误[" + time + "]"); }
            DataRow timeMod = timedt.Rows[0];
            return timeMod;
        }
        //定义的周期是否有效
        public bool TimeIsValid(string time)
        {
            DateTime stime = DateTime.Now;
            if (string.IsNullOrEmpty(time)) { return false; }
            else if (GetEndTime(stime, time) == stime) { return false; }
            else { return true; }
        }
    }
}
