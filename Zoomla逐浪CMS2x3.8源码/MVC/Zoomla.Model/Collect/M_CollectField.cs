using System;

namespace ZoomLa.Model
{
    public class M_CollectField
    {
        public M_CollectField(bool value) { this.m_IsNull = value; }
        private bool m_IsNull;
        public M_CollectField() { }

        /// <summary>
        /// 字段规则ID
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
        /// <summary>
        /// 字段名
        /// </summary>
        public string FieldName
        {
            get;
            set;
        }
        /// <summary>
        /// 字段类型
        /// </summary>
        public string FieldType
        {
            get;
            set;
        }
        /// <summary>
        /// 规则类型 0默认值 1指定值 2采集规则
        /// </summary>
        public int RuleType
        {
            get;
            set;
        }
        /// <summary>
        /// 开始代码
        /// </summary>
        public string StartCode
        {
            get;
            set;
        }
        /// <summary>
        /// 结束代码
        /// </summary>
        public string EndCode
        {
            get;
            set;
        }
        /// <summary>
        /// 过滤代码
        /// </summary>
        public string Filter
        {
            get;
            set;
        }
        /// <summary>
        /// 是否采集分页
        /// </summary>
        public bool PageSplit
        {
            get;
            set;
        }
        /// <summary>
        /// 分页采集设定
        /// </summary>
        public string Setting
        {
            get;
            set;
        }
        /// <summary>
        /// 是否空实例
        /// </summary>
        public bool IsNull
        {
            get { return m_IsNull; }
        }
    }
}
