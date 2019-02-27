<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShowProblem.aspx.cs" Inherits="User_UserZone_School_ShowProblem" %>
<%@ Register Src="../WebUserControlTop.ascx" TagName="WebUserControlTop" TagPrefix="uc1" %>
<!DOCTYPE HTML>
<html>
<head id="Head1" runat="server">
<title>我的班级</title>
<link href="../../../App_Themes/UserThem/user_user.css" rel="stylesheet" type="text/css" />
</head>
<body>
<form id="form1" runat="server">
<div class="us_topinfo">
<a title="会员中心" href='<%=ResolveUrl("~/User/Default.aspx") %>' target="_parent">会员中心</a>&gt;&gt;<a href="mySchoolList.aspx">我的班级</a>&gt;&gt;班级信息
</div>
<uc1:WebUserControlTop ID="WebUserControlTop1" runat="server"></uc1:WebUserControlTop>
<br />
<div class="us_showinfo">
<div>
		&nbsp;&nbsp;&nbsp;<a href="ShowRoom.aspx?Roomid=<%=Roomid%>"><%=RoomName %></a>
		&gt;&gt; <a href="ShowProblemList.aspx?Roomid=<%=Roomid%>">问题列表</a>&gt;&gt;问题详细<hr /></div>
	<table width="100%" cellpadding="0" cellspacing="0" border="0">
		<tr>
			<td align="center">
				<table width="90%" border="0" cellpadding="0" cellspacing="0">
					<tr>
						<td id="tdtitle" align="left" runat="server">
						</td>
						<td id="tdtimename" runat="server" align="right">
						</td>
					</tr>
					<tr>
						<td colspan="2" align="left" style="white-space: normal" id="tdcontext" runat="server">
						</td>
					</tr>
				</table>
				<hr />
			</td>
		</tr>
		<tr>
			<td align="center">
				<asp:Repeater ID="Repeater1" runat="server">
					<ItemTemplate>
						<table width="90%" border="0" cellpadding="0" cellspacing="0">
							<tr>
								<td width="84%" style="height: 31px" align="left">
									[<%#getusername(DataBinder.Eval(Container.DataItem, "UserID").ToString())%>] 回答道:</td>
							</tr>
							<tr>
								<td valign="top" style="white-space: normal" align="left">
									&nbsp;<%# DataBinder.Eval(Container.DataItem, "Result")%></td>
							</tr>
						</table>
					</ItemTemplate>
				</asp:Repeater>
			</td>
		</tr>
		<tr>
			<td align="center">
				共<asp:Label ID="Allnum" runat="server" Text=""></asp:Label>&nbsp;
				<asp:Label ID="Toppage" runat="server" Text=""></asp:Label>
				<asp:Label ID="Nextpage" runat="server" Text=""></asp:Label>
				<asp:Label ID="Downpage" runat="server" Text=""></asp:Label>
				<asp:Label ID="Endpage" runat="server" Text=""></asp:Label>
				页次：<asp:Label ID="Nowpage" runat="server" Text=""></asp:Label>/<asp:Label ID="PageSize"
					runat="server" Text=""></asp:Label>页
				<asp:Label ID="pagess" runat="server" Text=""></asp:Label>个/页 转到第<asp:DropDownList
					ID="DropDownList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
				</asp:DropDownList>页
			</td>
		</tr>
		<asp:Panel ID="Panel1" width="90%" runat="server">
			<tr>
				<td> &nbsp; &nbsp; &nbsp; &nbsp; 
					<asp:TextBox ID="txtResult" runat="server" Rows="6" TextMode="MultiLine" Width="360px"></asp:TextBox></td>
			</tr>
			<tr>
				<td>
					&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
					&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;<asp:Button ID="Button1" runat="server"  Text="回  答" OnClick="Button1_Click" /></td>
			</tr>
		</asp:Panel>
	</table>
</div>
</form>
</body>
</html>