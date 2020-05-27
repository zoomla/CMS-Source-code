using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Components;


namespace ZoomLaCMS.Manage.Template
{
    public partial class Design : System.Web.UI.Page
    {
        public string VPath { get { return Request.QueryString["vpath"]; } }
        //public string VPath = "%2fTemplate%2fV3%2f%E5%85%A8%E7%AB%99%E9%A6%96%E9%A1%B5.html";
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Save_Btn_Click(object sender, EventArgs e)
        {
            string head = HttpUtility.UrlDecode(Head_Hid.Value);
            string html = AllHtml_Hid.Value;
            int start = html.IndexOf(Call.Boundary) + Call.Boundary.Length;
            int len = html.Length - start;
            html = "<body>" + html.Substring(start, len);//处理iframe中标签错位Bug
            head = head.Replace(Call.Boundary, html);
            SafeSC.WriteFile(HttpUtility.UrlDecode(VPath), head);
            function.WriteSuccessMsg("模板保存成功");
        }
    }
}