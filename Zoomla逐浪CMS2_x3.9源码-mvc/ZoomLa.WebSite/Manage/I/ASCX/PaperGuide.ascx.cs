using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Exam;

namespace ZoomLaCMS.Manage.I.ASCX
{
    public partial class PaperGuide : System.Web.UI.UserControl
    {
        B_Exam_PaperNode NodeBll = new B_Exam_PaperNode();
        //点击需要打开的目标页面,默认为内容管理页
        private string Url
        {
            get
            {
                string _url = this.Attributes["Url"] == null ? "Exam/Papers_System_Manage.aspx" : this.Attributes["Url"].ToString();
                return CustomerPageAction.customPath2 + _url;
            }
        }
        string path = CustomerPageAction.customPath2 + "Exam/";
        protected void Page_Load(object sender, EventArgs e)
        {
            //根据不同的类型,传入不同的参数
            string hasChild = "<a href='" + path + "Papers_System_Manage.aspx?NodeID={0}' target='main_right' id='a{0}' class='list1' >{2}<span class='list_span'>{1}</span></a>";
            string noChild = "<a href='" + path + "Papers_System_Manage.aspx?NodeID={0}' target='main_right'>{2}{1}</a>";
            DataTable nodeDT = NodeBll.Sel();
            nodeDT.Columns["TypeName"].ColumnName = "Name";
            nodeDT.Columns["Pid"].ColumnName = "ParentID";
            string head = "<ul class='tvNav'><li class='menu_tit'><span class='fa fa-chevron-down'></span>试卷管理</li>";
            head += "<li><a class='list1' id='a0' href='" + path + "Papers_System_Manage.aspx' target='main_right' ><span class='list_span'>全部试卷</span><span class='fa fa-list'></span></a>";
            nodeHtml.Text = head + B_Node.GetLI(nodeDT, hasChild, noChild) + "</li></ul>";
        }
    }
}