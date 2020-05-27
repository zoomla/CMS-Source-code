<%@ Page Trace="True" language="c#" Codebehind="ListCategories.aspx.cs" AutoEventWireup="false" Inherits="RewriterTester.ListCategories" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>List Categories</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body>
		<form id="Form1" runat="server" onsubmit="alert('foo');">
			<h2>Choose a Category</h2>
			<P>
				<asp:DataGrid id="dgCategories" runat="server" BorderColor="White" BorderStyle="Ridge" CellSpacing="1"
					BorderWidth="2px" BackColor="White" CellPadding="3" GridLines="None" AutoGenerateColumns="False">
					<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#9471DE"></SelectedItemStyle>
					<ItemStyle ForeColor="Black" BackColor="#DEDFDE"></ItemStyle>
					<HeaderStyle Font-Bold="True" ForeColor="#E7E7FF" BackColor="#4A3C8C"></HeaderStyle>
					<FooterStyle ForeColor="Black" BackColor="#C6C3C6"></FooterStyle>
					<Columns>
						<asp:HyperLinkColumn DataNavigateUrlField="FriendlyURL" DataTextField="CategoryName" HeaderText="Category"></asp:HyperLinkColumn>
					</Columns>
					<PagerStyle HorizontalAlign="Right" ForeColor="Black" BackColor="#C6C3C6"></PagerStyle>
				</asp:DataGrid></P>
		</form>
	</body>
</HTML>
