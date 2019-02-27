namespace ZoomLa.BLL
{
    using System;
    using System.Data;
    using System.Configuration;
    using ZoomLa.Model;
    using ZoomLa.SQLDAL;
    using System.Data.SqlClient;
    using ZoomLa.SQLDAL.SQL;
    public class B_Favorite
    {
        private string TbName, PK;
        private M_Favorite initMod = new M_Favorite();
        public B_Favorite()
        {
            TbName = initMod.TbName;
            PK = initMod.PK;
        }
        public DataTable Sel(int ID)
        {
            return Sql.Sel(TbName, PK, ID);
        }
        public M_Favorite SelReturnModel(int ID)
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
        private M_Favorite SelReturnModel(string strWhere)
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
        public bool UpdateByID(M_Favorite model)
        {
            return Sql.UpdateByIDs(TbName, PK, model.FavoriteID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }
        public bool Del(int ID)
        {
            string sql = "DELETE FROM " + TbName + " WHERE FavoriteID=" + ID;
            return SqlHelper.ExecuteSql(sql);
        }
        public bool DelByIDS(string ids, int uid = 0)
        {
            SafeSC.CheckIDSEx(ids);
            string where = PK + " IN (" + ids + ")";
            if (uid > 0) { where += " AND Owner=" + uid; }
            return DBCenter.DelByWhere(TbName, where);
        }
        public int insert(M_Favorite model)
        {
            return Sql.insertID(TbName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        public int insertQues(M_Favorite model)
        {
            DataTable dt = SelBy(model);
            if (dt.Rows.Count <= 0)//判断是否存在
            {
                return insert(model);
            }
            return 0;
        }

        /// <summary>
        /// 添加收藏信息到收藏夹
        /// </summary>
        /// <param name="favorite">收藏信息实例</param>
        public void AddFavorite(M_Favorite model)
        {
            Sql.insertID(TbName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }

        public bool GetFavByUserIDAndFriendID(int Owner, int InfoID)
        {
            string sql = @"SELECT * FROM ZL_Favorite Where ZL_Favorite.Owner=@Owner and ZL_Favorite.InfoID=@InfoID";
            SqlParameter[] parameter = { new SqlParameter("Owner", Owner), new SqlParameter("InfoID", InfoID) };
            M_Favorite mf = new M_Favorite();
            using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, sql, parameter))
            {
                if (dr.Read())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public DataTable SelByType(int uid, int type = -100)
        {
            string where = "Owner=" + uid;
            if (type != -100) { where += " AND FavoriType=" + type; }
            return DBCenter.Sel(TbName, where, PK + " DESC");
        }
        /// <summary>
        /// 根据收藏信息实例ID将信息从收藏夹删除
        /// </summary>
        /// <param name="favoriteid">收藏信息实例ID</param>
        public void DelFavorite(int ID)
        {
            Sql.Del(TbName, PK + "=" + ID);
        }
        /// <summary>
        /// 获取会员的收藏信息列表
        /// MyID - 会员ID
        /// NodeID - 栏目节点ID
        /// keyword - 标题搜索关键字
        /// </summary>
        /// <param name="MyID">会员ID</param>
        /// <param name="NodeID">栏目节点ID</param>
        /// <param name="keyword">标题搜索关键字</param>
        /// <returns>会员的收藏信息列表</returns>
        public DataTable GetMyFavorite(int MyID, int NodeID, string keyword)
        {
            string strSql = "select A.*,B.Title,B.IsCreate from ZL_Favorite A left join ZL_CommonModel B on A.InfoID=B.GeneralID where A.Owner=" + MyID.ToString();
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("keyword", "%" + keyword + "%") };
            string strWhere = "";
            if (NodeID != 0)
                strWhere = strWhere + " and B.NodeID=" + NodeID.ToString();
            if (!string.IsNullOrEmpty(keyword))
                strWhere = strWhere + " and B.Title like @keyword";
            strSql = strSql + strWhere + " order by A.FavoriteDate Desc";
            return SqlHelper.ExecuteTable(CommandType.Text, strSql, sp);
        }
        /// <summary>
        /// 通过用户ID查找用户收藏
        /// </summary>
        public DataTable GetFavByUserID(int UserID)
        {
            DataTable dt = new DataTable();
            string strsql = "select * from ZL_Favorite where Owner=@userID";
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@userID", SqlDbType.Int) };
            para[0].Value = UserID;
            return SqlHelper.ExecuteTable(CommandType.Text, strsql, para);
        }
        /// <summary>
        /// 检测是否已收藏,True:已收藏
        /// </summary>
        public bool CheckFavoData(int uid, int type, int infoid)
        {
            string where = "Owner=" + uid + " AND FavoriType=" + type + " AND InfoID=" + infoid;
            return DBCenter.Count(TbName, where) > 0;
        }
        /// <summary>
        /// 用户ID,类型，关键字搜索
        /// </summary>
        /// <param name="uid">用户ID</param>
        /// <param name="type">类型</param>
        /// <param name="keyword">关键字</param>
        public DataTable GetFavByTidAndUid(int uid, int type, string keyword, int nodeID)
        {
            string sqlStr = "";
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("keyword", "%" + keyword + "%") };
            if (type == 1)  //内容收藏
            {
                sqlStr = "select A.*,B.Title,B.IsCreate from ZL_Favorite A left join ZL_CommonModel B on A.InfoID=B.GeneralID where A.Owner=" + uid.ToString() + " AND a.FavoriType=1";
                if (nodeID != 0)
                {
                    sqlStr += " and B.NodeID=" + nodeID.ToString();
                }
                if (!string.IsNullOrEmpty(keyword))
                {
                    sqlStr += "B.Title like @keyword";
                }
                sqlStr += " order by a.FavoriteDate Desc";
            }
            else if (type == 2)  //商品收藏
            {
                sqlStr = "SELECT a.*,b.proname as title,b.Thumbnails,b.ShiPrice,b.LinPrice,b.Nodeid FROM ZL_Favorite a left join ZL_Commodities b on a.infoID =b.ID WHERE a.FavoriType=2 AND a.Owner=" + uid.ToString();
                if (!string.IsNullOrEmpty(keyword))
                {
                    sqlStr += " AND b.Proname like @keyword";
                }
                if (nodeID != 0)
                {
                    sqlStr += " and B.NodeID=" + nodeID.ToString();
                }
                sqlStr += " order by a.FavoriteDate Desc";
            }
            else  //网店收藏
            {
                sqlStr = "SELECT a.*,b.Proname as title,b.Nodeid,b.Thumbnails,b.ShiPrice,b.LinPrice FROM ZL_Favorite a left join ZL_UserShop b on a.infoID=b.id WHERE a.FavoriType=3 AND Owner=" + uid.ToString();
                if (!string.IsNullOrEmpty(keyword))
                {
                    sqlStr += " AND b.Proname like @keyword";
                }
                if (nodeID != 0)
                {
                    sqlStr += " and B.NodeID=" + nodeID.ToString();
                }
                sqlStr += " order by a.FavoriteDate Desc";
            }
            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, sp);
        }
        public DataTable SelBy(M_Favorite model)
        {
            string strsql = "SELECT * FROM ZL_Favorite WHERE [Owner]=@Owner AND [InfoID]=@InfoID AND [FavoriType]=@FavoriType";
            SqlParameter[] sp = model.GetParameters();
            return SqlHelper.ExecuteTable(CommandType.Text, strsql, sp);
        }
        public void U_Del(int uid, int infoID, int type)
        {
            string where = "Owner=" + uid + " AND InfoID=" + infoID + " AND FavoriType=" + type;
            DBCenter.DelByWhere(TbName, where);
        }
        public static string GetFavType(int type)
        {
            //DataConverter.CLng(Eval("FavoriType"))
            switch (type)
            {
                case 1:
                    return "内容";
                case 2:
                    return "商品";
                case 3:
                    return "网店";
                case 4:
                    return "问题";
                case 5:
                    return "百科";
                default:
                    return "" + type;
            }
        }
        //--------------
        public PageSetting SelPage(int cpage, int psize, int uid, int type = -100)
        {
            string where = "Owner=" + uid;
            if (type != -100) { where += " AND FavoriType=" + type; }
            PageSetting setting = PageSetting.Single(cpage, psize, TbName, PK, where);
            DBCenter.SelPage(setting);
            return setting;
        }
    }
}