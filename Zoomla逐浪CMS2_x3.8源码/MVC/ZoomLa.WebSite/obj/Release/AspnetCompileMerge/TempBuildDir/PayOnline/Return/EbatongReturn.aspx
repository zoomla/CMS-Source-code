<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EbatongReturn.aspx.cs" Inherits="ZoomLaCMS.PayOnline.Return.EbatongReturn" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table>
        <tr>
            <td>订单号:</td> 
            <td><%=Request.QueryString["out_trade_no"] %></td>
        </tr>
        <tr>
            <td>通知时间:</td>
            <td><%=Request.QueryString["notify_time"] %></td>
        </tr>
        <tr>
            <td>商品名称:</td>
            <td><%=Request.QueryString["subject"] %></td>
        </tr>
        <tr>
            <td>交易状态:</td>
            <td><%=Request.QueryString["trade_status"] %></td>
        </tr>
        <tr>
            <td>交易金额</td>
            <td><%=Request.QueryString["total_fee"] %></td>
        </tr>
    </table>
    </div>
    </form>
</body>
</html>
