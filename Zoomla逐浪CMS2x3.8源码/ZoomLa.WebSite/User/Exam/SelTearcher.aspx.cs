using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Common;
using ZoomLa.BLL;
using ZoomLa.Model;
using System.Data;

public partial class User_Exam_SelTearcher : System.Web.UI.Page
{
    B_User buser = new B_User();
    B_Admin badmin = new B_Admin();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!buser.IsTeach() && !badmin.CheckLogin()) { function.WriteErrMsg("只有教师才能访问!"); }
        if (!IsPostBack)
        {
            MyBind();
        }
    }
    public void MyBind(string search="")
    {
        DataTable dt = buser.SelByGroupType("isteach",search);
        Egv.DataSource = dt;
        Egv.DataBind();
    }

    protected void Egv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Egv.PageIndex = e.NewPageIndex;
        MyBind();
    }

    protected void Search_Btn_Click(object sender, EventArgs e)
    {
        MyBind(Search_T.Text.Trim());
    }
}