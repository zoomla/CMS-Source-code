namespace ZoomLa.SQLDAL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Text;
    using ZoomLa.IDAL;
    using ZoomLa.Model;
    using System.Data.SqlClient;
    using ZoomLa.Common;

    /// <summary>
    /// SD_Favorite 的摘要说明
    /// </summary>
    public class SD_Favorite : ID_Favorite
    {
        public SD_Favorite()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        #region ID_Favorite 成员

        void ID_Favorite.AddFavorite(M_Favorite favorite)
        {
            string strSql = "PR_Favorite_Add";
            SqlParameter[] cmdParams = GetParameters(favorite);
            SqlHelper.ExecuteProc(strSql, cmdParams);
        }

        void ID_Favorite.DelFavorite(int favoriteid)
        {
            string strSql = "PR_Favorite_Del";
            SqlParameter[] cmdParams = new SqlParameter[] { new SqlParameter("@FavoriteID", SqlDbType.Int) };
            cmdParams[0].Value = favoriteid;
            SqlHelper.ExecuteProc(strSql, cmdParams);
        }

        DataTable ID_Favorite.GetFavoriteList(int owner, int NodeID, string keyword)
        {
            string strSql = "select A.*,B.Title,B.IsCreate from ZL_Favorite A left join ZL_CommonModel B on A.InfoID=B.GeneralID where A.Owner=" + owner.ToString();
            string strWhere = "";
            if (NodeID != 0)
                strWhere = strWhere + " and B.NodeID=" + NodeID.ToString();
            if (!string.IsNullOrEmpty(keyword))
                strWhere = strWhere + " and B.Title like '%" + keyword + "%'";
            strSql = strSql + strWhere + " order by A.FavoriteDate Desc";
            return SqlHelper.ExecuteTable(CommandType.Text, strSql, null);
        }

        #endregion
        private static SqlParameter[] GetParameters(M_Favorite Info)
        {
            SqlParameter[] parameter = new SqlParameter[] { 
                new SqlParameter("@FavoriteID", SqlDbType.Int),
                new SqlParameter("@Owner",SqlDbType.Int),
                new SqlParameter("@InfoID",SqlDbType.Int),
                new SqlParameter("@FavoriteDate",SqlDbType.DateTime)
            };
            parameter[0].Value = Info.FavoriteID;
            parameter[1].Value = Info.Owner;
            parameter[2].Value = Info.InfoID;
            parameter[3].Value = Info.AddDate;
            return parameter;
        }
        private static M_Favorite GetInfoFromReader(SqlDataReader rdr)
        {
            M_Favorite info = new M_Favorite();
            info.FavoriteID = DataConverter.CLng(rdr["FavoriteID"]);
            info.Owner = DataConverter.CLng(rdr["Owner"]);
            info.InfoID = DataConverter.CLng(rdr["InfoID"]);
            info.AddDate = DataConverter.CDate(rdr["FavoriteDate"]);
            rdr.Close();
            return info;
        }
    }
}