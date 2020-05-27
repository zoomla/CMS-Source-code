namespace ZoomLa.BLL
{
    using System;
    using System.Data;
    using ZoomLa.Model;
    using ZoomLa.Common;
    using ZoomLa.SQLDAL;
    using System.Data.SqlClient;
    using System.Text;
    using System.Collections.Generic;
    using System.Web;
    using SQLDAL.SQL;
    using ZoomLa.BLL.Helper;
    public class B_Cart
    {
        private string TbName, PK;
        private M_Cart initMod = new M_Cart();
        public B_Cart() 
        {
            TbName = initMod.TbName;
            PK = initMod.PK;
        }
        public DataTable Sel(int ID)
        {
            return Sql.Sel(TbName, PK, ID);
        }
        public M_Cart SelReturnModel(int ID)
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
        /// 按商品ID+用户|购物车号,返回是否有购物车记录
        /// </summary>
        public M_Cart SelModelByWhere(int uid, int proID)
        {
            string where = " WHERE ProID=" + proID + " AND UserID=" + uid;
            return SelReturnModel(where);
        }
        private M_Cart SelReturnModel(string strWhere)
        {
            using (SqlDataReader reader = Sql.SelReturnReader(TbName, strWhere))
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
        public DataTable Sel()
        {
            return Sql.Sel(TbName);
        }
        /// <summary>
        /// 以cartid作为身份标识,修改购物车中商品的数量
        /// </summary>
        public void UpdateProNum(string cartid, int uid, int id, int pronum)
        {
            List<SqlParameter> sp = new List<SqlParameter>() { new SqlParameter("cartid", cartid) };
            string where = "ID=" + id;
            if (uid > 0) { where += " AND (CartID=@cartid OR UserID=" + uid + ")"; }
            else { where += " AND CartID=@cartid"; }
            DBCenter.UpdateSQL(TbName, "Pronum=" + pronum, where, sp);
        }
        public bool UpdateByID(M_Cart model)
        {
            return Sql.UpdateByIDs(TbName, PK, model.ID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }
        public bool Del(int ID)
        {
            return Sql.Del(TbName, ID);
        }
        public int insert(M_Cart model)
        {
            return Sql.insertID(TbName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        public bool Add(M_Cart model) 
        {
            insert(model);
            return true;
        }
        public bool DeleteByID(int CartId)
        {
            return Sql.Del(TbName, CartId);
        }
        public bool DelByids(string ids)
        {
            if (SafeSC.CheckIDS(ids))
            {
                string sql = "Delete From " + TbName + " Where ID IN (" + ids + ")";
                return SqlHelper.ExecuteNonQuery(CommandType.Text, sql) > 0;
            }
            return false;
        }
        public bool Update(M_Cart model)
        {
            return Sql.UpdateByIDs(TbName, PK, model.ID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }
        public DataTable GetCartAll()
        {
            string strSql = "select * from ZL_Cart order by id desc";
            return SqlHelper.ExecuteTable(CommandType.Text, strSql, null);
        }
        public PageSetting SelPage(int cpage, int psize)
        {
            PageSetting setting = PageSetting.Single(cpage, psize, TbName, PK, "");
            DBCenter.SelPage(setting);
            return setting;
        }
        public M_Cart GetCartByid(int ID)
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
        /// 购物车是否存在,True:存在
        /// </summary>
        public bool FondCart(int id) 
        {
            string sql = "Select * From "+TbName+" Where ID="+id;
            return SqlHelper.ExecuteTable(CommandType.Text,sql).Rows.Count>0;
        }
        public DataTable GetByCartID(string uid)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("uid", uid) };
            string strSql = "select * from ZL_Cart where userid= @uid order by(id) desc";
            return SqlHelper.ExecuteTable(CommandType.Text, strSql, sp);
        }
        public DataTable GetCartUser(string Username)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("Username",Username) };
            string strSql = "select * from ZL_Cart where Username = @Username order by(id) desc";
            return SqlHelper.ExecuteTable(CommandType.Text, strSql, sp);
        }
        /// <summary>
        /// 返回购物车商品列表
        /// </summary>
        public DataTable GetCarProList(string cartno)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("cartno", cartno) };
            string cartsql = "Select * From ZL_Cart Where Cartid=@cartno";
            DataTable dt = SqlHelper.ExecuteTable(CommandType.Text, cartsql, sp);
            if (dt.Rows.Count < 1) { return null; }
            int cartid = Convert.ToInt32(dt.Rows[0]["ID"]);
            return DBCenter.JoinQuery("A.*,B.ProClass", "ZL_CartPro", "ZL_Commodities", "A.ProID=B.ID", "A.CartID=" + cartid);
        }
         /// <summary>
        /// 获取购物车总金额
        /// </summary>
        /// <param name="charid"></param>
        /// <returns></returns>
        public string GetCartAll(string charid)
        {
            string value = "";
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("charid", charid) };
            string sql = "select AllMoney from ZL_Cart where cartid=@charid";
            SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, sql,sp);
            if (dr.Read())
            {
                value = dr["AllMoney"].ToString();
            }
            dr.Close();
            return value;
        }
        //----------------------------新购物流程
        public DataTable SelByCookID(string cartid, int proclass, string ids = "")
        {
            return SelByCartID(cartid, 0, proclass, ids);
        }
        /// <summary>
        /// 登录时触发,多平台购物车数据共享
        /// </summary>
        public static void UpdateUidByCartID(string cartid, int uid)
        {
            if (string.IsNullOrEmpty(cartid) || uid < 1) { return; }
            List<SqlParameter> spList = new List<SqlParameter>() { new SqlParameter("cartid", cartid) };
            //更新掉CartID与UserID,使其在未登录购物车中不可见
            DBCenter.UpdateSQL("ZL_Cart", "UserID=" + uid, "Cartid=@cartid AND (UserID=0 OR UserID IS NULL)", spList);
            //按ProID分组取最大值,移除重复与小于其的(JD逻辑)
            string delSql = "DELETE FROM ZL_Cart WHERE UserID=" + uid + " AND ID NOT IN ({0})";
            string selMax = "SELECT MIN(B.ID) FROM "
                     + "(SELECT ProID,max(Pronum) AS Pronum FROM ZL_Cart WHERE UserID=" + uid + " GROUP BY ProID)T"
                     + " LEFT JOIN ZL_Cart B ON T.ProID=B.ProID AND T.Pronum=B.Pronum WHERE B.UserID=" + uid + " GROUP BY B.ProID";
            delSql = string.Format(delSql, selMax);
            SqlHelper.ExecuteSql(delSql);
        }
        /// <summary>
        /// 筛选用户商品,并更新购物车表信息
        /// </summary>
        /// <param name="cartid">PC下为Cookies值,APP中为用户ID,用于简化逻辑</param>
        /// <param name="proclass">类别</param>
        public DataTable SelByCartID(string cartid, int uid, int proClass, string ids = "")
        {
            //UpdateUidByCartID(cartid,uid);
            string fields = " A.*,B.LinPrice,B.PointVal,B.Thumbnails,B.ProClass,B.ProUnit,B.Allowed,B.Stock,B.FarePrice,B.LinPrice_Json,B.ParentID ";
            string where = "";
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("cartid", cartid) };
            if (uid > 0) { where = " (A.Cartid=@cartid OR A.UserID=" + uid + ")"; } else { where = " A.Cartid=@cartid"; }
            //--------------------------------------
            if (!string.IsNullOrEmpty(ids)) { SafeSC.CheckIDSEx(ids); where += " AND A.ID IN (" + ids + ")"; }
            if (proClass != -100) { where += " AND B.ProClass=" + proClass; }
            string sql = "SELECT " + fields + " FROM ZL_Cart A LEFT JOIN ZL_Commodities B ON A.ProID=B.ID WHERE " + where;
            //自营商品,店铺商品
            DataTable dt = SqlHelper.ExecuteTable(sql, sp);
            return dt;
        }
        /// <summary>
        /// 购物车中有则增加数量，否则添加记录
        /// </summary>
        public int AddModel(M_Cart model)
        {
            if (string.IsNullOrEmpty(model.Getip)) { model.Getip = IPScaner.GetUserIP(); }
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("cartid", model.Cartid) };
            string sql = "Select * From ZL_Cart Where StoreID=" + model.StoreID + " And CartID=@cartid And ProID=" + model.ProID;
            DataTable dt = SqlHelper.ExecuteTable(CommandType.Text, sql, sp);
            if (dt.Rows.Count > 0)
            {
                int id = Convert.ToInt32(dt.Rows[0]["ID"]);
                string updatesql = "Update ZL_Cart Set Pronum=Pronum+" + model.Pronum + " Where ID=" + id;
                SqlHelper.ExecuteSql(updatesql);
                return id;
            }
            else { return insert(model); }
        }
        public bool DelByIDS(string cartid, string uname, string ids)
        {
            SafeSC.CheckIDSEx(ids);
            List<SqlParameter> sp = new List<SqlParameter>() { new SqlParameter("cartid", cartid), new SqlParameter("uname", uname) };
            string where = " ID IN (" + ids + ") ";
            if (!string.IsNullOrEmpty(uname)) { where += " AND (CartID=@cartid OR UserName=@uname)"; }
            else { where += " AND CartID=@cartid"; }
            return DBCenter.DelByWhere(TbName, where, sp);
        }
        public void UpdateByField(int ftype, string value, string ids)
        {
            SafeSC.CheckIDSEx(ids);
            string field = "";
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("value", value) };
            switch (ftype)
            {
                default:
                    field = "OrderID";
                    break;
            }
            string sql = "Update " + TbName + " Set " + field + " =@value Where ID IN(" + ids + ")";
            SqlHelper.ExecuteNonQuery(CommandType.Text, sql, sp);
        }
        public string GenerateCookieID()
        {
            return GenerateCartID();
        }
        public static string GenerateCartID()
        {
            return "CT" + function.GetRandomString(15).ToLower();
        }
        public static string GetCartID(HttpContext context = null)
        {
            string cartID = "";
            try
            {
                if (context == null) { context = HttpContext.Current; }
                if (context.Request.Cookies["Shopby"] == null || context.Request.Cookies["Shopby"]["OrderNo"] == null)
                    context.Response.Cookies["Shopby"]["OrderNo"] = GenerateCartID();
                cartID = context.Request.Cookies["Shopby"]["OrderNo"];
            }
            catch (Exception ex) { ZLLog.L("GetCartID,购物车ID生成出错,原因:" + ex.Message); }
            return cartID;
        }
        public void U_DelByIDS(string ids, int uid)
        {
            if (string.IsNullOrEmpty(ids) || uid < 1) { return; }
            SafeSC.CheckIDSEx(ids);
            if (string.IsNullOrEmpty(ids)) return;
            string sql = "Delete From " + TbName + " Where ID IN (" + ids + ") And UserID=" + uid;
            SqlHelper.ExecuteNonQuery(CommandType.Text, sql);
        }
        public DataTable U_Sel(int uid)
        {
            string field = "A.*,B.LinPrice,B.Thumbnails,B.ProClass,B.ProUnit";
            string where = "A.UserID=" + uid;
            return DBCenter.JoinQuery(field, TbName, "ZL_Commodities", "A.ProID=B.ID", where);
        }
        public void U_SetNum(int uid, int id, int pronum)
        {
            DBCenter.UpdateSQL(TbName, "ProNum=" + pronum, "UserID=" + uid + " AND ID=" + id);
        }
    }
}
