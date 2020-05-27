﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Drafting.aspx.cs" Inherits="MIS_ZLOA_Drafting" ValidateRequest="false" MasterPageFile="~/Plat/Main.master"  %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<style type="text/css">
.drag { z-index: 999; width: 60px; height: 1px; position: relative; }/*设为1,这样下方不会有错位*/
.curDrag { z-index: 999; display: none; }
.edui-default { margin: auto; }
input[type=file] { display: inline-block; }
</style>
<script type="text/javascript" charset="utf-8" src="/Plugins/Ueditor/ueditor.config.js"></script>
<script type="text/javascript" charset="utf-8" src="/Plugins/Ueditor/ueditor.all.min.js"></script>
<title>公文起草</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<%--<ol class="breadcrumb">
<li><a href="/Plat/Blog/">能力中心</a></li>
<li><a href="/Plat/OA/">OA办公</a></li>
<li><a href="Drafting.aspx">公文拟稿</a></li>
</ol>--%>
<div class="container platcontainer">
<table class="table table-bordered" style="margin-top:10px;">
  <tr>
	<td class="text-right" style="width: 140px;">密 级：</td>
	<td><asp:DropDownList ID="Secret" runat="server"></asp:DropDownList></td>
	<td class="text-right" style="width: 120px;">紧急程度：</td>
	<td><asp:DropDownList ID="Urgency" runat="server"></asp:DropDownList></td>
	<td class="text-right" style="width: 100px;">重要程度：</td>
	<td><asp:DropDownList ID="Importance" runat="server"></asp:DropDownList></td>
	<td class="text-right" style="width: 100px;">起草部门：</td>
	<td><asp:Label ID="Label2" runat="server" /></td>
  </tr>
  <tr>
	<td class="text-right" style="width: 140px;">标 题：</td>
	<td colspan="1" style="width: 300px;"><asp:TextBox ID="Title" runat="server" Width="200" CssClass="form-control" />
	  <span class="required">*</span></td>
	<td class="text-right">主题词：</td>
	<td colspan="3"><%-- <div id="OAkeyword" style="max-width: 300px;"></div><div class="clearfix"></div>--%>
	  <asp:TextBox ID="Keywords" runat="server" CssClass="form-control"></asp:TextBox></td>
	<td class="text-right" style="width: 100px;">起草日期：</td>
	<td><asp:TextBox ID="CreateTime" Width="300" runat="server" onclick="WdatePicker({ dateFmt: 'yyyy/MM/dd HH:mm:ss' });" CssClass="form-control"></asp:TextBox></td>
  </tr>
  <tr>
	<td class="text-right" style="width: 140px;">起草人：</td>
	<td colspan="1"><asp:Label ID="Label1" runat="server" /></td>
	<td class="text-right">文件模板：</td>
	<td colspan="3"><asp:DropDownList ID="Type" runat="server" onchange="PostToCS();"> </asp:DropDownList></td>
	<td class="text-right" style="width: 100px;">流 程：</td>
	<td><asp:DropDownList runat="server" ID="proDP" Width="80"></asp:DropDownList>
	  <span class="required">*</span></td>
  </tr>
  <tr>
	<td colspan="8" id="contentTr"><div style="width: 815px; margin: auto;">
		<asp:TextBox ID="Content" name="Content" TextMode="MultiLine" Height="300" Width="100%" runat="server" ClientIDMode="Static"></asp:TextBox>
		<div class="curDrag" style="height: 1px;"> <img src="#" /></div>
	  </div></td>
  </tr>
  <tr>
	<td style="width: 140px; text-align: right; padding: 5px; height: 30px;"><button type="button" name="selruser" id="selruser" class="btn-primary" onclick="selRuser();">选择主办人</button></td>
	<td colspan="7"><asp:Label runat="server" ID="RUserName_Lab" Style="height: 60px; word-wrap: break-word;"></asp:Label>
	  <asp:HiddenField runat="server" ID="RUserID_Hid" /></td>
  </tr>
  <tr>
	<td style="width: 140px; text-align: right; padding: 5px; height: 30px;"><button type="button" name="selcuser" id="selcuser" class="btn-primary" onclick="selCuser();">选择协办人</button></td>
	<td colspan="7"><asp:Label runat="server" ID="CUserName_Lab" Style="height: 60px;"></asp:Label>
	  <asp:HiddenField runat="server" ID="CUserID_Hid" /></td>
  </tr>
  <tr id="hasFileTR" runat="server" visible="true">
	<td class="text-right" style="width: 140px;">已上传文件：
	  <asp:HiddenField runat="server" ID="hasFileData" ClientIDMode="Static" /></td>
	<td colspan="7" id="hasFileTD" runat="server"></td>
  </tr>
  <tr id="signTr" runat="server">
	<td class="text-right">签 章：</td>
	<td colspan="7"><asp:RadioButtonList runat="server" ID="signRadio" RepeatDirection="Horizontal"> </asp:RadioButtonList>
	  <span runat="server" id="signTrRemind" visible="false">你尚未配置个人签章！</span></td>
  </tr>
  <tr runat="server" id="upFileTR">
	<td style="text-align: right;"><input type="button" class="btn btn-primary" value="再加一个附件" onclick="addAttach();" /></td>
	<td colspan="7"><table id="attachTB">
		<tr>
		  <td><input type="file" name="fileUP" class="fileUP" /></td>
		</tr>
	  </table></td>
  </tr>
  <tr>
	<td class="text-right">操 作：</td>
	<td colspan="7"><asp:Button runat="server" CssClass="btn-primary" ID="saveBtn" Text="发布" OnClick="saveBtn_Click" OnClientClick="return checkF();" />
	  <asp:Button runat="server" CssClass="btn-primary" ID="AddNewBtn" Text="添加为新公文" OnClick="AddNewBtn_Click" Style="margin-left: 10px;" />
	  <asp:Button runat="server" CssClass="btn-primary" ID="DraftBtn" Text="草稿" OnClick="DraftBtn_Click" Style="margin-left: 10px;" />
	  <asp:Button runat="server" CssClass="btn-primary" ID="clearBtn" Text="重置" OnClick="clearBtn_Click" Style="margin-left: 10px;" />
	  <button type="button" class="btn-primary" style="margin-left: 10px" onclick="printpage();">打印</button></td>
  </tr>
