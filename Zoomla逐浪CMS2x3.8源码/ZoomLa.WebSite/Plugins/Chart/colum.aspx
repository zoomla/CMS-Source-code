<%@ Page Language="C#" AutoEventWireup="true" CodeFile="colum.aspx.cs" Inherits="Skin_colum" EnableViewStateMac="false" MasterPageFile="~/Common/Master/Empty.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title></title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div id="container" style="width:<%=BiaoS.Width %>px; height:<%=BiaoS.Height %>px; margin:0;float:left"></div>
 </asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
     <script type="text/javascript">
         var chart;
         $(document).ready(function () {
             chart = new Highcharts.Chart({
                 chart: {
                     renderTo: 'container',
                     defaultSeriesType: 'column'
                 },
                 title: {
                     text: ''
                 },
                 subtitle: {
                     text: '<%=BiaoS.Title %>'
		            },
		            xAxis: {
		                categories: [<%=BiaoS.Y %>
		                ]
		            },
		            yAxis: {
		                min: 0,
		                title: {
		                    text: '单位 (<%=BiaoS.unit  %>)'
		                }
		            },
		            legend: {
		                layout: 'vertical',
		                backgroundColor: '#FFFFFF',
		                align: 'left',
		                verticalAlign: 'top',
		                x: 100,
		                y: 70,
		                floating: true,
		                shadow: true
		            },
		            tooltip: {
		                formatter: function () {
		                    return '' +
					this.x + ': ' + this.y + ' <%=BiaoS.unit  %>';
		                }
		            },
		            plotOptions: {
		                column: {
		                    pointPadding: 0.2,
		                    borderWidth: 0
		                }
		            },
		            series: [<%=BiaoS.X %>]
		        });
		    });

		</script>
<script src="Js/highcharts.js" type="text/javascript"></script>
<script src="Js/exporting.js" type="text/javascript" charset="gb2312"></script>
</asp:Content>

