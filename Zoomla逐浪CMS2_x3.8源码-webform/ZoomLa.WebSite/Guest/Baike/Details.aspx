<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Details.aspx.cs" MasterPageFile="~/Guest/Baike/Baike.master" ClientIDMode="Static" Inherits="Guestbook_BkDetails" EnableViewStateMac="false" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>逐浪百科</title> 
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">  
<div class="container" data-offset="280">
<ol class="breadcrumb margin_top10" style="margin-bottom:0px;">
    <li><a href="/Baike">百科中心</a></li>
    <li class="active">词条详情</li>
    <li>
     <%--   <div class="text-right margin_r5 margin_t5">
<span class="bk_top_btn" title="收藏"><i class="fa fa-heart"></i> 收藏</span>|
<span class="bk_top_btn" title="点赞"><i class="fa fa-thumbs-o-up"></i> 点赞</span>
</div>--%>
    </li>
</ol>
<div>
<div class="tittle_div">
    <span id="tittle_sp" runat="server" class="bktitle"></span>
    <span id="cate_sp" runat="server" class="bktype"></span>
    <a runat="server" id="edit_a" href="#" class="btn btn-default margin_l5"><i class="fa fa-pencil"></i> 编辑</a>
    <div class="pull-right" style="margin-right:15px;" id="favAndLike_wrap">
        <span id="addfav_btn" style="display:none;" class="bk_top_btn" title="收藏" onclick="B_Fav.add(<%:Mid%>);"><i class="fa fa-star"></i> 收藏</span>
        <span id="delfav_btn" style="display:none;" class="bk_top_btn" title="取消收藏" onclick="B_Fav.del(<%:Mid%>);"><i class="fa fa-star" style="color:#F5BD5D"></i> 已收藏</span>
        <span class="margin_l5">|</span>
        <span id="addlike_btn" style="display:none;" class="bk_top_btn" title="点赞" onclick="B_Like.add(<%:Mid%>);"><i class="fa fa-thumbs-o-up"></i> <span class="like_count_sp r_gray"></span></span>
        <span id="dellike_btn" style="display:none;" class="bk_top_btn" title="已点赞"><i class="fa fa-thumbs-o-up" style="color:#ccc;"></i> <span class="like_count_sp r_gray"></span></span>
    </div>
</div>
<div class="brief_div">
    <div runat="server" id="pic_div" style="float:left;padding-right:10px;padding-bottom:10px;">
        <img runat="server" id="pic_img" style="max-width:200px;" />
    </div>
    <asp:Label runat="server" ID="Brief_L"></asp:Label>
    <div class="clearfix"></div>
</div>
<div class="info_div">
    <ul class="list-unstyled" id="info_tb"></ul>
</div>
<div style="position: relative;">
        <div id="loading"></div>
        <div class="index_div">
            <div class="block-title col-lg-2 col-md-2 col-xs-2">目录</div>
            <div class="catalog-list col-lg-10 col-md-10 col-xs-10" id="baike_list"></div>
            <div class="clearfix"></div>
        </div>
        <div >
            <div runat="server" id="Contents_div"></div>
        </div>
    </div>
<!--content end;-->
<div class="bk_bottom">
    <div class="bkheader"><span>参考资料</span></div>
    <div id="ref_body" style="margin-bottom:20px;"></div>
    <div class="bkheader"></div>
    <div><strong>词条标签：</strong><asp:Label runat="server" ID="BType_L"></asp:Label></div>
</div>
</div>
<nav id="baike_div" class="bs-docs-sidebar affix-top">
     <ul class="nav" id="baike_nav"></ul>
     <div><button type="button" style="width: 50px;height: 50px;font-size:20px; border-radius:0;" onclick="toggleNav()" class="btn btn-default"><span class="fa fa-th-list"></span></button></div>
    <div id="topcontrol" title="点击回到顶部！" style="cursor: pointer; opacity: 1;"></div>
