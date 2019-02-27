using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Data;
using Newtonsoft.Json.Linq;
using ZoomLa.Common;

namespace ZoomLa.BLL.ECharts
{
    //总配置类
    public class ChartOption
    {
        public ChartTitle title = new ChartTitle();
        public ChartToolTip tooltip = new ChartToolTip();
        public JObject toolbox = JsonConvert.DeserializeObject<JObject>("{show :true,feature : {dataView: {show: true,readOnly: true},restore : {show: true},saveAsImage : {show: true}}}");
        public ChartLegend legend = new ChartLegend();
        public List<ChartSeries> series = new List<ChartSeries>();
        public XAxis xAxis = null;//如不为空,则开启折线图等,所以这里必须特殊处理
        public YAxis yAxis = null;
        public bool calculable = false;
        //添加数据
        public virtual void AddDatas(string name, string datas) { }
        //返回该配置的数据表格式(table)
        public virtual string GetTableJson() { return ""; }
        public override string ToString()
        {
            JsonSerializerSettings setting = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
            return "option=" + JsonConvert.SerializeObject(this, Formatting.Indented, setting) + ";";
        }
        public ChartOption()
        {

        }
        //Disuse
        public ChartOption(string title, string subtitle)
        {
            this.title.text = title;
            this.title.subtext = subtitle;
        }
        public ChartOption(ChartTitle titleMod, string toolbox)
        {
            this.title = titleMod;
            if (!string.IsNullOrEmpty(toolbox))//为空使用默认配置
            { this.toolbox = JsonConvert.DeserializeObject<JObject>(toolbox); }
        }
    }
    //柱状图配置类,line
    public class BarChartOption : ChartOption
    {
        private string datatype;//图表类型(用于图表结构相同但类型不同的情况)
        private string tag = "";//用于相同类型不同显示形式的情况
        #region disuse
        public override void AddDatas(string name, string cdata)
        {
            if (!string.IsNullOrEmpty(cdata))
            {
                DataTable dt = JsonHelper.JsonToDT(cdata);
                foreach (DataRow item in dt.Rows)
                {
                    string itemName = item["name"].ToString();
                    if (itemName.Equals("legend"))
                    {
                        this.legend.data = JsonHelper.JsonDeserialize<string[]>(item["value"].ToString());
                        continue;
                    }
                    if (itemName.Equals("series"))
                    {
                        object[] objs = JsonHelper.JsonDeserialize<object[]>(item["value"].ToString());
                        for (int i = 0; i < objs.Length; i++)
                        {
                            this.series.Add(new ChartSeries() { name = this.legend.data[i], data = objs[i], type = datatype, itemStyle = null });
                        }

                    }
                    if (itemName.Equals("xaxis"))
                    {
                        object[] objs = JsonHelper.JsonDeserialize<object[]>(item["value"].ToString());
                        object[] curdatas = (object[])objs[0];
                        string[] datas = new string[curdatas.Length];
                        for (int i = 0; i < curdatas.Length; i++)
                        {
                            datas[i] = curdatas[i].ToString();
                        }
                        this.xAxis.data = datas;
                    }
                    //string[] itemdata = item["data"].ToString().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    //string[] data = itemdata;
                    //double[] ddata = new double[data.Length];
                    //for (int i = 0; i < data.Length; i++)
                    //{
                    //    ddata[i] = Convert.ToDouble(data[i]);
                    //}
                    //this.series.Add(new ChartSeries() { name = item["name"].ToString(), data = ddata, type = "bar" });
                }
            }
        }
        #endregion
        public void AddData(string[] rowdata, List<ChartSeries> seriesList, string tag)
        {
            this.series = seriesList;
            switch (tag)
            {
                case "area"://面积
                    this.xAxis = new XAxis() { data = rowdata };
                    for (int i = 0; i < this.series.Count; i++)
                    {
                        this.series[i].smooth = true;
                        this.series[i].itemStyle = new SeriesItemStyle() { normal = new ChartNormal() { areaStyle = new ChartAreaStyle() } };

                    }
                    break;
                case "ybar":
                case "yline":
                    this.yAxis = new YAxis() { data = rowdata, type = "category", axisLine = new ChartAxisLine() { onZero = false } };
                    this.xAxis.type = "value";
                    for (int i = 0; i < this.series.Count; i++)
                    {
                        this.series[i].smooth = true;
                        this.series[i].type = datatype;
                    }
                    break;
                default:
                    this.xAxis = new XAxis() { data = rowdata };
                    break;
            }
        }
        

