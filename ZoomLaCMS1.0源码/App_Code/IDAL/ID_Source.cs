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
    /// ID_Source 的摘要说明
    /// </summary>
    public interface ID_Source
    {
        bool Add(M_Source m_source);
        bool Update(M_Source m_source);
        bool DeleteByID(string sId);
        M_Source GetSourceByid(int sId);
        DataTable GetSourceAll();
        DataTable SearchSource(string sourcekey);
    }
}
