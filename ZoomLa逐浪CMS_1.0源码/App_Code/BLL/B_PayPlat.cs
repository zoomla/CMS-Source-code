
namespace ZoomLa.BLL
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
    using ZoomLa.DALFactory;
    using ZoomLa.IDAL;
    using ZoomLa.Model;
    using ZoomLa.Common;
    using ZoomLa.SQLDAL;
    /// <summary>
    /// B_PayPlat 的摘要说明
    /// </summary>
    public class B_PayPlat
    {
        private static readonly ID_PayPlat dal = IDal.CreatePayPlat();
        public B_PayPlat()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        public bool Add(M_PayPlat m_PayPlat)
        {
            return dal.Add(m_PayPlat);
        }
        public bool Update(M_PayPlat m_PayPlat)
        {
            return dal.Update(m_PayPlat);
        }
        public bool DeleteByID(int payplatId)
        {
            return dal.DeleteByID(payplatId);
        }
        public M_PayPlat GetPayPlatByid(int pId)
        {
            return dal.GetPayPlatByid(pId);
        }
        public DataTable GetPayPlatAll()
        {
            return dal.GetPayPlatAll();
        }
    }
}