        public override string GetTableJson()
        {
            ChartTableConfig tc = new ChartTableConfig();
            tc.xdata = new ChartXData[] { new ChartXData() { x = new int[] { 2, 1 }, name = "legend", value = "series" } };
            tc.ydata = new ChartYData() { x = new int[] { 1, 2 }, value = "xaxis" };
            tc.datacolum = new string[] { tc.xdata[0].name, tc.xdata[0].value, tc.ydata.value };
            JsonSerializerSettings setting = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
            return JsonConvert.SerializeObject(tc, setting);
        }
        /// <summary>
        /// 实例化柱状配置类
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="subttile">副标题</param>
        /// <param name="data">数据名称</param>
        /// <param name="category">X轴名</param>
        public BarChartOption(ChartTitle title, string toolbox, string datatype = "")
            : base(title, toolbox)
        {
            this.tooltip.trigger = "axis";
            this.calculable = true;
            this.xAxis = new XAxis();
            this.xAxis.type = "category";
            this.yAxis = new YAxis();
            this.yAxis.type = "value";
            this.datatype = datatype;
        }
    }
    //气泡配置类
    public class CircleChartOption : ChartOption
    {
        public override void AddDatas(string name, string cdata)
        {
            if (!string.IsNullOrEmpty(cdata))
            {
                DataTable dt = JsonHelper.JsonToDT(cdata);
                foreach (DataRow item in dt.Rows)
                {
                    string itemName = item["name"].ToString();
                    switch (itemName)
                    {
                        case "legend":
                            this.legend.data = JsonHelper.JsonDeserialize<string[]>(item["value"].ToString());
                            break;
                        case "series":
                            object[] objs = JsonHelper.JsonDeserialize<object[]>(item["value"].ToString());
                            for (int i = 0; i < objs.Length; i++)
                            {
                                this.series.Add(new ChartSeries() { name = this.legend.data[i], type = "scatter", data = objs[i], itemStyle = null, symbol = "circle" });
                            }
                            break;
                    }
                }
                //foreach (DataRow item in dt.Rows)
                //{
                //    string[] data = item["data"].ToString().Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                //    double[][] ddata = new double[data.Length][];
                //    for (int i = 0; i < data.Length; i++)
                //    {
                //        string[] curdata = data[i].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                //        ddata[i] = new double[3];
                //        ddata[i][0] = Convert.ToDouble(curdata[0]);
                //        ddata[i][1] = Convert.ToDouble(curdata[1]);
                //        ddata[i][2] = Convert.ToDouble(curdata[2]);
                //    }
                //    this.series.Add(new ChartSeries() 
                //    { 
                //        name = item["name"].ToString(), type = "scatter", data = ddata, itemStyle = null, symbol = "circle",
                //        symbolSize="function anonymous(value) {var radius = (value[2] - 0) * 16 / 100 + 4;return Math.max(Math.round(radius), 1) || 1;}"
                //    });
                //}
            }
        }
        public void AddData(ChartLegend legendMod, List<ChartSeries> seriesList)
        {
            this.legend = legendMod;
            this.series = seriesList;
            for (int i = 0; i < series.Count; i++)
            {
                series[i].symbol = "circle";
                series[i].type = "scatter";
                series[i].name = legendMod.data[i];
            }
        }
        public override string GetTableJson()
        {
            ChartTableConfig tc = new ChartTableConfig();
            tc.xdata = new ChartXData[] { new ChartXData() { x = new int[] { 1, 3 }, name = "legend", value = "series" } };
            tc.datacolum = new string[] { tc.xdata[0].name, tc.xdata[0].value };
            JsonSerializerSettings setting = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
            return JsonConvert.SerializeObject(tc, setting);
        }
        public override string ToString()//重写父类ToString()方法实现绑定气泡方法
        {
            string funcstr = " for (var i = 0; i < option.series.length; i++) {option.series[i].symbolSize = function anonymous(value) {var radius = (value[2] - 0) * 16 / 100 + 4;return Math.max(Math.round(radius), 1) || 1;}};";
            return base.ToString() + funcstr;
        }
        /// <summary>
        /// 实例化气泡图表配置类
        /// </summary>
        /// <param name="title"></param>
        /// <param name="subtitle"></param>
        /// <param name="ndata"></param>
        public CircleChartOption(ChartTitle title, string toolbox)
            : base(title, toolbox)
        {
            this.tooltip.trigger = "axis";
            this.tooltip.formatter = null;
            this.tooltip.showDelay = 0;
            this.tooltip.axisPointer = new ChartAxisPointer();
            this.tooltip.axisPointer.lineStyle = new ChartLineStyle();
            this.xAxis = new XAxis();
            this.xAxis.type = "value";
            this.xAxis.power = 1;
            this.xAxis.splitNumber = 4;
            this.xAxis.scale = true;
            this.yAxis = new YAxis();
            this.yAxis.type = "value";
            this.yAxis.power = 1;
            this.yAxis.splitNumber = 4;
            this.yAxis.scale = true;
        }
    }
    //漏斗图配置类
    public class FunnelChartOption : ChartOption
    {
        public override void AddDatas(string name, string cdatas)//cdatas格式:name,value|name,value|name,value
        {
            if (!string.IsNullOrEmpty(cdatas))
            {
                DataTable dt = JsonHelper.JsonToDT(cdatas);
                string[] names = null;
                List<string> legend = new List<string>();
                foreach (DataRow item in dt.Rows)
                {
                    string itemName = item["name"].ToString();
                    switch (itemName)
                    {
                        case "legend":
                            names = JsonHelper.JsonDeserialize<string[]>(item["value"].ToString());
                            break;
                        case "series":
                            object[] objs = JsonHelper.JsonDeserialize<object[]>(item["value"].ToString());
                            for (int i = 0; i < objs.Length; i++)
                            {
                                object[] curdatas = (object[])objs[i];
                                ChartData[] cdata = new ChartData[curdatas.Length];
                                for (int j = 0; j < curdatas.Length; j++)
                                {
                                    object[] c_curdata = (object[])curdatas[j];
                                    cdata[j] = new ChartData { name = c_curdata[0].ToString(), value = Convert.ToInt32(c_curdata[1]) };
                                    legend.Add(c_curdata[0].ToString());
                                }
                                this.series.Add(new ChartSeries() { name = names[i], type = "funnel", x = "15%", x2 = "15%", y2 = "10%", max = 100 });
                                this.series[i].itemStyle.normal.labelLine = new ChartLabelLine();
                                this.series[i].itemStyle.normal.labelLine.show = false;
                                this.series[i].itemStyle.emphasis.labelLine = new ChartLabelLine();
                                this.series[i].itemStyle.emphasis.labelLine.show = false;
                                this.series[i].data = cdata;
                            }
                            break;
                    }
                }
                this.legend.data = legend.ToArray();
            }
            //string[] datas = cdatas.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            //ChartData[] cdata = new ChartData[datas.Length];
            //for (int i = 0; i < datas.Length; i++)
            //{
            //    string[] value = datas[i].Split(',');
            //    cdata[i] = new ChartData() { name = value[0], value = Convert.ToInt32(value[1]) };
            //}
            //this.series.Add(new ChartSeries()
            //{
            //    name = name,
            //    type = "funnel",
            //    x = "15%",
            //    x2 = "15%",
            //    y2 = "10%",
            //    max = 100,
            //});
            //this.series[0].itemStyle.normal.labelLine = new ChartLabelLine();
            //this.series[0].itemStyle.normal.labelLine.show = false;
            //this.series[0].itemStyle.emphasis.labelLine = new ChartLabelLine();
            //this.series[0].itemStyle.emphasis.labelLine.show = false;
            //this.series[0].data = cdata;
        }
        public void AddData(ChartLegend legendMod, List<ChartSeries> seriesList)
        {
            this.legend = legendMod;
            this.series = seriesList;
            for (int i = 0; i < series.Count; i++)
            {
                series[i].type = "funnel";
                series[i].x = "15%"; series[i].x2 = "15%";
                series[i].y2 = "10%"; series[i].max = 100;
            }
        }
        public override string GetTableJson()
        {
            ChartTableConfig tc = new ChartTableConfig();
            tc.xdata = new ChartXData[] { new ChartXData() { x = new int[] { 1, 2 }, name = "legend", value = "series" } };
            tc.datacolum = new string[] { tc.xdata[0].name, tc.xdata[0].value };
            JsonSerializerSettings setting = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
            return JsonConvert.SerializeObject(tc, setting);
        }
        public FunnelChartOption(ChartTitle titleMod, string toolbox)
            : base(titleMod, toolbox)
        {
            this.title.x = "center";
            this.legend.orient = "vertical";
            this.legend.x = "left";
            this.legend.y = "center";
            this.calculable = true;
        }
    }
    //饼状图配置类
    public class PieChartOption : ChartOption
    {
        public override void AddDatas(string name, string cdatas)
        {
            if (!string.IsNullOrEmpty(cdatas))
            {
                DataTable dt = JsonHelper.JsonToDT(cdatas);
                string[] names = null;
                List<string> legend = new List<string>();
                foreach (DataRow item in dt.Rows)
                {
                    string itemName = item["name"].ToString();
                    switch (itemName)
                    {
                        case "legend":
                            names = JsonHelper.JsonDeserialize<string[]>(item["value"].ToString());
                            break;
                        case "series":
                            object[] objs = JsonHelper.JsonDeserialize<object[]>(item["value"].ToString());
                            for (int i = 0; i < objs.Length; i++)
                            {
                                object[] curdatas = (object[])objs[i];
                                ChartData[] cdata = new ChartData[curdatas.Length];
                                for (int j = 0; j < curdatas.Length; j++)
                                {
                                    object[] c_curdata = (object[])curdatas[j];
                                    cdata[j] = new ChartData { name = c_curdata[0].ToString(), value = Convert.ToInt32(c_curdata[1]) };
                                    legend.Add(c_curdata[0].ToString());
                                }
                                this.series.Add(new ChartSeries() { name = names[i], type = "pie", radius =  "55%", center = new string[] { "50%", "60%" }, itemStyle = null });
                                this.series[i].data = cdata;
                            }
                            break;
                    }
                }
                this.legend.data = legend.ToArray();
            }
        }
        public override string GetTableJson()
        {
            ChartTableConfig tc = new ChartTableConfig();
            tc.xdata = new ChartXData[] { new ChartXData() { x = new int[] { 1, 2 }, name = "legend", value = "series" } };
            tc.datacolum = new string[] { tc.xdata[0].name, tc.xdata[0].value };
            JsonSerializerSettings setting = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
            return JsonConvert.SerializeObject(tc, setting);
        }
        public void AddData(ChartLegend legendMod, List<ChartSeries> seriesList,string tag)
        {
            this.legend = legendMod;//主要获取行数据
            this.series = seriesList;
            switch (tag)
            {
                case "empy":
                    for (int i = 0; i < series.Count; i++)
                    {
                        series[i].type = "pie";
                        series[i].radius = new string[] { "50%","70%" };
                        series[i].center = new string[] { "50%", "60%" };
                        series[i].itemStyle = new SeriesItemStyle()
                        {
                            normal = new ChartNormal() { label = new ChartLabel() { show = false }, labelLine = new ChartLabelLine() { show = false } }
                            ,
                            emphasis = new Emphasis() { label = new ChartLabel() { show = true, position = "center", textStyle = new TextStyle() {fontSize="30",fontWeight="bold" } } }
                        };
                    }
                    break;
                case "inside":
                    series[0].type = "pie";
                    series[0].selectedMode = "single";
                    series[0].radius = new int[] {0,70 };
                    series[0].itemStyle = new SeriesItemStyle() { normal = new ChartNormal() { label = new ChartLabel() { position = "inside" }, labelLine = new ChartLabelLine() {show=false } } };
                    series[1].type = "pie";
                    series[1].radius = new int[] {100,140 };
                    break;
                case "areanan"://面积南丁玫瑰
                case "nanpie"://南丁玫瑰
                    this.legend.x = "center";
                    this.legend.y = "bottom";
                    series[0].type = "pie";
                    if (tag.Equals("areanan"))
                    {
                        series[0].radius = new int[] { 30, 90 };
                        series[0].roseType = "area";
                        series[0].itemStyle = null;
                    }
                    else
                    {
                        series[0].radius = new int[] { 20, 90 };
                        series[0].roseType = "radius";
                        series[0].itemStyle = new SeriesItemStyle()
                        {
                            normal = new ChartNormal() { label = new ChartLabel() { show = false }, labelLine = new ChartLabelLine() { show = false } }
                        ,
                            emphasis = new Emphasis() { label = new ChartLabel() { show = true }, labelLine = new ChartLabelLine() { show = true } }
                        };
                    }
                    series[0].center = new string[] { "50%", "50%" };
                    break;
                case "doublearea"://双玫瑰
                    this.legend.x = "center";
                    this.legend.y = "bottom";
                    series.Add(new ChartSeries());
                    series[0].radius = new int[] { 20, 90 };
                    series[0].roseType = "radius";
                    series[0].itemStyle = new SeriesItemStyle()
                    {
                        normal = new ChartNormal() { label = new ChartLabel() { show = false }, labelLine = new ChartLabelLine() { show = false } }
                    ,
                        emphasis = new Emphasis() { label = new ChartLabel() { show = true }, labelLine = new ChartLabelLine() { show = true } }
                    };
                    series[0].type = "pie";
                    series[0].center = new object[] { "25%", 150 };
                    series[1].radius = new int[] { 30, 90 };
                    series[1].roseType = "area";
                    series[1].type = "pie";
                    series[1].center = new object[] { "75%", 150 };
                    ChartData[] seriess = (ChartData[])(series[0].data);
                    ChartData[] cdata=new ChartData[seriess.Length];
                    seriess.CopyTo(cdata,0);
                    series[1].data = cdata;
                    series[1].name = "面积模式";
                    break;
                default:
                    for (int i = 0; i < series.Count; i++)
                    {
                        series[i].type = "pie";
                        series[i].radius = "55%";
                        series[i].center = new string[] { "50%", "60%" };
                    }
                    break;
            }
            
        }
        /// <summary>
        /// 饼状图表配置类
        /// </summary>
        /// <param name="title"></param>
        /// <param name="subtitle"></param>
        public PieChartOption(ChartTitle title, string toolbox)
            : base(title, toolbox)
        {
            this.title.x = "center";
            this.legend.orient = "vertical";
            this.legend.x = "left";
            this.legend.y = "center";
            this.calculable = true;
            this.tooltip.formatter = "{a} <br/>{b} : {c} ({d}%)";
        }
    }
    //雷达配置类(未实现)
    public class RadarChartOption : ChartOption
    {
        public ChartPolar[] polar = null;
        public override void AddDatas(string name, string datas)
        {
            if (!string.IsNullOrEmpty(datas))
            {
                DataTable dt = JsonHelper.JsonToDT(datas);
                foreach (DataRow item in dt.Rows)
                {
                    string itemname = item["name"].ToString();
                    switch (itemname)
                    {
                        case "legend":

                            break;
                    }
                }
            }
        }
        public override string GetTableJson()
        {
            ChartTableConfig tc = new ChartTableConfig();
            tc.xdata = new ChartXData[] { new ChartXData() { x = new int[] { 2, 1 }, name = "legend", value = "series" } };
            tc.ydata = new ChartYData() { x = new int[] { 1, 2 }, value = "indicator" };
            tc.datacolum = new string[] { "legend", "series", "indicator" };
            JsonSerializerSettings setting = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
            return JsonConvert.SerializeObject(tc, setting);
        }
        public override string ToString()
        {
            JsonSerializerSettings setting = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
            return "option=" + JsonConvert.SerializeObject(this, Formatting.None, setting) + ";";
        }
        public RadarChartOption(string title, string subtitle)
            : base(title, subtitle)
        {
            this.tooltip.trigger = "axis";
            this.legend.orient = "vertical";
            this.legend.x = "right";
            this.legend.y = "bottom";
        }
    }
    //散点图
    public class ScatterChartOption : ChartOption
    {
        public override void AddDatas(string name, string cdata)
        {
            if (!string.IsNullOrEmpty(cdata))
            {
                DataTable dt = JsonHelper.JsonToDT(cdata);//解析数据格式:{"name":"","data":"1,2|3,4|5,6"}
                foreach (DataRow item in dt.Rows)
                {
                    string itemName = item["name"].ToString();
                    if (itemName.Equals("legend"))
                    {
                        this.legend.data = JsonHelper.JsonDeserialize<string[]>(item["value"].ToString());
                        continue;
                    }
                    if (itemName.Equals("series"))
                    {
                        object[] objs = JsonHelper.JsonDeserialize<object[]>(item["value"].ToString());
                        for (int i = 0; i < objs.Length; i++)
                        {
                            this.series.Add(new ChartSeries() { name = this.legend.data[i], type = "scatter", data = objs[i], itemStyle = null });
                        }
                    }
                }
                //foreach (DataRow item in dt.Rows)
                //{
                //    string[] data = item["data"].ToString().Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                //    double[][] ddata = new double[data.Length][];
                //    for (int i = 0; i < data.Length; i++)
                //    {
                //        string[] curdata = data[i].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                //        ddata[i] = new double[2];
                //        ddata[i][0] = Convert.ToDouble(curdata[0]);
                //        ddata[i][1] = Convert.ToDouble(curdata[1]);
                //    }

                //    this.series.Add(new ChartSeries() { name = item["name"].ToString(), type = "scatter", data = ddata, itemStyle = null });
                //}
            }
        }
        public void AddData(ChartLegend legendMod, List<ChartSeries> seriesList)
        {
            this.legend = legendMod;
            this.series = seriesList;
            for (int i = 0; i < series.Count; i++)
            {
                series[i].type = "scatter";
                series[i].name = legendMod.data[i];
            }
        }
        /// <summary>
        /// 实例化散点图标类
        /// </summary>
        /// <param name="title"></param>
        /// <param name="subtitle"></param>
        /// <param name="ndata"></param>
        public ScatterChartOption(ChartTitle title, string toolbox)
            : base(title, toolbox)
        {
            this.tooltip.trigger = "axis";
            this.tooltip.formatter = null;
            this.tooltip.showDelay = 0;
            this.tooltip.axisPointer = new ChartAxisPointer();
            this.tooltip.axisPointer.lineStyle = new ChartLineStyle();
            this.xAxis = new XAxis();
            this.xAxis.type = "value";
            this.xAxis.power = 1;
            this.xAxis.precision = 2;
            this.xAxis.scale = true;
            this.yAxis = new YAxis();
            this.yAxis.type = "value";
            this.yAxis.power = 1;
            this.yAxis.precision = 2;
            this.yAxis.scale = true;
        }
    }
    //地图配置类
    public class MapChartOption : ChartOption
    {
        public ChartDataRange dataRange = null;
        public ChartRoamController roamController = null;

