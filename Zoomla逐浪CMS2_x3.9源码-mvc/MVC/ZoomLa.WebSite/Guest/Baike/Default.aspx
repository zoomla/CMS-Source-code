<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ZoomLaCMS.Guest.Baike.Default" MasterPageFile="~/Guest/Baike/Baike.master" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
<title>百科_<%:Call.SiteName %></title>
<script src="/JS/ICMS/ZL_Common.js"></script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div class="container">
<div class="row">
<div class="padding5">
<div class="col-lg-9 col-md-9 col-sm-9 col-xs-12 padding2 margin_top20">
<div class="baike_class">
<div class="baike_class_t"><a href="/Baike/Class.aspx" target="_blank">更多>></a>词条分类</div>
<div class="baike_class_c">
<div class="row">
<ul class="list-unstyled">
<asp:Repeater ID="BType_RPT" runat="server"  OnItemDataBound="BType_RPT_ItemDataBound">
	<ItemTemplate>
		<li class="col-lg-4 col-md-4 col-sm-4 col-xs-12"><a href="/Guest/Baike/Search.aspx?BType=<%#HttpUtility.UrlEncode(Eval("GradeName","")) %>" title="<%#Eval("GradeName") %>"><%#Eval("GradeName") %></a>
        <ul class="list-inline">
		<asp:Repeater ID="Repeater2" runat="server">
			<ItemTemplate>
				<li><a href="/Guest/Baike/Search.aspx?BType=<%#HttpUtility.UrlEncode(Eval("GradeName","")) %>" class="label label-info" title="<%#Eval("GradeName") %>"  onclick="clickTag(this); return false;"><%#Eval("GradeName") %></a></li>
			</ItemTemplate>
		</asp:Repeater> 
        </ul>
        </li>
	</ItemTemplate>
