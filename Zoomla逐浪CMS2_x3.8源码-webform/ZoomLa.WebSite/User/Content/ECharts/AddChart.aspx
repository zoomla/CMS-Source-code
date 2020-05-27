<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddChart.aspx.cs" Inherits="Manage_ECartImg_ECharts" MasterPageFile="~/User/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>智慧图表</title>
    <link href="/Plugins/Third/ystep/css/ystep.css" rel="stylesheet" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div id="pageflag" data-nav="office" data-ban="chart"></div>
<div class="container margin_t5">
<ol class="breadcrumb">
<li><a href='Default.aspx'>智慧图表</a></li>
<li class='active'>创建图表</li> 
</ol>
</div>
<div class="container" role="tabpanel">
<div class="ystep1"></div>
<ul class="nav nav-tabs" role="tablist">
<li role="presentation" class="active"><a href="#All" aria-controls="All" role="tab" data-toggle="tab">全部图表</a></li>
<li role="presentation"><a href="#Bar" aria-controls="Bar" role="tab" data-toggle="tab">柱状图表</a></li>
<li role="presentation"><a href="#Line" aria-controls="Line" role="tab" data-toggle="tab">折线图表</a></li>
<li role="presentation"><a href="#Pie" aria-controls="Pie" role="tab" data-toggle="tab">饼状图表</a></li>
<li role="presentation"><a href="#Gauge" aria-controls="Gauge" role="tab" data-toggle="tab">仪表盘</a></li>
<li role="presentation"><a href="#Map" aria-controls="Map" role="tab" data-toggle="tab">地图图表</a></li>
<li role="presentation"><a href="#Circle" aria-controls="Circle" role="tab" data-toggle="tab">气泡图表</a></li>
<li role="presentation"><a href="#Funnel" aria-controls="Funnel" role="tab" data-toggle="tab">漏斗图表</a></li>
<li role="presentation"><a href="#Radar" aria-controls="Radar" role="tab" data-toggle="tab">雷达图表</a></li>
<li role="presentation"><a href="#Scatter" aria-controls="Scatter" role="tab" data-toggle="tab">散点图表</a></li>
</ul>
<div class="tab-content">
<div role="tabpanel" class="tab-pane active" id="All">
<h3>柱状图</h3>
<div class="splitdiv"></div>
<div class="col-md-3 col-sm-3 col-xs-4 imglist" data-type="bar">
<div class="imglist_content">
<img src="/App_Themes/Admin/bar1.png" />
<h4>标准柱状图</h4>
<span>基本的柱状图</span>
</div>
</div>
<div class="col-md-3 col-sm-3 col-xs-4 imglist" data-type="bar" data-tag="ybar">
<div class="imglist_content">
<img src="/App_Themes/Admin/bar3.png" />
<h4>标准条形图</h4>
<span>柱状图的横纵坐标互换</span>
</div>
</div>
<div class="col-md-3 col-sm-3 col-xs-4 imglist">
<div class="imglist_content">
<img class="imgdisable" src="/App_Themes/Admin/bar21.png" />
<h4>堆积柱状图</h4>
<span>任意系列多维度堆积</span>
</div>
</div>
<div class="col-md-3 col-sm-3 col-xs-4 imglist">
<div class="imglist_content">
<img class="imgdisable" src="/App_Themes/Admin/bar4.png" />
<h4>堆积条形图</h4>
<span>任意系列多维度堆积</span>
</div>
</div>
<div class="clearfix"></div>
<h3>折线图</h3>
<div class="splitdiv"></div>
<div class="col-md-3 col-sm-3 col-xs-4 imglist" data-type="line">
<div class="imglist_content">
<img src="/App_Themes/Admin/line11.png" />
<h4>标准折线图</h4>
<span>普通的折线图</span>
</div>
</div>
<div class="col-md-3 col-sm-3 col-xs-4 imglist" data-type="line" data-tag="area">
<div class="imglist_content">
<img src="/App_Themes/Admin/line3.png" />
<h4>标准面积图</h4>
<span>填充样式，平滑曲线</span>
</div>
</div>
<div class="col-md-3 col-sm-3 col-xs-4 imglist" data-type="line" data-tag="yline">
<div class="imglist_content">
<img src="/App_Themes/Admin/line5.png" />
<h4>标准折线图</h4>
<span>横纵坐标互换，平滑曲线</span>
</div>
</div>
<div class="col-md-3 col-sm-3 col-xs-4 imglist">
<div class="imglist_content">
<img class="imgdisable" src="/App_Themes/Admin/line4.png" />
<h4>堆积面积图</h4>
<span>任意系列多维度堆积</span>
</div>
</div>
<div class="clearfix"></div>
<h3>饼状图</h3>
<div class="splitdiv"></div>
<div class="col-md-3 col-sm-3 col-xs-4 imglist" data-type="pie">
<div class="imglist_content">
<img src="/App_Themes/Admin/pie1.png" />
<h4>标准饼图</h4>
<span>基本的饼图</span>
</div>
</div>
<div class="col-md-3 col-sm-3 col-xs-4 imglist" data-type="pie" data-tag="empy">
<div class="imglist_content">
<img src="/App_Themes/Admin/pie2.png" />
<h4>标准环形图</h4>
<span>基本的环形饼图</span>
</div>
</div>
<div class="col-md-3 col-sm-3 col-xs-4 imglist" data-type="pie" data-tag="inside">
<div class="imglist_content">
<img src="/App_Themes/Admin/pie3.png" />
<h4>嵌套饼图</h4>
<span>由两个(或多个)饼图嵌套而成</span>
</div>
</div>
<div class="col-md-3 col-sm-3 col-xs-4 imglist" data-type="pie" data-tag="nanpie">
<div class="imglist_content">
<img src="/App_Themes/Admin/pie10.png" />
<h4>南丁格尔玫瑰图</h4>
<span>半径模式的玫瑰图</span>
</div>
</div>
<div class="col-md-3 col-sm-3 col-xs-4 imglist" data-type="pie" data-tag="areanan">
<div class="imglist_content">
<img src="/App_Themes/Admin/pie11.png" />
<h4>南丁格尔玫瑰图</h4>
<span>面积模式的玫瑰图</span>
</div>
</div>
<div class="col-md-3 col-sm-3 col-xs-4 imglist" data-type="pie" data-tag="doublearea">
<div class="imglist_content">
<img src="/App_Themes/Admin/pie4.png" />
<h4>南丁格尔玫瑰图</h4>
<span>双玫瑰图</span>
</div>
</div>
<div class="clearfix"></div>
<h3>仪表图</h3>
<div class="splitdiv"></div>
<div class="col-md-3 col-sm-3 col-xs-4 imglist" data-type="dash">
<div class="imglist_content">
<img src="/App_Themes/Admin/gauge1.png" />
<h4>标准仪表图</h4>
<span>标准仪表图</span>
</div>
</div>
<div class="clearfix"></div>
<h3>气泡图</h3>
<div class="splitdiv"></div>
<div class="col-md-3 col-sm-3 col-xs-4 imglist" data-type="circle">
<div class="imglist_content">
<img src="/App_Themes/Admin/scatter2.png" />
<h4>标准气泡图</h4>
<span>气泡大小计算</span>
</div>
</div>
<div class="clearfix"></div>
<h3>地图</h3>
<div class="splitdiv"></div>
<div class="col-md-3 col-sm-3 col-xs-4 imglist" data-type="map">
<div class="imglist_content">
<img src="/App_Themes/Admin/map1.png" />
<h4>标准中国地图</h4>
<span>标准中国地图(省级)</span>
</div>
</div>
<div class="col-md-3 col-sm-3 col-xs-4 imglist">
<div class="imglist_content">
<img class="imgdisable" src="/App_Themes/Admin/map4.png" />
<h4>标准世界地图</h4>
<span>标准世界地图</span>
</div>
</div>
<div class="col-md-3 col-sm-3 col-xs-4 imglist">
<div class="imglist_content">
<img class="imgdisable" src="/App_Themes/Admin/map3.png" />
<h4>标准省市区地图</h4>
<span>标准省市区地图</span>
</div>
</div>
<div class="col-md-3 col-sm-3 col-xs-4 imglist">
<div class="imglist_content">
<img class="imgdisable" src="/App_Themes/Admin/map4.png" />
<h4>多地图</h4>
<span>多个地图并存</span>
</div>
</div>
<div class="clearfix"></div>
<h3>漏斗图</h3>
<div class="splitdiv"></div>
<div class="col-md-3 col-sm-3 col-xs-4 imglist" data-type="funnel">
<div class="imglist_content">
<img src="/App_Themes/Admin/funnel10.png" />
<h4>标准漏斗图</h4>
<span>基本的漏斗图</span>
</div>
</div>
<div class="col-md-3 col-sm-3 col-xs-4 imglist">
<div class="imglist_content">
<img class="imgdisable" src="/App_Themes/Admin/funnel12.png" />
<h4>带辅助背景的漏斗图</h4>
<span>背景调谐了漏斗的“丁字形状”</span>
</div>
</div>
<div class="clearfix"></div>
<h3>雷达图</h3>
<div class="splitdiv"></div>
<div class="col-md-3 col-sm-3 col-xs-4 imglist">
<div class="imglist_content">
<img class="imgdisable" src="/App_Themes/Admin/radar1.png" />
<h4>雷达图表</h4>
<span>标准雷达图</span>
</div>
</div>
<div class="clearfix"></div>
<h3>散点图</h3>
<div class="splitdiv"></div>
<div class="col-md-3 col-sm-3 col-xs-4 imglist" data-type="scatter">
<div class="imglist_content">
<img src="/App_Themes/Admin/scatter1.png" />
<h4>标准散点图</h4>
<span>标注，标线</span>
</div>
</div>

