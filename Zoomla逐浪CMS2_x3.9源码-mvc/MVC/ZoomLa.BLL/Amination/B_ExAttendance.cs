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
    public class B_ExAttendance
    {
        public B_ExAttendance()
        {
            PK = initmod.PK;
            strTableName = initmod.TbName;
        }
        private string PK, strTableName;
        private M_ExAttendance initmod = new M_ExAttendance();
        public DataTable Sel(int ID)
        {
            return Sql.Sel(strTableName, PK, ID);
        }
        public M_ExAttendance GetSelect(int ID)
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
        private M_ExAttendance SelReturnModel(string strWhere)
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
        public bool GetUpdate(M_ExAttendance model)
        {
            return Sql.UpdateByIDs(strTableName, PK, model.AttID.ToString(), model.GetFieldAndPara(), model.GetParameters(model));
        }
        public bool DeleteByGroupID(int ID)
        {
            return Sql.Del(strTableName, ID);
        }
        public bool Del(string strWhere)
        {
            return Sql.Del(strTableName, strWhere);
        }
        public int insert(M_ExAttendance model)
        {
            return Sql.insert(strTableName, model.GetParameters(model), model.GetParas(), model.GetFields());
        }
        public int GetInsert(M_ExAttendance exAttendance)
        {
            string sqlStr = "INSERT INTO [dbo].[ZL_ExAttendance] ([Stuid],[LogTime],[Logtimeout],[Location],[StuName]) VALUES (@Stuid,@LogTime,@Logtimeout,@Location,@StuName);SET @AttID = SCOPE_IDENTITY()";
            SqlParameter[] cmdParams = initmod.GetParameters(exAttendance);
            return SqlHelper.ObjectToInt32(SqlHelper.ExecuteScalar(CommandType.Text, sqlStr, cmdParams));
        }
        public DataTable Select_All()
        {
            string sqlStr = "SELECT [AttID],[Stuid],[LogTime],[Logtimeout],[Location],[StuName] FROM [dbo].[ZL_ExAttendance]";
            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, null);
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
