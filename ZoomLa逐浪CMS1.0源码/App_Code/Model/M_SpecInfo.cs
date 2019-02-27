namespace ZoomLa.Model
{
    using System;

    /// <summary>
    /// 专题内容表
    /// </summary>
    public class M_SpecInfo
    {
        private int m_SpecInfoID;
        private int m_SpecialID;
        private int m_InfoID;
        private bool m_IsNull;

        public M_SpecInfo()
        {            
        }
        public M_SpecInfo(bool value)
        {
            this.m_IsNull = value;
        }
        /// <summary>
        /// 专题内容表ID
        /// </summary>
        public int SpecInfoID
        {
            get { return this.m_SpecInfoID; }
            set { this.m_SpecInfoID = value; }
        }
        /// <summary>
        /// 专题ID
        /// </summary>
        public int SpecialID
        {
            get { return this.m_SpecialID; }
            set { this.m_SpecialID = value; }
        }
        /// <summary>
        /// 内容ID
        /// </summary>
        public int InfoID
        {
            get { return this.m_InfoID; }
            set { this.m_InfoID = value; }
        }
        /// <summary>
        /// 是否为空对象
        /// </summary>
        public bool IsNull
        {
            get { return this.m_IsNull; }
        }
    }
}