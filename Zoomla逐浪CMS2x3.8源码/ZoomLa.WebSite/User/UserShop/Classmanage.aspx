<%@ Page Title="" Language="C#" MasterPageFile="~/User/Default.master" AutoEventWireup="true" CodeFile="Classmanage.aspx.cs" Inherits="User_UserShop_Classmanage" ClientIDMode="Static" ValidateRequest="false" EnableViewStateMac="false" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
<title>我的店铺</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<ol class="breadcrumb">
	<li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
	<li class="active">分类管理<a href="addclass.aspx">[添加分类]</a></li>
</ol>
<!-- str -->
<div class="us_seta" style="margin-top: 5px; height: 300px">
	<table class="table table-striped table-bordered table-hover">
		<tr>
			<td colspan="3" class="text-center">分类管理</td>
		</tr>
		<tr align="center">
			<td width="33%">分类ID
			</td>
			<td width="33%">分类名称
			</td>
			<td width="33%">操作
			</td>
		</tr>
		<ZL:ExRepeater ID="RPT" runat="server" PagePre="<tr><td colspan='3' class='text-center'><input type='checkbox' id='CheckAll' />" PageEnd="</td></tr>">
			<ItemTemplate>
				<tr class="">
					<td align="center">
						<%#Eval("id","{0}") %>
					</td>
					<td align="center">
						<a href="Addclass.aspx?menu=edit&id=<%#Eval("id") %>"><%#Eval("Classname")%></a>
					</td>
					<td align="center">
						<a href="Addclass.aspx?menu=edit&id=<%#Eval("id") %>">修改</a> <a href="?menu=delete&id=<%#Eval("id") %>" onclick="return confirm('不可恢复性删除数据,你确定将该数据删除吗？');">删除</a>
					</td>
				</tr>
			</ItemTemplate>
			<FooterTemplate></FooterTemplate>
		</ZL:ExRepeater>
	</table>
</div>

</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">

<script language="javascript">
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
</script>
</asp:Content>
