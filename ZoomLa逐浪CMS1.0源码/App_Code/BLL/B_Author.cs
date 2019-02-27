    using System;
    using System.Data;
    using System.Configuration;
    using System.Web;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Web.UI.WebControls.WebParts;
    using System.Web.UI.HtmlControls;
    using ZoomLa.DALFactory;
    using ZoomLa.IDAL;
    using ZoomLa.Model;
    using ZoomLa.Common;
    using ZoomLa.SQLDAL;

namespace ZoomLa.BLL
{

    /// <summary>
    /// B_Author 的摘要说明
    /// </summary>
    public class B_Author
    {
        private static readonly ID_Author dal = IDal.CreateAuthor();
        public B_Author()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        public bool Add(M_Author authorInfo)
        {
            return dal.Add(authorInfo);
        }
        public bool Update_Author_ByID(M_Author authorInfo)
        {
            return dal.Update(authorInfo);
        }
        public bool DeleteByID(string authorId)
        {
            return dal.DeleteByID(authorId);
        }
        public M_Author GetAuthorByid(int authorId)
        {
            return dal.GetAuthorByid(authorId);
        }
        //存储过程分页
        public DataTable GetAuthorPage(int authorid, int Cpage, int PageSize)
        {
            return dal.GetAuthorPage(authorid, Cpage, PageSize);
        }
        //sql分页
        public DataTable GetAuthorPage(int Cpage, int PageSize)
        {
            return dal.GetAuthorPage(Cpage, PageSize);
        }
        public DataTable GetSourceAll()
        {
            return dal.GetSourceAll();
        }
    }
}
