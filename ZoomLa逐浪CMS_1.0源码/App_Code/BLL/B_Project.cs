
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
    /// B_Project 的摘要说明
    /// </summary>
    public class B_Project
    {
        private static readonly ID_Project dal = IDal.CreateProject();
        public B_Project()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        public bool Add(M_Project projectInfo)
        {
            return dal.Add(projectInfo);
        }
        public bool Update(M_Project m_Project)
        {
            return dal.Update(m_Project);
        }
        public bool DeleteByID(int projectId)
        {
            return dal.DeleteByID(projectId);
        }
        public M_Project GetProjectByid(int prId)
        {
            return dal.GetProjectByid(prId);
        }
        public DataView GetProjectAll()
        {
            return dal.GetProjectAll().DefaultView;
        }
        public DataView GetProjectByUid(int Uid)
        {
            return dal.GetProjectByUid(Uid).DefaultView;
        }
        public int CountProjectNumByRid(int rid)
        {
            return dal.CountProjectNumByRid(rid);
        }
        public string GetProjectEndDate(int projectid)
        {
            return dal.GetProjectEndDate(projectid);
        }
    }
}
