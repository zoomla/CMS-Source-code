namespace ZoomLa.BLL
{
    using System;
    using System.Data;
    using ZoomLa.Model;
    using ZoomLa.Common;
    using ZoomLa.SQLDAL;
    using System.Data.SqlClient;
    using SQLDAL.SQL;
    /// <summary>
    /// B_UserBank 的摘要说明
    /// </summary>
    public class B_UserBank
    {
        public string TbName, PK;
        public M_UserBank initMod = new M_UserBank();
        public B_UserBank()
        {
            TbName = initMod.TbName;
            PK = initMod.PK;
        }
        #region UserBank 成员
        /// <summary>
        ///添加记录
        /// </summary>
        /// <param name="UserBank"></param>
        /// <returns></returns>
        public int GetInsert(M_UserBank model)
        {
            return DBCenter.Insert(model);
        }

        /// <summary>
        ///更新记录
        /// </summary>
        /// <param name="UserBank"></param>
        /// <returns></returns>
        public bool GetUpdate(M_UserBank model)
        {
            return Sql.UpdateByIDs(TbName, PK, model.Bank_ID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }

        /// <summary>
        ///不存在则添加否则更新
        /// </summary>
        /// <param name="UserBank"></param>
        /// <returns></returns>
        public bool InsertUpdate(M_UserBank userBank)
        {
            DataTable dt = SqlHelper.ExecuteTable(CommandType.Text, "Select * From " + TbName + " Where Bank_ID=" + userBank.Bank_ID);
            if (dt.Rows.Count > 0)
                return GetUpdate(userBank);
            else
            {
                if (GetInsert(userBank) > 0)
                    return true;
                else
                    return false;
            }
        }

        /// <summary>
        ///删除记录
        /// </summary>
        /// <param name="UserBank"></param>
        /// <returns></returns>
        public bool GetDelete(int UserBankID)
        {
            string sqlStr = "Delete From " + TbName + " Where Bank_ID=@UserBankID";
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("UserBankID", UserBankID) };
            return SqlHelper.ExecuteSql(sqlStr, sp);
        }

        /// <summary>
        ///按条件删除记录
        /// </summary>
        /// <param name="strSQL"></param>
        /// <returns></returns>
        public bool Delete_Where(string strSQL)
        {
            return Sql.Del(TbName, strSQL);
        }

        /// <summary>
        ///查找一条记录
        /// </summary>
        /// <param name="UserBank"></param>
        /// <returns></returns>
        public M_UserBank GetSelect(int ID)
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
        ///按条件查找记录
        /// </summary>
        /// <param name="strSQL"></param>
        /// <param name="strSelect"></param>
        /// <param name="Orderby"></param>
        /// <returns></returns>
        public DataTable Select_Where(string strSQL, string Orderby)
        {
            return Sql.Sel(TbName, strSQL, Orderby);
        }

        /// <summary>
        ///返回所有记录
        /// </summary>
        /// <returns></returns>
        public DataTable Select_All()
        {
            return Sql.Sel(TbName);
        }
        public PageSetting SelPage(int cpage, int psize)
        {
            PageSetting setting = PageSetting.Single(cpage, psize, TbName, PK, "");
            DBCenter.SelPage(setting);
            return setting;
        }

        #endregion
    }
}
