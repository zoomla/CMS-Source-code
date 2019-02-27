<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Addcardtype.aspx.cs" Inherits="ZoomLaCMS.Manage.Shop.Addcardtype" MasterPageFile="~/Manage/I/Default.master"%>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>VIP卡管理</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div>
	<table  class="table table-striped table-bordered table-hover">
		<tr align="center">
			<td colspan="2" class="spacingtitle">
			   <strong>卡类型</strong>
			</td>
		</tr>
		<tr>
			<td style="width:100px;">
				种类名称：</td>
			<td>
			 &nbsp;   <ZL:TextBox ID="tx_typename" runat="server" AllowEmpty="false" class="form-control text_md"></ZL:TextBox>
			</td>
		</tr>
	   
		 <tr>
			<td >
				折扣率：
			</td>
			<td>
				&nbsp;  <asp:TextBox ID="tx_count" runat="server" CssClass="form-control text_md"></asp:TextBox>
                <asp:RequiredFieldValidator ID="required" runat="server" ControlToValidate="tx_count" ForeColor="red" ErrorMessage="折扣率不能为空"></asp:RequiredFieldValidator><asp:RegularExpressionValidator ID="regular" runat="server" ControlToValidate="tx_count" ValidationExpression="^(?:[1-9]\d|100)$" ForeColor="red" ErrorMessage="必须为10-100的整数"></asp:RegularExpressionValidator>
			 </td>
		</tr>
		
	   
		 <tr>
			<td >
				<strong></strong>
			</td>
			<td>  &nbsp;
				<asp:Button ID="Button1" class="btn btn btn-primary" runat="server"  Text="提交" onclick="Button1_Click" />
				<asp:HiddenField ID="HiddenField1" runat="server" />
				<asp:HiddenField ID="ID_H" runat="server" />
			</td>
		</tr>
	</table>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent"></asp:Content>

