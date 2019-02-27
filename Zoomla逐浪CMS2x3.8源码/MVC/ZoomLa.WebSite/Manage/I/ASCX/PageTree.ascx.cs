using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;

namespace ZoomLaCMS.Manage.I.ASCX
{
    public partial class PageTree : System.Web.UI.UserControl
    {
        string path = CustomerPageAction.customPath2 + "Page/";
        B_Templata tlpBll = new B_Templata();
        protected void Page_Load(object sender, EventArgs e)
        {
            string hasChild = "<a href='" + path + "PageContent.aspx?TemplateID={0}' target='main_right' id='a{0}' class='list1' >{2}<span class='list_span'>{1}</span></a>";
            string noChild = "<a href='" + path + "PageContent.aspx?TemplateID={0}' target='main_right'>{2}{1}</a>";
            DataTable nodeDT = tlpBll.Sel();
            string head = "<ul class='tvNav'><li class='menu_tit'><span class='fa fa-chevron-down'></span>黄页内容管理</li>";
            head += "<li><a class='list1' id='a0' href='" + path + "PageContent.aspx' target='main_right' ><span class='list_span'>全部节点</span><span class='fa fa-list'></span></a>";
            nodeHtml.Text = head + B_Node.GetLI(nodeDT, hasChild, noChild) + "</li></ul>";
        }
    }
}