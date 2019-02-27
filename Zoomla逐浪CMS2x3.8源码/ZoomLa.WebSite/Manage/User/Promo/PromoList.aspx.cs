using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using ZoomLa.SQLDAL;
using ZoomLa.BLL.User;

public partial class Manage_User_Promo_PromoList : System.Web.UI.Page
{
    private int Year { get { return DataConvert.CLng(Request.QueryString["Year"]); } }
    private int Month { get { return DataConvert.CLng(Request.QueryString["Month"]); } }
    B_User_Promo promoBll = new B_User_Promo();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Init();
            MyBind();
        }
    }
    private new void Init()
    {
        string yearTlp = "<a href='PromoList.aspx?Year={1}&Month=" + Month + "' class='btn btn-default {0}'>{2}</a>";
        string monthTlp = "<a href='PromoList.aspx?Year=" + Year + "&Month={1}' class='btn btn-default {0}'>{2}</a>";
        Years_Li.Text = string.Format(yearTlp, Year == 0 ? "active" : "", "0","全部");
        for (int i = 0; i < 10; i++)
        {
            bool ischk = false;
            int val = (DateTime.Now.Year - i);
            if (Year == val) { ischk = true; }
            Years_Li.Text += string.Format(yearTlp, ischk ? "active" : "", val, val + "年");
        }
        Months_Li.Text = string.Format(monthTlp, Month == 0 ? "active" : "", "0", "全部");
        for (int i = 1; i < 12; i++)
        {
            bool ischk = false;
            if (Month == i) { ischk = true; }
            Months_Li.Text += string.Format(monthTlp, ischk ? "active" : "", i, i + "月");
        }
    }
    private void MyBind(string username="")
    {
        //生成饼状图统计
        //if (years.Count < 2) //如果年份只有一年,则直接生成月状图
        //{

        //}
        //生成天饼状图

        //按用户统计
        EGV.DataSource = promoBll.SelByFilter(Year,Month, username);
        EGV.DataBind();
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
    }
    protected void Search_Btn_Click(object sender, EventArgs e)
    {
        MyBind(Search_T.Text);
    }
}