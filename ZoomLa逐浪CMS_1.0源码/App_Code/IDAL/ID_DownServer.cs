
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
    /// ID_DownServer 的摘要说明
    /// </summary>
    public interface ID_DownServer
    {
        bool Add(M_DownServer m_downserver);
        bool Update(M_DownServer m_downserver);
        bool DeleteByID(string dId);
        M_DownServer GetDownServerByid(int dId);
        DataTable GetDownServerAll();
        int Max();
    }
}
