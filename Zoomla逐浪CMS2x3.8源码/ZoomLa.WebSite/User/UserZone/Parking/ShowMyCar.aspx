<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShowMyCar.aspx.cs" Inherits="User_UserZone_Parking_ShowMyCar" %>
<!DOCTYPE HTML>
<html>
<head runat="server">
<title>车位</title>
<link href="../../../App_Themes/UserThem/user_user.css" rel="stylesheet" type="text/css" />
<link rel="stylesheet"  href="../../css/subModal.css" type="text/css" rev="stylesheet" media="all" />
</head>
<body>
<form id="form1" runat="server">
<div>
	<ZL:ExGridView ID="EGV" PageSize="20" runat="server" Width="100%" DataKeyNames="Pid" ShowHeader="false" AutoGenerateColumns="False" OnRowEditing="GridView1_RowEditing">
		<Columns>
			<asp:TemplateField>
				<ItemTemplate>
				<table cellpadding="0" cellspacing="0" border="0" width="100%">
				<tr>
					<td>
						<asp:Image ID="Image1" runat="server" ImageUrl='<%#ResolveUrl(DataBinder.Eval(Container.DataItem,"CarImage").ToString()) %>' /></td>
					<td align="right">
					<table>
					<tr>
						<td rowspan="2" valign="top">
							<asp:Image ID="Image2" runat="server" ImageUrl='<%#DataBinder.Eval(Container.DataItem,"CarImageLog") %>' /></td>
						<td align="left">
							<%#DataBinder.Eval(Container.DataItem, "CarName")%>
						</td>
					</tr>
					<tr>
						<td align="left" >
							<%#GetStr(DataBinder.Eval(Container.DataItem, "P_last_uid").ToString())%><asp:LinkButton ID="LinkButton1" CommandName="Edit" runat="server"><%# DataBinder.Eval(Container.DataItem, "UserName")%></asp:LinkButton><asp:Label
								ID="Label1" runat="server" Text=""></asp:Label>
						</td>
					</tr>
					</table>
					</td>
				</tr>
				</table>
				</ItemTemplate>
			</asp:TemplateField>
		</Columns>
	</ZL:ExGridView>
</div>
</form>
</body>
</html>