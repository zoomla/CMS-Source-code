namespace ZoomLa.BLL
{
    using System;
    using System.Data;
    using ZoomLa.Model;
    using ZoomLa.Common;
    using ZoomLa.SQLDAL;
    using System.Data.SqlClient;
    using System.Collections.Generic;
    using SQLDAL.SQL;
    public class B_ExStudytime
    {
        public B_ExStudytime()
        {
            PK = initmod.PK;
            strTableName = initmod.TbName;
        }
        private string PK, strTableName;
        private M_ExStudytime initmod = new M_ExStudytime();
        public DataTable GetSelect(int ID)
        {
            return Sql.Sel(strTableName, PK, ID);
        }
        public M_ExStudytime SelReturnModel(int ID)
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
        private M_ExStudytime SelReturnModel(string strWhere)
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
        public DataTable Select_All()
        {
            return Sql.Sel(strTableName);
        }
        public bool GetUpdate(M_ExStudytime model)
        {
            return Sql.UpdateByIDs(strTableName, PK, model.StudyID.ToString(), model.GetFieldAndPara(), model.GetParameters(model));
        }
        public bool DeleteByGroupID(int ID)
        {
            return Sql.Del(strTableName, ID);
        }
        public int insert(M_ExStudytime model)
        {
            return Sql.insert(strTableName, model.GetParameters(model), model.GetParas(), model.GetFields());
        }
        public int GetInsert(M_ExStudytime exStudytime)
        {
            string sqlStr = "INSERT INTO [dbo].[ZL_ExStudytime] ([Stuid],[Course],[Studytime],[Resttime],[Schedule],[Reviews]) VALUES (@Stuid,@Course,@Studytime,@Resttime,@Schedule,@Reviews);select @@IDENTITY";
            SqlParameter[] cmdParams = initmod.GetParameters(exStudytime);
            return SqlHelper.ObjectToInt32(SqlHelper.ExecuteScalar(CommandType.Text, sqlStr, cmdParams));
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
