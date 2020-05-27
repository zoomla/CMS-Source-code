namespace ZoomLa.BLL
{
    using System;
    using System.Data;
    using ZoomLa.Model;
    using ZoomLa.Common;
    using ZoomLa.SQLDAL;
    using System.Data.SqlClient;
    public class B_DMusic
    {
        private string PK, strTableName;
        private M_DMusic initMod = new M_DMusic();
        public B_DMusic()
        {
            PK = initMod.PK;
            strTableName = initMod.TbName;
        }
        public DataTable Sel(int ID)
        {
            return Sql.Sel(strTableName, PK, ID);
        }
        public M_DMusic GetSelect(int ID)
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
        public bool GetUpdate(M_DMusic model)
        {
            return Sql.UpdateByIDs(strTableName, PK, model.id.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }


        public bool DeleteByGroupID(int ID)
        {
            return Sql.Del(strTableName, ID);
        }

        public int GetInsert(M_DMusic model)
        {
            return Sql.insert(strTableName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        /// <summary>
        /// 选择已启用音乐
        /// </summary>
        /// <returns></returns>
        public DataTable SelectTrueMusic()
        {
            string sqlStr = "SELECT [id],[MusicName],[MusicUrl],[IsTrue],[AddTime] FROM [dbo].[ZL_3DMusic] where IsTrue=1";
            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, null);
        }
    }
}