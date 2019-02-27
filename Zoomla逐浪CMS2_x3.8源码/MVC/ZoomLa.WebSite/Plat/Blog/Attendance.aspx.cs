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
using ZoomLa.SQLDAL;

namespace ZoomLaCMS.Plat.Blog
{
    public partial class Attendance : System.Web.UI.Page
    {
        B_User buser = new B_User();
        B_User_Plat userpBll = new B_User_Plat();
        B_Plat_Sign signBll = new B_Plat_Sign();
        protected int AllCount = 0; //当月班次
        protected int AttendCount = 0;//实际出勤次数
        protected int Late = 0;//迟到
        protected int LeaveEarly = 0;//早退
        protected int Lack = 0;//未签到/签退
                               //当前时间
        public DateTime CurDate
        {
            get { if (ViewState["CurDate"] == null) { ViewState["CurDate"] = DateTime.Now; } return Convert.ToDateTime(ViewState["CurDate"]); }
            set { ViewState["CurDate"] = value; }
        }
        public int UserID { get { return Request.QueryString["uid"] == null ? 0 : DataConvert.CLng(Request.QueryString["uid"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (B_User_Plat.GetLogin().CompID < 1) { function.WriteErrMsg("您还不是企业会员，请加入企业再使用考勤功能!"); }
                if (B_User_Plat.GetLogin().CompID <= 0) { stats_a.Visible = false; }
                if (UserID > 0)
                {
                    if (userpBll.SelReturnModel(UserID).CompID != B_User_Plat.GetLogin().CompID) { function.WriteErrMsg("只可查看本公司员工的考勤信息!"); }
                    Tit_L.Text = userpBll.SelReturnModel(UserID).TrueName + "-考勤信息";
                }
                MyBind();
            }
        }
        public void MyBind()
        {
            AllCount = 0;
            AttendCount = 0;
            Late = 0;
            LeaveEarly = 0;
            Lack = 0;

            GetTable(CurDate.Year, CurDate.Month);

            AllCount_L.Text = AllCount.ToString();
            Attendance_L.Text = AttendCount.ToString();
            Late_L.Text = Late.ToString();
            LeaveEarly_L.Text = LeaveEarly.ToString();
            Lack_L.Text = Lack.ToString();
        }
        public void GetTable(int year, int month)
        {

            int days = DateTime.DaysInMonth(year, month);//这个月有多少天
            DateTime st = Convert.ToDateTime(year + "-" + month + "-01");
            DateTime myst = new DateTime();
            DataTable dt = null;
            if (UserID > 0) { dt = signBll.SelUserByMonth(st, UserID); }
            else { dt = signBll.SelUserByMonth(st, B_User_Plat.GetLogin().UserID); }
            int first = (int)st.DayOfWeek, index = 0;//日期标识,当前进行到了多少天
            for (int i = 1; i <= 7; i++)//首周需要特殊处理
            {
                Literal lit = DateBody.FindControl("Rep_W1_D" + i) as Literal;
                if (i < first)
                {
                    lit.Text = GetEmptyHtml(st.AddDays(i - first));
                }
                else
                {
                    myst = st.AddDays(index);
                    lit.Text = GetHtml(GetOneDay(dt, myst), myst);
                    index++;
                }
            }
            for (int w = 2; w <= 5; w++)//第二周开始循环处理
            {
                for (int i = 1; i <= 7; i++)//周内日循环
                {
                    Literal lit = DateBody.FindControl("Rep_W" + w + "_D" + i) as Literal;
                    myst = st.AddDays(index);
                    if (myst.Month > month)
                        lit.Text = GetEmptyHtml(myst);
                    else
                    {
                        lit.Text = GetHtml(GetOneDay(dt, myst), myst);
                    }
                    index++;
                }
            }
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
        public string GetEmptyHtml(DateTime st)
        {
            string strtag = "";
            if (st.Month > CurDate.Month)//判断是否是下一个月的日期
                strtag = "nextmonth";
            else
                strtag = "premonth";
            string html = "<td class='" + strtag + "' data-date='" + st.ToString("yyyy-MM-dd") + "'><div style='font-size:15px;color:#ddd;'>" + st.Month + "月" + st.Day + "日" + "</div>";
            html += "<div style='height:60px;'>";
            html += "</div></td>";
            return html;
        }
        public string GetHtml(DataTable dt, DateTime st)
        {
            DateTime it = DateTime.Now;
            DateTime ot = DateTime.Now;
            string html = "<td class='datas'><div class='tdTitle' style='width:100px;'>" + st.Day + "</div>";
            html += "<div style='height:80px;'>";
            if (st <= DateTime.Now)
            {
                AllCount++;//班次+1
                string div = "<div >@signin - @signout</br>@state</div>";
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["ZType"].ToString().Equals("0"))
                    {
                        AttendCount++;//签到
                        it = DataConvert.CDate(dr["CDate"]);
                        if (dr["State"].ToString().Equals("1"))
                        {//迟到
                            Late++;
                            div = div.Replace("@signin", "<span style='color:red;'>" + it.ToShortTimeString() + "</span> 签到");
                        }
                        else
                        {
                            div = div.Replace("@signin", it.ToShortTimeString() + " 签到");
                        }

                    }
                    if (dr["ZType"].ToString().Equals("1"))
                    {
                        ot = DataConvert.CDate(dr["CDate"]);
                        if (dr["State"].ToString().Equals("2"))
                        {
                            LeaveEarly++;
                            div = div.Replace("@signout", "<span style='color:red;'>" + ot.ToShortTimeString() + "</span> 签退");
                        }
                        else
                        {
                            div = div.Replace("@signout", ot.ToShortTimeString() + " 签退");
                        }

                    }
                }
                if (st.Date == DateTime.Now.Date && div.Contains("@signout")) { div = div.Replace("@state", ""); }
                if (div.Contains("@signin"))//未签到
                {
                    Lack++;
                    div = "<span style='color:red;'>(无签到信息)</span>";
                }
                if (div.Contains("@signout"))
                {//未签退
                    Lack++;
                    div = div.Replace("@signout", "<span style='color:red;'>未签退</span>")
                                .Replace("@state", "<span style='color:red;'>(无效考勤)</span>");
                }
                double time = (ot - it).TotalHours;
                div = div.Replace("@state", "(共" + time.ToString("0.0") + "个小时)");
                html += div;
            }
            html += "</div></td>";
            return html;
        }
        //获取当天是否有数据
        public DataTable GetOneDay(DataTable dt, DateTime stime)
        {
            return FilterByDate(dt, stime, stime.AddDays(1));
        }
        //内存表中指定时间的数据
        public DataTable FilterByDate(DataTable dt, DateTime stime, DateTime etime)
        {
            string st = "#" + stime.ToString("yyyy-MM-dd") + "#";
            string et = "#" + etime.ToString("yyyy-MM-dd") + "#";
            dt.DefaultView.RowFilter = "CDate >=" + st + " And CDate < " + et;
            return dt.DefaultView.ToTable();
        }
    }
}