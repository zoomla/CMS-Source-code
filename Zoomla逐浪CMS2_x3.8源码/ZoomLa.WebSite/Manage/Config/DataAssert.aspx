<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DataAssert.aspx.cs" Inherits="manage_Config_DataAssert_"  EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master"%>
<%@ Register Src="~/Manage/I/ASCX/SPwd.ascx" TagPrefix="uc1" TagName="SPwd" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>表内容批处理</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <uc1:SPwd runat="server" ID="SPwd" Visible="false" />
    <div id="maindiv" runat="server" visible="false">
        <table class="table table-striped table-bordered table-hover">
            <tr>
                <td colspan="2" class="text-center">表内容处理</td>
            </tr>
            <tr>
                <td style="width: 200px"><strong>请选择数据库：</strong></td>
                <td>
                    <asp:DropDownList ID="DatabaseList" runat="server" Width="200px" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="TableDownList_SelectedIndexChanged" /></td>
            </tr>
            <tr>
                <td style="width: 200px;"><strong>主表：</strong></td>
                <td style="height: 22px">
                    <asp:DropDownList ID="DbTableDownList" CssClass="form-control" runat="server" Width="200px" AutoPostBack="True" OnSelectedIndexChanged="DBTableDownList_SelectedIndexChanged" /></td>
            </tr>
            <tr>
                <td><strong>输出字段：</strong></td>
                <td>
                    <asp:ListBox ID="DbFieldDownList" runat="server" Height="220px" Width="200px" CssClass="form-control" SelectionMode="Multiple" /></td>
            </tr>
            <tr>
                <td><strong>将被替换内容：</strong></td>
                <td>
                    <asp:TextBox ID="TextBox1" runat="server" Width="200px" CssClass="form-control"></asp:TextBox></td>
            </tr>
            <tr>
                <td><strong>替换后的内容：</strong></td>
                <td>
                    <asp:TextBox ID="TextBox2" runat="server" Width="200px" CssClass="form-control"></asp:TextBox></td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:Button ID="Button1" runat="server" Text="批量替换" class="btn btn-primary" OnClick="Button1_Click" />
                    <asp:Button ID="Button2" runat="server" Text="取消" class="btn btn-primary" OnClick="Button2_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<script type="text/javascript" src="/js/Common.js"></script>
</asp:Content>