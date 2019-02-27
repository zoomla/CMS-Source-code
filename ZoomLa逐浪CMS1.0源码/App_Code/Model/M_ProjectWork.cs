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
    /// M_ProjectWork 的摘要说明 项目执行内容表
    /// </summary>
    public class M_ProjectWork
    {
        private int m_WorkID;//执行内容ID
        private string m_WorkName;//执行内容名称
        private string m_WorkIntro;//执行内容名称
        private int m_ProjectID;//项目ID
        private int m_Approving;//客户满意度
        private int m_Status;//状态
        private DateTime m_EndDate;//完成时间

        public M_ProjectWork()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        public int WorkID
        {
            get { return this.m_WorkID; }
            set { this.m_WorkID = value; }
        }
        public string WorkName
        {
            get { return this.m_WorkName; }
            set { this.m_WorkName = value; }
        }
        public string WorkIntro
        {
            get { return this.m_WorkIntro; }
            set { this.m_WorkIntro = value; }
        }
        public int ProjectID
        {
            get { return this.m_ProjectID; }
            set { this.m_ProjectID = value; }
        }
        public int Approving
        {
            get { return this.m_Approving; }
            set { this.m_Approving = value; }
        }
        public int Status
        {
            get { return this.m_Status; }
            set { this.m_Status = value; }
        }
        public DateTime EndDate
        {
            get { return this.m_EndDate; }
            set { this.m_EndDate= value; }
        }
    }
}
