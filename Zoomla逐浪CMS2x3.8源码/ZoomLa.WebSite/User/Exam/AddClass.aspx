<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddClass.aspx.cs" Inherits="manage_Question_AddClass" EnableViewStateMac="false" MasterPageFile="~/User/Default.master"%>
<asp:Content runat="server" ContentPlaceHolderID="head">
	<link type="text/css" href="/dist/css/bootstrap-switch.min.css" rel="stylesheet" />
	<title>编辑班级</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div id="pageflag" data-nav="edu" data-ban="ke"></div>
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<asp:HiddenField runat="server" ID="classid_Hid" />
<div class="container margin_t5">
  <ol class="breadcrumb">
	<li><a href="/User/">用户中心</a></li>
	<li><a href="ClassManage.aspx">班级管理</a></li>
	<li class="active">编辑班级</li> 
  </ol>
</div>
<div class="container btn_green">
<table class="table table-striped table-bordered table-hover">
	<tbody id="Tabs0">
		<tr>
			<td class="td_m text-right">
				班级名称:
			</td>
			<td>
				<asp:TextBox ID="ClassName_T" runat="server" class="form-control text_300" ></asp:TextBox>
				<font color="red">*</font>
			</td>
		</tr>
		<tr>
			<td class="td_l text-right"><strong>所属学校:</strong></td>
			<td>
                <asp:TextBox ID="SchoolName_T" runat="server" CssClass="form-control text_300" Enabled="false" placeholder="请选择学校"></asp:TextBox>
                <asp:HiddenField runat="server" ID="SchoolName_Hid" />
                <button type="button" onclick="ShowSchool()" class="btn btn-primary">填写或选择学校</button>
			</td>
		</tr>
		<tr>
		<td class="text-right"><strong>班级年级:</strong></td>
			<td>
				<asp:DropDownList ID="GradeList_Drop" CssClass="form-control text_300" runat="server" DataTextField="GradeName" DataValueField="GradeID"></asp:DropDownList>
			</td>
		</tr>
		<tr>
			<td class="text-right"><strong>最大人数:</strong></td>
			<td>
				<asp:TextBox ID="ClassNum_T" Text="100" runat="server" CssClass="form-control text_x int"></asp:TextBox>
			</td>
		</tr>
		<tr>
			<td class="text-right"><strong>是否毕业:</strong></td>
			<td>
				<input type="checkbox" class="switchChk" runat="server" id="IsDone_Check" />
			</td>
		</tr>
		<tr>
			<td class="text-right">班级微标:</td>
			<td>
				<asp:TextBox ID="ClassIcon_T" runat="server" CssClass="form-control text_300"></asp:TextBox>
			</td>
		</tr>
		<tr>
			<td class="text-right">班级宣言:</td>
			<td>
				<asp:TextBox ID="ClassDeailt_T" runat="server" CssClass="form-control text_300" TextMode="MultiLine"></asp:TextBox>
			</td>
		</tr>
	</tbody>
	<tr class="tdbgbottom">
		<td></td>
		<td>
			<asp:Button ID="EBtnSubmit" class="btn btn-primary" Text="添加班级" runat="server" OnClientClick="return CheckData();" OnClick="EBtnSubmit_Click" />
			<a href="ClassManage.aspx" class="btn btn-primary">返回列表</a>
		</td>
	</tr>
</table>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<script type="text/javascript" src="/dist/js/bootstrap-switch.js"></script>
<script src="/JS/DatePicker/WdatePicker.js"></script>
<script type="text/javascript" src="/JS/Controls/ZL_Dialog.js"></script>
<script type="text/javascript" src="/JS/ZL_Regex.js"></script>
<script type="text/javascript" src="/JS/Plugs/IconSelector.js"></script>
<script src="/JS/ICMS/area.js"></script>
<script src="/JS/Controls/ZL_PCC.js"></script>
<script type="text/javascript">
	var typediag = new ZL_Dialog();
	$(function () {
		ZL_Regex.B_Num('.int');
		var iconsel = new iconSelctor("ClassIcon_T");
	});
	function CheckData() {
	    if (ZL_Regex.isEmpty($("#ClassName_T").val())) { alert("班级名称不能为空!"); return false; }
	    if (ZL_Regex.isEmpty($("#SchoolName_Hid").val())) { alert("请选择学校!"); return false; }
	    return true;
	}
	function ShowSchool() {
		typediag.url = "SelSchool.aspx";
		typediag.title = "选择学校";
		typediag.reload = true;
		typediag.maxbtn = false;
		typediag.width = "width1100";
		typediag.ShowModal();
	}
	function CloseDiag() {
		typediag.CloseModal();
	}
	function GetSchoolName(name) {
	    $("#SchoolName_T").val(name);
	    $("#SchoolName_Hid").val(name);
		typediag.CloseModal();
	}
</script>
</asp:Content>