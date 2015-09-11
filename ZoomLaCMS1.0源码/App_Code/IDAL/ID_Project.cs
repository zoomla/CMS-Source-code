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
    /// ID_Project 的摘要说明
    /// </summary>
    public interface ID_Project
    {
        bool Add(M_Project m_Project);
        bool Update(M_Project m_Project);
        bool DeleteByID(int projectId);
        M_Project GetProjectByid(int prId);      
        DataTable GetProjectAll();
        DataTable GetProjectByUid(int Uid);
        int CountProjectNumByRid(int rid);
        string GetProjectEndDate(int projectid);
    }
}