</div>
<div role="tabpanel" class="tab-pane" id="Bar">
<div class="col-md-3 col-sm-3 col-xs-4 imglist" data-type="bar">
<div class="imglist_content">
<img src="/App_Themes/Admin/bar1.png" />
<h4>标准柱状图</h4>
<span>基本的柱状图</span>
</div>
</div>
<div class="col-md-3 col-sm-3 col-xs-4 imglist" data-type="bar" data-tag="ybar">
<div class="imglist_content">
<img src="/App_Themes/Admin/bar3.png" />
<h4>标准条形图</h4>
<span>柱状图的横纵坐标互换</span>
</div>
</div>
<div class="col-md-3 col-sm-3 col-xs-4 imglist">
<div class="imglist_content">
<img class="imgdisable" src="/App_Themes/Admin/bar21.png" />
<h4>堆积柱状图</h4>
<span>任意系列多维度堆积</span>
</div>
</div>
<div class="col-md-3 col-sm-3 col-xs-4 imglist">
<div class="imglist_content">
<img class="imgdisable" src="/App_Themes/Admin/bar4.png" />
<h4>堆积条形图</h4>
<span>任意系列多维度堆积</span>
</div>
</div>
</div>
<div role="tabpanel" class="tab-pane" id="Line">
<div class="col-md-3 col-sm-3 col-xs-4 imglist" data-type="line">
<div class="imglist_content">
<img src="/App_Themes/Admin/line11.png" />
<h4>标准折线图</h4>
<span>普通的折线图</span>
</div>
</div>
<div class="col-md-3 col-sm-3 col-xs-4 imglist" data-type="line" data-tag="area">
<div class="imglist_content">
<img src="/App_Themes/Admin/line3.png" />
<h4>标准面积图</h4>
<span>填充样式，平滑曲线</span>
</div>
</div>
<div class="col-md-3 col-sm-3 col-xs-4 imglist" data-type="line" data-tag="yline">
<div class="imglist_content">
<img src="/App_Themes/Admin/line5.png" />
<h4>标准折线图</h4>
<span>横纵坐标互换，平滑曲线</span>
</div>
</div>
<div class="col-md-3 col-sm-3 col-xs-4 imglist">
<div class="imglist_content">
<img class="imgdisable" src="/App_Themes/Admin/line4.png" />
<h4>堆积面积图</h4>
<span>任意系列多维度堆积</span>
</div>
</div>
</div>
<div role="tabpanel" class="tab-pane" id="Pie">
<div class="col-md-3 col-sm-3 col-xs-4 imglist" data-type="pie">
<div class="imglist_content">
<img src="/App_Themes/Admin/pie1.png" />
<h4>标准饼图</h4>
<span>基本的饼图</span>
</div>
</div>
<div class="col-md-3 col-sm-3 col-xs-4 imglist" data-type="pie" data-tag="empy">
<div class="imglist_content">
<img src="/App_Themes/Admin/pie2.png" />
<h4>标准环形图</h4>
<span>基本的环形饼图</span>
</div>
</div>
<div class="col-md-3 col-sm-3 col-xs-4 imglist" data-type="pie" data-tag="inside">
<div class="imglist_content">
<img src="/App_Themes/Admin/pie3.png" />
<h4>嵌套饼图</h4>
<span>由两个(或多个)饼图嵌套而成</span>
</div>
</div>
<div class="col-md-3 col-sm-3 col-xs-4 imglist" data-type="pie" data-tag="nanpie">
<div class="imglist_content">
<img src="/App_Themes/Admin/pie10.png" />
<h4>南丁格尔玫瑰图</h4>
<span>半径模式的玫瑰图</span>
</div>
</div>
<div class="col-md-3 col-sm-3 col-xs-4 imglist" data-type="pie" data-tag="areanan">
<div class="imglist_content">
<img src="/App_Themes/Admin/pie11.png" />
<h4>南丁格尔玫瑰图</h4>
<span>面积模式的玫瑰图</span>
</div>
</div>
<div class="col-md-3 col-sm-3 col-xs-4 imglist" data-type="pie" data-tag="doublearea">
<div class="imglist_content">
<img src="/App_Themes/Admin/pie4.png" />
<h4>南丁格尔玫瑰图</h4>
<span>双玫瑰图</span>
</div>
</div>
</div>
<div role="tabpanel" class="tab-pane" id="Gauge">
<div class="col-md-3 col-sm-3 col-xs-4 imglist" data-type="dash">
<div class="imglist_content">
<img src="/App_Themes/Admin/gauge1.png" />
<h4>标准仪表图</h4>
<span>标准仪表图</span>
</div>
</div>
</div>
<div role="tabpanel" class="tab-pane" id="Circle">
<div class="col-md-3 col-sm-3 col-xs-4 imglist" data-type="circle">
<div class="imglist_content">
<img src="/App_Themes/Admin/scatter2.png" />
<h4>标准气泡图</h4>
<span>气泡大小计算</span>
</div>
</div>
</div>
<div role="tabpanel" class="tab-pane" id="Funnel">
<div class="col-md-3 col-sm-3 col-xs-4 imglist" data-type="funnel">
<div class="imglist_content">
<img src="/App_Themes/Admin/funnel10.png" />
<h4>标准漏斗图</h4>
<span>基本的漏斗图</span>
</div>
</div>
<div class="col-md-3 col-sm-3 col-xs-4 imglist">
<div class="imglist_content">
<img class="imgdisable" src="/App_Themes/Admin/funnel12.png" />
<h4>带辅助背景的漏斗图</h4>
<span>背景调谐了漏斗的“丁字形状”</span>
</div>
</div>
</div>
<div role="tabpanel" class="tab-pane" id="Radar">
<div class="col-md-3 col-sm-3 col-xs-4 imglist">
<div class="imglist_content">
<img class="imgdisable" src="/App_Themes/Admin/radar1.png" />
<h4>雷达图表</h4>
<span>标准雷达图</span>
</div>
</div>
</div>
<div role="tabpanel" class="tab-pane" id="Scatter">
<div class="col-md-3 col-sm-3 col-xs-4 imglist" data-type="scatter">
<div class="imglist_content">
<img src="/App_Themes/Admin/scatter1.png" />
<h4>标准散点图</h4>
<span>标注，标线</span>
</div>
</div>
</div>
<div role="tabpanel" class="tab-pane" id="Map">
<div class="col-md-3 col-sm-3 col-xs-4 imglist" data-type="map">
<div class="imglist_content">
<img src="/App_Themes/Admin/map1.png" />
<h4>标准中国地图</h4>
<span>标准中国地图(省级)</span>
</div>
</div>
<div class="col-md-3 col-sm-3 col-xs-4 imglist">
<div class="imglist_content">
<img class="imgdisable" src="/App_Themes/Admin/map4.png" />
<h4>标准世界地图</h4>
<span>标准世界地图</span>
</div>
</div>
<div class="col-md-3 col-sm-3 col-xs-4 imglist">
<div class="imglist_content">
<img class="imgdisable" src="/App_Themes/Admin/map3.png" />
<h4>标准省市区地图</h4>
<span>标准省市区地图</span>
</div>
</div>
<div class="col-md-3 col-sm-3 col-xs-4 imglist">
<div class="imglist_content">
<img class="imgdisable" src="/App_Themes/Admin/map4.png" />
<h4>多地图</h4>
<span>多个地图并存</span>
</div>
</div>
</div>
</div>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script src="/Plugins/Third/ystep/js/ystep.js"></script>
    <script>
        $(function () {
            $(".imglist").click(function () {
                var type = $(this).data("type");
                var tag = $(this).data("tag")?$(this).data("tag"):"";
                if (!type || type == "") { alert("该功能仅对商业用户开放,请选择其它图表或联系逐浪软件取得授权"); return; }
                location = "AddChart2.aspx?Type=" + $(this).data("type")+"&tag="+tag;
            });
            //步骤插件
            $(".ystep1").loadStep({
                size: "large",
                color: "green",
                steps: [{
                    title: "选择",
                    content: "选择图表"
                }, {
                    title: "配置",
                    content: "设置基本参数"
                }, {
                    title: "编辑",
                    content: "对表格进行编辑"
                }, {
                    title: "完成",
                    content: "点击保存完成操作"
                }]
            });
            $().ready(function () {
                openmenu('menu2');
            });
        })
    </script>
</asp:Content>