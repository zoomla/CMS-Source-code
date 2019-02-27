<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddRoom.aspx.cs" Inherits="User_UserZone_School_AddRoom" %>
<%@ Register Src="../WebUserControlTop.ascx" TagName="WebUserControlTop" TagPrefix="uc1" %>
<!DOCTYPE HTML>
<html>
<head id="Head1" runat="server">
<title><%=labe %></title>
<link href="../../../App_Themes/UserThem/user_user.css" rel="stylesheet" type="text/css" />
</head>
<body>
<form id="form1" runat="server">
<div class="us_topinfo"><a title="会员中心" href='<%=ResolveUrl("~/User/Default.aspx") %>' target="_parent">会员中心</a>&gt;&gt;<a href="mySchoolList.aspx"> 我的班级 </a>&gt;&gt;<%=labe %>
</div>
	<uc1:WebUserControlTop ID="WebUserControlTop1" runat="server"></uc1:WebUserControlTop>
	<br />
	<div class="us_showinfo">
	<div><a href="mySchoolList.aspx"> 我的班级 </a> &gt;&gt; <a href="SchoolList.aspx">学校列表</a>
			&gt;&gt; <a href="RoomList.aspx?schoolid=<%=SchoolID%>"><%=SchoolName%>班级列表</a> &gt;&gt; <%=labe %><hr /></div>
		<table width="100%" cellpadding="0" cellspacing="0" border="0">
			<tr>
				<td align="center" colspan="2" style="height: 30px">
					<strong>
						<%=labe %>
					</strong>
				</td>
			</tr>
			<tr>
				<td align="right" style="width: 260px">
					<strong>学校：</strong></td>
				<td align="left">
					<asp:Label ID="lblSchool" runat="server" Text=""></asp:Label></td>
			</tr>
			<tr>
				<td align="right" style="width: 260px">
				<strong>年级：</strong>
				</td>
				<td>
					<asp:DropDownList ID="ddlClass" runat="server">
					</asp:DropDownList>
					年
					<asp:TextBox ID="txtRoom" runat="server" Width="30px"></asp:TextBox>
					班</td>
			</tr>
			<tr>
				<td align="right" style="width: 260px">
					<strong>班级：</strong>
				</td>
				<td>
					<asp:TextBox ID="txtRoomName" runat="server"></asp:TextBox>
					<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtRoomName"
						ErrorMessage="请输入班级名称"></asp:RequiredFieldValidator></td>
			</tr>
			<tr>
				<td align="right" style="width: 260px">
					<strong>班主任：</strong>
				</td>
				<td>
					<asp:TextBox ID="txtTeacherName" runat="server"></asp:TextBox>
					<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtTeacherName"
						ErrorMessage="请输入班主任姓名"></asp:RequiredFieldValidator></td>
			</tr>
			<tr>
				<td align="right" style="width: 260px">
					<strong>班长：</strong>
				</td>
				<td>
					<asp:TextBox ID="txtMonitorName" runat="server"></asp:TextBox>
					<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtMonitorName"
						ErrorMessage="请输入班长姓名"></asp:RequiredFieldValidator></td>
			</tr>
			<tr>
				<td align="right" style="width: 260px">
					<strong>入学时间：</strong></td>
				<td>
					<asp:DropDownList ID="ddlYear" runat="server">
					</asp:DropDownList>&nbsp;</td>
			</tr>
			
			<tr>
				<td align="right">
					<strong>创建人的身份：</strong></td>
				<td>
					<asp:RadioButtonList ID="rdlStatusType" runat="server" RepeatDirection="Horizontal">
						<asp:ListItem Value="1" Selected="true">学生</asp:ListItem>
						<asp:ListItem Value="2">老师</asp:ListItem>
						<asp:ListItem Value="3">家长</asp:ListItem>
					</asp:RadioButtonList></td>
			</tr><tr>
				<td align="right" style="width: 260px">
					<strong>班级介绍：</strong>
				</td>
				<td>
					<asp:TextBox ID="txtClassInfo" runat="server" Rows="4" TextMode="MultiLine" Width="260px"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td align="right" style="width: 260px">
				</td>
				<td>
					&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp;
					&nbsp;
					<asp:Button ID="Button1" runat="server" Text="提  交" OnClick="Button1_Click" /></td>
			</tr>
		</table>
	</div>
</form>
</body>
</html>