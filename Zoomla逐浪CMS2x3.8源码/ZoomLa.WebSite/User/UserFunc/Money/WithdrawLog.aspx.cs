using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.User;
using ZoomLa.Model;

public partial class User_UserFunc_WithdrawLog : System.Web.UI.Page
{
    B_User buser = new B_User();
    B_Cash cashBll = new B_Cash();
    B_Temp tempMod = new B_Temp();
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
        dt = cashBll.SelectUserAll(mu.UserID);
        //dt = tempMod.SelByUid(mu.UserID);
        if (!string.IsNullOrEmpty(STime_T.Text))
        {
            dt.DefaultView.RowFilter = "sTime >#" + Convert.ToDateTime(STime_T.Text) + "#";
            dt = dt.DefaultView.ToTable();
        }
        if (!string.IsNullOrEmpty(ETime_T.Text))
        {
            dt.DefaultView.RowFilter = "sTime <=#" + Convert.ToDateTime(ETime_T.Text) + "#";
            dt = dt.DefaultView.ToTable();
        }
        EGV.DataSource = dt;
        EGV.DataBind();
    }
    protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        EGV.PageIndex = e.NewPageIndex;
        DataBind();
    }
    protected void Skey_Btn_Click(object sender, EventArgs e)
    {
        MyBind();
    }
    public string GetStatus() 
    {
        switch (Eval("yState").ToString())
        {
            case "-1":
                return "<span style='color:red;'>已拒绝</span>";
            case "99":
                return "<span style='color:blue;'>提现成功</span>";
            default:
                return "<span>处理中</span>";
        }
    }
    public string GetCost()
    {
        return (Convert.ToDouble(Eval("money")) * 0.1).ToString("f2");
    }
}