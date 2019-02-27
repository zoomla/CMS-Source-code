<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DSAdd.aspx.cs" Inherits="Manage_Template_ExternDS_DSAdd" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>数据源管理</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<table class="table table-striped table-bordered table-hover">
    <tr>
        <td class="td_m">名称</td>
        <td>
            <ZL:TextBox ID="DSName" runat="server" CssClass="form-control text_300" AllowEmpty="false" />
        </td>
    </tr>
    <tr>
        <td>类型</td>
        <td>
            <asp:DropDownList ID="DataSource_DP" CssClass=" form-control text_300" runat="server" OnSelectedIndexChanged="DataSource_DP_SIChanged" AutoPostBack="true">
                <asp:ListItem Value="mssql">SQL数据库</asp:ListItem>
                <asp:ListItem Value="mysql">MYSQL数据库</asp:ListItem>
                <asp:ListItem Value="oracle">Oracle数据库</asp:ListItem>
                <asp:ListItem Value="access">Access数据库</asp:ListItem>
<%--                <asp:ListItem Value="xml">XML</asp:ListItem>--%>
                <asp:ListItem Value="excel">Excel</asp:ListItem>
            </asp:DropDownList></td>
        <asp:HiddenField runat="server" ID="LastIndex_Hid" Value="1" />
    </tr>
    <tr>
        <td>描述</td>
        <td>
            <asp:TextBox ID="Remind_T" CssClass="form-control text_300" runat="server" TextMode="MultiLine" style="height:120px;" />
        </td>
    </tr>
    <tr>
        <td>连接字符串 </td>
        <td>
            <asp:TextBox ID="DBConnectText" CssClass="form-control m715-50" runat="server" Text="Data Source=(local);Initial Catalog=test;User ID=test;Password=test" />
            <asp:RequiredFieldValidator ID="RF1" runat="server" ControlToValidate="DBConnectText" ErrorMessage="连接字符串不能为空!" CssClass="tips" SetFocusOnError="True" Display="Dynamic" />
        </td>
    </tr>
    <tr><td>测试结果</td><td><asp:Literal runat="server" ID="TestResult_L" EnableViewState="false"></asp:Literal></td></tr>
    <tr>
        <td>操作</td>
        <td>
            <asp:Button ID="Test_Btn" runat="server" CssClass="btn btn-primary" Text="测试连接" OnClick="Test_Btn_Click" />
            <asp:Button ID="Save_Btn" runat="server" CssClass="btn btn-primary" Text="保存连接" OnClick="Save_Btn_Click" />
            <a href="DSList.aspx" class="btn btn-default">返回列表</a>
        </td>
    </tr>
</table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">

</asp:Content>