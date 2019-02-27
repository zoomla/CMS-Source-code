using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Content;
using ZoomLa.BLL.ECharts;
using ZoomLa.Common;
using ZoomLa.Model;

/*
 * 需扩展:
 * 1,暂只支持线图与饼图
 * 2,数据的强壮性,如自动补齐0值
 */ 
public partial class test_echart : CustomerPageAction
{
    B_Content_Chart chartBll = new B_Content_Chart();
    M_Content_Chart chartMod = new M_Content_Chart();
    public int Mid { get { return DataConverter.CLng(Request.QueryString["ID"]); } }
    public string Type
    {
        get
        {
            return string.IsNullOrEmpty(Request["type"]) ? "funnel" : Request["type"];
        }
    }
    public string Tag
    {
        get
        {
            return Request.QueryString["tag"];
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (function.isAjax())
        {

            string action = Request.Form["action"];
            string result = "";
            switch (action)
            {
                case "createimg":
                    {
                        ChartTitle title = JsonConvert.DeserializeObject<ChartTitle>(Request.Form["title"], new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });//数组中包含空数据等仍会造成问题
                        string toolbox = Request.Form["toolbox"];
                        ChartOption option = null;
                        switch (Type)
                        {
                            #region AJAX请求
                            case "bar":
                            case "line":
                                {
                                    option = new BarChartOption(title, toolbox, Type);
                                    ChartPackage packmod = JsonConvert.DeserializeObject<ChartPackage>(Request.Form["packmod"]);
                                    ((BarChartOption)option).AddData(packmod.rowdata, packmod.series, Tag);
                                }
                                break;
                            case "pie":
                                {
                                    option = new PieChartOption(title, toolbox);
                                    ChartLegend legend = new ChartLegend();
                                    ChartPackage packmod = JsonConvert.DeserializeObject<ChartPackage>(Request.Form["packmod"]);
                                    legend.data = packmod.rowdata;
                                    ((PieChartOption)option).AddData(legend, packmod.series,Tag);
                                }
                                break;
                            case "dash":
                                {
                                    option = new DashOption(title, toolbox);
                                    ChartPackage packmod = JsonConvert.DeserializeObject<ChartPackage>(Request.Form["packmod"]);
                                    ((DashOption)option).AddData(packmod.series);
                                }
                                break;
                            case "funnel":
                                {
                                    option = new FunnelChartOption(title, toolbox);
                                    ChartPackage packmod = JsonConvert.DeserializeObject<ChartPackage>(Request.Form["packmod"]);
                                    ChartLegend legend = new ChartLegend();
                                    legend.data = packmod.rowdata;
                                    ((FunnelChartOption)option).AddData(legend, packmod.series);
                                }
                                break;
                            case "scatter":
                                {
                                    option = new ScatterChartOption(title, toolbox);
                                    ChartPackage packmod = JsonConvert.DeserializeObject<ChartPackage>(Request.Form["packmod"]);
                                    ChartLegend legend = new ChartLegend();
                                    legend.data = packmod.rowdata;
                                    ((ScatterChartOption)option).AddData(legend, packmod.series);
                                }
                                break;
                            case "circle":
                                {
                                    option = new CircleChartOption(title, toolbox);
                                    ChartPackage packmod = JsonConvert.DeserializeObject<ChartPackage>(Request.Form["packmod"]);
                                    ChartLegend legend = new ChartLegend();
                                    legend.data = packmod.rowdata;
                                    ((CircleChartOption)option).AddData(legend, packmod.series);
                                }
                                break;
                            case "map":
                                {
                                    option = new MapChartOption(title, toolbox);
                                    ChartPackage packmod = JsonConvert.DeserializeObject<ChartPackage>(Request.Form["packmod"]);
                                    ChartLegend legend = new ChartLegend();
                                    legend.data = packmod.rowdata;
                                    ((MapChartOption)option).AddData(legend, packmod.series);
                                }
                                break;
                            #endregion
                        }
                        option.calculable = bool.Parse(Request.Form["calculable"]);
                        result = option.ToString();
                        break;
                    }
            }
            Response.Clear();
            Response.Write(result); Response.Flush(); Response.End();
        }
        else if (!IsPostBack)
        {
            if (Mid > 0)
            {
                chartMod = chartBll.SelReturnModel(Mid);
                ChartTitle_Hid.Value = chartMod.ChartTitle;
                Cdate_Hid.Value = chartMod.CDate.ToString();
                ToolBox_Hid.Value = chartMod.ToolBox;
                Package_Hid.Value = chartMod.Package;
                code.Text = chartMod.option;
                function.Script(this, "LoadEditConfig();");
            }
            else//调用初始化方法
            {
                function.Script(this, "LoadNewConfig();");
            }
            Call.SetBreadCrumb(Master, "<li><a href='" + customPath2 + "Main.aspx'>工作台</a></li><li><a href='Default.aspx'>智慧图表</a></li><li><a href='AddChart.aspx'>创建图表</a></li><li class='active'><a href='" + Request.RawUrl + "'>图表配置</a>[" + chartMod.GetTypeStr(Type) + "]</li>");
        }
    }
    protected void Save_Btn_Click1(object sender, EventArgs e)
    {
        if (Mid > 0) { chartMod = chartBll.SelReturnModel(Mid); }
        if (string.IsNullOrEmpty(Title_T.Text))
            function.WriteErrMsg("标题信息不能为空!");
        chartMod.TName = Title_T.Text;
        chartMod.ChartTitle = ChartTitle_Hid.Value;
        chartMod.ToolBox = ToolBox_Hid.Value;
        chartMod.Package = Package_Hid.Value;
        chartMod.option = code.Text;
        chartMod.SType = Type;
        chartMod.Tag = Tag;
        if (Mid > 0)
        {
            chartBll.UpdateByID(chartMod);
        }
        else
        {
            chartBll.Insert(chartMod);
        }
        function.WriteSuccessMsg("保存成功", "Default.aspx");
    }
}
//数据包,用于前后端通信
public class ChartPackage
{
    public string[] rowdata;
    public List<ChartSeries> series = new List<ChartSeries>();
}