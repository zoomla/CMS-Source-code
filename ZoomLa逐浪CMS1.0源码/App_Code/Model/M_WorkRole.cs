
namespace ZoomLa.Model
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

    /// <summary>
    /// M_WorkRole 的摘要说明
    /// </summary>
    public class M_WorkRole
    {
        private int m_ID;//标识ID
        private int m_WorkID;//工作内容
        private int m_RoleID;//角色ID
        public M_WorkRole()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        public int ID
        {
            get { return this.m_ID; }
            set { this.m_ID = value; }
        }
        public int WorkID
        {
            get { return this.m_WorkID;}
            set { this.m_WorkID= value; }
        }
        public int RoleID
        {
            get { return this.m_RoleID; }
            set { this.m_RoleID = value; }
        }
    }
}
