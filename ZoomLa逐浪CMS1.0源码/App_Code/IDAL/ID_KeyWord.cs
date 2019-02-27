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
using System.Collections.Generic;
/// <summary>
/// ID_KeyWord 的摘要说明
/// </summary>
namespace ZoomLa.IDAL
{
    public interface ID_KeyWord
    {
        bool Add(M_KeyWord m_keyword);
        bool Update(M_KeyWord m_keyword);
        bool DeleteByID(string kId);
        M_KeyWord GetKeyWordByid(int kId);
        DataTable GetKeyWordAll();
    }
}
