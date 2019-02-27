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
    B_Exam_Class classBll = new B_Exam_Class();
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
                    DataTable dt = knowBll.Select_All(NodeID, DataConverter.CLng(Request.Form["nid"]),1);
                    result = JsonConvert.SerializeObject(dt);
                    break;
                default:
                    break;
            }
            Response.Write(result); Response.Flush();Response.End(); return;
        }
        if (!IsPostBack)
        {
            MyBind();
        }
    }
    public void MyBind(string knowname="")
    {
        RPT.DataSource = knowBll.Select_All(NodeID,0,1,knowname);
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

    protected void Search_Btn_Click(object sender, EventArgs e)
    {
        MyBind(Search_T.Text);
    }
}