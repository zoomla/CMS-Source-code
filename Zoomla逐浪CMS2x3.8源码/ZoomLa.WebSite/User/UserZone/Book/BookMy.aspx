<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BookMy.aspx.cs" Inherits="BookMy" %>
<%@ Register Src="WebUserControlMy.ascx" TagName="WebUserControlMy" TagPrefix="uc1" %>
<%@ Register Src="../WebUserControlTop.ascx" TagName="WebUserControlTop" TagPrefix="uc3" %>
<!DOCTYPE HTML>
<html>
<head>
<title>读书</title>
<link href="../../../App_Themes/UserThem/user_user.css" rel="stylesheet" type="text/css" />
</head>
<body>
<div style="margin:auto;">
<div class="us_topinfo">
<a title="会员中心" href='<%=ResolveUrl("~/User/Default.aspx") %>' target="_parent"> 会员中心</a>我的书籍
</div>
  <uc3:WebUserControlTop ID="WebUserControlTop1" runat="server"></uc3:WebUserControlTop>
  &nbsp;&nbsp;
  <uc1:WebUserControlMy id="WebUserControlMy1" runat="server"> </uc1:WebUserControlMy>
  <br />
</div>
</body>
</html>