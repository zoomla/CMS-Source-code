<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SourceList.aspx.cs" Inherits="Edit_Contents" EnableViewStateMac="false" MasterPageFile="~/Common/Master/Empty.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>参考源</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div style="height:680px;">
    <iframe src="/NodePage.aspx?NodeID=78" id="frmlist"  scrolling="no" frameborder="0" style=" width:98%; height:98%; overflow: auto;"></iframe>
       </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
    <link href="../App_Themes/UserThem/edit.css" rel="stylesheet" type="text/css"/>
</asp:Content>



