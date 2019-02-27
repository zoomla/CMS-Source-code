namespace ZoomLa.SQLDAL
{
    using System;
    using System.Data;
    using ZoomLa.Model;
    using System.Data.SqlClient;
    using ZoomLa.IDAL;
    using ZoomLa.Common;

    /// <summary>
    /// SD_Correct 的摘要说明
    /// </summary>
    public class SD_Correct : ID_Correct
    {
        public SD_Correct()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        #region ID_Correct 成员

        void ID_Correct.Add(M_Correct info)
        {
            string strSql = "PR_Correct_Add";
            SqlParameter[] sp = GetParameters(info);
            SqlHelper.ExecuteProc(strSql, sp);
        }

        void ID_Correct.Delete(int CorrectID)
        {
            string strSql = "PR_Correct_Del";
            SqlParameter[] sp = new SqlParameter[] {
                new SqlParameter("@CorrectID", SqlDbType.Int)
            };
            sp[0].Value = CorrectID;
            SqlHelper.ExecuteProc(strSql, sp);
        }

        DataTable ID_Correct.GetList(string title)
        {
            string strSql = "Select * from ZL_Correct";
            if (!string.IsNullOrEmpty(title))
                strSql = strSql + " where CorrectTitle like '%" + title + "%'";
            return SqlHelper.ExecuteTable(CommandType.Text, strSql, null);
        }

        #endregion
        private static SqlParameter[] GetParameters(M_Correct Info)
        {
            SqlParameter[] sp = new SqlParameter[] {
                new SqlParameter("@CorrectID", SqlDbType.Int),
                new SqlParameter("@CorrectTitle", SqlDbType.NVarChar,255),
                new SqlParameter("@CorrectUrl", SqlDbType.NVarChar,255),
                new SqlParameter("@CorrectType", SqlDbType.Int),
                new SqlParameter("@CorrectDetail", SqlDbType.NVarChar,255),
                new SqlParameter("@CorrectPer", SqlDbType.NVarChar,50),
                new SqlParameter("@CorrectEmail", SqlDbType.NVarChar,50)
            };
            sp[0].Value = Info.CorrectID;
            sp[1].Value = Info.CorrectTitle;
            sp[2].Value = Info.CorrectUrl;
            sp[3].Value = Info.CorrectType;
            sp[4].Value = Info.CorrectDetail;
            sp[5].Value = Info.CorrectPer;
            sp[6].Value = Info.CorrectEmail;
            return sp;
        }
    }
}