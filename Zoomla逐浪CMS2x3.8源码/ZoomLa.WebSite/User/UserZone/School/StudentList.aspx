<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StudentList.aspx.cs" Inherits="User_UserZone_School_StudentList" %>
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
<a title="会员中心" href='<%=ResolveUrl("~/User/Default.aspx") %>' target="_parent">会员中心</a>&gt;&gt;<span><a href="mySchoolList.aspx">我的班级</a>&gt;&gt;班级信息
</div>
<uc1:WebUserControlTop ID="WebUserControlTop1" runat="server"></uc1:WebUserControlTop>
<br />
<div class="us_showinfo">
<div>
	&nbsp;&nbsp;&nbsp;<a href="ShowRoom.aspx?Roomid=<%=RoomID%>"><%=RoomName %>班级</a>
	&gt;&gt; 提升班干部<hr /></div>
	<table width="100%" cellpadding="0" cellspacing="0" border ="0">
	<tr>
	<td>
		<ZL:ExGridView ID="EGV" DataKeyNames="Noteid" runat="server" Width="100%" AutoGenerateColumns="False" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" >
			<Columns>
				<asp:TemplateField HeaderText="用户名">
					<ItemTemplate>
						<asp:Label ID="Label3" runat="server" Text='<%# Bind("UserName") %>'></asp:Label>
					</ItemTemplate>
					<HeaderStyle Width="150px" />
				</asp:TemplateField>
				<asp:TemplateField HeaderText="真实姓名">
					<ItemTemplate>
						<asp:Label ID="Label2" runat="server" Text='<%#GetName(DataBinder.Eval(Container.DataItem, "UserID").ToString()) %>'></asp:Label>
					</ItemTemplate>
					<HeaderStyle Width="150px" />
				</asp:TemplateField>
				<asp:TemplateField HeaderText="职务">
					<EditItemTemplate>
						<asp:DropDownList ID="DropDownList2" runat="server" OnDataBound="DropDownList2_DataBound" AutoPostBack="True" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged">
						</asp:DropDownList>
						<asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("StudentTypeTitle") %>'></asp:TextBox>
					</EditItemTemplate>
					<ItemTemplate>
						<asp:Label ID="Label1" runat="server" Text='<%# Bind("StudentTypeTitle") %>'></asp:Label>
					</ItemTemplate>
				</asp:TemplateField>
				<asp:CommandField ShowEditButton="True" EditText="提干" UpdateText="提交">
					<HeaderStyle Width="150px" />
				</asp:CommandField>
			</Columns>
		</ZL:ExGridView>
	</td>
	</tr>
	
</table>
</div>
</form>
</body>
</html>