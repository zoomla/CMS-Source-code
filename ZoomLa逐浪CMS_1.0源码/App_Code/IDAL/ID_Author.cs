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
    /// ID_Author 的摘要说明
    /// </summary>
    public interface ID_Author
    {
        bool Add(M_Author m_author);
        bool Update(M_Author m_author);
        bool DeleteByID(string authorId);
        M_Author GetAuthorByid(int authorId);
        DataTable GetAuthorPage(int Cpage, int PageSize);
        DataTable GetAuthorPage(int authorid, int Cpage, int PageSize);
        DataTable GetSourceAll();
        
    }
}
