using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;
using System.Data;
using ZoomLa.BLL.Exam;

public partial class User_Questions_MyExamResult : System.Web.UI.Page
{
    B_User buser = new B_User();
    B_Exam_Sys_Papers bps = new B_Exam_Sys_Papers();
    B_Exam_Answer answerBll = new B_Exam_Answer();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MyBind();
        }
    }
    public void MyBind()
    {
        M_UserInfo mu = buser.GetLogin();
        DataTable dt = answerBll.SelAllMyAnswer(mu.UserID);
        EGV.DataSource = dt;
        EGV.DataBind();
    }
    protected void Egv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        EGV.PageIndex = e.NewPageIndex;
        MyBind();
    }
    protected void Egv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "del2":
                break;
        }
        MyBind();
    }
}