<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LogDetail.aspx.cs" Inherits="ZoomLaCMS.Manage.Plus.LogDetail" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>日志详情</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
	<table class="table table-striped table-bordered table-hover">
        <tr>
            <td colspan="2" style="text-align:center">
                网站日志详细内容
            </td>
        </tr>
        <tr>
            <td class="text-left" style="width: 16%; height: 24px;">
                <strong>日志序号：</strong></td>
            <td class="text-left" style="word-wrap:break-word;word-break:break-all">
                <asp:Literal ID="LitLogID" runat="server"></asp:Literal></td>
        </tr>
        <tr>
            <td class="text-left" style="height: 24px">
                <strong>日志类型：</strong></td>
            <td class="text-left" style="word-wrap:break-word;word-break:break-all">
                <asp:Literal ID="LitLogCate" runat="server"></asp:Literal></td>
        </tr>
        <tr>
            <td class="text-left" style="height: 24px">
                <strong>日志优先级：</strong></td>
            <td class="text-left" style="word-wrap:break-word;word-break:break-all">
                <asp:Literal ID="LitLogPri" runat="server"></asp:Literal></td>
        </tr>
        <tr>
            <td class="text-left" style="height: 24px">
                <strong>日志记录页面：</strong></td>
            <td class="text-left" style="word-wrap:break-word;word-break:break-all">
                <asp:Literal ID="LitLogPage" runat="server"></asp:Literal></td>
        </tr>
        <tr>
            <td class="text-left" style="height: 24px">
                <strong>日志记录时间：</strong></td>
            <td class="text-left" style="word-wrap:break-word;word-break:break-all">
                <asp:Literal ID="LitLogTime" runat="server"></asp:Literal></td>
        </tr>
        <tr>
            <td class="text-left" style="height: 24px">
                <strong>操作人：</strong></td>
            <td class="text-left" style="word-wrap:break-word;word-break:break-all">
                <asp:Literal ID="LitUserName" runat="server"></asp:Literal></td>
        </tr>
        <tr>
            <td class="text-left" style="height: 24px">
                <strong>IP地址：</strong></td>
            <td class="text-left" style="word-wrap:break-word;word-break:break-all">
                <asp:Literal ID="LitUserIP" runat="server"></asp:Literal></td>
        </tr>
        <tr>
            <td class="text-left" style="height: 24px">
                <strong>用户提交信息：</strong></td>
            <td class="text-left" style="word-wrap:break-word;word-break:break-all">
                <asp:Literal ID="LitPost" runat="server"></asp:Literal></td>
        </tr>
        <tr>
            <td class="text-left" style="height: 24px">
                <strong>日志标题：</strong></td>
            <td class="text-left" style="word-wrap:break-word;word-break:break-all">
                <asp:Literal ID="LitTitle" runat="server"></asp:Literal></td>
        </tr>
        <tr>
            <td class="text-left" style="height: 24px">
                <strong>日志内容：</strong></td>
            <td class="text-left" style="word-wrap:break-word;word-break:break-all">
                <asp:Literal ID="LitMessage" runat="server"></asp:Literal></td>
        </tr>
        <tr>
            <td class="text-left" style="height: 24px">
                <strong>异常源、堆栈跟踪：</strong></td>
            <td class="text-left" style="word-wrap:break-word;word-break:break-all">
                <asp:Literal ID="LitSource" runat="server"></asp:Literal></td>
        </tr>
        <tr>
            <td colspan="2" class="text-center">
                <input id="Button1" type="button" class="btn btn-primary"  value="返 回" onclick="javascript: window.location.href = 'LogManage.aspx'" />
            </td>
        </tr>
    </table>
</asp:Content>