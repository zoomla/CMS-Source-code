<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowError.aspx.cs" Inherits="ZoomLaCMS.Prompt.ShowError" EnableViewStateMac="false" ValidateRequest="false" MasterPageFile="~/Common/Master/Empty.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<style type="text/css">ul li{list-style-type:none;}</style> 
<title>错误提示</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div class="container" style="margin-top: 10%">
<div class="row">
	<div class="col-lg-6 col-md-6 col-sm-6 col-xs-12 col-lg-offset-3 col-md-offset-3">
		<div class="panel panel-primary">
			<div class="panel-heading">
				<h3 class="panel-title" runat="server" id="title_h3"><span class="fa fa-exclamation-circle"></span> 错误请求-原因可能是：</h3>
			</div>
			<div class="panel-body text-center">
				<ul class="text-center list-unstyled">
					<asp:Literal ID="LtrSuccessMessage" runat="server"></asp:Literal>
				</ul>
			</div>
			<div class="panel-footer" style="text-align: center;">
				<a href="/" title="返回网站首页" style="margin-right: 10px;" class="btn btn-primary"><span class="fa fa-home"></span>网站首页</a>
				<asp:HyperLink ID="LnkReturnUrl" runat="server" class="btn btn-primary" ToolTip="返回上一页"><span class="fa fa-repeat"></span>返回上一页</asp:HyperLink>
			</div>
		</div>
	</div>
</div>
</div>
</asp:Content>