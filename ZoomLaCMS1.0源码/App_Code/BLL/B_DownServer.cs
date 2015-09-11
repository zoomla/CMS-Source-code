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
    using ZoomLa.IDAL;
    using ZoomLa.Model;
    using ZoomLa.Components;
    using ZoomLa.Common;
    using ZoomLa.DALFactory;

    /// <summary>
    /// B_DownServer 的摘要说明
    /// </summary>
    public class B_DownServer
    {
        private static readonly ID_DownServer DownServerMethod = IDal.CreateDownServer();
        public B_DownServer()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        public bool Add(M_DownServer downserverInfo)
        {
            return DownServerMethod.Add(downserverInfo);
        }
        public bool DeleteByID(string dId)
        {
            return DownServerMethod.DeleteByID(dId);
        }
        public DataTable GetDownServerAll()
        {
            return DownServerMethod.GetDownServerAll();
        }
        public bool Update(M_DownServer DownServerInfo)
        {
            return DownServerMethod.Update(DownServerInfo);
        }
        public M_DownServer GetDownServerByid(int dId)
        {
            return DownServerMethod.GetDownServerByid(dId);
        }
        public int Max()
        {
            return DownServerMethod.Max();
        }
    }
}
