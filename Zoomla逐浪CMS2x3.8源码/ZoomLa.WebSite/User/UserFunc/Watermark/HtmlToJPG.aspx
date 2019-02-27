<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HtmlToJPG.aspx.cs" Inherits="User_UserFunc_Watermark_HtmlToJPG" MasterPageFile="~/Common/Master/Empty.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title runat="server" id="Title_T"></title>
    <link href="/App_Themes/User.css" rel="stylesheet" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div class="container text-center"><img runat="server" ID="CerPic_Img" src="/UploadFiles/User/UserFunc/ukr.jpg" CssClass="center-block" /></div>
<div class="container text-center btn_green"><asp:Button runat="server" CssClass="btn btn-primary" Text="保存图片" ID="SaveImage" OnClick="SaveImage_Click" /></div>
</asp:Content>