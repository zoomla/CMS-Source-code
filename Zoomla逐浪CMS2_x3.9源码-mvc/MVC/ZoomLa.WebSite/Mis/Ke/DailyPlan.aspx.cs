using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Plat;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Model.Plat;
using ZoomLa.SQLDAL;

namespace ZoomLaCMS.MIS.Ke
{
    public partial class DailyPlan : System.Web.UI.Page
    {
        B_Blog_Sdl sdlBll = new B_Blog_Sdl();
        M_Blog_Sdl sdlMod = new M_Blog_Sdl();
        B_MisInfo typebll = new B_MisInfo();
        B_User buser = new B_User();
        public int UserID
        {
            get { return ViewState["userid"] == null ? DataConverter.CLng(Request.QueryString["userid"]) : DataConverter.CLng(ViewState["userid"]); }
            set { ViewState["userid"] = value; }
        }
        public int TypeID
        {
            get
            {
                if (ViewState["type"] == null)
                {
                    ViewState["type"] = DataConvert.CLng(Request.QueryString["type"]);
                }
                return DataConvert.CLng(ViewState["type"]);
            }
            set { ViewState["type"] = value; }
        }
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
                if (UserID <= 0) { UserID = buser.GetLogin().UserID; }
                M_UserInfo mu = buser.GetSelect(UserID);
                if (mu.IsNull || mu.UserID < 1) { function.WriteErrMsg("选定的用户不存在"); }
                title_str.InnerText = mu.UserName + "的日程";
                UserName_Li.Text = mu.UserName;
                MyBind();
            }
        }
        public void MyBind()
        {
            //日程列表
            DataTable dt = typebll.SelByUid(UserID);
            M_MisInfo typeMod = null;
            if (dt.Rows.Count < 1)
            {
                typeMod = new M_MisInfo() { Title = "默认日程", MID = UserID, };
                typeMod.ID = typebll.insert(typeMod);
                dt = typebll.SelByUid(UserID);
                TypeID = typeMod.ID;
            }
            if (TypeID == 0)
            {
                TypeID = typebll.SelFirstModel(UserID).ID;
            }
            if (TypeID > 0)
            {
                typeMod = typebll.SelReturnModel(TypeID);
                if (typeMod == null) { function.WriteErrMsg("该类别日程不存在!"); }
                TopSubName_Li.Text = "<span style='color:#999'>" + typeMod.Title + "</span>的";
            }
            else
            { TopSubName_Li.Text = "所有"; }
            SubList_RPT.DataSource = dt;
            SubList_RPT.DataBind();
            GetTable(CurDate.Year, CurDate.Month);
            if (dt.Rows.Count == 0)
                EmptySub_Div.Visible = true;
            else
                EmptySub_Div.Visible = false;
            //抽出最近日程
            dt = sdlBll.SelTopSubject(UserID, TypeID);
            MyTop_RPT.DataSource = dt;
            MyTop_RPT.DataBind();
            if (dt.Rows.Count == 0) { listempty_div.Visible = true; }
            else { listempty_div.Visible = false; }
        }
        public void GetTable(int year, int month)
        {

            int days = DateTime.DaysInMonth(year, month);//这个月有多少天
            DateTime st = Convert.ToDateTime(year + "-" + month + "-01");
            DateTime myst = new DateTime();
            DataTable dt = sdlBll.SelByMonth(st, UserID, TypeID);
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
        // <param name="dt">当日的数据</param>
        // <param name="hc">对应td或以后换成其他</param>
        public string GetHtml(DataTable dt, DateTime st)
        {
            string html = "<td class='datas' data-date='" + st.ToString("yyyy-MM-dd") + "'><div class='tdTitle'>" + st.Day + "</div>";
            html += "<div>";
            string div = "<div class='td_content_div' data-id='{0}'><span><a href='javascript:;' onclick='ViewDetail({0});'>{1}</a></span></div>";
            foreach (DataRow dr in dt.Rows)
            {
                //UserID,ID,Name
                html += string.Format(div, dr["ID"], dr["Name"]);
            }
            html += "</div></td>";
            return html;
        }
        public string GetEmptyHtml(DateTime st)
        {
            string strtag = "";
            if (st.Month > CurDate.Month)//判断是否是下一个月的日期
                strtag = "nextmonth";
            else
                strtag = "premonth";
            string html = "<td class='" + strtag + "' data-date='" + st.ToString("yyyy-MM-dd") + "'><div style='text-align:right;font-size:15px;color:#ddd;'>" + st.Day + "</div>";
            html += "<div>";
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
            dt.DefaultView.RowFilter = "StartDate >=" + st + " And StartDate < " + et;
            return dt.DefaultView.ToTable();
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
        //获取课程名
        public string GetSubName()
        {
            return Eval("Title").ToString();
        }
        protected void SubList_RPT_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "DelType":
                    typebll.Del(DataConvert.CLng(e.CommandArgument));
                    sdlBll.DelByType(UserID, TypeID);
                    Response.Redirect("DailyPlan.aspx");
                    break;
                default:
                    break;
            }
        }
    }
}