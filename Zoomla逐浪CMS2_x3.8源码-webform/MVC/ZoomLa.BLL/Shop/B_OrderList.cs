namespace ZoomLa.BLL
{
    using System;
    using System.Data;
    using ZoomLa.Model;
    using ZoomLa.Common;
    using ZoomLa.SQLDAL;
    using System.Data.SqlClient;
    using System.Web;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using SQLDAL.SQL;   

    public class B_OrderList
    {
        private string TbName, PK;
        private M_OrderList initMod = new M_OrderList();
        public B_OrderList()
        {
            TbName = initMod.TbName;
            PK = initMod.PK;
        }
        //--------------------------------------------SELECT
        public M_OrderList SelReturnModel(int ID)
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
        public M_OrderList GetOrderListByid(int ID)
        {
            return SelReturnModel(ID);
        }
        public M_OrderList GetOrderListByid(int OrderListId, int orderType)
        {
            return SelReturnModel(OrderListId);
        }
        public M_OrderList SelReturnModel(string strWhere, SqlParameter[] sp = null)
        {
            using (SqlDataReader reader = Sql.SelReturnReader(TbName, strWhere, sp))
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
        /// 取上一张或下一张订单
        /// </summary>
        public M_OrderList SelNext(int id, string direction = "next")
        {
            string sql = "SELECT * FROM " + TbName + " WHERE {0} ORDER BY {1}";
            string where = "", order = "";
            switch (direction)
            {
                case "pre":
                    where = "ID<" + id; order = "ID DESC";
                    break;
                default:
                    where = "ID>" + id; order = "ID ASC";
                    break;
            }
            sql = string.Format(sql, where, order);
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, sql))
            {
                if (reader.Read())
                {
                    return initMod.GetModelFromReader(reader);
                }
                else
                    return null;
            }
        }
        public M_OrderList SelModelByOrderNo(string orderno)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("OrderNo", orderno) };
            using (SqlDataReader reader = Sql.SelReturnReader(TbName, "Where OrderNo=@OrderNo", sp))
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
        public M_OrderList GetByOrder(string OrderNo, string orderType)
        {
            SqlParameter[] sp = new SqlParameter[]
            {
                new SqlParameter("OrderNo", OrderNo),
                new SqlParameter("OrderType",orderType)
            };
            string strSql = "select * from " + TbName + " WHERE OrderNo = @OrderNo AND OrderType=@OrderType";
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, strSql, sp))
            {
                if (reader.Read())
                {
                    return initMod.GetModelFromReader(reader);
                }
                else
                    return null;
            }
        }
        //订单ID是否存在
        public bool FondOrder(int id)
        {
            string sql = "Select * From " + TbName + " Where ID=" + id;
            return SqlHelper.Exists(CommandType.Text, sql);
        }
        public DataTable Search(string orderType, string logistics, string orderStatus, string payStatus, string stime, string etime, int storeid, string uname, string uids, string quickSql, string skeySql, List<SqlParameter> sp)
        {
            string where = " 1=1 ";
            if (!string.IsNullOrEmpty(orderType)) { where += " AND Ordertype IN(" + orderType + ")"; SafeSC.CheckIDSEx(orderType); }
            if (!string.IsNullOrEmpty(logistics)) { where += " AND StateLogistics IN (" + logistics + ")"; SafeSC.CheckIDSEx(logistics); }
            if (!string.IsNullOrEmpty(orderStatus)) { where += " AND orderStatus IN (" + orderStatus + ")"; SafeSC.CheckIDSEx(orderStatus); }
            if (!string.IsNullOrEmpty(stime)) { where += " AND DATEDIFF(DAY,@stime,AddTime) >= 0"; DateTime sdt; DateTime.TryParse(stime, out sdt); sp.Add(new SqlParameter("stime", stime)); }
            if (!string.IsNullOrEmpty(etime)) { where += " AND DATEDIFF(DAY,AddTime,@etime) >= 0"; DateTime edt; if (!DateTime.TryParse(etime, out edt)) { edt = DateTime.Now; } sp.Add(new SqlParameter("etime", etime)); }
            if (!string.IsNullOrEmpty(uname)) { where += " AND ReName = @uname"; sp.Add(new SqlParameter("uname", uname)); }
            if (!string.IsNullOrEmpty(payStatus)) { where += " AND Paymentstatus=" + Convert.ToInt32(payStatus); }
            if (!string.IsNullOrEmpty(uids)) { SafeSC.CheckIDSEx(uids); where += " AND UserID IN (" + uids + ")"; }
            if (storeid == -100)//取全部
            {
            }
            else if (storeid == -99)//仅店铺
            {
                where += " AND StoreID>0";
            }
            else //仅取商城,或指定店铺
            {
                where += " AND StoreID=" + storeid;
            }
            if (!string.IsNullOrEmpty(quickSql)) { where += " " + quickSql; }
            if (!string.IsNullOrEmpty(skeySql)) { where += " " + skeySql; }
            return DBCenter.Sel(TbName, where, PK + " DESC", sp);
        }
        public DataTable SearchByQuickAndSkey(string orderType, string orderStatus, string payStatus, int quick, int type, string skey, int storeid = -100, string uids = "", string stime = "", string etime = "")
        {
            string logistics = "", uname = "";
            string quickSql = "", skeySql = "";
            List<SqlParameter> sp = new List<SqlParameter>();
            #region 快速筛选
            switch (quick)
            {
                case 1://我负责的订单
                    //quickSql += " AND adduser=@adduser ";
                    break;
                case 2://今天新订单
                    quickSql += " AND datediff(day,addtime,getdate())<1 ";
                    break;
                case 4://最近10天
                    quickSql += " AND datediff(day,addtime,getdate())<10 ";
                    break;
                case 5://最近一月
                    quickSql += " AND datediff(day,addtime,getdate())<31 ";
                    break;
                case 6://未确认
                    orderStatus = "0";
                    break;
                case 7://未付款
                    payStatus = "0";
                    break;
                case 8://未全部付款
                    quickSql += " AND Ordersamount>=Receivablesamount ";
                    break;
                case 9://未送货
                    logistics = "0";
                    break;
                case 10://未签收
                    quickSql += " AND Signed<>1 ";
                    break;
                case 11://未结清
                    quickSql += " AND Settle<>1 ";
                    break;
                case 12://未确认收货
                    logistics = "1";
                    break;
                case 13://已作废
                    quickSql += " AND Aside=1 ";
                    break;
                case 14://已挂机
                    quickSql += " AND Suspended=1 ";
                    break;
                case 15://已发货
                    logistics = "1";
                    break;
                case 16://已签收
                    logistics = "2";
                    break;
                case 17://已结清
                    quickSql += " AND Settle=1 ";
                    break;
                case 18://已申请退款
                    orderStatus = "-1";
                    break;
                default:
                    break;
            }
            #endregion
            #region 关键词搜索
            if (!string.IsNullOrEmpty(skey))
            {
                sp.Add(new SqlParameter("keyinfo", "%" + skey + "%"));
                switch (type)
                {
                    case 1:
                        skeySql += " AND OrderNo like @keyinfo ";
                        break;
                    case 2:
                        skeySql += " AND Reuser like @keyinfo ";
                        break;
                    case 3:
                        skeySql += " AND Rename like @keyinfo ";
                        break;
                    case 4:
                    case 15:
                        skeySql += " AND Receiver like @keyinfo ";
                        break;
                    case 5:
                        skeySql += " AND Jiedao like @keyinfo ";
                        break;
                    case 6:
                        skeySql += " AND Phone like @keyinfo ";
                        break;
                    case 7:
                        skeySql += " AND AddTime like @keyinfo ";
                        break;
                    case 8:
                        skeySql += " AND Ordermessage like @keyinfo ";
                        break;
                    case 9:
                        skeySql += " AND id in (select Orderlistid from ZL_CartPro where Proname like @keyinfo)  ";
                        break;
                    case 10:
                        skeySql += " AND Email like @keyinfo ";
                        break;
                    case 11:
                        skeySql += " AND Invoice like @keyinfo ";
                        break;
                    case 12:
                        skeySql += " AND Internalrecords like @keyinfo ";
                        break;
                    case 13:
                        skeySql += " AND AddUser like @keyinfo ";
                        break;
                    case 14:
                        skeySql += " AND UserID=" + DataConvert.CLng(skey);
                        break;
                    default:
                        skeySql += " AND OrderNo like @keyinfo ";
                        break;
                }
            }
            #endregion
            return Search(orderType, logistics, orderStatus, payStatus, stime, etime, storeid, uname, uids, quickSql, skeySql, sp);
        }
        public DataTable Sel()
        {
            return Sql.Sel(TbName);
        }
        /// <summary>
        /// 用于用户中心--订单列表
        /// </summary>
        public PageSetting U_SelPage(int cpage, int psize, int uid, string filter, string skey = "", int orderType = -1)
        {
            List<SqlParameter> sp = new List<SqlParameter>() { new SqlParameter("Skey", "%" + skey + "%"), new SqlParameter("Code", skey) };
            string where = "1=1 ";
            if (uid > 0) { where += " AND A.UserID=" + uid; }
            switch (filter)
            {
                case "all"://全部(不含回收站)
                    where += " AND A.Aside=0";
                    break;
                case "needpay"://需付款
                    where += " AND A.Aside=0 AND A.PaymentStatus=0";
                    break;
                case "receive"://需确认收货
                    where += " AND A.Aside=0 AND A.StateLogistics=1";
                    break;
                case "comment"://已完结可评价
                    where += " AND A.Aside=0 AND A.OrderStatus>=" + (int)M_OrderList.StatusEnum.OrderFinish + " AND (CHARINDEX('comment',B.AddStatus)=0 OR B.AddStatus IS NULL) ";
                    break;
                case "recycle"://订单回收站
                    where += " AND A.Aside=1";
                    break;
            }
            if (orderType != -1) { where += " AND OrderType = " + orderType; }
            PageSetting setting = PageSetting.Double(cpage, psize, TbName, "ZL_CartPro", "A." + PK, "A.ID=B.OrderListID", where, "A." + PK + " DESC", sp, "A.*,B.AddStatus");
            DBCenter.SelPage(setting);
            return setting;
        }
        /// <summary>
        /// 用于前后台|店铺筛选订单(支持快速搜索)
        /// </summary>
        public PageSetting SelPage(int cpage, int psize, out double total, int storeid, string orderType, string orderStatus, string payStatus)
        {
            string where = "1=1 ";
            if (storeid != -100) { where += " AND StoreID=" + storeid; }
            if (!string.IsNullOrEmpty(orderType)) { SafeSC.CheckIDSEx(orderType); where += " AND OrderType IN (" + orderType + ")"; }
            if (!string.IsNullOrEmpty(orderStatus)) { where += " AND OrderStatus=" + DataConvert.CLng(orderStatus); }
            if (!string.IsNullOrEmpty(payStatus)) { where += " AND Paymentstatus=" + DataConvert.CLng(payStatus); }
            PageSetting setting = PageSetting.Single(cpage, psize, TbName, PK, where, "", null);
            DBCenter.SelPage(setting);
            total = DataConvert.CDouble(DBCenter.ExecuteScala(TbName, "SUM(ordersamount)", where));
            return setting;
        }
        public DataTable GetOrderListByUid(int ID)
        {
            return Sql.Sel(TbName, PK, ID);
        }
        //支持,号切割
        public DataTable GetOrderbyOrderNo(string orderno)
        {
            string insql = "";
            string[] orderNoArr = orderno.Split(',');
            SqlParameter[] sp = new SqlParameter[orderNoArr.Length];
            for (int i = 0; i < orderNoArr.Length; i++)
            {
                sp[i] = new SqlParameter("@OrderNo" + i, orderNoArr[i]);
                insql += sp[i].ParameterName + ",";
            }
            insql = insql.Trim(',');
            string sql = "SELECT * FROM " + TbName + " WHERE OrderNo IN (" + insql + ") ORDER BY [ID] DESC";
            return SqlHelper.ExecuteTable(CommandType.Text, sql, sp);
        }
        public DataTable GetOrderbyOrderlist(string ids)
        {
            SafeSC.CheckIDSEx(ids);
            string sqlStr = "select * from ZL_Orderinfo where id in (" + ids + ") order by(id) desc";
            //string sqlStr = "select PaymentNum from zl_payment where paymentid=" + idlist;
            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, null);
        }
        /// <summary>
        /// 用于财务流水 搜索类型 1:按userid检索,2:按UserName检索
        /// </summary>
        public DataTable SelByHistory(int searchtype = 1, string search = "")
        {
            string strwhere = " WHERE 1=1";
            if (!string.IsNullOrEmpty(search))
            {
                if (searchtype == 1)
                {
                    strwhere += " AND UserID=@search";
                    search = DataConverter.CLng(search).ToString();
                }
                else
                {
                    strwhere += " AND AddUser LIKE @search";
                    search = "%" + search + "%";
                }
            }
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("@search", search) };
            string sql = "SELECT OrderNo AS ExpHisID,UserID,AddTime AS HisTime,AddUser AS UserName,Balance_price AS Score,Balance_remark AS Detail FROM " + TbName + strwhere + " ORDER BY AddTime DESC";
            return SqlHelper.ExecuteTable(CommandType.Text, sql, sp);
        }
        public DataTable GetUserlist(string Username, int type)
        {
            string strSql = "select * from ZL_Orderinfo where Rename=@Username and (parentID<=0 or parentID is null) order by(id) desc";
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("Username", Username) };
            switch (type)
            {
                case 0:
                    strSql = "select * from ZL_Orderinfo where Rename=@Username and (parentID<=0 or parentID is null)  order by(id) desc";

                    break;
                case 1:
                    strSql = "select * from ZL_Orderinfo where Rename=@Username and (parentID<=0 or parentID is null)  and OrderStatus=1 order by(id) desc";
                    break;
                case 2:
                    strSql = "select * from ZL_Orderinfo where Rename=@Username and (parentID<=0 or parentID is null)  and StateLogistics=1 order by(id) desc";
                    break;
                case 3:
                    strSql = "select * from ZL_Orderinfo where Rename=@Username and (parentID<=0 or parentID is null)  and Aside=0 and Suspended=0 order by(id) desc";
                    break;
                case 4:
                    strSql = "select * from ZL_Orderinfo where Rename=@Username and (parentID<=0 or parentID is null)  and (Settle=1 or Signed=1) order by(id) desc";
                    break;
                case 5:
                    strSql = "select * from ZL_Orderinfo where Rename=@Username and (parentID<=0 or parentID is null)  and Aside=1 order by(id) desc";
                    break;
                default:
                    break;
            }
            DataTable dt = SqlHelper.ExecuteTable(CommandType.Text, strSql, sp);
            return SqlHelper.ExecuteTable(CommandType.Text, strSql, sp);
        }
        public DataTable orderuser()
        {
            string sqlStr = "select * from ZL_User where username in (select top 100 Rename from ZL_Orderinfo where Aside=0 Group by Rename,id order by count(Rename) desc)";
            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, null);
        }
        public DataTable orderbuyuser()
        {
            string sqlStr = "select * from ZL_User where username in (select top 100 Rename from ZL_Orderinfo where Aside=0 and Paymentstatus=1 Group by Rename,id order by count(Rename) desc)";
            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, null);
        }
        public DataTable IDC_Sel(int orderType, string skeetype, string skey, string order = "id desc", string addon = "", int orderStatus = -100, int payStatus = -100)
        {
            string where = " OrderType = " + orderType;
            List<SqlParameter> sp = new List<SqlParameter>();
            sp.Add(new SqlParameter("skey", "%" + skey + "%"));
            switch (skeetype)
            {
                case "orderno":
                    where += " AND OrderNo LIKE @skey";
                    break;
                case "rename":
                    where += " AND Rename LIKE @skey";
                    break;
                case "proname":
                    where += " AND ProName LIKE @skey";
                    break;
                case "userid":
                    if (DataConvert.CLng(skey) > 0)
                    {
                        where += " AND Userid = " + DataConvert.CLng(skey);
                    }
                    break;
            }
            order = order ?? "";
            switch (order.ToLower().Trim(' '))
            {
                case "addtime desc":
                    order = "A.STime DESC";
                    break;
                case "addtime asc":
                    order = "A.STime ASC";
                    break;
                case "endtime desc":
                    order = "A.ETime DESC";
                    break;
                case "endtime asc":
                    order = "A.ETime ASC";
                    break;
                default:
                    order = "A.ID DESC";
                    break;
            }
            addon = addon ?? "";
            switch (addon.ToLower())
            {
                case "normal"://正常订单
                    where += " AND DATEDIFF(DAY,GETDATE(),A.ETime)>0 AND Paymentstatus = 1";
                    break;
                case "aboutex"://30天内到期订单
                    where += " AND DATEDIFF(DAY,GETDATE(),A.ETime) < 30 AND  DATEDIFF(DAY,GETDATE(),A.ETime) > 0 AND Paymentstatus = 1";
                    break;
                case "expired"://到期订单
                    where += " AND DATEDIFF(DAY,GETDATE(),A.ETime) <= 0 AND Paymentstatus = 1";
                    break;
                case "nopay"://未付款
                    where += " AND Paymentstatus = 0";
                    break;
            }
            string mtable = "(SELECT A.*,B.STime,B.ETime,B.Domain FROM " + TbName + " A LEFT JOIN ZL_Order_IDC B ON A.OrderNo=B.OrderNo)";
            return DBCenter.JoinQuery("A.*,B.Proname,B.Proid,B.ID AS Cid", mtable, "ZL_CartPro", "A.ID=B.Orderlistid", where, order, sp.ToArray());
        }
        public DataTable GetProAndOrder(string producer)
        {
            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@producer", SqlDbType.NVarChar, 100);
            cmdParams[0].Value = producer;
            string strSql = "select ZL_Orderinfo.Paymentstatus,ZL_P_shop.diiprice,ZL_Orderinfo.id as Orderid,ZL_CartPro.*,ZL_Orderinfo.* from  ZL_Commodities inner join ZL_P_shop on ZL_Commodities.ItemID=ZL_P_shop.id inner join ZL_CartPro on ZL_Commodities.id=ZL_CartPro.ProID inner join ZL_Orderinfo on ZL_Orderinfo.id=ZL_CartPro.Orderlistid where Orderlistid>0 and Producer=@producer order by ZL_CartPro.Addtime desc";
            return SqlHelper.ExecuteTable(CommandType.Text, strSql, cmdParams);
        }
        /// <summary>
        /// 读取订单
        /// </summary>
        public DataTable GetUserlist(string Username, int type, int orderType)
        {
            return GetUserlist(Username, type, orderType.ToString());
        }
        /// <summary>
        /// 搜索订单
        /// </summary>
        /// <param name="orderType">订单类型:0正常 1酒店 2航班 3旅游 4积分</param>
        public DataTable GetUserlist(string Username, int type, string orderType)
        {
            SafeSC.CheckIDSEx(orderType);
            string strSql = "select * from ZL_Orderinfo where Ordertype in(" + orderType + ")";
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("Username", Username) };
            switch (type)
            {
                case 0://全部订单,不包含作废
                    strSql += " AND Rename=@Username and (parentID<=0 or parentID is null) And Aside=0  order by(id) desc";
                    break;
                case 1:
                    strSql += " AND Rename=@Username and (parentID<=0 or parentID is null)  and OrderStatus=1 order by(id) desc";
                    break;
                case 2:
                    strSql += " AND Rename=@Username and (parentID<=0 or parentID is null)  and StateLogistics=1 order by(id) desc";
                    break;
                case 3://正常订单
                    strSql += " AND Rename=@Username and (parentID<=0 or parentID is null)  and Aside=0 and Suspended=0 order by(id) desc";
                    break;
                case 4:
                    strSql += " AND Rename=@Username and (parentID<=0 or parentID is null)  and (Settle=1 or Signed=1) order by(id) desc";
                    break;
                case 5://作废
                    strSql += " AND Rename=@Username and (parentID<=0 or parentID is null)  and Aside=1 order by(id) desc";
                    break;
                default:
                    break;
            }
            DataTable dt = SqlHelper.ExecuteTable(CommandType.Text, strSql, sp);
            return SqlHelper.ExecuteTable(CommandType.Text, strSql, sp);
        }
        /// <summary>
        /// 根据类型读取指定编号订单
        /// </summary>
        public DataTable GetOrderbyOrderNo(string OrderNo, int type)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("OrderNo", OrderNo) };
            string strSql = "select * from ZL_Orderinfo where OrderNo =@OrderNo  AND Ordertype=" + type + "' order by(id) desc";
            return SqlHelper.ExecuteTable(CommandType.Text, strSql, sp);
        }
        /// <summary>
        /// 根据类型读取指定编号订单
        /// </summary>
        public DataTable GetOrderbyOrderNo(string OrderNo, string type)
        {
            SafeSC.CheckIDSEx(type);
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("OrderNo", OrderNo) };
            string strSql = "select * from ZL_Orderinfo where OrderNo = @OrderNo AND Ordertype in(" + type + ") order by(id) desc";
            return SqlHelper.ExecuteTable(CommandType.Text, strSql, sp);
        }
        /// <summary>
        /// 读取订单信息
        /// </summary>
        public M_OrderList GetOrderListByid(int OrderListId, string orderType)
        {
            SafeSC.CheckIDSEx(orderType);
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, "select * from ZL_Orderinfo where id=" + OrderListId + " AND Ordertype in (" + orderType + ") ORDER BY id desc"))
            {
                if (reader.Read())
                {
                    return initMod.GetModelFromReader(reader);
                }
                else
                    return new M_OrderList();
            }
        }
        /// <summary>
        /// 依据UserID与支付方式,查找目标,包含商品信息,用于IDC
        /// </summary>
        public DataTable SelByUserID(int userID, int payment = 1)
        {
            string field = "A.OrderNo,A.Internalrecords,A.OrderMessage,A.Paymentstatus,B.*";
            //string sql = "Select " + field + " From ZL_OrderInfo as a Left Join ZL_CartPro as b on a.id=b.orderlistid Where a.UserID=" + userID + " And a.OrderType=7 And a.PayMentStatus=" + payment + " Order by b.AddTime Desc";
            //return SqlHelper.ExecuteTable(CommandType.Text, sql);
            string where = "A.UserID=" + userID + " AND A.OrderType=7";
            if (payment != 2)//2显示全部
            {
                where += " AND A.PayMentStatus= " + payment;
            }
            return SqlHelper.JoinQuery(field, "ZL_OrderInfo", "ZL_CartPro", "A.ID=B.OrderListID", where, " B.AddTime DESC");
        }
        /// <summary>
        /// 依据订单ID查找目标,包含商品信息,用于IDC,续费
        /// </summary>
        public DataTable SelByOrderNo(string orderNo)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("OrderNo", orderNo) };
            string field = "a.OrderNo,a.Internalrecords,b.*";
            string sql = "Select " + field + " From ZL_OrderInfo as a Left Join ZL_CartPro as b on a.id=b.orderlistid Where a.OrderNo=@OrderNo And a.OrderType=7 Order by b.AddTime Desc";
            return SqlHelper.ExecuteTable(CommandType.Text, sql, sp);
        }
        public DataTable SelByTime(string stime, string etime, int type)
        {
            string sql = "Select * From " + TbName + " Where 1=1";
            string cds = "";
            string cdsd = "";
            if (stime != "")
            {
                cds = " AND AddTime>@stime ";
            }
            if (etime != "")
            {
                if (cds != "")
                {
                    cds = cds + " and ";
                }

                cds = cds + "AddTime<@etime";
            }
            if (cds != "")
            {
                cdsd = cds;
                cds = " and " + cds;

            }
            switch (type)
            {
                case 1://客户平均订单
                    sql += "and Aside=0 " + cds;
                    break;
                case 2://每次访问订单
                    sql += cdsd;
                    break;
                case 3://匿名购买率
                    sql += "and Rename='' and Aside=0 and Paymentstatus=1 " + cds;
                    break;
                case 4://会员购买率
                    sql += "and Rename<>'' and Aside=0 and Paymentstatus=1 " + cds;
                    break;
            }
            try
            {
                SqlParameter[] sp = new SqlParameter[] { new SqlParameter("stime", stime), new SqlParameter("etime", etime) };
                return SqlHelper.ExecuteTable(CommandType.Text, sql, sp);
            }
            catch { throw new Exception(sql); }
        }
        //--------------------------------------------INSERT
        public int insert(M_OrderList model)
        {
            return Sql.insertID(TbName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        public bool Add(M_OrderList model)
        {
            Sql.insertID(TbName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
            return true;
        }
        public int Adds(M_OrderList model)
        {
            return Sql.insertID(TbName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        //--------------------------------------------UPDATE
        public bool UpdateByID(M_OrderList model)
        {
            return Sql.UpdateByIDs(TbName, PK, model.id.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }
        public bool UpdateByField(string fieldName, string value, int id)
        {
            SafeSC.CheckDataEx(fieldName);
            string sql = "Update " + TbName + " Set " + fieldName + " =@value Where [id] =" + id;
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("value", value) };
            SqlHelper.ExecuteNonQuery(CommandType.Text, sql, sp);
            return true;
        }
        public bool Update(M_OrderList model)
        {
            return UpdateByID(model);
        }
        //页面无注入
        public bool UpOrderinfo(string info, int id)
        {
            string sqlStr = "update ZL_Orderinfo set " + info + " where id=" + id + "";
            return SqlHelper.ExecuteSql(sqlStr, null);
        }
        /// <summary>
        /// 插入快递单号
        /// </summary>
        public int UpdateExpressNum(string Num, int id)
        {
            string sql = "update ZL_Orderinfo set ExpressNum=@ExpressNum where id=" + id;
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("@ExpressNum", Num), };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(CommandType.Text, sql, sp));
        }
        /// <summary>
        /// 订单状态批量修改_后台调用
        /// </summary>
        public void ChangeStatus(string ids, M_OrderList.StatusEnum status)
        {
            if (string.IsNullOrEmpty(ids)) { return; }
            SafeSC.CheckIDSEx(ids);
            switch (status)
            {
                case M_OrderList.StatusEnum.Sured://确认订单
                    DBCenter.UpdateSQL(TbName, "OrderStatus=" + (int)M_OrderList.StatusEnum.Sured, "ID IN (" + ids + ") AND OrderStatus=" + (int)M_OrderList.StatusEnum.Normal);
                    break;
                default:
                    throw new Exception("[" + status + "]的操作不存在");
            }
        }
        public void ChangeStatus(string ids, string status)
        {
            if (string.IsNullOrEmpty(ids)) { return; }
            SafeSC.CheckIDSEx(ids);
            switch (status.ToLower())
            {
                case "recover":
                    DBCenter.UpdateSQL(TbName, "Aside=0", "ID IN (" + ids + ")");
                    break;
                case "recycle":
                    DBCenter.UpdateSQL(TbName, "Aside=1", "ID IN (" + ids + ")");
                    break;
                default:
                    throw new Exception("[" + status + "]操作不存在");
            }
        }
        //----------------------------DELETE
        public bool Del(int ID)
        {
            return Sql.Del(TbName, ID);
        }
        public void DelByIDS(string ids)
        {
            SafeSC.CheckIDSEx(ids);
            string sql = "DELETE FROM " + TbName + " WHERE ID IN(" + ids + ")";
            SqlHelper.ExecuteSql(sql);
        }
        public void ClearRecycle()
        {
            DBCenter.DelByWhere(TbName, "Aside=1");
        }
        //----------------------------Tools
        public static string CreateOrderNo(M_OrderList.OrderEnum orderType)
        {
            string result = "";
            string name = DateTime.Now.ToString("yyyyMMddHHmmssfff") + function.GetRandomString(4, 2);
            switch (orderType)
            {
                case M_OrderList.OrderEnum.Normal:
                    result = "DD" + name;
                    break;
                case M_OrderList.OrderEnum.IDC:
                    result = "IDC" + name;
                    break;
                case M_OrderList.OrderEnum.IDCRen:
                    result = "IDCR" + name;
                    break;
                case M_OrderList.OrderEnum.Score:
                    result = "DP" + name;
                    break;
                case M_OrderList.OrderEnum.Cloud:
                    result = "YG" + name;
                    break;
                case M_OrderList.OrderEnum.Purse://充值
                    result = "RC" + name;
                    break;
                case M_OrderList.OrderEnum.Trval:
                    result = "TR" + name;
                    break;
                case M_OrderList.OrderEnum.Hotel:
                    result = "HT" + name;
                    break;
                case M_OrderList.OrderEnum.Fast:
                    result = "FT" + name;
                    break;
                case M_OrderList.OrderEnum.Other:
                    result = "OT" + name;
                    break;
                case M_OrderList.OrderEnum.Donate:
                    result = "DO" + name;
                    break;
                default:
                    throw new Exception("指定的类型不存在");
            }
            return result;
        }
        /// <summary>
        /// 产生幸运码,并返回用于输出的Html
        /// </summary>
        public string CreateLuckCode(M_OrderList orderMod)
        {
            List<M_Product> proList = new List<M_Product>();
            B_Product proBll = new B_Product();
            B_Order_LuckCode codeBll = new B_Order_LuckCode();
            B_CartPro cartBll = new B_CartPro();
            string result = "";
            //*一个购物车中，可能会有多个商品,
            DataTable cartDT = cartBll.GetCartProOrderID(orderMod.id);//购物车记录
            //判断出购物车中哪些是云购商品
            for (int i = 0; i < cartDT.Rows.Count; i++)
            {
                int proID = Convert.ToInt32(cartDT.Rows[i]["ProID"]);
                M_Product proMod = proBll.GetproductByid(proID);
                if (proMod.ProClass == 5)
                {
                    //减去相应的库存
                    proMod.Stock = proMod.Stock - Convert.ToInt32(cartDT.Rows[i]["ProNum"]);
                    proBll.updateinfo(proMod);
                    proMod.Class = Convert.ToInt32(cartDT.Rows[i]["ProNum"]);//购买数量,用备用字段存着
                    proList.Add(proMod);
                }
            }
            //根据购买的数量与信息,生成对应条数的幸运码,与上面分离开,便于后期扩展
            for (int i = 0; i < proList.Count; i++)
            {
                //获取该商品在数据库中的最大幸运码数
                int code = codeBll.GetMaxLuckCode(proList[i].ID);
                M_Order_LuckCode codeMod = new M_Order_LuckCode();
                codeMod.ProID = proList[i].ID;
                codeMod.UserID = orderMod.Userid;
                codeMod.OrderID = orderMod.id;
                codeMod.OrderNO = orderMod.OrderNo;
                DateTime now = DateTime.Now;
                codeMod.CreateTime = now.ToString("yyyy/MM/dd HH:mm:ss:fff");
                codeMod.CreateTime2 = now.ToString("yyyyMMddHHmmssfff");
                result += "商品名:" + proList[i].Proname + "<br/>幸运码:";
                for (int j = 1; j <= proList[i].Class; j++)
                {
                    codeMod.Code = (code + j);
                    codeBll.Insert(codeMod);
                    result += codeMod.Code + ",";
                }
                result += "<br/>";
            }
            return result.TrimEnd(','); ;
        }
        /// <summary>
        /// 创建一张新订单
        /// </summary>
        public M_OrderList NewOrder(M_UserInfo mu, M_OrderList.OrderEnum orderType)
        {
            M_OrderList orderMod = new M_OrderList();
            orderMod.Ordertype = (int)orderType;
            orderMod.OrderNo = CreateOrderNo(orderType);
            orderMod.Userid = mu.UserID;
            orderMod.AddUser = mu.UserName;
            orderMod.Reuser = mu.UserName;
            orderMod.Receiver = mu.UserName;
            orderMod.Paymentstatus = (int)M_OrderList.PayEnum.NoPay;
            orderMod.OrderStatus = (int)M_OrderList.StatusEnum.Normal;
            orderMod.StateLogistics = (int)M_OrderList.ExpEnum.NoSend;
            orderMod.StoreID = 0;
            orderMod.Receivablesamount = 0;
            orderMod.Suspended = 0;
            orderMod.Developedvotes = 0;
            return orderMod;
        }
        //----------------------------Logical
        /// <summary>
        /// 支付完成结后,更改订单状态(支付单传入时无ID号)
        /// </summary>
        public bool FinishOrder(int mid, M_Payment payMod)
        {
            List<SqlParameter> sp = new List<SqlParameter>() { new SqlParameter("PaymentNo", payMod.PayNo) };
            DBCenter.UpdateSQL(TbName, "OrderStatus=99,PaymentNo=@PaymentNo", "ID=" + mid, sp);
            return true;
        }
        //----------------------------用户使用方法,所有必须传入UserID验证
        public void DelByIDS_U(string ids, int uid, int aside = 1)
        {
            if (string.IsNullOrEmpty(ids)) return;
            SafeSC.CheckIDSEx(ids);
            string sql = "";
            switch (aside)
            {
                case 0://还原
                    sql = "Update " + TbName + " Set Aside=0 Where ID in(" + ids + ") And UserID=" + uid;
                    break;
                case 1://即入用户回收站(可删除未付款或已完成的)
                    sql = "Update " + TbName + " Set Aside=1 Where ID in(" + ids + ") And UserID=" + uid + " And (Paymentstatus != 1 OR OrderStatus>" + (int)M_OrderList.StatusEnum.OrderFinish + ")";
                    break;
                case 2://用户彻底删除
                    sql = "Update " + TbName + " SET Aside=2 Where ID in(" + ids + ") And UserID=" + uid + " And Aside=1";
                    break;
            }
            SqlHelper.ExecuteSql(sql);
        }
        public DataTable SelInvoByUser(int uid)
        {
            string sql = "Select Top 5 * From (Select Distinct(Invoice) From " + TbName + " Where Userid=" + uid + " And Invoice !='')as A";
            DataTable dt = SqlHelper.ExecuteTable(CommandType.Text, sql);
            dt.Columns.Add(new DataColumn("Head", typeof(string)));
            dt.Columns.Add(new DataColumn("Detail", typeof(string)));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["Head"] = Regex.Split(dt.Rows[i]["Invoice"].ToString(), Regex.Escape("||"))[0];
                dt.Rows[i]["Detail"] = Regex.Split(dt.Rows[i]["Invoice"].ToString(), Regex.Escape("||"))[1];
            }
            return dt;
        }
        /// <summary>
        /// 主用用户中心订单获取方法(只显示正常未作废订单)
        /// </summary>
        public DataTable U_SelByUserID(int uid)
        {
            string sql = "SELECT * FROM " + TbName + " WHERE Aside=0 AND UserID=" + uid + " ORDER BY AddTime DESC";
            return SqlHelper.ExecuteTable(sql);

        }
        //----------------------------Store(后期分离为另一个BLL)
        //获取我的店铺订单
        public DataTable Store_Getsouchinfo(int type, string keyinfo, int storeid)//后期扩展StoreID属性,支持搜索指定店铺 
        {
            DataTable dt = Getsouchinfo(type, keyinfo, "0,4,8", true);
            dt.DefaultView.RowFilter = "StoreID=" + storeid;
            return dt.DefaultView.ToTable();
        }
        public DataTable Store_Getsouchinfo(int type, string keyinfo, int storeid, int ordertype)//后期扩展StoreID属性,支持搜索指定店铺 
        {
            string strwhere = ordertype == 0 ? "" : " AND Ordertype=" + ordertype;
            DataTable dt = Getsouchinfo(type, keyinfo, "0,4,8", true);
            dt.DefaultView.RowFilter = "StoreID=" + storeid + strwhere;
            return dt.DefaultView.ToTable();
        }
        public DataTable Store_GetOrderListtype(int type, int storeid, string orderStatus = "0")
        {
            DataTable dt = GetOrderListtype(type, "0,4,8", true, orderStatus);
            dt.DefaultView.RowFilter = "StoreID=" + storeid;
            return dt.DefaultView.ToTable();

        }
        public DataTable Store_GetOrderListtype(int type, int storeid, int ordertype, string orderStatus = "0")
        {
            string strwhere = ordertype == 0 ? "" : " AND Ordertype=" + ordertype;
            DataTable dt = GetOrderListtype(type, "0,4,8", true, orderStatus);
            dt.DefaultView.RowFilter = "StoreID=" + storeid + strwhere;
            return dt.DefaultView.ToTable();
        }
        public DataTable Store_SelMyOrder(M_UserInfo mu)
        {
            B_Content conBll = new B_Content();
            M_CommonData storeMod = conBll.SelMyStore(mu.UserName);
            if (storeMod == null)
            {
                return null;
            }
            return Store_SelMyOrder(storeMod.GeneralID);
        }
        public DataTable Store_SelMyOrder(M_UserInfo mu, int ordertype)
        {
            B_Content conBll = new B_Content();
            M_CommonData storeMod = conBll.SelMyStore(mu.UserName);
            if (storeMod == null)
            {
                return null;
            }
            return Store_SelMyOrder(storeMod.GeneralID, ordertype);
        }
        public DataTable Store_SelMyOrder(int sotreid)
        {
            string sql = "Select * From " + TbName + " WHERE StoreID=" + sotreid;
            return SqlHelper.ExecuteTable(CommandType.Text, sql);
        }
        public DataTable Store_SelMyOrder(int sotreid, int ordertype)
        {
            string sql = "Select * From " + TbName + " WHERE StoreID=" + sotreid + " ORDER BY AddTime DESC";
            return SqlHelper.ExecuteTable(CommandType.Text, sql);
        }
        //--------------------------Report
        /// <summary>
        /// 按日期获取数据,精确到日
        /// </summary>
        public DataTable Report_SelByDate(DateTime stime, DateTime etime)
        {
            string field = "A.ID,A.OrderNo,A.Ordersamount,A.Receivablesamount,B.PayTime,B.PayPlatID";
            string where = "(A.PaymentNO IS NOT NULL AND A.PaymentNO!='') AND A.OrderStatus=99 ";
            where += " AND PayTime>='" + stime.ToString("yyyy/MM/dd 00:00:00") + "'";
            where += " AND PayTime<='" + etime.ToString("yyyy/MM/dd 23:59:59") + "'";
            return DBCenter.JoinQuery(field, "ZL_OrderInfo", "ZL_Payment", "A.PaymentNo=B.PayNo", where);
        }
        public DataTable SelPVOrder(int type)
        {
            string sql = "Select a.*,(Select SUM(cart.Pronum*pro.PointVal) From ZL_CartPro cart left join ZL_Commodities pro on cart.ProID=pro.ID Where cart.Orderlistid=a.id ) PVs From " + TbName + " a Where Paymentstatus=1 And OrderStatus=99 And datediff(day,AddTime,getdate())>10 Order By AddTime Desc";
            return SqlHelper.ExecuteTable(CommandType.Text, sql);
        }
        #region 旧订单搜索
        public DataTable Getsouchinfo(string keyinfo)
        {
            return Getsouchinfo(1, keyinfo, "0,4,8", false);
        }
        public DataTable Getsouchinfo(int type, string keyinfo)
        {
            return Getsouchinfo(type, keyinfo, "0,4,8", false);
        }
        public DataTable Getsouchinfo(int type, string keyinfo, int orderType)
        {
            return Getsouchinfo(type, keyinfo, orderType.ToString());
        }
        /// <summary>
        /// 搜索订单
        /// </summary>
        /// <param name="orderType">订单类型:0正常 1酒店 2航班 3旅游 4积分</param>
        /// <isStore>True:店铺,False自营商城</isStore>
        public DataTable Getsouchinfo(int type, string keyinfo, string orderType = "0,4,8", bool isStore = false)
        {
            SafeSC.CheckIDSEx(orderType);
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("keyinfo", "%" + keyinfo + "%") };
            string strSql = "select * from " + TbName + " WHERE Ordertype in(" + orderType + ")";
            switch (type)
            {
                case 1:
                    strSql += " AND OrderNo like @keyinfo ";
                    break;
                case 2:
                    strSql += " AND Reuser like @keyinfo ";
                    break;
                case 3:
                    strSql += " AND Rename like @keyinfo ";
                    break;
                case 4:
                    strSql += " AND Receiver like @keyinfo ";
                    break;
                case 5:
                    strSql += " AND Jiedao like @keyinfo ";
                    break;
                case 6:
                    strSql += " AND Phone like @keyinfo ";
                    break;
                case 7:
                    strSql += " AND AddTime like @keyinfo ";
                    break;
                case 8:
                    strSql += " AND Ordermessage like @keyinfo ";
                    break;
                case 9:
                    strSql += " AND id in (select Orderlistid from ZL_CartPro where Proname like @keyinfo)  ";
                    break;
                case 10:
                    strSql += " AND Email like @keyinfo ";
                    break;
                case 11:
                    strSql += " AND Invoice like @keyinfo ";
                    break;
                case 12:
                    strSql += " AND Internalrecords like @keyinfo ";
                    break;
                case 13:
                    strSql += " AND AddUser like @keyinfo ";
                    break;
                case 14:
                    sp[0].Value = keyinfo;
                    strSql += " AND parentID=@keyinfo ";
                    break;
                default:
                    strSql += " AND OrderNo like @keyinfo ";
                    break;
            }
            if (isStore)
            {
                strSql += " AND StoreID>0";
            }
            else strSql += " And StoreID=0";
            strSql += " ORDER BY(id) DESC";
            return SqlHelper.ExecuteTable(CommandType.Text, strSql, sp);
        }
        /// <summary>
        /// 通过时间获取订单
        /// </summary>
        public DataTable GetOrderBytime(string time)
        {
            string sqlStr = "SELECT * FROM ZL_Orderinfo WHERE AddTime BETWEEN '" + time + "' AND DATEADD(DAY,1,'" + time + "')";
            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, null);
        }
        //--------------------------------------------
        public DataTable GetOrderListtype(int type)
        {
            return GetOrderListtype(type, "0,4,8");
        }
        public DataTable GetOrderListtype(int type, int orderType)
        {
            return GetOrderListtype(type, orderType.ToString());
        }
        /// <summary>
        /// 快速查询
        /// </summary>
        public DataTable GetOrderListtype(int type, string orderType = "0,4,8", bool isStore = false, string orderStatus = "0")
        {
            SafeSC.CheckIDSEx(orderType);
            string strSql = "select * from ZL_Orderinfo where 1=1";
            strSql += " AND Ordertype in(" + orderType + ")";
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("adduser", new B_Admin().GetAdminLogin().AdminName) };
            switch (type)
            {
                case 1:
                    strSql += " AND adduser=@adduser ";
                    break;
                case 2:
                    strSql += " AND datediff(day,addtime,getdate())<1 ";
                    break;
                case 3:
                    strSql += " ";
                    break;
                case 4:
                    strSql += " AND datediff(day,addtime,getdate())<10 ";
                    break;
                case 5:
                    strSql += " AND datediff(day,addtime,getdate())<31 ";
                    break;
                case 6:
                    strSql += " AND OrderStatus<>1 ";
                    break;
                case 7:
                    strSql += " AND Paymentstatus<>1 ";
                    break;
                case 8:
                    strSql += " AND Ordersamount>=Receivablesamount ";
                    break;
                case 9:
                    strSql += " AND StateLogistics<>1 ";
                    break;
                case 10:
                    strSql += " AND Signed<>1 ";
                    break;
                case 11:
                    strSql += " AND Settle<>1 ";
                    break;
                case 12:
                    strSql += " AND Developedvotes<>1 ";
                    break;
                case 13:
                    strSql += " AND Aside=1 ";
                    break;
                case 14:
                    strSql += " AND Suspended=1 ";
                    break;
                case 15:
                    strSql += " AND StateLogistics=1 ";
                    break;
                case 16:
                    strSql += " AND Signed=1 ";
                    break;
                case 17:
                    strSql += " AND Settle=1 ";
                    break;
                case 18:
                    strSql += " AND OrderStatus=-1";
                    break;
                default:
                    strSql += "select * from ZL_Orderinfo ";
                    break;
            }
            strSql += isStore ? " AND StoreID>0" : " And StoreID=0";
            strSql += " ORDER BY(id) DESC";
            return SqlHelper.ExecuteTable(CommandType.Text, strSql, sp);
        }
        public DataTable GetOrderListtype(int type, string orderStatus)
        {
            return GetOrderListtype(type, "0.4", false, orderStatus);
        }
        //--------------------------------------------
        /// <summary>
        /// 通过订单类型读取订单
        /// </summary>
        public DataTable GetOrderListByOrderType(string orderType = "0,4,8", bool isStore = false)
        {
            SafeSC.CheckIDSEx(orderType);
            string sql = "select * from ZL_Orderinfo where (parentID<=0 or parentID is null) AND Ordertype in(" + orderType + ") ";
            if (isStore)
            {
                sql += " AND StoreID>0";
            }
            else sql += " And StoreID=0";
            sql += " ORDER BY(id) DESC";
            return SqlHelper.ExecuteTable(CommandType.Text, sql, null);
        }
        public DataTable GetOrderListByOrderType(int orderType)
        {
            return GetOrderListByOrderType(orderType.ToString());
        }
        #endregion
    }
}