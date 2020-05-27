using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace BDUModel
{
   
    [Serializable]
    public class PagePagination
    {
        private int pageSize=10;

        /// <summary>
        /// 页面大小
        /// </summary>
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value; }
        }

        private int pageIndex=1;

        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex
        {
            get { return pageIndex; }
            set { pageIndex = value; }
        }
       
        private  Dictionary<string,string> pageOrder;

        /// <summary>
        /// 排序，可以按多列排序，0降序，1升序
        /// </summary>
        public Dictionary<string, string> PageOrder
        {
            get { return pageOrder; }
            set { pageOrder = value; }
        }


        /// <summary>
        /// 构造分页语句
        /// </summary>
        /// <param name="sqlStr">原始sql</param>
        /// <returns>分页功能sql</returns>
        public string PaginationSql(string sqlStr)
        {
            string order = "ORDER BY";
            int odercount = 0;
            foreach (string key in PageOrder.Keys)
            {
                if (odercount != 0)
                    order += " , ";
                if (PageOrder[key].ToString() == "0")
                    order += " " + key + " " + "DESC";
                else
                    order += " " + key;
                odercount++;
                
            }
            int beginPage = pageIndex * pageSize - pageSize + 1;
            StringBuilder pagSql = new StringBuilder();
            pagSql.Append(@"SELECT * FROM (");
            pagSql.Append(@"SELECT Row_Number() over("+order+") AS rowNum,* ");
            pagSql.Append(@"FROM (" + sqlStr + ") as yy) as tt");
            pagSql.Append(@" WHERE rowNum BETWEEN  ");
            pagSql.Append(beginPage + " AND " + pageIndex * pageSize);
            pagSql.Append(order);
            return pagSql.ToString();
        }

    }
}
