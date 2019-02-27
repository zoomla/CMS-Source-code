<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Royalty.aspx.cs" Inherits="Manage_Copyright_Royalty" MasterPageFile="~/Manage/I/Default.master" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>版权收益</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <iframe runat="server" id="royalty_ifr" style="width:100%;" frameborder="0" marginheight="0" marginwidth="0"></iframe>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<script>
    $("#royalty_ifr").load(function () {
        var $obj = $(this);
        $obj.height($(document).height() - 120);
    });
</script>
</asp:Content>
