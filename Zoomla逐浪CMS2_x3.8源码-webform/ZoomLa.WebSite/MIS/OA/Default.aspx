<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="MIS_ZLOA_Default" MasterPageFile="~/User/Empty.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>OA办公系统</title>
    <meta name="renderer" content="ie-comp" />
    <link href="/dist/css/font-awesome.min.css" rel="stylesheet" />
    <script src="/JS/calendar-brown.js"></script>
    <script src="/JS/ICMS/OAMain.js"></script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div id="OAtop">
	<div id="OAtop_l">
		<div id="logo" class="OA_logo"><a href="/MIS/OA/">OA办公系统</a></div>
		<div class="logo_tip"><span><asp:Label runat="server" ID="UName_L"></asp:Label></span>,欢迎回来!</div>
	</div>            
	<div id="OAtop_r">
	<ul class="list-unstyled">
	<li><a href="/User/CloudManage.aspx?Type=file" target="_blank"><span class="fa-stack fa-lg"><i class="fa fa-circle fa-stack-2x"></i><i class="fa fa-cloud fa-stack-1x fa-inverse"></i></span><br />网络云盘</a></li>
	<li><a href="/Plat/Blog/" target="_blank"><span class="fa-stack fa-lg"><i class="fa fa-circle fa-stack-2x"></i><i class="fa fa-envelope-o fa-stack-1x fa-inverse"></i></span><br />工作流</a></li>
	<li><a href="#"><span class="fa-stack fa-lg"><i class="fa fa-circle fa-stack-2x"></i><i class="fa fa-volume-down fa-stack-1x fa-inverse"></i></span><br />会议通知</a></li>
	<li><a href="javascript:;" onclick="ShowMain('','/Mis/OA/Flow/ApplyList.aspx')"><span class="fa-stack fa-lg"><i class="fa fa-circle fa-stack-2x"></i><i class="fa fa-list-alt fa-stack-1x fa-inverse"></i></span><br />信息管理</a></li>
	<li><a href="/ask" target="_blank"><span class="fa-stack fa-lg"><i class="fa fa-circle fa-stack-2x"></i><i class="fa fa-sitemap fa-stack-1x fa-inverse"></i></span><br />问答中心</a></li>
	<li><a href="/User/"><span class="fa-stack fa-lg"><i class="fa fa-circle fa-stack-2x"></i><i class="fa fa-user fa-stack-1x fa-inverse"></i></span><br />会员中心</a></li>
	</ul>
	</div>
