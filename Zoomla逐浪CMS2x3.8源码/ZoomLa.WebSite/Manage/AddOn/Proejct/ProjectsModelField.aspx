<%@ Page Language="C#" MasterPageFile="~/Manage/I/Default.master" AutoEventWireup="true" CodeFile="ProjectsModelField.aspx.cs" Inherits="manage_AddOn_ProjectsModelField" EnableViewStateMac="false" %>
<asp:Content ContentPlaceHolderID="head" runat="Server"><title>字段列表</title></asp:Content>
<asp:Content ContentPlaceHolderID="Content" runat="Server">
<table class="table table-striped table-bordered table-hover">
	<tr style="height: 25px;">
		<td width="10%" height="20"><strong>字段名</strong></td>
		<td width="10%"><strong>字段别名</strong></td>
		<td width="10%"><strong>字段类型</strong></td>
		<td width="10%"><strong>字段级别</strong></td>
		<td width="10%"><strong>是否必填</strong></td>
		<td width="20%"><strong>排序</strong></td>
		<td width="30%"><strong>操作</strong></td>
	</tr>
	<asp:Repeater ID="RepSystemModel" runat="server">
		<ItemTemplate>
			<tr>
				<td><%#Eval("FieldName")%></td>
				<td><%#Eval("FieldAlias")%></td>
				<td><%# Eval("FieldType", "{0}")%></td>
				<td><font color="#339900">系统</font></td>
				<td><%# GetStyleTrue(Eval("IsNotNull", "{0}"))%></td>
				<td></td>
				<td></td>
			</tr>
		</ItemTemplate>
	</asp:Repeater>
	<asp:Repeater ID="RepModelField" runat="server" OnItemCommand="Repeater1_ItemCommand">
		<ItemTemplate>
			<tr>
				<td><%#Eval("ShowList", "{0}") == "False" ? "<font color=#999999>" : ""%><%#Eval("FieldName")%><%#Eval("ShowList", "{0}") == "False" ? "</font>" : ""%></td>
				<td><%#Eval("FieldAlias")%></td>
				<td><%# Eval("FieldType", "{0}")%></td>
				<td>自定义</td>
				<td><%# GetStyleTrue(Eval("IsNotNull", "{0}"))%></td>
				<td>
					<asp:LinkButton ID="LinkButton2" runat="server" CommandName="UpMove" CommandArgument='<%# Eval("FieldID") %>'>上移</asp:LinkButton>
					|
					<asp:LinkButton ID="LinkButton3" runat="server" CommandName="DownMove" CommandArgument='<%# Eval("FieldID") %>'>下移</asp:LinkButton></td>
				<td>
					<asp:LinkButton ID="LinkButton1" runat="server" CommandName="Delete" CommandArgument='<%# Eval("FieldID") %>' OnClientClick="return confirm('确定删除此字段吗?\r\n\r\n删除字段后需要重新生成静态Html代码')" CssClass="option_style"><i class="fa fa-trash-o" title="删除"></i></asp:LinkButton>
                    <a href="../Content/AddModelField.aspx?FieldID=<%# Eval("FieldID") %>&ModelType=10&ModelID=-1" class="option_style"><i class="fa fa-pencil" title="修改"></i>修改</a> 
				</td>
			</tr>
		</ItemTemplate>
	</asp:Repeater>
</table>
<div class="clearbox"></div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<style>
	td{ text-align:center;}
</style>
<script type="text/javascript" src="/js/Common.js"></script>
</asp:Content>