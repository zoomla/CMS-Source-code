namespace ZoomLa.BLL
{
    using System;
    using System.Data;
    using ZoomLa.Model;
    using ZoomLa.Common;
    using ZoomLa.SQLDAL;
    using System.Data.SqlClient;

    /// <summary>
    /// B_RoomActiveJoin 的摘要说明
    /// </summary>
    public class B_RoomActiveJoin
    {
        public string TbName, PK;
        public M_RoomActiveJoin initMod = new M_RoomActiveJoin();
        public DataTable dt = null;
        public B_RoomActiveJoin()
        {
            TbName = initMod.TbName;
            PK = initMod.PK;
        }

        /// <summary>
        /// 根据ID查询一条记录
        /// </summary>
        public DataTable Sel(int ID)
        {
            return Sql.Sel(TbName, PK, ID);
        }

        /// <summary>
        /// 根据ID查询一条记录
        /// </summary>
        public M_RoomActiveJoin SelReturnModel(int ID)
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
        /// 根据条件查询一条记录
        /// </summary>
        public M_RoomActiveJoin SelReturnModel(string strWhere)
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

        /// <summary>
        /// 查询所有记录
        /// </summary>
        public DataTable Sel()
        {
            return Sql.Sel(TbName);
        }

        /// <summary>
        /// 排序
        /// </summary>
        public DataTable Sel(string strWhere, string strOrderby)
        {
            return Sql.Sel(TbName, strWhere, strOrderby);
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        public DataTable SelPage(string strWhere, string strOrderby, int pageNum, int pageSize)
        {
            return Sql.SelPage(TbName, PK, strWhere, strOrderby, pageNum, pageSize);
        }

        /// <summary>
        /// 根据ID更新
        /// </summary>
        public bool UpdateByID(M_RoomActiveJoin model)
        {
            return Sql.UpdateByIDs(TbName, PK, model.ID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters(model));
        }

        public bool Update(string strSet, string strWhere)
        {
            return Sql.Update(TbName, strSet, strWhere, null);
        }

        public bool Del(int ID)
        {
            return Sql.Del(TbName, ID);
        }

        public bool Del(string strWhere)
        {
            return Sql.Del(TbName, strWhere);
        }

        public int insert(M_RoomActiveJoin model)
        {
            return Sql.insertID(TbName, model.GetParameters(model), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        public DataTable Fonddateno(string strWhere)
        {
            return Sql.Fonddateno(TbName, strWhere);
        }

        /// <summary>
        ///添加记录
        /// </summary>
        /// <param name="RoomActiveJoin"></param>
        /// <returns></returns>
        public bool GetInsert(M_RoomActiveJoin model)
        {
            return Sql.insertID(TbName, model.GetParameters(model), BLLCommon.GetParas(model), BLLCommon.GetFields(model))>0;
        }

        /// <summary>
        ///更新记录
        /// </summary>
        /// <param name="RoomActiveJoin"></param>
        /// <returns></returns>
        public bool GetUpdate(M_RoomActiveJoin model)
        {
            return Sql.UpdateByIDs(TbName, PK, model.ID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters(model));
        }

        /// <summary>
        ///不存在则添加否则更新
        /// </summary>
        /// <param name="RoomActiveJoin"></param>
        /// <returns></returns>
        public bool InsertUpdate(M_RoomActiveJoin model)
        {
            if (model.ID > 0)
                GetUpdate(model);
            else
                GetInsert(model);
            return true;
        }

        /// <summary>
        ///删除记录
        /// </summary>
        /// <param name="RoomActiveJoin"></param>
        /// <returns></returns>
        public bool GetDelete(int ID)
        {
            return Sql.Del(TbName, PK + "=" + ID);
        }

        /// <summary>
        ///查找一条记录
        /// </summary>
        /// <param name="RoomActiveJoin"></param>
        /// <returns></returns>
        public M_RoomActiveJoin GetSelect(int ID)
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
        ///返回所有记录
        /// </summary>
        /// <returns></returns>
        public DataTable Select_All()
        {
            return Sql.Sel(TbName, "", "");
        }

        /// <summary>
        ///按更多的条件查找记录
        /// </summary>
        /// <param name="Selectstr"></param>
        /// <param name="strSQL"></param>
        /// <param name="Orderby"></param>
        /// <returns></returns>
        public DataTable Select_ByValue(string Selectstr, string strSQL, string Orderby)
        {
            string strSql = "select ";
            if (!string.IsNullOrEmpty(Selectstr))
            {
                strSql += Selectstr + " from ";
            }
            strSql += "ZL_RoomActiveJoin";
            if (!string.IsNullOrEmpty(strSQL))
            {
                strSql += " where " + strSQL;
            }
            if (!string.IsNullOrEmpty(Orderby))
            {
                strSql += " Order by " + Orderby;
            }
            return SqlHelper.ExecuteTable(CommandType.Text, strSql, null);
        }
        /// <summary>
        /// 根据用户查询数据
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="active">活动id</param>
        /// <returns></returns>
        public DataTable SelByUid(int uid,int active)
        {
            string sql = "SELECT * FROM " + TbName + " WHERE UserID=" + uid + " AND ActiveID="+active;
            return SqlHelper.ExecuteTable(CommandType.Text, sql);
        }

    }
}
