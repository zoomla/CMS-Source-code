﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AskResult.aspx.cs" Inherits="ZoomLaCMS.Design.AskResult" MasterPageFile="~/Common/Master/Ionic.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>结果分析</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div class="bar bar-header bar-calm">
    <a href="default.aspx" class="button icon ion-chevron-left"></a>
    <span class="title" runat="server" id="title_sp"></span>
</div>
<div style="margin-top:44px;">
    <div class="list">
    <asp:Repeater runat="server" ID="RPT" OnItemDataBound="RPT_ItemDataBound" EnableViewState="false">
        <ItemTemplate>
            <div class="item" id="item_<%#Eval("ID") %>">
                <div><strong>第<%#Container.ItemIndex+1 %>题：<%#Eval("QTitle") %></strong><span class="qtype">(<%#GetQType() %>)</span></div>
                <div runat="server" visible='<%#Eval("QType","").Equals("radio") %>'>
                    <table class="table table-bordered table-striped table-condensed">
                        <tr>
                            <td>选项</td>
                            <td>小计</td>
                            <td>比例</td>
                        </tr>
                        <asp:Repeater runat="server" ID="Radio_RPT" EnableViewState="false">
                            <ItemTemplate>
                                <tr>
                                    <td><%#Eval("Text") %></td>
                                    <td><%#Eval("count") %></td>
                                    <td><%#GetPercent() %></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                </div>
                <div runat="server" visible='<%#Eval("QType","").Equals("checkbox") %>'>
                    <table class="table table-bordered table-striped table-condensed">
                        <tr>
                            <td>选项</td>
                            <td>小计</td>
                            <td>比例</td>
                        </tr>
                        <asp:Repeater runat="server" ID="Checkbox_RPT" EnableViewState="false">
                            <ItemTemplate>
                                <tr>
                                    <td><%#Eval("Text") %></td>
                                    <td><%#Eval("count") %></td>
                                    <td><%#GetPercent() %></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                </div>
                <div runat="server" visible='<%#Eval("QType","").Equals("blank") %>'>
                    【<a href="javascript:;" onclick="scope.listbyqid('<%#Eval("ID") %>');" class="answer_more">查看本题答案详细信息</a>】
                </div>
                <div runat="server" visible='<%#Eval("QType","").Equals("score") %>'>
                    <asp:TextBox runat="server" CssClass="rating" ID="Score_T" Style="display: none;" disabled="disabled"/>
                </div>
                <div class="chart_wrap" <%#ShowChartWrap() %>>
                    <a href="javascript:;" class="button button-small button-calm" onclick="showchart('<%#Eval("ID") %>','pie')"><i class="fa fa-pie-chart"></i> 饼状</a>
                    <a href="javascript:;" class="button button-small button-calm" onclick="showchart('<%#Eval("ID") %>','circle')"><i class="fa fa-life-bouy"></i> 圆环</a>
                    <a href="javascript:;" class="button button-small button-calm" onclick="showchart('<%#Eval("ID") %>','bar')"><i class="fa fa-bar-chart"></i> 柱状</a>
                    <a href="javascript:;" class="button button-small button-calm" onclick="showchart('<%#Eval("ID") %>','line')"><i class="fa fa-reorder"></i> 条形</a>
                    <a href="javascript:;" class="button button-small button-assertive" onclick="hidechart('<%#Eval("ID") %>')"><i class="fa fa-remove"></i> 关闭</a>
                    <asp:HiddenField runat="server" ID="chart_hid" />
                    <div class="zlchart" id="chart_<%#Eval("ID") %>" style="width:100%;" ></div>
                </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>
    </div>
    <div runat="server" id="empty_div" visible="false" class="text-center">
        <i class="fa fa-inbox" style="font-size:120px;"></i>
        <div class="padding-top">还没有人回答该问卷</div>
    </div>
