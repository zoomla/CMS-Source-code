using System;
using System.Collections.Generic;
using System.Text;
using BDUModel;
using BDULogic;

namespace BDUBLL
{
    public class FriendKnowBLL
    {
        #region 提出问题
        /// <summary>
        /// 提出问题
        /// </summary>
        /// <param name="qu"></param>
        public Guid PutQuestion(Question qu)
        {
            return FriendKnowLogic.PutQuestion(qu);
        }
        #endregion

        #region 问题答案
        /// <summary>
        /// 问题答案
        /// </summary>
        /// <param name="an"></param>
        /// <returns></returns>
        public Guid AddAnswer(Answer an)
        {
            return FriendKnowLogic.AddAnswer(an);
        }
        #endregion

        #region 获取用户自己提出的问题
        /// <summary>
        /// 获取用户自己提出的问题
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<Question> GetQuestionByUserID(Guid userID, PagePagination page)
        {
            return FriendKnowLogic.GetQuestionByUserID(userID, page);
        }
        #endregion

        #region 获取问题的答案
        /// <summary>
        ///  获取问题的答案
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<Answer> GetAnswerByUserID(Guid qusetionID)
        {
            return FriendKnowLogic.GetAnswerByUserID(qusetionID);
        }
        #endregion

        #region 删除一个问题
        /// <summary>
        /// 删除一个问题
        /// </summary>
        /// <param name="questionID"></param>
        public void DeleteQuestion(Guid questionID)
        {
            FriendKnowLogic.DeleteQuestion(questionID);
        }
        #endregion

        #region 通过问题编号获取问题
        /// <summary>
        /// 通过问题编号获取问题
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Question GetQuestionByID(Guid id)
        {
            return FriendKnowLogic.GetQuestionByID(id);
        }
        #endregion

        #region 回答一个问题
        /// <summary>
        ///回答一个问题 
        /// </summary>
        /// <param name="fr"></param>
        public void RespondQuestion(FriendRespond fr)
        {
            FriendKnowLogic.RespondQuestion(fr);
        }
        #endregion

        #region 获取用户单个回答的问题
        /// <summary>
        /// 获取用户单个回答的问题
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="queID"></param>
        /// <returns></returns>
        public FriendRespond GetFriendRespondByUserIDQuesID(Guid userID, Guid queID)
        {
            return FriendKnowLogic.GetFriendRespondByUserIDQuesID(userID, queID);
        }
        #endregion

        #region 获取答案通过编号
        /// <summary>
        /// 获取答案通过编号
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Answer GetAnswerByID(Guid id)
        {
            return FriendKnowLogic.GetAnswerByID(id);
        }
        #endregion

        #region 获取答题数
        /// <summary>
        ///获取答题数 
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="frinedid"></param>
        /// <returns></returns>
        public List<FriendRespond> GetFriendRespondlist(Guid userid, Guid frinedid)
        {
            return FriendKnowLogic.GetFriendRespondlist(userid, frinedid);
        }
        #endregion

        #region 判断是否已经回答了此问题
        /// <summary>
        ///  判断是否已经回答了此问题
        /// </summary>
        /// <param name="quid"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public bool ExitRespond(Guid quid, Guid userid)
        {
            return FriendKnowLogic.ExitRespond(quid, userid);
        }
        #endregion

        #region 获取回答我们题的人
        /// <summary>
        /// 获取回答我们题的人
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public Dictionary<int, Guid> GetQuestionDataTable(Guid userID)
        {
            return FriendKnowLogic.GetQuestionDataTable(userID);
        }
        #endregion

        #region 删除一个人回答我的问题
        /// <summary>
        /// 删除一个人回答我的问题
        /// </summary>
        /// <param name="fiendID"></param>
        /// <param name="userid"></param>
        public  void DeleteFriendR(Guid fiendID, Guid userid)
        {
             FriendKnowLogic.DeleteFriendR(fiendID, userid);
        }
        #endregion

         #region  获取一个回答自己问题
        /// <summary>
        ///获取一个回答自己问题 
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="frinedid"></param>
        /// <returns></returns>
        public System.Data.DataTable GetFriendRespondlistDT(Guid userid, Guid frinedid)
        {
            return FriendKnowLogic.GetFriendRespondlistDT(userid, frinedid);
        }
         #endregion
    }
}
