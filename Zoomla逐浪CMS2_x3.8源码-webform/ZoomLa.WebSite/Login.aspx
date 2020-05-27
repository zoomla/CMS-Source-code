<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="ZoomLa.WebSite._Login" EnableViewStateMac="false" MasterPageFile="~/Common/Master/Empty.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<link href="/App_Themes/User.css" type="text/css" rel="stylesheet" />
<style>
.h_user_top{ padding:5px 10px; background:#f5f5f5; box-shadow:0 2px 5px #eee;}
.h_login{ margin:auto; margin-top:5px; padding:20px; width:340px; max-width:100%; box-shadow:0 0 10px 1px rgba(115,255,255,0.3); background:linear-gradient(#fff,#f5f5f5);}
.h_login li{ position:relative; margin-top:10px; margin-bottom:10px;}
.h_login li i{ position:absolute; top:10px; left:10px; font-size:1.2em; color:#337AB7;}
.h_login li .form-control{ padding-left:40px; border:none;} 
.h_login .form-control:focus{ box-shadow:none;}
#BtnLogin{ width:100%;}
</style>
<title>会员登录</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<asp:Panel ID="PnlLogin" runat="server">
<div class="h_user_top">
<div class="container">
<span class="pull-left">登录<%=Call.SiteName %></span>
<span class="pull-right"><a href="/User/Register.aspx">注册</a></span>
</div>
</div> 
<div class="h_login">
<ul class="list-unstyled">
<li><i class="fa fa-user"></i><asp:TextBox ID="TxtUserName" class="form-control" placeholder="帐号"  runat="server"></asp:TextBox></li>
<li><i class="fa fa-lock"></i><asp:TextBox ID="TxtPassword" class="form-control" placeholder="密码" runat="server" TextMode="Password"></asp:TextBox></li>
<li> <asp:Button ID="BtnLogin" runat="server" class="center-block btn btn-primary" Text="登录" OnClick="BtnLogin_Click" /></li> 
<li><asp:PlaceHolder ID="PhValCode" runat="server">验证码：<asp:TextBox ID="TxtValidateCode" MaxLength="6" Width="60" class="l_input" runat="server" onfocus="this.select();"></asp:TextBox>     <asp:Image ID="VcodeLogin" runat="server" ImageUrl="~/Common/ValidateCode.aspx" Height="20px" /></asp:PlaceHolder> 
</li>  
<li>
<a href="User/GetPassword.aspx" target="_top">忘记密码</a>
</li>
</ul>
</div>
    <asp:RequiredFieldValidator ID="ValrUserName" runat="server" ErrorMessage="请输入用户名！" ControlToValidate="TxtUserName" Display="None" SetFocusOnError="True"></asp:RequiredFieldValidator>
    <asp:RequiredFieldValidator ID="ValrPassword" runat="server" ErrorMessage="请输入密码！" ControlToValidate="TxtPassword" Display="None" SetFocusOnError="True"></asp:RequiredFieldValidator>
    <asp:RequiredFieldValidator ID="ValrValidateCode" runat="server" ErrorMessage="请输入验证码！"  ControlToValidate="TxtValidateCode" Display="None" SetFocusOnError="True"></asp:RequiredFieldValidator>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False" />
</asp:Panel>
<asp:Panel ID="PnlLoginStatus" runat="server">
    <div class="h_login">
        <ul>
          <li><asp:Literal ID="LitUserName" runat="server"></asp:Literal>，您好！</li>             
          <li><asp:Literal ID="LitMessage" runat="server">0</asp:Literal></li>
          <li><asp:Literal ID="LitLoginTime" runat="server">0</asp:Literal></li> 
          <li><asp:Literal ID="LitLoginDate" runat="server">0</asp:Literal></li>               
          <li><a href="User/Default.aspx" target="_top">会员中心</a>&nbsp;&nbsp;|&nbsp;&nbsp;<a href="javascript:void(0);" onClick="logout()" target="_top">退出登录</a></li>
        </ul>
    </div>
    <iframe name="pasd" id="pasd" style="display:none;"></iframe>
</asp:Panel>
<asp:Panel ID="PnlLoginMessage" runat="server" Visible="false">
  <ul>
    <asp:Literal ID="LitErrorMessage" runat="server"></asp:Literal>
    <li><asp:Button ID="BtnReturn" runat="server" class="C_input" Text="返回" OnClick="BtnReturn_Click" /></li>
  </ul>
</asp:Panel>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
<script  type="text/javascript"  src="/CounterLink.aspx"></script>
<script type="text/javascript" src="/js/jqueryajax.js"></script>
<script>
    var frmin = "";
    function loginSec(obj) {
        //obj ==0 为登录成功,-1你的帐户未通过验证或被锁定，请与网站管理员联系
        window.onload = function () { sybot(); }
        //if (obj == 0)
        //{
        //    self.parent.location.reload();
        //}
    }
    function sybot(url) {
        setTimeout(changeurl, 1000);
    }

    function changeurl() {
        document.getElementById("pasd").src = document.getElementById("script").value;
    }
</script>
<script type="text/javascript">
    function logout() {
        ZoomLa.ajaxlogout(function () {
            window.location = "/login.aspx?Style=" + Style;
        });
    }
    //登录样式 1为纵向 2为横向
    var Style = "<%=style%>";
if (Style == "2") {
    //登录前
    var Login = document.getElementById("login");
    if (Login != null) {
        Login.className = "horizontal";
    }
    //登录后
    var Logged = document.getElementById("logged");
    if (Logged != null) {
        Logged.className = "horizontal";
    }
    //登录错误
    var PnlLoginMessage = document.getElementById("PnlLoginMessage");
    if (PnlLoginMessage != null) {
        PnlLoginMessage.className = "horizontal";
    }
}
</script>
</asp:Content>