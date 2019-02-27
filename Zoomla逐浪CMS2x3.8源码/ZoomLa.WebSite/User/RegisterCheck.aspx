<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RegisterCheck.aspx.cs" Inherits="ZoomLa.WebSite.User.RegisterCheck" EnableViewStateMac="false" %>
<!DOCTYPE HTML>
<html>
<head runat="server">
<title>注册会员邮件认证</title>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta http-equiv="X-UA-Compatible" content="IE=edge">
<meta name="viewport" content="width=device-width, initial-scale=1.0">
<link type="text/css" href="/dist/css/bootstrap.min.css" rel="stylesheet" />
<link href="/App_Themes/UserThem/Responsive.css"rel="stylesheet" type="text/css"/>
<link href="/App_Themes/V3.css" rel="stylesheet" type="text/css" />
<script src="../JS/jquery-1.11.1.min.js"></script>
<script type="text/javascript" src="/dist/js/bootstrap.min.js"></script>
<script type="text/javascript" src="/JS/ajaxrequest.js"></script>
</head>
<body class="backs" >
    <div class="navbar navbar-static-top" style="background-color:#004b9b;">
      <div class="container">
        <div class="navbar-header">
          <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
            <span class="icon-bar"></span>
            <span class="icon-bar"></span>
            <span class="icon-bar"></span>
          </button>
          <a class="navbar-brand hidden-sm" style="color:#fff;" href="/" title="首页"><%:Call.SiteName%></a>
        </div>
        <div class="navbar-collapse collapse">
          <ul class="nav navbar-nav">
            <li><a href="http://www.z01.com">官网首页</a></li>
            <li><a href="http://bbs.z01.com/">技术社区</a></li>
            <li><a href="http://www.z01.com/pub/">下载产品</a></li>
            <li><a href="http://help.z01.com/">使用帮助</a></li>
          </ul>
        </div>
      </div>
    </div>

  <div id="main" class="container">
<form id="form1" runat="server">
   <div id="main_l" class="col-lg-6 col-md-6 col-sm-6 col-xs-12 text-center main_ldiv" style="padding:0 5px;" >
   	<h1 class="text-left"><span class="fa fa-user pull-left"></span>注册会员认证</h1>
<div class="clearfix"></div>
<div class="alert alert-info"> 请输入您注册时填写的用户名和密码，以及本站发给你的确认信中的随机验证码。必须完全正确后，你的帐户才会激活。</div>
 <ul>
			 <li>
			 <div class="form-group">
		<label class="col-sm-4 col-lg-3 col-md-3 col-xs-12 text-right padding0">用户名称：</label>
		<div class="col-sm-6 col-lg-6 col-md-8 col-xs-12 ">
		  <asp:TextBox ID="TxtUserName" runat="server"  CssClass="form-control"></asp:TextBox>
		</div>
	   </div>
	   <div class="clearfix"></div>
			 </li>
				  <li>
			 <div class="form-group">
		<label   class="col-sm-4 col-lg-3 col-md-3 col-xs-12 text-right padding0">用户密码：</label>
		<div class="col-sm-8 col-lg-6 col-md-6 col-xs-12 ">
		<asp:TextBox ID="TxtPassword" TextMode="Password" runat="server"  CssClass="form-control"></asp:TextBox>
		</div>
	   </div>
		<div class="clearfix"></div>
			 </li>
			 
	 <li>
			 <div class="form-group">
		<label   class="col-sm-4 col-lg-3 col-md-3 col-xs-12 text-right padding0">随机验证码：</label>
		<div class="col-sm-8 col-lg-6 col-md-6 col-xs-12 ">
	  <asp:TextBox ID="TxtCheckNum" runat="server"  CssClass="form-control"></asp:TextBox>
		</div>
	   </div>
		<div class="clearfix"></div>
			 </li>
			  <li>
			   <asp:Button ID="BtnRegCheck" runat="server" Text="验证" CssClass="btn btn-primary	" OnClick="BtnRegCheck_Click" />
			  
			  </li>
			  </ul>          
							 
</div>
<div id="main_r"  class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
<h2>登录会员中心您将获得：</h2>
<ul>
<li>自由发布信息 </li>
<li>查看积分与管理空间 </li>
<li>设定您的个性化空间</li><li>提交您的需求为您服务 </li>
<li>购物支付多彩商务体验 </li>
</ul>
</div>
<div class="clearfix"></div>
</form>
</div>
</body>
</html>