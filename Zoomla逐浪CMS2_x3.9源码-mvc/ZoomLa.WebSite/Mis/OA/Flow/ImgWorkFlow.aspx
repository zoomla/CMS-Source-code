<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ImgWorkFlow.aspx.cs" Inherits="ZoomLaCMS.MIS.OA.Flow.ImgWorkFlow"  MasterPageFile="~/User/Empty.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>查看流程图</title>
<style>
#myDiagram{height:400px;}
</style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div id="myDiagram">
    </div>
    <asp:HiddenField ID="ImgData_Hid" runat="server" />
    <script src="/Plugins/Third/go/go.js" type="text/javascript"></script>
    <script src="/js/plugs/ZL_Diagram.js" type="text/javascript"></script>
    <script>
        $().ready(function () {
            var datas = JSON.parse($("#ImgData_Hid").val());//流程数据源
            var proname = datas[0] ? datas[0].ProcedureName : "请为该流程添加步骤!";
            ZL_Diagram.InitDiagram("myDiagram", proname, datas);
        });
    </script>
</asp:Content>
