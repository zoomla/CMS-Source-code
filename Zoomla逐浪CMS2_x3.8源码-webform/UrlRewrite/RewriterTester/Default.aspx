<%@ Page language="c#" Codebehind="Default.aspx.cs" AutoEventWireup="false" Inherits="RewriterTester._Default" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Default</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<img src="asp.net.gif" border="0" align="right" />
			<P>
				<asp:Button Runat="server" Text="View List Products" ID="Button1"></asp:Button></P>
			<P>CategoryID:
				<asp:TextBox id="catID" runat="server" Columns="3"></asp:TextBox>&nbsp;
				<asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" ErrorMessage="You must provide a numeric category ID..."
					Display="Dynamic" ControlToValidate="catID"></asp:RequiredFieldValidator></P>
		</form>
	</body>
</HTML>
