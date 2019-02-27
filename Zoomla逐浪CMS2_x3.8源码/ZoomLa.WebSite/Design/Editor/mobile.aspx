<%@ Page Language="C#" AutoEventWireup="true" CodeFile="mobile.aspx.cs" Inherits="Design_Editor_mobile" MasterPageFile="~/Common/Master/Empty.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>在线设计</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div class="mb_header_n1">

</div>
<div class="mb_carousel_n1"><a></a></div>
<div class="mb_nav_n1"></div>
<div class="mb_copy_n1"></div>
<div class="mb_footer_n1"></div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
<style type="text/css">
.mb_carousel_n1 a {
    width: 100%;height:154px; display:block;
    cursor:pointer;
    background-image:url(/test/res/nav.jpg);
    background-repeat:no-repeat;
    background-size:contain; 
}
</style>
</asp:Content>