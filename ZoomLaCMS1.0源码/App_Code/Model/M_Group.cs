namespace ZoomLa.Model
{
    using System;

    /// <summary>
    /// 会员组类
    /// </summary>
    public class M_Group
    {
        private int m_GroupID;
        private string m_GroupName;
        private string m_Description;
        private bool m_RegSelect;
        private bool m_IsNull;

        public M_Group()
        {            
        }
        public M_Group(bool flag)
        {
            this.m_IsNull = flag;
        }
        /// <summary>
        /// 会员组ID
        /// </summary>
        public int GroupID
        {
            get { return this.m_GroupID; }
            set { this.m_GroupID = value; }
        }
        /// <summary>
        /// 会员组名
        /// </summary>
        public string GroupName
        {
            get { return this.m_GroupName; }
            set { this.m_GroupName = value; }
        }
        /// <summary>
        /// 会员组说明
        /// </summary>
        public string Description
        {
            get { return this.m_Description; }
            set { this.m_Description = value; }
        }
        /// <summary>
        /// 注册时是否可选
        /// </summary>
        public bool RegSelect
        {
            get { return this.m_RegSelect; }
            set { this.m_RegSelect = value; }
        }
        /// <summary>
        /// 是否为空
        /// </summary>
        public bool IsNull
        {
            get { return this.m_IsNull; }
        }
    }
}