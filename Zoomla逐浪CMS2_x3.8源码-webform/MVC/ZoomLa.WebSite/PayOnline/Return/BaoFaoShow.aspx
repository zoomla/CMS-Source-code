<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BaoFaoShow.aspx.cs" Inherits="ZoomLaCMS.PayOnline.Return.BaoFaoShow" MasterPageFile="~/Common/Common.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>支付结果</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div class="container">
        <table class="table table-bordered table-striped table-hover">
            <tr>
                <td class="text_12" align="right" width="100" height="20">订单号：</td>
                <td class="text_12" align="left">
                    <asp:Label ID="lbOrderID" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td class="text_12" align="right" width="100" height="20">支付金额：</td>
                <td class="text_12" align="left">
                    <asp:Label ID="lbMoney" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td class="text_12" align="right" width="100" height="20">交易时间：</td>
                <td class="text_12" align="left">
                    <asp:Label ID="lbDate" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td class="text_12" align="right" width="100" height="20">交易状态：</td>
                <td class="text_12" align="left">
                    <asp:Label ID="lbFlag" runat="server"></asp:Label></td>
            </tr>
        </table>
    </div>
</asp:Content>
