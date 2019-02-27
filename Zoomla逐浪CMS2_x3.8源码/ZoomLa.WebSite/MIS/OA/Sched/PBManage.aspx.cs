using System;
using System.Web.UI;
using System.Data;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.SQLDAL;

/*
 * 排班管理界面,类Excel操作,可从Excel导出至本页面,也可从本页面导出至Excel
 * 班次用不同颜色显示(BCAdd.aspx中定义)
 * 
 * 月度表，只给看不给改，只能从周处排班
 */
public partial class MIS_OA_Sched_PBManage : System.Web.UI.Page
{
    protected B_User buser = new B_User();
    protected B_OA_BC bcBll = new B_OA_BC();//班次
    protected M_OA_BC bcMod = new M_OA_BC();
    //获取排班的起始时间
    public DateTime StartTime
    {
        get
        {
            if (ViewState["StartTime"] == null)
            {
                ViewState["StartTime"] = GetStartTime();//默认为本周
            }
            return DateTime.Parse(ViewState["StartTime"].ToString());
        }
        set
        {
            ViewState["StartTime"] = value;
        }
    }
    public DateTime StartMonth
    {
        get
        {
            if (ViewState["StartMonth"] == null)
            {
                ViewState["StartMonth"] = DateTime.Now.ToString("yyyy-MM-01");//默认为本周
            }
            return DateTime.Parse(ViewState["StartMonth"].ToString());
        }
        set
        {
            ViewState["StartMonth"] = value;
        }
    }
    //班次信息表
    public DataTable BCDataTable 
    {
        get
        {
            if (ViewState["BCDataTable"] == null)
                ViewState["BCDataTable"] = bcBll.Sel();
            return ViewState["BCDataTable"] as DataTable;
        }
        set 
        {
            ViewState["BCDataTable"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        B_User.CheckIsLogged();
        if (!IsPostBack)
        {
            dateHtml.Text = GetDateTr(StartTime);
            DataBind();
            BCDataBind();
        }
     
        //----------按月份显示
        DateTime st = DateTime.Parse("2014-04-01");
        monthHtml.Text = GetDateTr(st, "month");
        monthRepeater.DataSource = SelByMonth(st);
        monthRepeater.DataBind();
    }
    private void DataBind(string key = "")
    {
        DataTable dt = new DataTable();
        if (string.IsNullOrEmpty(Request.QueryString["gid"]))
        {
            dt = buser.SelAll();
        }
        else
        {
            dt = buser.GetUserByGroupI(Convert.ToInt32(Request.QueryString["gid"]));
        }
        if (!string.IsNullOrEmpty(key))
        {
            dt.DefaultView.RowFilter = "UserName Like '%" + key + "%'";
            dt = dt.DefaultView.ToTable();
        }
        pbRepeater.DataSource = dt;
        pbRepeater.DataBind();
        dateHtml.Text = GetDateTr(StartTime);
        BCDataBind();
    }
    //绑定前台Select
    private void BCDataBind() 
    {
      string json= JsonHelper.JsonSerialDataTable(bcBll.Sel());
      Page.ClientScript.RegisterStartupScript(this.GetType(), "Init", "DataBind('" + json + "');", true);
      DataTable dt = new DataTable();
        //根据起始时间,获取对应的数据,如果有组限制,则再加上组
      if (string.IsNullOrEmpty(Request.QueryString["gid"]))
      {
      }
      else
      {
      }
      string valueJson = JsonHelper.JsonSerialDataTable(dt);
      Page.ClientScript.RegisterStartupScript(this.GetType(), "setValue", "SetValue('" + valueJson + "');", true);
    }

    //生成表头
    private string GetDateTr(DateTime st,string type="week") 
    {
        string result= "<td>所属部门</td><td>姓名</td>";
        switch (type)
        {
            case "week":
                for (int i = 0; i <= 6; i++)
                {
                    result += "<td>" + st.AddDays(i).ToString("yyyy年MM月dd日<br/>") + GetDate(st.AddDays(i)) + "</td>";
                }
                break;
            case "month":
                for (int i = 0; i < 31; i++)
                {
                    result += "<td>" + st.AddDays(i).ToString("MM月dd日<br/>") + GetDate(st.AddDays(i)) + "</td>";
                }
                break;
        }
        return result;
    }
    protected void searchBtn_Click(object sender, EventArgs e)
    {
        DataBind(searchText.Text.Trim());
    }
    //获取提交数据，保存,根据数据库中记录，判断是更新还是新增(UserID与StartTime两样对到了)
    protected void saveBtn_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(Request.Form["userID"]))
        {

        }
        else
        {
            string[] userArr = Request.Form["userID"].Split(new char[]{','},StringSplitOptions.RemoveEmptyEntries);
            //组织为一张表，给底层插入或更新
            foreach (string userID in userArr)
            {
            }
            BCDataBind();
        }
    }

