<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PhotoPws.aspx.cs" Inherits="User_UserZone_Pic_PhotoPws" %>
<!DOCTYPE HTML>
<html>
<head runat="server">
<title>相册密码</title>
<link href="../../../App_Themes/UserThem/user_user.css" rel="stylesheet" type="text/css" />
</head>
<body>
<form id="form1" runat="server">
<div>
    <div>相册密码：<asp:TextBox ID="PicCategPws" runat="server"></asp:TextBox></div>
    <asp:Button ID="btnOk" runat="server" Text="确定" onclick="btnOk_Click" />
    <div></div>
</div>
</form>
</body>
</html>