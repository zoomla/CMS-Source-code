<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SiteProgress.aspx.cs" Inherits="ZoomLaCMS.Plugins.Domain.SiteProgress" MasterPageFile="~/Manage/Site/SiteMaster2.master"%>
<asp:Content runat="server" ContentPlaceHolderID="head">
<style>
.tdl {margin-bottom:20px;position:relative;}
</style>
<title>站点进度</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<ol class="breadcrumb">
<li><a href="#">站群中心</a></li>
<li><a href="/site/default">智能建站</a></li>
<li class="active">生成网站</li>
</ol>
 <div id="site_main">
	 <div id="tab3">
<table style="width:40%;">
 <tr><td style="width:20%;"><label> 站点名：</label></td><td><asp:Label runat="server" ID="siteNameL" Text="测试站点" /></td></tr>
 <tr><td style="width:20%;"><label> 绑定域名：</label></td><td>
	 <asp:Label runat="server" ID="domNameL"/>
	 <a href="/Site/Domain" target="_applyDom" class="btn btn-primary" style="color:white;">申请域名</a>
	 <a href="/Site/Domain" target="_applyDom" class="btn btn-primary" style="color:white;">绑定域名</a>
  </td></tr>
 <tr><td><label class="tdl"> 正在生成网站：</label></td><td><div class="progress progress-striped active" >
	<div id="downCodeDiv" class="progress-bar" role="progressbar" aria-valuenow="45" aria-valuemin="0" aria-valuemax="100"><span id="downCodeSpan" class="sr-only" style="position:relative;"></span></div>
 </div></td></tr>
 <tr><td><label class="tdl"> 正在处理文件：</label></td><td>
 <div class="progress progress-striped active">
 <div id="unzipDiv" class="progress-bar" role="progressbar" aria-valuenow="45" aria-valuemin="0" aria-valuemax="100"><span id="uzipSpan" class="sr-only" style="position:relative;"></span></div>
 </div></td></tr>
 <tr><td><label class="tdl"> 更新配置信息：</label></td><td>
 <div class="progress progress-striped active">
 <div id="downTempDiv" class="progress-bar" role="progressbar" aria-valuenow="45" aria-valuemin="0" aria-valuemax="100" ><span id="downTempSpan" class="sr-only" style="position:relative;"></span></div>
 </div></td></tr>
  <tr><td colspan="2">
	 <div class="alert alert-info"> 
		  <strong><span id="dataInfo" runat="server"></span></strong></div></td></tr>
	<tr><td colspan="2">
	 <div class="alert alert-info"> 
		 <asp:Button runat="server" id="beginSetupBtn" Text="开始安装" class="btn btn-primary" disabled="disabled"  ClientIDMode="Static" OnClick="beginSetupBtn_Click"/>
		<span id="remindSpan">请稍等片刻,正在努力工作中...</span></div></td></tr>
	 </table>
	 </div><!--site_main End;-->
</div>
<script type="text/javascript">
	var interval;
	var actionArr = ["getCodeP", "getTempP", "getUnzipP"];//下载源码，下载模板,解压源码
	function PostToCS(a,v) {
		$.ajax({
			type: "Post",
			url: "SiteProgress.aspx",
			data: { action: a,value:v },
			success: function (data) {
				if (a == actionArr[0])//下载源码
				{
					$("#downCodeDiv").css("width", data + "%");
					$("#downCodeSpan").text(data + "%");
					if (data == 100)
					{
						clearInterval(interval);
						beginCheck(actionArr[2]);
					}
				}
				else if (a == actionArr[2])//解压
				{
					$("#unzipDiv").css("width", data + "%");
					$("#uzipSpan").text(data + "%");
					if (data == 100)
					{
						clearInterval(interval);
						beginCheck(actionArr[1]);
					}
				}
				else if (a == actionArr[1])//下载模板
				{
					$("#downTempDiv").css("width", data + "%");
					$("#downTempSpan").text(data + "%");
					if (data == 100) {
						clearInterval(interval);
						activeBtn();
					}
				   
				}
			},
			error: function (data) {
			}
		});
	}
	//调用其开始循环获取
	function beginCheck(request) { interval = setInterval(function () { PostToCS(request, '') }, 1000); }
	//完成,可以开始安装
	function activeBtn()
	{
		$("#beginSetupBtn").attr("disabled", false);
		$("#remindSpan").text("网站已经就绪,点击按钮开始配置!!");
		$("#remindSpan").parent().attr("class", "alert alert-success");
	}
</script>
</asp:Content>