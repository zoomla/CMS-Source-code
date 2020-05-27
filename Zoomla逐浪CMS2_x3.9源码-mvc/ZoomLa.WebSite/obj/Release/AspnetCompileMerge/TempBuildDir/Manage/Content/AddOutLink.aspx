<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddOutLink.aspx.cs" Inherits="ZoomLaCMS.Manage.Content.AddOutLink" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>添加外部链接</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<table class="table table-striped table-bordered table-hover">
	<tr align="center">
		<td class="spacingtitle" colspan="2">
			<asp:Label ID="LblTitle" runat="server" Text="添加外部链接" />
		</td>
	</tr>
	<tr class="tdbg">
		<td style="width: 288px" class="tdbgleft">
			<strong>所属节点：</strong>
		</td>
		<td>&nbsp;<asp:Label ID="LblNodeName" runat="server" Text=""></asp:Label><asp:HiddenField ID="HdnParentId" Value="0" runat="server" />
			<asp:HiddenField ID="HdnDepth" Value="0" runat="server" />
			<asp:HiddenField ID="HdnOrderID" Value="0" runat="server" />
			<asp:HiddenField ID="HdnNodeID" Value="0" runat="server" />
		</td>
	</tr>
	<tr class="tdbg">
		<td class="tdbgleft">
			<strong>外部链接名称：</strong>
		</td>
		<td>
			<asp:TextBox ID="TxtNodeName" class="form-control pull-left" style="max-width:200px;" runat="server" />
			<asp:RequiredFieldValidator ID="ValrNodeName" CssClass="tips" runat="server" ErrorMessage="单页名称不能为空！" ControlToValidate="TxtNodeName" Display="Dynamic" SetFocusOnError="True" />
		</td>
	</tr>
	<tr class="tdbg">
		<td class="tdbgleft">
			<strong>单页标识符：</strong>
		</td>
		<td>
			<asp:TextBox ID="TxtNodeDir" class="form-control pull-left" style="max-width:200px;" runat="server" onblur="GetPYDir()" />
			<asp:RequiredFieldValidator ID="ValrNodeIdentifier" runat="server" CssClass="tips" ForeColor="Red" ErrorMessage="标识符不能为空！" ControlToValidate="TxtNodeDir" Display="Dynamic" SetFocusOnError="True" />
		</td>
	</tr>
	<tr class="tdbg" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
		<td class="tdbgleft">
			<strong>外部链接地址：</strong>
		</td>
		<td>
			<asp:TextBox ID="TxtNodeUrl" class="form-control pull-left" style="max-width:200px;" runat="server" />
			<asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="tips" ForeColor="Red" runat="server" ErrorMessage="链接地址不能为空！" ControlToValidate="TxtNodeUrl" Display="Dynamic" SetFocusOnError="True" />
		</td>
	</tr>
	<tr class="tdbg">
		<td style="width: 288px" class="tdbgleft">
			<strong>打开方式：</strong>
		</td>
		<td>
			<asp:RadioButtonList ID="RBLOpenType" runat="server" RepeatDirection="Horizontal">
				<asp:ListItem Selected="True" Value="0">原窗口打开</asp:ListItem>
				<asp:ListItem Value="1">新窗口打开</asp:ListItem>
			</asp:RadioButtonList>
		</td>
	</tr>
	<tr class="tdbg" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
		<td style="width: 288px" class="tdbgleft">
			<strong>外部链接图片地址：</strong>
		</td>
		<td>
			<asp:TextBox ID="TxtNodePicUrl" class="form-control pull-left" style="max-width:300px;" runat="server" Columns="50"></asp:TextBox>
		</td>
	</tr>
	<tr class="tdbg" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
		<td style="width: 288px" class="tdbgleft">
			<strong>外部链接提示：</strong>
		</td>
		<td>
			<asp:TextBox ID="TxtTips" class="form-control pull-left" style="max-width:300px;" runat="server" Columns="60" Height="68px" Rows="2" TextMode="MultiLine"></asp:TextBox>
		</td>
	</tr>
</table>
<table border="0" cellpadding="0" cellspacing="0" width="100%">
	<tr>
		<td align="center">&nbsp; &nbsp;
		<asp:Button ID="EBtnSubmit" class="btn btn-primary" Text="保存" OnClick="EBtnSubmit_Click" runat="server" />&nbsp; &nbsp;
		<input name="Cancel" class="btn btn-primary" type="button" id="BtnCancel" value="取消" onclick="window.location.href = 'NodeManage.aspx'" />
		</td>
	</tr>
</table>
<script type="text/javascript">
	function GetPYDir() {
		var NodeName = document.getElementById("<%= TxtNodeName.ClientID %>");
		//var checkUserNameMessage = document.getElementById("CheckUserNameMessage");                        
		if (NodeName.value != "") {
			CallTheServer(NodeName.value, "");
		}
	}
	function CallTheServer(arg, context) {
		<%= CallBackReference %>
	}
	function ReceiveServerData(result) {
		var NodeDir = document.getElementById("<%= TxtNodeDir.ClientID %>");
		NodeDir.value = result;
	}
</script>
</asp:Content>