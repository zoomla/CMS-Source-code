using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ZoomLa.Model;
/// <summary>
/// ID_Question 的摘要说明
/// </summary>
namespace ZoomLa.IDAL
{
    public interface ID_Question
    {
        bool AddQuestiony(M_Question m_Question);
        bool UpdateQuestion(M_Question m_Question);
        M_Question GetQuestionByQid(int QuestionID);
        bool DelQuestionByQid(int QuestionID);
    }
}
