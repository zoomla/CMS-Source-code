<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddQuickPrint.aspx.cs" Inherits="Manage_Shop_Printer_AddQuickPrint" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>模板管理</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<table class="table table-bordered table-striped">
    <tr><td class="td_m">别名</td><td>
        <asp:TextBox runat="server" ID="Alias_T" CssClass="form-control text_300" MaxLength="30" />
        <asp:RequiredFieldValidator runat="server" ID="R1" ControlToValidate="Alias_T" ForeColor="Red" ErrorMessage="别名不能为空" />
                                </td></tr>
    <tr><td>内容</td><td>
        <asp:TextBox runat="server" ID="Content_T" TextMode="MultiLine" Style="height: 300px; width: 248px;" />
        <asp:RequiredFieldValidator runat="server" ID="R2" ControlToValidate="Content_T" ForeColor="Red" ErrorMessage="内容不能为空" />
                   </td></tr>
    <tr><td>备注</td><td><asp:TextBox runat="server" ID="Remind_T" TextMode="MultiLine" CssClass="m715-50" style="height:80px;" /></td></tr>
    <tr><td></td><td>
        <asp:Button runat="server" ID="Save_Btn" Text="保存信息" OnClick="Save_Btn_Click" CssClass="btn btn-primary" />
        <a href="QuickPrint.aspx" class="btn btn-primary">返回列表</a>
                 </td></tr>
</table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">

</asp:Content>
