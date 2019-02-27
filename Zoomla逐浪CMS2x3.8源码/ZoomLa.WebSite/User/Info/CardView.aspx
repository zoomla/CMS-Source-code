<%@ Page Title="" Language="C#" MasterPageFile="~/User/Default.master" AutoEventWireup="true" CodeFile="CardView.aspx.cs" Inherits="User_Info_CardView" ClientIDMode="Static" %>
<%@ Register Src="WebUserControlTop.ascx" TagName="WebUserControlTop" TagPrefix="uc1" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
<title>VIP卡信息</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<ol class="breadcrumb">
<li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
<li><a href="UserInfo.aspx">账户管理</a></li>
<li class="active">VIP卡信息</li>
</ol>
<div class="us_seta" style="margin-bottom: 10px;" id="manageinfos" runat="server">
<uc1:WebUserControlTop ID="WebUserControlTop1" runat="server" />
</div>
<div class="us_seta" id="manageinfo" runat="server">
<div style="text-align: center; vertical-align: middle;">
	<table class="table table-striped table-bordered table-hover">
		<tr>
			<td colspan="3" class="text-center">VIP卡信息</td>
		</tr>
		<tr>
			<td>卡号：</td>
			<td>
				<asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
			</td>
			<td><span style="color: red">VIP卡号</span> </td>
		</tr>
		<tr>
			<td class=" dbian">所属客户：</td>
			<td>
				<asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
			</td>
			<td><span style="color: red">该VIP卡的持卡人</span> </td>
		</tr>
		<tr>
			<td class=" dbian">初始化密码：</td>
			<td>
				<asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
			</td>
			<td><span style="color: red">创建该VIP卡时系统自动给该卡分配的密码</span> </td>
		</tr>
		<tr>
			<td class=" dbian">归属人：</td>
			<td>
				<asp:Label ID="Label4" runat="server" Text="Label"></asp:Label>
			</td>
			<td><span style="color: red">该VIP卡属于哪个代理商人</span> </td>
		</tr>
		<tr>
			<td class=" dbian">状态：</td>
			<td>
				<asp:Label ID="Label5" runat="server" Text="Label"></asp:Label>
			</td>
			<td><span style="color: red">该VIP处于什么状态</span> </td>
		</tr>
		<tr>
			<td class=" dbian">截止有效期限：</td>
			<td>
				<asp:Label ID="Label6" runat="server" Text="Label"></asp:Label>
			</td>
			<td><span style="color: red">该VIP卡过期时间</span> </td>
		</tr>
	</table>
</div>
</div>
</asp:Content>