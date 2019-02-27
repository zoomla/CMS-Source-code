<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShowSchoolMessage.aspx.cs" Inherits="User_UserZone_School_ShowSchoolMessage" %>
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
<div>&nbsp;&nbsp;&nbsp;<a href="ShowRoom.aspx?Roomid=<%=inceptID%>"><%=RoomName %></a> &gt;&gt; 留言板<hr /></div>
	<table width="100%"  border="0" cellpadding="0" cellspacing="0">
	<tr>
		<td valign="top" style="width: 100%">
			<asp:DataList ID="DataList1" runat="server" RepeatColumns="1" RepeatDirection="Horizontal"
				Width="100%" OnItemDataBound="DataList1_ItemDataBound" DataKeyField="ID">
				<ItemTemplate>
					<table width="100%" border="0" cellpadding="0" cellspacing="0">
						<tr>
							<td align="center" rowspan="3">
								<asp:Image ID="Image1" runat="server" Height="96px" Width="96px" ImageUrl='<%# getuserpic(DataBinder.Eval(Container.DataItem, "SendID").ToString())%>' /></td>
							<td width="84%" valign="top">
							   [<%#getusername(DataBinder.Eval(Container.DataItem, "SendID").ToString())%>]  在 <%# DataBinder.Eval(Container.DataItem, "Addtime")%>  留言道:</td>
						</tr>
						<tr>
							<td valign="top" style="white-space: normal">
								&nbsp;<%# DataBinder.Eval(Container.DataItem, "Mcontent")%></td>
						</tr>
						<tr>
							<td align="center" valign="top" style="width: 152px" class="trr">&nbsp;
								</td>
							<td class="trr">
								<asp:DataList ID="DataList2" runat="server" RepeatColumns="1" RepeatDirection="Horizontal" Width="100%" DataKeyField="ID">
								<ItemTemplate>
								<table width="100%" border="0" cellpadding="0" cellspacing="0">
								<tr>
								<td class="trr">
									&nbsp;
									<asp:LinkButton ID="lbDelete" runat="server" CausesValidation="False" OnClick="lbDelete_Click">[删除]</asp:LinkButton><%#getusername(DataBinder.Eval(Container.DataItem, "SendID").ToString())%>的回复<br />
									&nbsp;&nbsp;&nbsp;&nbsp;<%# DataBinder.Eval(Container.DataItem, "Mcontent")%></td>
								</tr>
								</table>
								</ItemTemplate>
								</asp:DataList>
							</td>
						</tr>
					</table>
				</ItemTemplate>
			</asp:DataList>
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
	<tr>
		<td>
			<table width="100%" border="0" cellpadding="0" cellspacing="0">
				<tr>
					<td align="center">
						内容:</td>
					<td>
						<textarea cols="50" rows="5" id="TEXTAREA1" runat="server"></textarea>
						<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TEXTAREA1"
							ErrorMessage="不能为空"></asp:RequiredFieldValidator>&nbsp;
					</td>
				</tr>
				<tr>
					<td align="center">
					</td>
					<td>
						<asp:Button ID="savebtn" runat="server" Height="28px" Text="留  言" Width="98px" OnClick="savebtn_Click" /></td>
				</tr>
			</table>
		</td>
	</tr>
</table>
<script type="text/javascript" language="javascript" src="../Common/common.js"></script>
<script type="text/javascript" language="javascript" src="../Common/subModal.js"></script>
</div>
</form>
</body>
</html>