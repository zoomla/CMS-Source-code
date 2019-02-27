using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.User;
using ZoomLa.Model.User;
using ZoomLa.SQLDAL;

public partial class Manage_User_UnitDetail : System.Web.UI.Page
{
    B_User_Consume conBll = new B_User_Consume();
    B_User_UnitWeek unitWeekBll = new B_User_UnitWeek();
    public int Pid { get { return Convert.ToInt32(Request.QueryString["Pid"]); } }
    public string Flow { get { return Request.QueryString["flow"] ?? ""; } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MyBind();
        }
    }
    public void MyBind() 
    {
        M_User_UnitWeek conMod = unitWeekBll.SelReturnModel(Pid);

        DataTable dt = SelByPid(Flow, string.IsNullOrEmpty(conMod.ChildIDS) ? "-1" : conMod.ChildIDS);
        EGV.DataSource = dt;
        EGV.DataBind();
        Call.SetBreadCrumb(Master, "<li><a href='UserManage.aspx'>会员管理</a></li><li><a href='UnitMain.aspx?stime=" + conMod.CDate + "'>会员提成_汇总</a></li><li><a href='" + Request.RawUrl + "'>会员提成_流水</a></li>");
        if (dt!=null&&dt.Rows.Count > 0)
        {
            TotalAmount_sp.InnerText = Convert.ToDouble(dt.Compute("sum(AMount)", "true")).ToString("0.00");
            TotalUnit_sp.InnerText = Convert.ToDouble(dt.Compute("sum(RealUnit)", "true")).ToString("0.00");
        }
    }
    protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        EGV.PageIndex = e.NewPageIndex;
        MyBind();
    }
    public DataTable SelByPid(string flow, string ids)
    {
        SafeSC.CheckIDSEx(ids);
        ids = ids.TrimEnd(',');
        SqlParameter[] sp = new SqlParameter[]{
            new SqlParameter("flow",flow)
            
            };
        string sql = "Select *,(Select UserName From ZL_User Where UserID=A.UserID) UserName,(Select UserName From ZL_User Where UserID=A.PUserID) PUserName From ZL_User_UnitWeek AS A Where Flow=@flow AND UserID IN(" + ids + ")";
        //if (string.IsNullOrEmpty(date))
        //{
        //    string stime, etime; B_User_UnitWeek.GetWeekSE(DataConvert.CDate(date), out stime, out etime);
        //    sp = new SqlParameter[] { new SqlParameter("stime", stime), new SqlParameter("etime", etime) };
        //    sql += "  AND CDate BetWeen @stime AND @etime";
        //}
        return SqlHelper.ExecuteTable(CommandType.Text, sql, sp);
    }
}