<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RoomList.aspx.cs" Inherits="User_UserZone_School_RoomList" %>
<%@ Register Src="../WebUserControlTop.ascx" TagName="WebUserControlTop" TagPrefix="uc1" %>
<!DOCTYPE HTML>
<html>
<head id="Head1" runat="server">
<title>班级列表</title>
<link href="../../../App_Themes/UserThem/user_user.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" language="javascript">
function refpage(ret)
{
	if(typeof(ret)!="undefined")
	{
		window.document.getElementById("<%=this.HiddenField1.ClientID %>").value=ret;
	   document.form1.submit();
	}
}
	function Selectroom(id)
	{
		window.open('EnterTheClass.aspx?rID='+id,'','width=600,height=450,resizable=0,scrollbars=yes');
	}
	
</script>
<script type="text/javascript" language="javascript" src="../Common/common.js"></script>
<script type="text/javascript" language="javascript" src="../Common/subModal.js"></script>
</head>
<body>
<form id="form1" runat="server">
<div class="us_topinfo">
<a title="会员中心" href='<%=ResolveUrl("~/User/Default.aspx") %>' target="_parent">会员中心</a>&gt;&gt; <a href="mySchoolList.aspx"> 我的班级 </a>&gt;&gt;班级信息
</div>
<uc1:WebUserControlTop ID="WebUserControlTop1" runat="server"></uc1:WebUserControlTop>
<asp:HiddenField ID="HiddenField1" runat="server" OnValueChanged="HiddenField1_ValueChanged" />
<br />
<div class="us_showinfo">
<div><a href="mySchoolList.aspx"> 我的班级 </a> &gt;&gt; <a href="SchoolList.aspx">学校列表</a>
		&gt;&gt; <%=SchoolName%>班级列表 <hr /></div>
	<table width="100%" cellpadding="0" cellspacing="0" border="0">
		<tr>
			<td>
				<table width="100%">
					<tr>
						<td>
							<strong>&nbsp; &nbsp;班&nbsp; &nbsp;级</strong></td>
						<td align="right">
							<a href="AddRoom.aspx?Sid=<%=SchoolID %>">添加班级</a></td>
						<td style="width: 20px">
						</td>
					</tr>
				</table>
			</td>
		</tr>
		<tr>
			<td>
				<asp:DataList ID="dlClassRoom" Width="100%" runat="server" RepeatColumns="5" RepeatDirection="Horizontal">
					<HeaderTemplate>
					</HeaderTemplate>
					<ItemTemplate>
						<a title='<%#"班级介绍："+DataBinder.Eval(Container.DataItem,"Classinfo") %>' href="###" onclick="Selectroom('<%# DataBinder.Eval(Container.DataItem, "RoomID")%>');">
							<%# DataBinder.Eval(Container.DataItem, "RoomName")%>
						</a>
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
				</asp:DropDownList>页
			</td>
		</tr>
	</table>
</div>
</form>
</body>
</html>