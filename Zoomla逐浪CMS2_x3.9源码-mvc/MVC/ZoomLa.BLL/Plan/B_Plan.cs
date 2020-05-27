namespace ZoomLa.BLL
{
    using System; 
    using ZoomLa.Model;
    using System.Data;
    using ZoomLa.SQLDAL;
    using System.Data.SqlClient;

    public class B_Plan
    {
        public B_Plan() 
        {
            strTableName = initMod.TbName;
            PK = initMod.PK;
        }  
        public string strTableName ,PK;
        private M_Plan initMod = new M_Plan();
       
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
        public M_Plan SelReturnModel(int ID)
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
        public bool UpdateByID(M_Plan model)
        {
            return Sql.UpdateByID(strTableName, PK, model.ID, BLLCommon.GetFieldAndPara(model), model.GetParameters(model));
        }

        public bool Update(string strSet, string strWhere)
        {
            return Sql.Update(strTableName, strSet, strWhere, null);
        }

        public bool Del(int ID)
        {
            return Sql.Del(strTableName, ID);
        }
        /// <summary>
        ///添加记录
        /// </summary>
        /// <param name="Sensitivity"></param>
        /// <returns></returns>

        public int insert(M_Plan model)
        {
            return Sql.insert(strTableName, model.GetParameters(model), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        } 
    }
}