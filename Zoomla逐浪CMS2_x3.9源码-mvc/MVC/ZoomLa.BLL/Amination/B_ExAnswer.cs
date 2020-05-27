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
    public class B_ExAnswer
    {
        public string PK, strTableName;
        private M_ExAnswer initmod = new M_ExAnswer();
        public B_ExAnswer()
        {
            PK = initmod.PK;
            strTableName = initmod.TbName;
        }
        public DataTable Sel(int ID)
        {
            return Sql.Sel(strTableName, PK, ID);
        }
        public M_ExAnswer GetSelect(int ID)
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
        private M_ExAnswer SelReturnModel(string strWhere)
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
        public bool GetUpdate(M_ExAnswer model)
        {
            return Sql.UpdateByIDs(strTableName, PK, model.AnswerID.ToString(), model.GetFieldAndPara(), model.GetParameters(model));
        }
        public bool DeleteByGroupID(int ID)
        {
            return Sql.Del(strTableName, ID);
        }
        public int insert(M_ExAnswer model)
        {
            return Sql.insert(strTableName, model.GetParameters(model), model.GetParas(), model.GetFields());
        }
        public int GetInsert(M_ExAnswer exAnswer)
        {
            string sqlStr = "INSERT INTO [dbo].[ZL_ExAnswer] ([ExaID],[Stuid],[Answer],[AnswerTime],[Fraction],[Scores],[Examiners],[RoomID],[StuName]) VALUES (@ExaID,@Stuid,@Answer,@AnswerTime,@Fraction,@Scores,@Examiners,@RoomID,@StuName),SET @AnswerID = SCOPE_IDENTITY()";
            SqlParameter[] cmdParams = initmod.GetParameters(exAnswer);
            return SqlHelper.ObjectToInt32(SqlHelper.ExecuteScalar(CommandType.Text, sqlStr, cmdParams));
        }
        public DataTable Select_All()
        {
            String sqlStr = "SELECT [AnswerID],[ExaID],[Stuid],[Answer],[AnswerTime],[Fraction],[Scores],[Examiners],[RoomID],[StuName] FROM [dbo].[ZL_ExAnswer]";
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
