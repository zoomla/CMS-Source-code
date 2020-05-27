<%@ Page Title="" Language="C#" MasterPageFile="~/User/Default.master" AutoEventWireup="true" CodeFile="UserDay.aspx.cs" Inherits="User_Info_UserDay" ClientIDMode="Static" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
<title>节日提醒</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div id="pageflag" data-nav="group" data-ban="UserDay"></div>
<div class="container margin_t5">
<ol class="breadcrumb">
<li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
<li class="active">节日提醒</li> 
</ol>
</div>
<asp:HiddenField ID="hidenid" runat="server" />
<div class="container ">
<div class="us_seta" style="margin-top: 5px;">
	<table width="100%" cellpadding="2" cellspacing="1" class="table table-striped table-bordered table-hover">
		<tr class="tdbgleft" style="text-align: center; font-weight: bold; height: 26px">
			<td width="10%">ID </td>
			<td width="10%">时间 </td>
			<td width="70%">节日标题 </td>
			<td width="10%">操作 </td>
		</tr>
		<asp:Repeater ID="Repeater1" runat="server">
			<ItemTemplate>
				<tr class="tdbg" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'" style="text-align: center">
					<td>
						<asp:Label ID="idla" runat="server" Text=""></asp:Label></td>
					<td><%#Eval("D_date","{0:d}")%></td>
					<td><%#Eval("D_name")%></td>
					<td><a href="?menu=edit&id=<%#Eval("id") %>">修改</a> <a href="?menu=delete&id=<%#Eval("id") %>" onclick="return confirm('不可恢复性删除数据,你确定将该数据删除吗？');">删除</a></td>
				</tr>
			</ItemTemplate>
		</asp:Repeater>
	</table>
</div>
</div>
<div class="container btn_green">
<div class="us_seta" style="margin-top: 5px;">
	<table class="table table-striped table-bordered table-hover">
		<tr>
			<td colspan="2" class="text-center">
				<asp:Literal ID="nodetxt" runat="server" Text="节日提醒"></asp:Literal>
			</td>
		</tr>
		<tr>
			<td>节日标题： </td>
			<td>
				<asp:TextBox ID="D_title" runat="server" class="form-control text_md" />
				<asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" runat="server" ControlToValidate="D_title" ErrorMessage="节日标题不能为空！"></asp:RequiredFieldValidator>
			</td>
		</tr>
		<tr>
			<td>节日日期： </td>
			<td>
				<asp:TextBox ID="D_date" runat="server" class="form-control text_md" Text="" onclick="WdatePicker({ dateFmt: 'yyyy-MM-dd HH:mm:ss' })"></asp:TextBox>
				<span id="Span2">
					<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="D_date" ErrorMessage="节日日期不能为空！"></asp:RequiredFieldValidator>
				</span>
			</td>
		</tr>
		<tr>
			<td>节日说明： </td>
			<td>
				<asp:TextBox ID="D_Content" runat="server" Height="80px" CssClass="form-control text_md" TextMode="MultiLine" Width="425px"></asp:TextBox></td>
		</tr>
		<tr>
			<td style="text-align: center" colspan="2">
				<asp:Button ID="BtnSubmit" runat="server" Text="添加" class="btn btn-primary" OnClick="BtnSubmit_Click" />
				<asp:Button ID="BtnCancle" runat="server" Text="取消" class="btn btn-primary" CausesValidation="false" OnClick="BtnCancle_Click" />
			</td>
		</tr>
	</table>
</div>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<script type="text/javascript" src="/JS/DatePicker/WdatePicker.js"></script>
</asp:Content>