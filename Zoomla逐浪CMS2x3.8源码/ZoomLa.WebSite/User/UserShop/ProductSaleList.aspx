<%@ Page Title="" Language="C#" MasterPageFile="~/User/Default.master" AutoEventWireup="true" CodeFile="ProductSaleList.aspx.cs" Inherits="User_UserShop_ProductSaleList" ClientIDMode="Static" ValidateRequest="false" EnableViewStateMac="false" %>
<%@ Register Src="WebUserControlTop.ascx" TagName="WebUserControlTop" TagPrefix="uc2" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
<title>销售记录</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div id="pageflag" data-nav="shop" data-ban="store"></div> 
<div class="container margin_t5">
<ol class="breadcrumb">
<li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
<li><a href="ProductList.aspx">我的店铺</a></li>
<li class="active">销售记录</li> 
</ol>
</div>
<div class="container btn_green">
<uc2:WebUserControlTop ID="WebUserControlTop1" runat="server" />
<div class="alert alert-success"><a href="ProductSaleList.aspx">总体销售统计</a> | <a href="ProductSale.aspx">商品销售排名</a> | <a href="ProductTypeSale.aspx">商品类别销售排名</a> | <a href="UserOrder.aspx">会员订单排名</a> | <a href="UserShopOrder.aspx">会员购物排名</a></div>
<div class="us_topinfo margin_t10">
    <div>
        从
				<asp:TextBox ID="toptime" CssClass="form-control" runat="server" onclick="WdatePicker();" />
        至
				<asp:TextBox ID="endtime" runat="server" CssClass="form-control" onclick="WdatePicker();" />
        <asp:Button ID="Button1" Text="查询" CssClass="btn btn-primary" runat="server" />
    </div>
	<table class="table table-bordered table-striped table-hover">
		<tr>
			<td colspan="3" class="text-center">
				客户平均订单金额
			</td>
		</tr>
		<tr>
			<td width="33%" class="title">总订单金额</td>
			<td width="33%" class="title">总订单数</td>
			<td width="33%" class="title">客户平均订单金额</td>
		</tr>
		<tr>
			<td class="text-center">
				<asp:Label ID="Label1" runat="server" Text=""></asp:Label>
			</td>
			<td class="text-center">
				<asp:Label ID="Label2" runat="server" Text=""></asp:Label>
			</td>
			<td class="text-center">
				<asp:Label ID="Label3" runat="server" Text=""></asp:Label>
			</td>
		</tr>
	</table>
	<table class="table table-bordered table-striped table-hover">
		<tr>
			<td colspan="3" class="text-center">
				每次访问平均订单金额
			</td>
		</tr>
		<tr align="center">
			<td width="33%" class="title">总订单金额</td>
			<td width="33%" class="title">总订单数</td>
			<td width="33%" class="title">客户平均订单金额</td>
		</tr>
		<tr>
			<td class="text-center">
				<asp:Label ID="Label4" runat="server" Text=""></asp:Label>
			</td>
			<td class="text-center">
				<asp:Label ID="Label5" runat="server" Text=""></asp:Label>
			</td>
			<td class="text-center">
				<asp:Label ID="Label6" runat="server" Text=""></asp:Label>
			</td>
		</tr>
	</table>
	<table class="table table-bordered table-striped table-hover">
		<tr>
			<td colspan="3" class="text-center">
				匿名订单购买率
			</td>
		</tr>
		<tr align="center">
			<td width="33%" class="title">总订单金额</td>
			<td width="33%" class="title">总订单数</td>
			<td width="33%" class="title">客户平均订单金额</td>
		</tr>
		<tr>
			<td class="text-center">
				<asp:Label ID="Label7" runat="server" Text=""></asp:Label>
			</td>
			<td class="text-center">
				<asp:Label ID="Label8" runat="server" Text=""></asp:Label>
			</td>
			<td class="text-center">
				<asp:Label ID="Label9" runat="server" Text=""></asp:Label>
			</td>
		</tr>
	</table>
	<table class="table table-bordered table-striped table-hover">
		<tr>
			<td colspan="3" class="text-center">
				注册会员购买率
			</td>
		</tr>
		<tr align="center">
			<td width="33%" class="title">总订单金额</td>
			<td width="33%" class="title">总订单数</td>
			<td width="33%" class="title">客户平均订单金额</td>
		</tr>
		<tr class="">
			<td class="text-center">
				<asp:Label ID="Label10" runat="server" Text=""></asp:Label>
			</td>
			<td class="text-center">
				<asp:Label ID="Label11" runat="server" Text=""></asp:Label>
			</td>
			<td class="text-center">
				<asp:Label ID="Label12" runat="server" Text=""></asp:Label>
			</td>
		</tr>
	</table>
</div>
</div>
<div class="u_sign" id="u_userinfo"></div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="scriptcontent">
<script src="/JS/DatePicker/WdatePicker.js"></script>
</asp:Content>
