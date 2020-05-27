<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddMsg.aspx.cs" Inherits="Manage_Shop_Printer_AddMsg" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>信息详情</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<table class="table table-bordered table-striped">
  <tr><td class="td_m">发送时间</td><td><asp:Label runat="server" ID="ReqTime_L"></asp:Label></td></tr>
  <tr><td>打印机别名</td><td><asp:Label runat="server" ID="Alias_L"></asp:Label></td></tr>
  <tr><td>所属门店</td><td><asp:Label runat="server" ID="ShopName_L"></asp:Label></td></tr>
  <tr><td>返回状态</td><td><asp:Label runat="server" ID="ReqStatus_L"></asp:Label></td></tr>
  <tr><td>内容</td><td>
    <asp:TextBox runat="server" ID="Detail_T" TextMode="MultiLine" Style="height: 300px; width: 248px;" />
  </td></tr>
    <tr>
        <td></td>
        <td>
            <a href="MessageList.aspx" class="btn btn-default">返回列表</a>
        </td>
    </tr>
</table> 
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">

</asp:Content>
