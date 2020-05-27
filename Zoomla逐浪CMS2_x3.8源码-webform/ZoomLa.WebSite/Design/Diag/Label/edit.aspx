<%@ Page Language="C#" AutoEventWireup="true" CodeFile="edit.aspx.cs" Inherits="Design_Diag_Label_edit" MasterPageFile="~/Common/Master/Empty.master" %>
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