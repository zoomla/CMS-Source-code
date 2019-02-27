namespace ZoomLa.BLL
{
    using System;
    using System.Data;
    using ZoomLa.Model;
    using ZoomLa.Common;
    using ZoomLa.SQLDAL;
    using System.Data.SqlClient;
    using SQLDAL.SQL;
    using System.Collections.Generic;
    public class B_Exroom
    {
        private string strTableName,PK;
        private M_Exroom initMod = new M_Exroom();
        public B_Exroom()
        {
            strTableName = initMod.TbName;
            PK = initMod.PK;
        }
        public DataTable Sel(int ID)
        {
            return Sql.Sel(strTableName, PK, ID);
        }
        public M_Exroom SelReturnModel(int ID)
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
        private M_Exroom SelReturnModel(string strWhere)
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
        public DataTable Select_All()
        {
            return Sql.Sel(strTableName);
        }
        public bool GetUpdate(M_Exroom model)
        {
            return Sql.UpdateByIDs(strTableName, PK, model.ExrID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters(model));
        }

        public bool DeleteByGroupID(int ID)
        {
            return Sql.Del(strTableName, ID);
        }
        public int insert(M_Exroom model)
        {
            return Sql.insert(strTableName, model.GetParameters(model), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        public int GetInsert(M_Exroom exroom)
        {
            string sqlStr = "INSERT INTO [dbo].[ZL_Exroom] ([RoomName],[Starttime],[Endtime],[ExaID],[AddTime],[Stuidlist]) VALUES (@RoomName,@Starttime,@Endtime,@ExaID,@AddTime,@Stuidlist);select @@IDENTITY";
            SqlParameter[] cmdParams = exroom.GetParameters(exroom);
            return SqlHelper.ObjectToInt32(SqlHelper.ExecuteScalar(CommandType.Text, sqlStr, cmdParams));
        }
        public M_Exroom GetSelect(int ExrID)
        {
            string sqlStr = "SELECT [ExrID],[RoomName],[Starttime],[Endtime],[ExaID],[AddTime],[Stuidlist] FROM [dbo].[ZL_Exroom] WHERE [ExrID] = @ExrID";
            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@ExrID", SqlDbType.Int, 4);
            cmdParams[0].Value = ExrID;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, sqlStr, cmdParams))
            {
                if (reader.Read())
                {
                    return initMod.GetModelFromReader(reader);
                }
                else
                {
                    return new M_Exroom();
                }
            }
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