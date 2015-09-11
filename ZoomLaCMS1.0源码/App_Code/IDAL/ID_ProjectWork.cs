
namespace ZoomLa.IDAL
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
    using ZoomLa.Model;
    /// <summary>
    /// ID_ProjectWork 的摘要说明
    /// </summary>
    public interface ID_ProjectWork
    {
        bool AddProjectWork(M_ProjectWork m_projwork);
        bool UpdateProjectWork(M_ProjectWork m_projwork);
        bool DelProWorByID(int workId);
        bool DelProWorByPID(int projectId);
        DataTable SelectWorkByPID(int projectId);
        M_ProjectWork SelectWorkByWID(int WId);
        int CountWork(int projectid);
        int CountFinishWork(int projectid);
        int GetMaxWorkID(int projectid);
        DataTable GetProjectWorkAll();
    }
}