</nav>
</div>
<div class="ask_bottom" style="width:100%;">
    <p class="text-center"><a target="_blank" title="如何提问" href="http://help.z01.com/?index/help.html#如何提问">如何提问</a> <a target="_blank" title="如何回答" href="http://help.z01.com/?index/help.html#如何回答">如何回答</a> <a target="_blank" title="如何获得积分" href="http://help.z01.com/?index/help.html#如何获得积分">如何获得积分</a> <a target="_blank" title="如何处理问题" href="http://help.z01.com/?index/help.html#如何处理问题">如何处理问题</a></p>
    <p class="text-center"><%Call.Label("{$Copyright/}"); %></p>
</div>
<div class="hidden">
    <asp:HiddenField runat="server" ID="info_hid" />
    <asp:HiddenField runat="server" ID="refence_hid" />
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
<style type="text/css">
.floatul li {float:left;margin-left:10px;}
.foot_sub {font-size:16px;font-weight:bold;}
/*资料,信息,参考*/
.bk_top_btn {color:#888;line-height:18px;cursor:pointer;}
.bk_top_btn i{color:#4d8ac8;}
.tittle_div {margin-top:15px;margin-bottom:5px;}
.tittle_div .bktitle {font-size:34px;line-height:34px;margin-bottom:14px;color:#000;font-weight:400;margin:0 10px 0 0;}
.tittle_div .bktype {font-size:20px;font-weight:400;color:#333;}
#info_tb li {border-bottom:1px dashed #ddd;line-height:26px; padding-left:10px;padding-right:10px;}
.info_li_div {width:48%;float:left;}
.info_l {width:107px;color:#999;font-weight:700; display:inline-block;}
.info_r {color:#333;display:inline-block;}
.ref_item  {color:#666;line-height:30px;font-size:12px;}
.ref_item .item_url {color:#666;}
/*目录索引*/
.block-title {border:1px solid #ddd;background:#fbfbfb;height: 210px; line-height: 210px; font-size: 1.5em; text-align: center;}
.catalog-list {display:block;position:relative;overflow:hidden;height:210px;
                padding-top:15px;padding-bottom:10px;background:#fff;
                border:1px solid #ddd;}
.dirul {float:left;width:150px;border-right:1px solid #ddd;padding-left:20px;list-style-type:none;list-style:none;}
.dirul>li{padding-bottom:3px;list-style-type:none;list-style:none;}
.drul li ul {padding:0px;margin:0px;list-style-type:none;}
.dirul .level1 {color:#136ec2;font-size:16px;font-weight:500;text-decoration:none;}
.dirul .level2 {color:#333;line-height:16px;font-size:12px;text-decoration:none;color:#136ec2;padding-left:12px;}
.dirul .level3 {color:#333;line-height:16px;font-size:12px;text-decoration:none;padding-left:24px;}
/*边栏滚动监听*/
#baike_div {position:fixed;right:10px;bottom:100px;padding:10px;}

#baike_nav li {border-left:3px solid #fff;}
#baike_nav li.active {border-left:3px solid #563d7c;}
#baike_nav li>a{padding:0px;padding-left:10px;padding-bottom:3px;}
#baike_nav .level1 {color:#333;font-weight:bold;}
#baike_nav .level2 {color:#666;padding-left:12px;}
#baike_nav .level3 {color:#666;padding-left:24px;}
#topcontrol{display: block;width: 50px;height: 50px;background: url(/App_Themes/User/top.jpg);margin-right: 14px;margin-bottom: 44px;}
#topcontrol:hover{background: url(/App_Themes/User/top.jpg) 0px 50px;background-image: url(http://www.z01.com/App_Themes/User/top.jpg);background-position-x: 0px;background-position-y: 50px;background-size: initial; background-repeat-x: initial;background-repeat-y: initial;background-attachment: initial;background-origin: initial;background-clip: initial;background-color: initial;}

.bkheader{padding-bottom:10px;border-bottom:2px solid #ccc;padding:0 !important;margin:0 !important;font-size:19px;line-height:45px;font-weight:200;}
.btype_a {margin-left:5px;margin-right:5px;color:#136ec2;}
.flag_h {border-bottom:1px solid #ddd;padding-bottom:5px;}
.flag_h .flag_num {font-size:14px; display:inline-block;width:25px;height:25px;line-height:25px;border-radius:4px; color:#fff;background-color:#136ec2;text-align:center;}
.flag_h .flag_name {font-size:18px;color:#2488e4;padding-left:5px;}
</style>
<script src="/JS/ICMS/ZL_Common.js"></script>
<script src="/JS/Plugs/Baike.js"></script>
<script src="/JS/ZL_Regex.js"></script>
<script src="/JS/Controls/ZL_Array.js"></script>
<script src="/JS/Modal/APIResult.js"></script>
<script>
    BaiKe.config.id = "Contents_div";
    BaiKe.config.dirid = "baike_list";
    BaiKe.config.navid = "baike_nav";
    BaiKe.Init();
</script>
<script>
    $(function () {
        $("#baike_div").hide();
        $('body').scrollspy({ target: '#baike_div', offset: 100 });
        $('body').on('activate.bs.scrollspy', function (e) {
            $("#baike_nav .navTitle").each(function (i) {
                if ($(this).parent()[0] == e.target) {
                    if (i >= 1) { $("#baike_div").show(); return; } else { $("#baike_div").hide(); }
                }
            })
        })
        //---------------------------------------
        var intertag = 0;
        $("#topcontrol").click(function () {
            intertag = setInterval(function () {
                if ($(window).scrollTop() <= 0) { clearInterval(intertag); }
                $(window).scrollTop($(window).scrollTop() - 50);
            }, 10);
        });
        //---------------------------------------
        {
            var val = $("#info_hid").val();
            if (!ZL_Regex.isEmpty(val)) { info.data = JSON.parse(val); info.dataToHtml(); }
        }
        //---------------------------------------
        {
            var val = $("#refence_hid").val();
            if (!ZL_Regex.isEmpty(val)) { refence.data = JSON.parse(val); refence.dataToHtml(); }
        }
    });
    function toggleNav() {
        if ($('#baike_nav').css("visibility") == "visible")
        { $('#baike_nav').css('visibility', 'hidden'); }
        else
        { $('#baike_nav').css('visibility', 'visible'); }
    }
    //---------修改h1标识的样式,根据选择开启
    var $items = $("#Contents_div").find("h1");
    //var flagTlp = '<h1 class="flag_h"><span class="flag_num">1</span><span class="flag_name">级别信息</span></h1>';
    for (var i = 0; i < $items.length; i++) {
        //替换标识
        var $item = $($items[i]);
        var name = $item.text(); $item.text("");
        $item.addClass("flag_h");
        $item.append('<span class="flag_num">' + (i + 1) + '</span><span class="flag_name">' + name + '</span>');
    }
</script>
<script>
var Mid = "<%:Mid%>";
var B_Fav = {
    add: function (id) {
        $.post("/API/Mod/collect.ashx?action=add", { infoID: id, title: $("#tittle_sp").text(), favurl: "<%=Request.RawUrl%>", type: "5" }, function (data) { });
        $("#addfav_btn").hide(); $("#delfav_btn").show();
    }, del: function (id) {
        $.post("/API/Mod/collect.ashx?action=del", { infoID: id, type: "5" }, function (data) { });
        $("#addfav_btn").show(); $("#delfav_btn").hide();
    }
};
var B_Like = {
    add: function (id) {
        $.post("/API/Mod/like.ashx?action=add", { infoID: id, source: "baike" }, function (data) { });
        var num = ConverToInt($(".like_count_sp:first").text()) + 1;
        $(".like_count_sp").text(num);
        $("#addlike_btn").hide(); $("#dellike_btn").show();
    },
    getCount: function () {
        $.post("/API/Mod/Like.ashx?action=count", { infoID: Mid, source: "baike" }, function (data) {
            var model = APIResult.getModel(data);
            $(".like_count_sp").text(ConverToInt(model.result));
        })
    }
};
$(function () {
    B_Like.getCount();
    $.post("/API/Mod/like.ashx?action=has", { infoID: Mid, source: "baike" }, function (data) {
        var model = APIResult.getModel(data);
        if (model.result == true) { $("#dellike_btn").show(); }
        else { $("#addlike_btn").show(); }
    });
    $.post("/API/Mod/Collect.ashx?action=has", { infoID: "<%:Mid%>", type: "5" }, function (data) {
            var model = APIResult.getModel(data);
            if (model.result == true) { $("#delfav_btn").show(); } else { $("#addfav_btn").show(); }
        });
    })
</script>
</asp:Content>