<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="ZoomLaCMS.Manage.login" MasterPageFile="~/Common/Common.master" %>

<asp:Content runat="server" ContentPlaceHolderID="head"><title>管理登录</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<center style="background:url(<%=Call.GetRandomImg()%>);background-position: center;left:0;top:0;right:0;bottom:0; position: absolute; background-repeat:no-repeat;background-size:cover;">
<div class="manage_login">
<ul class="list-unstyled">
<li class="manage_logo"><img class="img-responsive" src="<%:Call.LogoUrl%>" alt="<%:Call.SiteName%>_后台管理系统" /></li>
<li><i class="fa fa-user"></i>
<asp:TextBox runat="server" ID="TxtUserName" TabIndex="1" data-enter="1" size="20"  class="form-control input-control" autocomplete="off" placeholder="帐户"  autofocus="true"/></li>
<li><i class="fa fa-lock"></i>
<asp:TextBox runat="server" ID="TxtPassword" TabIndex="2" data-enter="2" TextMode="Password" MaxLength="18" AllowEmpty="false"
    class="form-control input-control" autocomplete="off" placeholder="密码" /><br />
</li>
<li id="safecode" runat="server"><asp:TextBox runat="server" ID="TxtAdminValidateCode" data-enter="3" MaxLength="6" class="form-control input-control"/></li>
<li id="CodeLi" runat="server" visible="false">
    <i class="fa fa-key"></i>
   <asp:TextBox runat="server" ID="VCode" TabIndex="3" data-enter="4" class="form-control input-control" MaxLength="6" autocomplete="off" placeholder="验证码"/>
   <img id="VCode_img" runat="server" title="点击刷新验证码" class="code" style="float:right; width:130px; height:34px; "/>
   <input type="hidden" id="VCode_hid" name="VCode_hid" />
   <div class="clearfix"></div>
</li>
<li id="ZnCode_Li" runat="server" visible="false">
<asp:TextBox runat="server" ID="ZnCode_T" CssClass="form-control input-control" placeholder="动态口令"></asp:TextBox>
</li>
<li>
    <input type="button" id="IbtnEnter" data-enter="5" class="btn btn-info apply_btn" value="登录" onclick="return ajaxlogin();" />
    <asp:Button runat="server" ID="RealBtn" style="display:none;" OnClick="IbtnEnter_Click" />
</li>
<li><a href="AccountForm.aspx" visible="false" id="test_Link" runat="server" class="btn btn-default apply_btn" target="_blank">申请测试帐号</a></li> 
<li> 
<a href="/" target="_blank" title="首页"><span class="fa fa-home"></span></a>
<a href="/Help.html" target="_blank" title="帮助"><span class="fa fa-globe"></span></a>
<a href="http://help.z01.com/Database/" target="_blank" title="数据字典"><span class="fa fa-book"></span></a>
</li>
</ul>
</div>
</center>
<div class="bolang1 footimg" style="bottom: -30.8598px;"></div>
<div class="bolang2 footimg" style="bottom: -0.831503px;"></div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
<style type="text/css">
.popover{width:300px; text-align:center;}
</style>
<link rel="stylesheet" href="/App_Themes/V3.css?id=20150520" />
<script src="/JS/ZL_ValidateCode.js"></script>
<script src="/JS/jquery.easing.js"></script>
<script src="/JS/Controls/Control.js"></script>
<script>
    $(function () {
        if (self != top) { top.location = self.location; }
        if ($("#Apple_Hid").val() == "1") location = "default.aspx";
        $("#VCode").ValidateCode();
        Control.EnableEnter();
    })
    var loginflag = false;
    function ajaxlogin() {
        if (!loginflag) {
            $("#IbtnEnter").attr('disabled', 'disabled');
            $("#IbtnEnter").val('登录中..');
            $.post("", { action: 'login', user: $("#TxtUserName").val(), pwd: $("#TxtPassword").val(), VCode_hid: $("#VCode_hid").val(), vcode: $("#VCode").val(), zncode: $("#ZnCode_T").val() },
                function (data) {
                    $("#IbtnEnter").removeAttr('disabled');
                    if (data != "True") {
                        $("#IbtnEnter").val('登录');
                        $("#IbtnEnter").popover({
                            animation: true,
                            placement: 'bottom',
                            content: '<span style="color:red;"><span class="fa fa-info-circle"></span> ' + data + '!</span> <span style="color:#999">(双击隐藏)</span>',
                            html: true,
                            trigger: 'manual',
                            delay: { show: 10000, hide: 100 }
                        });
                        $("#IbtnEnter").popover('show');
                        $(".popover").dblclick(function () {
                            $("#IbtnEnter").popover('destroy');
                            event.preventDefault();
                        });
                        //setTimeout(function () { $("#IbtnEnter").popover('destroy'); }, 2000);
                    } else {
                        loginflag = true;
                        $("#RealBtn").click();
                    }
                });
            return loginflag;
        }
    }
    //-----------------------------------
    //波浪效果
    var yueAnimate = {
        lbyFun: function () {
            $(".bolang1").css({ "bottom": "-36px" });
            $(".bolang2").css({ "bottom": "0" });
            $(".bolang1").animate({
                "bottom": "0"
            }, 1500, 'easeInCubic');
            $(".bolang1").animate({
                "bottom": "-36px"
            }, 1500, 'easeInCubic');

            $(".bolang1").animate({
                "bottom": "0"
            }, 1500, 'easeInCubic');
            $(".bolang1").animate({
                "bottom": "-36px"
            }, 1500, 'easeInCubic');

            $(".bolang2").animate({
                "bottom": "-24px"
            }, 1000, 'easeInCubic');
            $(".bolang2").animate({
                "bottom": "0"
            }, 1000, 'easeInCubic');

            $(".bolang2").animate({
                "bottom": "-24px"
            }, 1000, 'easeInCubic');
            $(".bolang2").animate({
                "bottom": "0"
            }, 1000, 'easeInCubic');

            $(".bolang2").animate({
                "bottom": "-24px"
            }, 1000, 'easeInCubic');
            $(".bolang2").animate({
                "bottom": "0"
            }, 1000, 'easeInCubic');
        },

    }
    setInterval(yueAnimate.lbyFun, 6000);
    setInterval(yueAnimate.btntop, 2000);

    var gotoAnchor = function (selector, isauto) {
        var anchor = $(selector);
        if (anchor.length < 0) return;
        var $win = $(window);
        var $body = $(window.document.documentElement);
        var ua = navigator.userAgent.toLowerCase();
        if (ua.indexOf("webkit") > -1) {
            $body = $(window.document.body)
        }
        var pos = anchor.offset();
        if (isauto) {
            var t = pos.top - $win.scrollTop(); //相对于屏幕显示区
            var t2 = $win.height() - t;
            if (t2 < anchor.outerHeight()) {
                $body.animate({ "scrollTop": pos.top }, "normal");
            }
            return;
        }
        $body.animate({ "scrollTop": pos.top }, { queue: false, complete: function () { shubiao = true; } });
    }
    yueAnimate.lbyFun();
</script>
</asp:Content>