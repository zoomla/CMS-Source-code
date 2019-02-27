<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="App_Client_Default" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title><%:STitle %></title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    
    <iframe src="<%=Url %>" style="width: 100%; border: none; height: 600px;"></iframe>
</asp:Content>

