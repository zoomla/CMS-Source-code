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
    /// ID_ClientRequire 的摘要说明
    /// </summary>
    public interface ID_ClientRequire
    {
        bool Add(M_ClientRequire m_CRInfo);
        bool Update(M_ClientRequire m_CRInfo);
        bool DeleteByID(int crId);
        //M_Author GetAuthorByid(int authorId);      
        DataTable GetClientRequireAll();
    }
}
