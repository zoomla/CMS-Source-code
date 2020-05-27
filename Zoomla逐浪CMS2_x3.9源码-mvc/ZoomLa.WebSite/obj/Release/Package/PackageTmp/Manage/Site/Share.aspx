<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Share.aspx.cs" Inherits="ZoomLaCMS.Manage.Site.Share" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta charset="utf-8">
<title>管理控制台</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
   用户: <asp:TextBox runat="server" ID="TxtUserName" /><br />
   密码: <asp:TextBox runat="server" ID="TxtPassword" TextMode="Password"/><br />
        <span id="codeSpan" runat="server" visible="false">
               验证码: <asp:TextBox runat="server" ID="TxtValidateCode" MaxLength="6" style="width:60px;" />
        <img src="/Common/ValidateCode.aspx" onclick="this.src='/Common/ValidateCode.aspx?t='+Math.random()" style="cursor:pointer;" /><br />
            </span>
        <asp:Button runat="server" ID="loginBtn" Text="登录" OnClick="loginBtn_Click"/> <input type="reset" value="重置"/>
    </div>
    </form>
</body>
</html>