using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.User;
using ZoomLa.Model;
using ZoomLa.Model.User;
using System.Data;
using ZoomLa.Common;


namespace ZoomLaCMS.Manage.Counter
{
    public partial class Year : CustomerPageAction
    {
        //public B_Counter counterabout = new B_Counter();
        B_Com_VisitCount visitBll = new B_Com_VisitCount();
        public DateTime CDate { get { return DataConverter.CDate(Server.UrlDecode(Request.QueryString["cdate"])); } }
        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!IsPostBack)
            {
                MyBind();
            }
            //pmonth = (int)counterabout.SelectCountYearA(Cyear);
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li> <li><a href='Counter.aspx'>访问统计</a></li><li><a href='Year.aspx'>月统计报表</a></li>");
        }
        public string GetMonthDate()
        {
            return Server.UrlEncode(new DateTime(CDate.Year, DataConverter.CLng(Eval("Month")), 1).ToString());
        }
        public void MyBind()
        {
            SumCount_L.Text = visitBll.CountByYear(CDate.Year).Rows[0]["Num"].ToString();
            DataTable dt = visitBll.SelByTime(2, CDate);
            CountRPT.DataSource = dt;
            CountRPT.DataBind();
            dt = visitBll.SelectAll(CDate.Year);
            EGV.DataSource = dt;
            EGV.DataBind();
        }

        protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            EGV.PageIndex = e.NewPageIndex;
            MyBind();
        }
    }
}