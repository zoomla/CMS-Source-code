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
    public partial class StructTree : System.Web.UI.UserControl
    {
        B_Structure strBll = new B_Structure();
        //----节点
        string path = CustomerPageAction.customPath2 + "AddOn/";
        string hasChild, noChild;
        protected void Page_Load(object sender, EventArgs e)
        {
            hasChild = "<a href='" + path + "StructMenber.aspx?ID={0}' target='main_right' id='a{0}' class='list1'>{2}<span class='list_span'>{1}</span></a>";
            noChild = "<a href='" + path + "StructMenber.aspx?ID={0}' target='main_right'>{2}{1}</a>";
            BindNode();
        }
        protected void BindNode()
        {
            DataTable dt = strBll.Sel();
            nodeHtml.Text = "<ul class='tvNav'><li><a  class='list1' id='a0' href='" + path + "StructList.aspx' target='main_right'><span class='list_span'>全部组织结构</span><span class='fa fa-list'></span></a>" + B_Node.GetLI(dt, hasChild, noChild) + "</li></ul>";
        }
    }
}