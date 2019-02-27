
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
    using System.Collections;
    using ZoomLa.Model;
    /// <summary>
    /// ID_WorkRole 的摘要说明
    /// </summary>
    public interface ID_WorkRole
    {
        bool AddWorkRole(M_WorkRole m_workrole);
        bool UpdateWorkRole(M_WorkRole m_workrole);
        ArrayList GetWorkRole(int WorkID);
        bool DelWorkRole(int WorkID);
    }
}
