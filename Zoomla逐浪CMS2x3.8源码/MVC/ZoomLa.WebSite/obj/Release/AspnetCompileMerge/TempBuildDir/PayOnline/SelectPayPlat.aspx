<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelectPayPlat.aspx.cs" Inherits="ZoomLaCMS.PayOnline.SelectPayPlat" EnableViewStateMac="false" MasterPageFile="~/Common/Master/User.Master"%>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>用户充值</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div id="pageflag" data-nav="home" data-ban="UserInfo"></div>
<div class="container margin_t5">
	<ol class="breadcrumb">
		<li><a title="会员中心" href="/User/Default">会员中心</a></li>
		<li><a href="/User/Info/UserInfo">账户管理</a></li>
		<li class="active">用户充值</li>
	</ol>
</div>
<div class="container">
	<div class="panel panel-primary" style="width:500px;margin:0 auto;">
		<div class="panel-heading text-center"><b>用户充值</b></div>
		<div class="panel-body">
			<span class="pull-left" style="line-height:32px; margin-left:70px;">充值金额：</span>
			<asp:TextBox ID="Money_T" CssClass="form-control text_md" Text="100" runat="server"></asp:TextBox>
			<asp:RequiredFieldValidator ID="R2" CssClass="tips" runat="server" ControlToValidate="Money_T" Display="Dynamic" ForeColor="Red" ErrorMessage="金额不能为空" />
			<asp:RegularExpressionValidator CssClass="tips" ID="R1" runat="server" ControlToValidate="Money_T" Display="Dynamic" ForeColor="Red" ErrorMessage="金额数值不正确" ValidationExpression="^\d+(\.\d{1,2})?$" />
			<div class="clearfix"></div>
		</div>
		<div class="panel-footer text-center">
			 <asp:Button ID="BtnSubmit" CssClass="btn btn-primary" runat="server" Text="前往充值" OnClick="BtnSubmit_Click" />
		</div>
	</div>
</div>

</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
</asp:Content>
