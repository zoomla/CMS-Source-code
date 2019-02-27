<%@ Register TagPrefix="skm" Namespace="ActionlessForm" Assembly="ActionlessForm" %>
<%@ Page Trace="True" language="c#" Codebehind="ListProductsByCategory.aspx.cs" AutoEventWireup="false" Inherits="RewriterTester.ListProductsByCategory" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Display Products</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body>
		<skm:Form id="Form1" onsubmit="alert('foo');" action="test" runat="server">
			<asp:Label id="lblCategoryName" Font-Bold="True" Font-Size="18pt" Runat="server"></asp:Label>
			<asp:DataGrid id="dgProducts" runat="server" AutoGenerateColumns="False" Width="369px" Height="200px"
				BorderColor="White" BorderStyle="Ridge" CellSpacing="1" BorderWidth="2px" BackColor="White"
				CellPadding="3" GridLines="None" AllowSorting="True">
				<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#9471DE"></SelectedItemStyle>
				<ItemStyle ForeColor="Black" BackColor="#DEDFDE"></ItemStyle>
				<HeaderStyle Font-Bold="True" ForeColor="#E7E7FF" BackColor="#4A3C8C"></HeaderStyle>
				<FooterStyle ForeColor="Black" BackColor="#C6C3C6"></FooterStyle>
				<Columns>
					<asp:BoundColumn DataField="ProductName" HeaderText="Product Name" SortExpression="ProductName"></asp:BoundColumn>
					<asp:BoundColumn DataField="UnitPrice" HeaderText="Price" DataFormatString="{0:c}" SortExpression="UnitPrice DESC">
						<ItemStyle HorizontalAlign="Right"></ItemStyle>
					</asp:BoundColumn>
				</Columns>
				<PagerStyle HorizontalAlign="Right" ForeColor="Black" BackColor="#C6C3C6"></PagerStyle>
			</asp:DataGrid>
		</skm:Form>
		
	</body>
</HTML>
