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
    /// B_ProjectDiscuss 的摘要说明
    /// </summary>
    public class B_ProjectDiscuss
    {
        private static readonly ID_ProjectDiscuss dal = IDal.CreateProjectDiscuss();
        public B_ProjectDiscuss()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        public bool AddProjectDiscuss(M_ProjectDiscuss ProjectDiscuss)
        {
            return dal.AddProjectDiscuss(ProjectDiscuss);
        }
        public bool DelProjectDiscuss(int DiscussID)
        {
            return dal.DelProjectDiscuss(DiscussID);
        }
        public bool UpdateProjectDiscuss(M_ProjectDiscuss ProjectDiscuss)
        {
            return dal.UpdateProjectDiscuss(ProjectDiscuss);
        }
        public DataView GetDiscussByWid(int WId)
        {
            return dal.GetDiscussByWid(WId).DefaultView;
        }
        public int CountDisByWid(int Wid, int Pid)
        {
            return dal.CountDisByWid(Wid, Pid);
        }
        public DataView GetDiscussAll()
        {
            return dal.GetDiscussAll().DefaultView;
        }
    }
}
