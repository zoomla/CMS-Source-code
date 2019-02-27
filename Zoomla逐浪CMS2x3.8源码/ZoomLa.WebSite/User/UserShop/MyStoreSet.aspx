<%@ Page Title="" Language="C#" MasterPageFile="~/User/Default.master" AutoEventWireup="true" CodeFile="MyStoreSet.aspx.cs" Inherits="User_UserShop_MyStoreSet" ClientIDMode="Static" ValidateRequest="false" EnableViewStateMac="false" %>
<%@ Register Src="WebUserControlTop.ascx" TagName="WebUserControlTop" TagPrefix="uc2" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
<title>我的店铺</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<ol class="breadcrumb">
	<li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
	<li><a href="ProductList.aspx">我的店铺</a></li>
	<li class="active">店铺基本信息</li>
</ol>
<div class="s_bright">
	<uc2:WebUserControlTop ID="WebUserControlTop1" runat="server" />
	<table class="table table-striped table-bordered table-hover">
		<tr>
			<td colspan="2" class="text-center">店铺基本管理</td>
		</tr>
		<tr>
			<td>商铺名称：</td>
			<td>
				<asp:TextBox ID="Nametxt" runat="server" Text='' CssClass=" form-control"></asp:TextBox>
				<span><font color="red">*</font></span>
				<asp:RequiredFieldValidator ID="RequiredFieldValidator5" Display="Dynamic" runat="server" ErrorMessage="RequiredFieldValidator" ControlToValidate="Nametxt">名称必填</asp:RequiredFieldValidator>
			</td>
		</tr>
		<tr>
			<td>
				商铺类型：
			</td>
			<td>
				<asp:DropDownList CssClass="form-control" ID="DropDownList1" runat="server"></asp:DropDownList>
				<span><font color="red">*</font></span>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1"
				runat="server" ErrorMessage="RequiredFieldValidator" ControlToValidate="DropDownList1">类型必填</asp:RequiredFieldValidator>
			</td>
		</tr>
		<tr>
			<td>所在城市：</td>
			<td>
				<asp:DropDownList ID="DropDownList2" CssClass="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged"> </asp:DropDownList>
				省
				<asp:DropDownList ID="DropDownList3" CssClass="form-control" runat="server"></asp:DropDownList>
				市
				<span><font color="red">*</font></span>
				<asp:RequiredFieldValidator ID="RequiredFieldValidator3" Display="Dynamic" runat="server" ErrorMessage="RequiredFieldValidator" ControlToValidate="DropDownList3">所在城市必填</asp:RequiredFieldValidator>
			</td>
		</tr>
		<tr>
			<td>商铺简介：</td>
			<td>
				<textarea id="TEXTAREA1" runat="server" class="form-control" style="max-width:300px;" cols="50" rows="6"></textarea><span><font color="red">*</font></span>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator4"
					runat="server" ErrorMessage="RequiredFieldValidator" ControlToValidate="TEXTAREA1">简介必填</asp:RequiredFieldValidator>
			</td>
		</tr>
		<tr>
			<td colspan="2" class="text-center">
				<asp:Button ID="EBtnSubmit" CssClass="btn btn-primary" Text="信息提交" runat="server" OnClick="EBtnSubmit_Click" />
				<input id="Button1" class="btn btn-primary" type="button" value="返  回" onclick="javascript: history.go(-1)" />
			</td>
		</tr>
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
