using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using ZoomLa.BLL;
using ZoomLa.Model;

public partial class MIS_MisPunch : System.Web.UI.Page
{
    B_MisAttendance Battendance = new B_MisAttendance();
    M_MisAttendance Mattendance = new M_MisAttendance();
    B_MisSign BMSign = new B_MisSign();
    M_MisSign MMSign = new M_MisSign();
    B_User buser = new B_User();
    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        buser.CheckIsLogin("/Mis/Target/Default.aspx");
        if (!IsPostBack)
        {
            string UName = buser.GetLogin().UserName;
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("UserName", UName) };
           
        }
    }
    protected void BtnBegin_Click(object sender, EventArgs e)
    {
        Mattendance.DepartMent = "";
        Mattendance.BeginTime = DateTime.Now.ToString().Replace("/","-");
        Mattendance.UserName = buser.GetLogin().UserName;
        Mattendance.Comment = "";
        Mattendance.BeginStatus = 1;
        if (Battendance.insert(Mattendance) > 0)
        {
            Response.Write("<script>alert('签到成功');location.href='MisAttendance.aspx';</script>");
        }
        else
        {
            Response.Write("<script>alert('签到失败');location.href='MisAttendance.aspx';</script>");
        }
    }
    protected void BtnEnd_Click(object sender, EventArgs e)
    {
        SqlParameter[] sp = new SqlParameter[] { new SqlParameter("UserName", buser.GetLogin().UserName) };
    }
    protected void BtnSub_Click(object sender, EventArgs e)
    {
    }
}