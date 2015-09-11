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
    /// M_Dic1 的摘要说明
    /// </summary>
    public class M_CateDetail
    {
        private int m_CateDetailID;
        private string m_CateDetailName;
        private int m_CateID;
        public M_CateDetail()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        public int CateDetailID
        {
            get
            {
                return this.m_CateDetailID;
            }
            set
            {
                this.m_CateDetailID = value;
            }
        }
        public string CateDetailName
        {
            get
            {
                return this.m_CateDetailName;
            }
            set
            {
                this.m_CateDetailName = value;
            }
        }
        public int CateID
        {
            get
            {
                return this.m_CateID;
            }
            set
            {
                this.m_CateID = value;
            }
        }

    }
}