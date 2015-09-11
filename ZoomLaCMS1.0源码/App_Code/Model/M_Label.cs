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
    /// M_Lable 的摘要说明
    /// </summary>
    public class M_Label
    {
        private int m_LableID;
        private string m_LableName;
        private string m_LabelCate;
        private int m_LableType;
        private string m_Desc;
        private string m_Param;
        private string m_LabelTable;
        private string m_LabelField;
        private string m_LabelWhere;
        private string m_LabelOrder;
        private string m_Content;
        private string m_LabelCount;        
        private bool m_IsNull;

        public M_Label()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        public M_Label(bool flag)
        {
            this.m_IsNull=flag;
        }
        public int LabelID
        {
            get{return this.m_LableID;}
            set{this.m_LableID=value;}
        }
        public string LableName
        {
            get{return this.m_LableName;}
            set{this.m_LableName=value;}
        }
        public string LabelCate
        {
            get{return this.m_LabelCate;}
            set{this.m_LabelCate=value;}
        }
        public int LableType
        {
            get { return this.m_LableType; }
            set { this.m_LableType = value; }
        }
        public string Desc
        {
            get { return this.m_Desc; }
            set { this.m_Desc = value; }
        }
        public string Param
        {
            get { return this.m_Param; }
            set { this.m_Param = value; }
        }
        public string LabelTable
        {
            get { return this.m_LabelTable; }
            set { this.m_LabelTable = value; }
        }
        public string LabelField
        {
            get { return this.m_LabelField; }
            set { this.m_LabelField = value; }
        }
        public string LabelWhere
        {
            get { return this.m_LabelWhere; }
            set { this.m_LabelWhere = value; }
        }
        public string LabelOrder
        {
            get { return this.m_LabelOrder; }
            set { this.m_LabelOrder = value; }
        }        
        
        public string Content
        {
            get { return this.m_Content; }
            set { this.m_Content = value; }
        }
        public string LabelCount
        {
            get { return this.m_LabelCount; }
            set { this.m_LabelCount = value; }
        }        
        public bool IsNull
        {
            get { return this.m_IsNull; }
        }
    }
}