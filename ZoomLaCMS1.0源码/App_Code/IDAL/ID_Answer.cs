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

namespace ZoomLa.IDAL
{

    /// <summary>
    /// ID_Answer 的摘要说明
    /// </summary>
    public interface ID_Answer
    {
        bool AddAnswer(M_Answer m_Answer);
        bool UpdateAnswer(M_Answer m_Answer);
        M_Answer GetAnswerByAid(int AnswerID);
        bool DelAnswerByAid(int AnswerID);
    }
}
