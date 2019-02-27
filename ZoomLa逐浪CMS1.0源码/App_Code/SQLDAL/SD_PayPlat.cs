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
/// SD_PayPlat 的摘要说明
/// </summary>
namespace ZoomLa.SQLDAL
{
    public class SD_PayPlat:ID_PayPlat
    {
        public SD_PayPlat()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        public bool Add(M_PayPlat m_PayPlat)
        {
            return true;
        }
        public bool Update(M_PayPlat m_PayPlat)
        {
            return true;
        }
        public bool DeleteByID(int payplatId)
        {
            return true;
        }
        public M_PayPlat GetPayPlatByid(int ppId)
        {
            return new M_PayPlat();
        }
        public DataTable GetPayPlatAll()
        {
            return new DataTable();
        }
    }
}
