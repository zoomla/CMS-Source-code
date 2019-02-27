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
/// ID_Survey 的摘要说明
/// </summary>
namespace ZoomLa.IDAL
{
    public interface ID_Survey
    {
        bool AddSurvey(M_Survey m_Survey);
        bool UpdateSurvey(M_Survey m_Survey);
        M_Survey GetSurveyBySid(int SurveyID);
        bool DelSurveyBySid(int SurveyID);
    }
}
