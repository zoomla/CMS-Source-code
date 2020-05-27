<%@ Page Language="C#" AutoEventWireup="true" CodeFile="welcome.aspx.cs" Inherits="Design_mobile_welcome" MasterPageFile="~/Common/Master/Empty.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>开始-<%:Call.SiteName %>免费手机建站|傻瓜设计和H5页面_逐浪CMS旗下主力平台</title>
<link href="/dist/css/weui.min.css" rel="stylesheet" />
<link href="/design/h5/css/swiper.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div class="welcome">
    <nav class="navbar navbar-default navbar-fixed-top visible-xs mdesign_nav">
<div class="container-fluid">
<!-- Brand and toggle get grouped for better mobile display -->
<div class="navbar-header">
<button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#bs-example-navbar-collapse-1">
<span class="sr-only">Toggle navigation</span>
<span class="icon-bar"></span>
<span class="icon-bar"></span>
<span class="icon-bar"></span>
</button>
<a class="navbar-brand" href="#"><img alt="<%Call.Label("{$SiteName/}");%>" src="<%Call.Label("{$LogoUrl/}");%>" /></a>
<span class="mdesign_nav_t">专业逐浪微站H5开发</span>
</div>

<!-- Collect the nav links, forms, and other content for toggling -->
<div class="collapse navbar-collapse" id="bs-example-navbar-collapse-1">
<ul class="nav navbar-nav">
<li><a href="/design/user/mbsite/default.aspx"><i class="fa fa-laptop"></i> 移动微站</a></li>
<li><a href="/design/mbh5/addscence.aspx"><i class="fa fa-html5"></i> H5微场景</a></li>
<li><a href="/design/ask/#/tab/index"><i class="fa fa-ask"></i> 问卷之星</a></li>
<li><a href="http://www.z01.com/mtv/"><i class="fa fa-film"></i> 免费教程</a></li>
<li><a href="/User/"><i class="fa fa-user"></i> 会员中心</a></li>
</ul>
</div><!-- /.navbar-collapse -->
</div><!-- /.container-fluid -->
</nav>
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
<h1><a href="/">动力版</a><span><a href="http://www.z01.com/" target="_blank"><i class="fa fa-rotate-right fa-rotate-180"></i> 回逐浪官网首页</a></span></h1>
</div>
<div class="navbar-collapse collapse">
<ul class="nav navbar-nav">
<li class="active"><a href="/">首页</a></li>
<li><a href="/design/user/mbsite/default.aspx">免费建站</a></li>
<li><a href="/design/mbh5/addscence.aspx">H5微场景</a></li>
<li><a href="/design/ask/#/tab/index">问卷之星</a></li>
<li><a href="http://www.ziti163.com/webfont">网页字体</a></li>
<li><a href="http://ad.z01.com">广告源码</a></li>
</ul>
<a href="http://doc.z01.com/help/" target="_blank" class="topIco"><i class="fa  fa-question-circle"></i></a>
<ul class="nav navbar-nav ">
<li class="nav_user"><a href="#" class="dropdown-toggle" data-toggle="dropdown"><i class="fa fa-user"></i>登录 <span class="caret"></span></a>
<ul class="dropdown-menu" role="menu">
<li><a href="/User/" target="_blank">用户登录</a></li>
<li><a href="/User/Register.aspx" target="_blank">用户注册</a></li>
<li><a href="/User/Info/UserInfo.aspx" target="_blank">注册信息</a></li>
<li><a href="javascript:;" onclick="LogoutFun();">安全退出</a></li>
</ul>
</li>
</ul>
</div>
</div>
</div>
</div>
<!-- 导航到此结束 -->
<button type="button" class="btn btn-info" id="showActionSheet"><i class="fa fa-align-justify"></i></button>
<div id="actionSheet_wrap">
<div class="weui_mask_transition" id="mask" style="display: none;"></div>
<div class="weui_actionsheet" id="weui_actionsheet">
<div class="weui_actionsheet_menu">
<ul>
<li><a href="/design/user/mbsite/default.aspx"><i class="fa fa-laptop"></i> 移动微站</a></li>
<li><a href="/design/mbh5/addscence.aspx"><i class="fa fa-html5"></i> H5微场景</a></li>
<li><a href="/design/ask/#/tab/index"><i class="fa fa-ask"></i> 问卷之星</a></li>
<li><a href="http://www.z01.com/mtv/"><i class="fa fa-film"></i> 免费教程</a></li>
<li><a href="/User/"><i class="fa fa-user"></i> 会员中心</a></li>
</ul>
</div>
<a id="actionsheet_cancel"></a>
</div>
</div>

