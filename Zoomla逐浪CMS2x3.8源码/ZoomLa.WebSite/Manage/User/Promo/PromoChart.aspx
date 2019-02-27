<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PromoChart.aspx.cs" Inherits="Manage_User_Promo_PromoChart" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>推广列表</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
  <%--  <div class="dashed">
        <strong>年份：</strong>
        <div class="btn-group" id="years_div" data-toggle="buttons">
            <asp:Literal ID="Years_Li" EnableViewState="false" runat="server"></asp:Literal>
        </div>
    </div>
    <div class="margin_t5 dashed">
        <strong>月份：</strong>
        <div class="btn-group" data-toggle="buttons">
            <asp:Literal ID="Months_Li" EnableViewState="false" runat="server"></asp:Literal>
        </div>
    </div>--%>
    <textarea class="codetext" id="code" runat="server"></textarea>
    <textarea class="codetext" id="piecode" runat="server" visible="false"></textarea>
    <textarea class="codetext" id="barcode" runat="server"></textarea>
<%--    <textarea class="codetext" id="mapcode" runat="server"></textarea>--%>
<%--    <iframe src="/Plugins/ECharts/ZLEcharts.aspx?codeid=mapcode" scrolling="no" class="chart_ifr" style="width:100%;"></iframe>--%>
    <iframe src="/Plugins/ECharts/ZLEcharts.aspx?codeid=code" scrolling="no" class="chart_ifr" style="width:100%;"></iframe>
   <%-- <iframe src="/Plugins/ECharts/ZLEcharts.aspx?CodeID=piecode" scrolling="no" style="width:48%;height:500px;border:none;"></iframe>--%>
    <div class="panel panel-default col-lg-4 col-md-6 rankbox" style="height:400px;display:inline-block;padding:0;">
        <div class="panel-heading"><i class="fa fa-signal"></i> 推广排行榜</div>
        <div class="panel-body rankitem padding0">
            <ul class="list-unstyled">
                <asp:Repeater runat="server" ID="TopRPT">
                    <ItemTemplate>
                        <li style="padding-right:10px;"><span><%#Eval("UserName") %></span><strong class="pull-right"><%#Eval("PCount") %></strong></li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
        </div>
    </div>
    <iframe src="/Plugins/ECharts/ZLEcharts.aspx?CodeID=barcode" class="col-lg-8 col-md-6" scrolling="no" style="height:410px;border:none;display:inline-block;"></iframe>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <style type="text/css">
        .codetext {width:300px;height:500px; display:none;}
        .dashed { border-bottom: 1px dashed #ccc;padding-bottom:5px;}
        .chart_ifr {border:none;overflow:hidden;height:410px;}
    </style>
</asp:Content>