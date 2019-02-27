<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ClassManage.aspx.cs" Inherits="manage_Question_ClassManage" EnableViewStateMac="false" MasterPageFile="~/User/Default.master"%>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>班级管理</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div id="pageflag" data-nav="edu" data-ban="ke"></div>
<div class="container margin_t5">
<ol class="breadcrumb">
<li><a href="/User/">用户中心</a></li>
<li class="active">班级管理 [<a href="AddClass.aspx">申请班级</a>]</li>
</ol>
</div>
<div class="container btn_green">
<ul class="nav nav-tabs" visible="false" id="StuClass_Div" runat="server" role="tablist">
<li role="presentation" data-tab="-1"><a href="ClassManage.aspx" >全部班级</a></li>
<li role="presentation" data-tab="1"><a href="ClassManage.aspx?audit=1">已通过班级</a></li>
<li role="presentation" data-tab="0"><a href="ClassManage.aspx?audit=0">未通过班级</a></li>
</ul>

<table class="table table-striped table-bordered table-hover" id="EGV">
<tr>
	<td class="td_s">
	</td>
	<td>名称</td>
	<td>班标</td>
	<td>所属学校</td>
	<td>班主任</td>
	<td>是否毕业</td>
	<td>班级星级</td>
	<td>建立时间</td>
	<td>学生</td>
	<td>教师</td>
	<td>家长</td>
	<td>审核状态</td>
	<td class="title">操作</td>
</tr>
<ZL:ExRepeater ID="RPT_Teach" PageSize="10" runat="server" PagePre="<tr><td><input type='checkbox' id='chkAll'/></td><td colspan='12'><div class='text-center'>" PageEnd="</div></td></tr>">
	<ItemTemplate>
		<tr id="<%#Eval("RoomID") %>" ondblclick="ShowTabs(this.id)">
			<td>
				<input name="Item" type="checkbox" value='<%#Eval("RoomID") %>' />
			</td>
			<td>
				<%#Eval("RoomName")%>
			</td>
			<td>
				<%#GetIcon() %>
			</td>
			<td>
				<a href="SchoolShow.aspx?id=<%#Eval("SchoolID") %>"><%#Eval("SchoolName") %></a> 
			</td>
			<td>
				<%#Eval("UserName")%>
			</td>
			<td>
				<%#GetIsDone() %>
			</td>
			<td>
				<%#GetStar() %>
			</td>
			<td>
				<%#Eval("Creation")%>
			</td>
			<td>
				<%#Eval("StuCount") %>
			</td>
			<td>
				<%#Eval("TeachCount") %>
			</td>
			<td>
				<%#Eval("FamilyCount") %>
			</td>
		   <td>
			   <%#GetStatu() %>
		   </td>
			
			<td class="text-center">
				<a href="ClassShow.aspx?cid=<%#Eval("RoomID") %>" title="浏览"><span class="fa fa-eye"></span></a>
				<%#GetOP() %>
			</td>
		</tr>
	</ItemTemplate>
	<FooterTemplate></FooterTemplate>
</ZL:ExRepeater>
</table>
<asp:HiddenField ID="DelClass_Hid" runat="server" />
<asp:Button ID="Del_Btn" class="btn btn-primary" Style="display:none;" runat="server" OnClientClick="return confirm('你确定将该班级删除吗？');" onclick="Button3_Click" />
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<script type="text/javascript">
$().ready(function () {
	$("#chkAll").click(function () {
		$("[name='Item']").each(function (i, v) {
			$(v)[0].checked = $("#chkAll")[0].checked;
		});
	});
});
function InitTab(id) {
	$("[data-tab]").removeClass('active');
	$("[data-tab='" + id + "']").addClass('active');
}
function DelByID(id) {
	$("#DelClass_Hid").val(id);
	$("#Del_Btn").click();
}
</script>
</asp:Content>