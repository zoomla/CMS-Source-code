using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Common;
/*
 * 后整修改为使用ASPOSE导出,前端整理为Json
 */ 
public partial class Common_Label_OutToExcel : System.Web.UI.Page
{
    public string Html { get { return HttpUtility.UrlDecode(Request.Form["html_toword_hid"] ?? ""); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        string name = HttpUtility.UrlDecode(Request["Name"] ?? "");
        if (string.IsNullOrEmpty(name)) { name = function.GetRandomString(6); }
        name = HttpUtility.UrlPathEncode(name);
        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel.numberformat:@";
        this.EnableViewState = false;
        Response.Charset = "UTF-8";
        Response.ContentEncoding = System.Text.Encoding.GetEncoding("gb2312");//设置输出流为简体中文  
        Response.AppendHeader("Content-Disposition", "attachment;filename=" + name + ".xls");
        Response.Write(Html);
        Response.End();
    }
}