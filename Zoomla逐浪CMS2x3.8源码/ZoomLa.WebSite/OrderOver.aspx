<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OrderOver.aspx.cs" Inherits="OrderOver" EnableViewStateMac="false" %>
<!DOCTYPE HTML>
<html>
<head>
<title>完成订单提交</title>
<meta http-equiv="Content-Type " content="text/html; charset=utf-8 " />
<link href="../App_Themes/UserThem/style.css" rel="stylesheet" type="text/css" />
<link href="../App_Themes/UserThem/user_user.css" rel="stylesheet" type="text/css" />
</head>
<body>
<form id="form1" runat="server">
<div id="main" class="rg_inout">
<asp:HiddenField ID="hfResult" runat="server" Value="" />
    <h1>第三步:完成订单提交&nbsp;<img alt="" src="/user/images/regl3.gif" /></h1>
    <div class="cart_lei"></div>
    <div class="rg_ok">
      <asp:Label ID="Label1" runat="server" BorderWidth="0px" ForeColor="Red"></asp:Label><br/>
      <asp:Button ID="Button1" CssClass="i_bottom" runat="server" Text="在线支付" OnClick="Button1_Click" />
      <asp:Button ID="Score_Btn" CssClass="i_bottom" runat="server" Text="积分支付" OnClick="Score_Btn_Click" Visible="false" />
      <asp:Button ID="UserPurseBTN" CssClass="i_bottom"  runat="server" Text="余额支付" OnClick="UserPurseBTN_Click" />
      <asp:Button ID="SiverCoin" CssClass="i_bottom"  runat="server" Text="银币支付" onclick="SiverCoin_Click" />
      <asp:Button ID="Button3" CssClass="i_bottom"  runat="server" Visible="false" Text="PayPal支付" OnClick="Button3_Click" />
      <asp:Button ID="Button2" CssClass="i_bottom"  runat="server" OnClientClick="location.href='/';return false;" Text="返回首页" />
    </div>
</div>
<div id="bottom">
<a href="/"><img src="<%Call.Label("{$LogoUrl/}"); %>" alt="<%:Call.SiteName %>" /></a>
<p>
<script type="text/javascript"> 
<!-- 
var year="";
mydate=new Date();
myyear=mydate.getYear();
year=(myyear > 200) ? myyear : 1900 + myyear;
document.write(year); 
--> 
</script>&copy;&nbsp;Copyright&nbsp; <%Call.Label("{$SiteName/}"); %> All rights reserved.</p>
</div>
</form>
</body>
</html>