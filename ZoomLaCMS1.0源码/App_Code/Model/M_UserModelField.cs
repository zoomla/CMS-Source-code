namespace ZoomLa.Model
{
    using System;

    /// <summary>
    /// 模型字段信息
    /// </summary>
    public class M_UserModelField
    {
        //字段ID
        private int m_FieldID;
        //所属模型ID
        private int m_ModelId;
        //字段名(建立在数据库表中的字段名)
        private string m_FieldName;
        //字段别名
        private string m_FieldAlias;        
        //字段描述
        private string m_Description;
        //是否必填
        private bool m_IsNotNull;
        //是否用于搜索表单
        private bool m_IsSearchForm;
        //字段类型
        private string m_FieldType;
        //存储字段的一些输入界面设置
        private string m_FieldSetting;
        //排列序号
        private int m_OrderId;
        //
        private bool m_IsNull;

        public M_UserModelField()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        public M_UserModelField(bool flag)
        {
            this.m_IsNull = flag;
        }
        /// <summary>
        /// 字段ID
        /// </summary>
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
        /// <summary>
        /// 所属模型ID
        /// </summary>
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
        /// <summary>
        /// 字段名(建立在数据库表中的字段名)
        /// </summary>
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
        /// <summary>
        /// 字段别名
        /// </summary>
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
        /// <summary>
        /// 字段描述
        /// </summary>
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
        /// <summary>
        /// 是否必填
        /// </summary>
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
        /// <summary>
        /// 是否用于搜索表单
        /// </summary>
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
        /// <summary>
        /// 字段类型
        /// </summary>
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
        /// <summary>
        /// 
        /// </summary>
        public string FieldSetting
        {
            get
            {
                return this.m_FieldSetting;
            }
            set
            {
                this.m_FieldSetting = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
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
        /// <summary>
        /// 
        /// </summary>
        public bool IsNull
        {
            get
            {
                return this.m_IsNull;
            }
        }
    }
}