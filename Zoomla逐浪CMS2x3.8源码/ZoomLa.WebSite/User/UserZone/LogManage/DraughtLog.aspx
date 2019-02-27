<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DraughtLog.aspx.cs" Inherits="DraughtLog"  %>
<%@ Register Src="../WebUserControlTop.ascx" TagName="WebUserControlTop" TagPrefix="uc1" %>
<!DOCTYPE HTML>
<html>
<head id="Head1" runat="server">
<title>会员中心 >> 我的日志</title>
<link href="/App_Themes/User.css" rel="stylesheet" type="text/css" />
</head>
<body>
<form id="form1" runat="server">
<!-- str -->
<div class="us_topinfo">
<a title="会员中心" href='<%=ResolveUrl("~/User/Default.aspx")%>' target="_parent"> 会员中心</a>&gt;&gt;<a title="我的日志" href='<%=ResolveUrl("~/User/UserZone/LogManage/SelfLogManage.aspx")%>'> 我的日志</a>&gt;&gt;我的草稿箱
</div>
<uc1:WebUserControlTop ID="WebUserControlTop1" runat="server"></uc1:WebUserControlTop>
</div>
<table width="100%"  border="0" cellpadding="0" cellspacing="0">
  <tr>
	<td rowspan="2" style="width: 110px">&nbsp;
	  <asp:Image ID="Image1" runat="server" ImageUrl="~/App_Themes/DefaultTheme/images/head.jpg" /></td>
	<td >&nbsp;我的草稿箱</td>
  </tr>
  <tr>
	<td valign="top" >&nbsp;
	  <asp:LinkButton ID="lbtnAddNewLog" runat="server">写新日志</asp:LinkButton>
	  &nbsp;|&nbsp;
	  <asp:LinkButton ID="lbtnManage" runat="server">日志管理</asp:LinkButton></td>
  </tr>
</table>
<div class="nav">
  <ul>
	<li class="nav_bg2"><a href="SelfLogManage.aspx" target="_blank">我的日志</a></li>
	<li class="nav_bg2"><a href="#" target="_blank">与我相关的日志</a></li>
	<li class="nav_bg1"><a href="DraughtLog.aspx" target="_blank">草稿箱</a></li>
  </ul>
</div>
<table border="0" cellpadding="0" cellspacing="1" width="100%" bgcolor="212,208,200">
  <tr>
	<td style="width: 80%" bgcolor="#FFFFFF" valign="top"><ZL:ExGridView ID="gvLog" runat="server" AutoGenerateColumns="False" Width="100%" GridLines="None" CellPadding="4" DataKeyNames="ID" ShowHeader="False">
		<Columns>
		<asp:TemplateField>
		  <ItemTemplate>
			<table border="0" cellpadding="0" cellspacing="0" width="100%">
			  <tr>
				<td><hr /></td>
			  </tr>
			  <tr>
				<td><table border="0" cellpadding="0" cellspacing="0" width="100%">
					<tr>
					  <td><asp:Label ID="Label1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"LogTitle") %>'></asp:Label></td>
					  <td><a href ="CreatLog.aspx?LogID=<%#DataBinder.Eval(Container.DataItem,"ID")%>&where=2">编辑</a> &nbsp;|&nbsp;
						<asp:LinkButton ID="lbtnDelete" runat="server"  OnClientClick="return confirm('确定删除吗？');">删除</asp:LinkButton></td>
					</tr>
					<tr>
					  <td colspan="2"><asp:Label ID="Label2" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"CreatDate") %>'></asp:Label>
						(分类:
						<asp:Label ID="Label3" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"LogTypeID") %>'></asp:Label>
						) </td>
					</tr>
					<tr>
					  <td colspan="2">&nbsp;</td>
					</tr>
					<tr>
					  <td colspan="2"><asp:Label ID="Label4" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"LogContext") %>'></asp:Label></td>
					</tr>
					<tr>
					  <td colspan="2" style="background: 247,247,247">&nbsp;</td>
					</tr>
				  </table></td>
			  </tr>
			</table>
		  </ItemTemplate>
		</asp:TemplateField>
		</Columns>
	  </ZL:ExGridView></td>
  </tr>
  <tr>
	<td colspan="3" bgcolor="#FFFFFF" ></td>
  </tr>
</table>
</form>
</body>
</html>