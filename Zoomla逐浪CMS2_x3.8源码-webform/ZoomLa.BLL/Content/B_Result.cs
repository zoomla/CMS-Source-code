namespace ZoomLa.BLL
{
    using System;
    using System.Data;
    using ZoomLa.Model;
    using ZoomLa.Common;
    using ZoomLa.SQLDAL;
    using System.Data.SqlClient;
    using SQLDAL.SQL;
    public class B_Result
    {
        public B_Result()
        {
            strTableName = initmod.TbName;
            PK = initmod.PK;
        }
        private string PK, strTableName;
        private M_Result initmod = new M_Result();
        public DataTable Sel(int ID)
        {
            return Sql.Sel(strTableName, PK, ID);
        }
        public PageSetting SelPage(int cpage, int psize)
        {
            PageSetting setting = PageSetting.Single(cpage, psize, strTableName, PK, "");
            DBCenter.SelPage(setting);
            return setting;
        }
        public M_Result SelReturnModel(int ID)
        {
            using (SqlDataReader reader = Sql.SelReturnReader(strTableName, PK, ID))
            {
                if (reader.Read())
                {
                    return initmod.GetModelFromReader(reader);
                }
                else
                {
                    return null;
                }
            }
        }
        private M_Result SelReturnModel(string strWhere)
        {
            using (SqlDataReader reader = Sql.SelReturnReader(strTableName, strWhere))
            {
                if (reader.Read())
                {
                    return initmod.GetModelFromReader(reader);
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
        public bool UpdateByID(M_Result model)
        {
            return Sql.UpdateByIDs(strTableName, PK, model.ID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }
        public bool Del(int ID)
        {
            return Sql.Del(strTableName, ID);
        }
        public int insert(M_Result model)
        {
            return Sql.insert(strTableName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        /// <summary>
        ///添加记录
        /// </summary>
        /// <param name="Result"></param>
        /// <returns></returns>
        public bool GetInsert(M_Result model)
        {
            return insert(model)>0;
        }

        /// <summary>
        ///更新记录
        /// </summary>
        /// <param name="Result"></param>
        /// <returns></returns>
        public bool GetUpdate(M_Result model)
        {
            return UpdateByID(model);
        }

        /// <summary>
        ///不存在则添加否则更新
        /// </summary>
        /// <param name="Result"></param>
        /// <returns></returns>
        public bool InsertUpdate(M_Result model)
        {
            if (model.ID > 0)
                UpdateByID(model);
            else
                insert(model);
            return true;
        }
        /// <summary>
        ///删除记录
        /// </summary>
        /// <param name="Result"></param>
        /// <returns></returns>
        public bool GetDelete(int ID)
        {
            return Sql.Del(strTableName, PK + "=" + ID);
        }
        public M_Result GetSelect(int ID)
        {
            return SelReturnModel(ID);
        }
        public DataTable SelByProID(int pid)
        {
            string sql = "SELECT * FROM " + strTableName + " WHERE ProblemID=" + pid + " ORDER BY ResultTime DESC";
            return SqlHelper.ExecuteTable(CommandType.Text, sql);
        }
        /// <summary>
        ///返回所有记录
        /// </summary>
        /// <returns></returns>
        public DataTable Select_All()
        {
            return Sel();
        }
        /// <summary>
        ///页面记录
        /// </summary>
        /// <returns></returns>
        public DataTable Select_Paged()
        {
            return Sel();
        }
    }
}