using System;
namespace ZoomLa.Model
{
    /// <summary>
    /// 专题类别Model
    /// </summary>
    public class M_SpecCate
    {
        private int m_SpecCateID;
        private string m_SpecCateName;
        private string m_SpecCateDir;
        private string m_SpecCateDesc;
        private bool m_OpenNew;
        private int m_FileExt;
        private string m_ListTemplate;
        private bool m_IsNull;

        public M_SpecCate()
        {            
        }
        public M_SpecCate(bool value)
        {
            this.m_IsNull = value;
        }
        /// <summary>
        /// 专题类别ID
        /// </summary>
        public int SpecCateID
        {
            get { return this.m_SpecCateID; }
            set { this.m_SpecCateID = value; }
        }
        /// <summary>
        /// 专题类别名
        /// </summary>
        public string SpecCateName
        {
            get { return this.m_SpecCateName; }
            set { this.m_SpecCateName = value; }
        }
        /// <summary>
        /// 专题类别目录
        /// </summary>
        public string SpecCateDir
        {
            get { return this.m_SpecCateDir; }
            set { this.m_SpecCateDir = value; }
        }
        /// <summary>
        /// 专题类别说明
        /// </summary>
        public string SpecCateDesc
        {
            get { return this.m_SpecCateDesc; }
            set { this.m_SpecCateDesc = value; }
        }
        /// <summary>
        /// 专题类别列表打开方式
        /// </summary>
        public bool OpenNew
        {
            get { return this.m_OpenNew; }
            set { this.m_OpenNew = value; }
        }
        /// <summary>
        /// 列表页扩展名
        /// </summary>
        public int FileExt
        {
            get { return this.m_FileExt; }
            set { this.m_FileExt = value; }
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
        /// 是否是空对象
        /// </summary>
        public bool IsNull
        {
            get { return this.m_IsNull; }
        }
    }
}