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
    /// B_ClientRequire 的摘要说明
    /// </summary>
    public class B_ClientRequire
    {
        private static readonly ID_ClientRequire dal = IDal.CreateClientRequire();
        public B_ClientRequire()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        public bool Add(M_ClientRequire CRinfo)
        {
            return dal.Add(CRinfo);
        }
        public bool Update(M_ClientRequire CRinfo)
        {
            return dal.Add(CRinfo);
        }
        public bool DeleteByRID(int RequireID)
        {
            return dal.DeleteByID(RequireID);
        }
        public DataView GetClientRequireAll()
        {
            return dal.GetClientRequireAll().DefaultView;
        }
    }
}
