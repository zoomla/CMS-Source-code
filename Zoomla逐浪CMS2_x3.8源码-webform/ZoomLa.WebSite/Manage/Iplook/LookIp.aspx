<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LookIP.aspx.cs" Inherits="manage_Iplook_LookIp" EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master"%>
<asp:Content runat="server" ContentPlaceHolderID="head">
        <title>IP地址管理</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-striped table-bordered table-hover">
        <thead class="spacingtitle">
            <tr style="height: 30px">
                <td colspan="7" align="center">IP信息
                </td>
            </tr>
        </thead>
        <tbody class="tdbg">
            <tr style="height: 26px" class="tdbgleft">
                <td align="center" style="width: 10%">IP_ID</td>
                <td align="center" style="width: 10%">分类ID</td>
                <td align="center" style="width: 16%">省级名称</td>
                <td align="center" style="width: 16%">市级名称</td>
                <td align="center" style="width: 15%">开始IP</td>
                <td align="center" style="width: 15%">结束IP</td>
                <td align="center" style="width: 25%">操作</td>
            </tr>
            <%= page_table()%>
        </tbody>
    </table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent"></asp:Content>