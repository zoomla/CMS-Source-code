<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" MasterPageFile="~/Common/Master/Empty.master" Inherits="MIS_Default" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
<title>MIS能力中心-首页</title>
<link href="/App_Themes/User.css" type="text/css" rel="stylesheet" />
<script type="text/javascript" src="/JS/FrameTab.js"></script>
<%--<script type="text/javascript" src="/JS/AdminIndex.js"></script>--%>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div id="header" style="position:inherit;">
<div class="container" style="position:relative;">
<div class="M_top_r_t">
<ul class="list-inline pull-right">
<li><a href="<%=CustomerPageAction.customPath2 %>login.aspx" target="_blank">系统管理</a></li>
<li><a href="#">购买帮助</a></li>
<li><a href="/user/logout.aspx?url=/Mis/">退出</a></li>
</ul>
</div>
<div class="col-lg-3 col-md-3 col-sm-3 col-xs-12"> 
<div id="Mis_logo"><%--<img src="" alt="" />--%> <%Call.Label("{$SiteName/}"); %>  </div>
</div>   
<div class="col-lg-9 col-md-9 col-sm-9 col-xs-12">
<div id="M_top_r"> 
<div class="M_top_r_b">
<ul class="list-inline pull-right">
<li><a href="/Mis/" id="hd" ><i class="fa fa-home"></i><br />面板</a></li>
<li><a href="javascript:ShowMain1('0','/Plat/');" id="hd0" ><i class="fa fa-comments"></i><br />沟通</a></li>
<li><a href="javascript:ShowMain1('1','/Mis/Target/');" id="hd1"><i class="fa fa-info-circle"></i><br />目标</a></li>
<li><a href="javascript:ShowMain1('2','/Mis/Daily/');" id="hd2"><i class="fa fa-edit"></i><br />日志</a></li>
<li><a href="javascript:ShowMain1('3','/Mis/Memo/');" id="hd3"><i class="fa fa-list-alt"></i><br />备忘</a></li>
<li><a href="javascript:ShowMain1('4','/Plat/Blog/DailyPlan.aspx');" id="hd4"><i class="fa fa-pie-chart"></i><br />计划</a></li>
<li><a href="javascript:ShowMain1('5','/Plat/Task/');" id="hd5"><i class="fa fa-tasks"></i><br />任务</a></li>
<li><a href="javascript:ShowMain1('6','/Plat/Blog/Project.aspx');" id="hd6"><i class="fa fa-file-text-o"></i><br />项目</a></li>
<li><a href="javascript:ShowMain1('7','/Mis/Approval/');" id="hd7"><i class="fa fa-sun-o"></i><br />审批</a></li>
<%--    <li><a href="javascript:ShowMain('','/Mis/Approval/');" id="A1"><img src="/App_Themes/UserThem/images/Mis/j_ico_approve.png" alt="公文" /><br />公文</a></li>--%>
<li><a href="javascript:void(0)" onClick="showd('t_pop_navli')"><i class="fa fa-ellipsis-h"></i><br />更多</a></li>
</ul>
</div>  
<div class="t_pop_navli" id="t_pop_navli" style="display:none;"> 
<div class="bgt"></div>
<div class="bgm">
<ul> 
<li class="j_ico_docu"><a href="javascript:void(0)" onClick="loadPage('Meno', '/Mis/File/')"  ><i class="fa fa-file-word-o"></i><br />文档</a></li>
<li class="j_ico_know"><a href=""><i class="fa fa-book"></i><br />知识</a></li>
<li class="j_ico_discuss"><a href=""><i class="fa fa-comments"></i><br />讨论</a></li>
<li class="j_ico_attent"><a href=""><i class="fa fa-eye"></i><br />关注</a></li>
<li class="j_ico_attend"><a href="/Mis/MisAttendance.aspx"><i class="fa fa-sign-in"></i><br />考勤</a></li>
<li class="j_ico_twitter"><a href=""><i class="fa fa-weibo"></i><br />微博</a></li> 
<li class="j_ico_bbs"><a href=""><i class="fa fa-bars"></i><br />论坛</a></li> 
<li class="j_ico_notice"><a href="javascript:void(0)" onClick="loadPage('Meno', '/Class_76/NodePage.aspx')"><i class="fa fa-bookmark"></i><br />公告</a></li> 
<li class="j_ico_address"><a href=""><i class="fa fa-eraser"></i><br />通讯录</a></li> 
<!--<li class="j_ico_exprience"><a href="">工作历程</a></li> -->
<li class="j_ico_sysremd"><a href="javascript:void(0)" onClick="loadPage('Meno', '/user/Message/Message.aspx')"><i class="fa fa-volume-up"></i><br />系统提醒</a></li> 
<li class="j_ico_relacomp"><a href=""><i class="fa fa-users"></i><br />友好企业</a></li> 
<li class="j_ico_online"><a href="javascript:window.open('/OnlineService/ShowForm.aspx','','width=640,height=550,top=200,left=300,resizable=yes');void(0);"><i class="fa fa-headphones"></i><br />在线客服</a></li>   
<li class="j_ico_site"><a href="/admin/Template/CloudLead.aspx" target="_Blank"><i class="fa fa-sitemap"></i><br />建站通</a></li> 
<li class="j_ico_cloud"><a  href="javascript:void(0)" onClick="loadPage('Meno', '/Class_105/NodePage.aspx')"><i class="fa fa-cloud"></i><br />云盘</a></li>
</ul><div style="clear:both;display:block;"></div>
</div>
<div class="bgb"></div>
</div>  
<div class="clear"></div>
</div>
</div>
</div>
</div>
<div class="m_tabs" ><!--顶部Tab--> 
<div class="FrameTabs_bg">
<div class="container">
<div id="FrameTabs" style="overflow: hidden; width: 100%;">
<div class="tab-strip-wrap">
<ul class="tab-strip pull-left">
<li class="current" id="iFrameTab1"><a href="javascript:"><span id="frameTabTitle">我的工作台</span></a><a class="closeTab"><span class="fa fa-minus-circle"></span></a>
</li>
<li id="newFrameTab"><a title="新选项卡" href="javascript:"><span class="fa fa-plus"></span></a></li>
</ul>
</div>
</div>
</div> 
</div> 
</div> 
<!-- 书签结束 -->
<div class="container">
<div class="thumbnail">
<div id="main_right_frame">
<iframe src="index.aspx"  marginheight="0" marginwidth="0" frameborder="0" scrolling="no" width="100%" height=100% id="main_right" name="main_right" onLoad="iFrameHeight(this)" ></iframe>
<div class="clearfix"></div>
</div>
</div>
</div> 
<!--Tab切换条-->
<div class="container">
<div id="iframeGuideTemplate" style="display: none;">
<iframe style="z-index: 2; visibility: inherit; width: 100%;" src="about:blank" frameborder="0" tabid="0"></iframe><div class="clearfix"></div>
</div>
<div id="iframeMainTemplate" style="display: none">
<iframe style="z-index: 2; visibility: inherit; overflow-x: hidden; width: 100%;" src="about:blank" frameborder="0" scrolling="yes" onload="SetTabTitle(this)" tabid="0"></iframe><div class="clearfix"></div>
</div><div class="clearfix"></div>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
<script>
function showd(obj) {
	var dr = document.getElementById(obj).style.display;
	if (dr == "none") {
		document.getElementById(obj).style.display = "block";
	}
	else {
		document.getElementById(obj).style.display = "none";
	}
}
document.getElementById("hd").className = "HidName";

window.onresize = setLayout; //窗口改变大小的时候，调用setLayout方法 
document.body.scroll = "no";
function setLayout() {
	document.getElementById("main_right_frame").style.width = document.documentElement.clientWidth - 205;
	document.getElementById("frmRtd").height = document.documentElement.clientHeight - 100;
	document.getElementById("main_right").height = document.getElementById("frmRtd").height - 30;
}
setLayout();
function iFrameHeight(obj)
{
	var ifm= document.getElementById("main_right");
	var subWeb = document.frames ? document.frames["main_right"].document :
	ifm.contentDocument;
	if(ifm != null && subWeb != null)
	{
		ifm.height = subWeb.body.scrollHeight;
	}
	SetTabTitle(this);
}
</script>
</asp:Content>

