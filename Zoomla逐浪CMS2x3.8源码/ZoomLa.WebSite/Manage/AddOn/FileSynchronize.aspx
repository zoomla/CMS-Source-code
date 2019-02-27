<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FileSynchronize.aspx.cs" Inherits="manage_AddOn_FileSynchronize" EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master"%>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>在线文件管理</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<table class="table table-striped table-bordered table-hover">
	<tr align="center">
		<td colspan="2" class="spacingtitle">
			<strong>
				<asp:Label ID="LblTitle" runat="server" Text="添加服务器" Font-Bold="True"></asp:Label>
			</strong>
		</td>
	</tr>
	<tr>
		<td class="tdbgleft" align="left" style="width: 40%">
			<strong>服务器名称：</strong><br />
			在此输入在前台显示的镜像服务器名，如广东下载、上海下载等。
		</td>
		<td class="tdbg" style="text-align: left; width: 60%;">
			<asp:TextBox ID="TxtServerName" runat="server" Width="290px" class="l_input"></asp:TextBox>
			<asp:RequiredFieldValidator ID="ValrServerName" runat="server" ErrorMessage="下载服务器名称不能为空" ControlToValidate="TxtServerName"></asp:RequiredFieldValidator></td>
	</tr>
</table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent"></asp:Content>