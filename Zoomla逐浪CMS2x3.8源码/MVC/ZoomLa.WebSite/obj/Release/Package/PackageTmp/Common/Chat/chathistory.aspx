<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="chathistory.aspx.cs" Inherits="ZoomLaCMS.Common.Chat.chathistory" MasterPageFile="~/Common/Common.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>历史聊天记录</title>
<link type="text/css" href="chat.css" rel="stylesheet" />
<style>
* { margin:0;}
html { min-height:100%;}
body { min-height:100%; background:url(chat_bg.jpg) center no-repeat; background-size:cover;}
</style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div class="container">
<div class="chat_top">
<span class="chat_top_l"></span>
<span id="uinfo_name"><asp:Label runat="server" ID="CUName_L"></asp:Label></span>
<span class="chat_top_r" data-toggle="tooltip" data-placement="left" title="点击进入聊天"><a href="chat.aspx?uid=<%= Request.QueryString["suid"] %>"><i class="fa fa-comments"></i> 进入聊天</a></span>
</div>
<div class="chat_history">
<asp:Repeater runat="server" ID="RPT">
<HeaderTemplate>
<div class="chat_body">
</HeaderTemplate>
<ItemTemplate>
<%#GetChatContent() %>
</ItemTemplate>
<FooterTemplate>
</div>
</FooterTemplate>
</asp:Repeater>
<div style="text-align:center;"><asp:Literal ID="Page_Lit" runat="server" EnableViewState="false"></asp:Literal></div>
</div>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
<script>
$(function () {
  $('[data-toggle="tooltip"]').tooltip()
})
</script>
</asp:Content>