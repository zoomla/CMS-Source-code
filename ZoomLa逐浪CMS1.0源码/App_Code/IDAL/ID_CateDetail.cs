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
/// ID_Dic1 的摘要说明
/// </summary>
public interface ID_CateDetail
	{
        bool Add(M_CateDetail catedetail);
        bool Update(M_CateDetail catedetail);
        bool DeleteByID(int catedetailID);
        DataTable SeachCateDetailAll(int cateID);
        M_CateDetail GetcatedetailById(int catedetailid);

	}
    

}

