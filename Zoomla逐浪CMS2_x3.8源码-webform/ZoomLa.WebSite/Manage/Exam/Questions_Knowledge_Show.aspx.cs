using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Common;
using ZoomLa.BLL;
using ZoomLa.Model;
using System.Data;

public partial class manage_Question_Questions_Knowledge_Show : CustomerPageAction
{

      protected B_Exam_Sys_Questions bq = new  B_Exam_Sys_Questions ();
    B_ExamPoint bqk;

    B_Admin badmin = new B_Admin();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MyBind();
            Call.SetBreadCrumb(Master, "<li>教育模块</li><li><a href='QuestionManage.aspx'>在线考试系统</a></li><li>知识点管理<a href='" +  hlAdd.NavigateUrl+ "'>[添加知识点]</a></li>");
        }
    }

    private void MyBind()
    {
        DataTable dt = new DataTable();
        bqk = new B_ExamPoint();
        if (Request.QueryString["c_id"] != null && Request.QueryString["c_id"] != "")
        {
            int cid = DataConverter.CLng(Request.QueryString["C_id"]);
            dt = bqk.SelByCid(cid);
        }
        else 
        {
            dt = bqk.Select_All(); 
        }
        EGV.DataSource = dt;
        EGV.DataBind();
    }
    //分页
    protected void txtPage_TextChanged(object sender, EventArgs e)
    {
        ViewState["page"] = "1";
        MyBind();
    }

    //行命令
    protected void gvCard_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("Upd"))  //修改
        {
            Response.Redirect("AddQuestion_Knowledge.aspx?menu=Edit&k_id=" + e.CommandArgument);
        }
        if (e.CommandName.Equals("Del")) //删除
        {
            int kid = DataConverter.CLng(e.CommandArgument);
            bqk = new B_ExamPoint ();
            bool result = bqk.DeleteByGroupID(kid);
            if (result)
            {
                this.DataBind();
            }
            else
            {
                function.WriteErrMsg("删除失败!");
            }
        }
    }
    protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        EGV.PageIndex = e.NewPageIndex;
        MyBind();
    }

}
