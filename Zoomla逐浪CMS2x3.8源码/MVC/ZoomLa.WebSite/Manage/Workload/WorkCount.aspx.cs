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
using ZoomLa.Safe;
using System.Text;

namespace ZoomLaCMS.Manage.Workload
{
    public partial class WorkCount : System.Web.UI.Page
    {
        B_Content_Count contentBll = new B_Content_Count();
        B_Model modelBll = new B_Model();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (function.isAjax())
            {
                string action = Request.Form["action"];
                DataTable dt = null;
                switch (action)
                {
                    case "groupcount":
                        dt = contentBll.GroupDayCount(DataConverter.CLng(Request.Params["NodeID"]), Convert.ToInt32(Request.Form["year"]), Convert.ToInt32(Request.Form["month"]), Convert.ToInt32(Request.Form["mid"]));
                        Response.Write("{\"nodeid\":\"" + DataConverter.CLng(Request.Params["NodeID"]) + "\",\"attr\":" + JsonHelper.JsonSerialDataTable(dt) + "}");
                        break;
                    default:
                        break;
                }
                Response.Flush(); Response.End();
            }
            else if (!IsPostBack)
            {
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='Subject.aspx?Type=subject'>工作统计</a></li><li class='active'>时间统计</li>" + Call.GetHelp(52)
                                        + " <div class='pull-right down_ico' onclick='downtable();' style='cursor: pointer' title='导出数据表'> <span class='fa fa-download'></span> </div>");
                InitWhere();
            }
        }
        //初始化筛选
        void InitWhere()
        {
            string datatemp = "<label class='btn btn-default {0}'><input type='radio' name='{1}' value='{2}' {3}>{4}</label>";
            Years_Li.Text = string.Format(datatemp, "active", "years", "-1", "checked", "全部");
            for (int i = 0; i < 10; i++)
            {
                Years_Li.Text += string.Format(datatemp, "", "years", DateTime.Now.Year - i, "", (DateTime.Now.Year - i) + "年");
            }
            Months_Li.Text = string.Format(datatemp, "active", "months", "-1", "checked", "全部");
            for (int i = 0; i < 12; i++)
            {
                Months_Li.Text += string.Format(datatemp, "", "months", i + 1, "", (i + 1) + "月");
            }
            DataTable dt = modelBll.GetModel("'内容模型'", "");
            Models_Li.Text = string.Format(datatemp, "active", "model", "-1", "checked", "全部");
            foreach (DataRow item in dt.Rows)
            {
                Models_Li.Text += string.Format(datatemp, "", "model", item["ModelID"], "", item["ModelName"]);
            }
        }
        protected void DownFile_Click(object sender, EventArgs e)
        {

        }
        protected void Export_B_Click(object sender, EventArgs e)
        {
            OfficeHelper office = new OfficeHelper();
            DataTable dt = contentBll.GroupDayCount(DataConverter.CLng(Request.Form["curNid_hid"]), Convert.ToInt32(Request.Form["years"]), Convert.ToInt32(Request.Form["months"]), Convert.ToInt32(Request.Form["model"]));
            dt.Columns.Add("Date");
            for(int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["Date"] = DataConverter.CDate(dt.Rows[i]["Years"] + "-" + dt.Rows[i]["Months"] + "-" + dt.Rows[i]["Days"]).ToString("yyyy-MM-dd"); ;
            }
            DataTable newDt = dt.DefaultView.ToTable(false, "Date,PCount,ComCount,Hits".Split(','));
            SafeC.DownStr(office.ExportExcel(newDt, "日期,发稿量,评论数,点击数"), DateTime.Now.ToString("yyyyMMdd") + "时间统计.xls", Encoding.Default);
        }
    }
}