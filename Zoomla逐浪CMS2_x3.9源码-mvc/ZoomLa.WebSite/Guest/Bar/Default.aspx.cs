using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Common;
using ZoomLa.Model.Message;
using System.Data;
using ZoomLa.SQLDAL;
using System.Text.RegularExpressions;


namespace ZoomLaCMS.Guest.Bar
{
    public partial class Default : CustomerPageAction
    {
        B_Guest_Bar barBll = new B_Guest_Bar();
        M_Guest_Bar barMod = new M_Guest_Bar();
        B_GuestBookCate cateBll = new B_GuestBookCate();
        public DataTable CateDT
        {
            get
            {
                if (ViewState["CateDT"] == null)
                    ViewState["CateDT"] = cateBll.GetCateList();
                return (DataTable)ViewState["CateDT"];
            }
            set { ViewState["CateDT"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MyBind();
            }
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            CateDT = null;
        }
        void MyBind()
        {
            B_User bu = new B_User();
            M_UserInfo mui = bu.GetLogin();
            ParentRPT.DataSource = cateBll.Cate_SelByType(M_GuestBookCate.TypeEnum.PostBar, 0);
            ParentRPT.DataBind();
            Top_Rpt.DataSource = barBll.SelFocus();
            Top_Rpt.DataBind();
            Week_Rpt.DataSource = barBll.SelTop(15);//每周热门
            Week_Rpt.DataBind();
            string tycount = barBll.SelYTCount();
            YCount_L.Text = tycount.Split(':')[0];
            TCount_L.Text = tycount.Split(':')[1];
            TopRPT.DataSource = GetBarImg();
            TopRPT.DataBind();
        }
        public DataTable GetBarImg()
        {
            RegexHelper regexHelper = new RegexHelper();
            DataTable dt = barBll.SelTop1(4);
            dt.Columns.Add(new DataColumn("TopImg", typeof(string)));
            dt.Columns.Add(new DataColumn("Index", typeof(int)));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                dr["Index"] = i;
                string value = regexHelper.GetValueBySE(dr["SubTitle"].ToString(), "<img", "/>");
                if (!string.IsNullOrEmpty(value))
                {
                    value = value.Replace("'", "\"");
                    dr["TopImg"] = regexHelper.GetHtmlAttr(value, "src");
                }
            }
            if (dt.Rows.Count < 1)
            {
                DataRow dr = dt.NewRow();
                dr["Index"] = 0;
                dr["ID"] = 0;
                dr["Title"] = "暂无数据";
                dr["TopImg"] = "/Images/nopic.gif";
                dt.Rows.Add(dr);
            }
            return dt;
        }
        public string getTitle()
        {
            string title = Eval("Title").ToString();
            return title.Length < 31 ? title : title.Substring(0, 26) + "...";
        }
        public string getNowWeek()
        {
            string week = "";
            switch (DateTime.Now.DayOfWeek)
            {
                case DayOfWeek.Friday:
                    week = "周五";
                    break;
                case DayOfWeek.Monday:
                    week = "周一";
                    break;
                case DayOfWeek.Saturday:
                    week = "周六";
                    break;
                case DayOfWeek.Sunday:
                    week = "周日";
                    break;
                case DayOfWeek.Thursday:
                    week = "周四";
                    break;
                case DayOfWeek.Tuesday:
                    week = "周二";
                    break;
                case DayOfWeek.Wednesday:
                    week = "周三";
                    break;
                default:
                    break;
            }
            return week;
        }
        public string ConverDate(object o, string format)
        {
            if (o != null && o != DBNull.Value)
            {
                return DataConvert.CDate(o).ToString(format);
            }
            else
            {
                return DateTime.Now.ToString(format);
            }
        }
        public string GetUrl()
        {
            int id = Convert.ToInt32(Eval("ID"));
            if (id < 1)
            {
                return "/PItem?id=" + Eval("ID");
            }
            else return "#";
        }
        //栏目列表绑定
        protected void ParentRPT_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item != null && e.Item.ItemType != ListItemType.Footer)
            {
                Repeater rpt = e.Item.FindControl("ChildRPT") as Repeater;
                DataRowView dr = e.Item.DataItem as DataRowView;
                CateDT.DefaultView.RowFilter = "";
                CateDT.DefaultView.RowFilter = " ParentID=" + dr["CateID"].ToString();
                rpt.DataSource = CateDT.DefaultView.ToTable();
                rpt.DataBind();
            }
        }
    }
}