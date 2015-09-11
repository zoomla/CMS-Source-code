using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
namespace ZoomLa.Model
{
    /// <summary>
    /// M_ClientRequire 的摘要说明
    /// </summary>
    public class M_ClientRequire
    {
        private int m_RequireID;//自动增加
        private string m_Require;//客户需求内容
        private int m_UserID;//客户名称     
        private DateTime m_ReuqireDate;//最近登录时间

        public M_ClientRequire()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        public int RequireID
        {
            get
            {
                return this.m_RequireID;
            }
            set
            {
                this.m_RequireID = value;
            }
        }
        public string Require
        {
            get { return this.m_Require; }
            set { this.m_Require = value; }
        }
        public int UserID
        {
            get { return this.m_UserID; }
            set { this.m_UserID = value; }
        }
        public DateTime ReuqireDate
        {
            get { return this.m_ReuqireDate; }
            set { this.m_ReuqireDate = value; }
        }
    }
}