    protected void nextWeekBtn_Click(object sender, EventArgs e)
    {
        StartTime = StartTime.AddDays(7);
        dateHtml.Text = GetDateTr(StartTime);
        BCDataBind();
    }
    protected void preWeekBtn_Click(object sender, EventArgs e)
    {
        StartTime = StartTime.AddDays(-7);
        dateHtml.Text = GetDateTr(StartTime);
        BCDataBind();
    }
    protected void thisWeekBtn_Click(object sender, EventArgs e)
    {
        StartTime = GetStartTime(DateTime.Now);
        dateHtml.Text = GetDateTr(StartTime);
        BCDataBind();
    }

    protected void radioWeek_CheckedChanged(object sender, EventArgs e)
    {
        //用RadioList不好布局，所以换，后期更换为Html控件
        Month_Next.Visible = false;
        Week_Next.Visible = true;
        week_div.Visible = true;
        monthDiv.Visible = false;
        dateHtml.Text = GetDateTr(StartTime);
        DataBind();
        BCDataBind();
        radioMonth.Checked = false;

    }
    protected void radioMonth_CheckedChanged(object sender, EventArgs e)
    {
        Month_Next.Visible = true;
        Week_Next.Visible = false;
        week_div.Visible = false;
        monthDiv.Visible = true;
        radioWeek.Checked = false;
    }
    //值须为 2014-04-01  格式
    public DataTable SelByMonth(DateTime et) 
    {
        DataTable dt = new DataTable();
        //switch(st.DayOfWeek)
        //{
        //    case DayOfWeek.Monday:
        //        break;
        //    case DayOfWeek.Tuesday:
        //        break;
        //    case DayOfWeek.Wednesday:
        //        break;
        //    case DayOfWeek.Thursday:
        //        break;
        //    case DayOfWeek.Friday:
        //        break;
        //    case DayOfWeek.Saturday:
        //        break;
        //    case DayOfWeek.Sunday:
        //        break;
        //}
        ////先将其转化为星期一,
        DateTime st = GetStartTime(et);
        //int gid = 1;
        string TbName = "ZL_OA_PBTable";
        string sTime = st.ToString("yyyy-MM-dd");
        string eTime = et.AddMonths(1).ToString("yyyy-MM-01");
        string sql = "Select a.*,u.GroupID,u.UserName,g.GroupName From " + TbName + " as a Left Join ZL_User as u On a.UserID=u.UserID  Left Join ZL_Group as g On u.GroupID=g.GroupID Where  StartTime >='" + sTime + "' And StartTime <'" + eTime + "' Order By StartTime";//  

        dt = SqlHelper.ExecuteTable(CommandType.Text,sql);
        DataTable resultDT = TrunToEveryMan(dt, et);//横列
        //DataTable resultDT = TrunToEveryDay(dt, et);//竖列
        return resultDT;
    }

