<%@ Page Language="C#" AutoEventWireup="true" CodeFile="pcview.aspx.cs" Inherits="Design_ppt_pcview" MasterPageFile="~/Common/Master/Empty2.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<script src="/JS/ZL_Regex.js"></script>
<link rel="stylesheet" href="/design/h5/css/swiper.min.css">
<link rel="stylesheet" href="/design/h5/css/animate.min.css">
<link href="/design/ppt/css/comp.css" rel="stylesheet" />
<style type="text/css">
.arrows-box{width:50px;height:100px;background-color:#B5BECC;position:fixed;margin-top:50vh;top:-50px;z-index:101;cursor:pointer;text-align:center;padding-top:15px;}
.arrows-box .fa{color:#fff;font-size:70px;}
.prev-arrow-box{left:0px;border-radius:0 3px 3px 0;}
.next-arrow-box{right:0px;border-radius:3px 0px 0px 3px;}
.progbar{height:6px;background-color: rgba(0,0,0,.2);width:100%;position:fixed;bottom:0px;z-index:9999;}
.progbar .bar{display:block;height:100%;width:0; background-color:#08A1EF;transition: width .8s cubic-bezier(.26,.86,.44,.985);}
.progbar .sindex{position:absolute;color:#fff;background-color:rgba(0, 0, 0, 0.25);bottom:6px;right:0px;font-size:20px;text-align:center;padding:3px 10px;}
</style>
<script src="/design/h5/js/swiper.min.js"></script>
<script src="/design/h5/js/swiper.animate.min.js"></script>
<title><asp:Literal runat="server" ID="Title_L"></asp:Literal></title>
<script>
    var phoneWidth = parseInt(window.screen.width),phoneHeight = $(window).height();
    var wScale = phoneWidth / 1280;
    var hScale=phoneHeight/720;
    console.log(phoneHeight,720,phoneHeight/720);
    $(function(){
        var margin=(((720*hScale)-720)/2)+"px";
        //负数情况下需要调整左边距
        if(wScale<1)
        {
            var left=(((1280*wScale)-1280)/2)+"px";
            $(".swiper-container").css("margin-left",left);
        }
        $(".swiper-container").css("transform","scale("+wScale+","+hScale+")")
        $(".swiper-container").css("margin-top",margin);
    })
</script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<form>
<div style="display:none;"><asp:Image ID="Wx_Img" runat="server" /></div>
<div ng-app="app" ng-controller="appCtrl">
    <div class="arrows-box prev-arrow-box" ng-click="pre();"><i class="fa fa-angle-left"></i></div>
    <div class="arrows-box next-arrow-box" ng-click="next();"><i class="fa fa-angle-right"></i></div>
    <div class="swiper-container scence" style="width:1280px;height:720px;">
        <div id="editorBody" class="swiper-wrapper">
            <section ng-repeat="se in scence.list|orderBy:'order' track by $index" class="swiper-slide" id="section_{{se.id}}">
                <div id="mainBody{{se.id}}" class="compbody"></div>
            </section>
        </div>
    </div>
    <div class="progbar"><span class="bar"></span><div class="sindex"><span class="index">0</span> / <span class="num">0</span></div></div>
</div>
<iframe style="display:none;" src="/CallCounter.aspx?ztype=ppt&id=<%:pageMod.ID %>&title=<%:pageMod.Title %>"></iframe>
<div id="WX_Share" runat="server" visible="false"></div>
</form>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
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
            direction: 'horizontal',
            effect:"slide",
            //autoplay:0,
            loop: true,
            speed: 1000,
            mousewheelControl: true,
            //pagination: '.swiper-pagination',
            onInit: function (swiper) { 
                if('<%=!pageMod.Seflag.Contains("offad") %>'=='True'){
                    //加入
                    var adhtml = "<a href=\"/Class_3/NodePage.aspx\" target=\"_blank\" class=\"ani\" swiper-animate-effect=\"lightSpeedIn\" swiper-animate-duration=\"1s\" swiper-animate-delay=\"0s\" style=\"position:absolute;bottom:1em;display:block;left: 50%;  width: 380px;  margin-left: -190px;font-size:1.6em;line-height:2.4em; background:rgba(0, 0, 0, 0.50);border-radius:12px;text-align:center;text-decoration:none;z-index:1000;\">"
                            + "<span style=\"color:#fff;\">免费极速创建</span>"
                            + "<i class=\"fa fa-hand-o-right\" style=\"margin-left:10px;margin-right:10px;color:#fff;\"></i>"
                            + "<span style=\"color:#f0ad4e;\">动力逐浪PPT</span>"
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
            //opts.direction=conf.direction;
            opts.effect=conf.effect;
            opts.autoplay=conf.autoplay;
            if(page.scence.list.length<=1){opts.loop=false;conf.loop=false;}
            else{conf.loop=true;}
            if(typeof(conf.automusic)=="undefined"||conf.automusic){$("#music_btn").click();}
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
                if (!compObj||$scope.list[compObj.id]) {  return; }
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
                scope.addDom(page.compList[i]);
            }
            var conf = page.pageData.scence_conf;
            if(typeof(conf)=="string"){conf=JSON.parse(conf);}
            myani.init(conf);
        });
    $(function(){
        //满屏
        //$(".swiper-container").height($(window).height());
        //$(window).resize(function(){
        //    $(".swiper-container").height($(window).height());
        //    $(".swiper-slide").height($(window).height());
        //});
        //键盘
        document.onkeydown=function(event){
            var e = event || window.event || arguments.callee.caller.arguments[0];
            if(e && e.keyCode==37){scope.pre();}
            if(e && e.keyCode==39){ scope.next();}            
        };
    })
</script>
</asp:Content>