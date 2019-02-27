namespace ZoomLa.BLL
{
    using System;
    using System.Data;
    using ZoomLa.Model;
    using ZoomLa.Common;
    using ZoomLa.SQLDAL;
    using System.Data.SqlClient;
    using SQLDAL.SQL;
    public class B_GroupBuyList
    {
        private string strTableName, PK;
        private M_GroupBuyList initMod = new M_GroupBuyList();
        public B_GroupBuyList() 
        {
            strTableName = initMod.TbName;
            PK = initMod.PK;
        }
        public DataTable Sel(int ID)
        {
            return Sql.Sel(strTableName, PK, ID);
        }
        public M_GroupBuyList SelReturnModel(int ID)
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
        private M_GroupBuyList SelReturnModel(string strWhere)
        {
            using (SqlDataReader reader = Sql.SelReturnReader(strTableName, strWhere))
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
            return Sql.Sel(strTableName);
        }
        public PageSetting SelPage(int cpage, int psize)
        {
            PageSetting setting = PageSetting.Single(cpage, psize, strTableName, PK, "");
            DBCenter.SelPage(setting);
            return setting;
        }
        public bool UpdateByID(M_GroupBuyList model)
        {
            return Sql.UpdateByIDs(strTableName, PK, model.id.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }
        public bool Del(int ID)
        {
            return Sql.Del(strTableName, ID);
        }
        public int insert(M_GroupBuyList model)
        {
            return Sql.insert(strTableName, initMod.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        public int GetInsert(M_GroupBuyList groupBuyList)
        {
            //return dal.GetInsert(groupBuyList);
            string sqlStr = "INSERT INTO [dbo].[ZL_GroupBuyList] ([UserID],[ProID],[Deposit],[DepositTime],[isbuy],[buytime],[buymoney],[Btime],[PayID],[DepositPayID],[Snum],[OrderID],[IsReceipt]) VALUES (@UserID,@ProID,@Deposit,@DepositTime,@isbuy,@buytime,@buymoney,@Btime,@PayID,@DepositPayID,@Snum,@OrderID,@IsReceipt);select  @@IDENTITY";
            SqlParameter[] cmdParams = groupBuyList.GetParameters();
            return SqlHelper.ObjectToInt32(SqlHelper.ExecuteScalar(CommandType.Text, sqlStr, cmdParams));
    
        }
        public bool GetUpdate(M_GroupBuyList groupBuyList)
        {
            //return dal.GetUpdate(groupBuyList);
            string sqlStr = "UPDATE [dbo].[ZL_GroupBuyList] SET [UserID] = @UserID,[ProID] = @ProID,[Deposit] = @Deposit,[DepositTime] = @DepositTime,[isbuy] = @isbuy,[buytime] = @buytime,[buymoney] = @buymoney,[Btime] = @Btime,[PayID]=@PayID,[DepositPayID]=@DepositPayID,[Snum]=@Snum,[OrderID]=@OrderID,[IsReceipt]=@IsReceipt WHERE [id] = @id";
            SqlParameter[] cmdParams = groupBuyList.GetParameters();
            return SqlHelper.ExecuteSql(sqlStr, cmdParams);
       
        }
        public bool DeleteByGroupID(int GroupBuyListID)
        {
            //return dal.DeleteByGroupID(GroupBuyListID);
            string sqlStr = "DELETE FROM [dbo].[ZL_GroupBuyList] WHERE [id] = @id";
            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@id", SqlDbType.Int, 4);
            cmdParams[0].Value = GroupBuyListID;
            return SqlHelper.ExecuteSql(sqlStr, cmdParams);
   
        }
        public M_GroupBuyList GetSelect(int GroupBuyListID)
        {
            //return dal.GetSelect(GroupBuyListID);
            string sqlStr = "SELECT * FROM [dbo].[ZL_GroupBuyList] WHERE [id] = @id";
            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@id", SqlDbType.Int, 4);
            cmdParams[0].Value = GroupBuyListID;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, sqlStr, cmdParams))
            {
                if (reader.Read())
                {
                    return initMod.GetModelFromReader(reader);
                }
                else
                {
                    return new M_GroupBuyList();
                }
            }
    
        }
        public DataTable Select_All()
        {
           // return dal.Select_All();
            string sqlStr = "SELECT * FROM [dbo].[ZL_GroupBuyList] ";
            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, null);
   
        }
        /// <summary>
        /// 根据商品ID查找团定列表
        /// </summary>
        /// <param name="ProID"></param>
        /// <returns></returns>
        public DataTable SelectGroupByProID(int ProID)
        {
            //return dal.SelectGroupByProID(ProID);
            string sqlStr = "SELECT * FROM [dbo].[ZL_GroupBuyList] where ProID=" + ProID + " and Deposit>0 order by IsReceipt,Btime desc";

            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, null);
      
        }

        /// <summary>
        /// 根据商品ID查找团定列表
        /// </summary>
        /// <param name="ProID"></param>
        /// <returns></returns>
        public DataTable SelectGroupByProUserID(int UserID)
        {
            //return dal.SelectGroupByUserID(UserID);
            string sqlStr = "SELECT * FROM [dbo].[ZL_GroupBuyList] where UserID=" + UserID + " order by IsReceipt,Btime desc";
            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, null);
        }

        /// <summary>
        /// 查询已付订金或未付订金的信息
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <param name="desp">true为已付,false为未付</param>
        /// <returns></returns>
        public DataTable SelectGroupByDes(int UserID, bool desp)
        {
            //return dal.SelectGroupByDes(UserID, desp);
            string sqlStr = "SELECT * FROM [dbo].[ZL_GroupBuyList] where UserID=" + UserID;
            if (desp)
            {
                sqlStr += " AND DepositPayID IN (SELECT PaymentID FROM ZL_Payment WHERE Status=3 OR Status=5)";
            }
            else
            {
                sqlStr += " AND DepositPayID NOT IN (SELECT PaymentID FROM ZL_Payment WHERE Status=3 OR Status=5)";
            }
            sqlStr += " order by IsReceipt,Btime desc";
            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, null);
        }

        /// <summary>
        /// 根据PID查询购买金额总价
        /// </summary>
        /// <returns></returns>
        public object GetBuymoney(int proid)
        {
            //return dal.GetBuymoney(proid);
            string sqlStr = "select sum(buymoney) as buymoney from ZL_GROUPBUYLIST where [proid]=@proid";
            SqlParameter[] para = new SqlParameter[]
            {
                new SqlParameter("@proid",proid)
            };
            return SqlHelper.ExecuteScalar(CommandType.Text, sqlStr, para);
        }
        /// <summary>
        /// 根据PID查询定金金额总价
        /// </summary>
        /// <returns></returns>
        public object GetDeposit(int proid)
        {
            //return dal.GetDeposit(proid);
            string sqlStr = "select sum(deposit) as deposit from ZL_GROUPBUYLIST where [proid]=@proid";
            SqlParameter[] para = new SqlParameter[]
            {
                new SqlParameter("@proid",proid)
            };
            return SqlHelper.ExecuteScalar(CommandType.Text, sqlStr, para);
        }
        public int insert_mod(M_GroupBuyList m_Announc)
        {
            string sqlStr = "PR_City_Add";
            SqlParameter[] cmdParams = m_Announc.GetParameters();
            return SqlHelper.ObjectToInt32(SqlHelper.ExecuteScalar(CommandType.StoredProcedure, sqlStr, cmdParams));
        }

    }
}
