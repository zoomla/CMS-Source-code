namespace ZoomLa.BLL
{
    using System;
    using System.Data;
    using ZoomLa.Model;
    using ZoomLa.Common;
    using ZoomLa.SQLDAL;
    using System.Data.SqlClient;
    using SQLDAL.SQL;
    using System.Collections.Generic;    /// <summary>
                                         /// B_AddRessList 的摘要说明
                                         /// </summary>
    public class B_AddRessList
    {
        private M_AddRessList initMod = new M_AddRessList();
        private string PK, strTableName;
        public B_AddRessList()
        {
            PK = initMod.PK;
            strTableName = initMod.TbName;
        }
        public DataTable Sel(int ID)
        {
            return Sql.Sel(strTableName, PK, ID);
        }
        public M_AddRessList SelReturnModel(int ID)
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
        private M_AddRessList SelReturnModel(string strWhere)
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
        public bool UpdateByID(M_AddRessList model)
        {
            return Sql.UpdateByIDs(strTableName, PK, model.ID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }
        public bool Del(int ID)
        {
            return Sql.Del(strTableName, ID);
        }
        public int insert(M_AddRessList model)
        {
            return Sql.insert(strTableName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        /// <summary>
        ///添加记录
        /// </summary>
        /// <param name="AddRessList"></param>
        /// <returns></returns>
        public bool GetInsert(M_AddRessList model)
        {
            return Sql.insertID(strTableName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model)) > 0;
        }

        /// <summary>
        ///更新记录
        /// </summary>
        /// <param name="AddRessList"></param>
        /// <returns></returns>
        public bool GetUpdate(M_AddRessList model)
        {
            return Sql.UpdateByIDs(strTableName, PK, model.ID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }
        /// <summary>
        ///不存在则添加否则更新
        /// </summary>
        /// <param name="AddRessList"></param>
        /// <returns></returns>
        public bool InsertUpdate(M_AddRessList addRessList)
        {
            DataTable dt = Sel();
            dt.DefaultView.RowFilter = "ID=" + addRessList.ID;
            dt = dt.DefaultView.ToTable();
            if (dt.Rows.Count > 0)
                return GetUpdate(addRessList);
            else
                return GetInsert(addRessList);
        }

        /// <summary>
        ///删除记录
        /// </summary>
        /// <param name="AddRessList"></param>
        /// <returns></returns>
        public bool GetDelete(int AddRessListID)
        {
            return Sql.Del(strTableName, AddRessListID);
        }

        /// <summary>
        ///查找一条记录
        /// </summary>
        /// <param name="AddRessList"></param>
        /// <returns></returns>
        public M_AddRessList GetSelect(int AddRessListID)
        {
            using (SqlDataReader reader = Sql.SelReturnReader(strTableName, PK, AddRessListID))
            {
                if (reader.Read())
                {
                    return initMod.GetModelFromReader(reader);
                }
                else
                {
                    return new M_AddRessList();
                }
            }
        }
        /// <summary>
        ///返回所有记录
        /// </summary>
        /// <returns></returns>
        public DataTable Select_All()
        {
            return Sel();
        }
        public PageSetting SelPage(int cpage, int psize)
        {
            string where = "1=1 ";
            PageSetting setting = PageSetting.Single(cpage, psize, strTableName, PK, where);
            DBCenter.SelPage(setting);
            return setting;
        }
    }
}
