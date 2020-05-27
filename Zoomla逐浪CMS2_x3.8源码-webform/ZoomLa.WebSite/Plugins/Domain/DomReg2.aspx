<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DomReg2.aspx.cs" Inherits="Plugins_Domain_DomReg2" MasterPageFile="~/Manage/Site/SiteMaster2.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>支付完成</title>
<link rel="stylesheet" href="css/style.css" type="text/css" media="all" />
<link rel="stylesheet" href="css/css.css" type="text/css" />
<script type="text/javascript" src="/JS/Common.js"></script>
<script type="text/javascript" src="site.js"></script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
 <div id="m_site"><p style="float:left;"> 站群中心 >> 支付完成</p></div>
 <div id="site_main">
		<div class="cartSteps">
	   <dl>
			<dd class="stepLeft"></dd>
			<dd class="stepTwo" style="width:33%;">1.查询域名并加入购物车<span></span></dd>
			<dd class="stepTwo" style="width:33%;">2.购物车结算<span></span></dd>
			<dd class="stepThree hover" style="width:33%;">交易完成<span></span></dd>
			<dd class="stepRight"></dd>
		</dl>
  <table style="width:540px;" border="0">
	   <tr><td>域名</td><td>年限</td><td>金额</td><td>结果</td></tr>
  <asp:Repeater runat="server" ID="finalRepeater">
	  <ItemTemplate>
		  <tr>
			  <td>
				  <%#Eval("DomName") %>
			  </td>
			  <td>
				  <%#Eval("Year") %>
			  </td>
			   <td>
				  <%#Eval("Money") %>
			  </td>
				<td>
				  <%#Eval("Result") %>
			  </td>
		  </tr>
	  </ItemTemplate>
  </asp:Repeater>
   </table>
			<div class="cart_info">
		<div class="head"><p>购物结算</p></div>
		<div class="left">
			<div><p>本次总消费：<span>￥</span><span id="allMoney"><asp:Label runat="server" ID="allMoneyL"></asp:Label></span></p>
			</div>
		</div>
	</div>
 </div>
 </div>
</asp:Content>