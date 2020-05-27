<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowM.aspx.cs" Inherits="ZoomLaCMS.Plugins.ECharts.ShowM" MasterPageFile="~/Common/Master/UserEmpty.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <style type="text/css">
       .main {height: 400px;overflow: hidden;padding: 10px;margin-bottom: 10px;border: 1px solid #e3e3e3;}
    </style><title></title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div id="main" class="main"></div>
    <asp:TextBox runat="server" ID="code" TextMode="MultiLine" Style="display: none;"></asp:TextBox>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
    <script>
        $(function () {
            //setTimeout(function () {
            //    //var ecConfig = require('echarts/config');
            //    //myChart.on(ecConfig.EVENT.CLICK, function (param) { console.log("222"); });
            //    //myChart.on("click", function (param) { console.log("222"); });
            //},2000);
            refresh();
        })
        function GetCode()
        {
            $("#code").val($(parent.document).find("#<%:CodeID%>").val());
        }
        function ReLoad()
        {
            GetCode();
            refresh(true);
        }
        function GetBase64()
        {
            return myChart.getDataURL();
        }
    </script>
    <script src="/Plugins/ECharts/build/source/echarts.js"></script>
    <script src="/Plugins/ECharts/ZL_Echarts.js"></script>
</asp:Content>