namespace ZoomLa.Model
{
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
    /// M_Project 的摘要说明
    /// </summary>
    public class M_Project
    {
        private int m_ProjectID;//关键字ID
        private string m_ProjectName;//项目名称
        private string m_ProjectIntro;//项目介绍
        private int m_Uid;//客户ID
        private int m_RequireID;//需求ID
        private DateTime m_StartDate;//项目开始时间
        private DateTime m_EndDate;//项目完成时间
        private int m_Status;//状态

        public M_Project()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        public int ProjectID
        {
            get { return this.m_ProjectID; }
            set { this.m_ProjectID = value; }
        }
        public string ProjectName
        {
            get { return this.m_ProjectName; }
            set { this.m_ProjectName = value; }
        }
        public string ProjectIntro
        {
            get { return this.m_ProjectIntro; }
            set { this.m_ProjectIntro = value; }
        }
        public int Uid
        {
            get { return this.m_Uid; }
            set { this.m_Uid = value; }
        }
        public int RequireID
        {
            get { return this.m_RequireID; }
            set { this.m_RequireID = value; }
        }
        public DateTime StartDate
        {
            get { return this.m_StartDate; }
            set { this.m_StartDate = value; }
        }
        public DateTime EndDate
        {
            get { return this.m_EndDate; }
            set { this.m_EndDate = value; }
        }
        public int Status
        {
            get { return this.m_Status; }
            set { this.m_Status = value; }
        }
    }
}
