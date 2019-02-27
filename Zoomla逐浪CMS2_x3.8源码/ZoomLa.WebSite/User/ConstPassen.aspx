<%@ Page Language="C#" MasterPageFile="~/User/Default.master" AutoEventWireup="true" CodeFile="ConstPassen.aspx.cs" Inherits="User_ConstPassen" ClientIDMode="Static" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
<title>常用客户管理</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div id="pageflag" data-nav="group" data-ban="ConstPassen"></div>
<div class="container margin_t5">
<ol class="breadcrumb">
<li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
<li class="active">客户管理<a href="AddConstPassen.aspx?FieldName=Person_Add">[添加客户]</a></li>
</ol>
</div>
<div class="container u_cnt btn_green">
<ul class="nav nav-tabs" id="type_ul"> 
<li data-id="2" role="presentation"><a href="ConstPassen.aspx?type=2">所有客户</a></li>
<li data-id="0" role="presentation"><a href="ConstPassen.aspx?type=0">个人客户</a></li>  
<li data-id="1" role="presentation"><a href="ConstPassen.aspx?type=1">企业客户</a></li>
</ul>  
<ZL:ExGridView runat="server" ID="EGV" AutoGenerateColumns="false" AllowPaging="true" EnableTheming="False" 
		CssClass="table table-striped table-bordered table-hover" IsHoldState="false" EmptyDataText="当前没有信息!!" 
		OnPageIndexChanging="EGV_PageIndexChanging" >
	<Columns>
		<asp:TemplateField ItemStyle-CssClass="td_m" ItemStyle-HorizontalAlign="Left">
			<HeaderStyle Width="4%" />
			<ItemTemplate>
				<input type="checkbox" name="idchk" value="<%#Eval("Code") %>" />
			</ItemTemplate>
		</asp:TemplateField>
		<asp:BoundField DataField="Flow" HeaderText="ID">
			<HeaderStyle Width="6%" /> 
		</asp:BoundField>
		<asp:BoundField DataField="Add_Date" HeaderText="时间">
			<HeaderStyle Width="12%" /> 
		</asp:BoundField>
		<asp:TemplateField HeaderText="客户名"> 
			<ItemTemplate>
				<a href='ViewPassen.aspx?FieldName=Person_Add&id=<%#Eval("Flow") %>'><%#Eval("P_name")%></a>
			</ItemTemplate>
			<ItemStyle HorizontalAlign="Left" />
		</asp:TemplateField>
		<asp:BoundField DataField="Code" HeaderText="客户编号">
			<HeaderStyle Width="14%" /> 
		</asp:BoundField>
		<asp:TemplateField HeaderText="客户组别">
			<HeaderStyle Width="10%" />
			<ItemTemplate>
				<a href="ConstPassen.aspx?group=<%#Eval("Client_Group") %>&type=<%#Eval("Client_Type") %>"><%#Eval("Client_Group") %></a>
				</ItemTemplate>
		</asp:TemplateField>
		<asp:BoundField DataField="Client_Source" HeaderText="客户来源">
			<HeaderStyle Width="10%" /> 
		</asp:BoundField>
		<asp:TemplateField HeaderText="操作">
			<HeaderStyle Width="16%" />
			<ItemTemplate>
				<a href="ViewPassen.aspx?FieldName=Person_Add&id=<%#Eval("Flow") %>" class="option_style"><i class="fa fa-eye"></i>预览</a><a href="AddConstPassen.aspx?FieldName=Person_Add&menu=edit&id=<%#Eval("Flow") %>" class="option_style"><i class="fa fa-edit"></i>修改</a><a href="?menu=delete&code=<%#Eval("Code") %>" onclick="return confirm('你确定要将所有选择删除吗？');" class="option_style"><i class="fa fa-trash-o"></i>删除</a>
				</ItemTemplate>
		</asp:TemplateField>
	</Columns>
	<PagerStyle HorizontalAlign="Center" />
	<RowStyle Height="24px" HorizontalAlign="Center"  />
</ZL:ExGridView> 
<asp:LinkButton runat="server" OnClick="Button1_Click" CssClass="btn btn-primary">批量删除</asp:LinkButton>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<script type="text/javascript" src="/JS/SelectCheckBox.js"></script>
<script type="text/javascript">
function CheckAll(spanChk)//CheckBox全选
{
	var oItem = spanChk.children;
	var theBox = (spanChk.type == "checkbox") ? spanChk : spanChk.children.item[0];
	xState = theBox.checked;
	elm = theBox.form.elements;
	for (i = 0; i < elm.length; i++)
		if (elm[i].type == "checkbox" && elm[i].id != theBox.id) {
			if (elm[i].checked != xState)
				elm[i].click();
		}
}
function Getinfo(id) {
	location.href = "AddConstPassen.aspx?menu=edit&id=" + id + "";
} 
$().ready(function () { 
	var type = '<%=type %>';
	$("#type_ul [data-id='" + type + "']").addClass('active');
});  
</script>
</asp:Content>