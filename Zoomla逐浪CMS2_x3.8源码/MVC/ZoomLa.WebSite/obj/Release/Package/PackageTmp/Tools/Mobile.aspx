<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Mobile.aspx.cs" Inherits="ZoomLaCMS.Tools.Mobile" ValidateRequest="false" MasterPageFile="~/Common/Common.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>微逐浪-响应式网站模拟浏览器</title>
<link href="/App_Themes/V3.css" rel="stylesheet" type="text/css" />
<script src="/JS/Mobile/Mobile.js" type="text/javascript"></script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content"> 
<div class="mobile_broswer"> 
<div class="input-group">
<span class="input-group-addon">网址</span>
<input type="text" name="Skey" id="Skey" class="form-control" placeholder="http://www.z01.com" onkeydown="return IsEnter(this);"/>
<span class="input-group-btn"> 
<button type="button" id="submitBtn" class="btn btn-default">GO</button>
</span>
</div> 
</div>
<div style="height:30px;"></div> 
<div id="iframeLoading" style="position:absolute; z-index:2"><div class="loadingBox"></div></div>
<div id="pagePanel" class="openPanel" style="display:none;"> 
<div class="f_main" style="right:0">
<div class="r_header"> 
<div class="responsiveNav" id="rulers">
<span id="lg_v_view" class="fa fa-desktop" title="宽屏电脑" data-width="max" data-tips="3_t"></span>
<span id="lg_h_view" class="fa fa-tablet fa-rotate-90" title="pad-横屏" data-width="1024" data-tips="3_t"></span>
<span id="md_h_view" class="fa fa-tablet" title="pad-竖屏" data-width="768" data-tips="3_t"></span>
<span id="xs_v_view" class="fa fa-mobile fa-rotate-90" title="手机-横屏" data-width="480" data-tips="3_t"></span>
<span id="xs_h_view" class="fa fa-mobile" title="手机-竖屏" data-width="320" data-tips="3_t"></span>
</div>
</div> 
<div class="f_content" id="pageContainer" style="bottom:0; top:40px;"> 
<div class="f_web" id="frameContainer">
<div id="frameMask"></div>
<iframe src="<%Call.Label("{$GetUrldecode({$GetRequest(Skey)$})$}"); %>"  width="100%" height="100%" scrolling="no" frameborder="0" id="pageFrame" ></iframe>
</div>
</div>
</div>
</div>
<script>
    var click = 'click',
    supportTouch = false;
    /*标尺点击事件*/
    var frameWidth = 'Max',
    rulers = $('#rulers'),
    pageContainer = $('#pageContainer'),
    frameContainer = $('#frameContainer'),
    pageFrame = $('#pageFrame');
    $('span', rulers).litabs({
    callBack: function (el) {
        var frameWidth = el.data('width') == 'max' ? '100%' : el.data('width');
        frameContainer.width(frameWidth);
    }
    });
    if (supportTouch) {
        pageFrame.css('height', 'auto');
        frameContainer.css({ '-webkit-overflow-scrolling': 'touch', 'overflow': 'auto' });
    };
    pageFrame.load(function () {
    if ($('#iframeLoading').length) {
        
    }
    });
    function GetQueryString(name) {
        var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
        var r = window.location.search.substr(1).match(reg);
        if (r != null) return unescape(r[2]); return null;
    }
    $().ready(function (e) {
        $("#xs_h_view").click();
        if ('<%Call.Label("{$GetUrldecode({$GetRequest(Skey)$})$}"); %>' != "") {
            $(".mobile_broswer").addClass("navbar-fixed-top");
            $('#iframeLoading').remove();
            $('#pagePanel').fadeIn();
        } 
    })
    $("#submitBtn").click(function (e) { 
        if ($("#Skey").val() != "")
            window.location = "Mobile.aspx?Skey=" + escape($("#Skey").val());
    })
    function IsEnter(obj) {
        if (event.keyCode == 13) {
            $("#submitBtn").click(); return false;
        }
    } 
</script>
</asp:Content>

