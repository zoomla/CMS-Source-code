namespace ZoomLa.Model
{
    using System;

    /// <summary>
    /// UserModel 的摘要说明
    /// </summary>
    public class M_UserModel
    {
        private int m_ModelID;
        private string m_ModelName;
        private string m_Description;
        private string m_TableName;
        private bool m_IsNull;

        public M_UserModel()
        {            
        }
        public M_UserModel(bool flag)
        {
            this.m_IsNull = flag;
        }
        /// <summary>
        /// 会员模型ID
        /// </summary>
        public int ModelID
        {
            get { return this.m_ModelID; }
            set { this.m_ModelID = value; }
        }
        /// <summary>
        /// 会员模型名
        /// </summary>
        public string ModelName
        {
            get { return this.m_ModelName; }
            set { this.m_ModelName = value; }
        }
        /// <summary>
        /// 会员模型注释
        /// </summary>
        public string Description
        {
            get { return this.m_Description; }
            set { this.m_Description = value; }
        }        
        /// <summary>
        /// 存储表名
        /// </summary>
        public string TableName
        {
            get { return this.m_TableName; }
            set { this.m_TableName = value; }
        }
        /// <summary>
        /// 是否空对象
        /// </summary>
        public bool IsNull
        {
            get { return this.m_IsNull; }
        }
    }
}