<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="pie-basic.aspx.cs" Inherits="ZoomLaCMS.Plugins.Chart.pie_basic" EnableViewStateMac="false" MasterPageFile="~/Common/Master/Empty.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title></title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
     <%--<div id="container" style="width:<%=BiaoS.Width %>px; height:<%=BiaoS.Height %>px; margin:0;float:left"></div>--%>
        <div id="container" style="width:300px; height:300px; margin:0;float:left"></div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
        <script type="text/javascript">
            //保留小数点后一位
            function Decimal(x) {
                var f_x = parseFloat(x);
                if (isNaN(f_x)) {
                    alert('参数为非数字，无法转换！');
                    return false;
                }
                var f_x = Math.round(x * 10) / 10;

                return f_x;
            }
            var chart;
            $(document).ready(function () {
                chart = new Highcharts.Chart({
                    chart: {
                        renderTo: 'container',
                        plotBackgroundColor: null,
                        plotBorderWidth: null,
                        plotShadow: false
                    },
                    title: {
                        text: '<%=BiaoS.Title %>'
                },
                tooltip: {
                    formatter: function () {
                        return '<b>' + this.point.name + '</b>: ' + Decimal(this.percentage) + ' %';
                    }
                },
                plotOptions: {
                    pie: {
                        allowPointSelect: true,
                        cursor: 'pointer',
                        dataLabels: {
                            enabled: true,
                            color: '#000000',
                            connectorColor: '#000000',
                            formatter: function () {
                                return '<b>' + this.point.name + '</b>: ' + Decimal(this.percentage) + ' %';
                            }
                        }
                    }
                },
                series: [{
                    type: 'pie',
                    name: 'Browser share',
                    data: [<%=BiaoS.X  %>//这里决定显示的值
                    ]
                }]
            });
        });
</script>
<script src="Js/highcharts.js" type="text/javascript"></script>
<script src="Js/exporting.js" type="text/javascript" charset="gb2312"></script>
</asp:Content>
