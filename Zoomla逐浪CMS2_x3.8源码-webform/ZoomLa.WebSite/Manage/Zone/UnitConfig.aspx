<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UnitConfig.aspx.cs" Inherits="manage_Zone_UnitConfig" MasterPageFile="~/Manage/I/Default.master" EnableViewStateMac="false" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>空间信息配置</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-striped table-bordered table-hover">
        <tr>
            <td class="tdleft td_l"><strong>虚拟商品单位：</strong></td>
            <td><asp:TextBox ID="TextBox1" class="form-control" runat="server" Width="50px"></asp:TextBox>币</td>
        </tr>
        <tr>
            <td></td>
            <td><asp:Button ID="Button1" class="btn btn-primary" runat="server" Text="保存设置" OnClick="Button1_Click" /></td>
        </tr>
    </table>
</asp:Content>
