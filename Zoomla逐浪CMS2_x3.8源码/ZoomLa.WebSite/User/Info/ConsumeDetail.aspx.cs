using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.SQLDAL;

public partial class User_Info_ConsumeDetail : System.Web.UI.Page
{
    B_History hisBll = new B_History();
    B_User buser = new B_User();
    //默认展示余额操作记录
    public int SType { get { int _type = DataConvert.CLng(Request.QueryString["SType"]); return _type == 0 ? 1 : _type; } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MyBind();
        }
    }
    private void MyBind()
    {
        M_UserInfo mu = buser.GetLogin();
        DataTable dt = new DataTable();
        dt = hisBll.SelByType_U(SType, mu.UserID);
        if (!string.IsNullOrEmpty(STime_T.Text))
        {
            dt.DefaultView.RowFilter = "HisTime >#" + Convert.ToDateTime(STime_T.Text) + "#";
            dt = dt.DefaultView.ToTable();
        }
        if (!string.IsNullOrEmpty(ETime_T.Text))
        {
            dt.DefaultView.RowFilter = "HisTime <=#" + Convert.ToDateTime(ETime_T.Text) + "#";
            dt = dt.DefaultView.ToTable();
        }
        if (!string.IsNullOrEmpty(Skey_T.Text.Trim()))
        {
            dt.DefaultView.RowFilter = "Detail Like '%" + Skey_T.Text + "%'";
            dt = dt.DefaultView.ToTable();
        }
        EGV.DataSource = dt;
        EGV.DataBind();
    }
    protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        EGV.PageIndex = e.NewPageIndex;
        MyBind();
    }
    protected void Skey_Btn_Click(object sender, EventArgs e)
    {
        MyBind();
    }
}