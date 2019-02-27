using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Common;
using ZoomLa.BLL;
using ZoomLa.SQLDAL;
using System.Data;
using System.Text;
using ZoomLa.Safe;

namespace ZoomLaCMS.Manage.Workload
{
    public partial class Subject : CustomerPageAction
    {
        private B_Node b_Node = new B_Node();
        private B_Content b_Content = new B_Content();
        private B_Admin b_Admin = new B_Admin();
        private B_User b_User = new B_User();
        protected B_Model bll = new B_Model();
        B_Content_Count countBll = new B_Content_Count();
        B_Group gpBll = new B_Group();
        string strHtml = string.Empty;
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
                Call.SetBreadCrumb(Master, "<li><a href='" + customPath2 + "Main.aspx'>工作台</a></li><li class='active'>工作统计</li>" + Call.GetHelp(52) + "<div class='pull-right down_ico' style='cursor: pointer' onclick='downtable();' title='导出数据表'> <span class='fa fa-download'></span> </div>");
            }
        }
        //初始化筛选
        void InitWhere()
        {
            string datatemp = "<label class='sealink btn btn-default {0}'><span data-href='Subject.aspx?nodeid={1}&year={2}&month={3}&model={4}'>{5}</span></label>";
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
        public void MyBind()
        {
            DataTable dt = SelDT();
            EGV.DataSource = dt;
            EGV.DataBind();
        }
        private DataTable SelDT()
        {
            DataTable dt = countBll.SelGroupByDate(NodeID, ModelID, Year, Month);
            return dt;
        }
        protected void count_Click(object sender, EventArgs e)
        {
            MyBind();
        }
        protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            EGV.PageIndex = e.NewPageIndex;
            DataBind();
        }
        protected void Export_Click(object sender, EventArgs e)
        {
            OfficeHelper office = new OfficeHelper();
            SafeC.DownStr(office.ExportExcel(SelDT(), "编辑,发稿量,评论量,点击数"), DateTime.Now.ToString("yyyyMMdd") + "编辑统计.xls", Encoding.Default);
        }
    }
}