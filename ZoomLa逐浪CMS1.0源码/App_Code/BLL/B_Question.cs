using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using ZoomLa.DALFactory;
using ZoomLa.IDAL;
using ZoomLa.Model;
using ZoomLa.Common;
using ZoomLa.SQLDAL;

namespace ZoomLa.BLL
{

    /// <summary>
    /// B_Question 的摘要说明
    /// </summary>
    public class B_Question
    {
        private static readonly ID_Question dal = IDal.CreateQuestion();
        public B_Question()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        public bool AddQuestiony(M_Question m_Question)
        {
            return dal.AddQuestiony(m_Question);
        }
        public bool UpdateQuestion(M_Question m_Question)
        {
            return dal.UpdateQuestion(m_Question);
        }
        public M_Question GetQuestionByQid(int QuestionID)
        {
            return dal.GetQuestionByQid(QuestionID);
        }
        public bool DelQuestionByQid(int QuestionID)
        {
            return dal.DelQuestionByQid(QuestionID);
        }
    }
}
