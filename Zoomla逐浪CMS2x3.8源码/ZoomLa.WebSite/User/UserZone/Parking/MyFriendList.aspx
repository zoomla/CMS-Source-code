<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MyFriendList.aspx.cs" Inherits="User_UserZone_Parking_MyFriendList" %>
<!DOCTYPE HTML>
<html>
<head runat="server">
<title>好友分组</title>
<link href="../../../App_Themes/UserThem/user_user.css" rel="stylesheet" type="text/css" />
<link rel="stylesheet" rev="stylesheet" href="../../css/subModal.css" type="text/css" media="all" /> 
</head>
<body>
<form id="form1" runat="server">
	<div>
		好友分组：<asp:DropDownList ID="DropDownList1" runat="server" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" AutoPostBack="True">
		</asp:DropDownList>
		<asp:RadioButtonList ID="RadioButtonList1" runat="server" 
		OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged"
			RepeatColumns="3" RepeatDirection="Horizontal" Width="100%" AutoPostBack="True">
		</asp:RadioButtonList>
		<li style="height: 30px; text-align: center;">共<asp:Label ID="Allnum" runat="server"
			Text=""></asp:Label>&nbsp;
			<asp:Label ID="Toppage" runat="server" Text="" />
			<asp:Label ID="Nextpage" runat="server" Text="" />
			<asp:Label ID="Downpage" runat="server" Text="" />
			<asp:Label ID="Endpage" runat="server" Text="" />
			页次：<asp:Label ID="Nowpage" runat="server" Text="" />/<asp:Label ID="PageSize" runat="server"	Text="" />页
			<asp:Label ID="pagess" runat="server" Text="" />个/页</li>
	</div>
</form>
</body>
</html>