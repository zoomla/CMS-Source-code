using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;

public partial class Manage_I_ASCX_QuestGuide : System.Web.UI.UserControl
{
    B_Exam_Class examBll = new B_Exam_Class();
    //----节点
    string hasChild, noChild;
    //点击需要打开的目标页面,默认为内容管理页
    public string Url
    {
        get
        {
            string _url = this.Attributes["Url"] == null ? "Exam/QuestionManage.aspx" : this.Attributes["Url"].ToString();
            return CustomerPageAction.customPath2 + _url;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        hasChild = "<a href='" + Url + "?NodeID={0}' target='main_right' id='a{0}' class='list1' style='padding-left:0.5em;'><span class='list_span'>{1}</span><i class='fa fa-chevron-down' title='浏览父节点'></i></a>";
        noChild = "<a href='" + Url + "?NodeID={0}' target='main_right' onclick='NodeTree.activeSelf(this);' style='padding-left:0.5em;'>{1}</a>";
        BindNode();
    }
    public string GetLI(DataTable dt, int pid = 0)
    {
        string result = "";
        DataRow[] dr = dt.Select("C_Classid='" + pid + "'");
        for (int i = 0; i < dr.Length; i++)
        {
            result += "<li>";
            if (dt.Select("C_Classid='" + Convert.ToInt32(dr[i]["C_id"]) + "'").Length > 0)
            {
                result += string.Format(hasChild, dr[i]["C_id"], dr[i]["C_ClassName"]);
                result += "<ul class='tvNav tvNav_ul' style='display:none;'>" + GetLI(dt, Convert.ToInt32(dr[i]["C_id"])) + "</ul>";
            }
            else
            {
                result += string.Format(noChild, dr[i]["C_id"], dr[i]["C_ClassName"]);
            }
            result += "</li>";
        }
        return result;
    }
    protected void BindNode()
    {
        DataTable dt = examBll.Select_All();
        foreach (DataRow dr in dt.Rows)
        {
            if (dr["C_ClassName"].ToString().Length > 7)
            {
                dr["C_ClassName"] = dr["C_ClassName"].ToString().Substring(0, 7) + "..";
            }
        }

        nodeHtml.Text = "<ul class='tvNav'><li><a  class='list1' id='a0' href='" + Url + "' target='main_right' style='padding-left:0.5em;' ><span class='list_span'>全部"+(Url.Contains("QuestionManage")?"试题":"试卷") +"</span><span class='fa fa-list'></span></a>" + GetLI(dt) + "</li></ul>";
    }
}