using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Common;
using System.Data;
using ZoomLa.BLL.Plat;
using ZoomLa.Model.Plat;
using ZoomLa.SQLDAL;

namespace ZoomLaCMS.MIS.Ke
{
    public partial class SubjectForWeek : System.Web.UI.Page
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
        public DateTime curDate
        {
            get { return ViewState["curDate"] == null ? DateTime.Now : DateTime.Parse(ViewState["curDate"].ToString()); }
            set { ViewState["curDate"] = value; }
        }
        string datatlp = "<tr><td class='text-center'>@index</td>@weekdata</tr>";//周视图
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (UserID <= 0)
                    UserID = buser.GetLogin().UserID;
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
                if (typeMod == null)
                    function.WriteErrMsg("该类别日程不存在!");
                TopSubName_Li.Text = "<span style='color:#999'>" + typeMod.Title + "</span>的";
            }
            else
            { TopSubName_Li.Text = "所有"; }
            SubList_RPT.DataSource = dt;
            SubList_RPT.DataBind();

            if (dt.Rows.Count == 0)
                EmptySub_Div.Visible = true;
            else
                EmptySub_Div.Visible = false;

            dt = sdlBll.SelByWeek(curDate, UserID, TypeID);
            InitWeek(dt);
            //抽出最近日程
            dt = sdlBll.SelTopSubject(UserID, TypeID);
            MyTop_RPT.DataSource = dt;
            MyTop_RPT.DataBind();
            if (dt.Rows.Count == 0)
                listempty_div.Visible = true;
            else
                listempty_div.Visible = false;

        }
        //初始化周视图
        public void InitWeek(DataTable dt)
        {
            int curweek = Convert.ToInt32(curDate.DayOfWeek);
            DateTime startDate = curDate.AddDays(-curweek + 1);
            DateTime endDate = curDate.AddDays(7 - curweek);
            DateSpan_L.Text = GetWeekTip();
            string datahtml = "";
            //输出日历标题
            for (int i = 0; i < 7; i++)
            {
                DateTime tempdate = curDate.AddDays((-curweek + 1 + i));
                Literal titleli = DateTitle.FindControl("TitleWeek_Li_D" + (i + 1)) as Literal;
                titleli.Text = tempdate.ToString("MM月dd日");
            }
            for (int i = 0; i < 24; i++)//小时循环
            {
                string daysstr = "";//每天数据模板
                for (int j = 0; j < 7; j++)
                {
                    DateTime tempdate = curDate.AddDays((-curweek + 1 + j));
                    daysstr += "<td class='datas' data-date='" + tempdate.ToString("yyyy-MM-dd") + "' data-hour='@hour'>" + GetContent(tempdate.Date.AddHours(i), dt) + "</td>";
                }
                datahtml += datatlp.Replace("@index", i.ToString()).Replace("@weekdata", daysstr).Replace("@hour", i.ToString());
            }
            WeekData_Li.Text = datahtml;
        }
        public string GetContent(DateTime date, DataTable dt)
        {
            dt.DefaultView.RowFilter = "StartDate<='" + date.AddMinutes(59).AddSeconds(59) + "' AND EndDate>='" + date + "'";//筛选符合时间条件的数据
            DataTable daysdata = dt.DefaultView.ToTable();
            if (daysdata.Rows.Count > 0)
            {
                //自适应计算
                string contenttlp = "<div class='content' data-id='@id' onclick='ViewDetail(this,@id)' style='width:" + (100 / daysdata.Rows.Count) + "%'>@name</div>";
                string html = "";
                foreach (DataRow item in daysdata.Rows)
                {
                    string Name = item["Name"].ToString();
                    html += contenttlp.Replace("@name", Name).Replace("@id", item["ID"].ToString());
                }
                return html;
            }
            return "";
        }
        public string GetWeekTip()
        {
            int curweek = Convert.ToInt32(curDate.DayOfWeek);
            DateTime startDate = curDate.AddDays(-curweek + 1);
            DateTime endDate = curDate.AddDays(7 - curweek);
            if (curDate.Date == DateTime.Now.Date.AddDays(-7))
                return "上周";
            if (curDate.Date == DateTime.Now.Date)
                return "本周";
            if (curDate.Date == DateTime.Now.Date.AddDays(7))
                return "下周";
            return startDate.ToString("yyyy年MM月dd日") + "-" + endDate.ToString("yyyy年MM月dd日");
        }

        //获取课程名
        public string GetSubName()
        {
            return Eval("Title").ToString();
        }
        protected void PreWeek_Btn_Click(object sender, EventArgs e)
        {
            curDate = curDate.AddDays(-7);
            MyBind();
        }
        protected void NextWeek_Btn_Click(object sender, EventArgs e)
        {
            curDate = curDate.AddDays(7);
            MyBind();
        }
        protected void BtnAdd_Click(object sender, EventArgs e)
        {
            sdlMod.Name = string.IsNullOrEmpty(Name_T.Text) ? "无标题" : Name_T.Text;
            sdlMod.TaskType = TypeID;
            sdlMod.StartDate = DataConverter.CDate(StartDate_Hid.Value.Trim());
            sdlMod.EndDate = DataConverter.CDate(EndDate_Hid.Value.Trim());
            if (sdlMod.StartDate > sdlMod.EndDate)
                function.WriteErrMsg("开始日期不能大于结束日期!");
            sdlMod.Describe = Request.Form["describe"].ToString();
            sdlMod.UserID = UserID;
            sdlBll.Insert(sdlMod);
            MyBind();
        }
        protected void AddType_B_Click(object sender, EventArgs e)
        {
            int typeid = DataConvert.CLng(TypeID_Hid.Value);
            M_MisInfo typeMod = typeid > 0 ? typebll.SelReturnModel(typeid) : new M_MisInfo();
            typeMod.Title = Type_T.Text;
            typeMod.MID = UserID;
            Type_T.Text = "";
            if (typeid > 0)
                typebll.UpdateByID(typeMod);
            else
                typebll.insert(typeMod);
            MyBind();
        }
        protected void SubList_RPT_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "DelType":
                    typebll.Del(DataConvert.CLng(e.CommandArgument));
                    sdlBll.DelByType(UserID, TypeID);
                    Response.Redirect("SubjectForWeek.aspx");
                    break;
                default:
                    break;
            }
        }
    }
}