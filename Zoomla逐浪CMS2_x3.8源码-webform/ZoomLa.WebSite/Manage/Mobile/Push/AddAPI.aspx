<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddAPI.aspx.cs" Inherits="Manage_Mobile_Push_AddAPI" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>API设置</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<table class="table table-bordered table-striped">
<tr><td class="td_m">APP名称</td><td>
    <asp:TextBox runat="server" ID="Alias_T" CssClass="form-control text_300" MaxLength="50"></asp:TextBox>
    <asp:RequiredFieldValidator runat="server" ID="R1" ControlToValidate="Alias_T" ForeColor="Red" ErrorMessage="名称不能为空" />
</td></tr>
<tr><td>Key</td><td><asp:TextBox runat="server" ID="APPKey_T" CssClass="form-control text_500"/>
    <asp:RequiredFieldValidator runat="server" ID="R2" ControlToValidate="APPKey_T" ForeColor="Red" ErrorMessage="Key不能为空" />
</td></tr>
<tr><td>Secret</td><td><asp:TextBox runat="server" ID="APPSecret_T" CssClass="form-control text_500"/>
        <asp:RequiredFieldValidator runat="server" ID="R3" ControlToValidate="APPSecret_T" ForeColor="Red" ErrorMessage="Secret不能为空" />
</td></tr>
<tr><td></td><td><asp:Button runat="server" ID="Save_Btn" Text="保存" OnClick="Save_Btn_Click" CssClass="btn btn-primary"></asp:Button></td></tr>
</table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">

</asp:Content>