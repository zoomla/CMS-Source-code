<%@ Page Title="" Language="C#" MasterPageFile="~/User/Default.master" AutoEventWireup="true" CodeFile="Producerlist.aspx.cs" Inherits="manage_Shop_Producerlist" ClientIDMode="Static" ValidateRequest="false" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
<title>选择厂商</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<ol class="breadcrumb">
<li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
<li><a href="ProductList.aspx">我的店铺</a></li>
<li class="active">选择厂商</li>
</ol>
<div>
<table class="table table-striped table-bordered table-hover">
	<tr class="title">
		<td align="left">
			<b>已经选定的厂商：</b></td>
		<td align="right">
			<a href="javascript:window.close();">返回&gt;&gt;</a></td>
	</tr>
	<tr class="tdbg">
		<td align="left">
			<input type="text" id="UserNameList" size="60" readonly="readonly" class=" form-control" />
			<input type="hidden" name="HdnUserName" id="HdnUserName" value="" /></td>
		<td align="center"></td>
	</tr>
</table>
<br />
<table class="table table-striped table-bordered table-hover">
	<tr class="title">
		<td align="left">
			<b>厂商列表：</b></td>
		<td align="right">&nbsp;&nbsp;</td>
	</tr>
	<tr>
		<td valign="top" colspan="2">
			<div id="DivUserName" runat="server" visible="false">
				未找到任何厂商！
			</div>
			<asp:Repeater ID="RepUser" runat="server" OnItemDataBound="RepUser_ItemDataBound">
				<HeaderTemplate>
					<table width="100%" border="0" cellspacing="1" cellpadding="1" class="border">
						<tr>
				</HeaderTemplate>
				<ItemTemplate>
					<td align="center">
						<a href="#" onclick="<%# "add('" + DataBinder.Eval(Container,"DataItem.Producername","{0}") + "')"%>">
							<%# Eval("Producername")%>
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
	if (opener.document.getElementById('Producer').value == "") {
		opener.document.getElementById('Producer').value = obj;
		document.getElementById('UserNameList').value = opener.document.getElementById('Producer').value;
		return false;
	}
	var singleUserName = obj.split(",");
	var ignoreUserName = "";
	for (i = 0; i < singleUserName.length; i++) {
		opener.document.getElementById('Producer').value = singleUserName[i];
		document.getElementById('UserNameList').value = opener.document.getElementById('Producer').value;
	}
}
</script>
</asp:Content>
