<%@ Page Language="C#" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="Design_mobile_default" MasterPageFile="~/Common/Master/Empty.master" EnableViewState="false" ValidateRequest="false"%>
<asp:Content runat="server" ContentPlaceHolderID="head">
<meta content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0;" name="viewport" />
<title><asp:Literal runat="server" ID="Title_L"></asp:Literal></title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div style="display:none;">
     <asp:Image runat="server" ID="Share_Img" />
</div>
<nav class="navbar navbar-default navbar-fixed-top mdesign_nav">
<div class="container-fluid">
<!-- Brand and toggle get grouped for better mobile display -->
<div class="navbar-header">
<button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#bs-example-navbar-collapse-1">
<span class="sr-only">Toggle navigation</span>
<span class="icon-bar"></span>
<span class="icon-bar"></span>
<span class="icon-bar"></span>
</button>
<a class="navbar-brand" href="#"><img alt="动力逐浪" src="/Template/PowerZ/style/Images/logo.svg" /></a>
<span class="mdesign_nav_t">专业逐浪微站H5开发</span>
</div>

<!-- Collect the nav links, forms, and other content for toggling -->
<div class="collapse navbar-collapse" id="bs-example-navbar-collapse-1">
<ul class="nav navbar-nav">
<li><a href="/design/user/mbsite/default.aspx"><i class="fa fa-laptop"></i> 移动微站</a></li>
<li><a href="/design/mbh5/addscence.aspx"><i class="fa fa-html5"></i> H5微场景</a></li>
<li><a href="/Class_13/Default.aspx"><i class="fa fa-shopping-bag"></i> 服务商城</a></li>
<li><a href="http://www.z01.com/mtv/"><i class="fa fa-film"></i> 免费教程</a></li>
<li><a href="/User/Default1.aspx"><i class="fa fa-user"></i> 会员中心</a></li>
<li><a href="/wxshare.aspx"><i class="fa fa-sitemap"></i> 分享推广</a></li>
</ul>
</div><!-- /.navbar-collapse -->
</div><!-- /.container-fluid -->
</nav>
<style>
.top_wrap {position:absolute;z-index:2001;background-color:#fff;width:100%;height:65px;}
.top_wrap .icon-bar {background-color:#888;display:block;}
.top_wrap .nav_subtitle {color:#999;float:right;margin-top:25px;font-size:12px;}
#topmenu_ul{background-color:rgba(203, 199, 199, 0.80);list-style-type:none;list-style:none;display:block;border-left:1px solid #fff;display:none;width:100%;}
#topmenu_ul li {float:left;color:#fff;text-align:center;height:60px;line-height:60px;width:33.3%;border-bottom:1px solid #fff;border-right:1px solid #fff;}
.mdesign_nav { margin-bottom:0; font-family:"STHeiti","Microsoft YaHei","黑体","arial"; z-index:2001; font-size:14px;}/*创建站点*/
.mdesign_nav.navbar-default .navbar-toggle:focus, .mdesign_nav.navbar-default .navbar-toggle:hover { background:none;}
.mdesign_nav .navbar-brand { padding-top:5px; padding-bottom:5px; height:auto;}
.mdesign_nav .navbar-brand img { height:40px;}
.mdesign_nav_t { float:left; margin-top:15px;}
.mdesign_nav .navbar-toggle { margin-right:0; margin-top:10px; transition: all .5s ease-in-out;}
.mdesign_nav .navbar-toggle .icon-bar { transition: all .5s ease-in-out;}
.mdesign_nav .navbar-toggle[aria-expanded="true"] {}
.mdesign_nav .navbar-toggle[aria-expanded="true"] .icon-bar:nth-of-type(2){transform:translate3d(0,6px,0) rotate(45deg)}
.mdesign_nav .navbar-toggle[aria-expanded="true"] .icon-bar:nth-of-type(3){opacity:0}
.mdesign_nav .navbar-toggle[aria-expanded="true"] .icon-bar:nth-of-type(4){transform:translate3d(0,-6px,0) rotate(-45deg)}
.mdesign_nav .navbar-collapse { padding-top:0;}
.mdesign_nav .navbar-nav { margin-top:0; margin-bottom:0;}
.mdesign_nav .navbar-nav li { float:left; width:50%; text-align:center;}
.mdesign_nav .navbar-nav li a { border-right:1px solid #ddd; border-bottom:1px solid #ddd;}
.mdesign_nav .navbar-nav li:nth-child(2n) a { border-right:none;}
</style>
<asp:Label runat="server" ID="Html_Lit"></asp:Label>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
<div runat="server" id="pop_div" visible="false" class="com_edit popup popup-edit" style="margin-top:66px;">
    <div class="content" id="pop_content"></div>
</div>
<div runat="server" id="btns_wrap" visible="false" class="com_edit">
    <div id="view_btn" class="btnlink" onclick="tools.changeMode();"><i class="fa fa-eye"></i></div>
</div>
<asp:HiddenField runat="server" ID="sitecfg_hid" />
<style type="text/css">
.page {margin-top:66px;}
</style>
<script>
    function topmenu(_this) {
        var $this = $(_this);
        if ($this.hasClass("collapsed")) {
            //展开
            $this.removeClass("collapsed");
            $("#topmenu_ul").slideDown();
        }
        else {
            $this.addClass("collapsed");
            $("#topmenu_ul").slideUp();
        }
    }
</script>
</asp:Content>
