namespace ZoomLa.BLL
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
    using ZoomLa.IDAL;
    using ZoomLa.Model;
    using ZoomLa.Components;
    using ZoomLa.Common;
    using ZoomLa.DALFactory;

    /// <summary>
    /// B_ProjectWork 的摘要说明
    /// </summary>
    public class B_ProjectWork
    {
        private static readonly ID_ProjectWork ProjectWorkMethod = IDal.CreateProjectWork();
        public B_ProjectWork()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        public bool AddProjectWork(M_ProjectWork m_projwork)
        {
            return ProjectWorkMethod.AddProjectWork(m_projwork);
        }
        public bool DelProWorByID(int workId)
        {
            return ProjectWorkMethod.DelProWorByID(workId);
        }
        public bool UpdateProjectWork(M_ProjectWork m_projwork)
        {
            return ProjectWorkMethod.UpdateProjectWork(m_projwork);
        }
        public DataView SelectWorkByPID(int projectId)
        {
            return ProjectWorkMethod.SelectWorkByPID(projectId).DefaultView;
        }
        public M_ProjectWork SelectWorkByWID(int WId)
        {
            return ProjectWorkMethod.SelectWorkByWID(WId);
        }
        public bool DelProWorByPID(int projectId)
        {
            return ProjectWorkMethod.DelProWorByPID(projectId);
        }
        public int CountWork(int projectid)
        {
            return ProjectWorkMethod.CountWork(projectid);
        }
        public int CountFinishWork(int projectid)
        {
            return ProjectWorkMethod.CountFinishWork(projectid);
        }
        public int GetMaxWorkID(int projectid)
        {
            return ProjectWorkMethod.GetMaxWorkID(projectid);
        }
        public DataTable GetProjectWorkAll()
        {
            return ProjectWorkMethod.GetProjectWorkAll();
        }
    }
}
