namespace ZoomLa.Model
{
    using System;

    /// <summary>
    /// 模型字段信息
    /// </summary>
    public class M_ModelField
    {
        //字段ID
        private int m_FieldID;
        //所属模型ID
        private int m_ModelId;
        //字段名(建立在数据库表中的字段名)
        private string m_FieldName;
        //字段别名
        private string m_FieldAlias;
        //字段提示
        private string m_FieldTips;
        //字段描述
        private string m_Description;
        //是否必填
        private bool m_IsNotNull;
        //是否用于搜索表单
        private bool m_IsSearchForm;
        //字段类型
        private string m_FieldType;        
        //存储字段的一些输入界面设置
        private string m_Content;
        //排列序号
        private int m_OrderId;
        //
        private bool m_IsNull;

        public M_ModelField()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        public M_ModelField(bool flag)
        {
            this.m_IsNull = flag;
        }
        public int FieldID
        {
            get
            {
                return this.m_FieldID;
            }
            set
            {
                this.m_FieldID = value;
            }
        }
        public int ModelID
        {
            get
            {
                return this.m_ModelId;
            }
            set
            {
                this.m_ModelId = value;
            }
        }
        public string FieldName
        {
            get
            {
                return this.m_FieldName;
            }
            set
            {
                this.m_FieldName = value;
            }
        }
        public string FieldAlias
        {
            get
            {
                return this.m_FieldAlias;
            }
            set
            {
                this.m_FieldAlias = value;
            }
        }
        public string FieldTips
        {
            get
            {
                return this.m_FieldTips;
            }
            set
            {
                this.m_FieldTips = value;
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
        public bool IsNotNull
        {
            get
            {
                return this.m_IsNotNull;
            }
            set
            {
                this.m_IsNotNull = value;
            }
        }
        public bool IsSearchForm
        {
            get
            {
                return this.m_IsSearchForm;
            }
            set
            {
                this.m_IsSearchForm = value;
            }
        }
        public string FieldType
        {
            get
            {
                return this.m_FieldType;
            }
            set
            {
                this.m_FieldType = value;
            }
        }
        public string Content
        {
            get
            {
                return this.m_Content;
            }
            set
            {
                this.m_Content = value;
            }
        }
        public int OrderID
        {
            get
            {
                return this.m_OrderId;
            }
            set
            {
                this.m_OrderId = value;
            }
        }
        public bool IsNull
        {
            get
            {
                return this.m_IsNull;
            }
        }
    }
}