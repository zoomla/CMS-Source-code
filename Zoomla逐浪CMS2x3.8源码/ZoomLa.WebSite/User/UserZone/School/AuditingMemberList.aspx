<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AuditingMemberList.aspx.cs" Inherits="User_UserZone_School_AuditingMemberList" %>
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
		<span><a href="showRoom.aspx?Roomid=<%=RoomID %>"><%=RoomName %></a></span> &gt;&gt; <span>申请人列表</span>
		<hr />
	</div>
	<ZL:ExGridView ID="EGV" DataKeyNames="Noteid" PageSize="20" runat="server" AutoGenerateColumns="False" Width="100%" OnSelectedIndexChanging="GridView1_SelectedIndexChanging">
		<Columns>
			<asp:BoundField DataField="UserName" HeaderText="用户名" >
				<HeaderStyle Width="20%" />
			</asp:BoundField>
			<asp:BoundField DataField="AuditingContext" HeaderText="申请理由" />
			<asp:TemplateField HeaderText="申请身份">
				<ItemTemplate>
					<asp:Label ID="Label1" runat="server" Text='<%#GetType(DataBinder.Eval(Container.DataItem, "StatusType").ToString()) %>'></asp:Label>
				</ItemTemplate>
			</asp:TemplateField>
			
			<asp:BoundField DataField="AddTime" HeaderText="申请时间" >
				<HeaderStyle Width="15%" />
			</asp:BoundField>
			<asp:CommandField SelectText="通过申请" ShowSelectButton="True" >
				<HeaderStyle Width="10%" />
			</asp:CommandField>
		</Columns>
	</ZL:ExGridView>
</div>
</form>
</body>
</html>