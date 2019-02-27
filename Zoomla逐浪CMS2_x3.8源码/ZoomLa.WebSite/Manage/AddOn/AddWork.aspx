<%@ Page Language="C#" MasterPageFile="~/Manage/I/Default.master" AutoEventWireup="true" CodeFile="AddWork.aspx.cs" Inherits="manage_AddOn_AddWork" EnableViewStateMac="false" %>
<asp:Content ContentPlaceHolderID="head" runat="Server"><title>添加流程</title></asp:Content>
<asp:Content ContentPlaceHolderID="Content" runat="Server">
<asp:Literal ID="Literal1" runat="server"></asp:Literal>
<table class="table table-striped table-bordered table-hover">
	<tr align="center">
		<td colspan="2" class="spacingtitle">
			<asp:Label ID="LblTitleAdd" runat="server" Text="添加流程" Font-Bold="True"></asp:Label>
			<asp:Label ID="LblTitleModify" runat="server" Text="修改流程" Font-Bold="True" Visible="false"></asp:Label>
		</td>
	</tr>
	<tr>
		<td align="right" style="width: 105px">
			<strong>流程名称：&nbsp;</strong>
		</td>
		<td>
			<asp:TextBox ID="TxtWorkName" runat="server" class="form-control text_300"></asp:TextBox>
			<asp:RequiredFieldValidator ID="ValrKeywordText" ControlToValidate="TxtWorkName" runat="server" ErrorMessage="流程名称不能为空！" Display="Dynamic"></asp:RequiredFieldValidator>
		</td>
	</tr>
	<tr>
		<td align="right" style="width: 105px">
			<strong>流程简述：&nbsp;</strong>
		</td>
		<td>
			<asp:TextBox ID="TxtWorkIntro" runat="server" TextMode="MultiLine" Rows="8" Columns="50" class="form-control text_300" Height="93px"></asp:TextBox>
			<asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="TxtWorkIntro" runat="server" ErrorMessage="项目简述不能为空！" Display="Dynamic"></asp:RequiredFieldValidator>
		</td>
	</tr>
	<tr>
		<td colspan="2" class="text-center m715-50">
			<asp:Button ID="EBtnModify" Text="修改" OnClick="EBtnModify_Click" runat="server" Visible="false" class="btn btn-primary" />
			<asp:Button ID="EBtnSubmit" Text="保存" OnClick="EBtnSubmit_Click" runat="server" class="btn btn-primary" />
			<input name="Cancel" type="button" id="Cancel" value="取消" onclick="javascript: history.go(-1)" class="btn btn-primary" />
		</td>
	</tr>
</table>
<asp:HiddenField ID="HFWid" runat="server" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<script>
	var ie = navigator.appName == "Microsoft Internet Explorer" ? true : false;
	function $(objID) {
		return document.getElementById(objID);
	}
</script>
</asp:Content>