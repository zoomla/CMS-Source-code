namespace ZoomLa.BLL
{
    using System;
    using System.Data;
    using ZoomLa.Model;
    using ZoomLa.Common;
    using ZoomLa.SQLDAL;
    using System.Data.SqlClient;

    /// <summary>
    /// Paypal
    /// </summary>
    public class B_Defray
    {
        public string strTableName ,PK;
        private M_Defray initMod = new M_Defray();
        public B_Defray()
        {
            strTableName = initMod.TbName;
            PK = initMod.PK;
        }

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
        public M_Defray SelReturnModel(int ID)
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
        public DataTable Sel()
        {
            return Sql.Sel(strTableName);
        }
        /// <summary>
        /// 根据ID更新
        /// </summary>
        public bool UpdateByID(M_Defray model)
        {
            return Sql.UpdateByIDs(strTableName, PK, model.flow.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }

        public bool Update(string strSet, string strWhere)
        {
            return Sql.Update(strTableName, strSet, strWhere, null);
        }

        public bool Del(int ID)
        {
            return Sql.Del(strTableName, ID);
        }
        public int insert(M_Defray model)
        {
            return Sql.insert(strTableName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        //private static readonly ID_Defray dal = IDal.CreateDefray();
        /// <summary>
        ///添加记录
        /// </summary>
        /// <param name="Defray"></param>
        /// <returns></returns>
        public int GetInsert(M_Defray model)
        {
            return insert(model);
        }

        /// <summary>
        ///更新记录
        /// </summary>
        /// <param name="Defray"></param>
        /// <returns></returns>
        public bool GetUpdate(M_Defray model)
        {
            return UpdateByID(model);
        }

        /// <summary>
        ///不存在则添加否则更新
        /// </summary>
        /// <param name="Defray"></param>
        /// <returns></returns>
        public bool InsertUpdate(M_Defray model)
        {
            if (model.flow > 0)
                UpdateByID(model);
            else
                insert(model);
            return true;
        }

        /// <summary>
        ///删除记录
        /// </summary>
        /// <param name="Defray"></param>
        /// <returns></returns>
        public bool GetDelete(int ID)
        {
            return Sql.Del(strTableName, PK + "=" + ID);
        }

        /// <summary>
        ///查找一条记录
        /// </summary>
        /// <param name="Defray"></param>
        /// <returns></returns>
        public M_Defray GetSelect(int ID)
        {
            return SelReturnModel(ID);
        }

        /// <summary>
        ///返回所有记录
        /// </summary>
        /// <returns></returns>
        public DataTable Select_All()
        {
            return Sel();
        }

    }
}
