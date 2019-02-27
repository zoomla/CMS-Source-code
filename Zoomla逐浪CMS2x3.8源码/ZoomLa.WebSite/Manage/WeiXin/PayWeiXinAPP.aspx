<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PayWeiXinAPP.aspx.cs" Inherits="Manage_WeiXin_PayWeiXinAPP" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>微信APP支付配置</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<table class="table table-bordered table-striped">
    <tr><td class="td_m">APPID</td><td><ZL:TextBox runat="server" ID="APPID_T" CssClass="form-control text_300" AllowEmpty="false" /></td></tr>
    <tr><td>APPSecret</td><td><ZL:TextBox runat="server" ID="APPSecret_T" CssClass="form-control text_300" AllowEmpty="false" /></td></tr>
    <tr><td>商户号</td><td><ZL:TextBox runat="server" ID="MCHID_T" CssClass="form-control text_300" AllowEmpty="false" /></td></tr>
    <tr><td>商户Key</td><td><ZL:TextBox runat="server" ID="Key_T" CssClass="form-control text_300" AllowEmpty="false" /></td></tr>
    <tr><td></td><td><asp:Button runat="server" ID="Save_Btn" Text="保存信息" OnClick="Save_Btn_Click" CssClass="btn btn-primary" /></td></tr>
</table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">

</asp:Content>