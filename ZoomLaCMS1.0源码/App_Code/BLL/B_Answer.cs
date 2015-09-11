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
    /// B_Answer 的摘要说明
    /// </summary>
    public class B_Answer
    {
        private static readonly ID_Answer dal = IDal.CreateAnswer();
        public B_Answer()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        public bool AddAnswer(M_Answer m_Answer)
        {
            return dal.AddAnswer(m_Answer);
        }
        public bool UpdateAnswer(M_Answer m_Answer)
        {
            return dal.UpdateAnswer(m_Answer);
        }
        public M_Answer GetAnswerByAid(int AnswerID)
        {
            return dal.GetAnswerByAid(AnswerID);
        }
        public bool DelAnswerByAid(int AnswerID)
        {
            return dal.DelAnswerByAid(AnswerID);
        }
    }
}
