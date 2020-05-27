using System;
using System.Collections.Generic;
using System.Text;
using BDUModel;
using System.Data;
using System.Data.SqlClient;
using ZoomLa.SQLDAL;
namespace BDULogic
{
    public class FriendKnowLogic
    {
        #region 提出问题
        /// <summary>
        /// 提出问题
        /// </summary>
        /// <param name="qu"></param>
        public static Guid PutQuestion(Question qu)
        {
            qu.ID = Guid.NewGuid();
            string SQLstr = @"INSERT INTO [ZL_Sns_Question] (
	                        [ID],
	                        [UserID],
	                        [QuestionContent],
	                        [CreatTime]
                        ) VALUES (
	                        @ID,
	                        @UserID,
	                        @QuestionContent,
	                        @CreatTime
                        )";

            SqlParameter[] parameter ={
                new SqlParameter("@ID",qu.ID),
               new SqlParameter("@UserID",qu.UserID),
               new SqlParameter("@QuestionContent",qu.QuestionContent),
               new SqlParameter("@CreatTime",DateTime.Now)
            };

            SqlHelper.ExecuteNonQuery(CommandType.Text, SQLstr, parameter);
            SystemLog log = new SystemLog();
           // log.LogContent = "您的好友" + UserTableLogic.GetUserTableByUserID(qu.UserID).UserName + "提出问题" + qu.QuestionContent;
            log.LogType = SystemLogType.PutQuestion.ToString();
            log.ModelUrl = @"~\Groupware\RespondQuestion.aspx?QUID=" + qu.ID;
            log.UserID = qu.UserID;
            SystemLogLogic.AddSystemLog(log);
            return qu.ID;
        }
        #endregion

        #region 问题答案
        /// <summary>
        /// 问题答案
        /// </summary>
        /// <param name="an"></param>
        /// <returns></returns>
        public static Guid AddAnswer(Answer an)
        {
            an.ID = Guid.NewGuid();
            string SQLstr = @"INSERT INTO [ZL_Sns_Answer] (
	                            [ID],
	                            [AnswerContent],
	                            [IsRight],
	                            [QusetionID]
                            ) VALUES (
	                            @ID,
	                            @AnswerContent,
	                            @IsRight,
	                            @QusetionID)";

            SqlParameter[] parameter ={
                new SqlParameter("@ID",an.ID),
                 new SqlParameter("@AnswerContent",an.AnswerContent),
                 new SqlParameter("@IsRight",an.IsRight),
                 new SqlParameter("@QusetionID",an.QuestionID)};

