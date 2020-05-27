using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ZoomLa.BLL;
using ZoomLa.BLL.User;
using ZoomLa.Model.User;
using ZoomLa.Common;
namespace ZoomLaCMS.Manage.Counter
{
    public partial class Month : CustomerPageAction
    {
        public int SumCount;
        //public B_Counter counterabout = new B_Counter();
        public int pmonth;
        public DateTime CDate { get { return DataConverter.CDate(Server.UrlDecode(Request.QueryString["cdate"])); } }
        B_Com_VisitCount visitBll = new B_Com_VisitCount();

        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!IsPostBack)
            {

                MyBind();
            }
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li> <li><a href='Counter.aspx'>访问统计</a></li><li><a href='Month.aspx'>日统计报表</a></li>");
        }
        public void MyBind()
        {
            CDate_L.Text = CDate.Year + "年" + CDate.Month.ToString("00") + "月";
            SumCount_L.Text = visitBll.CountByMonth(CDate.Year, CDate.Month).Rows[0]["Num"].ToString();
            DataTable dt = visitBll.SelByTime(3, DateTime.Now);
            CountRPT.DataSource = dt;
            CountRPT.DataBind();
            dt = visitBll.SelectAll(CDate.Year, CDate.Month);
            EGV.DataSource = dt;
            EGV.DataBind();
        }
        public string GetDayOfWeek()
        {
            DateTime time = new DateTime(DataConverter.CLng(Eval("Year")), DataConverter.CLng(Eval("Month")), DataConverter.CLng(Eval("Day")));
            switch (time.DayOfWeek.ToString())
            {
                case "Monday":
                    return "周一";
                case "Tuesday":
                    return "周二";
                case "Wednesday":
                    return "周三";
                case "Thursday":
                    return "周四";
                case "Friday":
                    return "周五";
                case "Saturday":
                    return "周六";
                case "Sunday":
                    return "周日";
            }
            return "";
        }

        protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            EGV.PageIndex = e.NewPageIndex;
            MyBind();
        }
    }
}