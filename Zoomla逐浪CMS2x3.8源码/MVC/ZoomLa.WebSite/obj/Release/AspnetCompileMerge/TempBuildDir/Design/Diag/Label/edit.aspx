<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="edit.aspx.cs" Inherits="ZoomLaCMS.Design.Diag.Label.edit" MasterPageFile="~/Common/Master/Empty.master" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="head"><title>修改标签</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">

</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
    <script>
        $(function () {
            location = "LabelCall.aspx?<%=Request.QueryString%>";
        })
    </script>
</asp:Content>