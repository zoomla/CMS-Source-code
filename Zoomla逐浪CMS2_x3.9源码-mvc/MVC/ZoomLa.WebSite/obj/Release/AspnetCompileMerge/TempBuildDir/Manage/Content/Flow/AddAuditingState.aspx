<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddAuditingState.aspx.cs" Inherits="ZoomLaCMS.Manage.Content.Flow.AddAuditingState" MasterPageFile="~/Manage/I/Default.master"%>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>审核状态</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-striped table-bordered table-hover">
		<tr>
			<td colspan="2" align="center" >
				添加审核状态码
			</td>
		</tr>
		<tr>
			<td class="tdleft td_l">
				<strong>录入状态码：</strong>
			</td>
			<td align="left">
				<asp:DropDownList ID="ddlStateCode" CssClass="form-control" Width="80" runat="server" DataSourceID="odsStateCode"></asp:DropDownList>
				<asp:ObjectDataSource ID="odsStateCode" runat="server" SelectMethod="GetStateCode" TypeName="ZoomLa.BLL.B_AuditingState"></asp:ObjectDataSource>
			</td>
		</tr>
		<tr>
			<td class="tdleft">
				<strong>入录状态名称：</strong>
			</td>
			<td  align="left">
				<input id="stateName" runat="server"  size="50"  class="form-control text_md"/>
				<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="stateName" ErrorMessage="状态名称不能为空"></asp:RequiredFieldValidator>
			</td>
		</tr>
		<tr><td colspan="2" align="center"><asp:Button ID="btnSave" runat="server" Text="保存状态码" onclick="btnSave_Click" class="btn btn-primary"  style="width:100px;"  /></td></tr>
</table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">

</asp:Content>
