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
using ZoomLa.IDAL;
using System.Data.SqlClient;
using ZoomLa.Common;
/// <summary>
/// ID_PayPlat 的摘要说明
/// </summary>

namespace ZoomLa.IDAL
{
    public interface ID_PayPlat
    {
        bool Add(M_PayPlat m_PayPlat);
        bool Update(M_PayPlat m_PayPlat);
        bool DeleteByID(int payplatId);
        M_PayPlat GetPayPlatByid(int pId);
        DataTable GetPayPlatAll();
    }
}
