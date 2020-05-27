<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WorksShow.aspx.cs" Inherits="ZoomLaCMS.Manage.Copyright.WorksShow"MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-bordered table-striped">
        <tr>
            <td class="td_m">标题</td>
            <td>
                <asp:Label runat="server" ID="Title_L"></asp:Label></td>
        </tr>
        <tr>
            <td>作者</td>
            <td>
                <asp:Label runat="server" ID="Author_L"></asp:Label></td>
        </tr>
        <tr>
            <td>关键字</td>
            <td>
                <asp:Label runat="server" ID="KeyWords_L"></asp:Label></td>
        </tr>
        <tr>
            <td>类型</td>
            <td>
                <asp:Label runat="server" ID="Type_L"></asp:Label></td>
        </tr>
        <tr>
            <td>应用对应类型</td>
            <td>
                <asp:Label runat="server" ID="FromType_L"></asp:Label></td>
        </tr>
        <tr>
            <td>转载收费</td>
            <td>
                <asp:Label runat="server" ID="RepPrice_L"></asp:Label></td>
        </tr>
        <tr>
            <td>素材收费</td>
            <td>
                <asp:Label runat="server" ID="MatPrice_L"></asp:Label></td>
        </tr>
        <tr>
            <td>添加时间</td>
            <td>
                <asp:Label runat="server" ID="CreateDate_L"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>访问地址</td>
            <td>
                <asp:Label runat="server" ID="FromUrl_L"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>内容</td>
            <td>
                <asp:TextBox runat="server" ID="Content_T" TextMode="MultiLine" Style="height: 300px; width: 600px;" />
            </td>
        </tr>
        <tr>
            <td></td>
            <td>
                <a href="WorksList.aspx" class="btn btn-default">返回列表</a>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent"></asp:Content>
