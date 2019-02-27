<%@ Page Language="C#" AutoEventWireup="true" validateRequest="false" CodeFile="login.aspx.cs" Inherits="User_login" EnableViewStateMac="false" MasterPageFile="~/Common/Master/Empty.master"%>
<asp:Content runat="server" ContentPlaceHolderID="head">
<link href="/App_Themes/User.css"rel="stylesheet" type="text/css"/>
<title>用户登录-<%:Call.SiteName %></title>
<style type="text/css">
.plat_sp {width:30px;height:30px;margin-right:5px;}
.plat_sp_btn {font-size:32px;color:#EB4E62;}
.plat_sp_btn:hover {color:rgb(10, 164, 231);}
</style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<center style="background: url(<%=GetBKImg()%>); background-position: center; left: 0; top: 0; right: 0; bottom: 0; position: absolute; background-repeat: no-repeat; background-size: cover;">
<div class="user_mimenu">
<div class="navbar navbar-fixed-top" role="navigation">
<button type="button" class="btn btn-default" id="mimenu_btn">
<span class="icon-bar"></span>
<span class="icon-bar"></span>
<span class="icon-bar"></span>
</button>
<div class="user_mimenu_left">
<ul class="list-unstyled">
<li><a href="/" target="_blank">首页</a></li>
<li><a href="/Home" target="_blank">能力</a></li>
<li><a href="/Index" target="_blank">社区</a></li>
<li><a href="/Ask" target="_blank">问答</a></li>
<li><a href="/Guest" target="_blank">留言</a></li>
<li><a href="/Baike" target="_blank">百科</a></li>
<li><a href="/Office" target="_blank">OA</a></li>
</ul>
</div>
<div class="navbar-header">
<button class="navbar-toggle in" type="button" data-toggle="collapse" data-target=".navbar-collapse">
<span class="sr-only">移动下拉</span>
<span class="icon-bar"></span>
<span class="icon-bar"></span>
<span class="icon-bar"></span>
</button>
</div>
</div> 
</div> 
<div class="user_login">
<h1 class="text-center">登录<%:Call.SiteName %>会员</h1>
<ul class="list-unstyled">
<li><i class="fa fa-user"></i>
<asp:TextBox ID="TxtUserName" autofocus="true" placeholder="用户名" onblur="CheckUserCode()" runat="server" CssClass="form-control"></asp:TextBox>
<asp:HyperLink ID="hlReg" runat="server" NavigateUrl="~/User/login.aspx?RegID=1" title="E-mail登录"><span class="fa fa-envelope"></span></asp:HyperLink>
<asp:HyperLink ID="uidReg" runat="server"  NavigateUrl="~/User/login.aspx?RegID=2" title="用户ID登录"><span class="fa fa-align-justify"></span></asp:HyperLink>
</li>
<li><i class="fa fa-lock"></i><asp:TextBox ID="TxtPassword" placeholder="密码" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox></li>
<li><div class="form-group" visible="false" id="trVcodeRegister" runat="server">
<asp:TextBox ID="VCode" placeholder="验证码" MaxLength="6" runat="server" CssClass="form-control codestyle" style="display:inline;width:auto;"></asp:TextBox>
<img id="VCode_img" title="点击刷新验证码" class="codeimg"  style="height:34px;margin-left:2px;width:25%;"/>
<input type="hidden" id="VCode_hid" name="VCode_hid" /></div></li>
<li id="usercode_li" style="display:none;">
<asp:TextBox ID="UserCode_T" runat="server" placeholder="动态口令" CssClass="form-control"></asp:TextBox>
</li>
<li class="margin_top0"><input type="checkbox" name="checkbox" checked="checked" id="checkbox" />记住登录<a href="/User/GetPassword.aspx" id="nopasswd">忘记密码？</a></li>
<li><asp:Button ID="btnLogin" CssClass="btn btn-info" OnClientClick="return ajaxlogin();" runat="server" Text="登 录" onclick="btnLogin_Click" /></li>
<li><a class="btn btn-default" id="reg_btn" href="Register.aspx?<%:Request.QueryString %>" style="color:#fff;">点此注册</a></li>
<li runat="server" id="plat_li">
<div style="color:#fff;">第三方登录：</div>
<div class="margin_t5">
 <span id="qq_span" runat="server" title="腾迅" visible="false" class="plat_sp"><a href="#" runat="server" id="qq_a"><span class="fa fa-qq plat_sp_btn"> </span></a></span>
 <span id="wechat_span" runat="server" title="微信" visible="false" class="plat_sp"><a href="/User/WxLogin.aspx"><span class="fa fa-wechat plat_sp_btn"></span></a></span>
 <asp:LinkButton ID="appSina" Visible="false" ToolTip="新浪" class="plat_sp" runat="server" OnClick="appSina_Click"><span class="fa fa-weibo plat_sp_btn"> </span></asp:LinkButton>
 <%--<asp:LinkButton ID="appBaidu" Visible="false" ToolTip="百度" class="plat_sp" runat="server"  OnClick="appBaidu_Click"><img src="/images/bdidu.png" style="vertical-align:top;" /></asp:LinkButton>--%>
</div>
</li>
</ul>
<div id="main_s" style="display:none">
<ul>
<li style="color:green; font-size:14px; font-weight:bold">欢迎您！登录成功</li>
<li style="margin-top:25px;">系统将在 <span id="sec">3</span> 秒后自动跳转到<a href="MemoView.aspx">会员首页</a></li>
</ul>
</div>
<div class="clearfix"></div>
<div class="login_type_div">
<div><a id="EMail_A" href="login.aspx?RegID=1" runat="server"><span class="fa fa-envelope"></span> 邮箱登录</a></div>
<div><a id="ID_A" runat="server" href="login.aspx?RegID=2"><span class="fa fa-info-circle"></span> ID登录</a></div>
<div><a id="User_A" runat="server" href="login.aspx"><span class="fa fa-user"></span> 用户登录</a></div>
</div>
</div>
</center>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
<script src="/JS/ZL_ValidateCode.js"></script>
<script>
function BtnTj() {
	var username = document.getElementById("TxtUserName");
	var userpass = document.getElementById("TxtPassword");
	var VCode = document.getElementById("VCode");
	if (username.value == "") {
		username.focus();
		return false;
	}
	if (userpass.value == "") {
		userpass.focus();
		return false;
	}
	if (VCode && VCode.value == "") {
		VCode.focus();
		return false;
	}
	return true;
}
</script>
<script type="text/javascript">
$(function () {
	setTimeout("CheckUserCode()", 500);
	$("#VCode").ValidateCode();
})
function CheckUserCode() {
	var bl = true;
	$.ajax({
		url: '/API/UserCheck.ashx',
		data: { action: 'CheckKey', uname: $("#TxtUserName").val() },
		type: 'POST',
		success: function (data) {
			if (data == '1') {
				$("#usercode_li").show();
				bl = false;
			} else {
				$("#usercode_li").hide();
			}
		}
	});
	return bl;
}
//登录
var loginflag = false;
function ajaxlogin() {
	if (BtnTj()) {
		if (!loginflag) {
			$.ajax({
				type: 'POST',
				data: { action: 'login', user: $("#TxtUserName").val(), pwd: $("#TxtPassword").val(), VCode_hid: $("#VCode_hid").val(), vcode: $("#VCode").val(), zncode: $("#UserCode_T").val() },
				success: function (data) {
					if (data != "True") {
						$("#btnLogin").popover({
							animation: true,
							placement: 'left',
							content: '<span style="color:red;"><span class="fa fa-info-circle"></span> ' + data + '!</span> <span style="color:#999">(双击隐藏)</span>',
							html: true,
							trigger: 'manual',
							delay: { show: 10000, hide: 100 }
						});
						$("#btnLogin").popover('show');
						$(".popover").dblclick(function () { $("#btnLogin").popover('destroy'); });
					} else {
						loginflag = true;
						$("#btnLogin").click();
					}
				}
			});
		}
	} else {
		return false;
	}
	return loginflag;
}
$("#mimenu_btn").click(function (e) {
	if ($(".user_mimenu_left").width() > 0) {
		$(".user_mimenu_left ul").fadeOut(100);
		$(".user_mimenu_left").animate({ width: 0 }, 200);
	}
	else {
		$(".user_mimenu_left").animate({ width: 150 }, 300);
		$(".user_mimenu_left ul").fadeIn();
	}
});
</script>
</asp:Content>