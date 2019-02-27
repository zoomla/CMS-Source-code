using System;

namespace ZoomLa.Model
{
    /// <summary>
    /// 采集页面规则模型
    /// </summary>
    public class M_CollectPage
    {
        public M_CollectPage(bool value) { this.m_IsNull = value; }
        private bool m_IsNull = false;
        public M_CollectPage() { }
        /// <summary>
        /// 规则ID
        /// </summary>
        public int RuleID
        {
            get;
            set;
        }
        /// <summary>
        /// 所属项目ID
        /// </summary>
        public int ItemID
        {
            get;
            set;
        }
        public string Filter
        {
            get;
            set;
        }
        /// <summary>
        /// 是否内容列表，只在目标网址是内容页时有用
        /// </summary>
        public bool IsList
        {
            get;
            set;
        }
        /// <summary>
        /// 列表开始代码
        /// </summary>
        public string ListStart
        {
            get;
            set;
        }
        /// <summary>
        /// 列表结束代码
        /// </summary>
        public string ListEnd
        {
            get;
            set;
        }
        /// <summary>
        /// 链接开始代码
        /// </summary>
        public string LinkStart
        {
            get;
            set;
        }
        /// <summary>
        /// 链接结束代码
        /// </summary>
        public string LinkEnd
        {
            get;
            set;
        }        
        /// <summary>
        /// 是否检测通过
        /// </summary>
        public bool Detection
        {
            get;
            set;
        }        
        /// <summary>
        /// 是否为空
        /// </summary>
        public bool IsNull
        {
            get { return m_IsNull; }
        }
    }
}