</div>
<div class="naver">
<div id="nav">
<ul class="nav_ull">
<li><a href="javascript:;" onclick="ShowMain('#userinfo_ul','/Mis/OA/Main.aspx');">OA首页</a></li>
<li><a href="javascript:;" onclick="ShowMain('','/Mis/OA/Flow/ApplyList.aspx?view=1');">待办事宜</a></li>
<%--<li><a href="#">日程安排</a></li>--%>
<li><a href="javascript:;" onclick="ShowMain('','/Mis/OA/Mail/MailManage.aspx?url=Message.aspx');">邮箱</a></li>
<li><a href="javascript:;" onclick="ShowMain('','/Mis/OA/UserConfig.aspx');">配置</a></li> 
<li><a href="http://code.z01.com/webOffice.rar">组件下载</a></li>              
</ul>
<ul class="nav_ulr">
<li><a href="/Mis/OA/" title="首页"><i class="fa fa-home"></i></a></li> 
<%--   <li><a href="#"><i class="fa fa-wechat"></i></a><span>2</span></li>--%>
<li><a href="javascript:;" onclick="ShowMain('','/mis/oa/Other/StructList.aspx?action=struct&value=2');" title="组织结构"><i class="fa fa-group"></i></a></li>        
<%--<li><a href="http://app.z01.com/" title="微应用" target="_blank"><i class="fa fa-weixin"></i></a></li>--%>
<li><a href="javascript:;" onclick="ShowMain('','/Mis/OA/UserConfig.aspx');" title="设置"><i class="fa fa-gear"></i></a></li>  
<li title="退出"><a href="/User/logout.aspx?url=/Mis/OA/" title="退出"><i class="fa fa-power-off"></i></a></li> 
</ul>
</div>
</div>
<div class="oamain">
	<table style="width: 100%;" cellpadding="0" cellspacing="0">
		<tr>
			<td id="toolbar1" style="width: 260px;" valign="top">
				<div class="oamain_left">
                    <iframe id="main_left" style="z-index: 2; visibility: inherit; width: 100%;height:700px;" name="main_right" src="/Mis/OA/Menu/LeftMenu.aspx?leftul=userinfo_ul" frameborder="0"></iframe>			
                    <div class="clearfix"></div>
				</div>
			</td>
			<td class="switchPoint" style="width: 10px; background: #ececff;" onclick="hiddendiv()">
				<img id="switchPoint" src="/App_Themes/Admin/butClose.gif" alt="关闭左栏" />
			</td>
			<td valign="top">
				<iframe id="main_right" style="z-index: 2; visibility: inherit; width: 100%;" name="main_right" src="/Mis/OA/Main.aspx" frameborder="0"></iframe>
			</td>
		</tr>
	</table>
	<div id="TimeDiv" class="pop_box panel panel-primary " style="display: none; height: 200px; width: 300px; margin-top: -30px; margin-bottom: 0; position: fixed; _position: absolute; _bottom: auto; _top: expression(eval(document.documentElement.scrollTop+document.documentElement.clientHeight-this.offsetHeight-(parseInt(this.currentStyle.marginTop,10)||0)-(parseInt(this.currentStyle.marginBottom,10)||0))); right: 0px; bottom: -200px;">
		<div class="panel-heading"><span class="m_close" onclick="HideDiv('TimeDiv')" title="关闭"></span><%=Call.SiteName%>_OA助手提示</div>
		<div class="panel-body" style="height: 165px;">
			<div id="warnContent" style="padding-left: 20px; padding-top: 30px;">
				<%--  <div style="float:left; margin-right:20px; height:40px;">上班：<asp:Label ID="lblBegin" runat="server"></asp:Label>
				<asp:Button ID="BtnBegin" Text="签到" CssClass="btn btn-primary" Width="50" runat="server" /></div>
			<div style="height:40px;">下班：<asp:Label ID="lblEnd" runat="server"></asp:Label> <asp:Button ID="BtnEnd" Text="签退" CssClass="btn btn-primary" Width="50" runat="server" />--%>
				<div id="contentDiv"></div>
			</div>
		</div>
	</div>
</div>
<script type="text/javascript">
    function hiddendiv() {
        $("#toolbar1").toggle("fast");
        var obj = document.getElementById("switchPoint");
        if (obj.alt == "关闭左栏") {
            obj.alt = "打开左栏";
            obj.src = "/App_Themes/Admin/butOpen.gif";
        }
        else {
            obj.alt = "关闭左栏";
            obj.src = "/App_Themes/Admin/butClose.gif";
        }
    }
    function hiddendiv1() {
        $("#toolbar1").css("display", "none");
        var obj = document.getElementById("switchPoint");
        obj.alt = "打开左栏";
        obj.src = "/App_Themes/Admin/butOpen.gif";
    }
    $().ready(function () {
        $("#user_left").hide();
        $("#user_right").css({ width: '100%' });
    })
</script>
<script type="text/javascript">
//-------未读邮件
function GetUnreadMail() {
	//a = "GetUnreadMail";
	//$.ajax({
	//    type: "Post",
	//    url: "/Mis/OA/OAajax.ashx",
	//    dataType: "json",
	//    data: { action: a },
	//    success: function (data) {
	//        if (data != 0) {///mis/oa/Mail/Message.aspx?view=1
	//            var str = "你当前有" + data.num + "封邮件未读,<a href='javascript:;' onclick='showMain(\"/Mis/OA/Mail/Message.aspx?view=1\");'>点击阅读</a>";
	//            $("#contentDiv").append(str);
	//            ShowDiv('TimeDiv');
	//        }
	//    },
	//    error: function (data) { }
	//});
}
setTimeout('GetUnreadMail()', 30000);
</script>
<style>
    #main_right #user_left { display:none;}
    #main_right #user_right { width:100%;}
    .switchPoint:hover {cursor: pointer;}
</style>
</asp:Content>
