using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.BLL;
using System.Data;
using Newtonsoft.Json;

public partial class Manage_Exam_KnowledgeManage : System.Web.UI.Page
{
    B_Questions_Knowledge knowBll = new B_Questions_Knowledge();
    B_Exam_Class nodeBll = new B_Exam_Class();
    B_Admin badmin = new B_Admin();
    B_User buser = new B_User();
    public int NodeID { get { return DataConverter.CLng(Request.QueryString["nid"]); } }
    public int IsRead { get { return DataConverter.CLng(Request.QueryString["isread"]); } }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (function.isAjax())
        {
            string action = Request.Form["action"];
            string result = "";
            switch (action)
            {
                case "getchild":
                    DataTable dt = knowBll.Select_All(NodeID, DataConverter.CLng(Request.Form["nid"]));
                    result = JsonConvert.SerializeObject(dt);
                    break;
                default:
                    break;
            }
            Response.Write(result); Response.Flush();Response.End(); return;
        }
        //if (!buser.IsTeach() && !badmin.CheckLogin()) { function.WriteErrMsg("只有教师才能访问!"); }
        if (!IsPostBack)
        {
            if (NodeID < 1) { function.WriteErrMsg("未指定科目"); }
            MyBind();
            M_Exam_Class nodeMod = nodeBll.GetSelect(NodeID);
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li> <li><a href='Papers_System_Manage.aspx'>教育模块</a></li><li><a href='Question_Class_Manage.aspx'>分类管理</a></li><li>知识点管理 [" + nodeMod .C_ClassName+ "] <a href='AddKnowledge.aspx?nid=" + NodeID + "'>[添加知识点]</a></li>");
        }
    }
    public void MyBind()
    {
        RPT.DataSource = knowBll.Select_All(NodeID,0);
        RPT.DataBind();
    }

    //输出知识点级别
    public string GetKnowType()
    {
        if (Eval("IsSys").Equals(1)) { return "系统"; }
        else { return "自定义"; }
    }
    //获取状态
    public string GetStatus()
    {
        if (Eval("Status").Equals(0))
        {
            return "<span style='color:red;'>禁用</span>";
        }
        else { return "<span style='color:green'>启用</span>"; }
    }
    protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("Del"))
        {
            knowBll.GetDelete(DataConverter.CLng(e.CommandArgument));
        }
        MyBind();
    }

    protected void Dels_Btn_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.Form["idchk"]))
        {
            knowBll.DelByIDS(Request.Form["idchk"]);
        }
        MyBind();
    }
    public string GetIcon()
    {
        return DataConverter.CLng(Eval("ChildCount")) > 0 ? "fa fa-folder" : "fa fa-file";
    }
    protected void RPT_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "del2":
                knowBll.DelByIDS(e.CommandArgument.ToString());
                break;
        }
        MyBind();
    }
}