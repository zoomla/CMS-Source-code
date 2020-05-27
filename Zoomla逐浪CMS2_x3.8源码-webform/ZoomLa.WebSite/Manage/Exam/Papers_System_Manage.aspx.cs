using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.BLL;
using System.Data;

public partial class manage_Question_Papers_System_Manage : CustomerPageAction
{
    B_Exam_Class examBll=new B_Exam_Class();
    B_Exam_Sys_Papers paperBll=new B_Exam_Sys_Papers();
    //试卷类型
    public int PaperClass { get { return DataConverter.CLng(Request.QueryString["type"]); } }
    private int NodeID { get { return DataConverter.CLng(Request.QueryString["NodeID"]); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        B_ARoleAuth.CheckEx(ZLEnum.Auth.content, "ContentMange");
        if (!IsPostBack)
        {
            MyBind();
            Call.SetBreadCrumb(Master, "<li><a href='Papers_System_Manage.aspx'>教育模块</a></li><li><a href='Papers_System_Manage.aspx'>考试管理</a></li><li>试卷管理[<a href='Add_Papers_System.aspx?menu=Add&NodeID=" + NodeID + "'>添加试卷</a>]</li>" + Call.GetHelp(74));
        }
    }
    private void MyBind()
    {
        DataTable dt = paperBll.SelAll(NodeID);
        if (!string.IsNullOrWhiteSpace(Skey_T.Text)) 
        {
            dt.DefaultView.RowFilter = "p_name LIKE '%" + Skey_T.Text + "%'";
            dt = dt.DefaultView.ToTable();
        }
        EGV.DataSource = dt;
        EGV.DataBind();
    }

    public string GetRType(string type)
    {
        if (type == "1")
        {
            return "自动阅卷";
        }
        else
        {
            return "手动阅卷";
        }
    }

    public string GetModus(string type)
    {
        if (type == "1")
        {
            return "固定试卷（手工）";
        }
        if (type == "2")
        {
            return "固定试卷（随机）";
        }
        if (type == "3")
        {
            return "随机试卷";
        }
        else
        {
            return "";
        }
    }


    //删除
    protected void BtnDelete_Click(object sender, EventArgs e)
    {
        string ids = Request.Form["idchk"];
        if (!string.IsNullOrEmpty(ids))
        {
            paperBll.DelByIDS(ids);
            MyBind();
        }
    }

    //分页
    protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        EGV.PageIndex = e.NewPageIndex;
        MyBind();
    }
    protected void EGV_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView gr = e.Row.DataItem as DataRowView;
            e.Row.Attributes["ondblclick"] = "location.href='Add_Papers_System.aspx?menu=Edit&id=" +gr["id"]+ "'";
            //e.Row.Attributes["style"] = "cursor:hander;";
            e.Row.Attributes["title"] = "双击修改试卷";
        }

    }
    protected void Skey_Btn_Click(object sender, EventArgs e)
    {
        MyBind();
    }
}
