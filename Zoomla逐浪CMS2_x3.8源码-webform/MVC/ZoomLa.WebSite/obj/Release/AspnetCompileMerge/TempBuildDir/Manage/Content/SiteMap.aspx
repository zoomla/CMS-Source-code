<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SiteMap.aspx.cs" Inherits="ZoomLaCMS.Manage.Content.SiteMap" MasterPageFile="~/Manage/I/Default.master"%>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>站点地图</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <asp:Label ID="Label1" runat="server" Text=""><br /></asp:Label>
<table class="table table-striped table-bordered table-hover">
	<tr class="tdbg">
		<td width="34%" height="24" align="left" class="title">
			★GOOGLE规范的XML地图生成操作
		</td>
	</tr>
	<tr class="tdbg">
		<td height="17" align="center">
			<a href='http://www.google.com/webmasters/sitemaps/login' target='_blank'>
				<img border="0" src="/images/google.gif" /></a>生成符合GOOGLE规范的XML格式地图页面
			<br />
		</td>
	</tr>
	<tr class="table table-striped table-bordered table-hover">
		<td height="18">
			更新频率：
			<asp:DropDownList ID="DropDownList1" runat="server">
				<asp:ListItem Value="always">频繁的更新</asp:ListItem>
				<asp:ListItem Value="hourly">每小时更新</asp:ListItem>
				<asp:ListItem Value="daily" Selected="selected">每日更新</asp:ListItem>
				<asp:ListItem Value="weekly">每周更新</asp:ListItem>
				<asp:ListItem Value="monthly">每月更新</asp:ListItem>
				<asp:ListItem Value="yearly">每年更新</asp:ListItem>
				<asp:ListItem Value="never">从不更新</asp:ListItem>
			</asp:DropDownList>
		</td>
	</tr>
	<tr>
		<td height="35">
			每个系统调用：
			<asp:TextBox ID="TextBox1" runat="server" class="l_input" Width="29px">5</asp:TextBox>
			条信息内容为最高注意度
		</td>
	</tr>
	<tr>
		<td height="35">
			注 意 度：<asp:TextBox ID="TextBox2" class="l_input" runat="server" Width="30px" Text="0.5"></asp:TextBox>&nbsp;0-1.0之间,推荐使用默认值
		</td>
	</tr>
</table>

<table class="table table-striped table-bordered table-hover">
	<tr>
		<td height="45" align="center">
			<asp:Button ID="Button1" runat="server" Text="开始生成网站地图" class="C_input" OnClick="Button1_Click"
				Width="180px" />&nbsp;
		</td>
	</tr>
</table>

<table class="table table-striped table-bordered table-hover">
	<tr class="tdbg">
		<td width="34%" height="24" align="left" class="title">
			★百度新闻开放协议XML生成操作
		</td>
	</tr>
	<tr class="tdbg">
		<td height="17" align="center">
			<a href="http://news.baidu.com/newsop.html#kg" target="_blank">
				<img border="0" src="/images/baidulogo.gif" /></a>生成符合百度XML格式的开放新闻协议
			<br />
		</td>
	</tr>
	<tr>
		<td height="18">
			更新周期：<asp:TextBox ID="changefreq" class="l_input" runat="server" Width="36px">15</asp:TextBox>分钟
		</td>
	</tr>
	<tr>
		<td height="35">每个系统调用：<asp:TextBox ID="prioritynum" class="l_input" runat="server" Width="31px">50</asp:TextBox>条信息内容为最高注意度(最多100条)
		</td>
	</tr>
</table>

<table width="100%" border="0" align="center" cellpadding="6" cellspacing="0">
	<tr>
		<td height="45" align="center">
			<asp:Button ID="Submit1" runat="server" Text="开始生成网站地图" class="C_input" OnClick="Submit1_Click" Width="180px" />
		</td>
	</tr>
</table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent"></asp:Content>