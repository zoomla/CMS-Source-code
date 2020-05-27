<%@ Page Language="C#" AutoEventWireup="true" CodeFile="mbview.aspx.cs" Inherits="Design_h5_mbview" EnableViewState="false" ClientIDMode="Static" %><!DOCTYPE html>
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<meta http-equiv="X-UA-Compatible" content="IE=edge" />
<link type="text/css" rel="stylesheet" href="/dist/css/bootstrap.min.css" />
<link rel="stylesheet" href="/dist/css/font-awesome.min.css"/>
<script src="/JS/jquery-1.11.1.min.js"></script>
<script src="/dist/js/bootstrap.min.js"></script>
<script>
    var phoneWidth = parseInt(window.screen.width),phoneHeight = parseInt(window.screen.height);
    var phoneScale = phoneWidth / 640;
    var ua = navigator.userAgent;
    document.write('<meta name="viewport" content="width=640, minimum-scale = ' + phoneScale + ', maximum-scale = ' + phoneScale + '">');
</script>
<link rel="stylesheet" href="/design/res/css/comp.css" />
<link rel="stylesheet" href="/design/res/css/preview.css" />
<link rel="stylesheet" href="/design/h5/css/swiper.min.css">
<link rel="stylesheet" href="/design/h5/css/animate.min.css">
<script src="/Plugins/ScreenTurn/screentrun.js"></script>
<script src="/design/h5/js/swiper.min.js"></script>
<script src="/design/h5/js/swiper.animate.min.js"></script>
<script src="/JS/ZL_Regex.js"></script>
<script src="/JS/Modal/APIResult.js"></script>
<script src="/design/js/Plugs/jquery.eraser.js"></script>
<title><asp:Literal runat="server" ID="Title_L"></asp:Literal></title>
<style>
html,body,form{height:100%;}
#eraser {position: absolute;top: 0px;left: 0px;z-index: 1999;height:1040px;width:100%;}
</style>
</head>
<body>
<form id="form1" runat="server">
<div style="display:none;">
    <asp:Image ID="Wx_Img" runat="server" />
    <asp:Label ID="Tit_L" runat="server" />
</div>
<div hidden>这么好看的模板,不建一个吗</div>
<aside id="loading">
    <div id="fountainTextG_body">
        <div id="fountainTextG_1" class="fountainTextG">L</div>
        <div id="fountainTextG_2" class="fountainTextG">o</div>
        <div id="fountainTextG_3" class="fountainTextG">a</div>
        <div id="fountainTextG_4" class="fountainTextG">d</div>
        <div id="fountainTextG_5" class="fountainTextG">i</div>
        <div id="fountainTextG_6" class="fountainTextG">n</div>
        <div id="fountainTextG_7" class="fountainTextG">g</div>
        <div id="fountainTextG_8" class="fountainTextG">.</div>
        <div id="fountainTextG_9" class="fountainTextG">.</div>
        <div id="fountainTextG_10" class="fountainTextG">.</div>
    </div>
    <div style="position:fixed;bottom:10px;width:100%;text-align:center;font-size:1.2em;color:#fff;">动力逐浪微场景-基于逐浪CMS</div>
</aside>
<div ng-app="app" class="swiper-container scence">
    <div id="editorBody" class="swiper-wrapper" ng-controller="appCtrl">
        <section ng-repeat="se in scence.list|orderBy:'order' track by $index" class="swiper-slide" id="section_{{se.id}}">
            <div id="mainBody{{se.id}}" class="compbody"></div>
        </section>
    </div>
    <div class="swiper-pagination"></div>
    <img src="/design/h5/images/arrow.png" id="array" class="resize">
</div>
<iframe style="display:none;" src="/CallCounter.aspx?ztype=mbh5&id=<%:pageMod.ID %>&title=<%:pageMod.Title %>"></iframe>

<div id="WX_Share" runat="server" visible="false">
<script src="http://res.wx.qq.com/open/js/jweixin-1.0.0.js"></script>
<script>
    //----------WXAPI
    wx.config({
        debug: false,
        appId: '<%:appid%>',
        timestamp:'<%:timeStamp%>' , 
        nonceStr: '<%:noncestr%>', 
        signature: '<%=sign%>',
        jsApiList: [
            'checkJsApi',
            'onMenuShareTimeline',
            'onMenuShareAppMessage',
            'onMenuShareQQ',
            'onMenuShareWeibo',
        ]
    });
    var link="<%=shareLink%>";
    var title = '<%=pageMod.Title %>';
    var desc = '<%=string.IsNullOrEmpty(pageMod.Meta) ? pageMod.Title : pageMod.Meta %>';
    var imgs = '<%=SiteUrl+pageMod.PreviewImg %>';
    wx.ready(function() {
        wx.onMenuShareTimeline({
            title: title,
            desc: desc,
            link: link,
            imgUrl: imgs,
            trigger: function(res) {
            },
        });
        wx.onMenuShareAppMessage({
            title: title,
            desc: desc,
            link: link,
            imgUrl: imgs,
            trigger: function(res) {
            },
        });
        var pagedata = <%=pageMod.page %>;
        var conf = pagedata.scence_conf;
        if(typeof(conf)=="string"){conf=JSON.parse(conf);}
        if(typeof(conf.automusic)=="undefined"||conf.automusic){
            $("#music_btn").click();
            page.music.$audio[0].play();
        }

    });
