namespace ZoomLa.BLL
{
    using SQLDAL.SQL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Data.SqlClient;
    using System.Security.Cryptography;
    using System.Text;
    using ZoomLa.Common;
    using ZoomLa.Model;
    using ZoomLa.SQLDAL;
    public class B_Payment
    {
        public string TbName, PK;
        private M_Payment initMod = new M_Payment();
        public B_Payment()
        {
            TbName = initMod.TbName;
            PK = initMod.PK;
        }
        public int Add(M_Payment model)
        {
            Check(model);
            return Sql.insertID(TbName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        ///// <summary>
        ///// 更新是否删除状态
        ///// </summary>
        //public bool DelByIDS(string ids, M_OrderList.StatusEnum status)
        //{
        //    SafeSC.CheckIDSEx(ids);
        //    string sql = "UPDATE " + TbName + " SET IsDel=" + (int)status + " WHERE PaymentID IN(" + ids + ")";
        //    SqlHelper.ExecuteSql(sql);
        //    return true;
        //}
        public bool ChangeRecycle(string ids, int isdel)
        {
            SafeSC.CheckIDSEx(ids);
            DBCenter.UpdateSQL(TbName, "IsDel=" + isdel, "PaymentID IN (" + ids + ")");
            return true;
        }
        public void RealDelByIDS(string ids)
        {
            SafeSC.CheckIDSEx(ids);
            string sql = "DELETE FROM " + TbName + " WHERE PaymentID IN (" + ids + ")";
            SqlHelper.ExecuteSql(sql);
        }
        public void ClearRecycle()
        {
            string sql = "DELETE FROM " + TbName + " WHERE [IsDel]=1";
            SqlHelper.ExecuteSql(sql);
        }
        public bool Del(int ID)
        {
            return Sql.Del(TbName, PK + "=" + ID);
        }
        /// <summary>
        /// 更新支付状态
        /// </summary>
        public bool UpdateByStatus(string ids, int status)
        {
            SafeSC.CheckIDSEx(ids);
            string sql = "UPDATE " + TbName + " SET [Status]=" + status + " WHERE PaymentID IN (" + ids + ")";
            return SqlHelper.ExecuteSql(sql);
        }
        public bool Update(M_Payment model)
        {
            Check(model);
            return DBCenter.UpdateByID(model, model.PaymentID);
        }
        /// <summary>
        /// 选定支付方式后更新平台信息
        /// </summary>
        public bool UpdatePlat(int id, M_PayPlat.Plat plat, string platInfo = "")
        {
            List<SqlParameter> sp = new List<SqlParameter>() { new SqlParameter("platInfo", platInfo) };
            DBCenter.UpdateSQL(TbName, "PayPlatID=" + (int)plat + ",PlatFormInfo=@platInfo", PK + "=" + id, sp);
            return true;
        }
        /// <summary>
        /// 更新处理状态
        /// </summary>
        public bool SetCStatus(int payid)
        {
            string sql = "Update ZL_Payment set CStatus=1 where PaymentID=" + payid;
            return SqlHelper.ExecuteSql(sql);
        }
        public M_Payment SelReturnModel(int ID)
        {
            using (SqlDataReader reader = Sql.SelReturnReader(TbName, PK, ID))
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
        /// 根据订单号获取支付单号,不开放给前端
        /// </summary>
        public M_Payment SelModelByOrderNo(string orderNo)
        {
            if (string.IsNullOrEmpty(orderNo)) { return null; }
            List<SqlParameter> sp = new List<SqlParameter>() { new SqlParameter("orderNo", orderNo) };
            using (DbDataReader reader = DBCenter.SelReturnReader(TbName, "PaymentNum LIKE @orderNo", "", sp))
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
        public M_Payment SelModelByPayNo(string payno)
        {
            payno = payno.Replace(" ", "");
            if (string.IsNullOrEmpty(payno)) { return null; }
            List<SqlParameter> sp = new List<SqlParameter>() { new SqlParameter("payno", payno) };
            using (DbDataReader reader = DBCenter.SelReturnReader(TbName, "PayNo=@payno", "", sp))
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
        /// 获取支付明细记录
        /// </summary>
        /// <returns></returns>
        public DataTable GetPay(string PaymentNum, string skeetype = "", string skey = "", string order = "id desc", string addon = "", int PayPlatID = -100)
        {
            string where = " 1=1";
            string orderby = "paymentid desc";
            List<SqlParameter> sp = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(PaymentNum)) { where += " AND PaymentNum LIKE @paynum"; sp.Add(new SqlParameter("paynum", "%" + PaymentNum + "%")); }
            if (PayPlatID != -100)
            {
                where += " AND PayPlatID = " + PayPlatID;
            }
            if (!string.IsNullOrEmpty(skeetype))
            {
                sp.Add(new SqlParameter("skey", "%" + skey + "%"));
                switch (skeetype.ToLower())
                {
                    case "paymentnum":
                        where += " AND PaymentNum LIKE @skey";
                        break;
                    case "username":
                        where += " AND UserID IN (SELECT userid FROM zl_user WHERE username LIKE @skey)";
                        break;
                    case "paytime":
                        where += " AND CONVERT(CHAR, PayTime, 20) LIKE  @skey";
                        break;
                }
            }
            if (!string.IsNullOrEmpty(addon))
            {
                switch (skeetype.ToLower())
                {
                    case "tendays"://10天内记录
                        where += " AND DATEDIFF(DAY,PayTime,'" + DateTime.Now + "')<10";
                        break;
                    case "onemonth"://一个月内记录
                        where += " AND DATEDIFF(DAY,PayTime,'" + DateTime.Now + "')<31";
                        break;
                    case "success":
                        where += " AND Status = 3";
                        break;
                    case "nosuccess":
                        where += " AND Status <> 3";
                        break;
                }
            }
            if (!string.IsNullOrEmpty(order))
            {
                switch (order.ToLower())
                {
                    case "id desc":
                    case "id asc":
                        orderby = order.ToLower().Replace("id", "paymentid");
                        break;
                    case "moneypay desc":
                    case "moneypay asc":
                    case "moneytrue desc":
                    case "moneytrue asc":
                    case "paytime desc":
                    case "paytime asc":
                        orderby = order;
                        break;
                }
            }
            return DBCenter.Sel(TbName, where, orderby, sp);
        }
        /// <summary>
        /// 用于充值信息列表页
        /// </summary>
        /// <param name="orderType">订单类型,-1全部</param>
        /// <param name="minMoney">金额必须大于</param>
        /// <param name="maxMoney">金额必须小于</param>
        /// <param name="type">搜索类型</param>
        /// <param name="skey">订单号模糊查询</param>
        /// <returns></returns>
        public PageSetting SelPage(int cpage, int psize, int uid = 0, int orderType = -1, double minMoney = 0, double maxMoney = 0, string type = "", string skey = "", int status = 0, string sysremark = "")
        {
            List<SqlParameter> sp = new List<SqlParameter>() { new SqlParameter("skey", "%" + skey + "%"), new SqlParameter("sysremark", "%" + sysremark + "%") };
            string mtable = "(SELECT A.*,B.UserName FROM (SELECT A.*,B.PayPlatName FROM " + TbName + " A LEFT JOIN ZL_PayPlat B ON A.PayPlatID=B.PayPlatID) A LEFT JOIN ZL_User B ON A.UserID=B.UserID)";
            string where = " 1=1 ";
            if (uid > 0) { where += " AND A.UserID=" + uid; }
            if (orderType > -1) { where += " AND OrderType=" + orderType; }
            if (minMoney >= 0) { where += " AND A.MoneyPay>=" + minMoney; }
            if (maxMoney > 0) { where += " AND A.MoneyPay<=" + maxMoney; }
            if (!string.IsNullOrEmpty(skey))
            {
                switch (type)
                {
                    case "1":
                        where += " AND UserName LIKE @skey";
                        break;
                    case "2":
                        where += " AND PaymentNum LIKE @skey";
                        break;
                    case "3":
                        where += " AND PayNo LIKE @skey";
                        break;
                    case "4":
                        where += " AND A.UserID = " + DataConverter.CLng(skey); ;
                        break;
                    default:
                        break;
                }
            }
            //回收站
            if (status == (int)M_OrderList.StatusEnum.Recycle) { where += " AND A.[IsDel]=1"; }
            else { where += " AND (A.[IsDel]!=1 OR A.IsDel IS NULL)"; }
            if (!string.IsNullOrEmpty(sysremark)) { where += " AND SysRemark LIKE @sysremark"; }
            //string sql = "(SELECT A.*,B.UserName FROM (SELECT A.*,B.PayPlatName FROM " + TbName + " A LEFT JOIN ZL_PayPlat B ON A.PayPlatID=B.PayPlatID) A ";
            //sql += "LEFT JOIN ZL_User B ON A.UserID=B.UserID)";
            //return SqlHelper.JoinQuery("A.*,B.OrderType", sql, "ZL_OrderInfo", "A.PaymentNum=B.OrderNo", where, "", sp);
            PageSetting setting = PageSetting.Double(cpage, psize, mtable, "ZL_OrderInfo", "A." + PK, "A.PaymentNum=B.OrderNo", where, "", sp, "A.*,B.OrderType");
            DBCenter.SelPage(setting);
            return setting;
        }
        public string CreatePayNo()
        {
            return "PD" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + function.GetRandomString(4, 2);
        }
        public M_Payment CreateByOrder(M_OrderList orderMod)
        {
            var list = new List<M_OrderList>();
            list.Add(orderMod);
            return CreateByOrder(list);
        }
        /// <summary>
        /// 通过订单创建支付单
        /// </summary>
        public M_Payment CreateByOrder(List<M_OrderList> list)
        {
            if (list.Count < 1) { throw new Exception("未指定需要生成的订单"); }
            M_Payment payMod = new M_Payment();
            payMod.PayNo = CreatePayNo();
            foreach (M_OrderList model in list)
            {
                payMod.PaymentNum += model.OrderNo + ",";
                payMod.MoneyPay += model.Ordersamount;
            }
            M_OrderList first = list[0];
            payMod.PaymentNum = payMod.PaymentNum.TrimEnd(',');
            payMod.UserID = first.Userid;
            payMod.Status = 1;
            return payMod;
        }
        private void Check(M_Payment model)
        {
            //?在管理员与所属用户不同密码的情况下,可能会获取不到信息导致报错(因为Cookies写入的关系)
            //允许为0,考虑到有优惠卷的情况
            if (model.MoneyPay < 0) { throw new Exception("支付单金额不正确"); }
            if (model.UserID < 1) { throw new Exception("支付单未绑定用户"); }
            if (string.IsNullOrEmpty(model.PaymentNum)) { throw new Exception("支付单未绑定订单"); }
            if (string.IsNullOrEmpty(model.PayNo)) { throw new Exception("未生成支付单号"); }
        }
    }
}