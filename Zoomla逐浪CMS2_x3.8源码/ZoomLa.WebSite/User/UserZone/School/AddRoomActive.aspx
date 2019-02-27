<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddRoomActive.aspx.cs" Inherits="User_UserZone_School_AddRoomActive" %>
<%@ Register Src="../WebUserControlTop.ascx" TagName="WebUserControlTop" TagPrefix="uc1" %>
<!DOCTYPE HTML>
<html>
<head id="Head1" runat="server">
<title>我的班级</title>
<link href="../../../App_Themes/UserThem/user_user.css" rel="stylesheet" type="text/css" />
<script language="javascript" type="text/javascript">
    function Check()
    {
        window.document.getElementById("txtStateDate").value
    }
</script>
<script src="/JS/DatePicker/WdatePicker.js" type="text/javascript"></script>
</head>
<body>
<form id="form1" runat="server">
<div class="us_topinfo">
<a title="会员中心" href='<%=ResolveUrl("~/User/Default.aspx") %>' target="_parent">会员中心</a>&gt;&gt;<a href="mySchoolList.aspx"> 我的班级 </a>&gt;&gt;班级信息
</div>
	<uc1:WebUserControlTop ID="WebUserControlTop1" runat="server"></uc1:WebUserControlTop>
	<br />
	<div class="us_showinfo">
		<div>
			&nbsp;&nbsp;&nbsp;<a href="ShowRoom.aspx?Roomid=<%=RoomID%>"><%=RoomName %></a>  &gt;&gt; <a href="RoomActiveList.aspx?RoomID=<%=RoomID%>"> 活动列表</a> &gt;&gt; 发起活动
			<hr />
		</div>
		<table width="100%" border="0" cellpadding="0" cellspacing="0">
		<tr>
		<td align="right">活动标题：</td>
		<td><asp:TextBox ID="txtTitle" runat="server" Width="360px"></asp:TextBox>
			<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtTitle" ErrorMessage="请输入活动标题"></asp:RequiredFieldValidator></td>
		</tr>
		<tr>
		<td align="right">活动开始时间：</td>
		<td><asp:TextBox ID="txtStateDate" runat="server" OnFocus="setday(this)" onclick="WdatePicker({ dateFmt: 'yyyy/MM/dd HH:mm:ss' });"></asp:TextBox>
			<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtStateDate" ErrorMessage="请输入活动开始时间"></asp:RequiredFieldValidator></td>
		</tr>
		<tr>
		<td align="right">活动结束时间：</td>
		<td><asp:TextBox ID="txtEndDate" runat="server" OnFocus="setday(this)" onclick="WdatePicker({ dateFmt: 'yyyy/MM/dd HH:mm:ss' });"></asp:TextBox>
			<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtEndDate"	ErrorMessage="请输入活动结束时间"></asp:RequiredFieldValidator>
			<asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtStateDate" ControlToValidate="txtEndDate" ErrorMessage="结束时间小于开始时间" Operator="GreaterThanEqual" Type="Date"></asp:CompareValidator>
		</td>
		</tr>
		<tr>
		<td valign="top" align="right">活动内容：</td>
		<td><asp:TextBox ID="txtContext" runat="server" Rows="10" TextMode="MultiLine" Width="500px"></asp:TextBox></td>
		</tr>
			<tr>
				<td align="right" valign="top">
				</td>
				<td>
				<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtContext"	ErrorMessage="请输入活动内容"></asp:RequiredFieldValidator></td>
			</tr>
		<tr>
		<td></td>
		<td>
			<asp:Button ID="Button1" runat="server" Text="提  交" OnClick="Button1_Click" /></td>
		</tr>
		</table>
	</div>
</form>
</body>
</html>