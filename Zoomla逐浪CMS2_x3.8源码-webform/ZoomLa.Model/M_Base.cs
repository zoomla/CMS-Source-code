using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;

namespace ZoomLa.Model
{
    [Serializable]
    public abstract class M_Base
    {
        private string _pk = "ID", _tbname = "";
        public virtual string PK { get { return _pk; } set { _pk = value; } }
        public virtual string GetPK() { return PK; }
        public virtual string TbName { get { return _tbname; } set { _tbname = value; } }
        public abstract string[,] FieldList();
        public virtual SqlParameter[] GetParameters() { throw new Exception("未实现GetParameters"); }
        //public virtual M_Base GetModelFromReader(DbDataReader reader) { throw new Exception("未实现GetModelFromReader"); }
        //-----------------------------------------
        protected string ConverToStr(object o)
        {
            return (o != null && o != DBNull.Value) ? o.ToString() : "";
        }
        protected int ConvertToInt(object o, int def = 0)
        {
            double r = def;
            if (o != null && o != DBNull.Value)
            {
                //用于避免浮点数转换失败
                if (!double.TryParse(o.ToString(), out r)) { r = def; }
            }
            return (int)r;//Convert.ToInt();(4舍五入)
        }
        protected double ConverToDouble(object o)
        {
            double r = 0;
            if (o != null && o != DBNull.Value)
            {
                double.TryParse(o.ToString(), out r);
            }
            return r;
        }
        protected decimal ConverToDec(object o) 
        {
            decimal r = 0;
            if (o != null && o != DBNull.Value)
            {
                decimal.TryParse(o.ToString(), out r);
            }
            return r;
        }
        protected bool ConverToBool(object o)
        {
            bool r = false;
            if (o != null && o != DBNull.Value)
            {
                string str = o.ToString().ToLower();
                if (str.Equals("1")||str.Equals("true")) { return true; }
                if (str.Equals("0") || str.Equals("false")) { return false; }
                bool.TryParse(o.ToString(), out r);
            }
            return r;
        }
        protected DateTime ConvertToDate(object o)
        {
            DateTime date = DateTime.Now;
            if (o != null && o != DBNull.Value)
            {
                DateTime.TryParse(o.ToString(), out date);
            }
            return date;
        }
        /// <summary>
        /// 获取值,为空或不存在,则返回空字符串
        /// </summary>
        protected string GetVal(DbDataReader rdr,string name) 
        {
            try { return ConverToStr(rdr[name]); }
            catch (Exception) { return ""; }
        }
        protected string GetVal(DataRow rdr, string name)
        {
            if (rdr.Table.Columns[name] != null)
            {
                return ConverToStr(rdr[name]);
            }
            else { return ""; }
        }
        //-----------------------------------------
        protected SqlParameter[] GetSP()
        {
            string[,] strArr = FieldList();
            int len = strArr.GetLength(0);
            SqlParameter[] sp = new SqlParameter[len];
            for (int i = 0; i < len; i++)
            {
                sp[i] = new SqlParameter("@" + strArr[i, 0], (SqlDbType)Enum.Parse(typeof(SqlDbType), strArr[i, 1]), Convert.ToInt32(strArr[i, 2]));
            }
            return sp;
        }
        protected string SafeStr(string o)
        {
            if (string.IsNullOrEmpty(o)) { return ""; }
            //替换字符串的特殊符号,避免用户输入网址等,主用于用户名,真实姓名与昵称
            string[] chars = { "&", ".", "<", ">", ":", "：",";", ",", "/", "\\", "?", "@", "(", ")", "[", "]", "+", "=", "_", "-", "!", "%" };
            for (int i = 0; i < chars.Length; i++)
            {
                o = o.Replace(chars[i], "");
            }
            return o;
        }
    }
}
