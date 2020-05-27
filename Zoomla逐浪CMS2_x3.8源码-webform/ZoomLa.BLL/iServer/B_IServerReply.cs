namespace ZoomLa.BLL
{
    using System;
    using System.Data;
    using System.Configuration;
    using System.Web;
    using ZoomLa.Model;
    using ZoomLa.Common;
    using System.Net.Mail;
    using System.Collections.Generic;
    using ZoomLa.Components;
    using System.Data.SqlClient;
    using ZoomLa.SQLDAL;

    public class B_IServerReply
    {
        public B_IServerReply()
        {
            strTableName = initmod.TbName;
            PK = initmod.PK;
        }
        public string PK, strTableName;
        private M_IServerReply initmod = new M_IServerReply();

        /// <summary>
        /// 根据ID查询一条记录
        /// </summary>
       

        //添加回复信息
        public static bool Add(M_IServerReply ReplyInfo)
        {
            string sqlStr = "INSERT INTO ZL_IServerReply(QuestionId,UserId,Title,ReplyTime,Content,Path,isRead)VALUES(@QuestionId,@UserId,@Title,@ReplyTime,@Content,@Path,@isRead);";
            SqlParameter[] parameter = new SqlParameter[7];
            parameter[0] = new SqlParameter("@QuestionId", SqlDbType.Int);
            parameter[0].Value = ReplyInfo.QuestionId;
            parameter[1] = new SqlParameter("@UserId", SqlDbType.Int);
            parameter[1].Value = ReplyInfo.UserId;
            parameter[2] = new SqlParameter("@Title", SqlDbType.NVarChar, 100);
            parameter[2].Value = ReplyInfo.Title;
            parameter[3] = new SqlParameter("@ReplyTime", SqlDbType.DateTime);
            parameter[3].Value = ReplyInfo.ReplyTime;
            parameter[4] = new SqlParameter("@Content", SqlDbType.NText);
            parameter[4].Value = ReplyInfo.Content;
            parameter[5] = new SqlParameter("@Path", SqlDbType.VarChar, 1000);
            parameter[5].Value = ReplyInfo.Path;
            parameter[6] = new SqlParameter("@isRead", SqlDbType.Int);
            parameter[6].Value = ReplyInfo.IsRead;
            return SqlHelper.ExecuteSql(sqlStr, parameter);
        }

        //根据问题Id查询回复
        public static DataTable SeachById(int QuestionId)
        {
            string sqlStr = "SELECT * FROM ZL_IServerReply WHERE QuestionId=@QuestionId Order by ReplyTime";
            SqlParameter[] cmdParams = new SqlParameter[] {
                new SqlParameter("@QuestionId", SqlDbType.Int),
            };
            cmdParams[0].Value = QuestionId;

            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, cmdParams);
        }
        //编辑回复内容
        public static bool UpdateByID(int replyid,string Content)
        {
            string sql = "UPDATE ZL_IServerReply SET Content=@content WHERE Id="+replyid;
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("@content", Content) };
            return SqlHelper.ExecuteSql(sql,sp);
        }
        /// <summary>
        /// 通过提问者查询
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public DataTable GetSelOUserid(int userId, int num)
        {
            string sqlStr = "SELECT top " + num + " b.*,a.* FROM ZL_IServerReply as a,ZL_IServer as b WHERE a.QuestionId =b.QuestionId AND b.userId =@userId Order by ReplyTime DESC";
            SqlParameter[] cmdParams = new SqlParameter[] {
                new SqlParameter("@userId", SqlDbType.Int),
            };
            cmdParams[0].Value = userId;

            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, cmdParams);
        }

        /// <summary>
        /// 更新是否已读的状态
        /// </summary>
        /// <param name="state">是否已读</param>
        /// <param name="questionId">问题ID</param>
        /// <returns></returns>
        public bool GetUpdataState(int state, int questionId)
        {
            string sqlStr = "Update ZL_IServerReply set isRead=@isRead where QuestionId=@QuestionId";
            SqlParameter[] para = new SqlParameter[]{
                new SqlParameter("@isRead",state),
                new SqlParameter("@QuestionId",questionId)
            };
            return SqlHelper.ExecuteSql(sqlStr, para);
        }

        /// <summary>
        /// 获取未读数量
        /// </summary>
        /// <param name="isread"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public DataTable GetReadMess(int isread, int userId)
        {
            string sqlStr = "SELECT b.*,a.* FROM ZL_IServerReply as a,ZL_IServer as b WHERE a.questionid=b.questionid AND b.userId=" + userId + "AND a.isRead=" + isread;
            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr);
        }
    }
}
