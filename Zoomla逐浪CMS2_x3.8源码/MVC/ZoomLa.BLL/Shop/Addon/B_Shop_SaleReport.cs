using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZoomLa.BLL.Shop
{
    public class B_Shop_SaleReport
    {
        /// <summary>
        /// 0为全部
        /// </summary>
        public string Html_Date(string type, string value, bool hasall = false)
        {
            string yearTlp = "<button type=\"button\"  class='btn btn-default {0} filter_year' data-num='{1}'>{1}年</a>";
            string monthTlp = "<button type=\"button\" class='btn btn-default {0} filter_month' data-num='{1}'>{1}月</a>";
            string html = "";
            switch (type)
            {
                case "year":
                    for (int i = 0; i <= 11; i++)
                    {
                        bool ischk = false;
                        int val = (DateTime.Now.Year - i);
                        if (value == val.ToString()) { ischk = true; }
                        html += string.Format(yearTlp, ischk ? "active" : "", val);
                    }
                    if (hasall) { html = "<button type=\"button\"  class='btn btn-default "+(value=="0"?"active":"")+" filter_year' data-num='0'>全部</a>" + html; }
                    break;
                case "month":
                    for (int i = 1; i <= 12; i++)
                    {
                        bool ischk = false;
                        if (value == i.ToString()) { ischk = true; }
                        html += string.Format(monthTlp, ischk ? "active" : "", i);
                    }
                    if (hasall) { html = "<button type=\"button\" class='btn btn-default "+(value=="0"?"active":"")+" filter_month' data-num='0'>全部</a>" + html; }
                    break;
            }
            return html;
        }
    }
}
