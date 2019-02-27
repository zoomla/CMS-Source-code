using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.BLL;
using ZoomLa.Model;
using System.Data;

/*
 * 不分班级等,只按类别筛选试卷
 */ 
public partial class User_Questions_MyMarks : System.Web.UI.Page
{
    protected B_User buser = new B_User();
    protected B_Exam_Sys_Questions bq = new B_Exam_Sys_Questions();
    protected B_Exam_Class bqc = new B_Exam_Class();
    protected B_Exam_Sys_Papers besp = new B_Exam_Sys_Papers();
    //ClassID
    private int Cid { get { return DataConverter.CLng(Request.QueryString["c_id"]); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MyBind();
            TreeNode();
        }
    }
    private void TreeNode()
    {
        MyTree.SelectedNode = Cid;
        MyTree.liAllTlp = "<a class='filter_class' data-val='0' href='MyMarks.aspx'>全部</a>";
        MyTree.LiContentTlp = "<a class='filter_class' data-val='@ID' href='MyMarks.aspx?c_id=@NodeID'>@NodeName</a>";
        MyTree.DataSource = bqc.Select_All();
        MyTree.DataBind();
    }
    private void MyBind()
    {
        DataTable dt = besp.Selelct_Classid(Cid);
        EGV.DataSource = dt;
        EGV.DataBind();
    }
    protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        EGV.PageIndex = e.NewPageIndex;
        MyBind();
    }
}
