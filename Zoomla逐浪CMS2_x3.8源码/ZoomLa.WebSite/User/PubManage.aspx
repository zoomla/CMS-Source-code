<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PubManage.aspx.cs" Inherits="User_PubManage" EnableViewStateMac="false" MasterPageFile="~/User/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>评论管理</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div id="pageflag" data-nav="group" data-ban="ChangeMP"></div>
<div class="container margin_t5">
<ol class="breadcrumb">
<li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
<li class="active">评论管理</li> 
</ol>
</div>
<div class="container" id="selallno">
<ZL:ExGridView runat="server" ID="EGV" AutoGenerateColumns="false" AllowPaging="true" PageSize="20" RowStyle-CssClass="tdbg" EnableTheming="False"  GridLines="None" CellPadding="2" CellSpacing="1"  Width="100%" CssClass="table table-striped table-bordered table-hover" EmptyDataText="当前没有评论信息!!" OnPageIndexChanging="EGV_PageIndexChanging" OnRowCommand="EGV_RowCommand" >
	<Columns>
		<asp:BoundField HeaderText="ID" DataField="Pubid" />
		<asp:BoundField HeaderText="模块名称" DataField="PubName" />
		<asp:TemplateField HeaderText="操作">
			<ItemTemplate>
				<asp:LinkButton ID="LinkButton1" CommandName="manage" CommandArgument='<%# Eval("Pubid") %>' runat="server">管理信息</asp:LinkButton>
			</ItemTemplate>
		</asp:TemplateField>
	</Columns>
	<PagerStyle HorizontalAlign="Center"/>
	<RowStyle Height="24px" HorizontalAlign="Center" />
</ZL:ExGridView>
</div>
</asp:Content>