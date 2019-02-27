<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DeliverList.aspx.cs" Inherits="ZoomLaCMS.Manage.Shop.DeliverList" MasterPageFile="~/Manage/I/Default.master" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>发退货明细</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-striped table-bordered table-hover">
        <tr class="tdbg text-center">
            <td width="72"><span>日期</span></td>
            <td width="35">方向</td>
            <td width="86">客户名称</td>
            <td width="68"><span>用户名</span></td>
            <td width="77">收货人姓名</td>
            <td width="102"><span>订单编号</span></td>
            <td width="96"><span>快递公司</span></td>
            <td width="50"><span>操作人</span></td>
            <td width="51"><span>经手人</span></td>
            <td width="49"><span>已签收</span></td>
            <td width="52"><span>操作</span></td>
        </tr>
    </table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
</asp:Content>
