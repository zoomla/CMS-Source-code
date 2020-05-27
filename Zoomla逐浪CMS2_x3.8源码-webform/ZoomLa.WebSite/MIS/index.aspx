<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Common/Master/Empty.master" CodeFile="index.aspx.cs" Inherits="MIS_Mis" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
<title>企业面版</title>
<link href="/App_Themes/User.css" type="text/css" rel="stylesheet" />
<script type="text/javascript">
function loadPage(id, url) {
    $("#" + id).addClass("loader");
    $("#" + id).append("Loading......");
    $.ajax({
        type: "get",
        url: url,
        cache: false,
        error: function () { alert('加载页面' + url + '时出错！'); },
        success: function (msg) {
            $("#" + id).empty().append(msg);
            $("#" + id).removeClass("loader");
        }
    });
}
function show2() {
    var divs = document.getElementById("DateDiv");
    var Digital = new Date();
    var year = Digital.getFullYear();
    var months = Digital.getMonth() + 1;
    var Days = Digital.getDate();
    var hours = Digital.getHours();
    var minutes = Digital.getMinutes();
    var seconds = Digital.getSeconds();
    var dn = "AM"
    if (hours >= 12) {
        dn = "PM";
        hours = hours - 12;
    }
    if (hours == 0)
        hours = 12;
    if (minutes <= 9)
        minutes = "0" + minutes;
    if (seconds <= 9)
        seconds = "0" + seconds;
    var ctime = year + "年" + months + "月" + Days + "日 " + hours + ":" + minutes + ":" + seconds + " " + dn;
    divs.innerHTML = ctime;
    setTimeout("show2()", 1000);
}
//隐藏DIV
function HideDiv(div_id) {
    $("#" + div_id).animate({ opacity: "hide" }, 300);
}
function ShowDiv(div_id) {
    var div_obj = $("#" + div_id);
    div_obj.animate({opacity: "show", left: 15, top: 660, width: div_obj.width, height: div_obj.height }, 300);
}
setTimeout("ShowDiv('TimeDiv')", 3000);
window.onload = function () {
   // show2();//打卡提示
}
</script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div id="Meno">
<div id="M_left" class="col-lg-3 col-md-3 col-sm-3 col-xs-12"> 
<div class="User_Info thumbnail" style="background:#ffffdb;">
<div class="User_Name text-center">
<asp:Image ID="imgUserPic" runat="server" CssClass="center-block" Width="70" Height="70" />
<asp:Label ID="lblUserName" runat="server" Text="Label"></asp:Label><br />
<a href="javascript:void(0)" onclick="loadPage('Meno', '/User/Info/UserBase.aspx')" >个性设置</a>
<div class="clearfix"></div>
</div>
<div class="login_li">
<ul class="list-unstyled"	>
<li> 今天是：<%Call.Label("{ZL:DateAndTime()/}"); %></li>
<li>上次登录：<asp:Label ID="LoginTime" runat="server" Text="Label"></asp:Label></li>
</ul>
</div>
</div>
<div class="m_Focus">
<a href="#" class="list-group-item active">
<span class="pull-right">更多</span>
我的关注
</a>
<ul class="m_border list-unstyled">
<li><a href="#">我的关注我的关注我的关注我的注关注</a></li>
<li><a href="#">我的关注我的关注我的关注我的关关注</a></li>
<li><a href="#">我的关注我的关注我的关注我的关注注</a></li>
<li><a href="#">我的关注我的关注我的关注我的关注关</a></li>
</ul>
</div>
</div> 
<div class="wid"></div>
<div id="M_center" class="col-lg-6 col-md-6 col-sm-6 col-xs-12 thumbnail">
<div class="M_tit"><strong>企业看板</strong></div>
<div class="m_list">
<a href="#" class="list-group-item active">
<span class="pull-right">更多</span>
目标
</a>
<ul class="list-unstyled">
</ul>
</div>
<div class="m_list">
<a href="#" class="list-group-item active">
<span class="pull-right">更多</span>
系统提醒
</a>
<ul class="list-unstyled">
</ul>
</div>
<div class="m_list">
<a href="#" class="list-group-item active">
<span class="pull-right">更多</span>
工作历程
</a>
<ul class="list-unstyled">
</ul>
</div>
<div class="m_list">
<a href="#" class="list-group-item active">
<span class="pull-right">更多</span>
延期的任务
</a>
<ul class="list-unstyled">
</ul>
</div>
<div class="m_list">
<a href="#" class="list-group-item active">
<span class="pull-right">更多</span>
重要且紧急
</a>
<ul class="list-unstyled">
</ul>
</div>
<div class="Meno_list">
<ul>
<asp:Repeater ID="Repeater1" runat="server">
<ItemTemplate>
<li>
<a href="#"><%#Eval("Title") %></a> -  创建于  <%#Eval("CreateTime") %>
<p><%#Eval("Content") %> </p>
</li>
</ItemTemplate>
</asp:Repeater>
</ul>
</div>
</div> 
<div id="M_right" class="col-lg-3 col-md-3 col-sm-3 col-xs-12">
<div class="M_File">
<a href="#" class="list-group-item active">
<span class="pull-right">更多</span>
公告栏
</a>
<ul class="list-unstyled m_border">
<li><span class="fa fa-file"></span><a href="#"> 企业社区 2.2 发布企业社区 2.2 发布</a></li>
<li><span class="fa fa-file"></span><a href="#"> 企业社区 2.2 发布企业社区 2.2 发布</a></li>
<li><span class="fa fa-file"></span><a href="#"> 企业社区 2.2 发布企业社区 2.2 发布</a></li>
<li><span class="fa fa-file"></span><a href="#"> 企业社区 2.2 发布企业社区 2.2 发布</a></li>
<li><span class="fa fa-file"></span><a href="#"> 企业社区 2.2 发布企业社区 2.2 发布</a></li>
</ul>
</div>
<div class="M_File">
<a href="#" class="list-group-item active">
<span class="pull-right">更多</span>
最新帖子
</a>
<ul class="list-unstyled m_border">
<li><span class="fa fa-file"></span><a href="#"> 企业社区 2.2 发布企业社区 2.2 发布</a></li>
<li><span class="fa fa-file"></span><a href="#"> 企业社区 2.2 发布企业社区 2.2 发布</a></li>
<li><span class="fa fa-file"></span><a href="#"> 企业社区 2.2 发布企业社区 2.2 发布</a></li>
<li><span class="fa fa-file"></span><a href="#"> 企业社区 2.2 发布企业社区 2.2 发布</a></li>
<li><span class="fa fa-file"></span><a href="#"> 企业社区 2.2 发布企业社区 2.2 发布</a></li>
</ul>
</div>
<div class="M_File">
<a href="#" class="list-group-item active">
<span class="pull-right">更多</span>
系统文档
</a>
<ul class="list-unstyled m_border">
<li><span class="fa fa-file"></span><a href="#"> 企业社区 2.2 发布企业社区 2.2 发布</a></li>
<li><span class="fa fa-file"></span><a href="#"> 企业社区 2.2 发布企业社区 2.2 发布</a></li>
<li><span class="fa fa-file"></span><a href="#"> 企业社区 2.2 发布企业社区 2.2 发布</a></li>
<li><span class="fa fa-file"></span><a href="#"> 企业社区 2.2 发布企业社区 2.2 发布</a></li>
<li><span class="fa fa-file"></span><a href="#"> 企业社区 2.2 发布企业社区 2.2 发布</a></li>
</ul>
</div>
<div class="M_File">
<a href="#" class="list-group-item active">
<span class="pull-right">更多</span>
最近邮件
</a>
<ul class="list-unstyled m_border">
<li><span class="fa fa-file"></span><a href="#"> 企业社区 2.2 发布企业社区 2.2 发布</a></li>
<li><span class="fa fa-file"></span><a href="#"> 企业社区 2.2 发布企业社区 2.2 发布</a></li>
<li><span class="fa fa-file"></span><a href="#"> 企业社区 2.2 发布企业社区 2.2 发布</a></li>
<li><span class="fa fa-file"></span><a href="#"> 企业社区 2.2 发布企业社区 2.2 发布</a></li>
<li><span class="fa fa-file"></span><a href="#"> 企业社区 2.2 发布企业社区 2.2 发布</a></li>
</ul>
</div>
</div>
<div id="TimeDiv" class="pop_box panel panel-primary" style=" height:200px; width:250px; margin-top:-180px;">
<div class="panel-heading">
<span class="pull-right m_close" onclick="HideDiv('TimeDiv')">关闭</span>
打卡提示 
</div>
<div class="panel-body">
<div id="TimeInfo">
<div id="DateDiv" style="font-size:20px; color:#278139;font-weight:bold;text-align:left;"></div>
<div><a href="MisAttendance.aspx">进入考勤页面</a></div>
<div id="warnContent" style="height:100px; margin-top:30px; padding-left:10px;">
<div style="float:left; margin-right:20px; height:40px;">上班：<asp:Label ID="lblBegin" runat="server"></asp:Label><asp:Button ID="BtnBegin" Text="签到" OnClick="BtnBegin_Click" CssClass="btn btn-primary" runat="server" /></div>
<div style="float:left; height:40px;">下班：<asp:Label ID="lblEnd" runat="server"></asp:Label> <asp:Button ID="BtnEnd" Text="签退" OnClick="BtnEnd_Click" CssClass="btn btn-primary" runat="server" /></div>
</div>
<div class="clearfix"></div>
</div>
</div>

</div>
<div class="clearfix"></div>
</div>
</asp:Content>
