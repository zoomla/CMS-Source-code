<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductGive.aspx.cs" Inherits="FreeHome.Shop.ProductGive" %>
<!DOCTYPE HTML>
<html>
<head id="Head1" runat="server">
<title>我的空间</title>
<link href="../../../App_Themes/UserThem/user_user.css" rel="stylesheet" type="text/css" />
</head>
<body>
<form id="form1" runat="server">
<table border="0" cellpadding="0" cellspacing="0" width="100%">
	<tr>
		<td align="center" rowspan="2">
			&nbsp;<asp:Image ID="PicImage" runat="server" />
		</td>
		<td>
			&nbsp;<asp:Label ID="Namelabel" runat="server" Text="Label"></asp:Label>
		</td>
	</tr>
	<tr>
		<td style="height: 18px">
			价格：<asp:Label ID="pricelabel" runat="server" Text="Label"></asp:Label>(30天使用期限)
		</td>
	</tr>
	<tr>
		<td colspan="2">
			<table width="100%" border="0" cellspacing="0" cellpadding="0">
				<tr>
					<td colspan="2" style="height: 14px">
						&nbsp;&nbsp;<font color="#FF6600" size="4">受赠人信息</font>
					</td>
				</tr>
				<br />
				<tr>
					<td align="center">
						受赠人用户名:
					</td>
					<td>
						<asp:TextBox ID="inceptname" runat="server"></asp:TextBox>
						<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="不能为空"	ControlToValidate="inceptname"></asp:RequiredFieldValidator>
					</td>
				</tr>
				<tr>
					<td align="center">
						备注留言:
					</td>
					<td>
						<textarea name="messagetextarea" id="messagetextarea" cols="25" rows="5" runat="server"></textarea>
					</td>
				</tr>
			</table>
		</td>
	</tr>
	<tr>
		<td>
			&nbsp;
		</td>
		<td>
			&nbsp;<asp:Button ID="BuyBtn" runat="server" Text="确定赠送" OnClick="BuyBtn_Click" />
			<asp:Button ID="resetbtn" runat="server" Text="取消" OnClientClick='window.parent.hidePopWin(true);' />
		</td>
	</tr>
</table>
</form>
</body>
</html>
