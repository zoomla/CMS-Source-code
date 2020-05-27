<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProjectDetail.aspx.cs" Inherits="ZoomLaCMS.Plat.Blog.ProjectDetail"  MasterPageFile="~/Plat/Empty.master" %>
<asp:Content runat="server" ContentPlaceHolderID="Head">
<title>项目详情</title>
<link type="text/css" rel="stylesheet" href="/Plugins/Uploadify/style.css" />
<link type="text/css" rel="stylesheet" href="/JS/atwho/jquery.atwho.css" />
<script type="text/javascript" src="/Plugins/Uploadify/jquery.uploadify.js"></script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div style="width:600px;">
<div style="background-color:#f7f7f7; width: 100%;height:50px;line-height:50px;padding-left:10px;">
<asp:TextBox runat="server" ID="Name" CssClass="form-control required"></asp:TextBox>
<asp:LinkButton runat="server" ID="Del_Btn" OnClick="Del_Btn_Click" OnClientClick="return confirm('确定要删除吗!!');"> <span class="fa fa-trash-o" title="删除" style="margin-left:10px;"></span> </asp:LinkButton>
</div>
<div style="padding-left:1em; padding-top:10px;">
<table id="DetailTable">
<tr>
  <td><span class="fa fa-calendar"></span> <span>时间： </span></td>
  <td><asp:TextBox class="form-control" ID="StartDate" runat="server" onclick="WdatePicker();" Style="width: 150px;"></asp:TextBox>
	-
	<asp:TextBox class="form-control" ID="EndDate" runat="server" onclick="WdatePicker();" Style="width: 150px;">></asp:TextBox></td>
</tr>
<tr>
  <td colspan="2"><span class="fa fa-user"></span> <a href="javascript:;" target="_blank">
	<asp:Label runat="server" ID="CUser_T"></asp:Label>
	</a> <span style="margin-left:10px;">创建</span></td>
</tr>
<tr>
  <td><span class="fa fa-user"></span> <span>主负责人：</span> <a href="#"><span class="fa fa-plus-circle" title="添加" onclick="selRuser();"></span></a>
	<asp:HiddenField runat="server" ID="LeaderIDS_Hid" /></td>
  <td><asp:Label runat="server" ID="LeaderIDS_L"></asp:Label></td>
</tr>
<tr>
  <td><span class="fa fa-user"></span> <span>任务成员：</span> <a href="#"><span class="fa fa-plus-circle" title="添加" onclick="selCuser();"></span></a>
	<asp:HiddenField runat="server" ID="ParterIDS_Hid" /></td>
  <td><asp:Label runat="server" ID="ParterIDS_L"></asp:Label></td>
</tr>
<tr>
  <td><span class="fa fa-list-alt"></span><span>项目详情：</span></td>
  <td><asp:TextBox runat="server" ID="Describe" CssClass="form-control required date" TextMode="MultiLine" style="max-width:100%;width:100%;height:60px;"></asp:TextBox></td>
</tr>
<tr>
  <td><span class="fa fa-bookmark"></span><span>是否公开：</span></td>
  <td><asp:RadioButtonList runat="server" ID="IsOpen_Rad" RepeatDirection="Horizontal" style="height:40px;">
	  <asp:ListItem Value="1">是</asp:ListItem>
	  <asp:ListItem Value="0">否</asp:ListItem>
	</asp:RadioButtonList></td>
</tr>
<tr>
  <td><span class="fa fa-bookmark"></span><span>是否完成：</span></td>
  <td><asp:RadioButtonList runat="server" ID="IsStatues" RepeatDirection="Horizontal" style="height:40px;">
	  <asp:ListItem Value="0">是</asp:ListItem>
	  <asp:ListItem Value="1">否</asp:ListItem>
	</asp:RadioButtonList></td>
