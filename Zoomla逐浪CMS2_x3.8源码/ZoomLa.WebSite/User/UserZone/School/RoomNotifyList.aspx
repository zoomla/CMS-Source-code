<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RoomNotifyList.aspx.cs" Inherits="User_UserZone_School_RoomNotifyList" %>
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
<a title="会员中心" href='<%=ResolveUrl("~/User/Default.aspx") %>' target="_parent">会员中心</a>&gt;&gt;<a href="mySchoolList.aspx">我的班级</a>&gt;&gt;班级信息
</div>
<uc1:WebUserControlTop ID="WebUserControlTop1" runat="server"></uc1:WebUserControlTop>
<br />
<div class="us_showinfo">
<div>
<span><a href="showRoom.aspx?Roomid=<%=RoomID %>"><%=RoomName %>班级</a></span> &gt;&gt; <span>黑板报列表</span>
<hr />
</div>
<table width="100%">
<tr id="trAdd" runat ="server">
<td align="right"><a href="addNotify.aspx?Roomid=<%=RoomID %>">添加黑板报</a></td>
</tr>
	<tr>
		<td id="tdn" runat="server">
			<asp:DataList ID="DataList1" Width="100%" runat="server">
				<ItemTemplate>
					<table width="100%">
						<tr>
							<td style="white-space:nowrap">
								<a href="ShowRoomNotify.aspx?Nid=<%#DataBinder.Eval(Container.DataItem, "ID")%>">
									<%#DataBinder.Eval(Container.DataItem, "NotifyTitle")%>
								</a>
							</td>
							<td align="right">
							<%#DataBinder.Eval(Container.DataItem, "AddTime")%>
							</td>
						</tr>
					</table>
				</ItemTemplate>
			</asp:DataList>
		</td>
	</tr>
	<tr>
	<td align="center" >
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
</table>
</div>
</form>
</body>
</html>