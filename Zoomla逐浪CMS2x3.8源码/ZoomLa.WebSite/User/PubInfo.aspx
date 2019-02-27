<%@ Page Language="C#" MasterPageFile="~/User/Default.master" AutoEventWireup="true" CodeFile="PubInfo.aspx.cs" Inherits="User_PubInfo" ClientIDMode="Static" ValidateRequest="false" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
<title>评论信息</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<ol class="breadcrumb">
<li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
<li><a href="PubManage.aspx">评论管理</a></li>
<li class="active">评论信息</li>
</ol>
<div id="ShowDiv1" runat="server" style="margin-top: 10px;">
<ZL:ExGridView runat="server" ID="EGV" AutoGenerateColumns="false" AllowPaging="true" PageSize="20" EnableTheming="False" GridLines="None" CellPadding="2" CellSpacing="1" Width="100%" CssClass="table table-striped table-bordered table-hover" EmptyDataText="当前没有评论信息!!" OnPageIndexChanging="EGV_PageIndexChanging" OnRowCommand="EGV_RowCommand">
	<Columns>
		<asp:BoundField HeaderText="ID" DataField="ID" />
		<asp:BoundField HeaderText="标题" DataField="PubTitle" />
		<asp:BoundField HeaderText="IP地址" DataField="PubIP" />
		<asp:TemplateField HeaderText="发表人">
			<ItemTemplate>
				<%#GetUserName(Eval("PubUserID","{0}")) %>
			</ItemTemplate>
		</asp:TemplateField>
		<asp:BoundField HeaderText="发表日期" DataField="PubAddTime" />
		<asp:TemplateField HeaderText="状态">
			<ItemTemplate>
				<%# GetStatus(Eval("Pubstart","{0}")) %>
			</ItemTemplate>
		</asp:TemplateField>
		<asp:TemplateField HeaderText="操作">
			<ItemTemplate>
				<asp:LinkButton ID="LinkButton4" CommandName="view" CommandArgument='<%#Eval("ID") %>' runat="server">查看详情</asp:LinkButton>
				<asp:LinkButton ID="LinkButton1" CommandName="pass" CommandArgument='<%#Eval("ID") %>' runat="server">通过审核</asp:LinkButton>
				<asp:LinkButton ID="LinkButton2" CommandName="Npass" CommandArgument='<%#Eval("ID") %>' runat="server">取消审核</asp:LinkButton>
				<asp:LinkButton ID="LinkButton5" CommandName="Edit1" CommandArgument='<%# Eval("ID") %>' runat="server">修改</asp:LinkButton>
				<asp:LinkButton ID="LinkButton3" CommandName="del2" CommandArgument='<%#Eval("ID") %>' runat="server">删除</asp:LinkButton>
			</ItemTemplate>
		</asp:TemplateField>
	</Columns>
	<PagerStyle HorizontalAlign="Center" />
	<RowStyle Height="24px" HorizontalAlign="Center" />
</ZL:ExGridView>
</div>
<div id="ShowDiv" runat="server" visible="false" style="margin-top: 10px;">
<table class="table table-striped table-bordered table-hover">
	<tr>
		<td style="width: 20%; text-align: right; padding-right: 10px;">ID</td>
		<td style="padding-left: 10px;">
			<asp:Label ID="CID" runat="server" Text=""></asp:Label>
		</td>
	</tr>
	<tr>
		<td style="width: 50%; text-align: right; padding-right: 10px;">用户名</td>
		<td style="padding-left: 10px;">
			<asp:Label ID="UserName" runat="server" Text=""></asp:Label>
		</td>
	</tr>
	<tr>
		<td style="width: 50%; text-align: right; padding-right: 10px;">标题</td>
		<td style="padding-left: 10px;">
			<asp:Label ID="Ctitle" runat="server" Text=""></asp:Label>
		</td>
	</tr>
	<tr>
		<td style="width: 50%; text-align: right; padding-right: 10px;">内容</td>
		<td style="padding-left: 10px;">
			<asp:Label ID="Content" runat="server" Text=""></asp:Label>
		</td>
	</tr>
	<tr>
		<td style="width: 50%; text-align: right; padding-right: 10px;">IP地址</td>
		<td style="padding-left: 10px;">
			<asp:Label ID="CIP" runat="server" Text=""></asp:Label>
		</td>
	</tr>
	<tr>
		<td style="width: 50%; text-align: right; padding-right: 10px;">添加时间</td>
		<td style="padding-left: 10px;">
			<asp:Label ID="AddTime" runat="server" Text=""></asp:Label>
		</td>
	</tr>
</table>
<div style="text-align: center;">
	<asp:Button ID="BackTo" CssClass="btn btn-primary" OnClick="BackTo_Click" runat="server" Text="返回" />
</div>
</div>
<div id="ShowDiv2" runat="server" visible="false">
<table class="table table-striped table-bordered table-hover">
	<tr style="height: 40px;">
		<td>标题</td>
		<td>
			<asp:TextBox ID="Title" CssClass="form-control" runat="server"></asp:TextBox></td>
	</tr>
	<tr>
		<td>内容</td>
		<td>
			<asp:TextBox ID="pubContent" CssClass="form-control" TextMode="MultiLine" Height="300" runat="server"></asp:TextBox></td>
	</tr>
	<asp:HiddenField ID="PubID" runat="server" />
</table>
<div style="text-align: center;">
	<asp:Button ID="EditBtn" CssClass="btn btn-primary" OnClick="EditBtn_Click" runat="server" Text="修改" />
</div>
</div>
</asp:Content>