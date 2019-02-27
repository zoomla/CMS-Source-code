<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UPToPlat.aspx.cs" Inherits="ZoomLaCMS.Manage.User.Addon.UPToPlat" MasterPageFile="~/Manage/I/Default.Master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>能力用户</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<table class="table table-bordered">
    <tr><td>用户名：</td><td><asp:Label runat="server" ID="UserName_L"></asp:Label></td></tr>
    <tr><td>能力信息：</td><td>
        <asp:Label runat="server" ID="PlatInfo_L"></asp:Label>
                      </td></tr>
    <tr><td class="td_m">目标公司：</td><td><asp:DropDownList runat="server" ID="PlatComp_DP" DataTextField="CompName" DataValueField="ID"
         AutoPostBack="true" OnSelectedIndexChanged="PlatComp_DP_SelectedIndexChanged" class="form-control text_300"></asp:DropDownList></td></tr>
    <tr><td>目标部门：</td><td><asp:DropDownList runat="server" ID="PlatGroup_DP" DataTextField="GroupName" DataValueField="ID" class="form-control text_300"></asp:DropDownList></td></tr>
    <tr><td></td><td>
        <asp:Button runat="server" ID="Save_Btn" Text="确认选择" OnClick="Save_Btn_Click" class="btn btn-info"/>
        <asp:Button runat="server" ID="Remove_Btn" Text="从当前公司移除" OnClick="Remove_Btn_Click" OnClientClick="return confirm('确定要将该用户退出公司吗?');" class="btn btn-info" />
    </td></tr>
</table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent"></asp:Content>