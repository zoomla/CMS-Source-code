using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// M_Answer 的摘要说明
/// </summary>

namespace ZoomLa.Model
{
    public class M_Answer
    {
        private int m_AnswerID; //答案ID
        private int m_SurveryID; //问卷ID
        private int m_QuestionID;//问题ID
        private int m_OptionID; //选项ID
        private string m_AnswerContent;//答案内容
        private int m_UserID;//用户ID     
        public M_Answer()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        public int AnswerID
        {
            get { return this.m_AnswerID; }
            set { this.m_AnswerID = value; }
        }
        public int SurveryID
        {
            get { return this.m_SurveryID; }
            set { this.m_SurveryID = value; }
        }
        public int QuestionID
        {
            get { return this.m_QuestionID; }
            set { this.m_QuestionID = value; }
        }
        public int OptionID
        {
            get { return this.m_OptionID; }
            set { this.m_OptionID = value; }
        }
        public string AnswerContent
        {
            get { return this.m_AnswerContent; }
            set { this.m_AnswerContent = value; }
        }
        public int UserID
        {
            get { return this.m_UserID; }
            set { this.m_UserID = value; }
        }
    }
}
