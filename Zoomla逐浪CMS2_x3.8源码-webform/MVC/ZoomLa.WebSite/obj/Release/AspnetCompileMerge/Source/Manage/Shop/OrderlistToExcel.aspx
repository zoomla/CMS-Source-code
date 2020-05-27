<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderlistToExcel.aspx.cs" Inherits="ZoomLaCMS.Manage.Shop.OrderlistToExcel" EnableViewState="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table id="orderlist" style="border-spacing: 0; border-collapse: collapse; text-align: center;">
                <tr>
                    <th style="border: 1px solid #ddd;">ID</th>
                    <th style="border: 1px solid #ddd;">订单号</th>
                    <th style="border: 1px solid #ddd;text-align:center;">商品ID</th>
                    <th style="border: 1px solid #ddd;">商品名</th>
                    <th style="border: 1px solid #ddd;">商品数量</th>
                    <th style="border: 1px solid #ddd;">收件人</th>
                    <th style="border: 1px solid #ddd;">用户名</th>
                    <th style="border: 1px solid #ddd;">下单时间</th>
                    <th style="border: 1px solid #ddd;">地区</th>
                    <th style="border: 1px solid #ddd;">地址</th>
                    <th style="border: 1px solid #ddd;">移动电话</th>
                    <th style="border: 1px solid #ddd;">邮编</th>
                    <th style="border: 1px solid #ddd;">座机</th>
                    <th style="border: 1px solid #ddd;">邮箱</th>
                    <th style="border: 1px solid #ddd;">订单金额</th>
                    <th style="border: 1px solid #ddd;">收款金额</th>
                    <th style="border: 1px solid #ddd;">订单状态</th>
                    <th style="border: 1px solid #ddd;">付款状态</th>
                    <th style="border: 1px solid #ddd;">物流状态</th>
                </tr>
                <asp:Repeater ID="RPT" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td style="border: 1px solid #ddd;text-align:center;"><%# Eval("ID") %></td>
                            <td style="border: 1px solid #ddd;text-align:center;"><%# Eval("OrderNo") %></td>
                            <td style="border: 1px solid #ddd;text-align:center;"><%# GetComm("id") %></td>
                            <td style="border: 1px solid #ddd;"><%# GetComm("name") %></td>
                            <td style="border: 1px solid #ddd;text-align:center;"><%# GetComm("num") %></td>
                            <td style="border: 1px solid #ddd;"><%# Eval("Receiver") %></td>
                            <td style="border: 1px solid #ddd;"><%# Eval("Rename") %></td>
                            <td style="border: 1px solid #ddd;text-align:center;"><%# Eval("AddTime","{0:yyyy-MM-dd HH:mm:ss}") %></td>
                            <td style="border: 1px solid #ddd;"><%# Eval("Shengfen") %></td>
                            <td style="border: 1px solid #ddd;"><%# Eval("Jiedao") %></td>
                            <td style="border: 1px solid #ddd;"><%# Eval("MobileNum") %></td>
                            <td style="border: 1px solid #ddd;"><%# Eval("ZipCode") %></td>
                            <td style="border: 1px solid #ddd;"><%# Eval("Phone") %></td>
                            <td style="border: 1px solid #ddd;"><%# Eval("Email") %></td>
                            <td style="border: 1px solid #ddd;text-align:center;">￥<%# Eval("Ordersamount","{0:f2}") %></td>
                            <td style="border: 1px solid #ddd;text-align:center;">￥<%# Eval("Receivablesamount","{0:f2}") %></td>
                            <td style="border: 1px solid #ddd;"><%#formatzt(Eval("OrderStatus", "{0}"),"0")%></td>
                            <td style="border: 1px solid #ddd;"><%#formatzt(Eval("Paymentstatus", "{0}"),"1")%></td>
                            <td style="border: 1px solid #ddd;"><%#formatzt(Eval("StateLogistics", "{0}"),"2")%></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
    </form>
</body>
</html>
