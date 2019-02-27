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

public partial class manage_Question_ExamPointManage : CustomerPageAction
{

    private B_Course bcourse = new B_Course();
    private B_Admin badmin = new B_Admin(); 
    private B_ExamPoint bep = new B_ExamPoint();
    public int AdminID = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        B_ARoleAuth.CheckEx(ZLEnum.Auth.content, "ContentMange");
        AdminID = badmin.GetAdminLogin().AdminId;
        int AdminId =DataConverter.CLng(Request.QueryString["AddUser"]);
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["menu"]) && Request.QueryString["menu"] == "delete")
            {
                int id = DataConverter.CLng(Request.QueryString["id"]);
                bep.DeleteByGroupID(id);
            }
            MyBind();
        }

        Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li> <li><a href='Papers_System_Manage.aspx'>教育模块</a></li> <li><a href='QuestionManage.aspx'>考试管理</a></li><li>考点管理<a href='Add_ExamPoint.aspx?AdminId=" + AdminID + "'>[添加考点]</a></li>" + Call.GetHelp(77));
    }
    private void MyBind()
    {
        DataTable dt = bep.Select_All();
        EGV.DataSource = dt;
        EGV.DataBind();
    }
    // 批量删除
    protected void Button3_Click(object sender, EventArgs e)
    {
        string item = Request.Form["item"];
        if (item != null && item != "")
        {
            if (item.IndexOf(',') > -1)
            {
                string[] itemarr = item.Split(',');
                for (int i = 0; i < itemarr.Length; i++)
                {
                    bcourse.DeleteByGroupID(DataConverter.CLng(itemarr[i]));
                }
            }
            else
            {
                bcourse.DeleteByGroupID(DataConverter.CLng(item));
            }
        }
        function.WriteSuccessMsg("操作成功!", "ExamPointManage.aspx");
    }
    // 读取用户名
    public string GetAdminName(string name)
    {
        int id = DataConverter.CLng(name);
        M_AdminInfo madmininfo = B_Admin.GetAdminByAdminId(id);       
        if (madmininfo != null && madmininfo.AdminId > 0)
        {
            return madmininfo.UserName;
           
        }
        else
        {
            return "";
        }        
    }
    protected void EGV_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row != null && e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("ondblclick", "window.location.href = 'UpdateExamPoint.aspx?id=" + (e.Row.DataItem as DataRowView)["ID"] + "&&AdminId="+ (e.Row.DataItem as DataRowView)["AddUser"] + "';");
        }
    }
    protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        EGV.PageIndex = e.NewPageIndex;
        MyBind();
    }
}