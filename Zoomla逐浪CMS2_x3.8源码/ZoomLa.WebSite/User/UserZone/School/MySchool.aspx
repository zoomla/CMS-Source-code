<%@ Page Language="C#" MasterPageFile="~/User/Default.master" AutoEventWireup="true" CodeFile="MySchool.aspx.cs" Inherits="User_UserZone_School_MySchool" %>
<%@ Register Src="../WebUserControlTop.ascx" TagName="WebUserControlTop" TagPrefix="uc1" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
<title>学校列表</title>
<link href="../../../App_Themes/UserThem/user_user.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div class="us_topinfo">
<a title="会员中心" href='<%=ResolveUrl("~/User/Default.aspx") %>' target="_parent">会员中心</a> &gt;&gt; <a href="mySchoolList.aspx">我的班级 </a>&gt;&gt;班级信息
</div>
<uc1:WebUserControlTop ID="WebUserControlTop1" runat="server"></uc1:WebUserControlTop>
<br />

<div class="us_topinfo">
	<table width="100%" cellpadding="0" cellspacing="0" border="0">
		<tr>
			<td>
				<strong>&nbsp; &nbsp;我的班级</strong>
			</td>
		</tr>
		<tr>
			<td>
				<asp:DataList ID="DataList1" Width="100%" runat="server">
					<ItemTemplate>
						<table>
							<tr>
								<td>
									学校名称：<%#DataBinder.Eval(Container.DataItem, "SchoolID")%>
								</td>
								<td>
									班级名称：<%#DataBinder.Eval(Container.DataItem, "RoomName")%>
								</td>
							</tr>
							<tr>
								<td>
									班主任：<%#DataBinder.Eval(Container.DataItem, "Teacher")%>
								</td>
								<td>
									班长：<%#DataBinder.Eval(Container.DataItem, "Monitor")%>
								</td>
							</tr>
						</table>
						<hr />
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
				页次：<asp:Label ID="Nowpage" runat="server" Text=""></asp:Label>/<asp:Label ID="PageSize" runat="server" Text=""></asp:Label>页
				<asp:Label ID="pagess" runat="server" Text=""></asp:Label>个/页 转到第<asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
				</asp:DropDownList>
				页
			</td>
		</tr>
	</table>
</div>
</div>
</asp:Content>