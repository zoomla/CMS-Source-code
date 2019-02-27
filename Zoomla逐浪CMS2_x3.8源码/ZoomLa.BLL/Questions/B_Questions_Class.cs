namespace ZoomLa.BLL
{
    using System;
    using System.Data;
    using System.Collections.Generic;
    using ZoomLa.Model;
    using ZoomLa.Common;
    using ZoomLa.SQLDAL;
    using System.Data.SqlClient;
    using SQLDAL.SQL;
    /// <summary>
    /// B_Questions_Class 的摘要说明
    /// </summary>
    public class B_Questions_Class
    {
        public B_Questions_Class()
        {
            strTableName = initMod.TbName;
            PK = initMod.PK;
        }
        public string strTableName ="";
        public string PK = "";
        public DataTable dt = null;
        private M_Questions_Class initMod = new M_Questions_Class();

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
        public M_Questions_Class SelReturnModel(int ID)
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
        public bool GetUpdate(M_Questions_Class model)
        {
            return Sql.UpdateByIDs(strTableName, PK, model.C_id.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }

        public bool Update(string strSet, string strWhere)
        {
            return Sql.Update(strTableName, strSet, strWhere, null);
        }

        public bool DeleteByGroupID(int ID)
        {
            return Sql.Del(strTableName, ID);
        }
        public int insert(M_Questions_Class model)
        {
            return Sql.insert(strTableName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        /// <summary>
        ///添加记录
        /// </summary>
        /// <param name="Questions_Class"></param>
        /// <returns></returns>
        public int GetInsert(M_Questions_Class model)
        {
           return DBCenter.Insert(model);
        }

       
      
        /// <summary>
        ///查找一条记录
        /// </summary>
        /// <param name="Questions_Class"></param>
        /// <returns></returns>
        public M_Questions_Class GetSelect(int Questions_ClassID)
        {
            string sqlStr = "SELECT [C_id],[C_ClassName],[C_Classid],[C_OrderBy] FROM [dbo].[ZL_Questions_Class] WHERE [C_id] = @C_id";
            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@C_id", SqlDbType.Int, 4);
            cmdParams[0].Value = Questions_ClassID;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, sqlStr, cmdParams))
            {
                if (reader.Read())
                {
                    return initMod.GetModelFromReader(reader);
                }
                else
                {
                    return new M_Questions_Class();
                }
            }
        }

        /// <summary>
        /// 根据类别名称查询分类记录
        /// </summary>
        /// <param name="C_ClassName"></param>
        /// <returns></returns>
        public M_Questions_Class GetSelectByCName(string C_ClassName,int c_ClassId)
        {
            string sqlStr = "SELECT [C_id],[C_ClassName],[C_Classid],[C_OrderBy] FROM [dbo].[ZL_Questions_Class] WHERE [C_ClassName] = @C_ClassName AND [C_Classid]=@C_Classid order by C_OrderBy";
            SqlParameter[] cmdParams = new SqlParameter[2];
            cmdParams[0] = new SqlParameter("@C_ClassName", SqlDbType.NVarChar, 50);
            cmdParams[0].Value = C_ClassName;
            cmdParams[1] = new SqlParameter("@C_Classid", SqlDbType.Int, 4);
            cmdParams[1].Value = c_ClassId;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, sqlStr, cmdParams))
            {
                if (reader.Read())
                {
                    return initMod.GetModelFromReader(reader);
                }
                else
                {
                    return new M_Questions_Class();
                }
            }
        }

        private List<M_Questions_Class> GetSelectByDt(DataTable dt)
        {
            List<M_Questions_Class> mqc;
            if (dt != null && dt.Rows.Count > 0)
            {
                mqc = new List<M_Questions_Class>();
                foreach (DataRow dr in dt.Rows)
                {
                    M_Questions_Class mq = new M_Questions_Class();
                    mq.C_Classid = DataConverter.CLng(dr["C_ClassId"]);
                    mq.C_id = DataConverter.CLng(dr["C_id"]);
                    mq.C_OrderBy = DataConverter.CLng(dr["C_OrderBy"].ToString());
                    mq.C_ClassName = dr["c_ClassName"].ToString();
                    mqc.Add(mq);
                }
                return mqc;
            }
            else
            {
                return new List<M_Questions_Class>();
            }
        }
        /// <summary>
        /// 查询所有分类
        /// </summary>
        /// <returns>list集合</returns>
        public List<M_Questions_Class> SelectQuesClasses()
        {
            DataTable dt = new DataTable();
            dt = Select_All();
            return GetSelectByDt(dt);
        }

        /// <summary>
        /// 根据所属ID查询分类
        /// </summary>
        /// <param name="C_ClassId"></param>
        /// <returns></returns>
        public DataTable GetSelectByC_ClassId(int C_ClassId)
        {
            string sqlStr = "SELECT [C_id],[C_ClassName],[C_Classid],[C_OrderBy] FROM [dbo].[ZL_Questions_Class] WHERE [C_Classid] =" + C_ClassId + " order by C_OrderBy";
            DataTable dt = new DataTable();
            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, null);
        }

        /// <summary>
        /// 根据所属ID查询分类
        /// </summary>
        /// <param name="C_ClassId"></param>
        /// <returns>list列表</returns>
        public List<M_Questions_Class> GetSelectByClassId(int C_ClassId)
        {
            DataTable dt = GetSelectByC_ClassId(C_ClassId);
            return GetSelectByDt(dt);
        }
    }
}