</asp:Repeater> 
</ul>
</div>
</div>
<div class="baike_class_cm"><a href="#" target="_blank">正文内容编辑指南>></a><span>编辑区域:</span><span>正文内容</span></div>
<div class="clearfix"></div>
<div class="baike_class_con">
<div class="row">
<div class="col-lg-3 col-md-3 col-sm-3 col-xs-12 ">
<ul class="list-unstyled border_right">
<%Call.Label("{ZL.Label id=\"输出指定分类百科词条\" ShowNum=\"5\" TitleNum=\"30\" /}");%>
</ul>
</div>
<div class="col-lg-3 col-md-3 col-sm-3 col-xs-12">
<ul class="list-unstyled border_right">
<%Call.Label("{ZL.Label id=\"输出指定分类百科词条\" ShowNum=\"5\" TitleNum=\"30\" /}");%>
</ul>
</div>
<div class="col-lg-3 col-md-3 col-sm-3 col-xs-12">
<ul class="list-unstyled border_right">
<%Call.Label("{ZL.Label id=\"输出指定分类百科词条\" ShowNum=\"5\" TitleNum=\"30\" /}");%>
</ul>
</div>
<div class="col-lg-3 col-md-3 col-sm-3 col-xs-12">
<ul class="list-unstyled">
<%Call.Label("{ZL.Label id=\"输出指定分类百科词条\" ShowNum=\"5\" TitleNum=\"30\" /}");%>
</ul>
</div>
</div>
</div> 
</div>
<div class="baike_hot">
<div class="baike_class_t">热词搜索</div>
<div class="baike_hot_c">
<div class="col-lg-6 col-md-6 col-sm-6 col-xs-12 padding0">
<div class="baike_hot_clist">
<ul class="list-unstyled">
<li>
<div class="baike_hot_clt">婚外情</div>
<ul class="list-unstyled">
<%Call.Label("{ZL.Label id=\"输出百科词条标题内容\" ShowNum=\"2\" TitleNum=\"30\" SynNum=\"30\" /}"); %>
</ul>
</li>
<li>
<div class="baike_hot_clt">家庭矛盾</div>
<ul class="list-unstyled">
<%Call.Label("{ZL.Label id=\"输出百科词条标题内容\" ShowNum=\"2\" TitleNum=\"30\" SynNum=\"30\" /}"); %>
</ul>
</li>
</ul>
</div>
</div>
<div class="col-lg-3 col-md-3 col-sm-3 col-xs-12 padding0">
<div class="baike_hot_mlist">
<ul class="list-unstyled">
<li>
<div class="baike_hot_clt1">亲子</div>
<ul class="list-unstyled">
<%Call.Label("{ZL.Label id=\"输出百科词条标题内容\" ShowNum=\"2\" TitleNum=\"30\" SynNum=\"30\" /}"); %>
</ul>
</li>
<li><div class="baike_hot_clt1">外遇</div></li>
<li><div class="baike_hot_clt1">外遇</div></li>
</ul>
<div class="clearfix"></div>
</div>
</div>
<div class="col-lg-3 col-md-3 col-sm-3 col-xs-12 padding0">
<div class="baike_hot_rlist">
<ul class="list-unstyled">
<li>
<div class="baike_hot_clt1">斗小三</div>
<ul class="list-unstyled">
<%Call.Label("{ZL.Label id=\"输出百科词条标题内容\" ShowNum=\"2\" TitleNum=\"30\" SynNum=\"30\" /}"); %>
</ul>
</li>
<li><div class="baike_hot_clt1">外遇</div></li>
<li><div class="baike_hot_clt1">外遇</div></li>
</ul>
<div class="clearfix"></div>
</div>
</div><div class="clearfix"></div>
</div>
</div>
<div class="baike_hun">
<div class="baike_class_t">婚姻百科</div>
<div class="baike_hun_c">
<div class="row">
<ul class="list-unstyled">
<li class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
<h3><a href="#" target="_blank">出现第三者你会怎么办？</a></h3>
<div class="row">
<div class="padding5">
<div class="col-lg-6 col-md-6 col-sm-6 col-xs-12 padding2"><a href="#" target="_blank"><img src="/Template/Defend/style/images/baike_img.jpg" onerror="shownopic(this);" alt="" /></a></div>
<div class="col-lg-6 col-md-6 col-sm-6 col-xs-12 padding2">
<p>出现第三者你会怎么办出现第三者你会怎么办出现第三者你会怎么办出现第三者你会怎么办出现第三者你会怎么办出现第三者你会怎么办出现第三者你会怎么办出现第三者你会怎么办出现第三者你会怎么者你会怎么办</p>
</div>
</div>
</div>
<div class="baike_hun_cb">
<a href="#" target="_blank">相关词条</a>
<a href="#" target="_blank">婚姻</a>
<a href="#" target="_blank">外遇</a>
<a href="#" target="_blank">婚外情</a>
<div class="clearfix"></div>
</div>
</li>
<li class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
<h3><a href="#" target="_blank">出现第三者你会怎么办？</a></h3>
<div class="row">
<div class="padding5">
<div class="col-lg-6 col-md-6 col-sm-6 col-xs-12 padding2"><a href="#" target="_blank"><img src="/Template/Defend/style/images/baike_img.jpg" onerror="shownopic(this);" alt="" /></a></div>
<div class="col-lg-6 col-md-6 col-sm-6 col-xs-12 padding2">
<p>出现第三者你会怎么办出现第三者你会怎么办出现第三者你会怎么办出现第三者你会怎么办出现第三者你会怎么办出现第三者你会怎么办出现第三者你会怎么办出现第三者你会怎么办出现第三者你会怎么者你会怎么办</p>
</div>
</div>
</div>
<div class="baike_hun_cb">
<a href="#" target="_blank">相关词条</a>
<a href="#" target="_blank">婚姻</a>
<a href="#" target="_blank">外遇</a>
<a href="#" target="_blank">婚外情</a>
<div class="clearfix"></div>
</div>
</li>
</ul>
</div>
</div>
</div>
<div class="baike_active">
<div class="baike_class_t">百科活动</div>
<div class="baike_class_c">
<div class="row">
<div class="padding5">
<div class="col-lg-6 col-md-6 col-sm-6 col-xs-12 padding2">
<div class="baike_class_cl">
<div class="baike_class_clt">参与任务，您将获得：</div>
<ul class="list-unstyled">
<li><p>更多Q币</p><span>新建或完善词条，最多可获5Q币</span></li>
<li><p>更快升级</p><span>积分翻倍，经验增长，特权加大</span></li>
<li><p>更多奖励</p><span>iPad mini大奖不定期送出，更多公仔等您拿</span></li>
</ul>
</div>
</div>
<div class="col-lg-6 col-md-6 col-sm-6 col-xs-12 padding2">
<div class="baike_class_cr">
<div class="baike_class_crt"><a href="#" target="_blank"><i class="fa fa-refresh"></i>换一组</a>待完善词条</div>
<div class="baike_class_crc">
<div class="row">
<div class="padding2">
<ul class="list-unstyled">
<asp:Repeater ID="Repeater6" runat="server" >
<ItemTemplate>
<li><span>10分钟前</span>  <a href="Details.aspx?ID=<%#Eval("ID")%>" target="_blank" title="<%#Eval("Tittle")%>"><%#Eval("Tittle")%></a> 
</li>
</ItemTemplate>
</asp:Repeater> 
<div class="clearfix"></div>
</ul>
</div>
</div>
</div>
<div class="baike_class_crt">创建新词条</div>
<div class="baike_class_crb">对推荐词条不感兴趣？赶快<a href="/Guest/Baike/CreatBaike.aspx" target="_blank">设置您的擅长领域</a>吧！</div>
</div>
</div>
</div>
</div>
</div>
</div>
</div>
<div class="col-lg-3 col-md-3 col-sm-3 col-xs-12 padding2 margin_top20">
<div class="baike_create">
<div class="row">
<div class="padding2">
<ul class="list-unstyled">
<li><a href="/Guest/Baike/CreatBaike.aspx" target="_blank"><i class="fa fa-pencil-square-o"></i>创建词条</a></li>
<li><a href="/Guest/Baike/CreatBaike.aspx" target="_blank"><i class="fa fa-file-text-o"></i>完善词条</a></li>
<li><a href="/Guest/Baike/CreatBaike.aspx" target="_blank"><i class="fa fa-star-o"></i>参加任务</a></li>
<li><a href="/Guest/Baike/CreatBaike.aspx" target="_blank"><i class="fa fa-gift"></i>兑换奖品</a></li>
</ul>
</div>
</div>
</div>
<div class="baike_dynamic">
<div class="baike_class_t">新闻动态</div>
<ul class="list-unstyled">
<asp:Repeater runat="server" ID="Latestrnews">
<ItemTemplate>
<li><span class="pull-right"><%#Eval("CreateTime","{0:MM-dd}") %></span> <a href='/Item/<%#Eval("GeneralID") %>.aspx' target="_blank"><%#GetLeftString(Eval("Title").ToString()) %></a></li>
</ItemTemplate>
</asp:Repeater>
</ul>
</div>
<div class="baike_history">
<div class="baike_class_t">历史上的今天</div>
<div class="baike_history_c">
<div id="carousel-example-generic" class="carousel slide" data-ride="carousel">
<div class="carousel-inner" role="listbox">
<div class="item active">
<div class="baike_history_ct">01月-16日</div>
<div class="baike_history_con">
<ul class="list-unstyled">
<%Call.Label("{ZL.Label id=\"输出百科标题\" NodeID=\"76\" TitleNum=\"30\" ShowNum=\"4\" /}"); %>
</ul>
</div>
</div>
<div class="item">
<div class="baike_history_ct">01月-17日</div>
<div class="baike_history_con">
<ul class="list-unstyled">
<%Call.Label("{ZL.Label id=\"输出百科标题\" NodeID=\"10\" TitleNum=\"30\" ShowNum=\"4\" /}"); %>
</ul>
</div>
</div>
<div class="item">
<div class="baike_history_ct">01月-18日</div>
<div class="baike_history_con">
<ul class="list-unstyled">
<%Call.Label("{ZL.Label id=\"输出百科标题\" NodeID=\"11\" TitleNum=\"30\" ShowNum=\"4\" /}"); %>
</ul>
</div>
</div>
</div>
<a class="left carousel-control" href="#carousel-example-generic" role="button" data-slide="prev">
<span class="fa fa-chevron-left"></span>
<span class="sr-only">Previous</span>
</a>
<a class="right carousel-control" href="#carousel-example-generic" role="button" data-slide="next">
<span class="fa fa-chevron-right"></span>
<span class="sr-only">Next</span>
</a>
</div>
</div>
</div>
<div class="baike_star">
<div class="baike_class_t">百科用户</div>
<div class="baike_star_c">
<div class="baike_star_ct"><span>百科之星</span></div>
<ul class="list-unstyled">
<asp:Repeater ID="Repeater5" runat="server" >
<ItemTemplate>
<li>
<div class="row">
<div class="padding5">
<div class="col-lg-3 col-md-3 col-sm-3 col-xs-12 padding2">
    <a href="/Baike/Search.aspx?UserID=<%#Eval("UserID") %>" target="_blank"><img src="<%#Eval("UserFace") %>" onerror="shownoface(this);" alt="<%#Eval("UserName") %>" /></a>
