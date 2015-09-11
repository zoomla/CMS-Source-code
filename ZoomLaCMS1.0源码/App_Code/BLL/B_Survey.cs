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
    /// B_Survey 的摘要说明
    /// </summary>
    public class B_Survey
    {
        private static readonly ID_Survey dal = IDal.CreateSurvey();
        public B_Survey()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        public bool AddSurvey(M_Survey m_Survey)
        {
            return dal.AddSurvey(m_Survey);
        }
        public bool UpdateSurvey(M_Survey m_Survey)
        {
            return dal.UpdateSurvey(m_Survey);
        }
        public M_Survey GetSurveyBySid(int SurveyID)
        {
            return dal.GetSurveyBySid(SurveyID);
        }
        public bool DelSurveyBySid(int SurveyID)
        {
            return dal.DelSurveyBySid(SurveyID);
        }
    }
}
