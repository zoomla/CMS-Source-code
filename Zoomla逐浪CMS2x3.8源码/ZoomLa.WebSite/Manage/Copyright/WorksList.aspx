<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WorksList.aspx.cs" Inherits="Manage_Copyright_WorksList" MasterPageFile="~/Manage/I/Default.master" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>我的作品</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <iframe runat="server" id="workslist_ifr" style="width:100%;" frameborder="0" marginheight="0" marginwidth="0"></iframe>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<script>
    $("#workslist_ifr").load(function () {
        var $obj = $(this);
        $obj.height($(document).height() - 120);
    });
</script>
</asp:Content>
