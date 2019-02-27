namespace ZoomLa.BLL
{
    using System;
    using System.Data;
    using ZoomLa.Model;
    using ZoomLa.Common;
    using ZoomLa.SQLDAL;
    using System.Data.SqlClient;
    using SQLDAL.SQL;
    public class B_Scheme
    {
        private string TbName, PK;
        private M_Scheme initMod = new M_Scheme();
        public B_Scheme()
        {
            TbName = initMod.TbName;
            PK = initMod.PK;
        }
        public DataTable Sel(int ID)
        {
            return Sql.Sel(TbName, PK, ID);
        }
        public PageSetting SelPage(int cpage, int psize)
        {
            PageSetting setting = PageSetting.Single(cpage, psize, TbName, PK, "");
            DBCenter.SelPage(setting);
            return setting;
        }
        public M_Scheme SelReturnModel(int ID)
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
        private M_Scheme SelReturnModel(string strWhere)
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
        /// 根据ID更新
        /// </summary>
        public bool UpdateByID(M_Scheme model)
        {
            return Sql.UpdateByIDs(TbName, PK, model.ID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }
        public bool Del(int ID)
        {
            return Sql.Del(TbName, ID);
        }
        public int insert(M_Scheme model)
        {
            return Sql.insertID(TbName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        /// <summary>
        ///添加记录
        /// </summary>
        /// <param name="Scheme"></param>
        /// <returns></returns>
        public bool GetInsert(M_Scheme model)
        {
            return Sql.insertID(TbName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model))>0;
        }
        /// <summary>
        ///更新记录
        /// </summary>
        /// <param name="Scheme"></param>
        /// <returns></returns>
        public bool GetUpdate(M_Scheme model)
        {
            return Sql.UpdateByIDs(TbName, PK, model.ID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }
        /// <summary>
        ///不存在则添加否则更新
        /// </summary>
        /// <param name="Scheme"></param>
        /// <returns></returns>
        public bool InsertUpdate(M_Scheme model)
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
        /// <param name="Scheme"></param>
        /// <returns></returns>
        public bool GetDelete(int ID)
        {
            return Sql.Del(TbName, PK + "=" + ID);
        }
        /// <summary>
        ///查找一条记录
        /// </summary>
        /// <param name="Scheme"></param>
        /// <returns></returns>
        public M_Scheme GetSelect(int ID)
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

        public DataTable GetID(string str)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("str", "%" + str + "%") };
            string sql = "Select Top 1 * From "+TbName+" Where SList LIKE @str Order By SAddTime DESC";
            return SqlHelper.ExecuteTable(CommandType.Text,sql,sp);
        }
    }
}