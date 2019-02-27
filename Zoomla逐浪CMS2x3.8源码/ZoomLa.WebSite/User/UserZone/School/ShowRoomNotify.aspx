<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShowRoomNotify.aspx.cs" Inherits="User_UserZone_School_ShowRoomNotify" %>
<%@ Register Src="../WebUserControlTop.ascx" TagName="WebUserControlTop" TagPrefix="uc1" %>
<!DOCTYPE HTML>
<html>
<head id="Head1" runat="server">
<title>班级列表</title>
<link href="../../../App_Themes/UserThem/user_user.css" rel="stylesheet" type="text/css" />
</head>
<body>
<form id="form1" runat="server">
<div class="us_topinfo">
<a title="会员中心" href='<%=ResolveUrl("~/User/Default.aspx") %>' target="_parent">会员中心</a> &gt;&gt; <a href="mySchoolList.aspx">我的班级 </a>&gt;&gt;班级信息
</div>
	<uc1:WebUserControlTop ID="WebUserControlTop1" runat="server"></uc1:WebUserControlTop>
	<br />
	<div class="us_showinfo">
	<div>
	<span><a href="showRoom.aspx?Roomid=<%=RoomID %>"><%=RoomName %>班级</a></span> &gt;&gt; <span><a href="RoomNotifyList.aspx?RoomID=<%=RoomID %>">黑板报列表</a></span> &gt;&gt; <span><%=Ntitle%></span>
	<hr />
	</div>
	<table cellpadding="0" cellspacing="0" border ="0" width="100%">
	<tr>
	<td id="tdTitle" runat="server" align="center" >
		
	</td>
	</tr>
	<tr>
	<td id="tdTime" runat="server" align="center">
		
	</td>
	</tr>
	<tr>
	<td id="tdContext" runat="server" >
		
	</td>
	</tr>
	
	</table>
</div>
</form>
</body>
</html>