</div>
<div class="col-lg-9 col-md-9 col-sm-9 col-xs-12 padding2">
<h6><span><%#Container.ItemIndex+1 %></span><span><%#Eval("UserName") %></span></h6>
<p>上周经验：<span><%#Eval("UserExp") %></span></p>
</div>
</div>
</div>
</li>
</ItemTemplate>
</asp:Repeater>
</ul>
</div>
</div>
</div>
</div>
</div>
</div>
<div class="container">
<div class="baike_explain">
<div class="baike_explain_t">新手教程·学习如何编辑词条</div>
<div class="row">
<ul class="list-unstyled">
<li class="col-lg-3 col-md-3 col-sm-3 col-xs-12">
<div>
<h6><a href='<%Call.Label("{ZL:GetInfoUrl(541)/}");%>' target="_blank">初窥门径</a></h6>
<p><a href='<%Call.Label("{ZL:GetInfoUrl(541)/}");%>' target="_blank">开始编辑词条</a></p>
</div>
</li>
<li class="col-lg-3 col-md-3 col-sm-3 col-xs-12">
<div>
<h6><a href="<%Call.Label("{ZL:GetInfoUrl(542)/}");%>" target="_blank">粗通皮毛</a></h6>
<p><a href="<%Call.Label("{ZL:GetInfoUrl(542)/}");%>" target="_blank">在正文中设置目录、添加表格模块</a></p>
</div>
</li>
<li class="col-lg-3 col-md-3 col-sm-3 col-xs-12">
<div>
<h6><a href="<%Call.Label("{ZL:GetInfoUrl(543)/}");%>" target="_blank">画龙点睛</a></h6>
<p><a href="<%Call.Label("{ZL:GetInfoUrl(543)/}");%>" target="_blank">在正文中插入图片</a></p>
</div>
</li>
<li class="col-lg-3 col-md-3 col-sm-3 col-xs-12">
<div>
<h6><a href="<%Call.Label("{ZL:GetInfoUrl(544)/}");%>" target="_blank">登堂入室</a></h6>
<p><a href="<%Call.Label("{ZL:GetInfoUrl(544)/}");%>" target="_blank">保存草稿箱、预览词条和提交词条</a></p>
</div>
</li>
</ul>
</div>
</div>
</div>
<div class="baike_bottom">
<div class="container">
<div class="row">
<div class="col-lg-10 col-md-10 col-sm-10 col-xs-12 col-md-offset-1">
<div class="row">
<div class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
<div class="baike_bottom_list">
<div class="baike_bottom_lt"><i class="fa fa-book"></i>新手上路</div>
<ul class="list-unstyled">
<li><a href="<%Call.Label("{ZL:GetInfoUrl(545)/}");%>" target="_blank">成长任务</a></li>
<li><a href="<%Call.Label("{ZL:GetInfoUrl(546)/}");%>" target="_blank">编辑入门</a></li>
<li><a href="<%Call.Label("{ZL:GetInfoUrl(547)/}");%>" target="_blank">编辑规则</a></li>
<li><a href="<%Call.Label("{ZL:GetInfoUrl(548)/}");%>" target="_blank">百科术语</a></li>
<div class="clearfix"></div>
</ul>
</div>
</div>
<div class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
<div class="baike_bottom_list">
<div class="baike_bottom_lt"><i class="fa fa-question-circle"></i>我有疑问</div>
<ul class="list-unstyled">
<li><a href="<%Call.Label("{ZL:GetInfoUrl(549)/}");%>" target="_blank">常见问题</a></li>
<li><a href="<%Call.Label("{ZL:GetInfoUrl(550)/}");%>" target="_blank">我要提问</a></li>
<li><a href="<%Call.Label("{ZL:GetInfoUrl(551)/}");%>" target="_blank">参加讨论</a></li>
<li><a href="<%Call.Label("{ZL:GetInfoUrl(552)/}");%>" target="_blank">意见反馈</a></li>
<div class="clearfix"></div>
</ul>
</div>
</div>
<div class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
<div class="baike_bottom_list">
<div class="baike_bottom_lt"><i class="fa fa-comments"></i>投诉建议</div>
<ul class="list-unstyled">
<li><a href="<%Call.Label("{ZL:GetInfoUrl(553)/}");%>" target="_blank">举报不良信息</a></li>
<li><a href="<%Call.Label("{ZL:GetInfoUrl(554)/}");%>" target="_blank">未通过词条申诉</a></li>
<li><a href="<%Call.Label("{ZL:GetInfoUrl(555)/}");%>" target="_blank">投诉侵权信息</a></li>
<li><a href="<%Call.Label("{ZL:GetInfoUrl(556)/}");%>" target="_blank">封禁查询与解封</a></li>
<div class="clearfix"></div>
</ul>
</div>
</div>
</div>
</div>
</div>
<div class="baike_copy"><%Call.Label("{$Copyright/}"); %></div>
</div>
</div>
<div class="top hidden">
<div class="topnav" id="toptab">
<div class="topleft">
<span  style="<%=getstyle()%>"> 您好！<a href="/user/" target="_blank"></a> 欢迎来<%Call.Label("{$SiteName/}"); %>问答系统！ [<a href="<%=ResolveUrl("~/User/logout.aspx") %>?url=/Ask">退出登录</a>]</span>
<span  style="<%=getstyles()%>">[<a  href="/user/Login.aspx?ReturnUrl=/guest">请登录</a>] [<a  href="../../user/register.aspx?ReturnUrl=/guest">免费注册</a>]</span>
</div>
<div class="topright" >
<a href="/">返回首页</a>
<a href="#" onclick="this.style.behavior='url(#default#homepage)';this.setHomePage(location.href);">设为首页</a>
<a href="#" onclick="window.external.AddFavorite(location.href,document.title)" style="cursor:pointer;color:blue">收藏本站</a>
</div>
</div>
</div>
<div class="hidden"> 
<div id="s_header">
<div id="s_search" class="wa_mode" wa_mode="top.search">
<h1 id="s_logo"><a href="/Baike" title="到<%Call.Label("{$SiteName/}"); %>百科首页"><img width="150" src="<%Call.Label("{$LogoUrl/}"); %>" alt="到<%Call.Label("{$SiteName/}"); %>百科首页" /> </a></h1> 
<div style="display:none;">
<input type="hidden" name="formids" value="searchText,enterLemma,searchLemma"/>
<input type="hidden" name="submitmode" value=""/>
<input type="hidden" name="submitname" value=""/>
</div>
<div class="s_search_form">
</div> 
<div id="divc" class="smartbox"  style="visibility: hidden;"></div>
</div>
</div> 
<div id="container">
<div id="nav">
<ul class="tittleul">
<li><a href="/Baike">百科首页</a></li>
<li><a href="/Guest/Baike/Search.aspx??btype=<%=Server.UrlEncode("自然") %>">自然</a></li>
<li><a href="/Guest/Baike/Search.aspx?btype=<%=Server.UrlEncode("文化") %>">文化</a></li>
<li><a href="/Guest/Baike/Search.aspx?btype=<%=Server.UrlEncode("物理") %>">地理</a></li>
<li><a href="/Guest/Baike/Search.aspx?btype=<%=Server.UrlEncode("历史") %>">历史</a></li>
<li><a href="/Guest/Baike/Search.aspx?btype=<%=Server.UrlEncode("生活") %>">生活</a></li>
<li><a href="/Guest/Baike/Search.aspx?btype=<%=Server.UrlEncode("社会") %>">社会</a></li>
<li><a href="/Guest/Baike/Search.aspx?btype=<%=Server.UrlEncode("艺术") %>">艺术</a></li>
<li><a href="/Guest/Baike/Search.aspx?btype=<%=Server.UrlEncode("人物") %>">人物</a></li>
<li><a href="/Guest/Baike/Search.aspx?btype=<%=Server.UrlEncode("经济") %>">经济</a></li>
<li><a href="/Guest/Baike/Search.aspx?btype=<%=Server.UrlEncode("科技") %>">科技</a></li>
<li><a href="/Guest/Baike/Search.aspx?btype=<%=Server.UrlEncode("体育") %>">体育</a></li>
</ul>
</div>
<div class="L1">
<!-- 特色词条 -->
<span ch="bk1">
<asp:Repeater ID="Repeater8" runat="server">
<ItemTemplate>
<div class="tod_int2">
<div class="tod_int2_m">
<a href="/v28762.htm" target="_blank">
<img src="http://pic.baike.z01.com/p/20120810/bki-20120810123653-802421712.jpg" border="0" /></a>
<div class="r">
<h2>特色词条： <a href="Details.aspx?ID=<%#Eval("ID")%>" target="_blank"><%#Eval("Tittle")%></a> </h2>
<p class="wordbreak">
<%#NoHTML(Eval("Contents","{0}").ToString())%>...
</p>
<a href="Details.aspx?ID=<%#Eval("ID")%>" target="_blank">阅读全文&gt;&gt;</a>
</div>
</div>
</div>
</ItemTemplate>
</asp:Repeater>
</span>
<!-- 每日图片 -->
<span ch="bk3">
<div class="entry3 ft_l">
<h3 class="entry_tit3">每日图片</h3>
<div class="entry_con3">
<asp:Repeater runat="server" ID="picture">
<ItemTemplate>
<div class="entry_con3">
<a href="Details.aspx?ID=<%#Eval("ID")%>" target="_blank">
<img src="http://pic.baike.z01.com/p/20120810/bki-20120810125323-1670790846.jpg" border="0" /></a>
<div class="r">
<h4><a href="Details.aspx?ID=<%#Eval("ID")%>" target="_blank"><%#Eval("Tittle")%></a> </h4>
<br />
<p class="wordbreak">
<%#NoHTML(Eval("contents","{0}").ToString())%>...
</p>
<br />
<a href="Details.aspx?ID=<%#Eval("ID")%>" target="_blank">查看全部&gt;&gt;</a>
</div>
</div>
</ItemTemplate>
</asp:Repeater>
</div>
</div>
</span>
<span ch="bk4">
<div class="entry3 ft_r">
<h3 class="entry_tit2"><span class="ft_1">你知道吗</span></h3>
<ul class="entry_con9" ch="bk6">
<asp:Repeater runat="server" ID="Youkown">
<ItemTemplate>
<li>
<span style="float: right"><a href="Details.aspx?ID=<%#Eval("ID")%>" target="_blank">详情</a></span>
<a href="Details.aspx?ID=<%#Eval("ID")%>" target="_blank"><%#Eval("Tittle")%></a>: <%#NoHTML(Eval("Brief").ToString())%>...</li>
</ItemTemplate>
</asp:Repeater>
</ul>
</div>
</span>
<div class="k"></div>
<!-- 优质词条 -->
<span ch="bk5">
<div class="entry9 ft_l">
<h3 class="entry_tit8"><span class="ft_l">优质词条</span></h3>
<ul class="entry_con9" ch="bk6">
<asp:Repeater ID="Elite_RPT" runat="server">
<ItemTemplate>
<li>
<span style="float: right;"><a href="Details.aspx?ID=<%#Eval("ID") %>" target="_blank">查看全部&gt;&gt;</a></span>
<a href="Details.aspx?ID=<%#Eval("ID") %>" target="_blank"><%#Eval("Tittle")%></a> <%#NoHTML(Eval("contents","{0}").ToString())%>...
</li>
</ItemTemplate>
</asp:Repeater>
</ul>
</div>
</span>
<div id="imperfect" class="entry9 ft_r">
<span ch="bk5">
<div class="entry_tit9">
<ul>
<li class="current">待完善词条</li>
<li><a onclick="showToBeCreated_OnClick()">待创建词条</a></li>
</ul>
<a href="Search.aspx?filter=incomplete" class="more" target="_blank">更多&gt;&gt;</a>
</div>
<ul class="entry_con9" ch="bk6">
</ul>
</div>
<div id="toBeCreated" class="entry9 ft_r" style="display: none">
<div class="entry_tit9">
<ul>
<li><a onclick="showImperfect_OnClick()">待完善词条</a></li>
<li class="current">待创建词条</li>
</ul>
<a href="CreatBaike.aspx" class="more" target="_blank">更多&gt;&gt;</a>
</div>
<ul class="entry_con11" ch="bk6">
<asp:Repeater ID="Repeater7" runat="server">
<ItemTemplate>
<li><a href="Details.aspx?ID=<%#Eval("ID") %>" target="_blank"><%#Eval("Tittle")%></a>
</li>
</ItemTemplate>
</asp:Repeater>
</ul>
</div>
<span>
<div id="showday" class="entry9 ft_l">
<div class="entry_tit9">
<ul>
<li><a href="/Guest/Baike/Search.aspx?btype=<%=Server.UrlEncode("人物") %>" target="_blank">人物百科</a></li>
</ul>
<a href="/Guest/Baike/Search.aspx?btype=<%=Server.UrlEncode("人物") %>" class="more" target="_blank">更多&gt;&gt;</a>
</div>
<ul class="entry_con9" ch="bk6">
<asp:Repeater runat="server" ID="mans">
<ItemTemplate>
<li>
<span style="float: right"><a href="Details.aspx?ID=<%#Eval("ID")%>" target="_blank">详情</a></span>
<a href="Details.aspx?ID=<%#Eval("ID")%>" target="_blank"><%#Eval("Tittle")%></a>: <%#NoHTML(Eval("Brief").ToString())%>...</li>
</ItemTemplate>
</asp:Repeater>
</ul>
</div>
</span>
<div id="Div1" class="entry9 ft_r">
<div class="entry_tit9"><a href="#" onclick="showday()">历史上的今天</a></div>
<ul class="entry_con9" ch="bk6">
<asp:Repeater runat="server" ID="Repeater9">
<ItemTemplate>
<li>
<span style="float: right"><a href="Details.aspx?ID=<%#Eval("ID")%>" target="_blank">详情</a></span>
<a href="Details.aspx?ID=<%#Eval("ID")%>" target="_blank"><%#Eval("Tittle")%></a>: <%#NoHTML(Eval("Brief").ToString())%>...</li>
</ItemTemplate>
</asp:Repeater>
</ul>
</div>
<script type="text/javascript">
    $(function () {
        $("#top_nav_ul li[title='百科首页']").addClass("active");
    })
