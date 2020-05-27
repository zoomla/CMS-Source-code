using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ZoomLa.SQLDAL.SQL
{
    public class PageSetting
    {
        private int _psize = 10, _cpage = 1;
        private string _order = "", _where = "", _fields = "*", _on = "";
        public string pk = "ID";
        public string t1, t2;
        public int psize { get { return (_psize > 0 ? _psize : 10); } set { _psize = value; } }
        /// <summary>
        /// 使用时需要-1,程序是从0开始
        /// </summary>
        public int cpage { get { return (_cpage < 1 ? 1 : _cpage); } set { _cpage = value; } }
        public int cursize { get { return (psize * (cpage - 1)); } }
        /// <summary>
        /// 不需要加ORDER BY
        /// </summary>
        public string order
        {
            get
            {
                if (string.IsNullOrEmpty(_order)) { return ""; }
                else { return " ORDER BY " + _order; }
            }
            set { _order = value; }
        }
        public string where { get { return (string.IsNullOrEmpty(_where) ? " 1=1 " : " " + _where); } set { _where = value; } }
        public string fields { get { return " " + _fields; } set { _fields = value; } }
        public string on { get { return " " + _on; } set { _on = value; } }
        /// <summary>
        /// 默认为 LEFT JOIN
        /// </summary>
        public string join = "LEFT JOIN";
        //public SqlParameter[] sp = new SqlParameter[0];
        public SqlParameter[] sp
        {
            get { return (spList == null) ? null : spList.ToArray(); }
            set
            {
                if (value == null) { spList = null; }
                else { spList.Clear(); spList.AddRange(value); }
            }
        }
        public List<SqlParameter> spList = new List<SqlParameter>();
        //---------------------配置
        /// <summary>
        /// 分页方法,默认主键分页(pk|row)
        /// </summary>
        public string pageMethod = "pk";
        public string T1Alias = "A";
        public string T2Alias = "B";
        public bool debug = false;
        //---------------------返回
        /// <summary>
        /// 返回的数据数量,页数,和执行的sql
        /// </summary>
        public int itemCount = 1;
        public int pageCount = 1;
        public DataTable dt = null;
        public string sql = "";
        public string countsql = "";//用于计总数的SQL
        public string addon = "";
        //--------------------分页辅助
        /// <summary>
        /// 行数,td分而需要该参数
        /// </summary>
        public int cols = 1;
        public string target = "EGV";//需要更新的目标对象ID
        /// <summary>
        /// 点击按钮提交的链接(AJAX|路由)
        /// </summary>
        public string url = "";
        public string unit = "条记录";
        ///// <summary>
        ///// 搜索模式,用于限定搜索
        ///// </summary>
        //public string skeymode = "";
        ///// <summary>
        ///// 多项以|隔开
        ///// </summary>
        //public string skey = "";
        /// <summary>
        /// 是否显示全选
        /// </summary>
        public bool Check = true;
        //--------------------辅助方法
        /// <summary>
        /// 主用于标签解析,可自定义别名
        /// </summary>
        public void DealWithAlias()
        {
            if (!T1Alias.Equals("A"))
            {
                sql = sql.Replace(" A ", " " + T1Alias + " ");
                countsql = countsql.Replace(" A ", " " + T1Alias + " ");
            }
            if (!T2Alias.Equals("B"))
            {
                sql = sql.Replace(" B ", " " + T2Alias + " ");
                countsql = countsql.Replace(" B ", " " + T2Alias + " ");
            }
        }
        public static PageSetting Single(int cpage, int psize, string tbname, string pk, string where, string order = "", List<SqlParameter> sp = null)
        {
            if (string.IsNullOrEmpty(order)) { order = pk + " DESC"; }
            return new PageSetting()
            {
                cpage = cpage,
                psize = psize,
                t1 = tbname,
                pk = pk,
                where = where,
                order = order,
                spList = sp
            };
        }
        public static PageSetting Double(int cpage, int psize, string t1, string t2, string pk, string on, string where, string order = "", List<SqlParameter> sp = null)
        {
            if (string.IsNullOrEmpty(order)) { order = pk + " DESC"; }
            return new PageSetting()
            {
                cpage = cpage,
                psize = psize,
                t1 = t1,
                t2 = t2,
                pk = pk,
                on = on,
                where = where,
                order = order,
                spList = sp
            };
        }
    }
}
