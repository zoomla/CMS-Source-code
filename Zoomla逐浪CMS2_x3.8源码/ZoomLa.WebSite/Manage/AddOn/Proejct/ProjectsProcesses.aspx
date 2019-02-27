<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProjectsProcesses.aspx.cs" Inherits="manage_AddOn_ProjectsAudit" EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>项目审核</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div class="r_navigation">        
	<span>后台管理</span> &gt;&gt; <span>CRM应用</span> &gt;&gt; <span><a href="Projects.aspx">项目管理</a></span> &gt;&gt; <span>
		<asp:LinkButton ID="LBtnProject" runat="server"></asp:LinkButton></span> &gt;&gt; <span>项目流程</span>&gt;&gt;[<a href="AddProcesses.aspx?id=<%=Request.QueryString["ID"]%>" style="color:Red">添加流程</a>]
</div>
<div class="clearbox"></div>
<table width="100%"  cellpadding="0" cellspacing="1" align="center" class="border" border="0">
	<tbody>
		<tr class="gridtitle" align="center" style="height:25px">
			<td width="7%">选择</td>
			<td width="7%">ID</td>
			<td width="7%">项目ID</td>
			<td width="15%">流程名称</td>
			<td width="15%">当前进度</td>
			<td width="10%">是否完成</td>
			<td width="15%">完成时间</td>
			<td width="24%">操作</td>
		</tr>
		<asp:Repeater ID="Repeater1" runat="server" onitemcommand="Repeater1_ItemCommand">
			<ItemTemplate>
				<tr class="tdbg" height="25px" align="center" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
					<td><asp:CheckBox ID="ChBx" runat="server"/><asp:Label ID="Label1" runat="server" Text='<%# Eval("ID") %>' Visible="false"></asp:Label></td>     
					<td><%# Eval("ID")%> </td>
					<td><%# Eval("OpjectID")%></td>
					<td><a href='AddProcesses.aspx?processesid=<%#Eval("ID") %>'><%# Eval("Name")%></a></td>
					<td><%# Eval("Progress")%>%</td>
					<td><%# GetComplete(Eval("IsComplete","{0}"))%> </td>
					<td><%# Eval("CompleteTime")%></td>
					<td>
						<asp:LinkButton ID="LkBtnEdit" runat="server" CommandArgument='<%# Eval("ID")%>' CommandName="edit">编辑</asp:LinkButton>&nbsp;|&nbsp;
						<asp:LinkButton ID="LkBtnComplete" runat="server" CommandArgument='<%# Eval("ID")%>' CommandName="Complete"><%#GetCom(Eval("IsComplete","{0}"))%>完成</asp:LinkButton>&nbsp;|&nbsp;
						<asp:LinkButton ID="LkBtnDel" runat="server" CommandArgument='<%# Eval("ID")%>' CommandName="del" OnClientClick="if(!this.disabled) return confirm('确实要删除吗？');">删除</asp:LinkButton>&nbsp;|&nbsp;
						<asp:LinkButton ID="LkBtnComments" runat="server" CommandArgument='<%# Eval("ID")%>' CommandName="Comments" Enabled='<%#Getbool(Eval("ID","{0}")) %>'>评论</asp:LinkButton></td>
				</tr>
			</ItemTemplate>
		</asp:Repeater>
	</tbody>
</table>
<table>
	<tr>
		<td>
			<asp:CheckBox ID="Checkall" runat="server" onclick="javascript:CheckAll(this);" Text="全选" />
			<asp:Button ID="btnDeleteAll" runat="server" style="width:110px;"  OnClientClick="if(!IsSelectedId()){alert('请选择删除项');return false;}else{return confirm('你确定要将所有选择项删除吗？')}" Text="批量删除" OnClick="btnDeleteAll_Click" class="btn btn-primary" />
		</td>
	</tr>
</table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">

</asp:Content>