<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DefaultTop.ascx.cs" Inherits="User_ASCX_DefaultTop" %>
<div class="user_mimenu user_home_mimenu hidden-xs">
<div class="navbar navbar-fixed-top" role="navigation">
<button type="button" class="btn btn-default" id="mimenu_btn">
<span class="icon-bar"></span>
<span class="icon-bar"></span>
<span class="icon-bar"></span>
</button>
<div class="user_mimenu_left">
<ul class="list-unstyled">
<li><a href="/" target="_blank">首页</a></li>
<li><a href="/Home" target="_blank">能力</a></li>
<li><a href="/Index" target="_blank">社区</a></li>
<li><a href="/Ask" target="_blank">问答</a></li>
<li><a href="/Guest" target="_blank">留言</a></li>
<li><a href="/Baike" target="_blank">百科</a></li>
<li><a href="/Office" target="_blank">OA</a></li>
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
<div class="navbar-fixed-top u_fixed">
<div class="container u_top">
<div class="row">
<div class="col-lg-4 col-md-4 col-sm-4 col-xs-7">
<a href="/" target="_blank"><img src="<%Call.Label("{$LogoUrl/}"); %>" class="img-responsive" alt="<%Call.Label("{$SiteName/}"); %>" /></a>
</div>
<div class="col-lg-8 col-md-8 col-sm-8 col-xs-5 u_search">
<div class="u_top_btn">
<ul class="list-inline pull-right">
<li class="hidden-xs"><div class="u_search_form"><input type="text" name="key" id="key" class="form-control" placeholder="快速搜索" onKeyDown="return IsEnter(this);" /><i class="fa fa-search" id="sub_btn" title="可按回车键快速检索"></i></div></li>
<li class="text-center hidden-xs"><i class="fa fa-user"></i><asp:Label ID="uName" runat="server"></asp:Label></li>
<li class="text-center"><a href="/User/Info/UserBase.aspx"><i class="fa fa-cog"></i></a></li>
<li class="text-center"><a href="/User/LogOut.aspx">退出</a></li>
</ul>
</div>
</div> 
</div>
</div>
<div class="u_nav">
<nav class="navbar navbar-default">
  <div class="container">
    <!-- Brand and toggle get grouped for better mobile display -->
    <div class="navbar-header">
      <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#bs-example-navbar-collapse-1">
        <span class="sr-only">Toggle navigation</span>
        <span class="icon-bar"></span>
        <span class="icon-bar"></span>
        <span class="icon-bar"></span>
      </button>
      <a class="navbar-brand visible-xs" href="#">快速导航</a>
    </div>
    <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-1">
      <ul class="nav navbar-nav">
        <li id="nav_home" class="active"><a href="/User/Default.aspx">首页</a></li>
        <li id="nav_content"><a href="/User/Content/MyContent.aspx">内容</a></li>
        <li id="nav_shop"><a href="/User/UserShop/Default.aspx">商城</a></li>
        <li id="nav_edu"><a href="/User/Exam/QuestList.aspx">教育</a></li> 
        <li id="nav_index"><a href="/User/UserZone/Structure.aspx">社区</a></li>
        <li id="nav_page"><a href="/User/Pages/Default.aspx">黄页</a></li>
        <li id="nav_cloud"><a href="/User/CloudManage.aspx?type=file">云盘</a></li>
        <li><a href="/Site/Default.aspx" target="_blank">建站</a></li>
        <li id="nav_office"><a href="/Office">办公</a></li>
        <li id="nav_ability"><a href="/Home">能力</a></li>
        </ul>
    </div>
  </div>
</nav>
</div>
</div>