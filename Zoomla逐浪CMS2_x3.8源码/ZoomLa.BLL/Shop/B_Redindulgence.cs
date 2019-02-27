namespace ZoomLa.BLL
{
    using System;
    using System.Data;
    using ZoomLa.Model;
    using ZoomLa.Common;
    using ZoomLa.SQLDAL;
    using System.Data.SqlClient;
    public class B_Redindulgence
    {
        public B_Redindulgence()
        {
            strTableName = initMod.TbName;
            PK = initMod.PK;
        }
        private string strTableName, PK;
        private M_Redindulgence initMod = new M_Redindulgence();
        public DataTable Sel(int ID)
        {
            return Sql.Sel(strTableName, PK, ID);
        }
        public M_Redindulgence GetSelect(int ID)
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
        public DataTable Sel()
        {
            return Sql.Sel(strTableName);
        }
        public bool GetUpdate(M_Redindulgence model)
        {
            return Sql.UpdateByIDs(strTableName, PK, model.id.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }
        public bool DeleteByGroupID(int ID)
        {
            return Sql.Del(strTableName, ID);
        }
        public int GetInsert(M_Redindulgence model)
        {
            return Sql.insert(strTableName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
		/// <summary>
		///返回所有记录
		/// </summary>
		/// <returns></returns>
		public DataTable Select_All()
        {
            string sqlStr = "SELECT * FROM [dbo].[ZL_Redindulgence] WHERE Type=0";
            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, null);
        }
        /// <summary>
        /// 链接地址查询
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public M_Redindulgence SelectByUrl(string url)
        {
            string sqlStr = "SELECT * FROM ZL_Redindulgence WHERE url LIKE '%" + url + "'";
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, sqlStr, null))
            {
                if (reader.Read())
                {
                    return initMod.GetModelFromReader(reader);
                }
                else
                {
                    return new M_Redindulgence();
                }
            }
        }
         /// <summary>
        /// 通过用户ID获取
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public DataTable SelectByUserId(int userid)
        {
            string sqlStr = "SELECT * FROM [dbo].[ZL_Redindulgence] WHERE userid=@UserId where Type=0";
            SqlParameter[] para = new SqlParameter[]{
                new SqlParameter("@UserId",userid)
            };
            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, para);
        }
        /// <summary>
        /// 查询用户是否使用的红包函
        /// </summary>
        /// <param name="isUse"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public DataTable SelectByUse(int isUse, int userid)
        {
            string sqlStr = "SELECT * FROM [dbo].[ZL_Redindulgence] WHERE [isUse] = @isUse AND userid=@UserId AND Type=0";
            SqlParameter[] para = new SqlParameter[]{
                new SqlParameter("@isUse",isUse),
                new SqlParameter("@UserId",userid)
             };
            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, para);
        }   
        /// <summary>
        /// 查询用户发送 mail
        /// </summary>
        /// <param name="mail"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public M_Redindulgence GetSelectByMail(string mail, int userid)
        {
            string sqlStr = "SELECT * FROM [dbo].[ZL_Redindulgence] WHERE [mail] = @mail AND userID=@userID and Type=0";
            SqlParameter[] para = new SqlParameter[]{
                new SqlParameter("@mail",mail),
                new SqlParameter("@userID",userid)
            };
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, sqlStr, para))
            {
                if (reader.Read())
                {
                    return initMod.GetModelFromReader(reader);
                }
                else
                {
                    return new M_Redindulgence();
                }
            }
        }
        /// <summary>
        /// 获取红包函数量
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="isUse"></param>
        /// <returns></returns>
        public int GetRedCount(int userid, int isUse)
        {
            string sqlStr = "SELECT COUNT(1) FROM ZL_Redindulgence WHERE userid=@userid AND isUse=@isUse AND Type=0";
            SqlParameter[] para = new SqlParameter[]{
                new SqlParameter("@userid",userid),
                new SqlParameter("@isUse",isUse)
             };
            return SqlHelper.ObjectToInt32(SqlHelper.ExecuteScalar(CommandType.Text, sqlStr, para));
        }
        /// <summary>
        /// 根据条件查询
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <param name="type">类型</param>
        /// <param name="isuse">是否使用</param>
        /// <returns></returns>
        public DataTable select_con(int userid, int type, int isuse)
        {
            string sqlStr = "SELECT * FROM [dbo].[ZL_Redindulgence] WHERE 1=1";
            if (userid > 0)
            {
                sqlStr += " and userid=" + userid;
            }
            if (type > -1)
            {
                sqlStr += " and type =" + type;
            }
            if (isuse > -1)
            {
                sqlStr += "and isUse=" + isuse;
            }
            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, null);
        }
	}
}
