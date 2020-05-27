<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddBorrow.aspx.cs" Inherits="MIS_OA_Doc_AddBorrow" MasterPageFile="~/MIS/OA.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>借阅登记</title>
<link href="/JS/DatePicker/datepicker.css" rel="stylesheet" />
<script src="/JS/DatePicker/WdatePicker.js"></script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <ol id="BreadNav" class="breadcrumb navbar-fixed-top">
        <li><a href="/MIS/OA/Main.aspx">办公管理</a></li>
        <li><a href="Borrow.aspx">归档公文</a></li>
        <li class="active">文档借阅</li>
    </ol>
    <div style="height: 40px;"></div>
    <table class="table table-bordered">
        <tr>
            <td>借阅文件：</td>
            <td>
                <asp:TextBox runat="server" CssClass="form-control" ID="ids_T" TextMode="MultiLine" Style="height: 60px;"></asp:TextBox>
                <asp:HiddenField runat="server" ID="ids_Hid" />
            </td>
        </tr>
        <tr>
            <td>借阅人：</td>
            <td>
                <asp:TextBox runat="server" ID="UserIDS_T" CssClass="form-control" TextMode="MultiLine" Style="height: 60px;" />
                <asp:HiddenField ID="UserIDS_Hid" runat="server" />
                <input type="button" class="btn btn-info" value="选择用户" onclick="ShowUser();" />
            </td>
        </tr>
        <tr>
            <td>到期时间：</td>
            <td>
                <asp:TextBox runat="server" ID="EDate_T" CssClass="form-control text_300" onclick="WdatePicker({ dateFmt: 'yyyy-MM-dd HH:mm:ss' })"></asp:TextBox></td>
        </tr>
        <tr>
            <td>备注：</td>
            <td>
                <asp:TextBox runat="server" ID="Remind_T" TextMode="MultiLine" CssClass="form-control" Style="height: 60px;" /></td>
        </tr>
        <tr>
            <td></td>
            <td>
                <asp:Button runat="server" ID="SaveBtn" CssClass="btn btn-primary" OnClick="SaveBtn_Click" Text="保存" /></td>
        </tr>
    </table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
    <script src="/JS/Controls/ZL_Dialog.js"></script>
    <script src="/JS/ICMS/ZL_Common.js"></script>
    <script>
        function ShowUser() {
            ShowComDiag("/Common/Dialog/SelStructure.aspx?Type=AllInfo#UserIDS", "选择用户");
        }
    </script>
</asp:Content> 
