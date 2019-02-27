<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddTlpClass.aspx.cs" Inherits="Manage_Design_AddTlpClass" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>模板分类管理</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<table class="table table-bordered table-striped">
<tr style="display:none;"><td class="td_m">所属分类</td><td><asp:TextBox runat="server" ID="PName_T" CssClass="form-control text_300" disabled="disabled"></asp:TextBox></td></tr>
<tr><td class="td_m">名称</td><td><ZL:TextBox runat="server" ID="Name_T" CssClass="form-control text_300" AllowEmpty="false" /></td></tr>
<tr><td>备注</td><td><ZL:TextBox runat="server" ID="Remind_T" CssClass="form-control m715-50" TextMode="MultiLine" style="height:120px;"></ZL:TextBox></td></tr>
<tr><td>创建时间</td><td><asp:Label runat="server" ID="CDate_L"></asp:Label></td></tr>
<tr><td></td><td>
    <asp:Button runat="server" ID="Save_Btn" Text="保存信息" OnClick="Save_Btn_Click" CssClass="btn btn-primary" />
    <a href="TlpClass.aspx" class="btn btn-default">返回列表</a>
 </td></tr>
</table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">

</asp:Content>