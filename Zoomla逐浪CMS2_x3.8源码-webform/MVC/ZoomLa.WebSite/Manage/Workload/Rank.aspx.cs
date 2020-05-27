using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Safe;

namespace ZoomLaCMS.Manage.Workload
{
    public partial class Rank : System.Web.UI.Page
    {
        B_Content_Count countBll = new B_Content_Count();
        B_Model bll = new B_Model();
        public string ZType { get { return Request.QueryString["Type"] ?? ""; } }
        public int Year { get { return DataConverter.CLng(Request.QueryString["year"]); } }
        public int Month { get { return DataConverter.CLng(Request.QueryString["month"]); } }
        public int ModelID { get { return DataConverter.CLng(Request.QueryString["model"]); } }
        public int NodeID { get { return DataConverter.CLng(Request.QueryString["nodeid"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitWhere();
                MyBind();
            }
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='Subject.aspx?Type=subject'>工作统计</a></li><li class='active'>排行榜</li><div class='pull-right down_ico' onclick='downtable();' style='cursor: pointer' title='导出数据表'> <span class='fa fa-download'></span> </div>");
        }
        public void MyBind()
        {
            RPT.DataSource = GetDt();
            RPT.DataBind();
        }
        private DataTable GetDt()
        {
            DataTable dt = new DataTable();
            switch (ZType.ToLower())
            {
                case "comment":
                    dt = countBll.SelRank(NodeID, ModelID, Year, Month, 2);
                    break;
                default:
                    dt = countBll.SelRank(NodeID, ModelID, Year, Month, 1);
                    break;
            }
            return dt;
        }
        //初始化筛选
        public void InitWhere()
        {
            string datatemp = "<label class='sealink btn btn-default {0}'><span data-href='Rank.aspx?nodeid={1}&year={2}&month={3}&model={4}'>{5}</span></label>";
            string active = Year == 0 ? "active" : "";
            Years_Li.Text = string.Format(datatemp, active, NodeID, 0, Month, ModelID, "全部");
            for (int i = 0; i < 10; i++)
            {
                active = DateTime.Now.Year - i == Year ? "active" : "";
                Years_Li.Text += string.Format(datatemp, active, NodeID, DateTime.Now.Year - i, Month, ModelID, (DateTime.Now.Year - i) + "年");
            }
            active = Month == 0 ? "active" : "";
            Months_Li.Text = string.Format(datatemp, active, NodeID, Year, 0, ModelID, "全部");
            for (int i = 0; i < 12; i++)
            {
                active = i + 1 == Month ? "active" : "";
                Months_Li.Text += string.Format(datatemp, active, NodeID, Year, i + 1, ModelID, (i + 1) + "月");
            }
            DataTable dt = bll.GetModel("'内容模型'", "");
            active = ModelID == 0 ? "active" : "";
            Models_Li.Text = string.Format(datatemp, active, NodeID, Year, Month, 0, "全部");
            foreach (DataRow item in dt.Rows)
            {
                active = DataConverter.CLng(item["ModelID"]) == ModelID ? "active" : "";
                Models_Li.Text += string.Format(datatemp, active, NodeID, Year, Month, item["ModelID"], item["ModelName"]);
            }
        }

        protected void DownToExcel_B_Click(object sender, EventArgs e)
        {
            OfficeHelper office = new OfficeHelper();
            string name = ZType.ToLower().Equals("comment") ? "评论排行.xls" : "点击排行.xls";
            SafeC.DownStr(office.ExportExcel(GetDt(), "标题,点击数,评论数,录入者,录入时间"), DateTime.Now.ToString("yyyyMMdd") + "点击排行.xls", Encoding.Default);
        }
    }
}