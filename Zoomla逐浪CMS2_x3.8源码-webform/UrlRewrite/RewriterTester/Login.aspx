<%@ Page language="c#" Codebehind="Login.aspx.cs" AutoEventWireup="false" Inherits="RewriterTester.Login" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" > 

<html>
  <head>
    <title>Login</title>
    <meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" Content="C#">
    <meta name=vs_defaultClientScript content="JavaScript">
    <meta name=vs_targetSchema content="http://schemas.microsoft.com/intellisense/ie5">
  </head>
  <body>
	
    <form id="Form1" method="post" runat="server">
		Username: <asp:TextBox Runat=server ID="UserName"></asp:TextBox><br>
		Password: <asp:TextBox Runat=server ID="Password" TextMode=Password></asp:TextBox><br>
		<asp:Button Runat=server ID="btnSubmit" Text="Logon"></asp:Button>
     </form>
	
  </body>
</html>
