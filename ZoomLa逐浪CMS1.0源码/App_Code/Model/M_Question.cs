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
/// M_Question 的摘要说明
/// </summary>

namespace ZoomLa.Model
{
    public class M_Question
    {
        private int m_ID; //问题ID
        private int m_SurveryID; //问卷的ID
        private int m_TypeID;//问题类型ID
        private string m_QuestionTitle; //问题题目
        private string m_QuestionContent;//问题内容
        private DateTime m_QuestionCreateTime;//问题创建时间
        private int m_ManagerID;//问题创建者
        public M_Question()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        public int QuestionID
        {
            get { return this.m_ID; }
            set { this.m_ID = value; }
        }
        public int SurveryID
        {
            get { return this.m_SurveryID; }
            set { this.m_SurveryID = value; }
        }
        public int TypeID
        {
            get { return this.m_ID; }
            set { this.m_ID = value; }
        }
        public string QuestionTitle
        {
            get { return this.m_QuestionTitle; }
            set { this.m_QuestionTitle = value; }
        }
        public string QuestionContent
        {
            get { return this.m_QuestionContent; }
            set { this.m_QuestionContent = value; }
        }
        public DateTime QuestionCreateTime
        {
            get { return this.m_QuestionCreateTime; }
            set { this.m_QuestionCreateTime = value; }
        }
        public int ManagerID
        {
            get { return this.m_ManagerID; }
            set { this.m_ManagerID = value; }
        }
    }
}
