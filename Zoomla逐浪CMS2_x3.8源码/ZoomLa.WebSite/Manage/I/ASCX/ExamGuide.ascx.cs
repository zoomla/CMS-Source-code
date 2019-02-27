using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
/*
 * 根据所传不同,载入不同的菜单,默认为年级
 */
public partial class Manage_I_ASCX_ExamGuide : System.Web.UI.UserControl
{

    string path = CustomerPageAction.customPath2 + "Exam/";
    protected void Page_Load(object sender, EventArgs e)
    {
        //根据不同的类型,传入不同的参数
        string hasChild = "<a href='" + path + "QuestList.aspx?grade={0}' target='main_right' id='a{0}' class='list1' >{2}<span class='list_span'>{1}</span></a>";
        string noChild = "<a href='" + path + "QuestList.aspx?grade={0}' target='main_right'>{2}{1}</a>";
        DataTable nodeDT = B_GradeOption.GetGradeList(6, 0);
        nodeDT.Columns["GradeID"].ColumnName = "ID";
        nodeDT.Columns["GradeName"].ColumnName = "Name";
        string head = "<ul class='tvNav'><li class='menu_tit'><span class='fa fa-chevron-down'></span>试题管理</li>";
        head += "<li><a class='list1' id='a0' href='" + path + "QuestList.aspx' target='main_right' ><span class='list_span'>全部试题</span><span class='fa fa-list'></span></a>";
        nodeHtml.Text = head + B_Node.GetLI(nodeDT, hasChild, noChild) + "</li></ul>";
    }
}