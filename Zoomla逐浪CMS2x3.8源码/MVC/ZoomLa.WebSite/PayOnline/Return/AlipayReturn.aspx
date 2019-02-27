<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AlipayReturn.aspx.cs" Inherits="ZoomLaCMS.PayOnline.Return.AlipayReturn" EnableViewStateMac="false" %>
<!DOCTYPE HTML>
<html>
<head id="Head1" runat="server">
<meta charset="utf-8">
<title>在线支付</title>
</head>
<body>
<form runat="server">
  <table class="border" align="center" cellspacing="1" id="Table1" runat="server">
    <tr class="title">
      <td><strong>在线支付操作（支付结果)</strong></td>
    </tr>
    <tr>
      <td class="tdbg"><div class="p_center">
          <table width="500px" class="border" align="center" cellspacing="1" id="display1"   cellpadding="2" style="background-color: #CCCCCC;">
            <tr class="title">
              <td align="center"   colspan="2"><B>支付结果</B></td>
            </tr>
            <tr>
              <td align="right"> 支付宝交易号：</td>
              <td align="left"><asp:Label ID="lbTrade_no" runat="server"></asp:Label></td>
            </tr>
            <tr>
              <td  align="right">订单号：</td>
              <td  align="left"><asp:Label ID="lbOut_trade_no" runat="server"></asp:Label></td>
            </tr>
            <tr>
              <td  align="right">付款总金额：</td>
              <td  align="left"><asp:Label ID="lbTotal_fee" runat="server"></asp:Label></td>
            </tr>
            <tr>
              <td  align="right"> 商品标题：</td>
              <td  align="left"><asp:Label ID="lbSubject" runat="server"></asp:Label></td>
            </tr>
            <tr>
              <td  align="right"> 商品描述：</td>
              <td  align="left"><asp:Label ID="lbBody" runat="server"></asp:Label></td>
            </tr>
            <tr>
              <td  align="right"> 买家账号：</td>
              <td align="left"><asp:Label ID="lbBuyer_email" runat="server"></asp:Label></td>
            </tr>
            <tr>
              <td  align="right"> 收货人姓名：</td>
              <td  align="left"><asp:Label ID="LbName" runat="server"></asp:Label></td>
            </tr>
            <tr>
              <td  align="right"> 收货人地址：</td>
              <td  align="left"><asp:Label ID="LbAddress" runat="server"></asp:Label></td>
            </tr>
            <tr>
              <td  align="right"> 收货人邮编：</td>
              <td  align="left"><asp:Label ID="LbZip" runat="server"></asp:Label></td>
            </tr>
            <tr>
              <td  align="right"> 收货人电话：</td>
              <td  align="left"><asp:Label ID="LbPhone" runat="server"></asp:Label></td>
            </tr>
            <tr>
              <td  align="right"> 收货人手机：</td>
              <td  align="left"><asp:Label ID="LbMobile" runat="server"></asp:Label></td>
            </tr>
            <tr>
              <td  align="right"> 交易状态：</td>
              <td  align="left"><asp:Label ID="lbTrade_status" runat="server"></asp:Label></td>
            </tr>
            <tr>
              <td  align="right"> 验证状态：</td>
              <td  align="left"><asp:Label ID="lbVerify" runat="server"></asp:Label></td>
            </tr>
          </table>
        </div></td>
    </tr>
  </table>
    <asp:Literal runat="server" ID="remindHtml"></asp:Literal>
</form>
</body>
</html>