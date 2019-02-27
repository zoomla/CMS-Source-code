<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ExamDetail.aspx.cs" Inherits="User_Questions_ExamDetail" EnableViewStateMac="false" ValidateRequest="false" MasterPageFile="~/User/Default.master"%>
<asp:Content runat="server" ContentPlaceHolderID="head">
<script src="/Plugins/Ueditor/ueditor.config.js" charset="utf-8"></script>
<script src="/Plugins/Ueditor/ueditor.all.min.js" charset="utf-8"></script>
<title>评阅试卷</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div id="pageflag" data-nav="edu" data-ban="ke"></div>
<div class="container margin_t5">
	<ol class="breadcrumb">
		<li><a href="/user">用户中心</a></li>
		<li><a href="QuestionManage.aspx">试题管理</a></li>
		<li class="active">评阅试卷</li>
	</ol>
</div>
<div class="container">
<div class="panel panel-primary" ng-app="app">
<div class="panel-heading">
	<i class="fa fa-file-text-o"></i>
	<span class="margin_l5">
		<asp:Label runat="server" ID="PName_L"></asp:Label></span>
	<span><i class="a fa-clock-o"></i><span id="time_sp" runat="server" class="margin_l5" data-time="0"></span></span>
	<div class="uinfo_div">
			<div class="input-group">
				<span class="input-group-addon">学校</span>
				<asp:TextBox runat="server" ID="MySchool_T" Enabled="false" CssClass="form-control text_150" />
			</div>
			<div class="input-group">
				<span class="input-group-addon">班级</span>
				<asp:TextBox runat="server" ID="MyClass_T" Enabled="false" CssClass="form-control text_150" />
			</div>
			<div class="input-group">
				<span class="input-group-addon">姓名</span>
				<asp:TextBox runat="server" ID="UName_T" Enabled="false" CssClass="form-control text_150" />
			</div>
			<div class="clearfix"></div>
		</div>