</tr>
<tr>
  <td><span class="fa fa-paperclip" onclick="openUpfile()"></span><span>附件：</span></td>
  <td><asp:Repeater ID="RShowFilelist" OnItemCommand="RShowFilelist_ItemCommand" runat="server">
	  <ItemTemplate>
		<div class="msg_content_attach"> <%#Eval("ExtName") %>
		  <asp:LinkButton runat="server" CommandName="Down" CommandArgument='<%#Eval("Path") %>'><%#Eval("FileName") %></asp:LinkButton>
		</div>
	  </ItemTemplate>
	</asp:Repeater></td>
</tr>
<tr>
  <td colspan="2" runat="server"><asp:Button runat="server" ID="Edit_Btn" Text="保存" OnClick="Edit_Btn_Click" CssClass="btn btn-primary" />
	<input type="button" value="关闭" class="btn btn-default" onclick="parent.HideMe();" /></td>
</tr>
</table>
</div>
</div>
<div style="display: none;" class="hidden_div"> <a href="javascript:;" data-toggle="modal" data-target="#myModal" id="Model_Btn"></a> <a href="javascript:;" data-toggle="modal" data-target="#fileup_div" id="FileUP_Btn"></a> <a href="javascript:;" data-toggle="modal" data-target="#forward_div" id="Forward_Btn"></a>
<asp:HiddenField runat="server" ID="UserInfo_Hid" />
</div>
<div class="modal" id="fileup_div" style="position: fixed; top: 35%;">
<div class="modal-dialog">
<div class="modal-content">
<div class="modal-header">
  <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
  <span class="modal-title" id="FileUP_Title"></span> </div>
<div class="modal-body" style="height: 150px;">
    <ZL:FileUpload runat="server" ID="file_upload_1" />
 <%-- <input type="file" id="file_upload_1" />--%>
  <input type="hidden" runat="server" id="Attach_Hid" name="Attach_Hid" />
</div>
<div class="modal-footer">
  <button type="button" class="btn btn-primary" data-dismiss="modal">关闭</button>
</div>
</div>
</div>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<script type="text/javascript" src="/JS/DatePicker/WdatePicker.js"></script>
<style type="text/css">
.msg_content_attach { display: inline-block; cursor: pointer; margin-top: 2px; margin-bottom: 2px; text-align: center; }
.msg_content_attach div { margin-left: 15px; }
.fa { color: #AFAFAF; font-size: 1.3em; margin-right: 5px; }
.fa:hover { color: #0066cc; }
#DetailTable tr td { padding-top: 10px; padding-bottom: 10px; }
</style>
<script type="text/javascript">
$(function () {
    $("#top_nav_ul li[title='主页']").addClass("active");
	var fileurl = "/Plugins/Uploadify/UploadFileHandler.ashx?value=" +<%=DetailID %>
	$("#file_upload_1").uploadify({
		//按钮宽高
		width: 120,
		height: 35,
		auto: true,
		swf: '/Plugins/Uploadify/uploadify.swf',
		uploader: fileurl,
		buttonText: "上传附件",
		buttonCursor: 'hand',
		fileTypeExts: "*.*",
		fileTypeDesc: "请选择文件",
		fileSizeLimit: "50000KB",
		formData: { "action": "Plat_Project" },
		queueSizeLimit: 1,
		removeTimeout: 2,
		multi: false,
		onUploadStart: function (file) { },
		onUploadSuccess: function (file, data, response) {//暂只允许上传一个文件,后期根据业务需要看是否更改, //json,result,tru||false
			$("#Attach_Hid").val(data);
			$("#Edit_Btn").click();
		},
		onQueueComplete: function (queueData) { },
		onUploadError: function (file) { alert(file.name + "上传失败"); }
	});
});
function openUpfile() {
	var d = $("#FileUP_Btn").click();
}
function selRuser() {
	parent.ChildSelRuser();
}
function selCuser() {
	parent.ChildSelCuser();
}
</script>
</asp:Content>
