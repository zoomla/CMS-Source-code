<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UActionImgWrok.aspx.cs" Inherits="ZoomLaCMS.Manage.User.Addon.UActionImgWrok" MasterPageFile="~/Common/Common.master"%>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>行为跟踪</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div id="myDiagram" style="height:400px;">
    </div>
    <asp:HiddenField ID="code" runat="server" />
    <script src="/Plugins/Third/go/go.js" type="text/javascript"></script>
    <script src="/js/plugs/ZL_Diagram.js" type="text/javascript"></script>
    <script>
        $().ready(function () {
            console.log($("#code").val());
            var data = JSON.parse($("#code").val());
            if (data.length > 0) {
                ZL_Diagram.InitDiagram("myDiagram", data[0].Table[0].uname+"的行为跟踪",data[0].Table );
            }
        });
    </script>
</asp:Content>