            SqlHelper.ExecuteNonQuery(CommandType.Text, SQLstr, parameter);
            return an.ID;
        }
        #endregion

        #region 获取用户自己提出的问题
        /// <summary>
        /// 获取用户自己提出的问题
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static List<Question> GetQuestionByUserID(Guid userID, PagePagination page)
        {
            try
            {
                List<Question> list = new List<Question>();
                string sql = @"select * from ZL_Sns_Question where UserID=@UserID";
                if (page != null)
                {
                    sql = page.PaginationSql(sql);
                }
                SqlParameter[] parameter ={
                new SqlParameter("@UserID",userID)};
                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, sql, parameter))
                {
                    while (dr.Read())
                    {
                        Question qu = new Question();
                        ReadQuestion(dr, qu);
                        list.Add(qu);
                    }
                }
                return list;
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 获取问题的答案
        /// <summary>
        ///  获取问题的答案
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static List<Answer> GetAnswerByUserID(Guid qusetionID)
        {
            try
            {
                List<Answer> list = new List<Answer>();
                string sql = @"select * from ZL_Sns_Answer where QusetionID=@QusetionID";
                SqlParameter[] parameter ={
                new SqlParameter("@QusetionID",qusetionID)};
                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, sql, parameter))
                {
                    while (dr.Read())
                    {
                        Answer qu = new Answer();
                        ReadAnswer(dr, qu);
                        list.Add(qu);
                    }
                }
                return list;
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 删除一个问题
        /// <summary>
        /// 删除一个问题
        /// </summary>
        /// <param name="questionID"></param>
        public static void DeleteQuestion(Guid questionID)
        {
            try
            {
                string sql = @"DELETE ZL_Sns_Answer WHERE QusetionID =@QusetionID ;DELETE Question WHERE ID=@QusetionID";
                SqlParameter[] parameter ={
                new SqlParameter("@QusetionID",questionID)};
                SqlHelper.ExecuteNonQuery(CommandType.Text, sql, parameter);
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 通过问题编号获取问题
        /// <summary>
        /// 通过问题编号获取问题
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Question GetQuestionByID(Guid id)
        {
            string sql = @"select * from ZL_Sns_Question where ID=@ID";
            SqlParameter[] parameter ={
                new SqlParameter("@ID",id)};
            Question qu = new Question();
            using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, sql, parameter))
            {
                if (dr.Read())
                {
                    ReadQuestion(dr, qu);
                }
            }
            return qu;
        }
        #endregion

        #region 回答一个问题
        /// <summary>
        ///回答一个问题 
        /// </summary>
        /// <param name="fr"></param>
        public static void RespondQuestion(FriendRespond fr)
        {
            fr.ID = Guid.NewGuid();
            string sql = @"INSERT INTO [ZL_Sns_FriendRespond] (
	                        [ID],
	                        [QusetionID],
	                        [AnswerID],
	                        [CreatTime],
	                        [FriendID]
                        ) VALUES (
	                        @ID,
	                        @QusetionID,
	                        @AnswerID,
	                        @CreatTime,
	                        @FriendID
                        )";
            SqlParameter[] parameter ={
                new SqlParameter("@ID",fr.ID),
            new SqlParameter("@QusetionID",fr.QusetionID),
            new SqlParameter("@AnswerID",fr.AnswerID),
            new SqlParameter("@CreatTime",DateTime.Now),
            new SqlParameter("@FriendID",fr.FriendID)};
            SqlHelper.ExecuteNonQuery(CommandType.Text, sql, parameter);
        }
        #endregion

        #region 获取用户单个回答的问题
        /// <summary>
        /// 获取用户单个回答的问题
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="queID"></param>
        /// <returns></returns>
        public static FriendRespond GetFriendRespondByUserIDQuesID(Guid userID, Guid queID)
        {
            FriendRespond fr = new FriendRespond();
            string sql = @"select * from ZL_Sns_FriendRespond where QusetionID=@QusetionID and FriendID=@FriendID";
            SqlParameter[] parameter ={
                     new SqlParameter("@QusetionID",queID),
                     new SqlParameter("@FriendID",userID)};
            using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, sql, parameter))
            {
                if (dr.Read())
                {
                    fr.AnswerID = (Guid)dr["AnswerID"];
                    fr.CreatTime = DateTime.Parse(dr["CreatTime"].ToString());
                    fr.FriendID = (Guid)dr["FriendID"];
                    fr.ID = (Guid)dr["ID"]; ;
                    fr.QusetionID = (Guid)dr["QusetionID"];
                }
            }
            return fr;

        }
        #endregion

        #region 获取答案通过编号
        /// <summary>
        /// 获取答案通过编号
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Answer GetAnswerByID(Guid id)
        {
            Answer fr = new Answer();
            string sql = @"select * from ZL_Sns_Answer where ID=@ID ";
            SqlParameter[] parameter ={
                     new SqlParameter("@ID",id)};
            using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, sql, parameter))
            {
                if (dr.Read())
                {
                    ReadAnswer(dr, fr);
                }
            }
            return fr;

        }
        #endregion

        #region 获取答题数
        /// <summary>
        ///获取答题数 
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="frinedid"></param>
        /// <returns></returns>
        public static List<FriendRespond> GetFriendRespondlist(Guid userid, Guid frinedid)
        {
            List<FriendRespond> list = new List<FriendRespond>();
            string sql = @" SELECT * FROM  ZL_Sns_FriendRespond WHERE QusetionID IN(SELECT ID FROM  ZL_Sns_Question WHERE UserID=@UserID) AND FriendID=@FriendID";
            SqlParameter[] parameter ={
                     new SqlParameter("@UserID",frinedid),
                 new SqlParameter("@FriendID",userid)
            };
            using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, sql, parameter))
            {
                while (dr.Read())
                {
                    FriendRespond fr = new FriendRespond();
                    fr.AnswerID = (Guid)dr["AnswerID"];
                    fr.CreatTime = DateTime.Parse(dr["CreatTime"].ToString());
                    fr.FriendID = (Guid)dr["FriendID"];
                    fr.ID = (Guid)dr["ID"]; ;
                    fr.QusetionID = (Guid)dr["QusetionID"];
                    list.Add(fr);
                }
            }
            return list;
        }
        #endregion

        #region 判断是否已经回答了此问题
        /// <summary>
        ///  判断是否已经回答了此问题
        /// </summary>
        /// <param name="quid"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static bool ExitRespond(Guid quid, Guid userid)
        {
            string sql = @" SELECT * FROM  ZL_Sns_FriendRespond WHERE QusetionID =@QusetionID AND FriendID=@FriendID";
            SqlParameter[] parameter ={
                     new SqlParameter("@QusetionID",quid),
                 new SqlParameter("@FriendID",userid)
            };
            using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, sql, parameter))
            {
                if (dr.Read())
                {
                    return true;
                }
            }
            return false;
        }
        #endregion

        #region 获取回答我们题的人
        /// <summary>
        /// 获取回答我们题的人
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static Dictionary<int, Guid> GetQuestionDataTable(Guid userID)
        {
            Dictionary<int, Guid> list = new Dictionary<int, Guid>();
            string sql = @"SELECT COUNT(*) AS con,FriendID  FROM ZL_Sns_FriendRespond WHERE QusetionID IN(SELECT ID FROM ZL_Sns_Question WHERE UserID=@UserID) GROUP BY(FriendID)";
            SqlParameter[] parameter ={
                     new SqlParameter("@UserID",userID)
            };
            using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, sql, parameter))
            {
                while (dr.Read())
                {
                    list.Add(int.Parse(dr["con"].ToString()), new Guid(dr["FriendID"].ToString()));
                }

            }
            return list;
        }
        #endregion

        #region 删除一个人回答我的问题
        /// <summary>
        /// 删除一个人回答我的问题
        /// </summary>
        /// <param name="fiendID"></param>
        /// <param name="userid"></param>
        public static void DeleteFriendR(Guid fiendID, Guid userid)
        {
            try
            {
                string sql = @"DELETE ZL_Sns_FriendRespond WHERE FriendID=@FriendID AND QusetionID IN(SELECT ID FROM  ZL_Sns_Question WHERE UserID=@UserID";
                SqlParameter[] parameter ={
                     new SqlParameter("@FriendID",fiendID),
                     new SqlParameter("@UserID",userid)};
                SqlHelper.ExecuteNonQuery(CommandType.Text, sql, parameter);
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region  获取一个回答自己问题
        /// <summary>
        ///获取一个回答自己问题 
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="frinedid"></param>
        /// <returns></returns>
        public static DataTable GetFriendRespondlistDT(Guid userid, Guid frinedid)
        {
            string sql = @"SELECT ZL_Sns_FriendRespond.QusetionID, ZL_Sns_Question.QuestionContent, 
                              ZL_Sns_FriendRespond.AnswerID
                        FROM ZL_Sns_FriendRespond INNER JOIN
                              ZL_Sns_Question ON ZL_Sns_FriendRespond.QusetionID = ZL_Sns_Question.ID where ZL_Sns_FriendRespond.FriendID=@FriendID ";
            SqlParameter[] parameter ={
                     new SqlParameter("@FriendID",frinedid)};
            DataTable dt = SqlHelper.ExecuteTable(CommandType.Text, sql, parameter);
            return dt;
        }
        #endregion


        public static void ReadQuestion(SqlDataReader dr, Question qu)
        {
            qu.ID = (Guid)dr["ID"];
            qu.CreatTime = DateTime.Parse(dr["CreatTime"].ToString());
            qu.QuestionContent = dr["QuestionContent"].ToString();
            qu.UserID = (Guid)dr["UserID"];
        }


        public static void ReadAnswer(SqlDataReader dr, Answer an)
        {
            an.ID = (Guid)dr["ID"];
            an.AnswerContent = dr["AnswerContent"].ToString();
            an.IsRight = bool.Parse(dr["IsRight"].ToString());
            an.QuestionID = (Guid)dr["QusetionID"];
        }
    }
}
