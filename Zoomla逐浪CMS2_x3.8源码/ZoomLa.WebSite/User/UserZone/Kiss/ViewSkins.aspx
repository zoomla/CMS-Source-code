<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewSkins.aspx.cs" Inherits="User_UserZone_Kiss_ViewSkins" %>
<%@ Register Src="../WebUserControlTop.ascx" TagName="WebUserControlTop" TagPrefix="uc2" %>
<!DOCTYPE HTML>
<html>
<head id="Head1" runat="server">
<title>设置中心</title>
<link href="../../../App_Themes/UserThem/user_user.css" rel="stylesheet" type="text/css" />
<script src='<%=ResolveUrl("~/JS/DatePicker/WdatePicker.js")%>' type="text/javascript"></script>
<style type="text/css">
.style1{height: 418px;}
.style2{height: 31px;}
.style3{height: 30px;}
</style>
</head>
<body>
<form id="form1" runat="server">
<div class="s_bright">
<!-- str -->
<div class="us_topinfo">
 <a title="会员中心" href='<%=ResolveUrl("~/User/Default.aspx")%>' target="_parent">会员中心</a>&gt;&gt;设置中心&gt;&gt;我的秋波
</div>
<uc2:WebUserControlTop ID="WebUserControlTop1" runat="server" />
<br />
<div class="us_topinfo">
	<asp:Repeater ID="Repeater1" runat="server">
	<ItemTemplate>
	 <table width="100%" height="91" border="0" cellpadding="0" cellspacing="0">
		<tr>
			<td valign="top" style="width: 100%">
			<table width="100%" border="0" cellpadding="0" cellspacing="0" style="word-break:break-all">
				<tr>
					<td align="center" rowspan="3">
						<asp:Image ID="Image1" runat="server" Height="96px" Width="96px" ImageUrl='<%# getuserpic(DataBinder.Eval(Container.DataItem, "InputerID").ToString())%>' />
						<br /><a href="rekiss.aspx?userid=<%#Eval("inputerID") %>">回发秋波</a>
					</td>
					<td width="84%" class="style2">
					<a href="kiss.aspx?menu=delete&id=<%#Eval("ID") %>" onclick="return confirm('确实要删除此秋波吗？');" >[删除]</a>&nbsp;<%#Eval("title","{0}")%>内容:
					</td>
				</tr>
				<tr>
					<td valign="top" style="white-space: normal">
					   <%#DataBinder.Eval(Container.DataItem, "content","{0}")%>
					</td>
				</tr>
				<tr>
					<td class="style3">
					<%#Eval("IsRead", "{0}") == "1" ? Eval("ReadTime", "查看时间：{0}　　") : ""%>发送人：<%#Eval("Inputer")%>　　发送时间：<%#Eval("SendTime")%>
						
					</td>
				</tr>
				<tr>
					<td align="center" valign="top" style="width: 152px" class="trr">					   
					</td>
					<td class="trr">
						
					</td>
				</tr>
			</table>
			
			</td>
		</tr>
	</table>
	</ItemTemplate>
	</asp:Repeater>   
</div>
</div>
</form>
</body>
</html>