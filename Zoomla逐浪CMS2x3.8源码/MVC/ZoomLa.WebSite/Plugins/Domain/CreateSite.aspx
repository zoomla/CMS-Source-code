<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateSite.aspx.cs" Inherits="ZoomLaCMS.Plugins.Domain.CreateSite" MasterPageFile="~/Manage/Site/SiteMaster2.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>智能建站_选择模板</title>
<link type="text/css" href="../JqueryUI/LightBox/css/lightbox.css" rel="stylesheet" />
<script type="text/javascript" src="../JqueryUI/LightBox/jquery.lightbox.js"></script>
<style>
.menu {position:absolute;bottom:-100px;width:95%;padding:8px 0; display:none;background-color: rgba(0,0,0,0.7);border-top: 1px solid #33383b;}
.spanC {color: white;margin: 0 10px 0 10px;font-family: 'Microsoft YaHei';}
</style>
<script type="text/javascript">
	$(document).ready(function () {
		base_url = document.location.href.substring(0, document.location.href.indexOf('index.html'), 0);

		$(".lightbox").lightbox({
			fitToScreen: true,
			imageClickClose: false
		});

		$(".lightbox-2").lightbox({
			fitToScreen: true,
			scaleImages: true,
			xScale: 1.2,
			yScale: 1.2,
			displayDownloadLink: true
		});
	});
	function setData(v) {
		$(":hidden[name='selectedTempData']").val(v);
		//$("#nextDiv").show();
		$("#<%=sureBtn.ClientID%>").trigger("click");
	}
	function disMenu(obj) {
		$(obj).find("div[name='menuDiv']").css("bottom", 20).show();
	}
	function hideMenu(obj) {
		$(obj).find("div[name='menuDiv']").css("bottom", -100).hide();
	}
	function closeDiv(id) {
		$("#" + id).hide();
	}
</script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<ol class="breadcrumb">
<li><a href="#">站群中心</a></li>
<li><a href="InquiryDomName.aspx">智能建站</a></li>
<li class="active">模板选择</li>
</ol>
<div id="site_main" style="margin-top:15px;">
	 <div class="container templatelist">
	<asp:Repeater runat="server" ID="tempRepeater" EnableViewState="false">
		<ItemTemplate>
			 <div class="col-xs-6 col-md-6" onmouseover="disMenu(this);" onmouseout="hideMenu(this);">
			   <%#GetThumbnail(Eval("TempDirName").ToString()) %>
				  <div name="menuDiv" class="menu">
					  <input type="button" class="btn btn-primary" onclick="setData('<%#Eval("Project")+":"+Eval("TempDirName") %>');"  value="选择" style="margin-left:20px;"/>
					  <span class="spanC">模板名:<%#Eval("Project") %></span><span class="spanC">作者:<%#Eval("Author") %></span>
				  </div>
			</div>
		</ItemTemplate>
	</asp:Repeater>
		 <div style="clear:both;"></div>
		 <div style="position:fixed;bottom:15px;"><asp:Literal runat="server" ID="pageHtmlLi"></asp:Literal></div> 
		 <input type="hidden" name="selectedTempData" />
</div>
<div  style="clear:both"></div>
</div><!--site_main-->
<div class="modal" id="nextDiv" style="display:none;margin:100px auto;">
	<div class="modal-dialog">
	  <div class="modal-content">
		<div class="modal-header">
		  <button type="button" class="close" data-dismiss="modal" aria-hidden="true" onclick="closeDiv('nextDiv')">×</button>
		  <h4 class="modal-title">第二步完成,下步开始自动建站!!</h4>
		</div>
		<div class="modal-body">
			<div class="alert alert-info"><asp:Label runat="server" ID="domNameL" /></div>
		  <p><asp:TextBox runat="server" ID="siteNameT" class="form-control" placeholder="网站名,请输入中文"/></p>
		 <%--    <asp:RequiredFieldValidator  runat="server" ControlToValidate="siteNameT" ForeColor="Red" ErrorMessage="请先输入网站名" Display="Dynamic" SetFocusOnError="true"/>
			 <asp:RegularExpressionValidator runat="server" ControlToValidate="siteNameT" ForeColor="Red" ErrorMessage="请输入三位以上,十位以下中文"
				  Display="Dynamic" SetFocusOnError="true" ValidationExpression="^([\u4e00-\u9fa5]{3,10}$)"  />--%>
		</div>
		<div class="modal-footer" style="text-align:left;">
			<asp:Button runat="server" ID="sureBtn" Text="继续" class="btn btn-primary" OnClick="sureBtn_Click"/>
		</div>
	  </div><!-- /.modal-content -->
	</div><!-- /.modal-dialog -->
  </div>
</asp:Content>