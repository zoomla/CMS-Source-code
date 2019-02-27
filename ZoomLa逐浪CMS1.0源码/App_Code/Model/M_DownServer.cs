
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
    /// DownServer 的摘要说明
    /// </summary>
    public class M_DownServer
    {
        private int m_ServerID;//镜像服务器ID
        private string m_ServerName;//镜像服务器名
        private string m_ServerUrl;//镜像服务器地址
        private string m_ServerLogo;//镜像服务器Logo
        private int m_OrderID;//镜像服务器排序ID
        private int m_ShowType;//镜像服务器显示方式

        public M_DownServer()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        public int ServerID
        {
            get { return this.m_ServerID; }
            set { this.m_ServerID = value; }
        }
        public string ServerName
        {
            get { return this.m_ServerName; }
            set { this.m_ServerName = value; }
        }
        public string ServerUrl
        {
            get { return this.m_ServerUrl; }
            set { this.m_ServerUrl = value; }
        }
        public string ServerLogo
        {
            get { return this.m_ServerLogo; }
            set { this.m_ServerLogo = value; }
        }
        public int OrderID
        {
            get { return this.m_OrderID; }
            set { this.m_OrderID = value; }
        }
        public int ShowType
        {
            get { return this.m_ShowType; }
            set { this.m_ShowType = value; }
        }
    }
}
