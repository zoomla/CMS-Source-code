using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ZoomLa.BLL.Exam;

public partial class Manage_I_ASCX_PublishNodeTree : System.Web.UI.UserControl
{
    B_Publish_Node nodebll = new B_Publish_Node();
    string path = CustomerPageAction.customPath2 + "Exam/";
    string hasChild, noChild;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        hasChild = "<a href='javascript:;' id='a{0}' class='list1' onclick='ShowMain(this,{0});'><span class='list_span'>{1}</span><span class='fa fa-chevron-down' style='font-size:1em' title='浏览父节点'></span></a>";
        noChild = "<a href='javascript:;' onclick='ShowInfo({0});'>{1}</a>";
        BindNode();
    }
    public string GetLI(DataTable dt, int pid = 0)
    {
        string result = "";
        DataRow[] dr = dt.Select("Pid='" + pid + "'");
        for (int i = 0; i < dr.Length; i++)
        {
            result += "<li>";
            if (dt.Select("Pid='" + Convert.ToInt32(dr[i]["ID"]) + "'").Length > 0)
            {
                result += string.Format(hasChild, dr[i]["ID"], dr[i]["NodeName"]);
                result += "<ul class='tvNav tvNav_ul' style='display:none;'>" + GetLI(dt, Convert.ToInt32(dr[i]["ID"])) + "</ul>";
            }
            else
            {
                result += string.Format(noChild, dr[i]["ID"], dr[i]["NodeName"]);
            }
            result += "</li>";
        }
        return result;
    }
    protected void BindNode()
    {
        DataTable dt = nodebll.Sel();
        nodeHtml.Text = "<ul class='tvNav'><li><a  class='list1' id='a0' href='javascript:;' onclick='ShowInfo(0);' ><span class='list_span'>全部报纸</span><span class='fa fa-list'></span></a>" + GetLI(dt) + "</li></ul>";
    }
}