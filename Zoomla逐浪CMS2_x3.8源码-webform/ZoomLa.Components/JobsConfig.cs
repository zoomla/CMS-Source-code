using System;
using System.Web;

namespace ZoomLa.Components
{
    /// <summary>
    /// 人才招聘模块配置
    /// </summary>
    [Serializable]
    public class JobsConfig
    {
        private bool m_IsUsed;
        private int m_CompanyGroup;
        private int m_PersonGroup;
        private int m_Resume;
        private int m_Company;
        private int m_CompanyField;
        private int m_CompanyJobs;
        private int m_JobsField;
        private int m_ConsumePoint;
        private int m_ConsumeType;
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsUsed
        {
            get { return this.m_IsUsed; }
            set { this.m_IsUsed = value; }
        }
        /// <summary>
        /// 企业用户所属用户组ID
        /// </summary>
        public int CompanyGroup
        {
            get { return this.m_CompanyGroup; }
            set { this.m_CompanyGroup = value; }
        }
        /// <summary>
        /// 个人用户所属用户组ID
        /// </summary>
        public int PersonGroup
        {
            get { return this.m_PersonGroup; }
            set { this.m_PersonGroup = value; }
        }
        /// <summary>
        /// 会员简历模型ID
        /// </summary>
        public int Resume
        {
            get { return this.m_Resume; }
            set { this.m_Resume = value; }
        }
        /// <summary>
        /// 企业会员信息模型
        /// 用于面试通知个人用户查看企业信息
        /// </summary>
        public int Company
        {
            get { return this.m_Company; }
            set { this.m_Company = value; }
        }
        /// <summary>
        /// 企业信息显示字段
        /// 用于面试通知个人用户查看企业信息的名称
        /// </summary>
        public int CompanyField
        {
            get { return this.m_CompanyField; }
            set { this.m_CompanyField = value; }
        }
        /// <summary>
        /// 招聘信息模型
        /// 用于定位招聘信息存储表
        /// </summary>
        public int CompanyJobs
        {
            get { return this.m_CompanyJobs; }
            set { this.m_CompanyJobs = value; }
        }
        /// <summary>
        /// 招聘信息职位字段
        /// 用于显示求职岗位名称显示
        /// </summary>
        public int JobsField
        {
            get { return this.m_JobsField; }
            set { this.m_JobsField = value; }
        }
        /// <summary>
        /// 每查看一次简历收费点数
        /// </summary>
        public int ConsumePoint
        {
            get { return this.m_ConsumePoint; }
            set { this.m_ConsumePoint = value; }
        }
        /// <summary>
        /// 查看简历消费模式
        /// </summary>
        public int ConsumeType
        {
            get { return this.m_ConsumeType; }
            set { this.m_ConsumeType = value; }
        }
        
    }
}
