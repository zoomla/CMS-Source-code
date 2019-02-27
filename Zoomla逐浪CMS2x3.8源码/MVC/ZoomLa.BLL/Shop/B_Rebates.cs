namespace ZoomLa.BLL
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Collections.Generic;
    using ZoomLa.Model;
    using ZoomLa.Common;
    using ZoomLa.SQLDAL;
    using SQLDAL.SQL;
    /// <summary>
    /// B_Rebates 的摘要说明
    /// </summary>
    public class B_Rebates
    {
        public B_Rebates()
        {
            strTableName = initMod.TbName;
            PK = initMod.PK;
        }
        public string strTableName ,PK;
        private M_Rebates initMod = new M_Rebates();
        /// <summary>
        /// 根据ID查询一条记录
        /// </summary>
        public DataTable Sel(int ID)
        {
            return Sql.Sel(strTableName, PK, ID);
        }

        /// <summary>
        /// 根据ID查询一条记录
        /// </summary>
        public M_Rebates GetSelect(int ID)
        {
            using (SqlDataReader reader = Sql.SelReturnReader(strTableName, PK, ID))
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
        public DataTable Select_All()
        {
            return Sql.Sel(strTableName);
        }
        public PageSetting SelPage(int cpage, int psize)
        {
            PageSetting setting = PageSetting.Single(cpage, psize, strTableName, PK, "");
            DBCenter.SelPage(setting);
            return setting;
        }
        /// <summary>
        /// 根据ID更新
        /// </summary>
        public bool GetUpdate(M_Rebates model)
        {
            return Sql.UpdateByIDs(strTableName, PK, model.ID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }

        public bool Update(string strSet, string strWhere)
        {
            return Sql.Update(strTableName, strSet, strWhere, null);
        }

        public bool DeleteByGroupID(int ID)
        {
            return Sql.Del(strTableName, ID);
        }
        public int GetInsert(M_Rebates model)
        {
            return Sql.insert(strTableName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        ///<summary>
        ///根据SQL语句和参数、查找一条记录
        /// </summary>
        /// <param name="Rebates"></param>
        /// <returns></returns>
        public M_Rebates GetSelectBySqlParams(string strSql, SqlParameter[] param)
        {
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, strSql, param))
            {
                if (reader.Read())
                {
                    return initMod.GetModelFromReader(reader);
                }
                else
                {
                    return new M_Rebates();
                }
            }
        }

        ///<summary>
        /// 分页返回数据集合 
        /// 例：int sumPage=0;
        /// GetSelectAll(1,8,sumPage,"and id=1 and name like '%admin%'","order by id")
        /// </summary>
        /// <param name="page">第几页数</param>
        /// <param name="sumPage">总数据、输出变量</param>
        /// <param name="andWhere">Sql Where语句</param>
        /// <param name="order">Sql order排序</param>
        /// <returns></returns>
        public List<M_Rebates> GetSelectAll(int page, int pageSize, out int sumPage, string andWhere, string order)
        {
            if (pageSize < 0)
            {
                pageSize = 0;
            }
            if (page <= 1)
            {
                page = 0;
            }
            if (andWhere == null)
            {
                andWhere = "";
            }
            string strSumSql = "select count(*) FROM [ZL_Rebates] where 1=1 " + andWhere;
            sumPage = DataConverter.CLng(SqlHelper.ExecuteScalar(CommandType.Text, strSumSql));
            page = sumPage / pageSize < page ? sumPage / pageSize : page;
            string sqlStr = "SELECT   TOP   " + pageSize + "   *   FROM   [ZL_Rebates]  WHERE   (ID   NOT   IN   (SELECT   TOP   " + page * pageSize + "   ID   FROM   [ZL_Rebates] where 1=1 " + andWhere + "   " + order + "))  " + andWhere + "  " + order;

            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, sqlStr, null))
            {
                return GetListInfoFromReader(reader);
            }
        }
        /// <summary>
        /// GetListInfoFromReader
        /// </summary>
        /// <param name="rdr">SqlDataReader</param>
        /// <returns></returns>
        private static List<M_Rebates> GetListInfoFromReader(SqlDataReader rdr)
        {
            List<M_Rebates> list = new List<M_Rebates>();
            while (rdr.Read())
            {
                M_Rebates info = new M_Rebates();
                info.ID = DataConverter.CLng(rdr["ID"].ToString());
                info.UserID = DataConverter.CLng(rdr["UserID"].ToString());
                info.BalanceMoney = DataConverter.CDouble(rdr["BalanceMoney"].ToString());
                info.Scale = DataConverter.CDouble(rdr["Scale"].ToString());
                info.Money = DataConverter.CDouble(rdr["Money"].ToString());
                info.ShopCount = DataConverter.CLng(rdr["ShopCount"].ToString());
                info.OrderID = rdr["OrderID"].ToString();
                list.Add(info);
            }
            rdr.Close();
            return list;
        }

        ///<summary>
        /// 根据sql语句返回list集合 
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public List<M_Rebates> GetSelectList(string sql, SqlParameter[] param)
        {
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, sql, param))
            {
                return GetListInfoFromReader(reader);
            }
        }

        ///<summary>
        ///返回所有记录
        /// </summary>
        /// <returns></returns>
        public DataTable GetSelectTableBySql(string sql, SqlParameter[] param)
        {
            return SqlHelper.ExecuteTable(CommandType.Text, sql, param);
        }

        /// <summary>
        /// 根据PID查询定金金额总价
        /// </summary>
        /// <returns></returns>
        public object GetBalanceMoney(int proid)
        {
            string sqlStr = "select sum(BalanceMoney) as BalanceMoney from ZL_Rebates where [UserID]=@UserID";
            SqlParameter[] para = new SqlParameter[]
            {
                new SqlParameter("@UserID",proid)
            };
            return SqlHelper.ExecuteScalar(CommandType.Text, sqlStr, para);
        }
    }
}