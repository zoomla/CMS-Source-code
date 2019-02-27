<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddVersion.aspx.cs" Inherits="Manage_Exam_AddVersion" MasterPageFile="~/Manage/I/Default.master" %>
<%@ Register Src="~/Manage/I/ASCX/TreeTlpDP.ascx" TagPrefix="ZL" TagName="TreeTlp" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>添加教材版本</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<table class="table table-bordered table-striped">
	<tr><td class="td_m">版本名称：</td><td>
		<asp:TextBox runat="server" ID="VName_T" CssClass="form-control text_300" />
		<asp:RequiredFieldValidator runat="server" ID="RV1" ControlToValidate="VName_T" ForeColor="Red" ErrorMessage="名称不能为空" />
	</td></tr>
    <tr><td>上传人：</td><td><asp:TextBox runat="server" ID="Inputer_T" CssClass="form-control text_300" /></td></tr>
    <tr><td>科目：</td><td>
		<ZL:TreeTlp ID="NodeTree" Selected="Node_Hid" NodeID="C_id" Pid="C_ClassID" Name="C_ClassName" EmpyDataText="请选择科目!" runat="server" />
		<asp:HiddenField ID="Node_Hid" runat="server" /></td></tr>
	<tr><td>版本时间：</td><td><asp:TextBox runat="server" ID="VTime_T" CssClass="form-control text_300" onfocus="WdatePicker({dateFmt:'yyyy'})"/></td></tr>
	<tr><td>册序：</td><td>
        <asp:TextBox runat="server" ID="Volume_T" CssClass="form-control text_300" />
        <div id="btnlist" class="btn btn-group">
            <button type="button" class="btn btn-default">上册</button>
            <button type="button" class="btn btn-default">中册</button>
            <button type="button" class="btn btn-default">下册</button>
            <button type="button" class="btn btn-default">全册</button>
        </div>
	   </td></tr>
    <tr><td>年级：</td><td>
	   <asp:RadioButtonList ID="Grade_Radio" runat="server" RepeatDirection="Horizontal" DataValueField="GradeID" DataTextField="GradeName"></asp:RadioButtonList>
	</td></tr>
    <tr><td>标价：</td><td><asp:TextBox runat="server" ID="Price_T" CssClass="form-control text_300 float" Text="0.00"></asp:TextBox></td></tr>
    <tr><td>节名称：</td><td><asp:TextBox runat="server" ID="SectionName_T" CssClass="form-control text_300"></asp:TextBox></td></tr>
    <tr><td>课名称：</td><td><asp:TextBox runat="server" ID="CourseName_T" CssClass="form-control text_300"></asp:TextBox></td></tr>
	<tr><td>知识点：</td><td>
        <div id="Examkeyword"></div>
        <button type="button" onclick="SelKnow()" class="btn btn-primary btn-sm">选择知识点</button>
        <asp:HiddenField ID="TagKey_T" runat="server" />
    </td></tr>
	<tr><td></td><td>
		<asp:Button runat="server" ID="Save_Btn" CssClass="btn btn-primary" Text="确定" OnClick="Save_Btn_Click" />
		<a href="VersionList.aspx" class="btn btn-primary">返回</a>
	</td></tr>
</table>
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
    })
</script>
</asp:Content>