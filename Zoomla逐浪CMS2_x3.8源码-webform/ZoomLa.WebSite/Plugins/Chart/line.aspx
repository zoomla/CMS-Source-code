<%@ Page Language="C#" AutoEventWireup="true" CodeFile="line.aspx.cs" Inherits="Skin_line" EnableViewStateMac="false" MasterPageFile="~/Common/Master/Empty.master"  %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title></title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
        <div id="container" style="width:<%=BiaoS.Width %>px; height:<%=BiaoS.Height %>px; margin:0;float:left"></div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
    <script type="text/javascript">
        var chart;
        $(document).ready(function () {
            InitDatas();
        });
        //初始化图表
        function InitImg(xdata, ydata) {
            chart = new Highcharts.Chart({
                chart: {
                    renderTo: 'container',
                    defaultSeriesType: 'spline'
                },
                title: {
                    text: ''
                },
                subtitle: {
                    text: '<%=BiaoS.Title %>'
            },
            xAxis: {
                categories: ydata
            },
            yAxis: {
                title: {
                    text: ''
                },
                labels: {
                    formatter: function () {
                        return this.value;
                    }
                }
            },
            tooltip: {
                crosshairs: true,
                shared: true
            },
            plotOptions: {
                spline: {
                    marker: {
                        radius: 2,
                        lineColor: '#666666',
                        lineWidth: 1
                    }
                }
            },
            series: xdata
        });
    }
    //初始化数据
    function InitDatas() {
        var hid = "<%=Request.QueryString["hid"] %>";//父框架控件id
        var datas = [];
        var datay = [];
        if (hid != "") {
            var Coordinate = $(parent.document).find("#" + hid).val().split('^');

            for (var i = 0; i < Coordinate.length; i++) {
                var ys = Coordinate[i].trim().replace(/{/, '').replace(/}/, '').split('|');
                datas.push({ name: ys[0], marker: { symbol: 'square' }, data: JSON.parse("[" + ys[1] + "]") });
                var tempdata = ys[2].split(',');
                for (var j = 0; j < tempdata.length; j++) {
                    datay.push(tempdata[j]);
                }
            }
        }
        else {
            datas = [<%=BiaoS.X %>];
            datay = [<%=BiaoS.Y %>];
        }
        InitImg(datas, datay);
    }

		</script>
<script src="Js/highcharts.js" type="text/javascript"></script>
<script src="Js/exporting.js" type="text/javascript" charset="gb2312"></script>
</asp:Content>