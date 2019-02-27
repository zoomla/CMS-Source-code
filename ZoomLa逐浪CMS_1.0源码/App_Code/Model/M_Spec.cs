using System;
namespace ZoomLa.Model
{
    /// <summary>
    /// 专题Model
    /// </summary>
    public class M_Spec
    {
        private int m_SpecID;
        private string m_SpecName;
        private string m_SpecDir;
        private string m_SpecDesc;
        private bool m_OpenNew;
        private int m_SpecCate;
        private int m_ListFileExt;
        private string m_ListTemplate;
        private int m_ListFileRule;
        private bool m_IsNull;

        public M_Spec()
        {            
        }
        public M_Spec(bool value)
        {
            this.m_IsNull = value;
        }
        /// <summary>
        /// 专题ID
        /// </summary>
        public int SpecID
        {
            get { return this.m_SpecID; }
            set { this.m_SpecID = value; }
        }
        /// <summary>
        /// 专题名称
        /// </summary>
        public string SpecName
        {
            get { return this.m_SpecName; }
            set { this.m_SpecName = value; }
        }
        /// <summary>
        /// 专题目录
        /// </summary>
        public string SpecDir
        {
            get { return this.m_SpecDir; }
            set { this.m_SpecDir = value; }
        }
        /// <summary>
        /// 专题说明
        /// </summary>
        public string SpecDesc
        {
            get { return this.m_SpecDesc; }
            set { this.m_SpecDesc = value; }
        }
        /// <summary>
        /// 是否在新窗口打开
        /// </summary>
        public bool OpenNew
        {
            get { return this.m_OpenNew; }
            set { this.m_OpenNew = value; }
        }
        /// <summary>
        /// 所属专题类别ID
        /// </summary>
        public int SpecCate
        {
            get { return this.m_SpecCate; }
            set { this.m_SpecCate = value; }
        }
        /// <summary>
        /// 扩展名index
        /// </summary>
        public int ListFileExt
        {
            get { return this.m_ListFileExt; }
            set { this.m_ListFileExt = value; }
        }
        /// <summary>
        /// 列表页模板
        /// </summary>
        public string ListTemplate
        {
            get { return this.m_ListTemplate; }
            set { this.m_ListTemplate = value; }
        }
        /// <summary>
        /// 列表页文件名规则
        /// </summary>
        public int ListFileRule
        {
            get { return this.m_ListFileRule; }
            set { this.m_ListFileRule = value; }
        }
        /// <summary>
        /// 是否是空对象
        /// </summary>
        public bool IsNull
        {
            get { return this.m_IsNull; }
        }
    }
}