using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL.ECharts;
using ZoomLa.BLL.User;
using ZoomLa.SQLDAL;

namespace ZoomLaCMS.Manage.User.Promo
{
    public partial class PromoChart : System.Web.UI.Page
    {
        private int Year { get { return DataConvert.CLng(Request.QueryString["Year"]); } }
        private int Month { get { return DataConvert.CLng(Request.QueryString["Month"]); } }
        B_User_Promo promoBll = new B_User_Promo();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Init();
                MyBind();
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='UserManage.aspx'>用户管理</a></li><li><a href='" + Request.RawUrl + "'>图表分析</a></li>");
            }
        }
        private void MyBind()
        {
            DataTable dt = promoBll.SelByTime();
            code.Value = BarChartBind(dt, "line");
            barcode.Value = BarChartBind(dt, "bar");
            //MapBind();
            PieChartBind(dt, "2015");
            //--------------
            TopRPT.DataSource = promoBll.SelByFilter(0, 0, "", 10);
            TopRPT.DataBind();
        }
        public string MapBind(DataTable dt)
        {
            return "";
        }
        public string BarChartBind(DataTable dt, string type)
        {
            //初始化图表
            BarChartOption option = new BarChartOption(new ChartTitle() { text = "推广统计", subtext = "按月统计" }, "", type);
            ChartLegend legend = new ChartLegend();
            List<ChartSeries> dataList = new List<ChartSeries>();
            //默认按用户统计
            List<string> years = new List<string>();//不存数据,只存年
            foreach (DataRow dr in dt.DefaultView.ToTable(true, "Year").Rows)
            {
                years.Add(dr["Year"].ToString());
            }
            foreach (string year in years)
            {
                List<int> months = new List<int>();//存12个月的数据,不足以0补齐
                for (int i = 1; i <= 12; i++)
                {
                    DataRow[] drs = dt.Select("Year=" + year + " AND Month=" + i);
                    if (drs.Length > 0) { months.Add(Convert.ToInt32(drs[0]["PCount"])); }
                    else { months.Add(0); }
                }
                dataList.Add(new ChartSeries()
                {
                    type = type,
                    name = year,
                    data_int = months.ToArray()
                });
            }
            /*数据完成,填充图表*/
            legend.data = years.ToArray();
            option.yAxis = new YAxis() { data = years.ToArray() };
            //与数据无须name对应,y轴需要name对应
            ((BarChartOption)option).AddData("1月,2月,3月,4月,5月,6月,7月,8月,9月,10月,11月,12月".Split(','), dataList, type);
            option.legend = legend;
            return option.ToString();
        }
        public void PieChartBind(DataTable dt, string wherestr, string field = "Year")
        {
            PieChartOption pieoption = new PieChartOption(new ChartTitle() { text = "推广统计", subtext = "按月统计" }, "");
            List<string> legeds = new List<string>();
            List<ChartSeries> dataList = new List<ChartSeries>();
            DataRow[] drs = dt.Select(field + "=" + wherestr);
            List<ChartData> datas = new List<ChartData>();
            foreach (DataRow dr in drs)
            {
                legeds.Add(dr["Month"].ToString());
                datas.Add(new ChartData() { name = dr["Month"].ToString(), value = DataConvert.CLng(dr["PCount"]) });
            }
            dataList.Add(new ChartSeries() { name = "", data_mod = datas.ToArray() });

            pieoption.AddData(new ChartLegend() { data = legeds.ToArray() }, dataList, "");
            piecode.Value = pieoption.ToString();
        }
        //public void MapBind() 
        //{
        //    string tool = "{show :true, orient : 'vertical',x: 'right',y: 'center',feature : {dataView: {show: true,readOnly: true},restore : {show: true},saveAsImage : {show: true}}}";
        //    ChartOption option = new MapChartOption(new ChartTitle()
        //    {
        //        text = "用户来源地汇总",
        //        subtext = "按地区"
        //    }, tool);
        //    option.tooltip = new ChartToolTip() { trigger = "item", formatter = null };
        //    ChartLegend legend = new ChartLegend();
        //    legend.data = "用户数".Split(',');
        //    //legend.data = "2013,2014,2015".Split(',');//年份
        //    List<ChartSeries> dataList = new List<ChartSeries>();
        //    dataList.Add(new ChartSeries()
        //    {
        //        name = "用户数",
        //        data_mod = SelByProvince().ToArray()
        //    });
        //    ((MapChartOption)option).AddData(legend, dataList);
        //    mapcode.InnerText = option.ToString();
        //}
        public List<ChartData> SelByProvince()
        {
            string sql = "SELECT Count(*)AS PCount,B.Province FROM ZL_User A LEFT JOIN ZL_UserBase B ON A.UserID=B.UserID WHERE B.Province!='' GROUP BY B.Province";
            DataTable dt = SqlHelper.ExecuteTable(sql);
            List<ChartData> list = new List<ChartData>();
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(new ChartData()
                {
                    name = dr["Province"].ToString().Replace("省", ""),
                    value = Convert.ToInt32(dr["PCount"])
                });
            }
            return list;
        }
    }
}