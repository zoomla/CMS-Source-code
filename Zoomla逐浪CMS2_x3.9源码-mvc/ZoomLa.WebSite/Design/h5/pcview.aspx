<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="pcview.aspx.cs" Inherits="ZoomLaCMS.Design.h5.pcview" %>
<!DOCTYPE html>
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<meta http-equiv="X-UA-Compatible" content="IE=edge" />
<meta name="viewport" content="width=device-width, initial-scale=1.0" />
<link type="text/css" rel="stylesheet" href="/dist/css/bootstrap.min.css" />
<!--[if lt IE 9]>
<script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
<script src="https://oss.maxcdn.com/libs/respond.js/1.3.0/respond.min.js"></script>
<![endif]-->
<link rel="stylesheet" href="/dist/css/font-awesome.min.css"/>
<script src="/JS/jquery-1.11.1.min.js"></script>
<script src="/dist/js/bootstrap.min.js"></script>
<script src="/JS/ICMS/ZL_Common.js"></script>
<script src="/JS/ZL_Regex.js"></script>
<link type="text/css" rel="stylesheet" href="/App_Themes/Guest.css" />  
<link href="/App_Themes/User.css"rel="stylesheet" type="text/css"/>
<link rel="stylesheet" href="/design/res/css/comp.css" />
<link rel="stylesheet" href="/design/res/css/preview.css"/>
<link rel="stylesheet" href="/design/h5/css/swiper.min.css">
<link rel="stylesheet" href="/design/h5/css/animate.min.css">
<style type="text/css">
.scence.swiper-container {height:1040px;transform:scale(0.5);position:absolute;top:-89px;left:-127px;background-color:#fff;}/*在浏览器强制为1040用于模拟演示*/
.pcview_bg{background-position:center;background-repeat:no-repeat; background-size:cover;left: 0px; top: 0px; right: 0px; bottom: 0px; position: fixed;}
</style>
<script src="/design/h5/js/swiper.min.js"></script>
<script src="/design/h5/js/swiper.animate.min.js"></script>
<script>
var phoneWidth = parseInt(window.screen.width),phoneHeight = parseInt(window.screen.height);
var phoneScale = phoneWidth / 640;
document.write('<meta name="viewport" content="width=640, minimum-scale = ' + phoneScale + ', maximum-scale = ' + phoneScale + '">');
</script>
<title><asp:Literal runat="server" ID="Title_L"></asp:Literal></title>
</head>
<body>
<div class="pcview_bg">
<form>
<div class="user_mimenu">
<div class="navbar navbar-fixed-top" role="navigation">
<button type="button" class="btn btn-default" id="mimenu_btn">
<span class="icon-bar"></span>
<span class="icon-bar"></span>
<span class="icon-bar"></span>
</button>
<div class="user_mimenu_left">
<ul class="list-unstyled">
<li class="active"><a href="http://v.z01.com/">首页</a></li>
<li><a href="http://v.z01.com/jz">免费建站</a></li>
<li><a href="http://v.z01.com/Class_2/Default.aspx">模板中心</a></li>
<li><a href="http://v.z01.com/h5">H5创作</a></li>
<li><a href="http://www.ziti163.com/webfont/">网页字体</a></li>
<li><a href="http://ad.z01.com">广告源码</a></li>
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
<div style="display:none;">
    <asp:Image ID="Wx_Img" runat="server" />
</div>
<div class="pcview" ng-app="app" ng-controller="appCtrl">
    <div class="pc_mobile" style="display:inline-flexbox;">
        <div class="tit_div"><asp:Label ID="Tit_L" runat="server" /></div>
        <div class="pc_cmsfont"><img src="<%=Call.LogoUrl %>" alt="<%:Call.SiteName %>" /></div>
        <div class="swiper-container scence">
            <div id="editorBody" class="swiper-wrapper">
                <section ng-repeat="scence in scence.list" class="swiper-slide" id="section_{{scence.id}}">
                    <div id="mainBody{{scence.id}}" class="compbody"></div>
                </section>
            </div>
            <div class="swiper-pagination"></div>
            <img src="/design/h5/images/arrow.png" id="array" class="resize">
            <div class="progbar"><span class="bar"></span><div class="sindex"><span class="index">0</span> / <span class="num">0</span></div></div>
        </div>
    </div>
    <div class="ctrl_panel">
        <div id="pre_page" class="cbtn pre_btn" ng-click="pre();">上一页</div>
        <div id="next_page" class="cbtn" ng-click="next();">下一页</div>
    </div>
    <div class="code_div">
        <div class="title_div">扫描并分享场景：</div>
        <div class="img_div">
            <img src="/common/common.ashx?url=<%=ZoomLa.Components.SiteConfig.SiteInfo.SiteUrl+"/design/h5/preview.aspx?id="+pageMod.guid%>" />
        </div>
        <div class="bdsharebuttonbox">
	    <a title="分享到微信" class="bds_weixin" href="#" data-cmd="weixin"></a>
	    <a title="分享到QQ空间" class="bds_qzone" href="#" data-cmd="qzone"></a>
	    <a title="分享到新浪微博" class="bds_tsina" href="#" data-cmd="tsina"></a>
	    <a title="分享到人人网" class="bds_renren" href="#" data-cmd="renren"></a>
	    <a class="bds_more" href="#" data-cmd="more"></a>
	    </div>
        <script>window._bd_share_config={"common":{"bdSnsKey":{},"bdText":"","bdUrl":document.URL,"bdMini":"2","bdMiniList":false,"bdPic":"","bdStyle":"0","bdSize":"32"},"share":{}};with(document)0[(getElementsByTagName('head')[0]||body).appendChild(createElement('script')).src='/static/api/js/share.js?v=89860593.js?cdnversion='+~(-new Date()/36e5)];</script>  
        <div class="view_div">这么漂亮的场景&nbsp;→<span><a target="_blank" href="http://v.z01.com/">我也来制作</a></span></div>
    </div>
</div>
<iframe style="display:none;" src="/CallCounter.aspx?ztype=h5&id=<%:Mid %>&title=<%:pageMod.Title %>"></iframe>
<img id="img_bk" onerror="setdefbk();" style="display:none;" />
<div id="WX_Share" runat="server" visible="false"></div>
<style>
.progbar{height:6px;background-color: rgba(0,0,0,.2);width:100%;position:absolute;bottom:0px;z-index:9999;}
.progbar .bar{display:block;height:100%;width:0; background-color:#08A1EF;transition: width .8s cubic-bezier(.26,.86,.44,.985);}
.progbar .sindex{position:absolute;color:#fff;background-color:rgba(0, 0, 0, 0.25);bottom:6px;right:0px;font-size:20px;text-align:center;padding:3px 10px;}
</style>
<script src="/Design/JS/sea.js"></script>
<script src="/JS/Modal/EventBase.js"></script>
<script src="/JS/Plugs/angular.min.js"></script>
<script src="/JS/Modal/APIResult.js"></script>
<script>
    var page,scope;
    var myani = { swiper: null,isinit:false };
    var progbar = { activeIndex : 0,scenceNum:0 };
    myani.init = function (conf) {
        var opts={
            direction: 'vertical',
            effect:"slide",
            //autoplay:0,
            loop: true,
            speed: 1000,
            mousewheelControl: true,
            pagination: '.swiper-pagination',
            onInit: function (swiper) { 
                if('<%=!pageMod.Seflag.Contains("offad") %>'=='True'){
                    //加入
                    var adhtml = "<a href=\"/Class_3/NodePage.aspx\" target=\"_blank\" class=\"ani\" swiper-animate-effect=\"lightSpeedIn\" swiper-animate-duration=\"1s\" swiper-animate-delay=\"0s\" style=\"position:absolute;bottom:1em;display:block;left: 50%;  width: 380px;  margin-left: -190px;font-size:1.6em;line-height:2.4em; background:rgba(0, 0, 0, 0.50);border-radius:12px;text-align:center;text-decoration:none;z-index:1000;\">"
                            + "<span style=\"color:#fff;\">免费极速创建</span>"
                            + "<i class=\"fa fa-hand-o-right\" style=\"margin-left:10px;margin-right:10px;color:#fff;\"></i>"
                            + "<span style=\"color:#f0ad4e;\">动力逐浪微场景</span>"
                            + "</a>";
                    $(".swiper-slide:not(.swiper-slide-duplicate):last").append(adhtml);
                }
                swiperAnimateCache(swiper);
                if(conf.loop)
                {
                    setTimeout(function(){
                        $(".swiper-slide-duplicate:first").html($(".swiper-slide:not(.swiper-slide-duplicate):last").html());
                        $(".swiper-slide-duplicate:last").html( $(".swiper-slide:not(.swiper-slide-duplicate):first").html());
                        swiperAnimate(swiper);
                        myani.isinit=true;
                    },1000);
                }
                else{ swiperAnimate(swiper);myani.isinit=true;}
                progbar.setindex();
            },
            onTransitionStart:function(swiper){},
            onSlideChangeEnd: function (swiper) { if(myani.isinit){swiperAnimate(swiper);}},
            onTransitionEnd: function (swiper) { if(myani.isinit){swiperAnimate(swiper);}},
            onSlideNextStart: function(swiper){
                var selen=page.scence.list.length;
                //最后一页往后翻回到第一页
                if(progbar.index==selen){progbar.index=1;}else { progbar.index=swiper.activeIndex; }
                progbar.setindex();
            },
            onSlidePrevStart: function(swiper){
                var selen=page.scence.list.length;
                //第一页往前翻前往最后一页
                if(progbar.index==1){ progbar.index=selen; }else{ progbar.index=swiper.activeIndex; }
                progbar.setindex();
            }
        };
        if(conf)
        {
            opts.direction=conf.direction;
            opts.effect=conf.effect;
            opts.autoplay=conf.autoplay;
            if(page.scence.list.length<=1){opts.loop=false;conf.loop=false;}
            else{conf.loop=true;}
        }
        myani.swiper = new Swiper('.swiper-container',opts);
    }

    progbar.index=1;
    progbar.change=function(index){
        progbar.setindex();
    }
    progbar.setindex = function(){
        var selen=page.scence.list.length;
        $(".progbar .index").html(progbar.index);
        $(".progbar .num").html(selen);
        $(".progbar .bar").css("width",progbar.index/selen*100+'%');
    }

    var app = angular.module("app", [], function ($compileProvider) { })
    .controller("appCtrl", function ($scope, $compile) {
        scope = $scope;
        $scope.list = {};
        $scope.addDom = function (compObj) {
            if ($scope.list[compObj.id]) {  return; }
            $scope.list[compObj.id] = compObj;
            var html = $(compObj.AnalyToHtml());
            html.attr("style",compObj.config.style);
            compObj.SetInstance($compile(angular.element(html))($scope),document);
            angular.element(document.getElementById(compObj.config.bodyid)).append(compObj.instance);
        }
        $scope.pre = function(){if(myani){myani.swiper.slidePrev(); }}
        $scope.next = function(){ if(myani){ myani.swiper.slideNext();}}
    })
    .filter("html", ["$sce", function ($sce) { return function (text) { return $sce.trustAsHtml(text); }}]);
    seajs.use(["/design/js/se_comp/page"], function (p) {
        page=p;
        page.guid = "<%:pageMod.guid%>";
        page.pageData =<%=pageMod.page%>;
        page.compData = <%=pageMod.comp%>;
        page.comp_global=<%=comp_global%>
        page.extendData=<%=extendData%>;
        page.scence.list=<%=pageMod.scence%>;
        scope.scence=page.scence;scope.$digest();
        page.instance = $(document);
        page.init();
        for (var i = 0; i < page.compList.length; i++) {
            page.compList[i].mode="view";
            scope.addDom(page.compList[i]);
        }
        var conf = page.pageData.scence_conf;
        if(typeof(conf)=="string"){conf=JSON.parse(conf);}
        myani.init(conf);
        if(typeof(conf.automusic)=="undefined"||conf.automusic){$("#music_btn").click();}
    });
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
    function setbk(url)
    {
        $("#img_bk").attr("src",url);
        $(".pcview_bg").css('background-image','url('+url+')');
    }
    function setdefbk(){console.log("123123");  $(".pcview_bg").css('background-image','url(/UploadFiles/bg_pcview.jpg)');}
    setbk("<%:ZoomLa.BLL.Design.B_Design_Helper.GetPCViewBK()%>");
</script>
</form>
</div>
</body>
</html>