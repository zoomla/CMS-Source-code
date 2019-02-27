<%@ Page Language="C#" MasterPageFile="~/Manage/I/Default.master" AutoEventWireup="true" CodeBehind="ProjectsAddType.aspx.cs" Inherits="ZoomLaCMS.Manage.AddOn.Project.ProjectsAddType" %>
<asp:Content ContentPlaceHolderID="head" runat="Server"><title>添加项目类型</title></asp:Content>
<asp:Content ContentPlaceHolderID="Content" runat="Server">
<table class="table table-striped table-bordered table-hover">
	<tr>
		<td align="center" colspan="2">
			<asp:Label ID="lblText" runat="server">添加项目类型</asp:Label>
		</td>
	</tr>
	<tr>
		<td style="width: 288px"><strong>项目类别名：</strong></td>
		<td>
			<ZL:TextBox ID="TxtProjectName" class="form-control text_300" AllowEmpty="false" runat="server" />
		</td>
	</tr>
	<tr>
		<td style="width: 288px">
			<strong>项目类别名称简介：</strong>
		</td>
		<td>
			<asp:TextBox ID="TxtTypeInfo" class="form-control text_300" runat="server" Height="135px" TextMode="MultiLine" />
		</td>
	</tr>
	<tr>
		<td colspan="2" class="text-center">
			<asp:Button ID="Commit_B" runat="server" Text="提交" class="btn btn-primary" OnClick="Commit_B_Click" />
			<asp:Button ID="Return_B" runat="server" Text="返回" class="btn btn-primary" OnClick="Return_B_Click" />
		</td>
	</tr>
</table>
</asp:Content>