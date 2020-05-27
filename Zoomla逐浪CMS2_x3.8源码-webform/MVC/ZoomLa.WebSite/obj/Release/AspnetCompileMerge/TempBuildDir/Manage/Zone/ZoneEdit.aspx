<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ZoneEdit.aspx.cs" Inherits="ZoomLaCMS.Manage.Zone.ZoneEdit"  MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>查看申请信息</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-striped table-bordered table-hover">
        <tr>
            <td colspan="2" align="center">
                <asp:Literal ID="LTitle" runat="server" Text="查看申请信息"></asp:Literal></td>
        </tr>
        <tr>
            <td style="width: 35%">
                <strong>申请人用户名：</strong>
            </td>
            <td>
                <asp:Label ID="namelaber" runat="server" Text="Label"></asp:Label></td>
        </tr>
        <tr>
            <td style="width: 35%">
                <strong>空间名称：</strong>
            </td>
            <td>
                <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label></td>
        </tr>
        <tr>
            <td style="width: 35%">
                <strong>空间简介：</strong>
            </td>
            <td>
                <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label></td>
        </tr>
        <tr>
            <td colspan="2" class="text-center">
                <input name="Cancel" type="button" class="btn btn-primary" id="Cancel" value="返回" onclick="javescript: history.go(-1)" />
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript" src="/JS/Common.js"></script>
</asp:Content>
