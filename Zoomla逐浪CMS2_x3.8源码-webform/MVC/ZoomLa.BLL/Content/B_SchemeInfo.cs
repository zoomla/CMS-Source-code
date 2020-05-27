namespace ZoomLa.BLL
{
    using System;
    using System.Data;
    using ZoomLa.Model;
    using ZoomLa.Common;
    using ZoomLa.SQLDAL;
    using System.Data.SqlClient;
    using SQLDAL.SQL;
    public class B_SchemeInfo
    {
        private string TbName, PK;
        private M_SchemeInfo initMod = new M_SchemeInfo();
        public B_SchemeInfo()
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
        public M_SchemeInfo SelReturnModel(int ID)
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
        private M_SchemeInfo SelReturnModel(string strWhere)
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
        public bool UpdateByID(M_SchemeInfo model)
        {
            return Sql.UpdateByIDs(TbName, PK, model.ID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }
        public bool Update(string strSet, string strWhere)
        {
            return Sql.Update(TbName, strSet, strWhere, null);
        }
        public bool Del(int ID)
        {
            return Sql.Del(TbName, ID);
        }
        public int insert(M_SchemeInfo model)
        {
            return Sql.insertID(TbName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        /// <summary>
        ///添加记录
        /// </summary>
        /// <param name="SchemeInfo"></param>
        /// <returns></returns>
        public bool GetInsert(M_SchemeInfo model)
        {
            return Sql.insertID(TbName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model))>0;
        }
        /// <summary>
        ///更新记录
        /// </summary>
        /// <param name="SchemeInfo"></param>
        /// <returns></returns>
        public bool GetUpdate(M_SchemeInfo model)
        {
            return Sql.UpdateByIDs(TbName, PK, model.ID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }
        /// <summary>
        ///不存在则添加否则更新
        /// </summary>
        /// <param name="SchemeInfo"></param>
        /// <returns></returns>
        public bool InsertUpdate(M_SchemeInfo model)
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
        /// <param name="SchemeInfo"></param>
        /// <returns></returns>
        public bool GetDelete(int ID)
        {
            return Sql.Del(TbName, PK + "=" + ID);
        }
        public bool DelBySID(int id)
        {
            return Sql.Del(TbName, "SID=" + id);
        }
        /// <summary>
        ///查找一条记录
        /// </summary>
        /// <param name="SchemeInfo"></param>
        /// <returns></returns>
        public M_SchemeInfo GetSelect(int ID)
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
        /// <summary>
        /// 根据方案ID查询的所有打折信息
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public DataTable SelectAgioList(string sid)
        {
            return Sql.Sel(TbName, " SID=" + sid, " SIAddTime desc"); 
        }
        /// <summary>
        /// 根据方案ID，数量查询相应的打折信息
        /// </summary>
        /// <param name="sid"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        public DataTable SelectAgioList(string sid, int num)
        {
            string sqlStr = "SELECT Top 1 * FROM " + TbName + " WHERE SID=" + sid + " AND SIULimit =" + num + " ORDER BY SIAddTime desc";
            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr);
         }
    }
}