<div class="swiper-container">
    <div class="swiper-wrapper">
        <div class="swiper-slide" style="background-image: url(http://v.z01.com/UploadFiles/Images/wxsite01.jpg);">
            <a href="/wxpromo.aspx?r=/design/mbh5/addscence.aspx"></a>
        </div>
        <div class="swiper-slide" style="background-image: url(http://v.z01.com/UploadFiles/Images/wxsite02.jpg);">
            <a href="/Design/h5/preview.aspx?TlpID=77"></a>
        </div>
        <div class="swiper-slide" style="background-image: url(http://v.z01.com/UploadFiles/Images/wxsite03.jpg);">
            <a href="/Class_13/Default.aspx"></a>
        </div>
    </div>
    <!-- 如果需要分页器 -->
    <div class="swiper-pagination swiper-pagination-white"></div>
</div>
<div class="container WXsite_list">
    <p>选取您要建立的微站样式:</p>
    <ul>
        <li class="col-lg-4 col-md-4 col-sm-4 col-xs-4 WXsite_list_li1"><a href="/design/mobile/newsite.aspx?tlpid=1"><i class="fa fa-modx"></i>经典微站</a></li>
        <li class="col-lg-4 col-md-4 col-sm-4 col-xs-4 WXsite_list_li2"><a href="/design/mobile/newsite.aspx?tlpid=2"><i class="fa fa-magic"></i>简洁风格</a></li>
        <li class="col-lg-4 col-md-4 col-sm-4 col-xs-4 WXsite_list_li3"><a href="/design/mobile/newsite.aspx?tlpid=3"><i class="fa fa-gift"></i>微店样式</a></li>
        <li class="col-lg-4 col-md-4 col-sm-4 col-xs-4 WXsite_list_li4"><a href="/design/mobile/newsite.aspx?tlpid=4"><i class="fa fa-picture-o"></i>相册模式</a></li>
        <li class="col-lg-4 col-md-4 col-sm-4 col-xs-4 WXsite_list_li5"><a href="/design/mobile/newsite.aspx?tlpid=5"><i class="fa fa-anchor"></i>文章资讯</a></li>
        <li class="col-lg-4 col-md-4 col-sm-4 col-xs-4 WXsite_list_li6"><a href="/design/mobile/newsite.aspx?tlpid=6"><i class="fa fa-soccer-ball-o"></i>人物展示</a></li>
        <li class="col-lg-4 col-md-4 col-sm-4 col-xs-4 WXsite_list_li7"><a href="/design/mobile/newsite.aspx?tlpid=7"><i class="fa fa-send "></i>图标宫格</a></li>
        <li class="col-lg-4 col-md-4 col-sm-4 col-xs-4 WXsite_list_li8"><a href="/design/mobile/newsite.aspx?tlpid=8"><i class="fa fa-leaf"></i>图文时尚</a></li>
        <li class="col-lg-4 col-md-4 col-sm-4 col-xs-4 WXsite_list_li9"><a href="/Class_370/Default.aspx"><i class="fa fa-plus-circle"></i>更多风格</a></li>
    </ul>
</div>
<div class="h50"></div>
<div class="muser_bottom">
    <ul>
        <li><a href="/design/user/mbsite/default.aspx"><i class="fa fa-gavel"></i>建站</a></li>
        <li><a href="/design/mbh5/addscence.aspx"><i class="fa fa-pencil"></i>场景</a></li>
        <li><a href="/design/ask/#/tab/index"><i class="fa fa-briefcase"></i>问卷</a></li>
        <li><a href="/user/"><i class="fa fa-user"></i>我的</a></li>
        <div class="clearfix"></div>
    </ul>
</div>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
<style>
body{background:#f2f2f2; }
</style>
<script src="/design/h5/js/swiper.min.js"></script>
<script>
    $(function () {
        if ($('.swiper-slide').length <= 1) {
            var mySwiper = new Swiper('.swiper-container', {
                noSwiping: true,
                noSwipingClass: 'stop-swiping',
                autoplay: 0
            });

        } else {
            var mySwiper = new Swiper('.swiper-container', {
                nextButton: '.swiper-button-next',
                prevButton: '.swiper-button-prev',
                pagination: '.swiper-pagination',
                paginationClickable: true,
                autoplay: 5000,
                autoplayDisableOnInteraction: false,
                loop: true,
                grabCursor: true
            });
        }
    });
    $("#showActionSheet").click(function () {
        var mask = $('#mask');
        var weuiActionsheet = $('#weui_actionsheet');
        weuiActionsheet.addClass('weui_actionsheet_toggle');
        mask.show()
			.focus()//加focus是为了触发一次页面的重排(reflow or layout thrashing),使mask的transition动画得以正常触发
			.addClass('weui_fade_toggle').one('click', function () {
			    hideActionSheet(weuiActionsheet, mask);
			});
        $('#actionsheet_cancel').one('click', function () {
            hideActionSheet(weuiActionsheet, mask);
        });
        mask.unbind('transitionend').unbind('webkitTransitionEnd');
    });
    function hideActionSheet(weuiActionsheet, mask) {
        weuiActionsheet.removeClass('weui_actionsheet_toggle');
        mask.removeClass('weui_fade_toggle');
        mask.on('transitionend', function () {
            mask.hide();
        }).on('webkitTransitionEnd', function () {
            mask.hide();
        })
    }
</script>
</asp:Content>