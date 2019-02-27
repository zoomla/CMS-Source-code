<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IPManage.aspx.cs" Inherits="IPManage" EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <meta content="text/html; charset=utf-8" http-equiv="Content-Type">
    <title>IP分类</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-striped table-bordered table-hover">
        <thead class="spacingtitle">
            <tr style="height: 30px">
                <td colspan="4" align="center">IP分类管理
                </td>
            </tr>
        </thead>
        <tr style="height: 26px" class="tdbgleft">
            <td align="center" style="width: 10%">ID</td>
            <td align="center" style="width: 25%">所属分类</td>
            <td align="center" style="width: 30%">分类名称</td>
            <td align="center" style="width: 35%">操作</td>
        </tr>
        <%=table() %>
    </table>
</asp:Content>