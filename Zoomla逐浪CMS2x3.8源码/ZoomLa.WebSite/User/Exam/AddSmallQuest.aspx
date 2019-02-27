<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddSmallQuest.aspx.cs" MasterPageFile="~/User/Empty.master" ValidateRequest="false" Inherits="User_Exam_AddSmallQuest" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>添加小题</title>
<script src="/Plugins/Ueditor/ueditor.config.js" charset="utf-8"></script>
<script src="/Plugins/Ueditor/ueditor.all.min.js" charset="utf-8"></script>
<script src="/Plugins/Ueditor/kityformula-plugin/addKityFormulaDialog.js"></script>
<script src="/Plugins/Ueditor/kityformula-plugin/getKfContent.js"></script>
<script src="/Plugins/Ueditor/kityformula-plugin/defaultFilterFix.js"></script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div id="pageflag" data-nav="edu" data-ban="ke"></div>
<div class="container">
 <table class="table table-striped table-bordered table-hover tab-content">
	 <tbody>
		 <tr>
		 <td class="td_m text-right">题型:</td>
		 <td>
			 <label><input type="radio" name="qtype_rad" value="0" checked="checked" />单选题 </label>
				<label><input type="radio" name="qtype_rad" value="1" />多选题 </label>
				<label><input type="radio" name="qtype_rad" value="2" />填空题 </label>
				<label><input type="radio" name="qtype_rad" value="3" />解析题 </label>
		 </td>
		 </tr>
		 <tr>
			 <td class="text-right">试题题干:</td>
			 <td>
				 <asp:TextBox ID="txtP_Content" runat="server" Style="height: 200px; width: 100%;" TextMode="MultiLine"></asp:TextBox>
				<%=Call.GetUEditor("txtP_Content",2) %>
			 </td>
		 </tr>
	 </tbody>
	 <tbody id="Options_body">
		 <tr>
		 <td class="text-right">选项信息:</td>
		 <td>
			 <span></span>
				<asp:DropDownList ID="ddlNumber1" CssClass="form-control text_x" onchange="AddOption($(this).val());" runat="server">
					<asp:ListItem Value="0">0</asp:ListItem>
					<asp:ListItem Value="1">1</asp:ListItem>
					<asp:ListItem Value="2">2</asp:ListItem>
					<asp:ListItem Value="3">3</asp:ListItem>
					<asp:ListItem Value="4">4</asp:ListItem>
					<asp:ListItem Value="5">5</asp:ListItem>
				</asp:DropDownList>
				<div>
					<ul id="option_ul"></ul>
				</div>
				<asp:Literal ID="Tips" runat="server" Visible="false"></asp:Literal>
				<div id="optionDiv" runat="server" visible="false"></div>
		 </td>
		 </tr>
		 <tr>
			 <td class="text-right">正确答案:<span class="rd_green">(仅用于自动改卷)</span></td>
			 <td>
				<asp:TextBox runat="server" ID="Answer_T" CssClass="form-control text_300"></asp:TextBox>
				<span class="r_green_x">如有多个答案用|号分隔,用于支持自动批阅试卷,单选:A,多选:A|B,填空:值1|值2|值3</span>
			</td>
		 </tr>
		 <tr>
			<td>正确答案：<span class="rd_green">(教师与学生可见)</span></td>
			<td>
				<asp:TextBox runat="server" ID="AnswerHtml_T" TextMode="MultiLine" Style="width: 100%; height: 200px;"></asp:TextBox>
				<%=Call.GetUEditor("AnswerHtml_T",2) %>
			</td>
		</tr>
	 </tbody>
	 <tr><td colspan="2" class="text-center">
			<asp:HiddenField ID="Optioninfo_Hid" runat="server" />
			<asp:Button ID="EBtnSubmit" class="btn btn-primary" Text="保存试题" OnClientClick="return CheckData();" OnClick="EBtnSubmit_Click" runat="server" />
			<a href="javascript:;" class="btn btn-primary">返回试题</a>
		</td></tr>
 </table>
<asp:HiddenField id="txtP_title_Hid" runat="server" />
<asp:HiddenField ID="rblDiff_Hid" runat="server" />
<asp:HiddenField ID="NodeID_Hid" runat="server" />
<asp:HiddenField ID="p_Views_Hid" runat="server" />
<asp:HiddenField ID="Tagkey_Hid" runat="server" />
<asp:HiddenField ID="IsShare_Hid" runat="server" />
<asp:HiddenField ID="Jiexi_Hid" runat="server" />
<asp:HiddenField ID="Version_Hid" runat="server" />
<asp:HiddenField ID="p_defaultScores_Hid" runat="server" />
<%--<asp:HiddenField ID="p_defaultScores_Hid" runat="server" />--%>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<style type="text/css">
#mymodal1 .modal-dialog{margin-top:7%;}
#mymodal1 .modal-dialog iframe{height:600px!important;}
#option_ul li{margin-top:3px;}
.tabinput{ border:none; padding-left:5px; height:30px; line-height:30px;}
.radius{margin:2px;margin-top:20px;height:24px; line-height:24px; background:#eee; border:1px solid #ddd;border-radius:3px;padding:3px;float:none!important;}
#errormes{z-index:9999;}
</style>
<script src="/dist/js/bootstrap-switch.js"></script>
<script src="/JS/Controls/ZL_Dialog.js"></script>
<script src="/JS/ICMS/ZL_Exam_Paper.js"></script>
<script src="/JS/SelectCheckBox.js"></script>
<script src="/JS/Controls/ZL_Array.js"></script>
<script src="/JS/Plugs/angular.min.js"></script>
<script src="/JS/ZL_Regex.js"></script>
<script src="/JS/ICMS/ZL_Exam_Question.js"></script>
<script>
	$(function () {
		ZL_Regex.B_Num(".int");
		InitData();
	})
	function InitData() {
		var $parent = $(parent.document);
		$("#txtP_title_Hid").val($parent.find("#txtP_title").val());//标题
		$("#rblDiff_Hid").val($parent.find("#rblDiff input[type='radio']:checked").val());//难度
		$("#NodeID_Hid").val($parent.find("#NodeID_Hid").val());//父节点
		$("#p_Views_Hid").val($parent.find("#Grade_Radio input[type='radio']:checked").val());//年级
		var keywords = "";
		$parent.find("[name='tabinput']").each(function (i, v) {
			keywords += $(this).val();
			if (i < $parent.find("[name='tabinput']").length - 1) { keywords += ",";}
		});
		$("#Tagkey_Hid").val(keywords);//关键字
		$("#IsShare_Hid").val($parent.find("#IsShare_Chk:checked").length);//是否分享
		$("#Jiexi_Hid").val($parent.find("#txtJiexi").val());//解析
		$("#p_defaultScores_Hid").val($parent.find("#txtDefaSocre").val());
		$("#Version_Hid").val($parent.find("#Version_Rad input[type=radio]:checked").val());
	}
	function CheckData() {
		GetOptions();
		return true;
	}
</script>
</asp:Content>