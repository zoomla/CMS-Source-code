namespace ZoomLa.SQLDAL
{
    using System;
    using System.Data;
    using ZoomLa.Model;
    using System.Data.SqlClient;
    using ZoomLa.IDAL;
    using ZoomLa.Common;

    /// <summary>
    /// 评论信息
    /// </summary>
    public class SD_Comment : ID_Comment
    {
        public SD_Comment()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }        
        /// <summary>
        /// 传参
        /// </summary>
        /// <param name="userGroup"></param>
        /// <returns>返回SqlParameter[]</returns>
        private static SqlParameter[] GetParameters(M_Comment commentInfo)
        {
            SqlParameter[] parameter = new SqlParameter[9];
            parameter[0] = new SqlParameter("@CommentID", SqlDbType.Int);   //有时候不需要用这条属性，值为 null 时，会不会报错？比如 Add 添加评论
            parameter[0].Value = commentInfo.CommentID;
            parameter[1] = new SqlParameter("@GeneralID", SqlDbType.Int);
            parameter[1].Value = commentInfo.GeneralID;
            parameter[2] = new SqlParameter("@Title", SqlDbType.NVarChar, 255);
            parameter[2].Value = commentInfo.Title;
            parameter[3] = new SqlParameter("@Contents", SqlDbType.NVarChar, 300);
            parameter[3].Value = commentInfo.Contents;
            parameter[4] = new SqlParameter("@Audited", SqlDbType.Bit);
            parameter[4].Value = commentInfo.Audited;
            parameter[5] = new SqlParameter("@UserID", SqlDbType.Int);
            parameter[5].Value = commentInfo.UserID;
            parameter[6] = new SqlParameter("@CommentTime", SqlDbType.DateTime);
            parameter[6].Value = commentInfo.CommentTime;
            parameter[7] = new SqlParameter("@Score", SqlDbType.Int);
            parameter[7].Value = commentInfo.Score;
            parameter[8] = new SqlParameter("@PK", SqlDbType.Bit);
            parameter[8].Value = commentInfo.PK;
            return parameter;
        }
        private static M_Comment GetInfoFromReader(SqlDataReader rdr)
        {
            M_Comment info = new M_Comment();
            info.CommentID = DataConverter.CLng(rdr["CommentID"]);
            info.GeneralID = DataConverter.CLng(rdr["GeneralID"]);
            info.Title = rdr["Title"].ToString();
            info.Contents = rdr["Contents"].ToString();
            info.Audited = DataConverter.CBool(rdr["Audited"].ToString()); ;
            info.UserID = DataConverter.CLng(rdr["UserID"]);
            info.CommentTime = DataConverter.CDate(rdr["CommentTime"]);
            info.Score = DataConverter.CLng(rdr["Score"]);
            info.PK = DataConverter.CBool(rdr["PK"].ToString());
            return info;
        }
        
        #region ID_Comment 成员
        /// <summary>
        /// 发表(增加)评论
        /// </summary>
        /// <param name=commentInfo"></param>
        /// <returns>返回True/False</returns>
        bool ID_Comment.Add(M_Comment commentInfo)
        {
            string sqlStr = "PR_Comments_Add";
            SqlParameter[] parameter = GetParameters(commentInfo);
            return SqlHelper.ExecuteProc(sqlStr, parameter);
        }
        /// <summary>
        /// 审核指定ID的评论
        /// </summary>
        /// <param name="commentID"></param>
        /// <returns></returns>
        bool ID_Comment.AuditedByID(int commentID,bool Audited)
        {
            string strSql = "PR_Comments_Audit";
            SqlParameter[] sp = new SqlParameter[] {
                new SqlParameter("@CommentID",SqlDbType.Int),
                new SqlParameter("@Audit",SqlDbType.Bit)
            };
            sp[0].Value = commentID;
            sp[1].Value = Audited;
            return SqlHelper.ExecuteProc(strSql, sp);
        }
        /// <summary>
        /// 删除指定评论
        /// </summary>
        /// <param name="commentID"></param>
        /// <returns></returns>
        bool ID_Comment.Delete(int commentID)
        {
            string strSql = "PR_Comments_Del";
            SqlParameter[] sp = new SqlParameter[] {
                new SqlParameter("@CommentID",SqlDbType.Int)
            };
            sp[0].Value = commentID;
            return SqlHelper.ExecuteProc(strSql, sp);
        }
        /// <summary>
        /// 查询节点的所有评论
        /// </summary>
        /// <returns>返回DataTable</returns>
        DataTable ID_Comment.SeachCommentAll(int nodeID)
        {
            string sqlStr = "select ZL_Comment.*,ZL_CommonModel.Title from ZL_Comment left join ZL_CommonModel on ZL_Comment.GeneralID=ZL_CommonModel.GeneralID where ZL_CommonModel.NodeID = " + nodeID.ToString();
            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, null);
        }
        /// <summary>
        /// 查询节点的所有评论
        /// </summary>
        /// <returns>返回DataTable</returns>
        DataTable ID_Comment.SeachComment()
        {
            string sqlStr = "select ZL_Comment.*,ZL_CommonModel.Title from ZL_Comment left join ZL_CommonModel on ZL_Comment.GeneralID=ZL_CommonModel.GeneralID";
            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, null);
        }
        DataTable ID_Comment.SeachCommentAudit(bool Audited)
        {
            string sqlStr = "select ZL_Comment.*,ZL_CommonModel.Title from ZL_Comment left join ZL_CommonModel on ZL_Comment.GeneralID=ZL_CommonModel.GeneralID where ZL_Comment.Audited=@Audit";
            SqlParameter[] sp = new SqlParameter[] {
                new SqlParameter("@Audit",SqlDbType.Bit)
            };
            sp[0].Value = Audited;
            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, sp);
        }
        /// <summary>
        /// 查询某内容的评论
        /// </summary>
        /// <param name="generalID"></param>
        /// <returns></returns>
        DataTable ID_Comment.SeachComment_ByItemID(int generalID)
        {
            string sqlStr = "select ZL_Comment.*,ZL_CommonModel.Title from ZL_Comment left join ZL_CommonModel on ZL_Comment.GeneralID=ZL_CommonModel.GeneralID where ZL_Comment.GeneralID = " + generalID.ToString();
            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, null);
        }
        /// <summary>
        /// 查询某内容指定标题的评论
        /// </summary>
        /// <param name="generalID"></param>
        /// <param name="Title"></param>
        /// <returns></returns>
        DataTable ID_Comment.SeachComment_ByIDTitle(int generalID, string Title)
        {
            string sqlStr = "select * from ZL_Comment where GeneralID = " + generalID.ToString() + " and Title='" + Title + "'";
            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, null);
        }
        /// <summary>
        /// 获取某内容指定标题的评论的支持方或反对方总数
        /// </summary>
        /// <param name="generalID"></param>
        /// <param name="Title"></param>
        /// <returns></returns>
        int ID_Comment.GetComment_CountIDTitlePK(int generalID, string Title, bool PK)
        {
            string sqlStr = "select count(*) from ZL_Comment where GeneralID = " + generalID.ToString() + " and Title='" + Title + "'";
            if (PK)
            {
                sqlStr = sqlStr + " and PK=1";//支持方
            }
            else
            {
                sqlStr = sqlStr + " and PK=0";//反对方
            }
            return SqlHelper.ObjectToInt32(SqlHelper.ExecuteScalar(CommandType.Text, sqlStr, null));
        }
        /// <summary>
        /// 查询某评论内容
        /// </summary>
        /// <param name="commentID"></param>
        /// <returns></returns>
        M_Comment ID_Comment.GetCommen_ByCommentID(int commentID)
        {
            string sqlStr = "select * from ZL_Comment where CommentID = " + commentID.ToString();
            using (SqlDataReader rdr = SqlHelper.ExecuteReader(CommandType.Text, sqlStr, null))
            {
                if (rdr.Read())
                {
                    return GetInfoFromReader(rdr);
                }
                else
                {
                    return new M_Comment();
                }
            }
        }
        /// <summary>
        /// 查询某节点的是否审核通过的评论
        /// </summary>
        /// <param name="Audited"></param>
        /// <param name="nodeID"></param>
        /// <returns></returns>
        DataTable ID_Comment.SeachComment_ByAudited(bool Audited, int nodeID)
        {
            string sqlStr = "PR_Comment_ByNodeAudit";
            SqlParameter[] sp = new SqlParameter[] {
                new SqlParameter("@Audited",SqlDbType.Bit),
                new SqlParameter("@NodeID",SqlDbType.Int)
            };
            sp[0].Value = Audited;
            sp[1].Value = nodeID;
            return SqlHelper.ExecuteTable(CommandType.StoredProcedure, sqlStr, sp);
        }
        /// <summary>
        /// 查询用户发表的评论
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="NodeID"></param>
        /// <returns></returns>
        DataTable ID_Comment.SearchByUser(int UserID, int NodeID)
        {
            string sqlStr = "select ZL_Comment.*,ZL_CommonModel.Title from ZL_Comment left join ZL_CommonModel on ZL_Comment.GeneralID=ZL_CommonModel.GeneralID where ZL_Comment.UserID = " + UserID.ToString();
            if (NodeID != 0)
                sqlStr = sqlStr + " and ZL_CommonModel.NodeID=" + NodeID.ToString() + " order by ZL_Comment.GeneralID,ZL_Comment.CommentTime Desc";
            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, null);
        }

        #endregion
    }
}
