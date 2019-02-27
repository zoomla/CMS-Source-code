<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddVersion.aspx.cs" Inherits="ZoomLaCMS.BU.Exam.AddVersion" MasterPageFile="~/Common/Master/User.Master" %>
<%@ Register Src="~/Manage/I/ASCX/TreeTlpDP.ascx" TagPrefix="ZL" TagName="TreeTlp" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>添加教材版本</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div class="container margin_t5">
	<ol class="breadcrumb">
		<li><a href="/User">用户中心</a></li>
		<li><a href="VersionList.aspx">教材列表</a></li>
		<li>教材版本</li>
	</ol>
</div>
<div id="pageflag" data-nav="edu" data-ban="ke"></div>
<div class="container">
<div class="input-group" style="width:600px;margin-bottom:10px;">
    <span class="input-group-addon">所属目录</span>
    <asp:Label runat="server" ID="PNode_L" CssClass="form-control text_300 disabled" Text="未指定" disabled="disabled" />
    <span class="input-group-addon">上传人</span>
    <asp:Label runat="server" ID="Inputer_L" CssClass="form-control text_300" disabled="disabled" />
<%--    <span class="input-group-addon">学校</span>
    <asp:Label runat="server" ID="School_L" CssClass="form-control text_md" disabled="disabled"  />--%>
</div>
<table class="table table-bordered table-striped">
	<tr><td style="width:120px;">版本名称：</td><td>
		<asp:TextBox runat="server" ID="VName_T" CssClass="form-control text_300" />
		<asp:RequiredFieldValidator runat="server" ID="RV1" ControlToValidate="VName_T" ForeColor="Red" ErrorMessage="名称不能为空" />
	</td></tr>
	<tr><td>科目：</td><td>
		<ZL:TreeTlp ID="NodeTree" Selected="NodeID_Hid" NodeID="C_id" Pid="C_ClassID" Name="C_ClassName" EmpyDataText="请选择科目!" runat="server" />
		<asp:HiddenField ID="NodeID_Hid" runat="server" /></td></tr>
	<tr><td>版本时间：</td><td><asp:TextBox runat="server" ID="VTime_T" CssClass="form-control text_300" onfocus="WdatePicker({dateFmt:'yyyy'})"/></td></tr>
	<tr><td>册序：</td><td>
		<asp:TextBox runat="server" ID="Volume_T" CssClass="form-control text_300" />
		<div runat="server" id="btnlist" class="btn btn-group">
			<button type="button" class="btn btn-default">上册</button>
			<button type="button" class="btn btn-default">中册</button>
			<button type="button" class="btn btn-default">下册</button>
			<button type="button" class="btn btn-default">全册</button>
		</div>
	   </td></tr>
	<tr><td>年级：</td><td>
	   <asp:RadioButtonList ID="Grade_Radio" runat="server" RepeatDirection="Horizontal" DataValueField="GradeID" DataTextField="GradeName"></asp:RadioButtonList>
	</td></tr>
	<tr><td>标价：</td><td><asp:TextBox runat="server" ID="Price_T" CssClass="form-control text_300 float" Text="0.00"/></td></tr>
    <tr id="Chapter_Tr" runat="server"><td>章(单元)名称：</td><td><asp:TextBox runat="server" ID="Chapter_T" CssClass="form-control text_300"/></td></tr>
    <tbody id="Section_Body" runat="server">
	    <tr><td>节名称：</td><td><asp:TextBox runat="server" ID="SectionName_T" CssClass="form-control text_300"/></td></tr>
	    <tr><td>课名称：</td><td><asp:TextBox runat="server" ID="CourseName_T" CssClass="form-control text_300"/></td></tr>
	    <tr><td>知识点：</td><td>
		    <div id="Examkeyword"></div>
		    <button type="button" onclick="SelKnow()" class="btn btn-primary btn-sm">选择知识点</button>
		    <asp:HiddenField ID="TagKey_T" runat="server" />
	    </td></tr>
    </tbody>
	<tr><td></td><td>
		<asp:Button runat="server" ID="Save_Btn" CssClass="btn btn-primary" Text="确定" OnClick="Save_Btn_Click" />
		<a href="VersionList.aspx" class="btn btn-primary">返回</a>
	</td></tr>
</table>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<script src="/JS/DatePicker/WdatePicker.js"></script>
<script src="/JS/Controls/ZL_Array.js"></script>
<script src="/JS/Controls/ZL_Dialog.js"></script>
<script src="/JS/ZL_Regex.js"></script>
<script src="/JS/OAKeyWord.js"></script>
<script src="/JS/ICMS/ZL_Exam_Controls.js"></script>
<script>
$(function () {
	ZL_Regex.B_Float(".float");
	InitKeyWord($("#TagKey_T").val());
	$("#btnlist button").click(function () {
		$("#Volume_T").val($(this).text());
	});
    //科目改变时初始化知识点
	var oNodeID = "";
	$("#NodeTree ul li").click(function () {
	    var NodeID = $("#NodeID_Hid").val();
	    console.log(NodeID);
	    if (oNodeID != NodeID)
	    {
	        InitKeyWord($("#TagKey_T").val());
	        $("#TagKey_T").val("");
	        oNodeID = NodeID;
	    }
	});
})
var readonly = function () {
    $('#NodeTree button').attr('disabled', 'true');
    $('#VTime_T').removeAttr('onfocus');
}
</script>
</asp:Content>