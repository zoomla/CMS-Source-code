<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowIfr.aspx.cs" Inherits="ZoomLaCMS.Manage.Common.ShowIfr" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <iframe runat="server" id="myifr" width="100%" height="1000px" src="<% %>"></iframe>
</asp:Content>
<asp:Content runat ="server" ContentPlaceHolderID="ScriptContent">
    <script>
    </script>
</asp:Content>