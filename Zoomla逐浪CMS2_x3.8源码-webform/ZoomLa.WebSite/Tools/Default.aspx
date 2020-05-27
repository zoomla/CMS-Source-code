<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Tools_Default" MasterPageFile="~/Common/Common.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>Supper Gavel-For ZoomlaCMS2</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div id="SupperGavel" runat="server" visible="false"> 
<h1><i class="fa fa-gavel"></i>Supper gavel</h1>
<ul >
	<li><asp:TextBox ID="UserName_T" placeholder="管理员" runat="server" CssClass="form-control text_300"/></li>
	<li><asp:TextBox TextMode="Password" ID="UserPwd_T" placeholder="口令" runat="server" CssClass="form-control text_300"/></li>
	<li>
		<asp:TextBox ID="VCode" runat="server" placeholder="验证码" CssClass="form-control text_300 code"/>
		<img id="VCode_img" title="点击刷新验证码" class="code"  style="height:34px;"/>
		<asp:HiddenField runat="server" id="VCode_hid" />
	</li>
	<li><asp:Button ID="Login_Btn" runat="server" CssClass="btn btn-primary text_300" OnClick="Login_Btn_Click" Text="登录" /></li>
</ul>
</div>
<div id="SupperGavelCon" runat="server">
<div class="container">
<h1><i class="fa fa-gavel"></i>Supper gavel维护工具</h1>
<table class="table table-striped table-bordered table-hover">
	<tr>
		<td class="td_l">配置文件检测:</td>
		<td><asp:Button ID="Check_Btn" runat="server" OnClick="Check_Btn_Click" Text="开始检测" /></td>
	</tr>
	<tr>
		<td>恢复默认配置:</td>
		<td><asp:Button ID="Update_Btn" runat="server" OnClick="Update_Btn_Click" Text="开始修复" /></td>
	</tr>
	<tr><td>启动开发调式模式:</td><td><asp:Button runat="server" ID="Develop_Btn" Text="开启" OnClick="Develop_Btn_Click" /></td></tr>
</table>
<table class="table table-bordered table-striped">
	<tr><td class="td_l">关闭HTTPS重写:</td><td><asp:Button runat="server" ID="Close_Btn" Text="关闭HTTPS" OnClick="Close_Btn_Click" /></td></tr>
	<tr><td>关闭管理员动态口令:</td><td><asp:Button runat="server" ID="Close_Code_Btn" Text="关闭" OnClick="Close_Code_Btn_Click" /></td></tr>
</table> 
<abbr>*不可逆操作请进行全站和数据备份后操作！</abbr>
<div class="alert alert-info" role="alert" runat="server" id="FileInfo_Div" visible="false">
	<table class="table">
		<thead>
			<tr><td class="td_lg">文件名</td><td>是否存在</td></tr>
		</thead>
		<asp:Literal ID="Files_Li" runat="server" EnableViewState="false"></asp:Literal>
	</table>
</div>
</div>
<div class="container SupperGavelTool">
<ins>扩展工具:</ins>
<ul class="bs-glyphicons-list">
<li>
<a href="http://bbs.z01.com" target="_blank">
<i class="fa fa-bold"></i>
<span class="glyphicon-class">BootStarp框架</span>
</a>
</li>
<li>
<a href="http://app.z01.com" target="_blank">
<i class="fa fa-mobile"></i>
<span class="glyphicon-class">微首页</span>
</a>
</li>
<li>
<a href="http://app.z01.com/Class_1/Default.aspx" target="_blank">
<i class="fa fa-weixin"></i>
<span class="glyphicon-class">场景列表</span>
</a>
</li>
<li>
<a href="http://www.z01.com/tool/" target="_blank">
<i class="fa fa-cog"></i>
<span class="glyphicon-class">站长工具</span>
</a>
</li>
<li>
<a href="http://www.z01.com/blog/techs/2409.shtml" target="_blank">
<i class="fa fa-font"></i>
<span class="glyphicon-class">WebFont</span>
</a>
</li>
<li>
<a href="http://ad.z01.com/" target="_blank">
<i class="fa fa-picture-o"></i>
<span class="glyphicon-class">广告源码</span>
</a>
</li>
<li>
<a href="http://ad.z01.com/color.htm" target="_blank">
<i class="fa fa-align-justify"></i>
<span class="glyphicon-class">网页配色</span>
</a>
</li>
<li>
<a href="http://bbs.z01.com/boot/" target="_blank">
<i class="fa fa-laptop"></i>
<span class="glyphicon-class">响应式工具</span>
</a>
</li>
<li>
<a href="http://www.z01.com/mb/" target="_blank">
<i class="fa fa-briefcase"></i>
<span class="glyphicon-class">免费模板</span>
</a>
</li>
<li>
<a href="http://www.z01.com/pub/" target="_blank">
<i class="fa fa-download"></i>
<span class="glyphicon-class">下载逐浪CMS</span>
</a>
</li>
<li>
<a href="http://bbs.z01.com/index" target="_blank">
<i class="fa fa-users"></i>
<span class="glyphicon-class">技术社区</span>
</a>
</li>
<li>
<a href="http://www.z01.com/mtv/" target="_blank">
<i class="fa fa-video-camera"></i>
<span class="glyphicon-class">视频教程</span>
</a>
</li>
<li>
<a href="https://www.z01.com/blog/techs/2975.shtml" target="_blank">
<i class="fa fa-child"></i>
<span class="glyphicon-class">Emmet</span>
</a>
</li>
<li>
<a href="https://www.ziti163.com" target="_blank">
<i class="fa ZoomlaICO2015"></i>
<span class="glyphicon-class">字体网</span>
</a>
</li>
</ul>
</div>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
<style>.code{display:none;}</style>
<script src="/JS/ZL_ValidateCode.js"></script>
<script>
function EnableCode() {
	if ($(".code").is(":hidden")) {
		$(".code").show();
		$("#VCode_img").click();
		$("#VCode").ValidateCode();
	}
}
</script>
</asp:Content>