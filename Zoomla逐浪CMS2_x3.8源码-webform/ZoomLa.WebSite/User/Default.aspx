<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="User_Default" ClientIDMode="Static" %><!DOCTYPE html>
<%@ Register Src="~/User/ASCX/DefaultTop.ascx" TagName="UserMenu" TagPrefix="ZL" %><!DOCTYPE html>
<%@ Register Src="~/Manage/I/ASCX/UserInfoBar.ascx" TagPrefix="ZL" TagName="UserBar" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<meta http-equiv="X-UA-Compatible" content="IE=edge">
<meta name="viewport" content="width=device-width, initial-scale=1.0">
<title>会员中心-<%Call.Label("{$SiteName/}"); %></title>
<link type="text/css" rel="stylesheet" href="/dist/css/bootstrap.min.css" />
<!--[if lt IE 9]>
<script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
<script src="https://oss.maxcdn.com/libs/respond.js/1.3.0/respond.min.js"></script>
<![endif]-->
<link rel="stylesheet" href="/dist/css/font-awesome.min.css"/>
<link type="text/css" rel="stylesheet" href="/App_Themes/User.css" />
<script src="/JS/jquery-1.11.1.min.js"></script>
<script src="/JS/ICMS/ZL_Common.js"></script>
<script src="/dist/js/bootstrap.min.js"></script> 
</head>
<body>
<form id="form1" runat="server">
<ZL:UserMenu runat="server" />
<div class="u_fix_height"></div>
<div class="u_info">
<div class="container">
<div class="row">
<div class="col-lg-2 col-md-2 col-sm-2 col-xs-4 u_face">
<a href="/User/Info/UserInfo.aspx"><asp:Image ID="imgUserPic" AlternateText="" onerror="shownoface(this);" runat="server" /></a>
<ZL:UserBar ID="UserBar_U" runat="server" Width="100" />
</div>
<div class="col-lg-10 col-md-10 col-sm-10 col-xs-8 u_syn">
<ul class="list-unstyled">
<li>
<ul class="list-unstyled">
<li><i class="fa fa-user"></i> 会员名称：<asp:Label ID="UserNameLb" runat="server" Text=""></asp:Label></li>
<li><i class="fa fa-certificate"></i> 等级： <asp:Literal id="LvIcon_Li" runat="server"></asp:Literal> <asp:Label ID="UserLvLb" runat="server" Text=""></asp:Label></li>
<li class="hidden-xs"><i class="fa fa-clock-o"></i> 加入时间：<asp:Label ID="UserRegTimeLb" runat="server" Text=""></asp:Label></li>
</ul>
</li>
<li><i class="fa fa-map-marker"></i> 地址：<asp:Label ID="UserAddressLb" runat="server" Text=""></asp:Label></li>
<li class="hidden-xs"><i class="fa fa-edit"></i> 个性签名：<asp:Label ID="UserSignLb" runat="server" Text=""></asp:Label></li>
<li>
<ul class="list-unstyled">
<li>
<i class="fa fa-usd"></i> 余额：<a href="info/ConsumeDetail.aspx?SType=1" title="点击查看资金明细"><asp:Label ID="UserYeLb" runat="server" Text=""></asp:Label></a>
<a href="/PayOnline/SelectPayPlat.aspx" target="_blank">[在线充值]</a>
<a href="UserFunc/Money/WithDraw.aspx">[申请提现]</a>
</li>
<li class="hidden-xs">银币：<asp:Label ID="UserJbLb" runat="server" Text=""></asp:Label></li>
<li><i class="fa fa-credit-card-alt"></i> 积分：<asp:Label ID="UserJfLb" runat="server" Text=""></asp:Label></li>
</ul>
</li>
</ul>
</div>
</div>
</div>
</div>
<div class="u_site margin_t5 hidden-xs">
<div class="container">
<ol class="breadcrumb">
<li><a href="/User/">会员中心</a></li>
<li class="active">功能引导</li>
</ol>
</div>
</div> 
<div class="container u_menu">
<div class="row padding5">
<asp:Literal ID="UserApp_Li" runat="server" EnableViewState="false"></asp:Literal>
<div class="clearfix"></div>
</div>
</div> 
<div class="u_menu_more text-center">
<a href="javascript:;" id="more_btn" title="点击显示更多">More <i class="fa fa-angle-double-down"></i></a>
</div>
<div class="user_menu_sub"> 
    <ul class="list-unstyled text-center">
        <asp:Literal runat="server" ID="onther_lit" EnableViewState="false"></asp:Literal>
    </ul>
<div class="clearfix"></div>
</div>  
<div class="u_footer text-center fixed_bottom">
<div class="footer_border hidden-xs"></div>
<div class="container">
<%Call.Label("{$Copyright/}"); %>
</div>
</div>
</form>
</body>
</html>
<script> 
$().ready(function (e) {//菜单颜色配置
	var colorArr=new Array('#c47f3e','#669933','#27a9e3','#f05033','#990066','#9999FF','#E48632','#990000','#22afc2','#6633FF','#9900FF','#1FA67A');
	var count=$(".user_menu_sub li").length;
	for(var i=0; i<count; i++){
		$(".user_menu").eq(i).css("background",colorArr[i%12]);	
	}
    if ($(".user_menu_sub li").length < 1)
        $(".u_menu_more").remove();
})
$("#mimenu_btn").click(function(e) { 
	if($(".user_mimenu_left").width()>0){ 
 		$(".user_mimenu_left ul").fadeOut(100);
		$(".user_mimenu_left").animate({width:0},200); 	
	}
	else{ 
		$(".user_mimenu_left").animate({width:150},300); 
		$(".user_mimenu_left ul").fadeIn();
	}
});
//会员菜单更多显示/隐藏
$("#more_btn").click(function(e) {
	if($(".user_menu_sub").css("display")=="none"){  
	    $(".user_menu_sub").slideDown();
		$(this).find("i").removeClass("fa-angle-double-down");
		$(this).find("i").addClass("fa-angle-double-up");
	}
	else {  
	    $(".user_menu_sub").slideUp(200); 
		$(this).find("i").removeClass("fa-angle-double-up");
		$(this).find("i").addClass("fa-angle-double-down");
	}
});
//会员搜索
$("#sub_btn").click(function(e) { 
    if($("#key").val()=="")
		alert("搜索关键字为空!");
	else
		window.location="/User/SearchResult.aspx?key="+escape($("#key").val());	
});
//搜索回车事件
function IsEnter(obj) {
	if (event.keyCode == 13) {
		$("#sub_btn").click(); return false;
	}
}
//判断div元素是否满屏，不满则自动补充高度
//$().ready(function(e) {
//    if ($(".u_footer").position().top + $(".u_footer").outerHeight() < window.innerHeight)
//       $(".u_footer").addClass("navbar-fixed-bottom");
//		$(".u_footer").height(window.innerHeight-$(".u_footer").position().top-3);
//}); 
</script>