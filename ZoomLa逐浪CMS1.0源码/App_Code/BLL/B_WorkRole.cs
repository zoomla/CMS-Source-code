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
using ZoomLa.DALFactory;
using ZoomLa.Model;
using ZoomLa.Common;
using System.Collections;
namespace ZoomLa.BLL
{
    /// <summary>
    /// B_WorkRole 的摘要说明
    /// </summary>
    public class B_WorkRole
    {
        private static readonly ID_WorkRole dal = IDal.CreateWorkRole();
        public B_WorkRole()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        public bool AddWorkRole(M_WorkRole m_workrole)
        {
            return dal.AddWorkRole(m_workrole);
        }
        public bool UpdateWorkRole(M_WorkRole m_workrole)
        {
            return dal.UpdateWorkRole(m_workrole);
        }
        public ArrayList GetWorkRole(int WorkID)
        {
            return dal.GetWorkRole(WorkID);
        }
        public bool DelWorkRole(int WorkID)
        {
            return dal.DelWorkRole(WorkID);
        }
    }
}
