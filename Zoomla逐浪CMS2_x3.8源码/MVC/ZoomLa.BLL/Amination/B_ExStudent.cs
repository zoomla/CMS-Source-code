namespace ZoomLa.BLL
{
    using System;
    using System.Data;
    using ZoomLa.Model;
    using ZoomLa.Common;
    using ZoomLa.SQLDAL;
    using System.Data.SqlClient;
    using SQLDAL.SQL;
    using System.Collections.Generic;    /// <summary>
                                         /// B_ExStudent 的摘要说明
                                         /// </summary>
    public class B_ExStudent
    {
        public B_ExStudent()
        {
            PK = initmod.PK;
            strTableName = initmod.TbName;
        }
        private string PK, strTableName;
        private M_ExStudent initmod = new M_ExStudent();
        public DataTable Sel(int ID)
        {
            return Sql.Sel(strTableName, PK, ID);
        }
        public M_ExStudent GetSelect(int ID)
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
        private M_ExStudent SelReturnModel(string strWhere)
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
        public bool GetUpdate(M_ExStudent model)
        {
            return Sql.UpdateByIDs(strTableName, PK, model.Stuid.ToString(), model.GetFieldAndPara(), model.GetParameters(model));
        }
        public bool DeleteByGroupID(int ID)
        {
            return Sql.Del(strTableName, ID);
        }

        public int insert(M_ExStudent model)
        {
            return Sql.insert(strTableName, model.GetParameters(model), model.GetParas(), model.GetFields());
        }
        public int GetInsert(M_ExStudent exStudent)
        {
            string sqlStr = "INSERT INTO [dbo].[ZL_ExStudent] ([Stuname],[Stupassword],[Stucardno],[Userid],[Exptime],[Lognum],[Examnum],[Logtimeout],[Stugroup],[Regulation],[strCompetence],[Course],[Qualified],[Addtime]) VALUES (@Stuname,@Stupassword,@Stucardno,@Userid,@Exptime,@Lognum,@Examnum,@Logtimeout,@Stugroup,@Regulation,@strCompetence,@Course,@Qualified,@Addtime);select @@IDENTITY";
            SqlParameter[] cmdParams = initmod.GetParameters(exStudent);
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
