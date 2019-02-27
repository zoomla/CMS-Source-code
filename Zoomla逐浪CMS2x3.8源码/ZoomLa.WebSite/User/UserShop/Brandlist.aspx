<%@ Page Title="" Language="C#" MasterPageFile="~/User/Default.master" AutoEventWireup="true" CodeFile="Brandlist.aspx.cs" Inherits="manage_Shop_Brandlist" ClientIDMode="Static" ValidateRequest="false" EnableViewStateMac="false" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
<title>选择品牌</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<ol class="breadcrumb">
	<li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
	<li><a href="ProductList.aspx">我的店铺</a></li>
	<li class="active">选择品牌</li>
</ol>
<div>
	<table class="table table-striped table-bordered table-hover">
		<tr>
			<td align="left">
				<b>已经选定的品牌：</b></td>
			<td align="right">
				<a href="javascript:window.close();">返回&gt;&gt;</a></td>
		</tr>
		<tr class="tdbg">
			<td align="left">
				<input type="text" id="UserNameList" size="60" maxlength="200" readonly="readonly"
					class="inputtext" />
				<input type="hidden" name="HdnUserName" id="HdnUserName" value="" runat="server" /></td>
			<td align="center"></td>
		</tr>
	</table>
	<table class="table table-striped table-bordered table-hover">
		<tr>
			<td align="left">
				<b>品牌列表：</b></td>
			<td align="right"></td>
		</tr>
		<tr>
			<td valign="top" colspan="2">
				<div id="DivUserName" runat="server" visible="false">
					未找到任何品牌！
				</div>
				<asp:Repeater ID="RepUser" runat="server" OnItemDataBound="RepUser_ItemDataBound">
					<HeaderTemplate>
						<table class="table table-striped table-bordered table-hover">
							<tr>
					</HeaderTemplate>
					<ItemTemplate>
						<td align="center">
							<a href="#" onclick="<%# "add('" + DataBinder.Eval(Container,"DataItem.Trname","{0}") + "')"%>">
								<%# Eval("Trname")%>
							</a>
						</td>
						<% 
							i++; %>
						<% if (i % 8 == 0 && i > 1)
						   {%>
					</tr><tr>
						<%} %>
					</ItemTemplate>
					<FooterTemplate>
						</tr></table>
					</FooterTemplate>
				</asp:Repeater>
			</td>
		</tr>
	</table>
	<table class="table table-striped table-bordered table-hover">
		<tr>
			<td align="center"></td>
		</tr>
	</table>
	<div id="pager1" runat="server"></div>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<script language="javascript" type="text/javascript">
	function add(obj) {
		if (obj == "") { return false; }
		if (opener.document.getElementById('Brand').value == "") {
			opener.document.getElementById('Brand').value = obj;
			document.getElementById('UserNameList').value = opener.document.getElementById('Brand').value;
			return false;
		}
		var singleUserName = obj.split(",");
		var ignoreUserName = "";
		for (i = 0; i < singleUserName.length; i++) {
			opener.document.getElementById('Brand').value = singleUserName[i];
			document.getElementById('UserNameList').value = opener.document.getElementById('Brand').value;
		}
	}
</script>
</asp:Content>
