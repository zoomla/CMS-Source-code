namespace ZoomLa.BLL
{
    using System;
    using System.Data;
    using ZoomLa.Model;
    using ZoomLa.Common;
    using ZoomLa.SQLDAL;
    using System.Data.SqlClient;

    public class B_CartPro
    {
        private string TbName, PK;
        private M_CartPro initMod = new M_CartPro();
        public B_CartPro()
        {
            TbName = initMod.TbName;
            PK = initMod.PK;
        }
        public M_CartPro GetCartProByid(int CartProId)
        {
            return SelReturnModel(CartProId);
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
        /// <summary>
        /// 通过商品ID和购物车ID获取信息
        /// </summary>
        /// <param name="proid">商品ID</param>
        /// <param name="cartid">购物车ID</param>
        /// <param name="attribute">商品属性</param>
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
        public M_CartPro GetSelect(int proid, int cartid, string attribute)
        {
            string sqlStr = "SELECT * FROM ZL_CartPro WHERE ProID=" + proid + " AND CartID=" + cartid;
            if (string.IsNullOrEmpty(attribute))
                sqlStr += " And Attribute='' OR Attribute IS Null";
            else
                sqlStr += " And Attribute=@attribute";
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("attribute", attribute) };
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, sqlStr, sp))
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
        public DataTable Sel(int ID)
        {
            return Sql.Sel(TbName, PK, ID);
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
        public bool Update(M_CartPro model)
        {
            return Sql.UpdateByIDs(TbName, PK, model.ID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
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
        public bool DeleteByID(int ID)
        {
            return Sql.Del(TbName, ID);
        }
        public bool DeleteByPreID(int id)
        {
            return Sql.Del(TbName, PK + "=" + id);
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
        public DataTable U_SelForOrderList(int uid, string filter, string Skey = "", int ordertype = -1)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("Skey", "%" + Skey + "%"), new SqlParameter("Code", Skey) };
            string mtbname = "(SELECT A.*,B.LinPrice,B.Thumbnails,B.GuessXML,B.ProCode FROM " + TbName + " A LEFT JOIN " + new M_Product().TbName + " B ON A.ProID=B.ID)";
            string where = "B.UserID=" + uid;
            string order = "B.AddTime DESC";
            string fields = "A.ID AS CartID,A.ProID,A.ProName,A.Pronum,A.AddStatus,A.AllMoney,A.StoreID,A.Thumbnails,A.GuessXML,";//ZL_Cart
            fields += "B.ID,B.OrderNo,B.AddUser,B.AddTime,B.OrderStatus,B.PaymentStatus,B.OrdersAmount,B.StateLogistics,B.Aside,B.Delivery,B.Service_charge,B.OrderType,B.OrderMessage";//ZL_OrderInfo
            switch (filter)
            {
                case "all"://全部(不含回收站)
                    where += " AND B.Aside=0";
                    break;
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
            }
            if (!string.IsNullOrEmpty(Skey))//商品名搜索,超过8位则启动编号与订单号搜索
            {
                where += " AND (A.ProName LIKE @Skey";
                if (Skey.Length > 8) { where += " OR (A.ProCode=@Code OR B.OrderNo=@Code)"; }
                where += ")";
            }
            if (ordertype != -1) { where += " AND OrderType = " + ordertype; }
            return SqlHelper.JoinQuery(fields, mtbname, "ZL_OrderInfo", "A.OrderListID=B.ID", where, order, sp);
        }
    }
}
