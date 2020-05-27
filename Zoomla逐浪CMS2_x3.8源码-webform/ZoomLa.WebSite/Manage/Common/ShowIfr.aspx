<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShowIfr.aspx.cs" Inherits="Manage_Common_ShowIfr" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <iframe runat="server" id="myifr" width="100%" height="1000px" src="<% %>"></iframe>
</asp:Content>
<asp:Content runat ="server" ContentPlaceHolderID="ScriptContent">
    <script>
    </script>
</asp:Content>