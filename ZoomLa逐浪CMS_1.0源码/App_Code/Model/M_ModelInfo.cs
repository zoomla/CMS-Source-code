namespace ZoomLa.Model
{
    using System;

    /// <summary>
    /// 内容模型信息
    /// </summary>
    public class M_ModelInfo
    {
        //模型ID
        private int m_ModelID;
        //模型名称
        private string m_ModelName;
        //模型描述
        private string m_Description;
        //模型内容存储表名
        private string m_TableName;
        //项目名称：如文章、新闻
        private string m_ItemName;
        //项目单位：如篇、条
        private string m_ItemUnit;
        //项目图标
        private string m_ItemIcon;
        /// <summary>
        /// 
        /// </summary>
        private int m_ModelType;
        /// <summary>
        /// 内容模板
        /// </summary>
        private string m_ContentModule;

        private bool m_IsNull;

        public M_ModelInfo()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        public M_ModelInfo(bool s)
        {
            this.m_IsNull = s;
        }
        public int ModelID
        {
            get
            {
                return this.m_ModelID;
            }
            set
            {
                this.m_ModelID = value;
            }
        }
        public string ModelName
        {
            get
            {
                return this.m_ModelName;
            }
            set
            {
                this.m_ModelName = value;
            }
        }
        public string Description
        {
            get
            {
                return this.m_Description;
            }
            set
            {
                this.m_Description = value;
            }
        }
        public string TableName
        {
            get
            {
                return this.m_TableName;
            }
            set
            {
                this.m_TableName = value;
            }
        }
        public string ItemName
        {
            get
            {
                return this.m_ItemName;
            }
            set
            {
                this.m_ItemName = value;
            }
        }
        public string ItemUnit
        {
            get
            {
                return this.m_ItemUnit;
            }
            set
            {
                this.m_ItemUnit=value;
            }
        }
        public string ItemIcon
        {
            get { return this.m_ItemIcon; }
            set { this.m_ItemIcon = value; }
        }
        public int ModelType
        {
            get { return this.m_ModelType; }
            set { this.m_ModelType = value; }
        }
        public string ContentModule
        {
            get { return this.m_ContentModule; }
            set { this.m_ContentModule = value; }
        }
        public bool IsNull
        {
            get { return this.m_IsNull; }
        }
    }
}