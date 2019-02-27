namespace ZoomLa.BLL
{
    using System;
    using System.Data;
    using ZoomLa.Model;
    using ZoomLa.Common;
    using ZoomLa.SQLDAL;
    using System.Data.SqlClient;

    /// <summary>
    /// B_RedEnvelope 的摘要说明
    /// </summary>
    public class B_RedEnvelope
    {
        public B_RedEnvelope()
        {
            strTableName = initMod.TbName;
            PK = initMod.PK;
        }
        public string strTableName,PK;
        private M_RedEnvelope initMod = new M_RedEnvelope();
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
        public M_RedEnvelope SelReturnModel(int ID)
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
        /// <summary>
        /// 根据ID更新
        /// </summary>
        public bool GetUpdate(M_RedEnvelope model)
        {
            return Sql.UpdateByIDs(strTableName, PK, model.id.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }
        public bool Update(string strSet, string strWhere)
        {
            return Sql.Update(strTableName, strSet, strWhere, null);
        }

        public bool DeleteByGroupID(int ID)
        {
            return Sql.Del(strTableName, ID);
        }
        public int GetInsert(M_RedEnvelope model)
        {
            return Sql.insert(strTableName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        /// <summary>
        ///查找一条记录
        /// </summary>
        /// <param name="RedEnvelope"></param>
        /// <returns></returns>
        public M_RedEnvelope GetSelect(int RedEnvelopeID)
        {
            string sqlStr = "SELECT * FROM [dbo].[ZL_RedEnvelope] WHERE [id] = @id ORDER BY [OrderData] DESC";
            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@id", SqlDbType.Int, 4);
            cmdParams[0].Value = RedEnvelopeID;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, sqlStr, cmdParams))
            {
                if (reader.Read())
                {
                    return initMod.GetModelFromReader(reader);
                }
                else
                {
                    return new M_RedEnvelope();
                }
            }
        }

        /// <summary>
        /// 通过用户查询
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public DataTable GetSelectByUserId(int userid)
        {
            string sqlStr = "SELECT * FROM [dbo].[ZL_RedEnvelope] WHERE Userid=@Userid ORDER BY [OrderData] DESC";
            SqlParameter[] para = new SqlParameter[]{
                new SqlParameter("@Userid",userid)
            };
            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, para);
        }

        /// <summary>
        /// 通过兑现ID查询
        /// </summary>
        /// <param name="rebateid"></param>
        /// <returns></returns>
        public M_RedEnvelope GetSelectByredId(int rebateid)
        {
            string sqlStr = "SELECT * FROM [dbo].[ZL_RedEnvelope] WHERE [RebateId] = @RebateId ORDER BY [OrderData] DESC";
            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@RebateId", SqlDbType.Int, 4);
            cmdParams[0].Value = rebateid;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, sqlStr, cmdParams))
            {
                if (reader.Read())
                {
                    return initMod.GetModelFromReader(reader);
                }
                else
                {
                    return new M_RedEnvelope();
                }
            }
        }
         /// <summary>
        /// 最后申请时间
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public string GetSelelastTimeByUserid(int userid)
        {
            string sqlStr = "SELECT TOP 1 OrderData FROM ZL_RedEnvelope WHERE userid=@userid ORDER BY [OrderData] DESC";
            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@userid", SqlDbType.Int, 4);
            cmdParams[0].Value = userid;
            object obj = SqlHelper.ExecuteScalar(CommandType.Text, sqlStr, cmdParams);
            if (obj != null)
            {
                return obj.ToString();
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 根据条件查询
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="data"></param>
        /// <param name="enddata"></param>
        /// <returns></returns>
        public DataTable GetSeleByCondi(int userid, string data, string enddata)
        {
            string sqlStr = "SELECT * FROM ZL_RedEnvelope WHERE 1=1";
            if (userid > 0)
            {
                sqlStr += " AND Userid =" + userid;
            }
            if (!string.IsNullOrEmpty(data) && string.IsNullOrEmpty(enddata))
            {
                sqlStr += " AND OrderData BETWEEN '" + data + "' AND DATEADD(DAY,1,'" + data + "')";
            }
            if (!string.IsNullOrEmpty(enddata) && string.IsNullOrEmpty(data))
            {
                sqlStr += " AND OrderData BETWEEN '" + enddata + "' AND DATEADD(DAY,1,'" + enddata + "')";
            }
            if (!string.IsNullOrEmpty(data) && !string.IsNullOrEmpty(enddata))
            {
                sqlStr += " AND OrderData BETWEEN '" + data + "' AND '" + enddata + "'";
            }
            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, null);
        }

        /// <summary>
        /// 更新负责人
        /// </summary>
        /// <param name="id"></param>
        /// <param name="adminid"></param>
        /// <returns></returns>
        public bool GetUpdataAdmin(int id, int adminid)
        {
            string sqlStr = "UPDATE ZL_RedEnvelope SET adminid=@adminid WHERE id=@id";
            SqlParameter[] para = new SqlParameter[]{
                new SqlParameter("@adminid",adminid),
                new SqlParameter("@id",id)
            };
            return SqlHelper.ExecuteSql(sqlStr, para);
        }

        /// <summary>
        /// 更新支付状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="paydate"></param>
        /// <param name="paystate"></param>
        /// <returns></returns>
        public bool GetUpdatePaySta(int id, DateTime paydate, int paystate)
        {
            string sqlStr = "UPDATE ZL_RedEnvelope SET payData=@payData , OrderState=@OrderState WHERE id=@id";
            SqlParameter[] para = new SqlParameter[]{
                new SqlParameter("@payData",paydate),
                new SqlParameter("@OrderState",paystate),
                new SqlParameter("@id",id)
            };
            return SqlHelper.ExecuteSql(sqlStr, para);
        }
    }
}
