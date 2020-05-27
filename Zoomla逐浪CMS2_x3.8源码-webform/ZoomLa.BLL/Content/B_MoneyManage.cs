namespace ZoomLa.BLL
{
    using System;
    using System.Data;
    using ZoomLa.Model;
    using ZoomLa.Common;
    using ZoomLa.SQLDAL;
    using System.Data.SqlClient;
    using SQLDAL.SQL;    /*
     * 支付币种,用于支付单
     */
    public class B_MoneyManage
    {
        private string TbName, PK;
        private M_MoneyManage initMod = new M_MoneyManage();
        public B_MoneyManage()
        {
            TbName = initMod.TbName;
            PK = initMod.PK;
        }
        public DataTable Sel(int ID)
        {
            return Sql.Sel(TbName, PK, ID);
        }
        public M_MoneyManage SelReturnModel(int ID)
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
        //返回默认汇率
        public M_MoneyManage SelDefModel()
        {
            using (SqlDataReader reader = Sql.SelReturnReader(TbName, " WHERE is_flag=1"))
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
            return Sql.Sel(TbName,"","is_flag DESC");
        }
        /// <summary>
        /// 排序
        /// </summary>
        public DataTable Sel(string strWhere, string strOrderby)
        {
            return Sql.Sel(TbName, strWhere, strOrderby);
        }
        public PageSetting SelPage(int cpage, int psize)
        {
            PageSetting setting = PageSetting.Single(cpage, psize, TbName, PK, "");
            DBCenter.SelPage(setting);
            return setting;
        }
        public bool UpdateByID(M_MoneyManage model)
        {
            return Sql.UpdateByIDs(TbName, PK, model.Flow.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }
        /// <summary>
        /// 取消所有默认
        /// </summary>
        public void CancelDef()
        {
            string sql = "UPDATE " + TbName + " SET is_flag=0";
            SqlHelper.ExecuteSql(sql);
        }
        public bool GetDelete(int ID)
        {
            return Sql.Delall(TbName, PK, ID.ToString());
        }
        public bool Del(string strWhere)
        {
            return Sql.Del(TbName, strWhere);
        }
        public int insert(M_MoneyManage model)
        {
            return Sql.insertID(TbName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        public bool DeleteByIds(string ids)
        {
            SafeSC.CheckIDSEx(ids);
            return SqlHelper.ExecuteSql("Delete From " + TbName + " Where Flow IN (" + ids + ")");
        }
        /// <summary>
        /// 英磅支付
        /// </summary>
        /// <param name="flow"></param>
        /// <returns></returns>
        public DataSet GetMoney(string money_code)
        {
            SqlParameter[] cmdParams = new SqlParameter[] { new SqlParameter("@money_code", SqlDbType.VarChar, 4) };
            cmdParams[0].Value = money_code;
            string strSql = "select * from ZL_MoneyManage where money_code=@money_code ";
            return SqlHelper.ExecuteDataSet(CommandType.Text, strSql, cmdParams);
        }
    }
}