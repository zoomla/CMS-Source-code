<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MySubscription.aspx.cs" Inherits="User_MySubscription" EnableViewStateMac="false" MasterPageFile="~/User/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>订阅管理</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div id="pageflag" data-nav="group" data-ban="sub"></div>
<div class="container margin_t5">
<ol class="breadcrumb">
<li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
<li class="active">订阅管理</li> 
</ol>
</div>
<div class="container btn_green">
    <table class="table table-striped table-bordered">
	<tr><td colspan="5" class="text-center">我的订阅管理</td></tr>
	<tr>
		<td>编号</td>
		<td>订阅标题</td>
		<td>生效日期</td>
		<td>到期日期</td>
		<td>操作</td>
	</tr>
	<ZL:ExRepeater ID="RPT" runat="server" PagePre="<tr><td colspan='5' class='text-center'><input type='checkbox' id='CheckAll' />" PageEnd="</td></tr>" OnItemDataBound="RPT_ItemDataBound">
		<ItemTemplate>
			<tr>
				<td><%#Eval("Id")%></td>
				<td>
                    <a href="javascript::"><%#Eval("title") %></a>
				</td>
				<td class="td_l"><%#Eval("effectTime")%></td>
				<td class="td_l"><%#Eval("endTime","{0}")%> </td>
				<td class="td_l">
					<asp:LinkButton runat="server" CommandName="Del" CommandArgument='<%#Eval("Id") %>'>删除</asp:LinkButton>
				</td>
			</tr>
		</ItemTemplate>
		<FooterTemplate></FooterTemplate>
	</ZL:ExRepeater>
</table>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent"></asp:Content>