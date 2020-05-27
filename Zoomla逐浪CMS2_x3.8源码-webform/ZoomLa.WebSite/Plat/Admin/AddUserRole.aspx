<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddUserRole.aspx.cs" Inherits="Plat_Admin_AddUserRole" MasterPageFile="~/Plat/Main.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>权限信息</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">

<div class="container platcontainer">
<div class="child_head"><span class="child_head_span1"></span> <span class="child_head_span2">角色管理</span></div>
    <table class="table table-bordered table-hover table-striped">
        <tr>
          <td class="menu">角色名称：</td>
          <td><asp:TextBox CssClass="form-control text_300" ID="RoleName_T" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
          <td class="menu">备注：</td>
          <td><asp:TextBox CssClass="form-control text_300" ID="RoleDesc_T" TextMode="MultiLine" runat="server" style="height:100px;"></asp:TextBox></td>
        </tr>
        <tr>
            <td></td>
          <td><asp:Button runat="server" Text="添加角色" ID="Save_Btn" CssClass="btn btn-primary" OnClick="Save_Btn_Click" /></td>
        </tr>
    </table>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<style>
.menu { width: 8%; }
</style>
</asp:Content>