</div>
<div ng-app="app"><div ng-controller="APPCtrl"></div></div>
<style type="text/css">
.item .qtype {color:#999; display:inline-block;margin-left:5px;}
.item .ans_more {color:#0094ff;font-weight:bold;}
.item .rating-container .caption {display:none;}
#empty_div {color:#999;}
.popup-container .popup {width: 95%;}
</style>
<link href="/dist/css/bootstrap.min.css" rel="stylesheet" />
<link href="/dist/css/star-rating.min.css" rel="stylesheet" />
<script src="/dist/js/star-rating.min.js"></script>
<script src="/Plugins/ECharts/build/source/echarts.js"></script>
<script>
$(".rating").rating('refresh', { showClear: false });
var scope = null;
angular.module("app", ["ionic"]).controller("APPCtrl", function ($scope, $ionicPopup) {
    scope = $scope;
    $scope.showpop = function () {
        $ionicPopup.alert({
            title: '详情列表',
            templateUrl: "templates/diag/anslist.html?v=" + Math.random(),
            scope: $scope,
            buttons: [{ text: "关闭", type: 'button-positive', onTap: function (e) { } }]
        });
    }
    $scope.list = [];
    $scope.listbyqid = function (qid) {
        $.post("/design/ask/server/answer.ashx?action=listbyqid", {"qid":qid}, function (data) {
            APIResult.ifok(data, function (result) {
                $scope.list = result;
                $scope.$digest();
                $scope.showpop();
            })
        })
    }
});
var mychart = {
    pie: function (data) {
        //饼图,[{name:"",value:50}]
        var option = {
            title: { left: "left", text: '', x: 'center' },//数据统计
            tooltip: { trigger: 'item', formatter: "{a} <br/>{b} : {c} ({d}%)" },
            legend: { show: false, orient: 'vertical', left: 'left', data: [] },
            series: [{
                name: '数据量', type: 'pie', radius: '55%', center: ['50%', '50%'], data: [],
                itemStyle: { emphasis: { shadowBlur: 10, shadowOffsetX: 0, shadowColor: 'rgba(0, 0, 0, 0.5)' } }
            }]
        };
        option.legend.data = [];
        option.series[0].data = [];
        for (var i = 0; i < data.length; i++) {
            option.legend.data.push(data[i].name);
            option.series[0].data.push(data[i]);
        }
        return option;
    },
    circle: function (data) {
        //环形,[{name:"",value:50}]
        var option = {
            title: { left: "left", text: '', x: 'center' },//数据统计
            tooltip: { trigger: 'item', formatter: "{a} <br/>{b} : {c} ({d}%)" },
            legend: { orient: 'vertical', left: 'left', data: [] },
            series: [{
              name: '数据量',type: 'pie',radius: ['50%', '70%'],avoidLabelOverlap: false,data: []
          }]
        };
        option.legend.data = [];
        option.series[0].data = [];
        for (var i = 0; i < data.length; i++) {
            option.legend.data.push(data[i].name);
            option.series[0].data.push(data[i]);
        }
        return option;
    },
    bar: function (data) {
        var option = {
            color: ['#3398DB'],
            tooltip: { trigger: 'axis', axisPointer: { type: 'shadow' } },
            grid: { left: '3%', right: '4%', bottom: '3%', containLabel: true },
            xAxis: [{ type: 'category', axisTick: { alignWithLabel: true }, data: [] }],
            yAxis: [{ type: 'value' }],
            series: [{ name: '', type: 'bar', barWidth: '30%', data: [] }]
        };
        option.series[0].data = [];
        for (var i = 0; i < data.length; i++) {
            option.xAxis[0].data.push(data[i].name);
            option.series[0].data.push(data[i].value);
        }
        return option;
    },
    line: function (data) {
        var option = {
            color: ['#3398DB'],
            title: { text: '' },
            tooltip: { trigger: 'axis', axisPointer: { type: 'shadow' } },
            legend: { data: [''] },
            grid: { left: '3%', right: '4%', bottom: '3%', containLabel: true },
            xAxis: { type: 'value', boundaryGap: [0, 0.01] },
            yAxis: { type: 'category', data: [] },
            series: [{ name: '', type: 'bar', data: [] }]
        };
        option.legend.data = [];
        option.series[0].data = [];
        for (var i = 0; i < data.length; i++) {
            option.yAxis.data.push(data[i].name);
            option.series[0].data.push(data[i].value);
        }
        return option;
    },
};
//var chart = echarts.init(document.getElementById('pie_chart'));
//chart.setOption(mychart.pie([{ name: "第一项", value: 30 }, { name: "第二项", value: 70 }]));
//chart.setOption(mychart.circle([{ name: "第一项", value: 30 }, { name: "第二项", value: 70 }]));
//chart.setOption(mychart.bar());
//chart.setOption(mychart.line([{ name: "第一项", value: 30 }, { name: "第二项", value: 70 }, { name: "新的选功", value: 88 }]));
//chart.setOption(mychart.bar([{ name: "第一项", value: 30 }, { name: "第二项", value: 70 }, { name: "新的选功", value: 88 }]));
var charts = [];
function showchart(id, type) {
    var chart = null;
    var data = JSON.parse($("#item_" + id).find("#chart_hid").val());
    var $chartDiv = $("#item_" + id).find(".zlchart");
    if ($chartDiv.height() == 0) { $chartDiv.height(260); }
    $chartDiv.show();
    if (!charts[id]) {
        chart = echarts.init(document.getElementById("chart_" + id));
        charts[id] = chart;
    }
    else { chart = charts[id]; }
    chart.clear();
    switch (type) {
        case "pie":
            chart.setOption(mychart.pie(data));
            break;
        case "circle":
            chart.setOption(mychart.circle(data));
            break;
        case "bar":
            chart.setOption(mychart.bar(data));
            break;
        case "line":
            chart.setOption(mychart.line(data));
            break;
    }
}
function hidechart(id) {
    var $chartDiv = $("#item_" + id).find(".zlchart");
    $chartDiv.hide();
}
</script>
</asp:Content>