</script>
</div>
</form>
<script src="/design/h5/js/swiper.min.js"></script>
<script src="/design/h5/js/swiper.animate.min.js"></script>
<script src="/JS/ZL_Regex.js"></script>
<script src="/JS/Modal/APIResult.js"></script>
<script src="/Design/JS/sea.js"></script>
<script src="/JS/Modal/EventBase.js"></script>
<script src="/JS/Plugs/angular.min.js"></script>
<link href="/Plugins/ScreenTurn/screentrun.css" rel="stylesheet" />

<script>
    var sitecfg=<%=sitecfg%>;
    var page,scope;
    var myani = { swiper: null,isinit:false };
    var app = angular.module("app", [], function ($compileProvider) { })
    .controller("appCtrl", function ($scope, $compile) {
        scope = $scope;
        $scope.list = {};
        $scope.addDom = function (compObj) {
            if (!compObj||$scope.list[compObj.id]) {  return; }
            $scope.list[compObj.id] = compObj;
            var html = $(compObj.AnalyToHtml());
            html.attr("style",compObj.config.style);
            compObj.SetInstance($compile(angular.element(html))($scope),document);
            angular.element(document.getElementById(compObj.config.bodyid)).append(compObj.instance);
        }
    })
    .filter("html", ["$sce", function ($sce) {
        return function (text) { return $sce.trustAsHtml(text); }
    }]);
    seajs.use(["/design/js/se_comp/page"], function (p) {
        page=p;
        page.guid = "<%:pageMod.guid%>";
        page.pageData =<%=pageMod.page%>;
        page.compData = <%=pageMod.comp%>;
        page.comp_global=<%=comp_global%>
        page.extendData=<%=extendData%>;
        page.scence.list=<%=pageMod.scence%>;
        scope.scence=page.scence;
        scope.$digest();
        page.instance = $(document);
        page.init();
        for (var i = 0; i < page.compList.length; i++) {
            scope.addDom(page.compList[i]);
        }
        myani.init = function (conf) {
            var opts={
                direction: 'vertical',
                effect:"slide",
                //autoplay:0,
                loop: true,
                speed: 1000,
                mousewheelControl: false,
                pagination: '.swiper-pagination',
                onInit: function (swiper) { 
                    if('<%=!pageMod.Seflag.Contains("offad") %>'=='True'){
                    //加入
                    var adhtml = "<a href=\"http://v.z01.com/h5/listpage.shtml\" target=\"_blank\" class=\"ani\" swiper-animate-effect=\"lightSpeedIn\" swiper-animate-duration=\"1s\" swiper-animate-delay=\"0s\" style=\"position:absolute;bottom:1em;display:block;left: 50%;  width: 380px;  margin-left: -190px;font-size:1.6em;line-height:2.4em; background:rgba(0, 0, 0, 0.50);border-radius:12px;text-align:center;text-decoration:none;z-index:1000;\">"
                        + "<span style=\"color:#fff;\">免费极速创建</span>"
                        + "<i class=\"fa fa-hand-o-right\" style=\"margin-left:10px;margin-right:10px;color:#fff;\"></i>"
                        + "<span style=\"color:#f0ad4e;\">动力逐浪微场景</span>"
                        + "</a>";
                    $(".swiper-slide:not(.swiper-slide-duplicate):last").append(adhtml);
                }
                swiperAnimateCache(swiper);
                //与angular不兼容,需要特殊处理
                setTimeout(function(){
                    $(".swiper-slide-duplicate:first").html($(".swiper-slide:not(.swiper-slide-duplicate):last").html());
                    $(".swiper-slide-duplicate:last").html( $(".swiper-slide:not(.swiper-slide-duplicate):first").html());
                    setTimeout(function(){ 
                        $("#loading").remove();myani.isinit=true;
                        if(swiper.slides.length>1&&!parent){ //ios 下iframe引入不兼容                      
                            swiper.activeIndex=0;swiperAnimate(swiper);
                            setTimeout(function(){ swiper.activeIndex=1;swiperAnimate(swiper);},200);}
                        else{swiperAnimate(swiper);}
                    },500);
                },1000);
            },
                onSlideChangeEnd: function (swiper) { if(myani.isinit){swiperAnimate(swiper);}},
            onTransitionEnd: function (swiper) { if(myani.isinit){swiperAnimate(swiper);}}
        };
        if(conf)
        {
            opts.direction=conf.direction;
            opts.effect=conf.effect;
            opts.autoplay=Convert.ToInt(conf.autoplay,0);
        }
        myani.swiper = new Swiper('.swiper-container',opts);
    }
        var conf = JSON.parse(page.pageData.scence_conf);
        myani.init(conf);
        if(conf){ screenTurn.init(conf.screen); }
    });

</script>
</body>
</html>