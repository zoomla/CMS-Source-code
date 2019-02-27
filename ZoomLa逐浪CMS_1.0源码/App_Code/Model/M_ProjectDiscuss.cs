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
    /// M_ProjectDiscuss 的摘要说明
    /// </summary>
    public class M_ProjectDiscuss
    {
        private int m_DiscussID;
        private int m_ProjectID; //项目ID
        private int m_WorkID; //执行内容ID 
        private int m_UserID; //发布讨论人ID 
        private string m_Content;//执行内容名称
        private DateTime m_DiscussDate;//讨论时间     

        public M_ProjectDiscuss()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        public int DiscussID
        {
            get { return this.m_DiscussID; }
            set { this.m_DiscussID = value; }
        }
        public int ProjectID
        {
            get { return this.m_ProjectID; }
            set { this.m_ProjectID = value; }
        }

        public int WorkID
        {
            get { return this.m_WorkID; }
            set { this.m_WorkID = value; }
        }
        public int UserID
        {
            get { return this.m_UserID; }
            set { this.m_UserID = value; }
        }
        public string Content
        {
            get { return this.m_Content; }
            set { this.m_Content = value; }
        }
        public DateTime DiscussDate
        {
            get { return this.m_DiscussDate; }
            set { this.m_DiscussDate = value; }
        }
    }
}
