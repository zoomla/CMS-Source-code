namespace ZoomLa.BLL
{
    using System;
    using System.Data;
    using ZoomLa.Model;
    using ZoomLa.Common;
    using ZoomLa.SQLDAL;
    using System.Data.SqlClient;
    using SQLDAL.SQL;
    using System.Collections.Generic;
    //public class OrderSearch
    //{

    //}
    public class B_CartPro
    {
        private string TbName, PK;
        private M_CartPro initMod = new M_CartPro();
        public B_CartPro()
        {
            TbName = initMod.TbName;
            PK = initMod.PK;
        }
        public M_CartPro SelReturnModel(int ID)
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
        public M_CartPro GetSelect(int proid, int cartid)
        {
            string sqlStr = "SELECT * FROM ZL_CartPro WHERE ProID=" + proid + " AND CartID=" + cartid;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, sqlStr, null))
            {
                if (reader.Read())
                {
                    return initMod.GetModelFromReader(reader);
                }
                else
                    return new M_CartPro();
            }
        }
        /// <summary>
        /// 主用于IDC订单，IDC一张订单只有一个商品，其他订单勿用
        /// </summary>
        public M_CartPro SelModByOrderID(int orderID)
        {
            string sqlstr = "Select  Top 1 * from ZL_CartPro where Orderlistid=@Orderlistid";
            SqlParameter[] para = new SqlParameter[]{
                new SqlParameter("@Orderlistid",orderID)
            };
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, sqlstr, para))
            {
                if (reader.Read())
                {
                    return initMod.GetModelFromReader(reader);
                }
                else
                    return new M_CartPro();
            }
        }
        public DataTable Sel()
        {
            return Sql.Sel(TbName);
        }
        public DataTable GetCartProOrderID(int Orderid)
        {
            string sql = "SELECT A.*,B.* FROM " + TbName + " A LEFT JOIN ZL_Commodities B ON A.Proid=B.ID WHERE A.Orderlistid=" + Orderid;
            return SqlHelper.ExecuteTable(CommandType.Text, sql, null);
        }
        public DataTable GetCartProOrderIDW(int Orderid)
        {
            string strSql = "select a.*,b.Proinfo,b.Procontent,b.proclass from ZL_CartPro a,ZL_Commodities b where a.ProID=b.ID and Orderlistid = " + Orderid + " order by(a.id) desc";
            return SqlHelper.ExecuteTable(CommandType.Text, strSql, null);
        }
        public DataTable SelByOrderID(int orderListID)
        {
            return SqlHelper.JoinQuery("A.*,B.LinPrice,B.ProCode", TbName, "ZL_Commodities", "A.ProID=B.ID", "A.OrderListID=" + orderListID);
        }
        public bool UpdateByID(M_CartPro model)
        {
            return Sql.UpdateByIDs(TbName, PK, model.ID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }
        public int GetInsert(M_CartPro model)
        {
            return Sql.insertID(TbName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        public bool Add(M_CartPro model)
        {
            Sql.insertID(TbName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
            return true;
        }
        public bool Del(int ID)
        {
            return Sql.Del(TbName, ID);
        }
        //--------------------------Tools
        /// <summary>
        /// True表示库存大于购买量,False则有库存Stock小于购买量的商品
        /// </summary>
        public bool CheckStock(int orderListID)
        {
            string sql = "Select * From " + TbName + " as a Left Join ZL_Commodities as b on a.ProID=b.ID Where a.ProNum>b.Stock And a.OrderListID=" + orderListID;
            return SqlHelper.ExecuteTable(CommandType.Text, sql).Rows.Count < 1;
        }
        /// <summary>
        /// 拷贝一份至ZL_CartPro长久保存
        /// </summary>
        public void CopyToCartPro(M_UserInfo mu, DataTable dt, int oid)
        {
            B_Product proBll = new B_Product();
            string[] fields = "Additional,StoreID,AllMoney_Json,code".Split(',');
            foreach (string field in fields)
            {
                if (!dt.Columns.Contains(field)) { dt.Columns.Add(new DataColumn(field, typeof(string))); }
            }
            foreach (DataRow dr in dt.Rows)
            {
                M_Product proMod = proBll.GetproductByid(Convert.ToInt32(dr["Proid"]));
                M_CartPro model = new M_CartPro();
                model.Orderlistid = oid;
                model.ProID = proMod.ID;
                model.Pronum = DataConverter.CLng(dr["Pronum"]);
                model.Proname = proMod.Proname;
                model.Username = mu.UserName;
                model.Shijia = proMod.LinPrice;
                if (!dt.Columns.Contains("AllMoney")) { model.AllMoney = proMod.LinPrice * model.Pronum; }
                else { model.AllMoney = Convert.ToDouble(dr["AllMoney"]); }
                model.Danwei = proMod.ProUnit;
                model.Addtime = DateTime.Now;
                model.Additional = DataConvert.CStr(dr["Additional"]);
                model.StoreID = DataConvert.CLng(dr["StoreID"]);
                model.AllMoney_Json = DataConvert.CStr(dr["AllMoney_Json"]);
                model.code = DataConvert.CStr(dr["code"]);
                model.Attribute = DataConvert.CStr(dr["ProAttr"]);
                int id = GetInsert(model);
                //用于支持购物车扩展字段
                //DataTable cartFieldDT = fieldBll.Select_Type(1);
                //if (cartFieldDT.Rows.Count > 1)
                //{
                //    string sql = "Update ZL_CartPro Set ", fieldstr = "";
                //    SqlParameter[] sp = new SqlParameter[cartFieldDT.Rows.Count];
                //    for (int i = 0; i < cartFieldDT.Rows.Count; i++)
                //    {
                //        DataRow fdr = cartFieldDT.Rows[i];
                //        string field = fdr["FieldName"].ToString();
                //        string vname = "@val" + i;
                //        string value = dr[field].ToString();
                //        if (string.IsNullOrEmpty(value)) continue;
                //        sp[i] = new SqlParameter(vname, value);
                //        fieldstr += field + "=" + vname + ",";
                //    }
                //    fieldstr = fieldstr.TrimEnd(',');
                //    sql = sql + fieldstr + " Where ID=" + id;
                //    if (!string.IsNullOrEmpty(fieldstr))
                //        SqlHelper.ExecuteSql(sql, sp);
                //}
            }
        }
        //---------------------------User
        /// <summary>
        /// 专用于展示,带图片等信息
        /// </summary>
        ///<filter>需要过滤参数</filter>
        public DataTable SelForRPT(int oid, string filter)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("filter", filter) };
            string fields = " A.*,B.LinPrice,B.Proname,B.Thumbnails,B.ProClass,B.ProUnit,B.ParentID";
            string where = " A.OrderListID=" + oid;
            if (!string.IsNullOrEmpty(filter))
            {
                where += " AND (CHARINDEX(@filter,A.AddStatus)=0 OR A.AddStatus IS NULL)";
            }
            //if (!string.IsNullOrEmpty(ids))
            //{
            //    SafeSC.CheckIDSEx(ids);
            //    where += "A.ID IN(" + ids + ") ";
            //}
            return SqlHelper.JoinQuery(fields, TbName, new M_Product().TbName, "A.ProID=B.ID", where, "", sp);
        }
        /// <summary>
        /// 用于用户中心OrderList
        /// </summary>
        /// <param name="ids">订单的IDS</param>
        public DataTable U_SelForOrderList(string ids)
        {
            return U_SelForOrderList(-100, "", "", -1, ids);
        }
        /// <summary>
        /// 仅用于用户中心OrderList
        /// </summary>
        public DataTable U_SelForOrderList(int uid, string filter, string Skey = "", int ordertype = -1, string ids = "")
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("Skey", "%" + Skey + "%"), new SqlParameter("Code", Skey) };
            //商品信息
            string mtbname = "(SELECT A.*,B.LinPrice,B.Thumbnails,B.GuessXML,B.ProCode FROM " + TbName + " A LEFT JOIN " + new M_Product().TbName + " B ON A.ProID=B.ID)";
            //订单与用户信息
            string stbname = "(SELECT A.*,B.ParentUserID FROM ZL_OrderInfo A LEFT JOIN ZL_User B ON A.UserID=B.UserID)";
            string where = "1=1 ";
            if (uid != -100) { where += " AND B.UserID=" + uid; }
            string order = "A.ID DESC";
            string fields = "A.ID AS CartID,A.ProID,A.ProName,A.Pronum,A.AddStatus,A.AllMoney,A.StoreID,A.Thumbnails,A.GuessXML";//ZL_Cart
            fields += ",B.ID,B.OrderNo,B.AddUser,B.AddTime,B.OrderStatus,B.PaymentStatus,B.OrdersAmount,B.StateLogistics,B.Aside,B.Delivery,B.Service_charge,B.OrderType,B.OrderMessage";//ZL_OrderInfo
            fields += ",B.ParentUserID";//User
            switch (filter)
            {
                //--------用户层筛选
                case "needpay"://需付款
                    where += " AND B.Aside=0 AND B.PaymentStatus=0";
                    break;
                case "receive"://需确认收货
                    where += " AND B.Aside=0 AND B.StateLogistics=1";
                    break;
                case "comment"://已完结可评价
                    where += " AND B.Aside=0 AND B.OrderStatus>=" + (int)M_OrderList.StatusEnum.OrderFinish + " AND (CHARINDEX('comment',A.AddStatus)=0 OR A.AddStatus IS NULL) ";
                    break;
                case "recycle"://订单回收站
                    where += " AND B.Aside=1";
                    break;
                case "all"://全部(不含回收站)
                default:
                    where += " AND B.Aside=0";
                    break;
            }
            if (!string.IsNullOrEmpty(Skey))//商品名搜索,超过8位则启动编号与订单号搜索
            {
                where += " AND (A.ProName LIKE @Skey";
                if (Skey.Length > 8) { where += " OR (A.ProCode=@Code OR B.OrderNo=@Code)"; }
                where += ")";
            }
            if (!string.IsNullOrEmpty(ids)) { SafeSC.CheckIDSEx(ids); where += " AND A.OrderListID IN (" + ids + ")"; }
            if (ordertype != -1) { where += " AND OrderType = " + ordertype; }
            return DBCenter.JoinQuery(fields, mtbname, stbname, "A.OrderListID=B.ID", where, order, sp);
        }
        /// <summary>
        /// 后台使用订单筛选
        /// </summary>
        public DataTable SelForOrderList(string orderType, string filter, string proname, string orderno, string reuser, string mobile, 
            string uids,string skeyType,string skey, string stime, string etime,string expstime,string expetime)
        {
            List<SqlParameter> sp = new List<SqlParameter>();
            //商品信息
            string mtbname = "(SELECT A.*,B.LinPrice,B.Thumbnails,B.GuessXML,B.ProCode FROM " + TbName + " A LEFT JOIN " + new M_Product().TbName + " B ON A.ProID=B.ID)";
            //订单与用户信息
            string stbname = "(SELECT A.*,B.ParentUserID,B.HoneyName,ZL_Order_Exp.CDate AS ExpSTime"
                             + " FROM ZL_OrderInfo A LEFT JOIN ZL_User B ON A.UserID=B.UserID LEFT JOIN ZL_Order_Exp ON A.ExpressNum=ZL_Order_Exp.ID)";
            string where = " B.Aside=0 ";
            string order = "A.ID DESC";
            string fields = "A.ID AS CartID,A.ProID,A.ProName,A.Shijia,A.Pronum,A.AddStatus,A.AllMoney,A.StoreID,A.Thumbnails,A.GuessXML,B.Freight";//ZL_Cart
            fields += ",B.ID,B.OrderNo,B.AddUser,B.AddTime,B.OrderStatus,B.PaymentStatus,B.OrdersAmount,B.StateLogistics,B.Aside,B.Delivery,B.Service_charge";//ZL_OrderInfo
            fields += ",B.OrderType,B.OrderMessage,B.Receivablesamount,B.ExpressNum";
            fields += ",B.Shengfen,B.Jiedao,B.ZipCode,B.MobileNum,B.Phone,B.Email,B.Receiver,B.Rename,B.ExpressDelivery";//地址信息 ZL_OrderInfo
            fields += ",B.UserID,B.ParentUserID,B.HoneyName";//User
            fields += ",B.ExpSTime";
            switch (filter)//筛选
            {
                case "unexp"://待发货==已付款+未发货
                    where += " AND B.PaymentStatus=" + (int)M_OrderList.PayEnum.HasPayed + " AND B.StateLogistics=" + (int)M_OrderList.ExpEnum.NoSend;
                    break;
                case "unpaid"://待付款==状态为未付款的
                    where += " AND B.PaymentStatus=" + (int)M_OrderList.PayEnum.NoPay;
                    break;
                case "exped"://已发货==大于未发货状态的订单
                    where += " AND B.StateLogistics>" + (int)M_OrderList.ExpEnum.NoSend;
                    break;
                case "finished":
                    where += " AND B.OrderStatus=" + (int)M_OrderList.StatusEnum.OrderFinish;
                    break;
                case "unrefund":
                    where += " AND B.PaymentStatus=" + (int)M_OrderList.PayEnum.RequestRefund;
                    break;
                case "refunded":
                    where += " AND B.PaymentStatus=" + (int)M_OrderList.PayEnum.Refunded;
                    break;
                case "recycle"://订单回收站==已关闭
                    where = " B.Aside=1 ";
                    break;
                case "all"://全部(不含回收站)
                default:
                    break;
            }
            //未指定类型,则抽出常规订单
            if (string.IsNullOrEmpty(orderType)) { where += " AND B.OrderType=0"; }
            else if (orderType.Equals("-1")) { }
            else { SafeSC.CheckIDSEx(orderType); where += " AND B.Ordertype IN (" + orderType + ")"; }

            if (!string.IsNullOrEmpty(proname)) { where += " AND A.ProName LIKE @proname"; sp.Add(new SqlParameter("proname", "%" + proname + "%")); }
            if (!string.IsNullOrEmpty(orderno)) { where += " AND B.OrderNo LIKE @orderno"; sp.Add(new SqlParameter("orderno", "%" + orderno + "%")); }
            if (!string.IsNullOrEmpty(reuser)) { where += " AND B.ReUser LIKE @reuser"; sp.Add(new SqlParameter("reuser", "%" + reuser + "%")); }
            if (!string.IsNullOrEmpty(mobile)) { where += " AND B.Mobile LIKE @mobile"; sp.Add(new SqlParameter("mobile", "%" + mobile + "%")); }
            if (!string.IsNullOrEmpty(uids) && SafeSC.CheckIDS(uids)) { where += " AND UserID IN (" + uids + ")"; }
            if (!string.IsNullOrEmpty(stime))//支持下单日期筛选
            {
                DateTime result = DateTime.Now;
                if (DateTime.TryParse(stime, out result)) { where += " AND B.AddTime>=@stime"; sp.Add(new SqlParameter("stime", result.ToString("yyyy/MM/dd 00:00:00"))); }
            }
            if (!string.IsNullOrEmpty(etime))
            {
                DateTime result = DateTime.Now;
                if (DateTime.TryParse(etime, out result)) { where += " AND B.AddTime<=@etime"; sp.Add(new SqlParameter("etime", result.ToString("yyyy/MM/dd 23:59:59"))); }
            }
            if (!string.IsNullOrEmpty(expstime) || !string.IsNullOrEmpty(expetime))
            {
                where += " AND B.ExpSTime IS NOT NULL ";
            }
            if (!string.IsNullOrEmpty(expstime))//按发货日期筛选
            {
                DateTime result = DateTime.Now;
                if (DateTime.TryParse(expstime, out result)) { where += " AND B.ExpSTime>=@expstime"; sp.Add(new SqlParameter("expstime", result.ToString("yyyy/MM/dd 00:00:00"))); }
            }
            if (!string.IsNullOrEmpty(expetime))
            {
                DateTime result = DateTime.Now;
                if (DateTime.TryParse(expetime, out result)) { where += " AND B.ExpSTime<=@expetime"; sp.Add(new SqlParameter("expetime", result.ToString("yyyy/MM/dd 23:59:59"))); }
            }
            //唯一性条件搜索
            if (!string.IsNullOrEmpty(skey))
            {
                sp.Add(new SqlParameter("skey", "%" + skey + "%"));
                switch (skeyType)
                {
                    case "exp":
                        where += " AND B.ExpressDelivery LIKE @skey";
                        break;
                    case "oid":
                        where += " AND A.ID= " + DataConvert.CLng(skey);
                        break;
                }
            }
            return DBCenter.JoinQuery(fields, mtbname, stbname, "A.OrderListID=B.ID", where, order, sp.ToArray());
        }
        public DataTable SelOrderCount()
        {
            string tbname = "ZL_Orderinfo";
            string sql = "SELECT COUNT(ID) AS [all]";
            sql += ",(SELECT COUNT(ID) FROM " + tbname + " WHERE Aside=0 AND PaymentStatus=" + (int)M_OrderList.PayEnum.HasPayed + " AND StateLogistics=" + (int)M_OrderList.ExpEnum.NoSend + ") unexp";
            sql += ",(SELECT COUNT(ID) FROM " + tbname + " WHERE Aside=0 AND PaymentStatus=" + (int)M_OrderList.PayEnum.NoPay + ") unpaid";
            sql += ",(SELECT COUNT(ID) FROM " + tbname + " WHERE Aside=0 AND StateLogistics>" + (int)M_OrderList.ExpEnum.NoSend + ") exped";
            sql += ",(SELECT COUNT(ID) FROM " + tbname + " WHERE Aside=0 AND OrderStatus=" + (int)M_OrderList.StatusEnum.OrderFinish + ") finished";
            sql += ",(SELECT COUNT(ID) FROM " + tbname + " WHERE Aside=0 AND PaymentStatus=" + (int)M_OrderList.PayEnum.RequestRefund + ") unrefund";
            sql += ",(SELECT COUNT(ID) FROM " + tbname + " WHERE Aside=0 AND PaymentStatus=" + (int)M_OrderList.PayEnum.Refunded + ") refunded";
            sql += ",(SELECT COUNT(ID) FROM " + tbname + " WHERE Aside=1) recycle";
            sql += " FROM " + tbname + " WHERE Aside=0";
            return DBCenter.ExecuteTable(sql);
        }
    }
}