</table>
</div>
<div id="select" style="position: absolute; display: none; width: 800px; background: #FFF; border: 1px solid #ddd; top: 20%; left: 20%; z-index: 99999;">
<div id="Free_Div" runat="server" class="panel panel-primary">
  <div class="panel-heading">
	<h3 class="panel-title" style="text-align: center; width: 760px; float: left;">自由流程,请选择需要投递的用户</h3>
	<span style="position:absolute;top:13px;font-size:1.2em; cursor: pointer;" title="关闭" onclick="closesel();" class="fa fa-remove"></span>
	<div class="clearfix"></div>
  </div>
  <div class="panel-body">
	<iframe runat="server" id="User_IFrame" style="visibility: inherit; overflow: auto; overflow-x: hidden; width: 800px; height: 430px;" name="main_right" src="/Plat/Common/SelUser.aspx?Type=AllInfo" frameborder="0"></iframe>
  </div>
</div>
</div>
<asp:HiddenField runat="server" ID="curPosD" Value="0|0" />
<script type="text/javascript">
        $(function () {
            $("#top_nav_ul li[title='办公']").addClass("active");
        })
        setactive("办公");
		function checkF() {
			var flag = true;
			if ($("#Title").val() == "") {
				flag = false;
				alert("标题不能为空!!!");
			}
			if ($("#proDP").val() == -1 && flag) {
				confirm("请先选择流程!!!");
			}
			if ($("#RUserID_Hid").val() == "" && flag) {
				flag = false;
				alert("尚未选择主办人!!!")
			}
			return flag;
		}
		var uptr = '<tr><td><input type="file" name="fileUP" class="fileUP" /><input type="button" value="删除" onclick="delAttach(this);" class="btn btn-sm btn-primary" /></td></tr>';
		var id = '<%= Server.HtmlEncode(Request.QueryString["appID"])%>';
		if (id != "") {
			var keys = "<%=key%>";
		if (keys = "") {
			keys = keys.split(',');
			for (var i = 0; i < keys.length; i++) {
				$("#OAkeyword").append("<span name='tab' class='radius'>" + keys[i] + "<a class='deltab' onclick='closediv(this)'>×</a></span>")
			}
		}
	}
	function addAttach() {
		$("#attachTB").append(uptr);
	}
	function delAttach(obj) {
		$(obj).parent().remove();
	}
	function delHasFile(v, obj) {
		rv = $("#hasFileData").val().replace(v, "");
		$("#hasFileData").val(rv)
		$(obj).parent().remove();
	}
	function preView() {
		var winname = window.open('', "_blank", '');
		winname.document.open('text/html', 'replace');
		winname.opener = null;
		winname.document.write($("#Content").val());
		winname.document.close();
	}
	<%= GetEditor("Content")%>
		function disWin() {
			var iTop = (window.screen.availHeight - 30 - 550) / 2;
			var iLeft = (window.screen.availWidth - 10 - 960) / 2;
			var myWin = window.open('/Mis/OA/PreViewProg.aspx?proID=<%:proDP.SelectedValue%>', 'newwindow', 'height=550, width=960,top=' + iTop + ',left=' + iLeft + ',toolbar=no, menubar=no, scrollbars=no, resizable=no, location=no, status=no');
	}
	$(function () {
		$("#OAkeyword").tabControl({ maxTabCount: 5, tabW: 80 });
		$("#saveBtn").click(function () {
			var v = $("#OAkeyword").getTabVals();
			$("#Keywords").val(v.join(","));
		});
		$("#AddNewBtn").click(function () {
			var v = $("#OAkeyword").getTabVals();
			$("#Keywords").val(v.join(","));
		});
		$("#DraftBtn").click(function () {
			var v = $("#OAkeyword").getTabVals();
			$("#Keywords").val(v.join(","));
		});
	});
	function closediv(obj) {
		if (confirm("确定删除该标签？")) {
			$(obj).parent().remove();
		}
	}
	//---------签章
	//为radio绑定单击事件
	$().ready(function () {
		$("#signRadio :radio:not(:first)").click(function () {
			$(".curDrag").show();
			$(".curDrag img").attr("src", $(this).parent().attr("picUrl"));
			var img = $(".curDrag img"); //获取img元素
			$(".curDrag").css("width", img.width());//设置宽度
		});//click end;
		$("#signRadio :radio:first").click(function () {
			$(".curDrag").hide();
			$("#curPosD").val("0|0");
		});//click end;

		$(".curDrag").draggable
			({
				addClasses: false,
				axis: false,
				cursor: 'crosshair',
				//start: function () { alert(12);},
				//drag: function (){},
				stop: function () { GetPos(); },
				containment: 'parent'
			});//dragable end;
	});
	function GetPos() {
		//无时为auto:auto
		obj = $($(".curDrag")[0]);
		x = obj.css("top");
		y = obj.css("left");
		$("#curPosD").val(x + "|" + y);
	}
	//ID:x|y
	function InitPos(v) {
		var arr = v.split(':');
		$("#curPosD").val(arr[1]);
		pic = $("#signRadio :radio[value='" + arr[0] + "']").parent().attr("picUrl");
		$(".curDrag img").attr("src", pic);
		$(".curDrag").css("top", arr[1].split('|')[0]);
		$(".curDrag").css("left", arr[1].split('|')[1]);
		$(".curDrag").show();
	}
	function PostToCS() {
		v = $("#Type").val();
		if (v == "0") {
			editor.setContent("", false); return;
		}
		var target = document.getElementById('foo');
		var spinner = new Spinner().spin(target);
		$.ajax({
			type: "Post",
			url: "Drafting.aspx",
			data: { value: v },
			success: function (data) {
				spinner.stop();
				editor.setContent(data, false);
			},
			error: function () { spinner.stop(); }
		});
	}
	function printpage() {
		var img = $(".curDrag img").attr("src");
		var con = $("#Content").val();
		$.ajax({
			type: "Post",
			url: "Drafting.aspx",
			data: { Action: "Print", Content: con, Image: img },
			success: function (data) { if (data == 1) { window.open("/Common/PrintPage.aspx") } },
			error: function () { alert("打印失败"); }
		});
	}
	function UserFunc(json, select) {
		var uname = "";
		var uid = "";
		for (var i = 0; i < json.length; i++) {
			uname += json[i].UserName + ",";
			uid += json[i].UserID + ",";
		}
		if (uid) uid = uid.substring(0, uid.length - 1);
		if (select == "ReferUser") {
			$("#RUserName_Lab").text(uname);
			$("#RUserID_Hid").val(uid);
		}
		if (select == "CCUser") {
			$("#CUserName_Lab").text(uname);
			$("#CUserID_Hid").val(uid);
		}
		$("#select").hide();
	}
	function closesel() {
		$("#select").hide();
	}
	function selRuser() {
		$("#select").css("margin-top", $(document).scrollTop());
		$("#User_IFrame").attr("src", "/Plat/Common/SelUser.aspx?Type=AllInfo#ReferUser");
		//$("#User_IFrame")[0].contentWindow.ClearChk();//清除checkbox
		$("#select").show();
	}
	function selCuser() {
		$("#select").css("margin-top", $(document).scrollTop());
		$("#User_IFrame").attr("src", "/Plat/Common/SelUser.aspx?Type=AllInfo#CCUser");
		//$("#User_IFrame")[0].contentWindow.ClearChk();
		$("#select").show();
	}
	$(".edui-default").height = "300px;";
	</script>
<div id="foo" style="position: absolute; top: 50%; left: 50%; display: block;"></div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<script type="text/javascript" src="/JS/jquery-ui.min.js"></script>
<script type="text/javascript" src="/JS/DatePicker/WdatePicker.js"></script>
<script type="text/javascript" src="/Plugins/JqueryUI/spin/spin.js"></script>
</asp:Content>