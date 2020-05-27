<%@ Page Title="" Language="C#" MasterPageFile="~/User/Default.master" AutoEventWireup="true" CodeFile="BossContent.aspx.cs" Inherits="User_BossInfo_BossContent" ClientIDMode="Static" EnableViewStateMac="false" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
<title>上级代理商信息</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div id="pageflag" data-nav="page" data-ban="page"></div>
<div class="container">
<ol class="breadcrumb">
<li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
<li class="active">上级代理商信息</li>
</ol>
</div>
<div class="container">
<table class="table table-striped table-bordered table-hover">
<tr>
<td align="center" colspan="2">你的上级代理商信息
</td>
</tr>
<tr>
<td align="right">代理商名称:
</td>
<td width="72%" align="left">&nbsp;<asp:Label ID="Label7" runat="server" Text=""></asp:Label>
</td>
</tr>
<tr>
<td align="right">联系电话：
</td>
<td align="left">&nbsp;<asp:Label ID="Label8" runat="server" Text=""></asp:Label>
</td>
</tr>
<tr>
<td align="center" colspan="2">代理商基本信息
</td>
</tr>
<tr>
<td align="right">代理商名称:
</td>
<td width="72%" align="left">&nbsp;<asp:Label ID="tx_cname" runat="server" Text=""></asp:Label>
</td>
</tr>
<tr>
<td align="right">代理商电话:
</td>
<td width="72%" align="left">&nbsp;<asp:Label ID="txtTel" runat="server" Text=""></asp:Label>
</td>
</tr>
<tr>
<td align="center" colspan="2">总 业 绩 ：
</td>
</tr>
<tr>
<td align="right">收益金额：
</td>
<td align="left">&nbsp;<asp:Label ID="tx_money" runat="server" Text=""></asp:Label>
&nbsp;&nbsp;
<asp:Label ID="lblVIP" runat="server" Text=""></asp:Label>
</td>
</tr>
<tr>
<td align="right">定单金额：
</td>
<td align="left">&nbsp;<asp:Label ID="tx_zong" runat="server" Text=""></asp:Label>
</td>
</tr>
<tr>
<td align="right">定单数量：
</td>
<td align="left">&nbsp;<asp:Label ID="tx_num" runat="server" Text=""></asp:Label>
</td>
</tr>
<tr>
<td align="center" colspan="2">招 商 费 ：
</td>
</tr>
<tr>
<td align="right" class="style2">收益金额：
</td>
<td align="left" style="height: 24px">&nbsp;<asp:Label ID="Label1" runat="server" Text=""></asp:Label>
</td>
</tr>
<tr>
<td align="right">直接招商金额：
</td>
<td align="left">&nbsp;<asp:Label ID="Label2" runat="server" Text=""></asp:Label>
</td>
</tr>
<tr id="jjzs" runat="server">
<td align="right">间接招商金额：
</td>
<td align="left">&nbsp;<asp:Label ID="Label3" runat="server" Text=""></asp:Label>
</td>
</tr>
<tr>
<td align="center" colspan="2">下 级 数 量
</td>
</tr>
<tr>
<td align="right" class="style2">服务中心：
</td>
<td align="left" style="height: 24px">&nbsp;<asp:Label ID="fhwunum" runat="server" Text=""></asp:Label>
</td>
</tr>
<tr>
<td align="right" class="style2">服务店:
</td>
<td align="left" style="height: 24px">&nbsp;<asp:Label ID="Enum" runat="server" Text=""></asp:Label>
</td>
</tr>
</table>
</div>
</asp:Content>