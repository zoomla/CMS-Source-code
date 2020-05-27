<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReBack.aspx.cs" Inherits="ZoomLaCMS.dai.ReBack" MasterPageFile="~/Cart/order.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<style type="text/css">
#maindiv {background-color:#fff;border:1px solid #ddd;border-top:none;padding:20px;}
.cam_div {float: left;height:511px; overflow:hidden;}
.cam_title {background:url(res/head.png) no-repeat;width:175px;height:30px;}
.wrap_div {width: 640px; height: 480px;background-color:#999;border:1px solid #00A2FF;}
.cam_btns {position:absolute;width:185px;right:0px;text-align:right;margin-top:-70px;z-index:5;}
.cam_btn {display:inline-block;font-size:2.0em;cursor:pointer;cursor:pointer;border-right:1px solid #999;background-color:rgba(255, 255, 255, 0.70);}
.cam_btn:hover {color:#00A2FF;}
.prolist_div {background-color:#FCFCFC;border:1px solid #ddd;float:left;margin-left:15px;padding:0 15px;width:440px;height:510px;overflow-y:auto;}
.pro_title {color:#00A2FF;font-size:1.2em;}
.prolist_ul li {float:left;width:45%;height:95px;margin-right:18px;margin-bottom:15px;cursor:pointer;}
.prolist_item img {width:100%;height:50px;}
.prolist_item .proname {width:175px; text-overflow:ellipsis;white-space:nowrap;overflow:hidden; color:#999;padding:5px;}
.prolist_item .price {color:#ff0000;padding:5px;}
.slide_div {position:absolute;left:0px;margin-top:-35px;padding:5px;}
.camera_btns {}
.game_btns{display:none;}
</style>
<title>真人试戴</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div class="head_div">
        <a href="/"><img src="<%=Call.LogoUrl %>" class="margin_l5" /></a>
    <div class="input-group pull-right skey_div text_300">
        <input type="text" id="skey_t" placeholder="商品搜索" class="form-control skey_t" data-enter="0"/>
        <span class="input-group-btn">
            <input type="button" value="搜索" class="btn btn-default" onclick="skey();" data-enter="1"/>
        </span>
    </div>
</div>
<div class="container margin_t5">
<ul class="nav nav-tabs" id="nav_dai" role="tablist">
        <li role="presentation"><a href="/dai"><span class="fa fa-user"></span>模特试镜</a></li>
        <li role="presentation" class="active"><a href="#main_tab" aria-controls="main_tab" role="tab" data-toggle="tab"><span class="fa fa-camera"></span>拍照试戴</a></li>
        <li role="presentation"><a href="/Class_2/Default.aspx" target="_blank"><span class="fa fa-shopping-cart"></span>在线商城</a></li>
    </ul>
<div id="maindiv">
<div class="cam_div">
    <div class="cam_title"></div>
    <div style="position: relative;" id="camera_div" class="wrap_div">
        <video id="cam_video" autoplay="autoplay" width="640" height="480"></video>
        <div class="btn btn-group cam_btns camera_btns">
            <a class="btn btn-default cam_btn" title="拍照试戴" onclick="shotImg();"><i class="fa fa-camera"></i></a>
            <a class="btn btn-default cam_btn" title="前往购物" href="/Class_2/Default.aspx" target="_blank"><i class="fa fa-shopping-cart"></i></a>
            <a class="btn btn-default cam_btn" title="下载保存"><i class="fa fa-download"></i></a>
        </div>
    </div>
    <div style="position:relative;" id="game_div" class="wrap_div">
        <canvas id="gameCanvas" width="640" height="480"></canvas>
          <div class="btn btn-group cam_btns game_btns">
            <a class="btn btn-default cam_btn" title="重新拍照" onclick="backToCamera();"><i class="fa fa-chevron-circle-left"></i></a>
            <a class="btn btn-default cam_btn" title="前往购物" href="/Class_2/Default.aspx" target="_blank"><i class="fa fa-shopping-cart"></i></a>
            <a class="btn btn-default cam_btn" title="下载保存" onclick="downImg();"><i class="fa fa-download"></i></a>
        </div>
          <div class="slide_div game_btns" title="放大缩小图片">
            <div class="nstSlider" id="picSlider" data-range_min="-50" data-range_max="100" data-cur_min="0" data-cur_max="0">
                <div class="nst_bar"></div>
                <div class="leftGrip"></div>
            </div>
        </div>
          <div class="btn btn-group cam_btns game_btns" style="top:70px;width:130px;">
            <a onclick="RotePhoto(-90)" title="左转" class="btn btn-default cam_btn"><i class="fa fa-rotate-left"></i></a>
            <a onclick="RotePhoto(90)"  title="右转" class="btn btn-default cam_btn"><i class="fa  fa-rotate-right"></i></a>
        </div>
    </div>
</div>
<div class="prolist_div">
    <h1 class="pro_title">商品列表</h1>
    <ul class="list-unstyled prolist_ul">
        <asp:Repeater runat="server" ID="RPT" EnableViewState="false">
            <ItemTemplate>
                <li class="prolist_item" title="<%#Eval("spm") %>">
                    <img src="<%#Eval("sptp") %>" data-sub="<%#Eval("sptp2") %>" class="img50" />
                    <div class="proname"><%#Eval("spm") %></div>
                    <span class="price"><i class="fa fa-rmb"></i><%#Eval("price") %></span>
                </li>
            </ItemTemplate>
        </asp:Repeater>
    </ul>
    </div>
<div class="clearfix"></div>
    </div>
</div>
<div class="hidden">
    <asp:HiddenField ID="TempData_Hid" Value="[{&quot;ID&quot;:12,&quot;price&quot;:200.0,&quot;sptp&quot;:&quot;res/20120608glass_12.png&quot;,&quot;spm&quot;:&quot;蝙蝠侠_BM97004 B1(透明黑)&quot;,&quot;sptp2&quot;:null},{&quot;ID&quot;:11,&quot;price&quot;:300.0,&quot;sptp&quot;:&quot;res/20120608glass_11.png&quot;,&quot;spm&quot;:&quot;蝙蝠侠_BM97004 B6(绅士银)&quot;,&quot;sptp2&quot;:null},{&quot;ID&quot;:10,&quot;price&quot;:600.0,&quot;sptp&quot;:&quot;res/20120608glass_10.png&quot;,&quot;spm&quot;:&quot;蝙蝠侠_BM95002 C9D(绅士黑)&quot;,&quot;sptp2&quot;:null},{&quot;ID&quot;:9,&quot;price&quot;:300.0,&quot;sptp&quot;:&quot;res/20120608glass_9.png&quot;,&quot;spm&quot;:&quot;沙漠之鹰_R5137 C16(绅士黑)&quot;,&quot;sptp2&quot;:null},{&quot;ID&quot;:8,&quot;price&quot;:120.0,&quot;sptp&quot;:&quot;res/20120608glass_8.png&quot;,&quot;spm&quot;:&quot;沙漠之鹰_R5152 CCG(绅士银)&quot;,&quot;sptp2&quot;:&quot;/Plugins/tryin/res/glass_7_2.png&quot;},{&quot;ID&quot;:7,&quot;price&quot;:150.0,&quot;sptp&quot;:&quot;res/20120608glass_7.png&quot;,&quot;spm&quot;:&quot;沙漠之鹰_R5152 C16(绅士黑)&quot;,&quot;sptp2&quot;:null},{&quot;ID&quot;:6,&quot;price&quot;:200.0,&quot;sptp&quot;:&quot;res/20120608glass_6.png&quot;,&quot;spm&quot;:&quot;毕加索_55-2051 C11(荧光红)&quot;,&quot;sptp2&quot;:null},{&quot;ID&quot;:5,&quot;price&quot;:100.0,&quot;sptp&quot;:&quot;res/20120608glass_5.png&quot;,&quot;spm&quot;:&quot;毕加索_55-2068 C11(荧光红)&quot;,&quot;sptp2&quot;:null},{&quot;ID&quot;:4,&quot;price&quot;:120.0,&quot;sptp&quot;:&quot;res/20120608glass_4.png&quot;,&quot;spm&quot;:&quot;毕加索_55-2001 C6(绅士黑)&quot;,&quot;sptp2&quot;:null},{&quot;ID&quot;:3,&quot;price&quot;:150.0,&quot;sptp&quot;:&quot;res/20120608glass_2.png&quot;,&quot;spm&quot;:&quot;佐腾樱花_ZTYH-010(蓝色)&quot;,&quot;sptp2&quot;:null},{&quot;ID&quot;:2,&quot;price&quot;:200.0,&quot;sptp&quot;:&quot;res/20120608glass_3.png&quot;,&quot;spm&quot;:&quot;毕加索_55-2062 C6(绅士黑)&quot;,&quot;sptp2&quot;:null},{&quot;ID&quot;:1,&quot;price&quot;:100.0,&quot;sptp&quot;:&quot;res/20120608glass_1.png&quot;,&quot;spm&quot;:&quot;佐腾樱花_ZTYH-001(豹纹色)&quot;,&quot;sptp2&quot;:&quot;&quot;}]" runat="server" />
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<link type="text/css" rel="stylesheet" href="src/jquery.nstSlider.css">
<script src="/JS/Controls/ZL_Webup.js"></script>
<script src="Camera.js"></script>
<script src="src/jquery.nstSlider.min.js"></script>
<script src="src/cocos2d-js-v3.8-lite.js" charset="UTF-8"></script>
<script src="src/resource.js" charset="UTF-8"></script>
<script>
    var GameCanvas, curLayer;
    var timeflag = 0;//延迟标记
    var rageflag = 0;//旋转标记
    $(function () {
        Camera.init("cam_video");
        Camera.open();
        cc.game.onStart = function () {
            cc.LoaderScene.preload(g_resources, function () {
                cc.director.runScene(new PlayScene());
            }, this);
        };
        cc.game.run();
        //绑定事件
        $(".prolist_item").click(function () {
            cur_glass_front = $(this).find('img').attr('src')
            curLayer.addGlass(cur_glass_front);
        });
        $("#picSlider").nstSlider({
            "left_grip_selector": ".leftGrip",
            "value_bar_selector": ".nst_bar",
            "value_changed_callback": function (cause, leftValue, rightValue) {
                //$(".slide_font").text(100 + leftValue + "%");
                var tempval = (100 + leftValue) / 100;
                clearTimeout(timeflag);
                timeflag = setTimeout(function () { if (!curLayer) { return; } curLayer.bgSprite.runAction(cc.sequence(cc.scaleTo(0.3, tempval, tempval))); }, 100);
            },
        });
    })
    function shotImg() {
        //拍照并进入试戴界面
        $("#camera_div").hide();
        $("#game_div").show();
        $(".camera_btns").hide(); $(".game_btns").show();
        var base64 = Camera.shot();
        Camera.save(base64, function (url) {
            curLayer.changeBG(url);
        });
    }
    //--------------------
    //旋转图片
    function RotePhoto(flag) {
        rageflag += flag;
        if (rageflag > 360) { rageflag = 90; }
        if (rageflag < -360) { rageflag = -90; }
        if (!curLayer) { return; }
        curLayer.bgSprite.runAction(cc.sequence(cc.rotateTo(0.3, rageflag)));
    }
    function backToCamera() {
        $("#camera_div").show();
        //$("#game_div").show();
        $(".camera_btns").show(); $(".game_btns").hide();
    }
    function downImg() {
        var $form = $('<form action="DownImg.aspx" target="_blank" method="post">');
        var $hid = $('<input type="hidden" name="img_hid">');
        $hid.val($("#gameCanvas")[0].toDataURL());
        $form.append($hid);
        $form.submit();
    }
    function skey() {
        var key = $("#skey_t").val();
        window.open("/Search/SearchList.aspx?node=0&keyword=" + key);
    }
</script>
</asp:Content>