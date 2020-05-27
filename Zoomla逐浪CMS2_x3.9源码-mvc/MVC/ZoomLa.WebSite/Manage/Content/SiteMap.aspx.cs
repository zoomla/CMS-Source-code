using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Components;
using System.Text;
using System.Xml;
using System.Collections.Generic;

namespace ZoomLaCMS.Manage.Content
{
    public partial class SiteMap : CustomerPageAction
    {
        B_SiteMap bs = new B_SiteMap();//定义一个对象 bs,继承 B_SiteMap 的方法

        protected void Page_Load(object sender, EventArgs e)
        {
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='ContentManage.aspx'>内容管理</a></li> <li><a href='CreateHtmlContent.aspx'>生成发布</a></li><li><a href='" + Request.RawUrl + "'>站点地图</a></li>");
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            int num = DataConverter.CLng(TextBox1.Text);
            string changefreqs = DropDownList1.SelectedValue;
            string prioritys = TextBox2.Text;
            Response.Write("<script>location.href='Makexml.aspx?type=google&num=" + num + "&changefreq=" + changefreqs + "&priority=" + prioritys + "';</script>");


        }

        protected void Submit1_Click(object sender, EventArgs e)
        {
            string num = prioritynum.Text;
            string updatePeris = changefreq.Text;
            Response.Write("<script>location.href='Makexml.aspx?type=baidu&num=" + num + "&updatePeri=" + updatePeris + "';</script>");
        }
    }
}