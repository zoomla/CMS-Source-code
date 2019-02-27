<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MyOrderRepair.aspx.cs" MasterPageFile="~/User/Default.master" Inherits="User_Order_MyOrderRepair" %>
<%@ Register Src="~/User/ASCX/OrderTop.ascx" TagPrefix="ZL" TagName="OrderTop" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
<title>返修/退货记录</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div id="pageflag" data-nav="shop" data-ban="shop"></div> 
<div class="container margin_t5">
<ol class="breadcrumb">
	<li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
	<li class="active">返修/退货记录</li>
</ol>
    <ZL:OrderTop runat="server" />
</div>
<div class="container myorderrepair">
	<ZL:ExGridView ID="EGV" runat="server" AllowPaging="True" AllowSorting="True" 
		AutoGenerateColumns="False" OnPageIndexChanging="EGV_PageIndexChanging" 
		class="table table-striped table-bordered table-hover " PageSize="10" EmptyDataText="没有相关数据" >
		<Columns>
			<asp:BoundField DataField="ID" HeaderText="编号" ItemStyle-CssClass="td_s" />
			<asp:BoundField DataField="OrderNo" HeaderText="订单编号" />
			<asp:TemplateField HeaderText="商品名称">
				<ItemTemplate>
					<div class="proinfo_div">
						<div class="pro_img"><img src="/<%#Eval("Thumbnails") %>" /></div>
						<div class="pro_title"><%#Eval("Proname") %></div>
					</div>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="操作">
				<ItemTemplate>
					<a href="ReqRepair.aspx?id=<%#Eval("ID") %>" target="_blank">查看</a>
				</ItemTemplate>
			</asp:TemplateField>
		</Columns>
	</ZL:ExGridView>
</div>

<style>
.myorderrepair .proinfo_div div{float:left;margin-left:5px;}
.myorderrepair .proinfo_div .pro_img img{width:70px;height:80px;}
.myorderrepair .proinfo_div .pro_title{padding-top:10px;}
</style>
</asp:Content>