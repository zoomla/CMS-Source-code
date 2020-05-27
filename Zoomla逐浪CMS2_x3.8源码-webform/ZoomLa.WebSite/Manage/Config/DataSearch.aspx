<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DataSearch.aspx.cs" Inherits="manage_Config_DataSearch" EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master" %>
<%@ Register Src="~/Manage/I/ASCX/SPwd.ascx" TagPrefix="uc1" TagName="SPwd" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>全库搜索</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
        <uc1:SPwd runat="server" ID="SPwd" Visible="false" />
    <div runat="server" id="maindiv" visible="false">
        <table class="table table-striped table-bordered table-hover">
            <tr>
                <td style="width: 200px;"><strong>搜索内容：</strong></td>
                <td>
                    <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control text_md"></asp:TextBox>
                    <asp:Button ID="Button2" runat="server" Text="搜索" CssClass="btn btn-primary" OnClick="Button1_Click" />
                </td>
            </tr>
        </table>
        <div id="list" runat="server" visible="false" style="text-align: left; font-size: 15px; font-weight: bold;">
            <table class="table table-striped table-bordered table-hover">
                <tr>
                    <td style="width: 200px;"><strong>查询的结果：</strong></td>
                    <td>
                        <asp:ListBox ID="listBox1" runat="server" Height="220px" Width="200px" class="l_input" SelectionMode="Multiple" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<script type="text/javascript" src="/js/Common.js"></script>
</asp:Content>