        public override void AddDatas(string name, string datas)
        {
            DataTable dt = JsonHelper.JsonToDT(datas);
            foreach (DataRow item in dt.Rows)
            {
                string itemName = item["name"].ToString();
                switch (itemName)
                {
                    case "legend":
                        this.legend.data = JsonConvert.DeserializeObject<string[]>(item["value"].ToString());
                        break;
                    case "series":
                        object[] objs = JsonHelper.JsonDeserialize<object[]>(item["value"].ToString());
                        for (int i = 0; i < objs.Length; i++)
                        {
                            object[] curdata = (object[])(objs[i]);
                            ChartData[] cdatas = new ChartData[curdata.Length];
                            for (int j = 0; j < curdata.Length; j++)
                            {
                                object[] c_cdata = (object[])curdata[j];
                                cdatas[j] = new ChartData() { name = c_cdata[0].ToString(), value = DataConverter.CLng(c_cdata[1]) };
                            }
                            this.series.Add(new ChartSeries() { name = this.legend.data[i], type = "map", mapType = "china", roam = false, mapValueCalculation = "sum" });
                            this.series[i].itemStyle = new SeriesItemStyle();
                            this.series[i].itemStyle.normal = new ChartNormal() { label = new ChartLabel() { show = true } };
                            this.series[i].itemStyle.emphasis = new Emphasis() { label = new ChartLabel() { show = true } };
                            this.series[i].data = cdatas;
                        }
                        break;
                }
            }
        }
        public override string ToString()
        {
            JsonSerializerSettings setting = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
            return "option=" + JsonConvert.SerializeObject(this, Formatting.None, setting) + ";";
        }
        public override string GetTableJson()
        {
            ChartTableConfig tc = new ChartTableConfig();
            tc.xdata = new ChartXData[] { new ChartXData() { x = new int[] { 1, 2 }, name = "legend", value = "series" } };
            tc.datacolum = new string[] { tc.xdata[0].name, tc.xdata[0].value };
            JsonSerializerSettings setting = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
            return JsonConvert.SerializeObject(tc, setting);
        }
        public void AddData(ChartLegend legendMod, List<ChartSeries> seriesList, string mapType = "china")
        {
            this.legend = legendMod;
            this.series = seriesList;
            for (int i = 0; i < series.Count; i++)
            {
                series[i].type = "map";
                series[i].mapType = mapType;
                series[i].roam = false;
                series[i].name = legendMod.data[i];
                series[i].itemStyle = new SeriesItemStyle();
                series[i].itemStyle.emphasis.label.show = true;//地图上标识文字的显示
                series[i].itemStyle.normal.label.show = true;
            }
        }
        public MapChartOption(ChartTitle titleMod, string toolbox)
            : base(titleMod, toolbox)
        {
            this.title.x = "center";
            this.legend.orient = "vertical";
            this.legend.x = "left";
            //this.legend.data = ndata;
            this.dataRange = new ChartDataRange();
            this.roamController = new ChartRoamController();
        }
    }
    //仪表盘
    public class DashOption : ChartOption
    {
        public DashOption(ChartTitle title, string toolbox)
            : base(title, toolbox)
        {
            this.tooltip.formatter = "{a} <br/>{b} : {c}%";
        }
        public void AddData(List<ChartSeries> seriesList)
        {
            this.series = seriesList;
            for (int i = 0; i < series.Count; i++)
            {
                series[i].type = "gauge";
                series[i].detail.formatter = "{value}%";
            }
        }
    }
}
