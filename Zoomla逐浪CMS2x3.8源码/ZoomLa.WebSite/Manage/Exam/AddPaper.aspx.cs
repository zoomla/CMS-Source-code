using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.BLL;


public partial class manage_Question_AddPaper : CustomerPageAction
{
    protected B_Exam_Sys_Questions bq;
    protected B_Exam_Class bqc;
    protected B_Exam_Sys_Papers bps;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MyBind();
            Call.SetBreadCrumb(Master, "<li>教育模块</li><li><a href='QuestionManage.aspx'>在线考试系统</a></li><li>系统试卷管理</li>");
        }
    }

    // 数据邦定

    private void MyBind()
    {
        bps = new B_Exam_Sys_Papers ();
        List<M_Exam_Sys_Papers> qus = new List<M_Exam_Sys_Papers>();
        qus = bps.GetSelect_All();
        EGV.DataSource = qus;
        this.EGV.DataBind();
    
    }

    //行绑定
    protected void gvPapers_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HyperLink hlName = e.Row.FindControl("hlName") as HyperLink; //标题
            HyperLink hlQuestions = e.Row.FindControl("hlQuestions") as HyperLink;  //浏览试题
            HyperLink hlUpdate = e.Row.FindControl("hlUpdate") as HyperLink; //修改试卷
            Label lblClassId = e.Row.FindControl("lblClassId") as Label;  //分类
            HiddenField hfClassId = e.Row.FindControl("hfClassId") as HiddenField;  //分类ID

            int Classid = DataConverter.CLng(hfClassId.Value);
            bqc = new B_Exam_Class ();
            M_Exam_Class mqc = bqc.GetSelect(Classid);
            if (mqc != null && mqc.C_id > 0)
            {
                lblClassId.Text = mqc.C_ClassName;
            }
        }
    }

    //删除
    protected void BtnDelete_Click(object sender, EventArgs e)
    {
        bps = new B_Exam_Sys_Papers();
        int Ids = 0;
        for (int i = 0; i < EGV.Rows.Count; i++)
        {
            CheckBox cbox = (CheckBox)EGV.Rows[i].FindControl("chkSel");
            if (cbox.Checked)
            {
                Ids = DataConverter.CLng((EGV.Rows[i].FindControl("hfId") as HiddenField).Value);
                //bool res = bps.GetDelete(Ids);
              
            }
        }
        MyBind();
    }
    //分页
    protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)     
    {
        EGV.PageIndex = e.NewPageIndex;
        MyBind();
    }

}
