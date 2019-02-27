<%@ Page Language="C#" MasterPageFile="~/User/Default.master" AutoEventWireup="true" CodeFile="SearchResult.aspx.cs" Inherits="User_SearchResult"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<title>搜索结果</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" Runat="Server">
<div id="pageflag" data-nav="home" data-ban="UserInfo"></div>
<div class="container margin_t5">
<ol class="breadcrumb">
<li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
<li class="active">搜索结果</li>
</ol>
</div>
<div class="container btn_green u_cnt">
<ZL:ExGridView runat="server" ID="EGV" AutoGenerateColumns="false" EnableTheming="False"  AllowPaging="true" OnPageIndexChanging="EGV_PageIndexChanging" PageSize="15"
	EmptyDataText="没有匹配的页面，建议更换关键词!!"  class="table table-striped table-bordered table-hover">
<Columns>
		<asp:BoundField DataField="Index" HeaderText="ID"/>
		<asp:BoundField DataField="DTitle" HeaderText="标题" HtmlEncode="false"/>
		<asp:TemplateField HeaderText="路径">
			<ItemTemplate>
				<a href="<%#Eval("Url") %>" title="点击访问"><%#Eval("Url") %></a>
			</ItemTemplate>
		</asp:TemplateField>
</Columns>
</ZL:ExGridView>
</div>
<style>
td{ text-align:center;}
</style>
</asp:Content>
