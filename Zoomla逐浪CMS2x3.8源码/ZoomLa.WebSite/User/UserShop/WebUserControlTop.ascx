<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WebUserControlTop.ascx.cs" Inherits="User_UserShop_WebUserControlTop" %>
<style>
    .nav_header{margin-bottom:10px;margin-top:10px;}
</style>
<div class="btn-group nav_header">
<a class="btn btn-info" href="Default.aspx">店铺基本信息</a>
<a class="btn btn-info" href="ProductList.aspx">商品管理</a>
<asp:HyperLink ID="HyperLink1" class="btn btn-info" NavigateUrl="CommentsManage.aspx" runat="server">店铺评论管理</asp:HyperLink>
<a class="btn btn-info" href="StockList.aspx">库存管理</a>
<a class="btn btn-info" href="OrderList.aspx">订单管理</a>
<a class="btn btn-info" href="OrderList.aspx?ordertype=10">代购订单</a>
<a class="btn btn-info" href="SaleList.aspx">明细记录</a>
<a class="btn btn-info" href="ProductSaleList.aspx">销售记录</a>
<a class="btn btn-info" href="DeliverType.aspx">运费模块</a>
<a class="btn btn-info" href="DeliverUserManage.aspx">送货员管理</a>
</div>
<div class="opion_header">
商品管理：
<div class="btn-group">
<button type="button" class="btn btn-default dropdown-toggle" data-toggle="dropdown" aria-expanded="false">
商品管理 <span class="caret"></span>
</button>
<ul class="dropdown-menu" role="menu">
<li><a href="SelShopNode.aspx">添加商品</a></li>
<li><a href="ProductList.aspx?type=0">商品管理</a></li>
</ul>
</div>
</div>