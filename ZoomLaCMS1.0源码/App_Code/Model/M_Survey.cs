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
/// M_Survey 的摘要说明
/// </summary>

namespace ZoomLa.Model
{
    public class M_Survey
    {
        private int m_SurveyID; //调查问卷ID
        private string m_SurveyName; //调查问卷名称
        private string m_Description; //调查问卷描述 
        private int m_IPRepeat; //同一IP允许重复提交次数
        private DateTime m_CreateDate; //创建日期
        private DateTime m_EndTime; //结束日期
        private int m_IsOpen; //1--启用，0---没起用
        private int m_NeedLogin; //登录后才能参与问卷调查 (1--需要，0---不需要)    
        private string m_Template; //问卷模板

        public M_Survey()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        public int SurveyID
        {
            get { return this.m_SurveyID; }
            set { this.m_SurveyID = value; }
        }
        public string SurveyName
        {
            get { return this.m_SurveyName; }
            set { this.m_SurveyName = value; }
        }
        public string Description
        {
            get { return this.m_Description; }
            set { this.m_Description = value; }
        }       
        public int IPRepeat
        {
            get { return this.m_IPRepeat; }
            set { this.m_IPRepeat = value; }
        }

        public DateTime CreateDate
        {
            get { return this.m_CreateDate; }
            set { this.m_CreateDate = value; }
        }     
        public DateTime EndTime
        {
            get { return this.m_EndTime; }
            set { this.m_EndTime = value; }
        }

        public int IsOpen
        {
            get { return this.m_IsOpen; }
            set { this.m_IsOpen = value; }
        }
        public int NeedLogin
        {
            get { return this.m_NeedLogin; }
            set { this.m_NeedLogin = value; }
        }  
        public string Template
        {
            get { return this.m_Template; }
            set { this.m_Template = value; }
        }       
    }
}
