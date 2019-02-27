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
    public class B_Examination
    {
        public B_Examination()
        {
            PK = initmod.PK;
            strTableName = initmod.TbName;
        }
        private string PK, strTableName;
        private M_Examination initmod = new M_Examination();
        public DataTable Sel(int ID)
        {
            return Sql.Sel(strTableName, PK, ID);
        }
        public M_Examination SelReturnModel(int ID)
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
        private M_Examination SelReturnModel(string strWhere)
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
        public bool GetUpdate(M_Examination model)
        {
            return Sql.UpdateByIDs(strTableName, PK, model.ExaID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }
        public bool DeleteByGroupID(int ID)
        {
            return Sql.Del(strTableName, ID);
        }
        public int insert(M_Examination model)
        {
            return Sql.insert(strTableName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        public int GetInsert(M_Examination examination)
        {
            string sqlStr = "INSERT INTO [dbo].[ZL_Examination] ([StuUserName],[Stuid],[Generate],[Starttime],[Endtime],[Qlist],[Total],[Score],[Reviews],[Examiners],[RoomID]) VALUES (@StuUserName,@Stuid,@Generate,@Starttime,@Endtime,@Qlist,@Total,@Score,@Reviews,@Examiners,@RoomID),SET @ExaID = SCOPE_IDENTITY()";
            SqlParameter[] cmdParams = initmod.GetParameters();
            return SqlHelper.ObjectToInt32(SqlHelper.ExecuteScalar(CommandType.Text, sqlStr, cmdParams));
        }
        public M_Examination GetSelect(int ExaminationID)
        {

            string sqlStr = "SELECT [ExaID],[StuUserName],[Stuid],[Generate],[Starttime],[Endtime],[Qlist],[Total],[Score],[Reviews],[Examiners],[RoomID] FROM [dbo].[ZL_Examination] WHERE [ExaID] = @ExaID";
            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@ExaID", SqlDbType.Int, 4);
            cmdParams[0].Value = ExaminationID;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, sqlStr, cmdParams))
            {
                if (reader.Read())
                {
                    return initmod.GetModelFromReader(reader);
                }
                else
                {
                    return new M_Examination();
                }
            }
        }
        public DataTable Select_All()
        {
            string sqlStr = "SELECT [ExaID],[StuUserName],[Stuid],[Generate],[Starttime],[Endtime],[Qlist],[Total],[Score],[Reviews],[Examiners],[RoomID] FROM [dbo].[ZL_Examination]";
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
