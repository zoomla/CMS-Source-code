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
    /// M_Dic 的摘要说明
    /// </summary>
    public class M_Cate
    {
        private int m_CateID;
        private string m_CateName;

        public M_Cate()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
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
        public string CateName
        {
            get
            {
                return this.m_CateName;
            }
            set
            {
                this.m_CateName = value;
            }
        }
    }
}