<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="User_DaiGou_Default" EnableViewStateMac="false" %>
<!DOCTYPE HTML>
<html>
<head id="Head1" runat="server">
<title>国际运费估算</title>
<style type="text/css">
    .style1 {width: 305px;}
</style>
</head>
<body>
<form id="form1" runat="server">
<div>
<table width="100%" border="0" cellpadding="2" cellspacing="1" style="margin: 0 auto;">
<tr>
    <td colspan="2" class="title">代购费用估算</td>
</tr>
<tr>
    <td class="style1">1: 填写您需购买的商品总价格</td>
    <td>
        <asp:TextBox ID="txtProMoney" class="l_input" runat="server"></asp:TextBox>
        （元）<asp:RequiredFieldValidator ID="money" runat="server" ControlToValidate="txtProMoney"
            Display="Dynamic" ErrorMessage="请输入商品价格"></asp:RequiredFieldValidator>
        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtProMoney"
            Display="Dynamic" ErrorMessage="请输入有效数字" Operator="GreaterThanEqual" Type="Double"
            ValueToCompare="0"></asp:CompareValidator>
    </td>
</tr>
<tr>
    <td class="style1">2: 估算您需要购买的商品总重量(不包括包装)</td>
    <td>
        <asp:TextBox ID="txtProHeight" class="l_input" runat="server"></asp:TextBox>
        （ｇ）<asp:RequiredFieldValidator ID="money0" runat="server" ControlToValidate="txtProHeight" Display="Dynamic" ErrorMessage="请输入商品重量"></asp:RequiredFieldValidator>
        <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txtProHeight" Display="Dynamic" ErrorMessage="请输入有效数字" Operator="GreaterThanEqual" Type="Double" ValueToCompare="0"></asp:CompareValidator>
    </td>
</tr>
<tr>
    <td class="style1">3: 选择您的送货地区</td>
    <td>
        <asp:DropDownList ID="DropDownList1" runat="server">
            <asp:ListItem Text="运送区域" Value="0" />
            <asp:ListItem Text="美国" Value="1" />
            <asp:ListItem Text="加拿大" Value="2" />
            <asp:ListItem Text="法国" Value="3" />
            <asp:ListItem Text="英国" Value="4" />
            <asp:ListItem Text="荷兰" Value="5" />
            <asp:ListItem Text="爱尔兰" Value="6" />
            <asp:ListItem Text="澳大利亚" Value="7" />
            <asp:ListItem Text="新西兰" Value="8" />
            <asp:ListItem Text="德国" Value="9" />
            <asp:ListItem Text="西欧" Value="10" />
            <asp:ListItem Text="东南亚" Value="11" />
            <asp:ListItem Text="其他国家" Value="12" />
            <asp:ListItem Text="国内转送" Value="13" />
        </asp:DropDownList>
    </td>
</tr>
<tr>
    <td class="style1">4: 进行计算</td>
    <td><asp:Button ID="feeBtn" class="C_input"  style="width:110px;" runat="server" Text="费用估算" OnClick="feeBtn_Click" /></td>
</tr>
</table>
</div>
<div class="divline"></div>
<div style="display: none" id="test" runat="server">
<table width="100%" border="0" cellpadding="2" cellspacing="1" class="border" style="margin: 0 auto;">
<tr >
    <td class="title">运送方式</td>
    <td class="title">商品价格(元)</td>
    <td class="title">服务费(元)</td>
    <td class="title">运费(元)</td>
    <td class="title">总计(元)</td>
</tr>
<tr>
    <td> EMS</td>
    <td><%=proMoney %></td>
    <td><%=proser%></td>
    <td><%=proFee%></td>
    <td><%=proAllMoney%></td>
</tr>
</table>
</div>
</form>
</body>
</html>