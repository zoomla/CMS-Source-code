namespace ZoomLa.BLL
{
    using System;
    using System.Data;
    using ZoomLa.Model;
    using ZoomLa.Common;
    using ZoomLa.SQLDAL;
    using System.Web;
    using System.Security.Cryptography;
    using System.Text;
    using System.Data.SqlClient;
    using System.Collections.Generic;
    /// <summary>
    /// 支付信息逻辑层
    /// </summary>
    public class B_Payment
    {
        public string TbName, PK;
        public M_Payment initMod = new M_Payment();
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
        public void Del_Payment(DateTime whereSql)
        {
            string cmd = "delete from ZL_Payment where PayTime <'" + whereSql + "'";
            SqlParameter[] para = new SqlParameter[]{
            new SqlParameter("@whereSql",SqlDbType.DateTime)
            };
            para[0].Value = whereSql;
            SqlHelper.ExecuteNonQuery(CommandType.Text, cmd, para);
        }
        public bool DelByIDS(string ids, M_OrderList.StatusEnum status)
        {
            SafeSC.CheckIDSEx(ids);
            string sql = "UPDATE " + TbName + " SET IsDel=" + (int)status + " WHERE PaymentID IN(" + ids + ")";
            SqlHelper.ExecuteSql(sql);
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
            string sql = "DELETE FROM " + TbName + " WHERE [IsDel]=" + (int)M_OrderList.StatusEnum.Recycle;
            SqlHelper.ExecuteSql(sql);
        }
        public bool UpdateByStatus(string ids, int status)
        {
            SafeSC.CheckIDSEx(ids);
            string sql = "UPDATE " + TbName + " SET [Status]=" + status + " WHERE PaymentID IN (" + ids + ")";
            return SqlHelper.ExecuteSql(sql);
        }
        public bool Update(M_Payment model)
        {
            Check(model);
            return Sql.UpdateByIDs(TbName, PK, model.PaymentID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }
        public bool Del(int ID)
        {
            return Sql.Del(TbName, PK + "=" + ID);
        }
        public bool SetCStatus(int payid)
        {
            string sql = "Update ZL_Payment set CStatus=1 where PaymentID=" + payid;
            return SqlHelper.ExecuteSql(sql);
        }
        public M_Payment GetPament(int ID)
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
        public M_Payment GetPamentByID(int paymentid)
        {
            return GetPament(paymentid);
        }
        public DataTable GetUserPayment(int UserID)
        {
            string strSql = "select a.*,b.PayPlatName from ZL_Payment a left outer join ZL_PayPlat b on a.PayPlatID=b.PayPlatID where a.UserID=@UserID order by a.Status asc,a.PaymentID desc";
            SqlParameter[] sp = new SqlParameter[] {
                new SqlParameter("@UserID",SqlDbType.Int)
            };
            sp[0].Value = UserID;
            return SqlHelper.ExecuteTable(CommandType.Text, strSql, sp);
        }
        /// <summary>
        /// <summary>
        /// 根据登录ID获取所有支付信息列表
        /// </summary>
        public DataTable GetPayListID(int id)
        {
            string sql = "select * from ZL_Payment where paymentID in(select payplatID from ZL_PayPlat where UID=" + id + ")";
            return SqlHelper.ExecuteTable(CommandType.Text, sql, null);
        }
        /// <summary>
        /// 获取所有交易成功的支付信息
        /// </summary>
        public DataTable GetAllSucc()
        {
            string sql = "select a.*,b.PayPlatName from " + TbName + " a left outer join ZL_PayPlat b on a.PayPlatID=b.PayPlatID where a.Status=3 order by a.Status asc,a.PaymentID desc";
            return SqlHelper.ExecuteTable(CommandType.Text, sql);
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
        /// 获取所有交易尚未成功的支付信息
        /// </summary>
        public DataTable GetPayNoSucc()
        {
            string sql = "select a.*,b.PayPlatName from " + TbName + " left outer join ZL_PayPlat b on a.PayPlatID=b.PayPlatID where a.Status<3 order by a.Status asc,a.PaymentID desc";
            return SqlHelper.ExecuteTable(CommandType.Text, sql);
        }
        public DataTable GetPayPlatSucc(int PlatID)
        {
            string sql = "select a.*,b.PayPlatName FROM " + TbName + " a left outer join ZL_PayPlat b on a.PayPlatID=b.PayPlatID where a.PayPlatID=" + PlatID + " and a.Status=3 order by a.Status asc,a.PaymentID desc";
            return SqlHelper.ExecuteTable(CommandType.Text, sql);
        }
        public DataTable GetPayPlatUnSucc(int PlatID)
        {
            string sql = "select a.*,b.PayPlatName from " + TbName + " a left outer join ZL_PayPlat b on a.PayPlatID=b.PayPlatID where a.PayPlatID=" + PlatID + " and a.Status<>3 order by a.Status asc,a.PaymentID desc";
            return SqlHelper.ExecuteTable(CommandType.Text, sql);
        }
        public string GetApplicationName()
        {
            return GetApplicationName(HttpContext.Current);
        }
        public string GetApplicationName(HttpContext context)
        {
            if (context == null)
            {
                return string.Empty;
            }
            string host = context.Request.Url.Host;
            string applicationPath = context.Request.ApplicationPath;
            return (host + applicationPath);
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
        public DataTable GetPayList(int uid = 0, int orderType = -1, double minMoney = 0, double maxMoney = 0, string type = "", string skey = "", int status = 0, string sysremark = "")
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("skey", "%" + skey + "%"), new SqlParameter("sysremark", "%" + sysremark + "%") };
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
                    default:
                        break;
                }
            }
            //回收站
            if (status == (int)M_OrderList.StatusEnum.Recycle) { where += " AND A.[IsDel]=" + (int)M_OrderList.StatusEnum.Recycle; }
            else { where += " AND (A.[IsDel]!=" + (int)M_OrderList.StatusEnum.Recycle + " OR A.IsDel IS NULL)"; }
            if (!string.IsNullOrEmpty(sysremark)) { where += " AND SysRemark LIKE @sysremark"; }
            string sql = "(SELECT A.*,B.UserName FROM (SELECT A.*,B.PayPlatName FROM " + TbName + " A LEFT JOIN ZL_PayPlat B ON A.PayPlatID=B.PayPlatID) A ";
            sql += "LEFT JOIN ZL_User B ON A.UserID=B.UserID)";
            return SqlHelper.JoinQuery("A.*,B.OrderType", sql, "ZL_OrderInfo", "A.PaymentNum=B.OrderNo", where, "", sp);
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
        public M_Payment SelModelByPayNo(string payno)
        {
            payno = payno.Replace(" ", "");
            if (string.IsNullOrEmpty(payno)) { return null; }
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("payno", payno) };
            using (SqlDataReader reader = Sql.SelReturnReader(TbName, " Where PayNo=@payno", sp))
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
        #region ALIPAY
        /// <summary>
        /// 支付宝算法
        /// </summary>
        public string GetMD5(string s, string _input_charset)
        {
            /// <summary>
            /// 与ASP兼容的MD5加密算法
            /// </summary>
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] t = md5.ComputeHash(Encoding.GetEncoding(_input_charset).GetBytes(s));
            StringBuilder sb = new StringBuilder(32);
            for (int i = 0; i < t.Length; i++)
            {
                sb.Append(t[i].ToString("x").PadLeft(2, '0'));
            }
            return sb.ToString();
        }
        /// <summary>
        /// 支付宝冒泡排序
        /// </summary>
        public static string[] BubbleSort(string[] r)
        {
            /// <summary>
            /// 冒泡排序法
            /// </summary>
            int i, j; //交换标志 
            string temp;
            bool exchange;
            for (i = 0; i < r.Length; i++) //最多做R.Length-1趟排序 
            {
                exchange = false; //本趟排序开始前，交换标志应为假
                for (j = r.Length - 2; j >= i; j--)
                {
                    if (System.String.CompareOrdinal(r[j + 1], r[j]) < 0)　//交换条件
                    {
                        temp = r[j + 1];
                        r[j + 1] = r[j];
                        r[j] = temp;
                        exchange = true; //发生了交换，故将交换标志置为真 
                    }
                }
                if (!exchange) //本趟排序未发生交换，提前终止算法 
                {
                    break;
                }
            }
            return r;
        }
        public string CreatUrl(
            string gateway,
            string service,
            string partner,
            string sign_type,
            string out_trade_no,
            string subject,
            string body,
            string payment_type,
            string total_fee,
            string show_url,
            string seller_email,
            string key,
            string return_url,
            string _input_charset,
            string notify_url
            )
        {
            /// <summary>
            /// created by sunzhizhi 2006.5.21,sunzhizhi@msn.com。
            /// </summary>
            int i;
            //构造数组；
            string[] Oristr ={
                "service="+service,
                "partner=" + partner,
                "subject=" + subject,
                "body=" + body,
                "out_trade_no=" + out_trade_no,
                "total_fee=" + total_fee,
                "show_url=" + show_url,
                "payment_type=" + payment_type,
                "seller_email=" + seller_email,
                "notify_url=" + notify_url,
                "_input_charset="+_input_charset,
                "return_url=" + return_url
                };
            //进行排序；
            string[] Sortedstr = BubbleSort(Oristr);
            //构造待md5摘要字符串 ；
            StringBuilder prestr = new StringBuilder();
            for (i = 0; i < Sortedstr.Length; i++)
            {
                if (i == Sortedstr.Length - 1)
                {
                    prestr.Append(Sortedstr[i]);
                }
                else
                {
                    prestr.Append(Sortedstr[i] + "&");
                }
            }
            prestr.Append(key);
            string sign = GetMD5(prestr.ToString(), _input_charset);
            return sign;
        }
        #endregion
    }
}