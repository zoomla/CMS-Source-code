<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MultiDropList.aspx.cs" Inherits="ZoomLa.WebSite.Common.MultiDropList" MasterPageFile="~/Common/Master/Empty.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <link href="/App_Themes/V3.css" rel="stylesheet" />
    <title>多级选项</title>
    <asp:Literal runat="server" ID="CSS_L"></asp:Literal>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
   <div style="padding-left:5px;padding-top:5px;">
        <asp:DropDownList ID="DDLGrade1" CssClass="form-control text_200_auto" runat="server" Visible="false" AutoPostBack="true" OnSelectedIndexChanged="DDL_Grade1ChangeIndex">
    </asp:DropDownList>
    <asp:DropDownList ID="DDLGrade2" CssClass="form-control text_200_auto" runat="server" Visible="false" AutoPostBack="true" OnSelectedIndexChanged="DDL_Grade2ChangeIndex">
    </asp:DropDownList>
    <asp:DropDownList ID="DDLGrade3" CssClass="form-control text_200_auto" runat="server" Visible="false" AutoPostBack="true" OnSelectedIndexChanged="DDLGrade3_SelectedIndexChanged">
    </asp:DropDownList>
    <asp:DropDownList ID="DDLGrade4" CssClass="form-control text_200_auto" runat="server" Visible="false" AutoPostBack="true" OnSelectedIndexChanged="DDLGrade4_SelectedIndexChanged">
    </asp:DropDownList>
    <asp:DropDownList ID="DDLGrade5" CssClass="form-control text_200_auto" runat="server" Visible="false" AutoPostBack="true" OnSelectedIndexChanged="DDLGrade5_SelectedIndexChanged">
    </asp:DropDownList>
    <asp:DropDownList ID="DDLGrade6" CssClass="form-control text_200_auto" runat="server" Visible="false" AutoPostBack="true" OnSelectedIndexChanged="DDLGrade6_SelectedIndexChanged">
    </asp:DropDownList>
   </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
   
</asp:Content>