function showDiv(id) {
var div = document.getElementById(id);
if (div)
div.style.display = 'block';
}

function hideDiv(id) {
var div = document.getElementById(id);
if (div)
div.style.display = 'none';
}

function showToBeCreated_OnClick() {
hideDiv('imperfect');
showDiv('toBeCreated');
}

function showImperfect_OnClick() {
hideDiv('toBeCreated');
showDiv('imperfect');
}

function showday() {
var m = new Date();
var mon = m.getMonth() + 1;
var day = m.getDate()
var today = mon + "月" + day + "日";
window.open("Details.aspx?tittle=" + today, 'newwindow', "", "");
}
</script>
<div class="k"></div>
</div>
<div class="R1">
<a target="_blank" href="CreatBaike.aspx">
<div class="bt2" style="margin-top: 6px;" title="创建词条">
<div class="inner_bt2">
<div class="icon23_1"></div>
创建词条
</div>
</div>
</a>
<span ch="bk7">
<div class="entry6">
<h3 class="entry_tit6">新闻动态</h3>
<ul class="entry_con6">
</ul>
</div>
</span>

<span ch="bk8">
<div class="entry7">
<h3 class="entry_tit7">关注事件</h3>
<ul class="list6">
<asp:Repeater ID="Repeater3" runat="server">
<ItemTemplate>
<li><span class="l"><span class="strong"><%#Container.ItemIndex+1 %></span><a href='Details.aspx?ID=<%#Eval("ID") %>' target="_blank"><%#Eval("Tittle")%></a></span><div class="icon18_1"></div>
</li>
</ItemTemplate>
</asp:Repeater>
</ul>
</div>
</span>
<div class="sideColumnWrap">
<div class="titleWrap">
<h3>上周经验飙升榜</h3>
</div>
<div class="sideColumnList"></div>
</div>
</div>
<div class="k"></div>
</div>
</div>
<script>
    //检测固顶事件
    var IO = document.getElementById('baike_scolls'), Y = IO, H = 0, IE6;
    IE6 = window.ActiveXObject && !window.XMLHttpRequest;
    while (Y) { H += Y.offsetTop; Y = Y.offsetParent };
    if (IE6)
        IO.style.cssText = "position:absolute;top:expression(this.fix?(document" +
        ".documentElement.scrollTop-(this.javascript||" + H + ")):0)";
    window.onscroll = function () {
        var d = document, s = Math.max(d.documentElement.scrollTop, document.body.scrollTop);
        if (s > H && IO.fix || s <= H && !IO.fix) return;
        if (!IE6) IO.style.position = IO.fix ? "" : "fixed";
        IO.fix = !IO.fix;
    };
    try { document.execCommand("BackgroundImageCache", false, true) } catch (e) { };
    //]]>
</script>
</asp:Content>
