<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShowMessage.aspx.cs" Inherits=" ZoomLa.WebSite.ShowMessage" EnableViewStateMac="false" MasterPageFile="~/Common/Master/Empty.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>信息提示</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div class="container" style="margin-top: 10%">
	<div class="row">
		<div class="col-lg-6 col-md-6 col-sm-6 col-xs-12 col-lg-offset-3 col-md-offset-3">
			<div class="panel panel-primary">
				<div class="panel-heading">
					<h3 class="panel-title"><span class="fa fa-remove-circle pull-left"></span><asp:Label ID="LblMessageTitle" runat="server" Text="信息提示"></asp:Label></h3>
				</div>
				<div class="panel-body text-center">
					<ul class="text-center list-unstyled">
						<asp:Literal ID="LtrMessage" runat="server"></asp:Literal>
					</ul>
				</div>
				<div class="panel-footer" style="text-align: center;">
					<asp:LinkButton ID="Link1"  OnClick="Link1_Click1" runat="server" Text="确定"></asp:LinkButton>
					<a href="/" title="返回网站首页" style="margin-right: 10px;" class="btn btn-primary"><span class="fa fa-home"></span>网站首页</a>
					<asp:HyperLink ID="LnkReturnUrl" runat="server" class="btn btn-primary" ToolTip="返回上一页"><span class="fa fa-repeat"></span>返回上一页</asp:HyperLink>
				</div>
			</div>
		</div>
	</div>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
</asp:Content>