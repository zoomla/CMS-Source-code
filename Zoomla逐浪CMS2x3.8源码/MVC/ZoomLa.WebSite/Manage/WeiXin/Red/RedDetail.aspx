<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RedDetail.aspx.cs" Inherits="ZoomLaCMS.Manage.WeiXin.Red.RedDetail" MasterPageFile="~/Manage/I/Default.Master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>红包详情</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<table class="table table-bordered table-striped">
    <tr><td class="td_m">公众号</td><td><asp:Label runat="server" ID="Alias_L"></asp:Label></td></tr>
    <tr><td>领取码</td><asp:Label runat="server" ID="RedCode_L"></asp:Label></tr>
    <tr><td>金额</td><asp:TextBox runat="server" ID="Amount_T"></asp:TextBox></tr>
    <tr><td>状态</td><td>
        <label><input type="radio" value="1" name="zstatus_rad" />未使用</label>
        <label><input type="radio" value="99" name="zstatus_rad"/>已领取</label>
    </td></tr>
    <tr><td>领取人</td><td><asp:Label runat="server" ID="UserName_L"></asp:Label></td></tr>
    <tr><td></td><td>
        <asp:Button runat="server" ID="Save_Btn" Text="保存信息" class="btn btn-primary" OnClick="Save_Btn_Click" />
        <a href="RedPacketFlow.aspx" class="btn btn-default">返回列表</a>
                 </td></tr>
</table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent"></asp:Content>
