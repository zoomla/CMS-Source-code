using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using ZoomLa.BLL;
using ZoomLa.Model;

public partial class MIS_MisAttendanceInfo : System.Web.UI.Page
{
    B_User buser = new B_User();
    DataTable dt = new DataTable();
    B_MisAttendance Battendance = new B_MisAttendance();
    B_MisSign Bsign = new B_MisSign();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindPeson();
        }
    }
    protected void BindPeson()
    {
        string getYear = this.Year.SelectedValue;
        string getMonth = this.month.SelectedValue;
        string Dates = getYear + "-" + getMonth;
        dt = buser.Sel();
        this.repShow.DataSource = dt;
        this.repShow.DataBind();

        string Months = DateTime.Now.ToString();
        string mon = Months.Split(new char[] { '/' })[1].ToString();
        string year = Months.Split(new char[] { '/' })[0].ToString();
        this.month.SelectedIndex = Convert.ToInt32(mon)-1;
        this.Year.SelectedItem.Text = year + "年";
    }

    //出勤
    protected int CheckDays(string Name)
    {
        return 0;
    }
    //旷工
    protected int CheckNoWork(string Name)
    {
        return 0;
    }
    //迟到
    protected int CheckLate(string Name)
    {
        return 0;
    }
    //早退
    protected int CheckLeave(string Name)
    {
        return 0;
    }
    //缺卡
    protected int CheckLost(string Name)
    {
        return 0;
    }
    //晚卡
    protected int CheckLates(string Name)
    {
        return 0;
    }
}