using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZoomLa.BLL.ECharts
{
    //标题所在位置
    public class ChartTitle
    {
        public string text = "未定义标题", subtext = "";
        public string x = "right", y = "bottom";//left|right,top|bottom
        public string link = null, sublink = null;
    }
    public class ChartToolTip//鼠标何种情况下触发ToolTip,ToolTip格式
    {
        public string trigger = "item";
        public string formatter = "{a} : {b}";
        public int showDelay = 0;
        private ChartAxisPointer _axisPointer = null;
        public ChartAxisPointer axisPointer
        {
            get { return _axisPointer; }
            set { _axisPointer = value; }
        }
    }
    public class ChartAxisPointer//散点图新增
    {
        public string type = "cross";
        private ChartLineStyle _lineStyle = null;
        public ChartLineStyle lineStyle
        {
            get { return _lineStyle; }
            set { _lineStyle = value; }
        }
    }
    public class ChartLineStyle//散点图新增
    {
        public string type = "dashed";
        public int width = 1;
    }
    public class ChartLegend //图例配置
    {
        public string x = "left", y = null;
        public string orient = "horizontal";//vertical
        public string[] data = new string[] { "示例" };//不能为空,{主站,子站},需要与series内的每一组数据的name值保持一致 
    }
    //---------------------------
    //图表Series数据序列配置,相当于总配置
    public class XAxis
    {
        public string type = "category";
        public bool? boundaryGap = null;
        public string[] data;
        public int? power=null;
        public int? precision = null;
        public bool? scale = null;
        public int? splitNumber = null;
        public string name;
    }
    public class YAxis
    {
        public string type = "value";
        public string name;
        public int? power = null;
        public int? precision = null;
        public bool? scale = null;
        public int? splitNumber = null;
        public string[] data;
        public ChartAxisLine axisLine = null;
        public bool? boundaryGap = null;
    }
    public class ChartAxisLine 
    {
        public bool? onZero = null;
    }
    public class ChartSeries
    {
        //force|gauge(仪表盘)
        public string type = "force";//用何种图例显示
        public string name = "说明";
        public bool ribbonType = false;
        public bool? smooth = null;
        public Categories[] categories;//=new string[]{"主站","子站","二级子站"}
        public SeriesItemStyle itemStyle = null;
        public string selectedMode;
        public string roseType;
        //----------数据区
        public int[] data_int { private get; set; }
        public double[] data_doub { private get; set; }
        public List<double[]> data_doub_arr { private get; set; }//用于散点图,气泡图
        public string[] data_str { private get; set; }
        public ChartData[] data_mod { private get; set; }
        private object _data = null;
        //----------用于力导向图
        public List<ChartNode> nodes = null;//new List<ChartNode>();
        public List<ChartLink> links = null;//new List<ChartLink>();
        //----------
        public ChartDetail detail = new ChartDetail();
        public object radius = null;//["50%", "60%"],用于饼图
        public object[] center = null;//new string[] { "50%", "50%" };
        //public int minRadius = 15, maxRadius = 25;
        public string gravity = "1.1", scaling = "1.2";
        public bool draggable = false;
        public string linkSymbol = "arrow";
        public int steps = 10;
        public double coolDown = 0.9;
        public string x = null, x2 = null,y2=null;
        public int? max = null;
        public string symbol = null;
        public string symbolSize = null;
        public string mapType = null;//地图类型
        public bool? roam = null;//地图类型字段
        public string mapValueCalculation = null;//地图类型字段
        public object data
        {
            get
            {
                if (data_int != null) return data_int;
                else if (data_doub != null) return data_doub;
                else if (data_doub_arr != null) return data_doub_arr;
                else if (data_str != null) return data_str;
                else if (data_mod != null) return data_mod;
                else { return _data; }
            }
            set { _data = new object(); _data = value; }
        }
    }
    public class ChartData//仅用于仪表盘
    {
        public string name;
        public int value;
    }
    public class ChartDetail
    {
        public string formatter;
    }
    public class ChartNode
    {
        public int category = 0;
        public string name, value;
        //风格相关
        public string symbol = "rectangle"; //节点显示的形状,默认长方形
        public int[] symbolSize = new int[] { 60, 35 };//节点的宽高
        public int[] initial;//起始位置[500,40]
        public bool fixX, fixY;//设为false的话,会在随机位置生成图形
        public bool draggable;//可否拖动
        //public NodeItemStyle itemStyle = new NodeItemStyle();
        //----自定义
        public string url = null;
    }
    public class ChartLink
    {
        public string name;
        //起始节点和目标节点(name)
        public string source, target;
        public int weight = 1;
        //public ItemStyle itemStyle = new ItemStyle();
    }
    public class Categories
    {
        public Categories(){}
        public Categories(string n) { name = n; }
        public string name;
        public string symbol = "rectangle";
        //public int[] symbolSize;//该类目节点的大小
        //public ItemStyle itemStyle = new ItemStyle();
    }
    //----------主样式类,给Node,Data或series等处使用
    public class NodeItemStyle
    {
        NodeStyle nodeStyle = new NodeStyle();
        //public LinkStyle linkStyle = new LinkStyle();
    }
    public class SeriesItemStyle
    {
        public ChartNormal normal = new ChartNormal();
        public Emphasis emphasis = new Emphasis();
    }
    //-----------次样式类
    public class NodeStyle
    {
        public string brushType = "both";
        public string borderColor = "rgba(255,215,0,0.4)";
        public int borderWidth = 1;
        public string color = "#f08c2e";//填充色
    }
    public class LinkStyle//节点线之间
    {
        //线条类型，可选为：'curve'（曲线） | 'line'（直线）
        public string type;
        public string color = "#5182ab";
        public int width = 5;
    }
    public class ChartNormal
    {
        public ChartLabel label = new ChartLabel();
        private ChartLabelLine _labelLine = null;
        public ChartAreaStyle areaStyle = null;
        public ChartLabelLine labelLine 
        {
            get {
                return _labelLine;
            }
            set {
                if (_labelLine == null)
                {
                    _labelLine = new ChartLabelLine();
                }
                _labelLine = value;
            }
        }
    }
    public class ChartAreaStyle
    {
        public string type = "default";
    }
    public class ChartLabelLine 
    {
        public bool? show = null;
    }
    public class Emphasis
    {
        public ChartLabel label = new ChartLabel();
        public NodeStyle nodeStyle = new NodeStyle();
        public LinkStyle linkStyle = new LinkStyle();
        private ChartLabelLine _labelLine = null;
        public ChartLabelLine labelLine
        {
            get
            {
                return _labelLine;
            }
            set
            {
                if (_labelLine == null)
                {
                    _labelLine = new ChartLabelLine();
                }
                _labelLine = value;
            }
        }
    }
    public class ChartLabel
    {
        public bool show = true;
        public TextStyle textStyle = new TextStyle();
        public string formatter;
        public string position = "center";
    }
    public class TextStyle
    {
        public string fontSize;
        public string color = "#333";
        public string fontWeight;
    }
    //图表的表格形式配置
    public class ChartTableConfig
    {
        public ChartXData[] xdata =null;
        public ChartYData ydata=null;
        public string[] datacolum = null;
        //计算总数据列数
        public int XCount 
        {
            get 
            {
                int count = 0;
                foreach (var data in xdata)
                {
                    count += data.x[1];
                }
                return count;
            }
        }
    }
    /// <summary>
    /// y轴描述类
    /// </summary>
    public class ChartYData
    {
        //x[0]:第几列，x[1]第几行开始
        public int[] x = new int[2];
        public string value;
    }
    /// <summary>
    /// x轴数据配置
    /// </summary>
    public class ChartXData
    {
        /// <summary>
        /// 表格列数据(x[0]:表格的首列索引，x[1]:数据跨行)
        /// </summary>
        public int[] x = new int[2];
        public string name;//列名
        public string value;//绑定数据名
    }
    //以下为雷达新增字段
    public class ChartPolar
    {
        public ChartIndicator[] indicator = null;
    }
    public class ChartIndicator
    {
        public string text;
        public int min;
        public int max;
    }
    //以下为地图新增字段
    public class ChartDataRange
    {
        public int min = 0;
        public int max = 2500;
        public string x = "left";
        public string y = "bottom";
        public string[] text = new string[] { "高", "低" };
        public bool calculable = true;
    }
    public class ChartRoamController
    {
        public bool show = true;
        public string x = "right";
        public ChartMapTypeControl mapTypeControl = new ChartMapTypeControl();
    }
    public class ChartMapTypeControl
    {
        public bool china = true;
    }
}
