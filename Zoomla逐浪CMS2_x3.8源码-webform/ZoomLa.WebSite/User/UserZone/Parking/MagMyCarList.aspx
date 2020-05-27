<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeFile="MagMyCarList.aspx.cs" Inherits="MagMyCarList" %>
<%@ Register Src="../WebUserControlTop.ascx" TagName="WebUserControlTop" TagPrefix="uc1" %>
<!DOCTYPE HTML>
<html>
<head id="Head1" runat="server">
<title>会员中心 >> 抢车位</title>
<link href="../../../App_Themes/UserThem/user_user.css" rel="stylesheet" type="text/css" />
</head>
<body>
<form id="form1" runat="server">
<div class="s_bright">
<!-- str -->
<div class="us_topinfo">
<a title="会员中心" href='<%=ResolveUrl("~/User/Default.aspx") %>' target="_parent">会员中心</a>&gt;&gt;抢车位
</div>
	<uc1:WebUserControlTop ID="WebUserControlTop1" runat="server"></uc1:WebUserControlTop>
	<br />
	<div class="us_topinfo">
		<ul>
			<li style="float: left"><a href="Default.aspx">[抢车位]</a> </li>
			<li><a href="MagMyCarList.aspx">[车市]</a></li>
		</ul>
		<asp:DataList ID="DataList1" Width="100%" DataKeyField="Pid" runat="server" RepeatColumns="4"
			RepeatDirection="Horizontal" OnEditCommand="DataList1_EditCommand">
			<ItemTemplate>
				<table>
					<tr>
						<td align="center">
							<asp:Image ID="Image1" runat="server" ImageUrl='<%#ResolveUrl(DataBinder.Eval(Container.DataItem,"P_car_img").ToString())%>' /></td>
					</tr>
					<tr>
						<td align="center">
							<asp:Image ID="Image2" runat="server" ImageUrl='<%#ResolveUrl(DataBinder.Eval(Container.DataItem,"P_car_img_logo").ToString())%>' /><%#DataBinder.Eval(Container.DataItem,"P_car_name")%></td>
					</tr>
					<tr>
						<td align="center">
							价格：<%#DataBinder.Eval(Container.DataItem,"P_car_money")%>元
						</td>
					</tr>
					<tr>
						<td align="center">
							<asp:Button ID="Button1" runat="server" CommandName="Edit" OnClientClick="return confirm('你确定要购买这辆车吗？');"
								Text="购 买" /></td>
					</tr>
				</table>
			</ItemTemplate>
		</asp:DataList>&nbsp;</div>
	<li style="height: 30px; text-align: center">共<asp:Label ID="Allnum" runat="server"
		Text=""></asp:Label>&nbsp;
		<asp:Label ID="Toppage" runat="server" Text=""></asp:Label>
		<asp:Label ID="Nextpage" runat="server" Text=""></asp:Label>
		<asp:Label ID="Downpage" runat="server" Text=""></asp:Label>
		<asp:Label ID="Endpage" runat="server" Text=""></asp:Label>
		页次：<asp:Label ID="Nowpage" runat="server" Text=""></asp:Label>/<asp:Label ID="PageSize"
			runat="server" Text=""></asp:Label>页
		<asp:Label ID="pagess" runat="server" Text=""></asp:Label>个/页 转到第<asp:DropDownList
			ID="DropDownList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
		</asp:DropDownList>页 </li>
</div>
</form>
</body>
</html>