    //将其整合为一张表，(竖列,即一天的排班为一条记录),需要转化的表，开始时间
    public DataTable TrunToEveryDay(DataTable dt,DateTime st)
    {
        DataTable resultDT = new DataTable();//最终要返回的
        resultDT.Columns.Add(new DataColumn("Index", typeof(int)));
        resultDT.Columns.Add(new DataColumn("UserID", typeof(int)));
        resultDT.Columns.Add(new DataColumn("Week", typeof(string)));
        resultDT.Columns.Add(new DataColumn("StartTime", typeof(DateTime)));
        resultDT.Columns.Add(new DataColumn("BCID", typeof(int)));
        resultDT.Columns.Add(new DataColumn("UserName", typeof(string)));
        resultDT.Columns.Add(new DataColumn("GroupName", typeof(string)));
        int index = 0;
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            int uid = Convert.ToInt32(dt.Rows[i]["UserID"]);
            DateTime tTime = DateTime.Parse(dt.Rows[i]["StartTime"].ToString());
            for (int j = 0; j < 7; j++)//这里可能会有几个用户，每个用户需要一个独立的day指示器
            {
                DataRow dr = resultDT.NewRow();
                dr[0] = index++;
                dr[1] = uid;
                dr[2] = GetDate(tTime.AddDays(j));
                dr[3] = tTime.AddDays(j);
                dr[4] = dt.Rows[i][(j + 2)];//班次，从Money开始
                dr[5] = dt.Rows[i]["UserName"];
                dr[6] = dt.Rows[i]["GroupName"];
                resultDT.Rows.Add(dr);
            }
        }
        resultDT.DefaultView.RowFilter = "StartTime >='" + st.ToString("yyyy-MM-01") + "' And StartTime <'" + st.AddMonths(1).ToString("yyyy-MM-01") + "'";
        resultDT.DefaultView.Sort = "UserID ASC";
        return resultDT.DefaultView.ToTable();
    }
    //按用户汇总一个月的数据,需转换的表，开始时间,如果无数据，则为填充空,用户从Excel导入数据，则必须按周导入
    public DataTable TrunToEveryMan(DataTable dt, DateTime st) 
    {
        DataTable dataDT = TrunToEveryDay(dt,st);
        DataTable resultDT = CreateDT();
        for (int i = 0; i < dataDT.Rows.Count-1;)
        {
            DataRow dr = resultDT.NewRow();
            dr["UserID"] = dataDT.Rows[i]["UserID"];
            dr["UserName"] = dataDT.Rows[i]["UserName"];
            dr["GroupName"] = dataDT.Rows[i]["GroupName"];
            dr["StartTime"] = st;
            for (int j = 1; i < dataDT.Rows.Count && Convert.ToInt32(dr["UserID"]) == Convert.ToInt32(dataDT.Rows[i]["UserID"]); j++, i++)
            {
                int flag= (DateTime.Parse(dataDT.Rows[i]["StartTime"].ToString())).Day;
                dr["BC" + flag] = GetBCName(DataConvert.CLng(dataDT.Rows[i]["BCID"]));
                dr["BCColor" + flag] = GetBCColor(DataConvert.CLng(dataDT.Rows[i]["BCID"]));
            }
            resultDT.Rows.Add(dr);
        }
        return resultDT;
    }
    public string GetBCName(int bcid) 
    {
        string result = "未选择";
        if (bcid == 0)
        { }
        else
        {
            BCDataTable.DefaultView.RowFilter = "ID='"+bcid+"'";
            if (BCDataTable.DefaultView.ToTable().Rows.Count > 0)
                result = BCDataTable.DefaultView.ToTable().Rows[0]["BCName"].ToString();
            else 
            {
                result = "未知排班";
            }
        }
        return result;
    }
    public string GetBCColor(int bcid)
    {
        string result = "";
        if (bcid == 0)
        {
        }
        else
        {
            BCDataTable.DefaultView.RowFilter = "ID='" + bcid + "'";
            if (BCDataTable.DefaultView.ToTable().Rows.Count > 0)
                result = BCDataTable.DefaultView.ToTable().Rows[0]["BackColor"].ToString();
            else
            {
                result = "";
            }
        }
        return result;
    }

    //创建EvernMan的需要的表
    public DataTable CreateDT() 
    {
        DataTable dt = new DataTable();//最终要返回的

        for (int i = 1; i <=31; i++)
        {
            //dt.Columns.Add(new DataColumn("day"+i,typeof(DateTime)));
            dt.Columns.Add(new DataColumn("BC" + i, typeof(string)));
            dt.Columns.Add(new DataColumn("BCColor" + i, typeof(string)));
        }
        dt.Columns.Add(new DataColumn("UserID", typeof(int)));
        dt.Columns.Add(new DataColumn("UserName", typeof(string)));
        dt.Columns.Add(new DataColumn("GroupName", typeof(string)));
        dt.Columns.Add(new DataColumn("StartTime", typeof(DateTime)));
        return dt;
    }
    //获取当前是周几
    public string GetDate(DateTime dt)
    {
        string weekstr = "";
        switch (dt.DayOfWeek)
        {
            case DayOfWeek.Monday: weekstr = "星期一"; break;
            case DayOfWeek.Tuesday: weekstr = "星期二"; break;
            case DayOfWeek.Wednesday: weekstr = "星期三"; break;
            case DayOfWeek.Thursday: weekstr = "星期四"; break;
            case DayOfWeek.Friday: weekstr = "星期五"; break;
            case DayOfWeek.Saturday: weekstr = "星期六"; break;
            case DayOfWeek.Sunday: weekstr = "星期日"; break;
        }
        return weekstr;
    }
    //获取本周起始日期
    public DateTime GetStartTime()
    {
        return GetStartTime(DateTime.Now);
    }
    //根据时间，获取起始时间,即周一
    public DateTime GetStartTime(DateTime dt)
    {
        DateTime sWeek = dt.AddDays(1 - Convert.ToInt32(dt.DayOfWeek.ToString("d")));
        DateTime eWeek = sWeek.AddDays(6);
        return sWeek;
    }
    #region
    ////行列对转
    //public DataTable Col2Row(DataTable src, int columnHead)
    //{
    //    DataTable result = new DataTable();
    //    DataColumn myHead = src.Columns[columnHead];
    //    result.Columns.Add(myHead.ColumnName);
    //    for (int i = 0; i < src.Rows.Count; i++)
    //    {
    //        result.Columns.Add(src.Rows[i][myHead].ToString());
    //    }
    //    //
    //    foreach (DataColumn col in src.Columns)
    //    {
    //        if (col == myHead)
    //            continue;
    //        object[] newRow = new object[src.Rows.Count + 1];
    //        newRow[0] = col.ColumnName;
    //        for (int i = 0; i < src.Rows.Count; i++)
    //        {
    //            newRow[i + 1] = src.Rows[i][col];
    //        }
    //        result.Rows.Add(newRow);
    //    }
    //    return result;
    //}

    //public DataTable Col2Row(DataTable src, string columnHead)
    //{
    //    for (int i = 0; i < src.Columns.Count; i++)
    //    {
    //        if (src.Columns[i].ColumnName.ToUpper() == columnHead.ToUpper())
    //            return Col2Row(src, i);
    //    }
    //    return new DataTable();
    //}
    #endregion
    protected void preMonthBtn_Click(object sender, EventArgs e)
    {
        StartMonth = StartMonth.AddMonths(-1);
        DateTime st = DateTime.Parse(StartMonth.ToString("yyyy-MM-01"));
        monthHtml.Text = GetDateTr(st, "month");
        monthRepeater.DataSource = SelByMonth(st);
        monthRepeater.DataBind();
    }
    protected void NextMonthBtn_Click(object sender, EventArgs e)
    {
        StartMonth = StartMonth.AddMonths(1);
        DateTime st = DateTime.Parse(StartMonth.ToString("yyyy-MM-01"));
        monthHtml.Text = GetDateTr(st, "month");
        monthRepeater.DataSource = SelByMonth(st);
        monthRepeater.DataBind();
    }
    protected void thisMonthBtn_Click(object sender, EventArgs e)
    {
        StartMonth = DateTime.Now;
        DateTime st = DateTime.Parse(StartMonth.ToString("yyyy-MM-01"));
        monthHtml.Text = GetDateTr(st, "month");
        monthRepeater.DataSource = SelByMonth(st);
        monthRepeater.DataBind();
    }
}
//搜索，如何只判断年月日，不判断小时等
//Select * From ZL_OA_BC Where StartTime >='2014-04-19' And StartTime <'2014-04-20'
//在按星期的数据结构下，如何获取一个月的排班?


//---异常:
//1,从 char 数据类型到 datetime 数据类型的转换导致 datetime 值越界。
//原因:日期小于1700,日期过大,或日期异常,如4月只有30天,你要查4.31则会报此错
