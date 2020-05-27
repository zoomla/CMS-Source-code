<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowFlash.aspx.cs" Inherits="ZoomLaCMS.Common.ShowFlash" MasterPageFile="~/Common/Master/Empty.master"%>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>在线浏览Swf</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div style="height:100%;">
        <embed id="Embed1"  src='<%=swfurl%>' pluginspage="http://www.macromedia.com/shockwave/download/" type="application/x-shockwave-flash" onscroll style="width:100%;height:100%;" />    
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
    <script type="text/javascript">
        $obj = $("#Embed1");
        $("#Embed1").css("height", window.screen.height * 0.9);
</script>
</asp:Content>