<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserGroupBuy.aspx.cs" Inherits="UserGroupBuy" EnableViewStateMac="false" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title>团购支付</title>
<link href="../App_Themes/UserThem/Default.css" type="text/css" rel="stylesheet" />
<link href="../User/css/commentary.css" rel="stylesheet" type="text/css" />
<link href="../User/css/default1.css" rel="stylesheet" type="text/css" />
</head>
<body>
<form id="form1" runat="server">
<div id="Diverror" runat="server" align="center" style="margin-top:300px" visible="false">
<div id="DivHtml" runat='server'></div>
<a href="/user/GroupList.aspx?start=false">返回</a></div>
<div id="Divpay" runat="server">
<table class="border" cellspacing="1" align="center">
<tr class="title">
    <td>团购支付操作(选择支付平台)</td>
</tr>
<tr class="tdbg">
    <td style=" text-align:center;">
        <br />
        <table width="500px" cellspacing="1" cellpadding="2" style="background-color: #CCCCCC;">
            <tr class="title">
                <td colspan="2">
                    <b>团订支付</b></td>
            </tr>
            <tr class="tdbg">
                <td style="width: 30%; text-align: right;">
                    商品名称：</td>
                <td style="text-align: left;">
                   <asp:TextBox ID="txtShopname" runat="server"></asp:TextBox></td>
            </tr>
            <tr class="tdbg">
                <td style="width: 30%; text-align: right;">
                    报名人数：</td>
                <td style="text-align: left;">
                   <asp:TextBox ID="txtNum" runat="server"></asp:TextBox></td>
            </tr>
            <tr class="tdbg">
                <td style="width: 30%; text-align: right;">
                    支付平台：</td>
                <td style="text-align: left;">
                    <asp:DropDownList ID="DDLPayPlat" runat="server" Width="70px">
                    </asp:DropDownList></td>
            </tr>
            <tr class="tdbg">
                <td style="text-align: right;">
                    你支付的金额：</td>
                <td style="text-align: left;">
                    <asp:TextBox ID="TxtvMoney" Text="0" runat="server" Enabled="false"></asp:TextBox>
                    <asp:HiddenField ID="hfMoney" runat="server" />
                    </td>
            </tr>
            <tr class="tdbg">
                <td colspan="2">
                <asp:HiddenField ID="hfGID" runat="server" />
                    <asp:Button ID="BtnSubmit" runat="server" Text=" 下一步 " OnClick="BtnSubmit_Click" />
                    <input id="btnBack" type="button" runat="server" value="返回" style="width:90px" onclick="javascript:location.href='/user/GroupList.aspx?start=false'" />
                </td>
            </tr>
        </table>
        <br />
    </td>
</tr>
</table>
</div>
</form>
</body>
</html>