</div>
<div class="panel-body" ng-controller="appController">
<asp:Repeater runat="server" ID="MainRPT" EnableViewState="false" OnItemDataBound="MainRPT_ItemDataBound">
<ItemTemplate>
<div  style="margin-top:5px;">
<%#ZoomLa.BLL.Helper.StrHelper.ConvertIntegral(Container.ItemIndex+1) +"．"+Eval("QName")+"（有"+Eval("QNum")+"小题,共"+Eval("TotalScore")+"分）" %>
</div>
<div><%#Eval("LargeContent") %></div>
<asp:Repeater runat="server" ID="RPT" EnableViewState="false">
<ItemTemplate>
<div class="item" data-id="<%#Eval("p_id") %>" id="item_<%#Eval("p_id") %>">
<div class="content">
<span><%#Container.ItemIndex+1+"．"+Eval("P_Title") %></span><%#GetContent() %>
</div>
<div class="submit">
<ul class="submitul"><%#GetSubmit() %></ul><div class="clearfix"></div>
</div>
<div class="remark_div margin_t5">
<div class="panel panel-info">
<div class="panel-heading">
<i class="fa fa-file-text-o"></i><span class="margin_l5">教师批阅</span>
<a href="QuestView.aspx?ID=<%#Eval("Qid") %>" target="_blank" class="margin_l5">查看解析</a>
</div>
<div class="panel-body">
<div>
<label><input type="radio" value="1" <%#Eval("IsRight").ToString().Equals("1")?"checked=checked":"" %> name="isright_<%#Eval("ID") %>" />正确</label>
<label><input type="radio" value="2" <%#Eval("IsRight").ToString().Equals("2")?"checked=checked":"" %> name="isright_<%#Eval("ID") %>" />错误</label>
<%#GetScoreHtml() %>
</div>
<div>
<div id="remark_<%#Eval("ID") %>" contenteditable='true' class='answerdiv remark'><%#Eval("Remark") %></div>
</div>
</div>
</div>
</div><!--remark_div end-->
</div>
</ItemTemplate>
</asp:Repeater>
</ItemTemplate>
</asp:Repeater>
</div>
<div class="panel-footer text-center" >
<asp:Button runat="server" ID="Submit_Btn" CssClass="btn btn-primary" Text="提交" OnClick="Submit_Btn_Click" OnClientClick="return PreMark();" />
<a href="Papers_System_Manage.aspx" class="btn btn-primary">返回</a>
</div>
</div>
</div>
<div>
<asp:HiddenField runat="server" ID="QuestDT_Hid" EnableViewState="false" />
<asp:HiddenField runat="server" ID="Answer_Hid" EnableViewState="false" />
<asp:HiddenField runat="server" ID="Remark_Hid" EnableViewState="false" />
</div>
<div id="answer_ue_div" style="display:none;">
<textarea id="editor" style="height: 120px;"></textarea>
<div id="ue_foot" style="text-align:center;padding:5px;">
<input type="button" value="确定" class="btn btn-primary" onclick="LoadContent();" />
<input type="button" value="关闭" class="btn btn-default" onclick="$('#answer_ue_div').hide();" />
</div>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<script src="/Plugins/Ueditor/kityformula-plugin/addKityFormulaDialog.js"></script>
<script src="/Plugins/Ueditor/kityformula-plugin/getKfContent.js"></script>
<script src="/Plugins/Ueditor/kityformula-plugin/defaultFilterFix.js"></script>
<script src="/JS/Controls/ZL_Array.js"></script>
<script src="/JS/jquery-ui.min.js"></script>
<script src="/JS/Plugs/angular.min.js"></script>
<style type="text/css">
label {font-weight:normal;}
.item {border:1px solid #ddd; padding-left:20px;padding-top:5px;text-align:left;}
.opitem:hover {color:#ff6a00;}
.opitem p {display:inline-block;}
.submitul li {float:left;margin-left:20px;}
.answerdiv {border:1px dashed #ddd;height:50px;color:green;width:100%;padding:5px;height:80px;overflow:auto;}
.answersp {border-bottom:1px solid #286090;padding:5px 15px 3px 15px; color:green;}
.answersp p {display:inline;}
#answer_ue_div {width:500px;position:absolute;top:300px;right:30%;border:1px solid #ddd;box-shadow:0 4px 20px 1px rgba(0,0,0,0.2);background:#ffffff;cursor:move;}
</style>
<script>
var page={scope:null,addModel:function(name,model){ 
	page.scope.list[name]=model;
}};
var app=angular.module("app",[]).controller("appController",function($scope,$compile){
	page.scope=$scope;
	$scope.list={};
	var idsArr=[];//仅用于显示答案
	<%=AngularJS%>
	if("<%:Action%>"=="view")
	{
		var answerArr = JSON.parse($("#Answer_Hid").val());
		//model即一个完形填空的问题集合,问题集合-->问题(答案)-->选项
		for (var i = 0; i < idsArr.length; i++) {
			var model=$scope.list["filltextblank_"+idsArr[i]];
			var answer= answerArr.GetByID(idsArr[i],"QID");
			if(!answer||answer==null||answer==""){return;}
			answer=JSON.parse(answer.Answer);
			for (var j = 0; j < model.length; j++) {
				model[j].answer=answer[j].answer;
			}
		}
	}
});
app.filter(
	 'to_trusted', ['$sce', function ($sce) {
		 return function (text) {
			 return $sce.trustAsHtml(text);
		 }
	 }]
 );

var ue, $curAnswer, boundary = "|||", action = "<%=Action%>",exTime= parseInt("<%=ExTime%>"); force = <%=ExTime>0?"true":"false"%>;
$(function () {
	 ue = UE.getEditor('editor', {
		toolbars: [[
			'bold', 'italic', 'underline', '|', 'fontsize', '|', 'kityformula'
		]],
		elementPathEnabled: false,wordCount: false
	 });
	 $("#answer_ue_div").draggable(
		  { handle: '#ue_foot,#edui1_toolbarbox' });
	 $(".remark").click(function () {
		 $curAnswer = $(this);
		 SetContent($curAnswer);
		 $("#answer_ue_div").show();
	 });
})
$(window).scroll(function(){
	var scrollTop = $(this).scrollTop();//已滚动多少
	var nowTop=$("#answer_ue_div").css("top");
	$("#answer_ue_div").css("top",scrollTop+300);
});
//var s={id:1,opts:[{op:"A",value:""}]};
</script>
<script src="/JS/ICMS/ZL_Exam_Paper.js"></script>
</asp:Content>