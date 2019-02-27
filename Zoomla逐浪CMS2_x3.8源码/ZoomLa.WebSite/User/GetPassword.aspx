<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GetPassword.aspx.cs" Inherits="ZoomLa.WebSite.User.User_GetPassword" MasterPageFile="~/Common/Common.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>找回密码-<%:Call.SiteName %></title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<center style="background: url(http://code.z01.com/user_login.jpg); background-position: center; left: 0; top: 0; right: 0; bottom: 0; position: absolute; background-repeat: no-repeat; background-size: cover;">
<div class="user_login">
<h3><span class="fa fa-refresh"></span>找回密码</h3>
<ul class="list-unstyled">
<asp:Panel ID="Email_Div" runat="server" Visible="false">
	<li>
		<i class="fa fa-user"></i>
		<asp:TextBox ID="TxtUserName" placeholder="输入会员名" runat="server" CssClass="form-control text_max" />
		<asp:RequiredFieldValidator ID="ValrTxtUserName" runat="server" ErrorMessage="输入会员名!" ForeColor="Red" ControlToValidate="TxtUserName" Display="dynamic" SetFocusOnError="True" /></li>
	<li class="margin_t5">
		<i class="fa fa-qrcode"></i>
		<div class="form-group">
			<div>
                <asp:TextBox runat="server" id="VCode"  maxlength="6" placeholder="验证码" class="form-control text_x" autocomplete="off" />
			<img id="VCode_img" title="点击刷新验证码" class="code" style="height: 34px;" />
			<input type="hidden" id="VCode_hid" name="VCode_hid" />
			</div>
            <asp:RequiredFieldValidator runat="server" ID="RV1" ForeColor="Red"  ErrorMessage="验证码不能为空" ControlToValidate="VCode" Display="Dynamic"/>
		</div>
	</li>
	<li class="text-center margin_t5">
        <asp:LinkButton runat="server" ID="SendMail_Btn" OnClick="SendMail_Btn_Click" class="btn btn-info margin_t5" ><span class="fa fa-envelope"></span> 发送邮件</asp:LinkButton>
        <asp:LinkButton runat="server" ID="SendMsg_Btn" OnClick="SendMsg_Btn_Click" class="btn btn-info margin_t5" ><span class="fa fa-mobile"></span> 发送短信</asp:LinkButton>
	</li>
</asp:Panel>
<asp:Panel runat="server" ID="Mobile2_Div" Visible="false">
    <li>
        <asp:TextBox runat="server" ID="CheckCode_T" CssClass="form-control" placeholder="请输入校验码" />
        <asp:RequiredFieldValidator ID="RC1" runat="server" ErrorMessage="校验码不能为空" ForeColor="Red" ControlToValidate="CheckCode_T" Display="Dynamic" SetFocusOnError="True" />
    </li>
    <li class="margin_t5"><asp:Button runat="server" ID="ValidMobile_Btn" CssClass="btn btn-info" Text="验证手机" OnClick="ValidMobile_Btn_Click" /></li>
</asp:Panel>
<asp:Panel ID="Answer_Div" runat="server" Visible="false">
	<li>密码提示问题：<asp:Literal ID="Question_L" runat="server"></asp:Literal></li>
	<li class="margin_t5">密码提示答案：<asp:TextBox ID="Answer_T" runat="server" CssClass="form-control text_400"></asp:TextBox>
        <asp:RequiredFieldValidator runat="server" ID="RA1" ControlToValidate="Answer_T" ForeColor="Red" Display="Dynamic" ErrorMessage="答案不能为空" />
	</li>
	<asp:Button ID="SureAnswer_Btn" runat="server" Text="完成" OnClick="SureAnswer_Btn_Click" CssClass="btn btn-primary" />
</asp:Panel>
<asp:Panel ID="Final_Div" runat="server" Visible="false">
	<li><asp:TextBox ID="TxtPassword" CssClass="form-control" TextMode="Password" runat="server" placeholder="新密码"></asp:TextBox></li>
	<li class="margin_t5"><asp:TextBox ID="TxtConfirmPassword" runat="server" TextMode="Password" CssClass="form-control" placeholder="确认密码"></asp:TextBox>
		<asp:CompareValidator ID="CompareValTxtConfirmPassword" ControlToValidate="TxtConfirmPassword" ControlToCompare="TxtPassword" Display="Dynamic" Type="String" Operator="Equal" runat="server" ForeColor="Red" ErrorMessage="两次密码输入不一致！"></asp:CompareValidator>
		<asp:LinkButton ID="Final_Btn" runat="server" OnClick="Final_Btn_Click" CssClass="btn btn-primary margin_t5" > 修改密码</asp:LinkButton>
	</li>
</asp:Panel>
</ul>
</div>
</center>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
<script src="/JS/ZL_ValidateCode.js"></script>
<script type="text/javascript">
    $(function () {
        $("#TxtUserName").focus();
        $("#VCode").ValidateCode();
    });
</script>
</asp:Content>
