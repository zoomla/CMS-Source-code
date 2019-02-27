<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Design_ask_Default" %><!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<meta name="viewport" content="initial-scale=1, maximum-scale=1, user-scalable=no, width=device-width" />
<link rel="stylesheet" type="text/css" href="/Plugins/ionic/css/ionic.css"/>
<link rel="stylesheet" type="text/css" href="/dist/css/font-awesome.min.css"/>
<link rel="stylesheet" type="text/css" href="/dist/css/bootstrap.min.css" />
<link rel="stylesheet" type="text/css" href="/Design/ask/js/global.css"/>
<script src="/JS/jquery-1.11.1.min.js"></script>
<script src="/dist/js/bootstrap.min.js"></script>
<script src="/JS/Modal/APIResult.js?v=<%:Ver %>"></script>
<script src="/JS/ZL_Regex.js"></script>
<script src="/JS/Controls/ZL_Array.js?v=<%:Ver %>"></script>
<script src="/JS/Plugs/moment.js"></script>
<script src="/Plugins/ionic/js/ionic.bundle.min.js"></script>
<script src="/design/ask/js/app.js?v=<%:Ver %>"></script>
<script src="/design/ask/js/controllers.js?v=<%:Ver %>"></script>
<script src="/design/ask/js/services.js?v=<%:Ver %>"></script>
<script>
    var cfg = { scope: null };
</script>
<title>动力逐浪微问卷系统</title>
</head>
<body ng-app="starter">
<div class="relative hidden-xs">
<div class="navbar navbar-default navbar-fixed-top" role="navigation" id="home_scolls">
<div class="container">
<div class="navbar-header">
<button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
<span class="sr-only">智能切换导航</span>
<span class="icon-bar"></span>
<span class="icon-bar"></span>
<span class="icon-bar"></span>
</button>
<a class="navbar-brand" href="/"><img alt="<%Call.Label("{$SiteName/}");%>" src="<%Call.Label("{$LogoUrl/}");%>" /></a>
</div>
<div class="navbar-collapse collapse">
<ul class="nav navbar-nav">
<li><a href="/">首页</a></li>
<li><a href="/Design/Mobile/Welcome.aspx">免费建站</a></li>
<li><a href="/design/ask/#/tab/index">问卷之星</a></li>
<li><a href="/design/h5/">H5创作</a></li>
<li><a href="http://www/ziti163.com/webfont" target="_blank">网页字体</a></li>
<li><a href="http://ad.z01.com" target="_blank">广告源码</a></li>
</ul>
<a href="http://doc.z01.com/help/" target="_blank" class="topIco"><i class="fa  fa-question-circle"></i></a>
<ul class="nav navbar-nav ">
<li class="nav_user"><a href="#" class="dropdown-toggle" data-toggle="dropdown"><%Call.Label("{ZL:GetuserName()/}");%>↓</a>
<ul class="dropdown-menu" role="menu">
<li><a href="/User/" target="_blank">会员中心</a></li>
<li><a href="/User/Info/UserInfo.aspx" target="_blank">注册信息</a></li>
<li><a href="javascript:;" onclick="LogoutFun();">安全退出</a></li>
</ul>
</li>
</ul>
</div>
</div>
</div>
</div>

<form id="form1" runat="server">
<ion-side-menus><ion-side-menu-content drag-content="false"><ion-nav-view></ion-nav-view><div ng-controller="IndexCtrl"></div></ion-side-menu-content></ion-side-menus>
</form>
</body>
</html>
