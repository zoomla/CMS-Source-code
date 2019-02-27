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
                                         /// B_UserDay 的摘要说明
                                         /// </summary>
    public class B_UserDay
    {
        public string strTableName = "";
        public string PK = "";
        public DataTable dt = null;
        M_UserDay initMod = new M_UserDay();
        public B_UserDay()
        {
            strTableName = initMod.TbName;
            PK = initMod.PK;
        }
        /// <summary>
        ///添加记录
        /// </summary>
        /// <param name="UserDay"></param>
        /// <returns></returns>
        public int GetInsert(M_UserDay model)
        {
            return Sql.insert(strTableName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }

        /// <summary>
        ///更新记录
        /// </summary>
        /// <param name="UserDay"></param>
        /// <returns></returns>
        public bool GetUpdate(M_UserDay model)
        {
            string sqlStr = "UPDATE [dbo].[ZL_UserDay] SET [D_name] = @D_name,[D_date] = @D_date,[D_Content] = @D_Content,[D_UserID] = @D_UserID,[D_mail] = @D_mail,[D_mobile] = @D_mobile,[D_SendNum] = @D_SendNum WHERE [id] = @id";
            SqlParameter[] cmdParams = model.GetParameters();
            return SqlHelper.ExecuteSql(sqlStr, cmdParams);
        }

        /// <summary>
        ///删除记录
        /// </summary>
        /// <param name="UserDay"></param>
        /// <returns></returns>
        public bool DeleteByGroupID(int UserDayID)
        {
            string sqlStr = "DELETE FROM [dbo].[ZL_UserDay] WHERE [id] = @id";
            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@id", SqlDbType.Int, 4);
            cmdParams[0].Value = UserDayID;
            return SqlHelper.ExecuteSql(sqlStr, cmdParams);
        }

        /// <summary>
        ///查找一条记录
        /// </summary>
        /// <param name="UserDay"></param>
        /// <returns></returns>
        public M_UserDay GetSelect(int UserDayID)
        {
            string sqlStr = "SELECT [id],[D_name],[D_date],[D_Content],[D_UserID],[D_mail],[D_mobile],[D_SendNum] FROM [dbo].[ZL_UserDay] WHERE [id] = @id";
            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@id", SqlDbType.Int, 4);
            cmdParams[0].Value = UserDayID;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, sqlStr, cmdParams))
            {
                if (reader.Read())
                {
                    return initMod.GetModelFromReader(reader);
                }
                else
                {
                    return new M_UserDay();
                }
            }
        }

        /// <summary>
        ///返回所有记录
        /// </summary>
        /// <returns></returns>
        public DataTable Select_All()
        {
            return Sql.Sel(strTableName);
        }
        public PageSetting SelPage(int cpage, int psize, int userid = -100)
        {
            string where = " 1=1";
            if (userid != -100) { where += " AND D_UserID=" + userid; }
            PageSetting setting = PageSetting.Single(cpage, psize, strTableName, PK, where);
            DBCenter.SelPage(setting);
            return setting;
        }

        /// <summary>
        /// 根据用户ID查询节日
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public DataTable Select_All(int userID)
        {
            string sqlStr = "SELECT [id],[D_name],[D_date],[D_Content],[D_UserID],[D_mail],[D_mobile],[D_SendNum] FROM [dbo].[ZL_UserDay] where D_UserID=@userID";
            SqlParameter[] parameter = new SqlParameter[] {
            new SqlParameter("@UserID",SqlDbType.Int,4)
            };
            parameter[0].Value = userID;

            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, parameter);
        }

        /// <summary>
        /// 查询日期内指定会员节日列表
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="topdate">起点时间</param>
        /// <param name="spandate">选择时间</param>
        /// <returns></returns>
        public DataTable Select_All(int userID, DateTime topdate, string spandate)
        {
            if (spandate != null && spandate != "")
            {
                string sqlstr = "";
                sqlstr = " and DateDiff(d," + topdate + ",getdate())";
                if (spandate.IndexOf(',') > -1)
                {
                    sqlstr = "<=1";
                }
                else
                {
                    sqlstr = "=" + sqlstr;
                }

                string sqlStr = "SELECT [id],[D_name],[D_date],[D_Content],[D_UserID],[D_mail],[D_mobile],[D_SendNum] FROM [dbo].[ZL_UserDay] where D_UserID=@userID" + sqlstr;
                SqlParameter[] parameter = new SqlParameter[] {
            new SqlParameter("@UserID",SqlDbType.Int,4)
            };
                parameter[0].Value = userID;

                return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, parameter);
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 查询日期内节日列表
        /// </summary>
        /// <param name="topdate">起点时间</param>
        /// <param name="spandate">选择时间</param>
        /// <returns></returns>
        public DataTable Select_All(DateTime topdate, string spandate)
        {
            if (spandate != null && spandate != "")
            {
                string sqlstr = "";
                sqlstr = " DateDiff(d,'" + topdate + "',D_date)";
                if (spandate.IndexOf(',') > -1)
                {
                    string[] selnd = spandate.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    if (selnd.Length > 1)
                    {
                        sqlstr += ">=0 and DateDiff(d,'" + topdate + "',D_date)<2";
                    }
                    else
                    {
                        sqlstr = sqlstr + "=" + selnd[0];
                    }
                }
                string sqlStr = "SELECT [id],[D_name],[D_date],[D_Content],[D_UserID],[D_mail],[D_mobile],[D_SendNum] FROM [dbo].[ZL_UserDay] where" + sqlstr;

                return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, null);
            }
            else
            {
                return null;
            }
        }

        public DataTable Select_All(DateTime topdate)
        {
            return Sql.Sel(strTableName, " DateDiff(d,'" + topdate + "',D_date)=0", "");
        }
    }
}
