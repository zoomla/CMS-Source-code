using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using ZoomLa.SQLDAL;
//用于生成链接,Html控件
public class MVCHelper
{
    /// <summary>
    /// 用于form表单
    /// </summary>
    /// <param name="action">要跳转的action,不带?号</param>
    /// <param name="Request">当前页面Request</param>
    /// <param name="param">需要附加的参数,不要以?或&开头</param>
    public static string GetAction(string action, HttpRequestBase Request, string param = "")
    {
        string query = Request.QueryString.ToString();
        if (!string.IsNullOrEmpty(query)) { query = "?" + HttpUtility.UrlDecode(query); }
        action = action + query;
        if (!string.IsNullOrEmpty(param))
        {
            action = string.IsNullOrEmpty(query) ? action + "?" + param : action + "&" + param;
        }
        return action;
    }
    public static MvcHtmlString H_Radios(string name, string[] texts, string[] values, string selected)
    {
        string html = "";
        for (int i = 0; i < texts.Length; i++)
        {
            string text = texts[i];
            string value = values[i];
            string chked = "";
            if (string.IsNullOrEmpty(selected) && i == 0) { chked = "checked=\"checked\""; }
            else if (value.Equals(selected)) { chked = "checked=\"checked\""; }
            html += "<label><input type=\"radio\" name=\"" + name + "\" value=\"" + value + "\" " + chked + ">" + text + "</label>";
        }
        return MvcHtmlString.Create(html);
    }
    public static MvcHtmlString H_Radios(string name, DataTable dt, string vfield, string tfield, string selected = "")
    {
        string html = "";
        if (dt == null || dt.Rows.Count < 1) {return MvcHtmlString.Create("<div id=\""+name+"\">无选项</div>"); }
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataRow dr = dt.Rows[i];
            string text = DataConvert.CStr(dr[tfield]);
            string value = DataConvert.CStr(dr[vfield]);
            string chked = "";
            if (string.IsNullOrEmpty(selected) && i == 0) { chked = "checked=\"checked\""; }
            else if (value.Equals(selected)) { chked = "checked=\"checked\""; }
            html += "<label><input type=\"radio\" name=\"" + name + "\" value=\"" + value + "\" " + chked + ">" + text + "</label>";
        }
        return MvcHtmlString.Create(html);
    }
    public static SelectList ToSelectList(DataTable dt, string text, string value, string selected = "")
    {
        List<SelectListItem> list = new List<SelectListItem>();
        foreach (DataRow dr in dt.Rows)
        {
            list.Add(new SelectListItem() { Text = DataConvert.CStr(dr[text]), Value = DataConvert.CStr(dr[value]) });
        }
        SelectList slist = new SelectList(list, "Value", "Text", selected);
        return slist;
    }
    public static SelectList ToSelectList(string[] textArr, string[] valueArr, string seled = "")
    {
        List<SelectListItem> list = new List<SelectListItem>();
        for (int i = 0; i < textArr.Length; i++)
        {
            string text = textArr[i];
            string value = valueArr[i];
            list.Add(new SelectListItem() { Text = text, Value = value });
        }
        SelectList slist = new SelectList(list, "Value", "Text", "");
        return slist;
    }
    public static string To(string name)
    {
        return "@" + name;
    }
}