using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.User;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.SQLDAL;

public partial class Plat_Blog_AttendanceStats : System.Web.UI.Page
{
    B_Plat_Sign signBll = new B_Plat_Sign();
    protected int AllCount = 0; //当月班次
    protected int Late = 0;//迟到
    protected int LeaveEarly = 0;//早退
    protected int Lack = 0;//未签到/签退
    protected int Invalid = 0;//无效考勤
    protected int Tabs { get { return DataConvert.CLng(Request.QueryString["tabs"]) == 0 ? 0 : 1; } }

    //当前时间
    public DateTime CurDate
    {
        get { if (ViewState["CurDate"] == null) { ViewState["CurDate"] = DateTime.Now; } return Convert.ToDateTime(ViewState["CurDate"]); }
        set { ViewState["CurDate"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (B_User_Plat.GetLogin().CompID < 1) { function.WriteErrMsg("您还不是企业会员，请加入企业再使用考勤功能!"); }
            MyBind();
        }
    }
    public void MyBind()
    {
        M_User_Plat up = B_User_Plat.GetLogin();
        //---------------总览
        DateTime st = Convert.ToDateTime(CurDate.Year + "-" + CurDate.Month + "-01");
        DataTable dt = signBll.SelCompByMonth(st, up.CompID);
        DateTime dtNow = DateTime.Now;
        if (CurDate.Year == dtNow.Year && CurDate.Month == dtNow.Month) { AllCount = dtNow.Day; }
        else { AllCount = DateTime.DaysInMonth(st.Year, st.Month); }
        dt.Columns.Add("lack", typeof(int));
        dt.Columns.Add("invalid", typeof(int));
        foreach (DataRow dr in dt.Rows)
        {
            dr["lack"] = AllCount - DataConvert.CLng(dr["attendance"]);
            dr["invalid"] = DataConvert.CLng(dr["attendance"])*2 - DataConvert.CLng(dr["signcount"]);
        }
        AllCount_L.Text = AllCount.ToString();
        Late_L.Text = dt.Compute("SUM(late)", "true").ToString();
        LeaveEarly_L.Text = dt.Compute("SUM(leave)", "true").ToString();
        Lack_L.Text = dt.Compute("SUM(lack)", "true").ToString();
        Invalid_L.Text = dt.Compute("SUM(invalid)","true").ToString();

        //----------------考勤概略
        if (Tabs == 0)
        {
            RPT.DataSource = dt;
            RPT.DataBind();
        }
        //----------------出勤清单
        else
        {
            //获取当月员工考勤数据
            DataTable info_dt = signBll.SelInfoByMonth(st, up.CompID);
            //新建一列，用来标识签到日期
            info_dt.Columns.Add("Date", typeof(DateTime));
            foreach (DataRow row in info_dt.Rows)
            {
                row["Date"] = DataConvert.CDate(row["CDate"]).Date;
            }
            //新建清单表，为info_dt合并重复的Date、TrueName数据
            DataTable newDt = info_dt.AsDataView().ToTable(true, "Date", "TrueName","UserID");
            //增加一列，保存工时
            newDt.Columns.Add("TimeLen", typeof(string));
            newDt.Columns.Add("InTime", typeof(string));
            newDt.Columns.Add("OutTime", typeof(string));
            int count = 0;
            for (int i = 0; i < newDt.Rows.Count + count; i++)
            {
                DataRow row = newDt.Rows[i - count];
                //从info_dt获取指定日期的数据
                string where = "TrueName = '" + row["TrueName"] + "' AND Date ='" + row["Date"] + "'";
                DataRow[] r = info_dt.Select(where);
                if (r.Length == 2)//签到、签退记录都有才计有效考勤
                {
                    DateTime t1 = DataConvert.CDate(r[0]["CDate"]);
                    DateTime t2 = DataConvert.CDate(r[1]["CDate"]);
                    row["InTime"] = t1.ToShortTimeString();
                    row["OutTime"] = t2.ToShortTimeString();
                    row["TimeLen"] = (t2 - t1).TotalHours.ToString("0.0") + "小时";
                }
                else { newDt.Rows.Remove(row); count++; }
            }
            EGV.DataSource = newDt;
            EGV.DataBind();
        }
        function.Script(this, "CheckTabs(" + Tabs + ");");
    }
    protected void PreMonth_Btn_Click(object sender, EventArgs e)
    {
        CurDate = CurDate.AddMonths(-1);
        MyBind();
    }
    protected void NextMonth_Btn_Click(object sender, EventArgs e)
    {
        CurDate = CurDate.AddMonths(1);
        MyBind();
    }

    protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        EGV.PageIndex = e.NewPageIndex;
        MyBind();
    }
}