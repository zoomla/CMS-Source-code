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
    /// 评论信息
    /// </summary>
    public interface ID_Cate
    {
        bool Add(M_Cate cate);
        bool Update(M_Cate cate);
        bool DeleteByID(int cateID);
        M_Cate GetCateByid(int cateId);
        DataTable SeachCateAll();
      

    }
}
