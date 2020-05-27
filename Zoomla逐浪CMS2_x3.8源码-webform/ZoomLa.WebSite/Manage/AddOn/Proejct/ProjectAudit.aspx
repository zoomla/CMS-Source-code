<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Manage/I/Default.master" CodeFile="ProjectAudit.aspx.cs" Inherits="manage_AddOn_ProjectAudit" EnableViewStateMac="false" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>项目审核</title>    
<script language="javascript" type="text/javascript">
function CheckAll(spanChk)//CheckBox全选
{
var oItem = spanChk.children;
var theBox=(spanChk.type=="checkbox")?spanChk:spanChk.children.item[0];
xState=theBox.checked;
elm=theBox.form.elements;
for(i=0;i<elm.length;i++)
if(elm[i].type=="checkbox" && elm[i].id!=theBox.id)
{
	if(elm[i].checked!=xState)
	elm[i].click();
}
}
function getinfo(id) {
location.href = "ProjectsDetail.aspx?ProjectID=" + id + "";
}
</script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<table class="table table-responsive table-bordered table-hover">
	<tbody>
		<tr class="gridtitle">
			<td>选择</td>
			<td>ID</td>
			<td>项目名称</td>
			<td>项目类型</td>
			<td>项目价格</td>
			<td>审核状态</td>
			<td>申请时间</td>
			<td>操作</td>
		</tr>
		<ZL:ExRepeater ID="RPT" runat="server" PagePre="<tr><td colspan='8' class='text-center'><input type='checkbox' id='CheckAll' />" PageEnd="</td></tr>"
			onitemcommand="Repeater1_ItemCommand" >
			<ItemTemplate>
				<tr id='<%#Eval("ID") %>' class="tdbg" align="center" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'" ondblclick="getinfo(this.id);" title="双击查看此项目详情">
					<td><asp:CheckBox ID="ChBx" runat="server"/><asp:Label ID="Label1" runat="server" Text='<%# Eval("ID") %>' Visible="false"></asp:Label></td>     
					<td><%# Eval("ID","{0}")%></td>
					<td><a href="ProjectsDetail.aspx?ProjectID=<%# Eval("ID","{0}")%>"><%# Eval("Name")%></a></td>
					<td><a href="Projects.aspx?type=<%#Eval("TypeID") %>"><%#Eval("TypeID") %></a></td>
					<td>￥<%# Eval("Price")%>.00</td>
					<td><%#Eval("AuditStatus")%></td>
					<td><%# Eval("ApplicationTime")%></td>
					<td><asp:LinkButton ID="LBtnAudit" runat="server" CommandArgument='<%# Eval("ID")%>' CommandName="Audit" OnClientClick="if(!this.disabled) return confirm('确定要执行此操作吗？');">审核</asp:LinkButton>&nbsp;|
						<asp:LinkButton ID="LBtnDel" runat="server" CommandArgument='<%# Eval("ID")%>' CommandName="Del" OnClientClick="if(!this.disabled) return confirm('确实要删除吗？');">删除</asp:LinkButton>
					</td>
				</tr>
			</ItemTemplate>
            <FooterTemplate></FooterTemplate>
		</ZL:ExRepeater>
	</tbody>
</table>
<table>
	<tr>
		<td>
			<asp:CheckBox ID="Checkall" runat="server" onclick="javascript:CheckAll(this);" Text="全选" />
			<asp:Button ID="btnDeleteAll" runat="server" style="width:110px;"  
				OnClientClick="if(!IsSelectedId()){alert('请选择删除项');return false;}else{return confirm('你确定要将所有选择项删除吗？')}" Text="批量删除" class="C_input" 
				onclick="btnDeleteAll_Click" />
			<asp:Button ID="BtnAudit" runat="server" style="width:110px;" 
				OnClientClick="return confirm('确定要执行此操作吗？')" Text="批量审核" class="C_input" 
				onclick="BtnAudit_Click"/>
		</td>
	</tr>
</table>
</asp:Content>

