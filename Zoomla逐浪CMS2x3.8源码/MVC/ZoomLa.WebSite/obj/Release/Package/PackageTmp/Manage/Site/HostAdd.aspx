<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HostAdd.aspx.cs" Inherits="ZoomLaCMS.Manage.Site.HostAdd" MasterPageFile="~/Manage/I/Default.Master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>主机管理</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<table class="table table-bordered">
    <tr><td class="td_m">FTP用户名</td><td><asp:TextBox runat="server" ID="Name_T" CssClass="form-control text_300" disabled="disabled"/></td></tr>
    <tr><td>FTP密码</td><td><asp:TextBox runat="server" ID="UserPwd_T" CssClass="form-control text_300" disabled="disabled"/></td></tr>
    <tr><td>创建时间</td><td><asp:TextBox runat="server" ID="CDate_T" CssClass="form-control text_300" onclick="WdatePicker({ dateFmt: 'yyyy/MM/dd HH:mm' });"/></td></tr>
    <tr><td>到期时间</td><td><asp:TextBox runat="server" ID="EndDate_T" CssClass="form-control text_300" onclick="WdatePicker({ dateFmt: 'yyyy/MM/dd' });"/></td></tr>
    <tr><td>站点名称</td><td><asp:TextBox runat="server" ID="SiteInfo_T" CssClass="form-control text_300"/></td></tr>
    <tr><td></td><td><asp:Button runat="server" ID="Save_Btn" Text="保存信息" class="btn btn-primary" OnClick="Save_Btn_Click"/></td></tr>
</table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script src="/JS/DatePicker/WdatePicker.js"></script>
</asp:Content>