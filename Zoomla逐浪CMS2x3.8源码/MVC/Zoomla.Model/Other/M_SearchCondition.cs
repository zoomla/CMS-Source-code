using System;
using System.Collections.Generic;
using System.Text;

namespace ZoomLa.Model
{
    /// <summary>
    /// 查询条件对象
    /// </summary>
    [Serializable]
    public class M_SearchCondition
    {
        /// <summary>
        /// 查询条件对象构造函数
        /// </summary>
        /// <param name="fieldname">查询条件字段名称</param>
        /// <param name="fieldtype">查询条件字段类型 为创建参数使用</param>
        /// <param name="svalue">查询条件字段输入值</param>
        /// <param name="stype">查询类型 1=精确查询或日期时间的上下限 0=模糊查询或范围查询</param>
        /// <param name="rangtype">查询范围类别 1=没有或上下限查询 0=自定义范围</param>
        public M_SearchCondition(string fieldname, string fieldtype, string svalue, int stype, int rangtype,string tablename)
        {
            this.FieldName = fieldname;
            this.FieldType = fieldtype;
            this.SearchValue = svalue;
            this.SearchType = stype;
            this.RangeType = rangtype;
            this.TableName = tablename;
        }
        /// <summary>
        /// 查询条件对象不带参数构造函数
        /// </summary>
        public M_SearchCondition()
        {
            this.FieldName = "";
            this.FieldType = "";
            this.SearchValue = "";
            this.SearchType = 0;
            this.RangeType = 1;
            this.TableName = "";
        }
        /// <summary>
        /// 查询条件字段名称
        /// </summary>
        public string FieldName { get; set; }
        /// <summary>
        /// 查询条件字段类型 为创建参数使用
        /// </summary>
        public string FieldType { get; set; }
        /// <summary>
        /// 查询条件字段输入值
        /// </summary>
        public string SearchValue { get; set; }
        /// <summary>
        /// 查询类型 1=精确查询或日期时间的上下限 0=模糊查询或范围查询
        /// </summary>
        public int SearchType { get; set; }
        /// <summary>
        /// 查询范围类别 1=没有或上下限查询 0=自定义范围
        /// </summary>
        public int RangeType { get; set; }
        public string TableName { get; set; }
    }
}
