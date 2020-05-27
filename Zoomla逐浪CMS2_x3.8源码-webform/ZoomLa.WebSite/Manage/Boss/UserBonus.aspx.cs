using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.CreateJS;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.SQLDAL;

public partial class Manage_Boss_UserBonus : System.Web.UI.Page
{
    /*
     * 会员分红,单用于二元期权
     * 1,用户存储进入高频交易
     * 2,每周运行一次,计算分红,并将记录写入分红模型表中
     * 3,根据分红,为其手工帐户打入资金
     */
    B_User buser = new B_User();
    B_Deposit depBll = new B_Deposit();
    B_CodeModel modelBll = new B_CodeModel("ZL_C_fhmx");
    protected void Page_Load(object sender, EventArgs e)
    {
        StructDP.MyBind = MyBind;
        if (!IsPostBack)
        {
            if (ZoomLa.SQLDAL.DBHelper.Table_IsExist(modelBll.TbName)) { function.WriteErrMsg("分红模型,[" + modelBll.TbName + "]不存在"); }
            MyBind();
            Call.SetBreadCrumb(Master, "<li><a href='AdminManage.aspx'>用户管理</a></li><li><a href='UserManage.aspx'>会员管理</a></li><li><a href='"+Request.RawUrl+"' class='active'>会员分红</a></li>");
        }
    }
    public void MyBind()
    {
        DataTable dt = SelByWhere(UName_T.Text, STime_T.Text, ETime_T.Text);
        EGV.DataSource = dt;
        EGV.DataBind();
        if (dt.Rows.Count > 0)
        {
            bonus_query_sp.InnerHtml = "(" + Convert.ToDouble(dt.Compute("SUM(bonus_int)", "")).ToString("f2") + ")";
            double percent = DataConvert.CDouble(dt.Compute("AVG(phpercent_db)", ""));
            percent_sp.InnerHtml = "(" + GetPerStr(percent) + ")";
            bdmoney_sp.InnerHtml = "(" + Convert.ToDouble(dt.Compute("SUM(bdmoney_int)", "")).ToString("f2") + ")";
        }
        else { bonus_query_sp.InnerHtml = "0.00"; percent_sp.InnerHtml = "0.00"; bdmoney_sp.InnerHtml = "0.00"; }
    }
    protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        EGV.PageIndex = e.NewPageIndex;
        MyBind();
    }
    protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "del2":
                break;
        }
        MyBind();
    }
    //开始分红,每周执行一次
    protected void BeginBonus_Btn_Click(object sender, EventArgs e)
    {
        function.WriteErrMsg("已设为自动分红,取消申请");
    }
    private string GetPerStr(double num)
    {
        return (num * 100).ToString("f2") + "%";
    }
    private DataTable SelByWhere(string uname = "", string stime = "", string etime = "")
    {
        uname = uname.Trim(); stime = stime.Trim(); etime = etime.Trim();
        SqlParameter[] sp = new SqlParameter[] { 
            new SqlParameter("uname", "%"+uname+"%"), 
            new SqlParameter("stime",stime),
            new SqlParameter("etime",etime)
        };
        string fields = "A.*,CAST(unit as money) bonus_int,CAST(phpercent as decimal(9, 6)) phpercent_db,CAST(bdmoney as money)bdmoney_int,B.StructureID ";
        string where = " 1=1 ";
        if (!string.IsNullOrEmpty(uname))
        {
            where += " AND UserName LIKE @uname";
        }
        if (!string.IsNullOrEmpty(stime))
        {
            where += " AND CAST(cdate as datetime) > @stime";
        }
        if (!string.IsNullOrEmpty(etime))
        {
            where += " AND CAST(cdate as datetime) < @etime";
        }
        if (StructDP.StructID > 0)
        {
            where += " AND B.StructureID=" + StructDP.StructID;
        }
        return SqlHelper.JoinQuery(fields, modelBll.TbName, "ZL_User", "A.UserID=B.UserID", where, "ID DESC", sp);
    }
    protected void Skey_Btn_Click(object sender, EventArgs e)
    {
        MyBind();
    }
    //------------------
    public string GetStatus()
    {
        string tlp = "<a href='" + CustomerPageAction.customPath2 + "/User/Log4.aspx?ID=" + Eval("LogID") + "' title='查看处理日志' >{0}</a>";
        switch (Eval("MyStatus").ToString())
        {
            case "0":
                return "<span style='color:green;'>未处理</span>";
            case "1":
                return string.Format(tlp, "<span style='color:green;'>已入账</span>");
            case "-1":
                return string.Format(tlp, "<span style='color:red;'>入账失败(用户名或密码错误)</span>");
            default:
                return "<span>异常状态</span>";
        }
    }
}