<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ManagerZone.aspx.cs" Inherits="User_UserZone_GatherStrainManage_ManagerZone" %>
<!DOCTYPE html >
<%@ Register Src="../WebUserControlTop.ascx" TagName="WebUserControlTop" TagPrefix="uc1" %>
<%@ Register Src="WebUserControlGztherLetf.ascx" TagName="WebUserControlGztherLetf" TagPrefix="uc2" %>
<html >
<head runat="server">
    <title></title>
    <link href="../../../App_Themes/UserThem/user_user.css" rel="stylesheet" type="text/css" />
</head>
<body>
<form id="form1" runat="server">
<div class="us_topinfo">
<a title="会员中心" href="/User/Default.aspx" target="_parent">会员中心</a>&gt;&gt; <a title="我的空间" href="/User/Userzone/Default.aspx">我的空间</a>
</div>
<uc1:WebUserControlTop ID="WebUserControlTop1" runat="server" />
<uc2:WebUserControlGztherLetf ID="WebUserControlGztherLetf2" runat="server" />
</div>
<div class="us_topinfo" style="margin-top: 10px;">
<table width="100%" border="0" cellspacing="0" height="70px" cellpadding="4">
	<tr>
		<td>
		<a href="GSBuid.aspx?GSID=<%=GSID %>&where=5" >	<asp:Image ID="imgGSICQ" runat="server" Width="150px" Height="100px" /></a>
		</td>
	</tr>
</table>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
	<tr>
		<td>
			<table>
				<tr>
					<td>
						<a href='GSBuid.aspx?GSID=<%=GSID %>'>群族首页</a>
					</td>
					<td>
						<a href='CreatHuaTee.aspx?GSID=<%=GSID %>'>话题</a>
					</td>
					<td>
						<a href='FileShareManage.aspx?GSID=<%=GSID %>'>文件共享</a>
					</td>
					<td>
						<a href='GSMember.aspx?GSID=<%=GSID %>'>成员</a>
					</td>
					<td>
					</td>
				</tr>
			</table>
		</td>
		<td style="width: 2px">
			&nbsp;
		</td>
		<td>
		</td>
	</tr>
    </table>
</div>
<hr />
<asp:Label runat="server" ID="LB_Q" Visible="false"></asp:Label>
<div runat="server" id="ceshi">

</div>
<div runat="server" id="addmanager">
    <table><tr><td style="width:80px">会员名</td><td style="width:150px">注册时间</td><td style="width:80px">积分</td><td style="width:80px">操作</td></tr></table>
    <asp:Repeater runat="server" ID="RP_Show" onitemcommand="RP_Show_ItemCommand" >
    <ItemTemplate>
   <div style="width:80px;float:left"> <%#Eval("UserName")%></div><div style="width:150px; float:left"><%#Eval("JoinTime")%></div><div style="width:80px; float:left"><%#Eval("UserExp")%></div><div style="width:80px; float:left"><asp:LinkButton runat="server" ID="add" CommandName="add" CommandArgument='<%#Eval("UserID")%>'>添加</asp:LinkButton> </div><br />
    </ItemTemplate>
    </asp:Repeater>
</div>


<div runat="server" id="DV_Invite" >
    <table><tr><td style="width:80px">会员名</td><td style="width:150px">注册时间</td><td style="width:80px">积分</td><td style="width:80px">操作</td></tr></table>
    <asp:Repeater runat="server" ID="RP_Invite" 
        onitemcommand="RP_Show_ItemCommand1" >
    <ItemTemplate>
   <div style="width:80px;float:left"> <%#Eval("UserName")%></div><div style="width:150px; float:left"><%#Eval("JoinTime")%></div><div style="width:80px; float:left"><%#Eval("UserExp")%></div><div style="width:80px; float:left"><asp:LinkButton runat="server" ID="Invite" CommandName="Invite" CommandArgument='<%#Eval("UserID")%>' >邀请 </asp:LinkButton> </div><br />
    </ItemTemplate>
    </asp:Repeater>
</div>

<hr />
<table><tr><td style="width: 647px">
						<div style="height: 30px; text-align: center;">共<asp:Label ID="Allnum" runat="server"
							Text=""></asp:Label>页&nbsp;
							<asp:hyperlink id="lnkPrev" runat="server">上页</asp:hyperlink>
                            <asp:hyperlink id="lnkNext" runat="server">下页</asp:hyperlink>
							第：<asp:Label ID="Nowpage" runat="server" Text="" />页
							8个/页 转到第<asp:DropDownList ID="DropDownList1"
								runat="server" AutoPostBack="True">
							</asp:DropDownList>
							页</div>
					</td></tr></table>
<asp:Label runat="server" ID="LB_show" Visible="false"></asp:Label>
<div runat="server" id="DV_XS">
<asp:Label runat="server" ID="LB_Name"></asp:Label>
    <table><tr><td style="width:80px">会员名</td><td style="width:150px">注册时间</td><td style="width:80px">积分</td><td style="width:80px">操作</td></tr></table>
    <asp:Repeater runat="server" ID="Repeater2" 
        onitemcommand="RP_Show_ItemCommand2" >
    <ItemTemplate>
   <div style="width:80px;float:left"> <%#Eval("UserName")%></div><div style="width:150px; float:left"><%#Eval("JoinTime")%></div><div style="width:80px; float:left"><%#Eval("UserExp")%></div><div style="width:80px; float:left"><asp:LinkButton runat="server" ID="cancel" CommandName="cancel" CommandArgument='<%#Eval("UserID")%>'>撤销</asp:LinkButton> </div><br />
    </ItemTemplate>
    </asp:Repeater>
</div>


</form>
</body>
</html>
