namespace ZoomLa.BLL
{
    using System;
    using System.Data;
    using ZoomLa.Model;
    using ZoomLa.Common;
    using ZoomLa.SQLDAL;
    using System.Data.SqlClient;
    public class B_S_FloPack
    {
        public B_S_FloPack()
        {
            strTableName = initMod.TbName;
            PK = initMod.PK;
        }
        private string strTableName, PK;
        private M_S_FloPack initMod = new M_S_FloPack();
        public DataTable Sel(int ID)
        {
            return Sql.Sel(strTableName, PK, ID);
        }
        public M_S_FloPack GetSelect(int ID)
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
        public DataTable Select_All()
        {
            return Sql.Sel(strTableName);
        }
        public bool GetUpdate(M_S_FloPack model)
        {
            return Sql.UpdateByIDs(strTableName, PK, model.ID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }
        public bool DeleteByGroupID(int ID)
        {
            return Sql.Del(strTableName, ID);
        }
        public int GetInsert(M_S_FloPack model)
        {
            return Sql.insert(strTableName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        public bool DelByIds(string ids)
        {
            SafeSC.CheckIDSEx(ids);
            return SqlHelper.ExecuteSql("DELETE FROM [dbo].[ZL_S_FloPack] WHERE [ID] IN (" + ids + ")");
        }
        /// <summary>
        /// 根据用户ID查询包装
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <returns></returns>
        public DataTable GetSelects(int userid)
        {
            string sqlStr = "SELECT * FROM ZL_S_FloPack WHERE UserID=@UserID";
            SqlParameter[] para = new SqlParameter[]{
                new SqlParameter("@UserID",userid)
            };
            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, para);
        }
    }
}