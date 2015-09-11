using System;
namespace ZoomLa.Model
{
    /// <summary>
    /// M_CommonData 的摘要说明
    /// </summary>
    public class M_CommonData
    {
        private int m_GeneralID;
        private int m_NodeID;
        private int m_ModelID;
        private int m_ItemID;
        private string m_TableName;
        private string m_Title;
        private string m_Inputer;
        private int m_EliteLevel;
        private string m_InfoID;
        private string m_SpecialID;
        private int m_IsCreate;
        private string m_HtmlLink;
        private int m_Hits;
        private string m_Template;
        private int m_Status;
        private bool m_IsNull;

        public M_CommonData()
        {
            
        }
        public M_CommonData(bool flag)
        {
            this.m_IsNull = flag;
        }
        public int GeneralID
        {
            get { return this.m_GeneralID; }
            set { this.m_GeneralID = value; }
        }
        public int NodeID
        {
            get { return this.m_NodeID; }
            set { this.m_NodeID = value; }
        }
        public int ModelID
        {
            get { return this.m_ModelID; }
            set { this.m_ModelID = value; }
        }
        public int ItemID
        {
            get { return this.m_ItemID; }
            set { this.m_ItemID = value; }
        }
        public string TableName
        {
            get { return this.m_TableName; }
            set { this.m_TableName = value; }
        }
        public string Title
        {
            get { return this.m_Title; }
            set { this.m_Title = value; }
        }
        public string Inputer
        {
            get { return this.m_Inputer; }
            set { this.m_Inputer = value; }
        }
        public int EliteLevel
        {
            get { return this.m_EliteLevel; }
            set { this.m_EliteLevel = value; }
        }
        public string InfoID
        {
            get { return this.m_InfoID; }
            set { this.m_InfoID = value; }
        }
        public string SpecialID
        {
            get { return this.m_SpecialID; }
            set { this.m_SpecialID = value; }
        }
        public int IsCreate
        {
            get { return this.m_IsCreate; }
            set { this.m_IsCreate = value; }
        }
        public string HtmlLink
        {
            get { return this.m_HtmlLink; }
            set { this.m_HtmlLink = value; }
        }
        public int Hits
        {
            get { return this.m_Hits; }
            set { this.m_Hits = value; }
        }
        public string Template
        {
            get { return this.m_Template; }
            set { this.m_Template = value; }
        }
        public int Status
        {
            get { return this.m_Status; }
            set { this.m_Status = value; }
        }
        public bool IsNull
        {
            get { return this.m_IsNull; }
        }
    }
}