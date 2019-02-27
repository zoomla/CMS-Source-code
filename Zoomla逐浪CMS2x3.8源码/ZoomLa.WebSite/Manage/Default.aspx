<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Manage_I_Default" ClientIDMode="Static" %><!DOCTYPE html>
<html>
<head>
<title><%:Call.SiteName%>_<%=Resources.L.后台管理 %></title>
<meta charset="utf-8">
<meta http-equiv="X-UA-Compatible" content="IE=edge" />
<meta name="viewport" content="width=device-width, initial-scale=1.0" />
<meta name="renderer" content="webkit">
<!--[if lt IE 9]>
<script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
<script src="https://oss.maxcdn.com/libs/respond.js/1.3.0/respond.m in.js"></script>
<![endif]-->
<link rel="stylesheet" type="text/css" href="/dist/css/bootstrap.min.css" />
<link rel="stylesheet" type="text/css" href="/dist/css/font-awesome.min.css" />
<link rel="stylesheet" type="text/css" href="/App_Themes/V3.css?version=<%:ConfigurationManager.AppSettings["Version"].ToString() %>" />
<link href="/dist/css/animate.min.css" rel="stylesheet" />
<script src="/JS/jquery-1.11.1.min.js"></script>
<script src="/JS/jquery-ui.min.js"></script>
<script src="/JS/ICMS/FrameTab.js"></script>
<script src="/Plugins/ECharts/build/source/echarts.js"></script>
</head>
<body style="overflow-y: hidden;">
<form id="form1" runat="server">
<div class="newbody" id="newbody" style="display: none;" ondblclick="$(this).hide();">
<div class="newbody_t">
	<div class="newbody_tl">
		<span runat="server" id="newbody_logo1" visible="false"><span class="logo1">凵</span><span class="logo2">刂</span></span>
		<span runat="server" id="newbody_logo2" visible="false"><span class="logo3"><%=ComRE.Img_NoPic(ZoomLa.Components.SiteConfig.SiteInfo.LogoAdmin, "zllogo10") %></span>
			<span class="logo4"><%=ZoomLa.Components.SiteConfig.SiteInfo.LogoPlatName.Split('<')[0] %></span></span>
		<a onclick="javascript:$('.newbody').hide();showleft('menu1_1','{$path}Site/SiteConfig.aspx')">门户管理</a>
		<a onclick="javascript:$('.newbody').hide();showleft('menu1_1','{$path}WorkFlow/OAConfig.aspx')">办公平台</a>
		<a onclick="javascript:$('.newbody').hide();showleft('menu1_1','{$path}Content/ECharts/AddChart.aspx')">数据管理</a>
		<a onclick="javascript:$('.newbody').hide();showleft('menu10_1','/APP/Default.aspx')">移动开发</a>
		<a onclick="javascript:$('.newbody').hide();showleft('menu10_1','WeiXin/home.aspx')">微信应用</a>
	</div>
	<div class="newbody_tr">
		<i class="fa fa-lightbulb-o"></i>双击跳过20秒预编译
		<asp:LinkButton runat="server" ID="NoShow_Btn" OnClick="NoShow_Btn_Click"><i class="fa fa-times-circle"></i> Close</asp:LinkButton>
	</div>
	<div class="clearfix"></div>
</div>
<div class="newbody_c"><i class="fa ZoomlaICO2015 animated"></i></div>
<div class="newbody_c1">
	<div class="newbody_c1t" style="display: none;"><%:ZoomLa.Components.SiteConfig.SiteInfo.Webmaster %><span>CMS</span></div>
	<div class="newbody_clm" style="display: none;">
		<img src="/App_Themes/Admin/shadow.png" class="img-responsive center-block" /></div>
</div>
<div id="wave"></div>
<asp:Button runat="server" ID="ShowAD_Btn" Style="display: none;" OnClick="ShowAD_Btn_Click" />
</div>
<!--新后台背景效果结束-->
<asp:ScriptManager ID="SM1" runat="server"></asp:ScriptManager>
<div class="m_top">
<table class="main_table">
	<tr>
		<td class="main_table_l" style="overflow: hidden;">
			<div onclick="window.open('/');" title="首页" runat="server" id="zlogo1" visible="false" class="zlogo1">凵</div>
			<div onclick="location=location;" title="刷新" runat="server" id="zlogo2" visible="false" class="zlogo2">刂</div>
			<div onclick="window.open('/');" title="首页" runat="server" id="zlogo3" visible="false" class="zlogo3"></div>
			<div onclick="location=location;" title="刷新" runat="server" id="zlogo4" visible="false" class="zlogo4"></div>
		</td>
		<td>
			<div class="margin-15 m_top_right">
				<div class="col-lg-9 col-md-10 col-sm-9 col-xs-12 hidden-xs">
					<div class="m_top_menu">
						<ul>
							<li class="menu1 active" onclick="openmenu('menu1')"><a onclick="showleft('menu1_1');" data-toggle="tooltip" data-placement="top" title="<%=Resources.L.开始 %>"><span class="visible-sm"><%=Resources.L.开始 %></span><span class="hidden-sm"><%=Resources.L.开始 %></span></a></li>
							<li class="menu2" onclick="openmenu('menu2')"><a onclick="ShowMain('NodeTree.ascx','');" title="<%=Resources.L.内容 %>"><span class="visible-sm"><%=Resources.L.内容 %></span><span class="hidden-sm"><%=Resources.L.内容 %></span></a></li>
							<li class="menu3" onclick="openmenu('menu3')"><a onclick="ShowMain('ShopNodeTree.ascx','');" title="<%=Resources.L.商城 %>"><span class="visible-sm"><%=Resources.L.商城 %></span><span class="hidden-sm"><%=Resources.L.商城 %></span></a></li>
							<li class="menu4" onclick="openmenu('menu4')"><a onclick="showleft('menu4_1');" title="<%=Resources.L.黄页 %>"><span class="visible-sm"><%=Resources.L.黄页 %></span><span class="hidden-sm"><%=Resources.L.黄页 %></span></a></li>
							<li class="menu5" onclick="openmenu('menu5')"><a onclick="showleft('menu5_1');" title="<%=Resources.L.教育 %>"><span class="visible-sm"><%=Resources.L.教育 %></span><span class="hidden-sm"><%=Resources.L.教育 %></span></a></li>
							<li class="menu6" onclick="openmenu('menu6')"><a onclick="ShowMain('UserGuide.ascx','');" title="<%=Resources.L.用户 %>"><span class="visible-sm"><%=Resources.L.用户 %></span><span class="hidden-sm"><%=Resources.L.用户 %></span></a></li>
							<li class="menu7" onclick="openmenu('menu7')"><a onclick="showleft('menu7_1');" title="<%=Resources.L.扩展 %>"><span class="visible-sm"><%=Resources.L.扩展 %></span><span class="hidden-sm"><%=Resources.L.扩展 %></span></a></li>
							<li class="menu8" onclick="openmenu('menu8')"><a onclick="showleft('menu8_1');" title="<%=Resources.L.系统 %>"><span class="visible-sm"><%=Resources.L.系统 %></span><span class="hidden-sm"><%=Resources.L.系统 %></span></a></li>
							<li class="menu9" onclick="openmenu('menu9')"><a onclick="showleft('menu9_1');" title="<%=Resources.L.办公 %>"><span class="visible-sm"><%=Resources.L.办公 %></span><span class="hidden-sm"><%=Resources.L.办公 %></span></a></li>
							<li class="menu10" onclick="openmenu('menu10')"><a onclick="showleft('menu10_6');" title="<%=Resources.L.移动 %>"><span class="visible-sm"><%=Resources.L.移动 %></span><span class="hidden-sm"><%=Resources.L.移动 %></span></a></li>
							<li class="menu11" onclick="openmenu('menu11')"><a onclick="showleft('menu11_1');" title="<%=Resources.L.站群 %>"><span class="visible-sm"><%=Resources.L.站群 %></span><span class="hidden-sm"><%=Resources.L.站群 %></span></a></li>
						</ul>
					</div>
				</div>
				<div class="col-lg-3 col-md-2 col-sm-3 col-xs-12 m_top_rl">
					<div id="Announce">
						<div class=" pull-right hidden-md hidden-sm hidden-xs">
							<a href="/" target="_blank" title="<%=Resources.L.返回首页 %>"><span class="fa fa-home"></span></a>
							<a href="/user/" target="_target" title="ALT+U<%=Resources.L.进入会员中心 %>"><span class="fa fa-user"></span></a>
							<a href="javascript:void(0)" style="cursor: pointer;" title='Alt+Q<%=Resources.L.锁定屏幕 %>' onclick="showWindow('Lockin.aspx',900,460);"><span class="fa fa-lock"></span>
								<a href="<%= CustomerPageAction.customPath2 %>SignOut.aspx" title="<%=Resources.L.退出 %>"><span class="fa fa-power-off"></span></a>
						</div>
						<div class="dropdown pull-right topdrop">
							<a data-toggle="dropdown" class="dropdown-toggle" href="javascript:;" aria-expanded="false">
								<label runat="server" id="nameL"></label>
								<span class="fa fa-th-large"></span>
							</a>
							<ul class="dropdown-menu" role="menu" aria-labelledby="dLabel">
								<li><a href="javascript:ShowAD();" title="<%=Resources.L.场景切换 %>"><%=Resources.L.场景切换 %></a></li>
								<li><a href="javascript:modalDialog('/Common/calc.html', 'calculator', 400, 300);"><%=Resources.L.计算器 %></a></li>
								<li><a href="/help.html" target="_blank"><%=Resources.L.快速帮助 %></a></li>
								<li><a href="http://help.z01.com/" target="_blank"><%=Resources.L.线上支持 %></a></li>
							</ul>
						</div>
					</div>
					<div class="pull-right hidden-lg search_btn">
						<span class="fa fa-search" onclick="showsearch()"></span>
					</div>
					<div class="pull-right hidden-lg hidden_group" id="search_div">
						<div class="input-group input-group-sm">
							<asp:TextBox runat="server" ID="keyText" class="form-control input-control nofocus" placeholder="快速搜索  回车确认" onkeydown="return IsEnter(this);" /><i class="fa fa-search"></i>
						</div>
					</div>
				</div>
			</div>
		</td>
	</tr>
</table>
<div class="mb_nav visible-xs">
	<nav class="navbar navbar-default">
		<div class="navbar-header pull-right visible-xs-inline">
			<button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#bs-example-navbar-collapse-1">
				<span class="sr-only">Toggle navigation</span>
				<span class="icon-bar"></span>
				<span class="icon-bar"></span>
				<span class="icon-bar"></span>
			</button>
		</div>
		<div class="collapse navbar-collapse" id="bs-example-navbar-collapse-1">
			<ul class="nav navbar-nav">
				<li><a href="javascript:;" data-label="menu1"><%=Resources.L.开始 %></a></li>
				<li><a href="javascript:;" data-label="menu2"><%=Resources.L.内容 %></a></li>
				<li><a href="javascript:;" data-label="menu3"><%=Resources.L.商城 %></a></li>
				<li><a href="javascript:;" data-label="menu4"><%=Resources.L.黄页 %></a></li>
				<li><a href="javascript:;" data-label="menu5"><%=Resources.L.教育 %></a></li>
				<li><a href="javascript:;" data-label="menu6"><%=Resources.L.用户 %></a></li>
				<li><a href="javascript:;" data-label="menu7"><%=Resources.L.扩展 %></a></li>
				<li><a href="javascript:;" data-label="menu8"><%=Resources.L.系统 %></a></li>
				<li><a href="javascript:;" data-label="menu9"><%=Resources.L.办公 %></a></li>
				<li><a href="javascript:;" data-label="menu10"><%=Resources.L.移动 %></a></li>
				<li><a href="javascript:;" data-label="menu11"><%=Resources.L.站群 %></a></li>
			</ul>
			<ul class="sub_nav"></ul>
			<ul class="thi_nav"></ul>
		</div>
	</nav>
</div>
<div class="clearfix"></div>
<div class="m_top_bottom hidden-xs">
	<ul class="menu1 open">
		<li class="menu1_1 active"><a onclick="showleft('menu1_1','{$path}Profile/Worktable.aspx')"><%=Resources.L.版本信息 %></a></li>
		<li class="menu1_2 "><a onclick="showleft('menu1_1','Main.aspx')"><%=Resources.L.从此开始 %></a></li>
		<li class="menu1_3"><a onclick="showleft('menu1_2','Config/SearchFunc.aspx')"><%=Resources.L.快速导航 %></a></li>
		<li class="menu1_4"><a onclick="showleft('menu1_3','Profile/ModifyPassword.aspx')"><%=Resources.L.修改密码 %></a></li>
	</ul>
	<ul class="menu2">
		<li class="menu2_1 active"><a onclick="ShowMain('NodeTree.ascx','Content/ContentManage.aspx');"><%=Resources.L.按栏目管理 %></a></li>
		<li class="menu2_2"><a onclick="ShowMain('NodeTree.ascx','Content/SpecialManage.aspx')"><%=Resources.L.按专题管理 %></a></li>
		<li class="menu2_3"><a onclick="showleft('menu2_3','Content/CommentManage.aspx')"><%=Resources.L.评论管理 %></a></li>
		<li class="menu2_4">
			<div class="btn-group topdrop">
				<button type="button" onclick="showleft('menu2_4','Pub/PubManage.aspx')" class="btn btn-default btn-sm"><%=Resources.L.互动模块 %></button>
				<button type="button" class="btn btn-default  btn-sm dropdown-toggle" data-toggle="dropdown" aria-expanded="false">
					<span class="caret"></span><span class="sr-only"></span>
				</button>
				<ul class="dropdown-menu" role="menu">
					<li><a href="javascript:;" onclick="showleft('menu2_4','Pub/PubManage.aspx')"><%=Resources.L.互动模块 %></a></li>
					<li><a href="javascript:;" onclick="showleft('menu2_4','Pub/FormManage.aspx')"><%=Resources.L.互动表单 %></a></li>
					<li><a href="javascript:;" onclick="showleft('menu2_4','Content/ModelManage.aspx?ModelType=7')"><%=Resources.L.互动模型 %></a></li>
					<li><a href="javascript:;" onclick="showleft('menu2_4','Pub/PubRecycler.aspx')"><%=Resources.L.互动存档 %></a></li>
					<li><a href="javascript:;" onclick="showleft('menu2_4','Pub/PubExcel.aspx')"><%=Resources.L.Excel导出 %></a></li>
				</ul>
			</div>
		</li>
		<li class="menu2_7"><a onclick="showleft('menu2_7','Content/CreateHtmlContent.aspx')"><%=Resources.L.生成发布 %></a></li>
		<li class="menu2_6"><a onclick="showleft('menu2_6','Content/Video/VideoList.aspx')"><%=Resources.L.视频管理 %></a></li>
		<li class="menu2_5">
			<div class="btn-group topdrop">
				<button type="button" onclick="showleft('menu2_5','Content/Collect/CollectionManage.aspx')" class="btn btn-default  btn-sm "><%=Resources.L.采集检索 %></button>
				<button type="button" class="btn btn-default  btn-sm dropdown-toggle" data-toggle="dropdown" aria-expanded="false">
					<span class="caret"></span><span class="sr-only"></span>
				</button>
				<ul class="dropdown-menu" role="menu">
					<li><a href="javascript:;" data-url="Content/Collect/CollectionStep1.aspx"><%=Resources.L.添加采集 %></a></li>
					<li><a href="javascript:;" data-url="Content/Collect/CollectionManage.aspx"><%=Resources.L.采集项目 %></a></li>
					<li><a href="javascript:;" data-url="Content/Collect/CollectionStart.aspx"><%=Resources.L.开始采集 %></a></li>
					<li><a href="javascript:;" data-url="Content/Collect/CollectionStatus.aspx"><%=Resources.L.采集进度 %></a></li>
					<li><a href="javascript:;" data-url="Content/Collect/CollectionList.aspx"><%=Resources.L.采集记录 %></a></li>
					<li><a href="javascript:;" data-url="Content/Collect/CollSite.aspx"><%=Resources.L.统一检索 %></a></li>
					<li><a href="javascript:;" data-url="Content/Collect/InfoLog.aspx"><%=Resources.L.检索动态 %></a></li>
				</ul>
			</div>
		</li>
		<li class="menu2_8"><a onclick="ShowMain('NodeTree.ascx?url=Content/ContentRecycle.aspx','Content/ContentRecycle.aspx');"><%=Resources.L.回收站 %></a></li>
		<li class="menu2_9"><a onclick="showleft('menu2_9','Guest/GuestCateMana.aspx?Type=1')"><%=Resources.L.百科问答贴吧 %></a></li>
		<li class="menu2_10"><a onclick="showleft('menu2_10','AddOn/SourceManage.aspx')"><%=Resources.L.内容参数 %></a></li>
	</ul>
	<ul class="menu3">
		<li class="menu3_1 active"><a onclick="ShowMain('ShopNodeTree.ascx','Shop/ProductManage.aspx');"><%=Resources.L.商品管理 %></a></li>
		<li class="menu3_2"><a onclick="showleft('menu3_2','Shop/StockManage.aspx')"><%=Resources.L.库存管理 %></a></li>
		<li class="menu3_3"><a onclick="showleft('menu3_3','Shop/OrderList.aspx')"><%=Resources.L.订单管理 %></a></li>
		<li class="menu3_4"><a onclick="showleft('menu3_4','Shop/BankRollList.aspx')"><%=Resources.L.明细记录 %></a></li>
		<li class="menu3_5"><a onclick="showleft('menu3_5','Shop/ProductSale.aspx')"><%=Resources.L.销售统计 %></a></li>
		<li class="menu3_6"><a onclick="showleft('menu3_6','Shop/PresentProject.aspx')"><%=Resources.L.促销优惠 %></a></li>
		<li class="menu3_7"><a onclick="showleft('menu3_7','Shop/DeliverType.aspx')"><%=Resources.L.商城配置 %></a></li>
		<li class="menu3_8"><a onclick="showleft('menu3_8','User/Promo/PromoList.aspx')"><%=Resources.L.推广中心 %></a></li>
		<li class="menu3_9"><a onclick="ShowMain('ShopRecycle.ascx','Shop/ShopRecycler.aspx');"><%=Resources.L.商品回收站 %></a></li>
		<li class="menu3_10"><a onclick="showleft('menu3_10','Shop/IDC/IDCOrder.aspx?OrderType=7')">IDC管理</a></li>
		<li class="menu3_13">
			<div class="btn-group topdrop">
				<button type="button" onclick="showleft('menu3_13','Content/ModelManage.aspx?ModelType=6')" class="btn btn-default  btn-sm "><%=Resources.L.店铺管理 %></button>
				<button type="button" class="btn btn-default  btn-sm dropdown-toggle" data-toggle="dropdown" aria-expanded="false">
					<span class="caret"></span><span class="sr-only"></span>
				</button>
				<ul class="dropdown-menu" role="menu">
					<li class="active"><a href="javascript:;" data-url="UserShopManage/StoreManage.aspx"><%=Resources.L.店铺审核 %></a></li>
					<li><a href="javascript:;" data-url="Content/ModelManage.aspx?ModelType=6"><%=Resources.L.申请模型 %></a></li>
					<li><a href="javascript:;" data-url="UserShopManage/StoreStyleManage.aspx"><%=Resources.L.店铺模板 %></a></li>
					<li><a href="javascript:ShowMain('UserShopNodeTree.ascx','Shop/ProductManage.aspx?StoreID=-1',this);"><%=Resources.L.商品管理 %></a></li>
					<li><a href="javascript:;" data-url="Content/ModelManage.aspx?ModelType=5"><%=Resources.L.店铺模型 %></a></li>
					<li><a href="javascript:;" data-url="UserShopManage/ShopinfoManage.aspx"><%=Resources.L.店铺配置 %></a></li>
				</ul>
			</div>
		</li>
		<li class="menu3_14"><a onclick="showleft('menu3_14','/3D/3DManage.aspx')">3D<%=Resources.L.店铺管理 %></a></li>
	</ul>
	<ul class="menu4">
		<li class="menu4_1 active"><a onclick="showleft('menu4_1','Page/UserModelManage.aspx')"><%=Resources.L.黄页申请 %></a></li>
		<li class="menu4_2"><a onclick="showleft('menu4_2','Page/PageManage.aspx')"><%=Resources.L.黄页审核 %></a></li>
		<li class="menu4_4"><a onclick="ShowMain('PageTree.ascx','Page/PageContent.aspx')"><%=Resources.L.内容管理 %></a></li>
		<li class="menu4_5"><a onclick="showleft('menu4_5','Content/ModelManage.aspx?ModelType=4')"><%=Resources.L.黄页模型 %></a></li>
		<li class="menu4_6"><a onclick="showleft('menu4_2','Page/PageStyle.aspx')"><%=Resources.L.黄页样式 %></a></li>
		<li class="menu4_7"><a onclick="showleft('menu4_2','Page/PageConfig.aspx')"><%=Resources.L.黄页配置 %></a></li>
	</ul>
	<ul class="menu5">
		<li class="menu5_1"><a onclick="ShowMain('QuestGuide.ascx?url=Exam/Papers_System_Manage.aspx','Exam/Papers_System_Manage.aspx')"><%=Resources.L.试卷管理 %></a></li>
		<li class="menu5_9"><a onclick="ShowMain('QuestGuide.ascx','Exam/QuestList.aspx')"><%=Resources.L.试题管理 %></a></li>
		<li class="menu5_10"><a onclick="ShowMain('ExamGuide.ascx','Exam/QuestList.aspx')"><%=Resources.L.按年级管理 %></a></li>
		<li class="menu5_7"><a onclick="showleft('menu5_7','Exam/Papers_System_Manage.aspx')"><%=Resources.L.组卷配置 %></a></li>
		<li class="menu5_2"><a onclick="showleft('menu5_2','Exam/ToScore.aspx')"><%=Resources.L.阅卷中心 %></a></li>
		<li class="menu5_3"><a onclick="showleft('menu5_3','Zone/SnsClassRoom.aspx')"><%=Resources.L.班级管理 %></a></li>
		<li class="menu5_4"><a onclick="showleft('menu5_4','AddCRM/CustomerList.aspx?usertype=0')"><%=Resources.L.学员管理 %></a></li>
		<li class="menu5_5"><a onclick="showleft('menu5_5','Exam/ExTeacherManage.aspx')"><%=Resources.L.培训资源库 %></a></li>
		<li class="menu5_6"><a onclick="showleft('menu5_6','Shop/OrderList.aspx')"><%=Resources.L.财务管理 %></a></li>
		<li class="menu5_8"><a onclick="showleft('menu5_8','Exam/News/News.aspx')"><%=Resources.L.数字出版 %></a></li>
	</ul>
	<ul class="menu6">
		<li class="menu6_1 active"><a onclick="ShowMain('UserGuide.ascx','User/UserManage.aspx');"><%=Resources.L.会员管理 %></a></li>
		<li class="menu6_2"><a onclick="showleft('menu6_2','User/AdminManage.aspx')"><%=Resources.L.管理员管理 %></a></li>
		<li class="menu6_3"><a onclick="showleft('menu6_3','User/PermissionInfo.aspx')"><%=Resources.L.用户角色 %></a></li>
		<li class="menu6_4"><a onclick="showleft('menu6_4','User/Mail/SendMailList.aspx')"><%=Resources.L.信息发送 %></a></li>
		<li class="menu6_5">
			<div class="btn-group topdrop">
				<button type="button" onclick="showleft('menu6_5','User/Mail/SubscriptListManage.aspx?menu=audit')" class="btn btn-default  btn-sm "><%=Resources.L.订阅中心 %></button>
				<button type="button" class="btn btn-default  btn-sm dropdown-toggle" data-toggle="dropdown" aria-expanded="false">
					<span class="caret"></span><span class="sr-only"></span>
				</button>
				<ul class="dropdown-menu" role="menu">
					<li><a href="javascript:;" data-url="User/Mail/SubscriptListManage.aspx?menu=all"><%=Resources.L.邮件订阅 %></a></li>
					<li><a href="javascript:;" data-url="User/Mail/MailSysTlp.aspx"><%=Resources.L.邮件模板 %></a></li>
					<li><a href="javascript:;" data-url="User/Mail/SendSubMail.aspx"><%=Resources.L.发送邮件 %></a></li>
					<li><a href="javascript:;" data-url="User/Mail/MailIdiographList.aspx"><%=Resources.L.签名管理 %></a></li>
				</ul>
			</div>
		</li>
		<li class="menu6_6"><a onclick="showleft('menu6_6','User/Jobsconfig.aspx')"><%=Resources.L.人才招聘 %></a></li>
		<li class="menu6_7"><a onclick="showleft('menu6_7','Zone/ZoneConfig.aspx')"><%=Resources.L.SNS社区 %></a></li>
		<li class="menu6_8"><a onclick="showleft('menu6_8','APP/WSApi.aspx')"><%=Resources.L.整合接口 %></a></li>
	</ul>
	<ul class="menu7">
		<li class="menu7_1 active"><a onclick="showleft('menu7_1','Config/DatalistProfile.aspx')"><%=Resources.L.开发中心 %></a></li>
		<li class="menu7_2"><a onclick="showleft('menu7_2','Plus/ADZoneManage.aspx')"><%=Resources.L.广告管理 %></a></li>
		<li class="menu7_3"><a onclick="showleft('menu7_3','Counter/Counter.aspx')"><%=Resources.L.访问统计 %></a></li>
		<li class="menu7_4"><a onclick="showleft('menu7_4','Plus/SurveyManage.aspx')"><%=Resources.L.问卷调查 %></a></li>
		<li class="menu7_5"><a onclick="showleft('menu7_5','File/UploadFile.aspx')"><%=Resources.L.文件管理 %></a></li>
		<li class="menu7_6"><a onclick="showleft('menu7_6','Addon/DictionaryManage.aspx')"><%=Resources.L.数据字典 %></a></li>
		<li class="menu7_7"><a onclick="showleft('menu7_7','File/Addlinkhttp.aspx')"><%=Resources.L.站内链接 %></a></li>
		<li class="menu7_8"><a onclick="showleft('menu7_8','Copyright/Config.aspx')">版权中心</a></li>
		<li class="menu7_11"><a onclick="showleft('menu7_11','Iplook/IPManage.aspx')"><%=Resources.L.其他功能 %></a></li>
		<li class="menu7_12"><a onclick="showleft('menu7_12','Template/Code/PageList.aspx')"><%=Resources.L.扩展页面 %></a></li>
	</ul>
	<ul class="menu8">
		<li class="menu8_1 active"><a onclick="showleft('menu8_1','Config/SiteInfo.aspx')"><%=Resources.L.网站配置 %></a></li>
		<li class="menu8_2"><a onclick="showleft('menu8_2','Content/ModelManage.aspx?ModelType=1')"><%=Resources.L.模型管理 %></a></li>
		<li class="menu8_3"><a onclick="showleft('menu8_3','Content/NodeManage.aspx')"><%=Resources.L.节点管理 %></a></li>
		<li class="menu8_4"><a onclick="showleft('menu8_4','Content/Flow/FlowManager.aspx')"><%=Resources.L.流程管理 %></a></li>
		<li class="menu8_5"><a onclick="showleft('menu8_5','Content/SpecialManage.aspx')"><%=Resources.L.专题管理 %></a></li>
		<li class="menu8_6"><a onclick="ShowMain('LabelGuide.ascx','Template/TemplateSet.aspx')"><%=Resources.L.模板风格 %></a></li>
		<li class="menu8_7"><a onclick="ShowMain('LabelGuide.ascx','Template/LabelManage.aspx')"><%=Resources.L.标签管理 %></a></li>
		<li class="menu8_8"><a onclick="showleft('menu8_8','Pay/PayPlatManage.aspx')"><%=Resources.L.在线支付 %></a></li>
		<li class="menu9_8"><a onclick="showleft('menu9_8','Plus/LogManage.aspx?LogType=4')"><%=Resources.L.日志管理 %></a></li>
	</ul>
	<ul class="menu9">
		<li class="menu9_1 active"><a onclick="showleft('menu9_1','WorkFlow/OAConfig.aspx')">OA<%=Resources.L.办公 %></a></li>
		<li class="menu9_2"><a onclick="ShowMain('StructTree.ascx','AddOn/StructList.aspx')"><%=Resources.L.组织结构 %></a></li>
		<li class="menu9_9"><a onclick="showleft('menu9_9','Plat/PlatInfoManage.aspx')"><%=Resources.L.能力中心 %></a></li>
		<li class="menu9_3"><a onclick="showleft('menu9_3','AddOn/Project/Projects.aspx')"><%=Resources.L.项目管理 %></a></li>
		<li class="menu9_4"><a onclick="showleft('menu9_4','AddCRM/CustomerList.aspx?&modelid=48&usertype=0')"><%=Resources.L.CRM管理 %></a></li>
		<li class="menu9_5"><a onclick="showleft('menu9_5','iServer/BiServer.aspx?num=-3')"><%=Resources.L.有问必答 %></a></li>
		<li class="menu9_11"><a onclick="showleft('menu9_11','Content/ECharts/AddChart.aspx')"><%=Resources.L.智慧图表 %></a></li>
		<li class="menu9_10"><a onclick="ShowMain('SenTree.ascx','Sentiment/Default.aspx')"><%=Resources.L.舆情监测 %></a></li>
		<li class="menu9_6"><a onclick="showleft('menu9_6','User/Service/ServiceSeat.aspx')"><%=Resources.L.客服通 %></a></li>
		<li class="menu9_7"><a onclick="showleft('menu9_7','Workload/WorkCount.aspx')"><%=Resources.L.工作统计 %></a></li>

	</ul>
	<ul class="menu10">
		<li class="menu10_6"><a onclick="showleft('menu10_6','Design/SceneList.aspx')">H5创作</a></li>
		<li class="menu10_1 active"><a onclick="showleft('menu10_1','WeiXin/home.aspx')"><%=Resources.L.移动应用 %></a></li>
		<li class="menu10_2"><a onclick="showleft('menu10_2','WeiXin/home.aspx')"><%=Resources.L.微信应用 %></a></li>
		<li class="menu10_3"><a onclick="showleft('menu10_1','/Tools/Mobile.aspx')"><%=Resources.L.移动浏览器 %></a></li>
		<li class="menu10_4"><a onclick="showleft('menu10_3','Shop/Printer/ListDevice.aspx')">智能硬件</a></li>
		<li class="menu10_5"><a onclick="showleft('menu10_4','Mobile/Push/APIList.aspx')">消息推送</a></li>
	</ul>
	<ul class="menu11">
		<li class="menu11_1 active"><a onclick="showleft('menu11_1','Site/SiteConfig.aspx')"><%=Resources.L.全局配置 %></a></li>
		<li class="menu11_2"><a onclick="showleft('menu11_1','Site/ServerClusterConfig.aspx')"><%=Resources.L.服务器集群 %></a></li>
		<li class="menu11_3"><a onclick="showleft('menu11_1','Site/DBManage.aspx')"><%=Resources.L.数据库管理 %></a></li>
		<li class="menu11_4"><a onclick="showleft('menu11_1','Site/Default.aspx')"><%=Resources.L.站点列表 %></a></li>
		<li class="menu11_5"><a onclick="showleft('menu11_1','Site/SitePool.aspx')"><%=Resources.L.应用程序池 %></a></li>
		<li class="menu11_6"><a onclick="showleft('menu11_1','Template/CloudLead.aspx')"><%=Resources.L.模板云台 %></a></li>
		<li class="menu11_7"><a onclick="showleft('menu11_1','Site/SiteCloudSetup.aspx')"><%=Resources.L.快云安装 %></a></li>
		<li class="menu11_8"><a onclick="showleft('menu11_1','Site/SiteDataCenter.aspx')"><%=Resources.L.智能采集 %></a></li>
		<li class="menu11_9"><a onclick="showleft('menu11_2','Site/DomName.aspx')"><%=Resources.L.域名注册 %></a></li>
		<li class="menu11_10"><a onclick="showleft('menu11_10','design/SiteList.aspx')">动力设计</a></li>
	</ul>
</div>
</div>
<div class="m_main">
<div class="padding0 col-lg-2 col-md-2 col-sm-2 col-xs-2 main_left border_right hidden-xs" style="height: 100%;" id="left">
	<div id="left_ul_div">
		<div id="menu1" class="unstyled m_left_ul open">
			<ul id="menu1_1" class="m_left_ulin open">
				<li class="menu_tit"><span class="fa fa-chevron-down"></span><%=Resources.L.便捷菜单 %> </li>
				<li id="menu1_1_0"><a onclick="ShowMain('NodeTree.ascx','Content/ContentManage.aspx');"><%=Resources.L.内容管理 %></a></li>
				<li id="menu1_1_1"><a onclick="ShowMain('ShopNodeTree.ascx','Shop/ProductManage.aspx');"><%=Resources.L.商品管理 %></a></li>
				<li id="menu1_1_2"><a onclick="showleft('menu3_3','Shop/OrderList.aspx')"><%=Resources.L.订单管理 %></a></li>
				<li id="menu1_1_3"><a onclick="showleft('menu8_2','Content/ModelManage.aspx?ModelType=1')"><%=Resources.L.模型管理 %></a></li>
				<li id="menu1_1_4"><a onclick="showleft('menu8_3','Content/NodeManage.aspx')"><%=Resources.L.节点管理 %></a></li>
				<li id="menu1_1_5"><a onclick="ShowMain('LabelGuide.ascx','Template/TemplateSet.aspx')"><%=Resources.L.模版管理 %></a></li>
				<li id="menu1_1_6"><a onclick="ShowMain('LabelGuide.ascx','Template/LabelManage.aspx')"><%=Resources.L.标签管理 %></a></li>
				<li id="menu1_1_7"><a onclick="ShowMain('UserGuide.ascx','User/UserManage.aspx');"><%=Resources.L.会员管理 %></a></li>
				<li id="menu1_1_8"><a onclick="showleft('menu2_7','Content/CreateHtmlContent.aspx')"><%=Resources.L.生成发布 %></a></li>
				<li id="menu1_1_9"><a onclick="showleft('menu8_1','Config/SiteInfo.aspx')"><%=Resources.L.网站配置 %></a></li>
				<li id="menu1_1_10"><a onclick="showleft('menu3_10','Shop/IDC/IDCOrder.aspx?OrderType=7')">IDC管理</a></li>
			</ul>
			<ul id="menu1_2" class="m_left_ulin">
				<li class="menu_tit"><span class="fa fa-chevron-down"></span><%=Resources.L.便捷导航 %> </li>
				<li>
					<div class="input-group">
						<input type="text" id="searchkeyword" class="form-control" onkeydown="return ASCX.OnEnterSearch('Guest/AllSearch.aspx?keyWord=',this);" placeholder="<%=Resources.L.导航标题 %>" />
						<span class="input-group-btn">
							<button class="btn btn-default" type="button" onclick="ASCX.Search('Config/FuncSearch.aspx?keyWord=','searchkeyword');"><span class="fa fa-search"></span></button>
						</span>
					</div>
				</li>
				<li id="menu1_2_1"><a href="javascript:;" data-url="Config/SearchFunc.aspx"><%=Resources.L.管理导航 %></a></li>
				<li id="menu1_2_2"><a href="javascript:;" data-url="Config/UserFunc.aspx?EliteLevel=1"><%=Resources.L.会员导航 %></a></li>
			</ul>
			<ul id="menu1_3" class="m_left_ulin">
				<li class="menu_tit"><span class="fa fa-chevron-down"></span><%=Resources.L.修改密码 %> </li>
				<li id="menu1_3_1"><a href="javascript:;" data-url="Profile/ModifyPassword.aspx"><%=Resources.L.修改密码 %></a></li>
			</ul>
			<ul id="menu1_4" class="m_left_ulin">
				<li id="menu1_4_1"><a href="{$path}/SignOut.aspx"><%=Resources.L.安全退出 %></a></li>
			</ul>
		</div>
		<div id="menu2" class="m_left_ul">
			<ul class="list-unstyled m_left_ulin open" id="menu2_1"></ul>
			<ul class="list-unstyled m_left_ulin" id="menu2_2">
				<li id="menu2_2_1"><a href="javascript:ShowMain('','Content/SpecContent.aspx');"><%=Resources.L.网站专题 %></a></li>
			</ul>
			<ul class="list-unstyled m_left_ulin" id="menu2_3">
				<li class="menu_tit"><span class="fa fa-chevron-down"></span><%=Resources.L.评论管理 %> </li>
				<li id="menu2_3_1"><a href="javascript:;" data-url="Content/CommentManage.aspx"><%=Resources.L.评论管理 %></a></li>
				<li id="menu2_3_2"><a href="javascript:;" data-url="Content/SohuChatManage.aspx"><%=Resources.L.畅言评论管理 %></a></li>
				<li id="menu2_3_3"><a href="javascript:;" data-url="Content/SohuChatManage.aspx?show=2"><%=Resources.L.畅言评论配置 %></a></li>
			</ul>
			<ul class="list-unstyled m_left_ulin" id="menu2_4">
				<li class="menu_tit"><span class="fa fa-chevron-down"></span><%=Resources.L.互动模块 %></li>
				<li id="menu2_4_2"><a href="javascript:;" data-url="Pub/PubManage.aspx"><%=Resources.L.互动模块 %></a></li>
				<li id="menu2_4_1"><a href="javascript:;" data-url="Pub/FormManage.aspx"><%=Resources.L.互动表单 %></a></li>
				<li id="menu2_4_3"><a href="javascript:;" data-url="Pub/PubManage.aspx"><%=Resources.L.互动信息 %></a></li>
				<li id="menu2_4_4"><a href="javascript:;" data-url="Content/ModelManage.aspx?ModelType=7"><%=Resources.L.互动模型 %></a></li>
				<li id="menu2_4_5"><a href="javascript:;" data-url="Pub/PubRecycler.aspx"><%=Resources.L.互动存档 %></a></li>
				<li id="menu2_4_6"><a href="javascript:;" data-url="Pub/PubExcel.aspx"><%=Resources.L.Excel导出 %></a></li>
			</ul>
			<ul class="list-unstyled m_left_ulin" id="menu2_5">
				<li class="menu_tit"><span class="fa fa-chevron-down"></span><%=Resources.L.采集检索 %></li>
				<li id="menu2_5_1"><a href="javascript:;" data-url="Content/Collect/CollectionManage.aspx"><%=Resources.L.采集项目 %></a></li>
				<li id="menu2_5_3"><a href="javascript:;" data-url="Content/Collect/CollectionStart.aspx"><%=Resources.L.开始采集 %></a></li>
				<li id="menu2_5_4"><a href="javascript:;" data-url="Content/Collect/CollectionStatus.aspx"><%=Resources.L.采集进度 %></a></li>
				<li id="menu2_5_5"><a href="javascript:;" data-url="Content/Collect/CollectionList.aspx"><%=Resources.L.采集记录 %></a></li>
				<li id="menu2_5_6"><a href="javascript:;" data-url="Content/Collect/CollSite.aspx"><%=Resources.L.统一检索 %></a></li>
				<li id="menu2_5_7"><a href="javascript:;" data-url="Content/Collect/InfoLog.aspx"><%=Resources.L.检索动态 %></a></li>
			</ul>
			<ul class="list-unstyled m_left_ulin" id="menu2_6">
				<li class="menu_tit"><span class="fa fa-chevron-down"></span><%=Resources.L.视频管理 %></li>
				<li id="menu2_6_2"><a href="javascript:;" data-url="Content/Video/VideoList.aspx"><%=Resources.L.视频列表 %></a></li>
				<li id="menu2_6_3"><a href="javascript:;" data-url="Content/Video/VideoConfig.aspx"><%=Resources.L.视频配置 %></a></li>
				<li id="menu2_6_4"><a href="javascript:;" data-url="Content/Video/VideoPath.aspx"><%=Resources.L.视频路径 %></a></li>
			</ul>
			<ul class="list-unstyled m_left_ulin" id="menu2_7">
				<li class="menu_tit"><span class="fa fa-chevron-down"></span><%=Resources.L.生成发布 %></li>
				<li id="menu2_7_1"><a href="javascript:;" data-url="Content/CreateHtmlContent.aspx"><%=Resources.L.生成发布 %></a></li>
				<li id="menu2_7_3"><a href="javascript:;" data-url="Content/ListHtmlContent.aspx"><%=Resources.L.生成管理 %></a></li>
				<li id="menu2_7_4"><a href="javascript:;" data-url="Content/SiteMap.aspx"><%=Resources.L.站点地图 %></a></li>
				<li id="menu2_7_5"><a href="javascript:;" data-url="Content/ManageJsContent.aspx"><%=Resources.L.JS生成管理 %></a></li>
			</ul>
			<ul class="list-unstyled m_left_ulin" id="menu2_8">
				<li id="menu2_8_1"><a href="javascript:;" data-url="Content/ContentRecycle.aspx"><%=Resources.L.节点栏目导航 %></a></li>
			</ul>
			<ul class="list-unstyled m_left_ulin" id="menu2_9">
                <li><p class="bg-info margin_b0px"><i class="fa fa-chevron-down"></i>百科问答</p></li>
				<li>
					<p class="bg-info laybtn"><i class="fa fa-arrow-circle-down"></i><%=Resources.L.论坛留言 %></p>
					<ul class="list-unstyled">
						<li id="menu2_9_6"><a href="javascript:;" data-url="Guest/GuestCateMana.aspx?Type=1"><%=Resources.L.贴吧版面 %></a></li>
						<li id="menu2_9_1"><a href="javascript:;" data-url="Guest/GuestManage.aspx"><%=Resources.L.留言分类 %></a></li>
					</ul>
				</li>
				<li>
					<p class="bg-info laybtn"><i class="fa fa-arrow-circle-down"></i><%=Resources.L.问答管理 %></p>
					<ul class="list-unstyled">
						<li id="menu2_9_4"><a href="javascript:;" data-url="Guest/WdCheck.aspx"><%=Resources.L.问答管理 %></a></li>
						<li id="menu2_9_5"><a href="javascript:;" data-url="AddOn/GradeOption.aspx?CateID=2"><%=Resources.L.问答分类 %></a></li>
						<li id="menu2_9_7"><a href="javascript:;" data-url="Guest/WDConfig.aspx"><%=Resources.L.问答配置 %></a></li>
					</ul>
				</li>
				<li>
					<p class="bg-info laybtn"><i class="fa fa-arrow-circle-down"></i><%=Resources.L.百科管理 %></p>
					<ul class="list-unstyled">
						<li id="menu2_9_9"><a href="javascript:;" data-url="Guest/BKVersionList.aspx">词条版本</a></li>
						<li id="menu2_9_2"><a href="javascript:;" data-url="Guest/BkCheck.aspx"><%=Resources.L.百科词条 %></a></li>
						<li id="menu2_9_3"><a href="javascript:;" data-url="AddOn/GradeOption.aspx?CateID=3"><%=Resources.L.百科分类 %></a></li>
						<li id="menu2_9_8"><a href="javascript:;" data-url="Guest/BKConfig.aspx"><%=Resources.L.百科设置 %></a></li>
					</ul>
				</li>
				<li>
					<div class="input-group">
						<input type="text" id="tiekeyword" class="form-control" onkeydown="return ASCX.OnEnterSearch('Guest/AllSearch.aspx?keyWord=',this);" placeholder="<%=Resources.L.贴吧 %>,<%=Resources.L.百科 %>,<%=Resources.L.留言标题 %>" />
						<span class="input-group-btn">
							<button class="btn btn-default" type="button" onclick="ASCX.Search('Guest/AllSearch.aspx?keyWord=','tiekeyword');"><span class="fa fa-search"></span></button>
						</span>
					</div>
				</li>
			</ul>
			<ul class="list-unstyled m_left_ulin" id="menu2_10">
				<li class="menu_tit"><span class="fa fa-chevron-down"></span><%=Resources.L.内容参数 %></li>
				<li id="menu2_10_1"><a href="javascript:;" data-url="AddOn/SourceManage.aspx"><%=Resources.L.来源管理 %></a></li>
				<li id="menu2_10_2"><a href="javascript:;" data-url="AddOn/AuthorManage.aspx"><%=Resources.L.作者管理 %></a></li>
				<li id="menu2_10_3"><a href="javascript:;" data-url="AddOn/KeyWordManage.aspx"><%=Resources.L.关键字集 %></a></li>
				<li id="menu2_10_4"><a href="javascript:;" data-url="AddOn/CorrectManage.aspx"><%=Resources.L.纠错管理 %></a></li>
			</ul>
		</div>
		<div id="menu3" class="m_left_ul">
			<ul class="list-unstyled m_left_ulin open" id="menu3_1">
			</ul>
			<ul id="menu3_2" class="m_left_ulin">
				<li class="menu_tit"><span class="fa fa-chevron-down"></span><%=Resources.L.库存管理 %></li>
				<li id="menu3_2_1"><a href="javascript:;" data-url="Shop/StockManage.aspx"><%=Resources.L.库存管理 %></a></li>
				<li id="menu3_2_2"><a href="javascript:;" data-url="Shop/Stock.aspx"><%=Resources.L.入库出库 %></a></li>
			</ul>
			<ul id="menu3_3" class="m_left_ulin">
				<li class="menu_tit"><span class="fa fa-chevron-down"></span><%=Resources.L.订单管理 %></li>
				<li id="menu3_3_2"><a href="javascript:;" data-url="Shop/OrderList.aspx"><%=Resources.L.商城订单 %></a></li>
				<li id="menu3_3_3"><a href="javascript:;" data-url="Shop/OrderList.aspx?StoreID=-99"><%=Resources.L.店铺订单 %></a></li>
				<li id="menu3_3_17"><a href="javascript:;" data-url="Shop/OrderList.aspx?OrderType=10"><%=Resources.L.代购订单 %></a></li>
				<li id="menu3_3_6"><a href="javascript:;" data-url="Shop/OrderList.aspx?OrderType=1"><%=Resources.L.酒店订单 %></a></li>
				<li id="menu3_3_7"><a href="javascript:;" data-url="Shop/OrderList.aspx?OrderType=2"><%=Resources.L.机票订单 %></a></li>
				<li id="menu3_3_8"><a href="javascript:;" data-url="Shop/OrderList.aspx?OrderType=3"><%=Resources.L.旅游订单 %></a></li>
				<li id="menu3_3_9"><a href="javascript:;" data-url="Shop/OrderList.aspx?OrderType=5"><%=Resources.L.域名订单 %></a></li>
				<li id="menu3_3_9"><a href="javascript:;" data-url="Shop/OrderList.aspx?OrderType=11">捐赠订单</a></li>
				<li id="menu3_3_4"><a href="javascript:;" data-url="Shop/OrderList.aspx?orderstatus=-1"><%=Resources.L.退款订单 %></a></li>
				<li id="menu3_3_19"><a href="javascript:;" data-url="Shop/OrderRepairAudit.aspx"><%=Resources.L.返修退货申请 %></a></li>
				<li id="menu3_3_11"><a href="javascript:;" data-url="Shop/FillOrder.aspx"><%=Resources.L.补订单 %></a></li>
				<li id="menu3_3_5"><a href="javascript:;" data-url="Shop/OrderSql.aspx"><%=Resources.L.账单管理 %></a></li>
				<li id="menu3_3_1"><a href="javascript:;" data-url="Shop/CartManage.aspx"><%=Resources.L.购物车记录 %></a></li>
				<li id="menu3_3_12"><a href="javascript:;" data-url="Shop/LocationReport.aspx"><%=Resources.L.省市报表 %></a></li>
				<li id="menu3_3_13"><a href="javascript:;" data-url="Shop/MonthlyReport.aspx"><%=Resources.L.月报表 %></a></li>
				<li id="menu3_3_15"><a href="javascript:;" data-url="Shop/SystemOrderModel.aspx?Type=0"><%=Resources.L.订单模型 %></a></li>
				<li id="menu3_3_16"><a href="javascript:;" data-url="Shop/SystemOrderModel.aspx?Type=1">购物车模型</a></li>
				<li id="menu3_3_14"><a href="javascript:;" data-url="Shop/Printer/MessageList.aspx">打印流水</a></li>
			</ul>
			<ul id="menu3_4" class="m_left_ulin">
				<li class="menu_tit"><span class="fa fa-chevron-down"></span><%=Resources.L.明细记录 %></li>
				<li id="menu3_4_1"><a href="javascript:;" data-url="Shop/BankRollList.aspx"><%=Resources.L.资金明细 %></a></li>
				<li id="menu3_4_2"><a href="javascript:;" data-url="Shop/SaleList.aspx"><%=Resources.L.销售明细 %></a></li>
				<li id="menu3_4_3"><a href="javascript:;" data-url="Shop/PayList.aspx"><%=Resources.L.支付明细 %></a></li>
				<li id="menu3_4_4"><a href="javascript:;" data-url="Shop/InvoiceList.aspx"><%=Resources.L.发票明细 %></a></li>
				<li id="menu3_4_5"><a href="javascript:;" data-url="Shop/DeliverList.aspx"><%=Resources.L.退货明细 %></a></li>
			</ul>
			<ul id="menu3_5" class="m_left_ulin">
				<li class="menu_tit"><span class="fa fa-chevron-down"></span><%=Resources.L.销售统计 %></li>
				<li id="menu3_5_1"><a href="javascript:;" data-url="Shop/TotalSale.aspx"><%=Resources.L.总体销售 %></a></li>
				<li id="menu3_5_2"><a href="javascript:;" data-url="Shop/ProductSale.aspx"><%=Resources.L.商品销售 %></a></li>
				<li id="menu3_5_3"><a href="javascript:;" data-url="Shop/CategotySale.aspx"><%=Resources.L.类别销售 %></a></li>
				<li id="menu3_5_4"><a href="javascript:;" data-url="Shop/UserOrders.aspx"><%=Resources.L.订单排名 %></a></li>
			</ul>
			<ul id="menu3_6" class="m_left_ulin">
				<li class="menu_tit"><span class="fa fa-chevron-down"></span><%=Resources.L.促销优惠 %></li>
				<li id="menu3_6_1"><a href="javascript:;" data-url="Shop/PresentProject.aspx"><%=Resources.L.促销方案管理 %></a></li>
				<li id="menu3_6_2"><a href="javascript:;" data-url="Shop/AddPresentProject.aspx"><%=Resources.L.添加促销方案 %></a></li>
				<li id="menu3_6_3"><a href="javascript:;" data-url="Shop/AgioProject.aspx"><%=Resources.L.打折方案管理 %></a></li>
				<li id="menu3_6_5"><a href="javascript:;" data-url="Shop/Arrive/ArriveManage.aspx"><%=Resources.L.优惠券管理 %></a></li>
				<li id="menu3_6_6"><a href="javascript:;" data-url="Shop/Arrive/AddArrive.aspx?menu=add"><%=Resources.L.新增优惠券 %></a></li>
			</ul>
			<ul id="menu3_7" class="m_left_ulin">
				<li class="menu_tit"><span class="fa fa-chevron-down"></span><%=Resources.L.商城配置 %></li>
				<li id="menu3_7_1"><a href="javascript:;" data-url="Shop/DeliverType.aspx"><%=Resources.L.运费模板 %></a></li>
				<%--	<li id="menu3_7_2"><a href="javascript:;" data-url="Shop/FreePost.aspx"><%=Resources.L.免邮设置%></a></li>--%>
				<li id="menu3_7_3"><a href="javascript:;" data-url="Shop/ProducerManage.aspx"><%=Resources.L.厂商管理 %></a></li>
				<li id="menu3_7_4"><a href="javascript:;" data-url="Shop/TrademarkManage.aspx"><%=Resources.L.品牌管理 %></a></li>
				<li id="menu3_7_5"><a href="javascript:;" data-url="Shop/CardManage.aspx"><%=Resources.L.VIP卡管理 %></a></li>
				<li id="menu3_7_6"><a href="javascript:;" data-url="Shop/CashManage.aspx"><%=Resources.L.VIP卡提现 %></a></li>
				<li id="menu3_7_9"><a href="javascript:;" data-url="Shop/MoneyManage.aspx"><%=Resources.L.货币管理 %></a></li>
				<li id="menu3_7_10"><a href="javascript:;" data-url="Shop/InvtoManage.aspx"><%=Resources.L.发票计算 %></a></li>
			</ul>
			<ul id="menu3_8" class="m_left_ulin">
				<li class="menu_tit"><span class="fa fa-chevron-down"></span><%=Resources.L.推广中心 %></li>
				<li id="menu3_8_1"><a href="javascript:;" data-url="User/Promo/PromoChart.aspx"><%=Resources.L.图表分析 %></a></li>
				<li id="menu3_8_2"><a href="javascript:;" data-url="User/Promo/PromoList.aspx"><%=Resources.L.用户明细 %></a></li>
				<li id="menu3_8_3"><a href="javascript:;" data-url="User/Promo/PromoUserList.aspx"><%=Resources.L.用户流水 %></a></li>
				<li id="menu3_8_4"><a href="javascript:;" data-url="Shop/profile/StatisticsBriefing.aspx"><%=Resources.L.统计简报 %></a></li>
				<li id="menu3_8_5"><a href="javascript:;" data-url="User/UnitMain.aspx"><%=Resources.L.管理奖金 %></a></li>
				<!--<li id="menu3_12_2"><a href="javascript:;" data-url="Shop/profile/config.aspx">推广设置</a></li>-->
				<li id="menu3_8_6"><a href="javascript:;" data-url="User/DepositManage.aspx"><%=Resources.L.用户提现 %></a></li>
				<%--           <li id="menu3_8_7"><a href="javascript:;" data-url="Boss/UserBonus.aspx">会员分红</a></li>
<li id="menu3_8_8"><a href="javascript:;" data-url="Boss/PromoBonus.aspx">推广佣金</a></li>--%>
				<li id="menu3_8_9"><a href="javascript:;" data-url="Shop/PVManager.aspx"><%=Resources.L.订单提成 %></a></li>
				<%--  <li><p class="bg-info laybtn"><i class="fa fa-arrow-circle-down"></i>加盟管理</p>
	<ul class="list-unstyled">
		<li id="menu3_8_10"><a href="javascript:;" data-url="Guest/BkCheck.aspx">加盟商管理</a></li>
		<!--<li id="menu3_8_11"><a href="javascript:;" data-url="Shop/profile/LmUserManage.aspx">联盟会员</a></li>-->
		<!--<li id="menu3_12_4"><a href="javascript:;" data-url="Shop/profile/LmUserListTree.aspx">联盟会员树状图</a></li>-->
</ul></li>--%>
			</ul>
			<ul id="menu3_9" class="m_left_ulin">
				<li class="menu_tit"><span class="fa fa-chevron-down"></span><%=Resources.L.商品回收站 %></li>
				<li id="menu3_9_1"><a href="javascript:;" data-url="Shop/ShopRecycler.aspx"><%=Resources.L.商品回收站 %></a></li>
			</ul>
			<ul id="menu3_10" class="m_left_ulin">
				<li class="menu_tit"><span class="fa fa-chevron-down"></span>IDC管理</li>     
				<li id="menu3_12_1"><a href="javascript:;" data-url="Shop/IDC/IDCOrder.aspx"><%=Resources.L.IDC订单 %></a></li>
				<li id="menu3_12_2"><a href="javascript:;" data-url="Shop/OrderList.aspx?OrderType=9"><%=Resources.L.IDC续费 %></a></li>
			</ul>
			<%--	<ul id="menu3_12" class="m_left_ulin">
<li class="menu_tit"><span class="fa fa-chevron-down"></span>推广返利</li>
<li id="menu3_12_1"><a href="javascript:;" data-url="Shop/profile/StatisticsBriefing.aspx"><%=Resources.L.统计简报%></a></li>
<li id="menu3_12_2"><a href="javascript:;" data-url="Shop/profile/config.aspx"><%=Resources.L.推广设置%></a></li>
<li id="menu3_12_3"><a href="javascript:;" data-url="Shop/profile/LmUserManage.aspx"><%=Resources.L.联盟会员%></a></li>
<li id="menu3_12_4"><a href="javascript:;" data-url="User/DepositManage.aspx"><%=Resources.L.用户提现%></a></li>
<li id="menu3_12_5"><a href="javascript:;" data-url="Boss/UserBonus.aspx"><%=Resources.L.会员分红%></a></li>
<li id="menu3_12_6"><a href="javascript:;" data-url="Boss/PromoBonus.aspx"><%=Resources.L.推广佣金%></a></li>
<li id="menu3_12_7"><a href="javascript:;" data-url="User/UnitMain.aspx"><%=Resources.L.管理奖%></a></li>
<li id="menu3_12_8"><a href="javascript:;" data-url="Shop/PVManager.aspx"><%=Resources.L.订单提成%></a></li>
<li id="menu3_12_4"><a href="javascript:;" data-url="Shop/profile/LmUserListTree.aspx"><%=Resources.L.联盟会员树状图%></a></li>
</ul>--%>
			<ul id="menu3_13" class="m_left_ulin">
				<li class="menu_tit"><span class="fa fa-chevron-down"></span><%=Resources.L.店铺管理 %></li>
				<li id="menu3_13_1"><a href="javascript:;" data-url="UserShopManage/StoreManage.aspx"><%=Resources.L.店铺审核 %></a></li>
				<li id="menu3_13_2"><a href="javascript:;" data-url="Content/ModelManage.aspx?ModelType=6"><%=Resources.L.申请模型管理 %></a></li>
				<li id="menu3_13_3"><a href="javascript:;" data-url="UserShopManage/StoreStyleManage.aspx"><%=Resources.L.店铺模板管理 %></a></li>
				<li id="menu3_13_4"><a href="javascript:ShowMain('UserShopNodeTree.ascx','Shop/ProductManage.aspx?StoreID=-1',this);"><%=Resources.L.店铺商品管理 %></a></li>
				<li id="menu3_13_6"><a href="javascript:;" data-url="UserShopManage/ShopinfoManage.aspx"><%=Resources.L.店铺信息配置 %></a></li>
				<li id="menu3_13_8"><a href="javascript:;" data-url="UserShopManage/ShopSearchKey.aspx"><%=Resources.L.搜索管理 %></a></li>
				<li id="menu3_13_9"><a href="javascript:;" data-url="UserShopManage/ShopRemark.aspx"><%=Resources.L.评论管理 %></a></li>
				<li id="menu3_13_10"><a href="javascript:;" data-url="UserShopManage/ShopScutcheon.aspx"><%=Resources.L.品牌管理 %></a></li>
				<li id="menu3_13_11"><a href="javascript:;" data-url="UserShopManage/ShopGrade.aspx"><%=Resources.L.等级管理 %></a></li>
				<li id="menu3_13_12"><a href="javascript:;" data-url="UserShopManage/ProducerManage.aspx"><%=Resources.L.厂商管理 %></a></li>
				<li id="menu3_13_15"><a href="javascript:;" data-url="UserShopManage/ShopMailConfig.aspx"><%=Resources.L.店铺邮件设置 %></a></li>
			</ul>
			<ul id="menu3_14" class="m_left_ulin">
				<li class="menu_tit"><span class="fa fa-chevron-down"></span>3D<%=Resources.L.店铺管理 %></li>
				<li id="menu3_14_1"><a href="javascript:;" data-url="/3D/3DManage.aspx">3D<%=Resources.L.店铺管理 %></a></li>
				<li id="menu3_14_2"><a href="javascript:;" data-url="/3D/AddShop.aspx"><%=Resources.L.添加店铺 %></a></li>
			</ul>
		</div>
		<div id="menu4" class="m_left_ul">
			<ul class="m_left_ulin open" id="menu4_1">
				<li class="menu_tit"><span class="fa fa-chevron-down"></span><%=Resources.L.黄页申请 %></li>
				<li id="menu4_1_1"><a href="javascript:;" data-url="Page/UserModelManage.aspx"><%=Resources.L.申请模型管理 %></a></li>
				<li id="menu4_1_2"><a href="javascript:;" data-url="Page/UserModel.aspx"><%=Resources.L.添加申请模型 %></a></li>
			</ul>
			<ul class="m_left_ulin" id="menu4_2">
				<li class="menu_tit"><span class="fa fa-chevron-down"></span><%=Resources.L.黄页审核 %></li>
				<li id="menu4_2_1"><a href="javascript:;" data-url="page/PageManage.aspx"><%=Resources.L.黄页审核 %></a></li>
				<li id="menu4_2_3"><a href="javascript:;" data-url="Page/PageStyle.aspx"><%=Resources.L.黄页样式 %></a></li>
				<li id="menu4_2_4"><a href="javascript:;" data-url="Page/AddPageStyle.aspx"><%=Resources.L.添加样式 %></a></li>
				<li id="menu4_2_5"><a href="javascript:;" data-url="Page/PageConfig.aspx"><%=Resources.L.黄页配置 %></a></li>
			</ul>
			<%--<ul class="m_left_ulin" id="menu4_3">
<li class="menu_tit"><span class="fa fa-chevron-down"></span>黄页用户栏目管理</li>
<li id="menu4_3_1"><a href="javascript:;" data-url="page/PageAudit.aspx"><%=Resources.L.黄页用户栏目管理%></a></li>
</ul>--%>
			<ul class="m_left_ulin" id="menu4_4">
				<li class="menu_tit"><span class="fa fa-chevron-down"></span><%=Resources.L.黄页内容管理 %></li>
				<li id="menu4_4_1"><a href="javascript:;" data-url="page/PageContent.aspx"><%=Resources.L.黄页内容 %></a></li>
			</ul>
			<ul class="m_left_ulin" id="menu4_5">
				<li class="menu_tit"><span class="fa fa-chevron-down"></span><%=Resources.L.黄页模型管理 %></li>
				<li id="menu4_5_1"><a href="javascript:;" data-url="Content/ModelManage.aspx?ModelType=4"><%=Resources.L.黄页模型管理 %></a></li>
				<li id="menu4_5_2"><a href="javascript:;" data-url="Page/AddPageModel.aspx"><%=Resources.L.添加黄页模型 %></a></li>
			</ul>
			<ul class="m_left_ulin" id="menu4_6">
				<li class="menu_tit"><span class="fa fa-chevron-down"></span><%=Resources.L.黄页样式管理 %></li>
				<li id="menu4_6_1"><a href="javascript:;" data-url="Page/PageStyle.aspx"><%=Resources.L.黄页样式管理 %></a></li>
				<li id="menu4_6_2"><a href="javascript:;" data-url="Page/AddPageStyle.aspx"><%=Resources.L.添加黄页样式 %></a></li>
			</ul>
		</div>
		<div id="menu5" class="m_left_ul">
			<ul class="m_left_ulin open" id="menu5_1">
				<li class="menu_tit"><span class="fa fa-chevron-down"></span><%=Resources.L.考试管理 %></li>
				<li id="menu5_1_2"><a href="javascript:;" data-url="Exam/CoureseManage.aspx"><%=Resources.L.课程管理 %></a></li>
				<li id="menu5_1_4"><a href="javascript:;" data-url="Exam/Question_Class_Manage.aspx"><%=Resources.L.分类管理 %></a></li>
				<li id="menu5_1_5"><a href="javascript:;" data-url="Exam/ExamPointManage.aspx"><%=Resources.L.考点管理 %> </a></li>
				<%--	<li id="menu5_1_6"><a href="javascript:;" data-url="Exam/QuestionTypeManage.aspx"><%=Resources.L.题型管理 %></a></li>--%>
			</ul>
			<ul class="m_left_ulin" id="menu5_2">
				<li class="menu_tit"><span class="fa fa-chevron-down"></span><%=Resources.L.阅卷中心 %></li>
				<li id="menu5_2_1"><a href="javascript:;" data-url="Exam/ToScore.aspx"><%=Resources.L.评阅试卷 %></a></li>
				<%--	<li id="menu5_2_2"><a href="javascript:;" data-url="Exam/ScoreStatics.aspx"><%=Resources.L.成绩统计 %></a> </li>--%>
			</ul>
			<ul class="m_left_ulin" id="menu5_3">
				<li class="menu_tit"><span class="fa fa-chevron-down"></span><%=Resources.L.排课管理 %></li>
				<li id="menu5_3_1"><a href="javascript:;" data-url="Zone/SnsSchool.aspx"><%=Resources.L.学校管理%></a></li>
				<li id="menu5_3_2"><a href="javascript:;" data-url="Zone/SnsClassRoom.aspx"><%=Resources.L.班级管理 %></a></li>
				<%--			<li id="menu5_3_1"><a href="javascript:;" data-url="Exam/ClassManage.aspx"><%=Resources.L.班级管理 %></a></li>
<li id="menu5_3_3"><a href="javascript:;" data-url="Exam/InsertClass.aspx"><%=Resources.L.班级类别 %></a></li>--%>
				<li id="menu5_3_4"><a href="javascript:;" data-url="Exam/ExTeacherManage.aspx"><%=Resources.L.教师管理 %></a></li>
				<li id="menu5_3_5"><a href="javascript:;" data-url="Exam/SubjectManage.aspx"><%=Resources.L.学科管理 %></a></li>
			</ul>
			<ul class="m_left_ulin" id="menu5_4">
				<li class="menu_tit"><span class="fa fa-chevron-down"></span><%=Resources.L.学员管理 %></li>
				<li id="menu5_4_3"><a href="javascript:;" data-url="iServer/BselectiServer.aspx?num=-3&strTitle="><%=Resources.L.学员管理 %></a></li>
			</ul>
			<ul class="m_left_ulin" id="menu5_5">
				<li class="menu_tit"><span class="fa fa-chevron-down"></span><%=Resources.L.培训资源库 %></li>
				<li id="menu5_5_1"><a href="javascript:;" data-url="Exam/ApplicationManage.aspx"><%=Resources.L.培训资源库 %></a></li>
			</ul>
			<ul class="m_left_ulin" id="menu5_6">
				<li class="menu_tit"><span class="fa fa-chevron-down"></span><%=Resources.L.财务管理 %></li>
				<li id="menu5_6_1"><a href="javascript:;" data-url="Shop/OrderList.aspx"><%=Resources.L.所有订单 %></a></li>
				<li id="menu5_6_2"><a href="javascript:;" data-url="Shop/BankRollList.aspx"><%=Resources.L.销售明细 %></a></li>
				<li id="menu5_6_3"><a href="javascript:;" data-url="Shop/InvoiceList.aspx"><%=Resources.L.开发票明细 %></a></li>
				<li id="menu5_6_4"><a href="javascript:;" data-url="Shop/CartManage.aspx"><%=Resources.L.购物车记录 %></a></li>
				<li id="menu5_6_5"><a href="javascript:;" data-url="Shop/PayList.aspx"><%=Resources.L.支付明细 %></a></li>
			</ul>
			<ul class="m_left_ulin" id="menu5_7">
				<li>
					<div class="input-group ">
						<input type="text" placeholder="<%=Resources.L.试题名 %>" class="form-control" id="question_text" />
						<span class="input-group-btn">
							<a href="javascript:;" style="line-height: 21px; padding: 6px 12px;" data-url="" onclick="$(this).attr('data-url','Exam/QuestionManage.aspx?KeyWord='+$('#question_text').val());" class="btn btn-default"><span style="line-height: 21px;" class="fa fa-search"></span></a>
						</span>
					</div>
				</li>
				<li id="menu5_7_3"><a href="javascript:;" data-url="Exam/Setting.aspx"><%=Resources.L.配置中心 %></a></li>
				<li id="menu5_7_1"><a href="javascript:;" data-url="Exam/Paper_Class_Manage.aspx"><%=Resources.L.试卷节点 %></a></li>
				<li id="menu5_7_2"><a href="javascript:;" data-url="Exam/Question_Class_Manage.aspx"><%=Resources.L.试题科目 %></a></li>
				<%--   <li id="menu5_7_3"><a href="javascript:;" data-url="Exam/QuestGrade.aspx?cate=5"><%=Resources.L.难度管理 %></a></li>--%>
				<li id="menu5_7_4"><a href="javascript:;" data-url="Exam/QuestGrade.aspx?cate=6"><%=Resources.L.年级管理 %></a></li>
				<li id="menu5_7_5"><a href="javascript:;" data-url="Exam/VersionList.aspx"><%=Resources.L.教材版本 %></a></li>
				<%--<li id="menu5_7_6"><a href="javascript:;" data-url="Exam/Papers_User_Manage.aspx"><%=Resources.L.用户试卷管理 %></a></li>--%>
			</ul>
		</div>
		<div id="menu6" class="m_left_ul">
			<ul class="m_left_ulin open" id="menu6_1"></ul>
			<ul class="m_left_ulin" id="menu6_2">
				<li class="menu_tit"><span class="fa fa-chevron-down"></span><%=Resources.L.管理员管理 %></li>
				<li id="menu6_2_1"><a href="javascript:;" data-url="User/AdminManage.aspx"><%=Resources.L.管理员管理 %></a></li>
				<li id="menu6_2_2"><a href="javascript:;" data-url="User/RoleManage.aspx"><%=Resources.L.管理员角色 %></a></li>
				<li id="menu6_2_3"><a href="javascript:;" data-url="User/SetAdminKey.aspx"><%=Resources.L.手机口令器 %></a></li>
				<li>
					<div class="input-group margintop10">
						<input type="text" id="keyWordss" class="form-control" placeholder="<%=Resources.L.搜索管理员名称 %>" onkeydown="return ASCX.OnEnterSearch('User/AdminManage.aspx?keyWordss=',this);" />
						<span class="input-group-btn">
							<button id="Ok" class="btn btn-default" type="button" onclick="ASCX.Search('User/AdminManage.aspx?keyWordss=','keyWordss');"><span class="fa fa-search"></span></button>
						</span>
					</div>
				</li>
			</ul>
			<ul class="m_left_ulin" id="menu6_3">
				<li class="menu_tit"><span class="fa fa-chevron-down"></span><%=Resources.L.用户角色 %></li>
				<li id="menu6_3_1"><a href="javascript:;" data-url="User/PermissionInfo.aspx"><%=Resources.L.角色管理 %></a></li>
			</ul>
			<ul class="m_left_ulin" id="menu6_4">
				<li class="menu_tit"><span class="fa fa-chevron-down"></span><%=Resources.L.信息发送 %></li>
				<li id="menu6_4_1"><a href="javascript:;" data-url="User/MessageSend.aspx"><%=Resources.L.发送短消息 %></a></li>
				<li id="menu6_4_2"><a href="javascript:;" data-url="User/Message.aspx"><%=Resources.L.短消息列表 %></a></li>
				<li id="menu6_4_3"><a href="javascript:;" data-url="User/MobileMsg.aspx"><%=Resources.L.手机信息 %></a></li>
				<li id="menu6_4_4"><a href="javascript:;" data-url="User/Mail/SendMailList.aspx">订阅管理</a></li>
				<li id="menu6_4_7"><a href="javascript:;" data-url="User/Mail/MailList.aspx"><%=Resources.L.邮件发送 %></a></li>
				<li id="menu6_4_8"><a href="javascript:;" data-url="User/Mail/MailTemplist.aspx">邮件模板</a></li>
				<li id="menu6_4_6"><a href="javascript:;" data-url="User/Mail/MailIdiographList.aspx">邮件签名</a></li>
				<li id="menu6_4_9"><a href="javascript:;" data-url="User/Mail/MailSysTlp.aspx">系统邮件模板</a></li>
			</ul>
			<ul class="m_left_ulin" id="menu6_5">
				<li class="menu_tit"><span class="fa fa-chevron-down"></span><%=Resources.L.订阅中心 %></li>
				<li id="menu6_5_1"><a href="javascript:;" data-url="User/Mail/SubscriptListManage.aspx?menu=all"><%=Resources.L.邮件订阅 %></a></li>
				<li id="menu6_5_4"><a href="javascript:;" data-url="User/Mail/MailSysTlp.aspx"><%=Resources.L.邮件模板 %></a></li>
				<li id="menu6_5_5"><a href="javascript:;" data-url="User/Mail/SendSubMail.aspx"><%=Resources.L.发送邮件 %></a></li>
				<li id="menu6_5_8"><a href="javascript:;" data-url="User/Mail/MailIdiographList.aspx"><%=Resources.L.签名管理 %></a></li>
			</ul>
			<ul class="m_left_ulin" id="menu6_6">
				<li class="menu_tit"><span class="fa fa-chevron-down"></span><%=Resources.L.人才招聘 %></li>
				<li id="menu6_6_1"><a href="javascript:;" data-url="User/Jobsconfig.aspx"><%=Resources.L.人才模板 %></a></li>
				<li id="menu6_6_2"><a href="javascript:;" data-url="User/Jobsinfos.aspx?modeid=qiye"><%=Resources.L.招聘企业 %></a></li>
				<li id="menu6_6_3"><a href="javascript:;" data-url="User/Jobsinfos.aspx?modeid=zhappin"><%=Resources.L.职位信息 %></a></li>
				<li id="menu6_6_4"><a href="javascript:;" data-url="User/Jobsinfos.aspx?modeid=geren"><%=Resources.L.用户简历 %></a></li>
				<li id="menu6_6_5"><a href="javascript:;" data-url="User/JobsRecycler.aspx?modeid=qiye"><%=Resources.L.招聘回收站 %></a></li>
			</ul>
			<ul class="m_left_ulin" id="menu6_7">
				<li class="menu_tit"><span class="fa fa-chevron-down"></span><%=Resources.L.SNS社区模块 %></li>
				<li id="menu6_7_1"><a href="javascript:;" data-url="Zone/ZoneConfig.aspx"><%=Resources.L.空间配置 %></a></li>
				<li id="menu6_7_2"><a href="javascript:;" data-url="Zone/ZoneManage.aspx"><%=Resources.L.空间管理 %></a></li>
				<li id="menu6_7_3"><a href="javascript:;" data-url="Zone/ZoneApplyManage.aspx"><%=Resources.L.审核空间 %></a></li>
				<li id="menu6_7_4"><a href="javascript:;" data-url="Zone/ZoneStyleManage.aspx"><%=Resources.L.模板管理 %></a></li>
				<li id="menu6_7_5"><a href="javascript:;" data-url="Zone/FriendSearchManage.aspx"><%=Resources.L.好友管理%></a></li>
				<li id="menu6_7_6"><a href="javascript:;" data-url="Shop/ProductManage.aspx"><%=Resources.L.虚拟商品%></a></li>
				<li id="menu6_7_7"><a href="javascript:;" data-url="Zone/CarManage.aspx"><%=Resources.L.抢车管理%></a></li>
				<li id="menu6_7_10"><a href="javascript:;" data-url="Zone/UnitConfig.aspx"><%=Resources.L.信息设置%></a></li>
				<%--			<li id="menu6_7_11"><a href="javascript:;" data-url="Zone/SnsSchool.aspx"><%=Resources.L.学校管理%></a></li>
<li id="menu6_7_12"><a href="javascript:;" data-url="Zone/SnsClassRoom.aspx"><%=Resources.L.班级管理%></a></li>--%>
				<li id="menu6_7_8"><a href="javascript:;" data-url="Zone/LargessMoney.aspx"><%=Resources.L.游戏币%></a></li>
				<li id="menu6_7_9"><a href="javascript:;" data-url="Zone/GSManage.aspx"><%=Resources.L.回收站%></a></li>
			</ul>
			<ul class="m_left_ulin" id="menu6_8">
				<li class="menu_tit"><span class="fa fa-chevron-down"></span><%=Resources.L.整合接口%></li>
				<li class="menu6_8_1"><a href="javascript:;" data-url="User/UserApi.aspx"><%=Resources.L.整合配置%></a></li>
				<li class="menu6_8_2"><a href="javascript:;" data-url="User/UApiConfig.aspx?step=2"><%=Resources.L.站点拓扑%></a></li>
				<li class="menu6_8_0"><a href="javascript:;" data-url="User/SiteManage.aspx"><%=Resources.L.子站管理%></a></li>
				<li class="menu6_8_3"><a href="javascript:;" data-url="APP/Suppliers.aspx"><%=Resources.L.社会登录%></a></li>
				<li class="menu6_8_4"><a href="javascript:;" data-url="APP/Ucenter.aspx"><%=Resources.L.跨站接入%>1.0</a></li>
				<li class="menu6_8_5"><a href="javascript:;" data-url="APP/WSApi.aspx"><%=Resources.L.跨站接入%>2.0</a></li>
				<li class="menu6_8_6"><a href="javascript:;" data-url="APP/ConPush.aspx"><%=Resources.L.智农推送%></a></li>
				<li class="menu6_8_7"><a href="javascript:;" data-url="APP/JsonPManage.aspx"><%=Resources.L.移动接口%></a></li>
				<li class="menu6_8_8"><a href="javascript:;" data-url="Config/PlatInfoList.aspx">平台接口</a></li>
			</ul>
		</div>
		<div id="menu7" class="m_left_ul">
			<ul class="m_left_ulin open" id="menu7_1">
				<li class="menu_tit"><span class="fa fa-chevron-down"></span><%=Resources.L.开发中心%></li>
				<li id="menu7_1_1"><a href="javascript:;" data-url="Config/CreateTable.aspx"><%=Resources.L.快速智能建表%></a></li>
				<li id="menu7_1_2"><a href="javascript:;" data-url="Config/ViewList.aspx"><%=Resources.L.主库视图管理%></a></li>
				<li id="menu7_1_3"><a href="javascript:;" data-url="Config/DatalistProfile.aspx"><%=Resources.L.系统全库概况%></a></li>
				<li id="menu7_1_4"><a href="javascript:;" data-url="Config/BackupRestore.aspx"><%=Resources.L.备份还原数据%></a></li>
				<li id="menu7_1_5"><a href="javascript:;" data-url="Config/Optimization.aspx"><%=Resources.L.数据索引优化%></a></li>
				<li id="menu7_1_6"><a href="javascript:;" data-url="Config/DataSearch.aspx"><%=Resources.L.全库数据检索%></a></li>
				<li id="menu7_1_7"><a href="javascript:;" data-url="Config/RunSql.aspx"><%=Resources.L.执行SQL语句%></a></li>
				<li id="menu7_1_8"><a href="javascript:;" data-url="Config/DataAssert.aspx"><%=Resources.L.表内容批处理%></a></li>
				<li id="menu7_1_9"><a href="javascript:;" data-url="Config/EmptyData.aspx"><%=Resources.L.清空测试数据%></a></li>
				<li id="menu7_1_11"><a href="javascript:;" data-url="Content/Schedule/Default.aspx">网站计划任务</a></li>
				<li id="menu7_1_10"><a href="javascript:;" data-url="Common/SystemFinger.aspx"><%=Resources.L.主机信息总览%></a></li>
			</ul>
			<ul class="m_left_ulin" id="menu7_2">
				<li class="menu_tit"><%=Resources.L.广告管理%></li>
				<li id="menu7_2_1"><a href="javascript:;" data-url="Plus/ADZoneManage.aspx"><%=Resources.L.广告版位%></a></li>
				<li id="menu7_2_2"><a href="javascript:;" data-url="Plus/ADManage.aspx"><%=Resources.L.广告内容%></a></li>
				<li id="menu7_2_3"><a href="javascript:;" data-url="Plus/ADAdbuy.aspx"><%=Resources.L.推广申请%></a></li>
				<li id="menu7_2_4"><a href="javascript:;" data-url="Plus/ChartManage.aspx"><%=Resources.L.图表管理%></a></li>
				<li id="menu7_2_9"><a href="javascript:;" data-url="/Plugins/Flex/Default.aspx"><%=Resources.L.在线设计%></a></li>
			</ul>
			<ul class="m_left_ulin" id="menu7_3">
				<li class="menu_tit"><span class="fa fa-chevron-down"></span><%=Resources.L.访问统计%></li>
				<li id="menu7_3_8"><a href="javascript:;" data-url="Counter/StatisticalCode.aspx"><%=Resources.L.统计代码%></a></li>
				<li id="menu7_3_1"><a href="javascript:;" data-url="Counter/Counter.aspx"><%=Resources.L.统计导航%></a></li>
				<li id="menu7_3_2"><a href="javascript:;" data-url="Counter/Ip.aspx"><%=Resources.L.全站统计%></a></li>
				<li id="menu7_3_3"><a href="javascript:;" data-url="Counter/Month.aspx"><%=Resources.L.每日统计%></a></li>
				<li id="menu7_3_4"><a href="javascript:;" data-url="Counter/Year.aspx"><%=Resources.L.每月统计%></a></li>
				<li id="menu7_3_5"><a href="javascript:;" data-url="Counter/Local.aspx"><%=Resources.L.地区数据%></a></li>
				<li id="menu7_3_6"><a href="javascript:;" data-url="Counter/Browser.aspx"><%=Resources.L.浏览信息%></a></li>
				<li id="menu7_3_7"><a href="javascript:;" data-url="Counter/Os.aspx"><%=Resources.L.操作系统%></a></li>
			</ul>
			<ul class="m_left_ulin" id="menu7_4">
				<li class="menu_tit"><span class="fa fa-chevron-down"></span><%=Resources.L.问卷调查%></li>
				<li id="menu7_4_1"><a href="javascript:;" data-url="Plus/SurveyManage.aspx"><%=Resources.L.问卷调查管理%></a></li>
				<li id="menu7_4_2"><a href="javascript:;" data-url="Plus/Survey.aspx"><%=Resources.L.添加问卷投票%></a></li>
			</ul>
			<ul class="m_left_ulin" id="menu7_5">
				<li class="menu_tit"><span class="fa fa-chevron-down"></span><%=Resources.L.文件管理%></li>
				<li id="menu7_5_1"><a href="javascript:;" data-url="File/UploadFile.aspx"><%=Resources.L.本地文件%></a></li>
				<li id="menu7_5_2"><a href="javascript:;" data-url="File/FtpAll.aspx"><%=Resources.L.云端存储%></a></li>
				<li id="menu7_5_3"><a href="javascript:;" data-url="File/DownServerManage.aspx"><%=Resources.L.下载服务器%></a></li>
				<li id="menu7_5_4"><a href="javascript:;" data-url="File/BackupManage.aspx"><%=Resources.L.文件备份%></a></li>
			</ul>
			<ul class="m_left_ulin" id="menu7_6">
				<li class="menu_tit"><span class="fa fa-chevron-down"></span><%=Resources.L.数据字典%></li>
				<li id="menu7_6_1"><a href="javascript:;" data-url="Addon/DictionaryManage.aspx"><%=Resources.L.单级数据字典管理%></a></li>
				<li id="menu7_6_2"><a href="javascript:;" data-url="Addon/GradeCateManage.aspx"><%=Resources.L.多级数据字典管理%></a></li>
				<li id="menu7_6_3"><a href="javascript:;" data-url="Config/CitizenXmlConfig.aspx"><%=Resources.L.国籍数据字典管理%></a></li>
			</ul>
			<ul class="m_left_ulin" id="menu7_7">
				<li class="menu_tit"><span class="fa fa-chevron-down"></span>站内链接</li>
				<li id="menu7_7_1"><a href="javascript:;" data-url="File/Addlinkhttp.aspx"><%=Resources.L.管理链接%></a></li>
				<li id="menu7_7_2"><a href="javascript:;" data-url="File/tjlink.aspx"><%=Resources.L.添加内链%></a></li>
			</ul>
			<ul class="m_left_ulin" id="menu7_8">
				<li class="menu_tit"><span class="fa fa-chevron-down"></span>版权中心</li>
				<%--<li id="menu7_8_7"><a href="javascript:;" data-url="Copyright/Login.aspx">版权登录</li>--%>
				<li id="menu7_8_1"><a href="javascript:;" data-url="Copyright/Config.aspx">版权配置</a></li>
				<li id="menu7_8_2"><a href="javascript:;" data-url="Copyright/AddWorks.aspx">添加作品</a></li>
				<li id="menu7_8_3"><a href="javascript:;" data-url="Copyright/WorksList.aspx">作品列表</a></li>
				<li id="menu7_8_4"><a href="javascript:;" data-url="Copyright/LocalWorks.aspx">作品镜像</a></li>
				<li id="menu7_8_5"><a href="javascript:;" data-url="Copyright/Authorized.aspx">我的授权</a></li>
				<li id="menu7_8_6"><a href="javascript:;" data-url="Copyright/Royalty.aspx">版权收益</a></li>
			</ul>
			<ul class="m_left_ulin" id="menu7_9">
                <li><p class="bg-info margin_b0px"><i class="fa fa-chevron-down"></i>移动与微信</p></li>
				<li>
					<p class="bg-info laybtn"><i class="fa fa-arrow-circle-down"></i><%=Resources.L.微信应用%></p>
					<ul class="list-unstyled">
						<li id="menu7_9_1"><a href="javascript:;" data-url="WeiXin/WxAppManage.aspx"><%=Resources.L.公众号码%></a></li>
					</ul>
				</li>
				<li>
					<ul class="list-unstyled">
						<li id="menu7_9_4"><a href="javascript:;" data-url="WeiXin/QrCodeManage.aspx"><%=Resources.L.二维扫码%></a></li>
						<li id="menu7_9_5"><a href="javascript:;" data-url="WeiXin/QrCodeDecode.aspx"><%=Resources.L.解码管理%></a></li>
						<li id="menu7_9_6"><a href="javascript:;" data-url="AddOn/UAgent.aspx"><%=Resources.L.设备接入%></a></li>
					</ul>
				</li>
			</ul>
			<ul class="m_left_ulin" id="menu7_11">
				<li class="menu_tit"><span class="fa fa-chevron-down"></span><%=Resources.L.其他功能%></li>
				<li id="menu7_11_1"><a href="javascript:;" data-url="Iplook/IPManage.aspx"><%=Resources.L.IP分类管理%></a></li>
				<li id="menu7_11_2"><a href="javascript:;" data-url="Iplook/LookIp.aspx"><%=Resources.L.IP地址管理%></a></li>
				<li id="menu7_11_3"><a href="javascript:;" data-url="Search/DirectoryManage.aspx"><%=Resources.L.定义全文检索%></a></li>
				<li id="menu7_11_4"><a href="javascript:;" data-url="Search/SeachDirectory.aspx"><%=Resources.L.全文目录管理%></a></li>
				<li id="menu7_11_5"><a href="javascript:;" data-url="AddOn/FileseeManage.aspx"><%=Resources.L.比较所有文件%></a></li>
				<li id="menu7_11_6"><a href="javascript:;" data-url="AddOn/FileSynchronize.aspx"><%=Resources.L.文件同步%></a></li>
			</ul>
			<ul class="m_left_ulin" id="menu7_12">
				<li class="menu_tit"><span class="fa fa-chevron-down"></span><%=Resources.L.扩展页面%></li>
				<li id="menu7_12_1"><a href="javascript:;" data-url="Template/Code/PageList.aspx"><%=Resources.L.页面管理%></a></li>
			</ul>
		</div>
		<div id="menu8" class="m_left_ul">
			<ul class="m_left_ulin open" id="menu8_1">
				<li class="menu_tit"><span class="fa fa-chevron-down"></span><%=Resources.L.网站配置%></li>
				<li id="menu8_1_1"><a href="javascript:;" data-url="Config/SiteInfo.aspx"><%=Resources.L.基本信息%></a></li>
				<li id="menu8_1_2"><a href="javascript:;" data-url="Config/SiteOption.aspx"><%=Resources.L.进阶信息%></a></li>
				<li id="menu8_1_10"><a href="javascript:;" data-url="Config/SetOrderStatus.aspx"><%=Resources.L.订单配置%></a></li>
				<li id="menu8_1_3"><a href="javascript:;" data-url="Config/MobileMsgConfig.aspx"><%=Resources.L.短信配置%></a></li>
				<li id="menu8_1_4"><a href="javascript:;" data-url="Config/MailConfig.aspx"><%=Resources.L.邮件参数%></a></li>
				<li id="menu8_1_5"><a href="javascript:;" data-url="Config/ThumbConfig.aspx"><%=Resources.L.水印缩图%></a></li>
				<li id="menu8_1_6"><a href="javascript:;" data-url="Config/IPLockConfig.aspx"><%=Resources.L.访问限定%></a></li>
				<li id="menu8_1_7"><a href="javascript:;" data-url="Config/AppConfig.aspx"><%=Resources.L.维护中心%></a></li>
				<li id="menu8_1_8"><a href="javascript:;" data-url="Config/Sensitivity.aspx"><%=Resources.L.敏感过滤%></a></li>
				<%--<li id="menu8_1_11"><a href="javascript:;" data-url="Config/PlatInfoList.aspx">平台接口</a></li>--%>
				<li id="menu8_1_9"><a href="javascript:;" data-url="Config/CreateMap.aspx"><%=Resources.L.快速索引%></a></li>
			</ul>
			<ul class="m_left_ulin" id="menu8_2">
				<li class="menu_tit"><span class="fa fa-chevron-down"></span><%=Resources.L.模型管理%></li>
				<li id="menu8_2_1"><a href="javascript:;" data-url="Content/ModelManage.aspx?ModelType=1"><%=Resources.L.内容模型%></a></li>
				<li id="menu8_2_2"><a href="javascript:;" data-url="Content/ModelManage.aspx?ModelType=2"><%=Resources.L.商城模型%></a></li>
				<li id="menu8_2_3"><a href="javascript:;" data-url="Content/ModelManage.aspx?ModelType=5"><%=Resources.L.店铺模型%></a></li>
				<li id="menu8_2_4"><a href="javascript:;" data-url="Content/ModelManage.aspx?ModelType=3"><%=Resources.L.用户模型%></a></li>
				<li id="menu8_2_5"><a href="javascript:;" data-url="Content/ModelManage.aspx?ModelType=4"><%=Resources.L.黄页模型%></a></li>
			</ul>
			<ul class="m_left_ulin" id="menu8_3">
				<li class="menu_tit"><span class="fa fa-chevron-down"></span><%=Resources.L.节点管理%></li>
				<li id="menu8_3_1"><a href="javascript:;" data-url="Content/NodeManage.aspx"><%=Resources.L.节点管理%></a></li>
				<li id="menu8_3_2"><a href="javascript:;" data-url="Content/BatchNode.aspx"><%=Resources.L.批量设置%></a></li>
				<li id="menu8_3_3"><a href="javascript:;" data-url="Content/UnionNode.aspx"><%=Resources.L.合并迁移%></a></li>
				<li id="menu8_3_4"><a href="javascript:;" data-url="Config/RouteConfig.aspx"><%=Resources.L.站点路由%></a></li>
				<li id="menu8_3_6"><a href="javascript:;" data-url="Content/Addon/SToS.aspx"><%=Resources.L.站站迁移%></a></li>
				<li id="menu8_3_7"><a href="javascript:;" data-url="Content/DesignNodeManage.aspx">动力节点</a></li>
				<li id="menu8_3_5" class="input-group">
					<div class="input-group margintop10">
						<input type="text" id="NodeSkey_t" class="form-control" placeholder="节点ID或名称" onkeydown="return ASCX.OnEnterSearch('Content/NodeSearch.aspx?NodeID=',this);" />
						<span class="input-group-btn">
							<button id="NodeBtn" class="btn btn-default" type="button" onclick="ASCX.Search('Content/NodeSearch.aspx?NodeID=','NodeSkey_t');"><span class="fa fa-search"></span></button>
						</span>
					</div>
				</li>
			</ul>
			<ul class="m_left_ulin" id="menu8_4">
				<li class="menu_tit"><span class="fa fa-chevron-down"></span><%=Resources.L.流程管理%></li>
				<li id="menu8_4_1"><a href="javascript:;" data-url="Content/Flow/FlowManager.aspx"><%=Resources.L.流程管理%></a></li>
				<li id="menu8_4_2"><a href="javascript:;" data-url="Content/Flow/AuditingState.aspx"><%=Resources.L.状态编码%></a></li>
			</ul>
			<ul class="m_left_ulin" id="menu8_5">
				<li class="menu_tit"><span class="fa fa-chevron-down"></span><%=Resources.L.专题管理%></li>
				<li id="menu8_5_1"><a href="javascript:;" data-url="Content/SpecialManage.aspx"><%=Resources.L.专题列表%></a></li>
				<li id="menu8_5_2"><a href="javascript:;" data-url="Content/MoveSpec.aspx">专题迁移</a></li>
				<li id="menu8_5_3" class="input-group">
					<div class="input-group margintop10">
						<input type="text" id="SpecKey_t" class="form-control" placeholder="专题ID或名称" onkeydown="return ASCX.OnEnterSearch('Content/SpecialManage.aspx?skey=',this);" />
						<span class="input-group-btn">
							<button id="Spec_btn" class="btn btn-default" type="button" onclick="ASCX.Search('Content/SpecialManage.aspx?skey=','SpecKey_t');"><span class="fa fa-search"></span></button>
						</span>
					</div>
				</li>
			</ul>
			<ul class="m_left_ulin" id="menu8_6">
				<li class="menu_tit"><span class="fa fa-chevron-down"></span><%=Resources.L.模板风格%></li>
				<li id="menu8_6_1"><a href="javascript:;" data-url="Template/TemplateSet.aspx"><%=Resources.L.方案列表%></a></li>
				<li id="menu8_6_2"><a href="javascript:;" data-url="Template/TemplateManage.aspx"><%=Resources.L.模板中心%></a></li>
				<li id="menu8_6_3"><a href="javascript:;" data-url="Template/CSSManage.aspx"><%=Resources.L.风格管理%></a></li>
				<li id="menu8_6_4"><a href="javascript:;" data-url="Template/TemplateSetOfficial.aspx"><%=Resources.L.云端下载%></a></li>
				<li id="menu8_6_5"><a href="javascript:;" data-url="Template/ExternDS/DSList.aspx"><%=Resources.L.外部数据库源%></a></li>
			</ul>
			<ul class="m_left_ulin" id="menu8_7">
			</ul>
			<ul class="m_left_ulin" id="menu8_8">
				<li class="menu_tit"><span class="fa fa-chevron-down"></span><%=Resources.L.在线支付%></li>
				<li id="menu8_8_1"><a href="javascript:;" data-url="Pay/PayPlatManage.aspx"><%=Resources.L.支付平台管理%></a></li>
				<li id="menu8_8_2"><a href="javascript:;" data-url="Pay/AlipayBank.aspx"><%=Resources.L.支付宝单网银%></a></li>
				<li id="menu8_8_5"><a href="javascript:;" data-url="Pay/Paypalmanage.aspx"><%=Resources.L.paypal国际付%></a></li>
				<li id="menu8_8_4"><a href="javascript:;" data-url="Pay/PaymentList.aspx"><%=Resources.L.充值信息管理%></a></li>
				<li id="menu8_8_6"><a href="javascript:;" data-url="WeiXin/PayWeiXin.aspx">PC端微信支付</a></li>
				<li id="menu8_8_7"><a href="javascript:;" data-url="WeiXin/PayWeiXinAPP.aspx">APP微信支付</a></li>
				<li id="menu8_8_8"><a href="javascript:;" data-url="User/Addon/RegularList.aspx">充值赠送</a></li>
			</ul>
		</div>
		<div id="menu9" class="m_left_ul">
			<ul class="m_left_ulin open" id="menu9_1">
				<li class="menu_tit"><span class="fa fa-chevron-down"></span><%=Resources.L.OA配置%></li>
				<li id="menu9_1_1"><a href="javascript:;" data-url="Content/ModelManage.aspx?ModelType=12"><%=Resources.L.办公模型%></a></li>
				<li id="menu9_1_2"><a href="javascript:;" data-url="WorkFlow/Default.aspx"><%=Resources.L.流程管理%></a></li>
				<li id="menu9_1_3"><a href="javascript:;" data-url="WorkFlow/FlowTypeList.aspx"><%=Resources.L.类型管理%></a></li>
				<li id="menu9_1_4"><a href="javascript:;" data-url="WorkFlow/MisModelManage.aspx"><%=Resources.L.套红管理%></a></li>
				<li id="menu9_1_5"><a href="javascript:;" data-url="WorkFlow/SignManage.aspx"><%=Resources.L.签章管理%></a></li>
				<li id="menu9_1_7"><a href="javascript:;" data-url="WorkFlow/OAConfig.aspx"><%=Resources.L.系统配置%></a></li>
				<%--		<li id="menu9_1_8"><a href="javascript:;" data-url="WorkFlow/AffairManage.aspx"><%=Resources.L.公文管理%></a></li>--%>
				<li id="menu9_1_10"><a href="javascript:;" data-url="WorkFlow/MailManage.aspx"><%=Resources.L.邮箱管理%></a></li>
			</ul>
			<ul class="m_left_ulin" id="menu9_2">
				<%--<li class="menu_tit"><span class="fa fa-chevron-down"></span>组织结构</li> 
<li id="menu9_2_1"><a href="javascript:;" data-url="AddOn/StructManage.aspx?type=0"><%=Resources.L.组织结构%></a></li>
<li id="menu9_2_2"><a href="javascript:;" data-url="AddOn/StructList.aspx?type=0"><%=Resources.L.配置结构%></a></li>
<li id="menu9_2_3"><a href="javascript:;" data-url="AddOn/StructList.aspx?type=1"><%=Resources.L.管理结构%></a></li>
<li id="menu9_2_4"><a href="javascript:;" data-url="AddOn/StructAnalysis.aspx"><%=Resources.L.分析结构%></a></li>--%>
			</ul>
			<ul class="m_left_ulin" id="menu9_3">
				<li class="menu_tit"><span class="fa fa-chevron-down"></span><%=Resources.L.项目管理%></li>
				<li id="menu9_3_4"><a href="javascript:;" data-url="AddOn/Project/ProjectsType.aspx"><%=Resources.L.项目配置%></a></li>
				<li id="menu9_3_1"><a href="javascript:;" data-url="AddOn/Project/ManageProjects.aspx"><%=Resources.L.项目管理%></a></li>
				<li id="menu9_3_2"><a href="javascript:;" data-url="AddOn/Project/ProjectRequireList.aspx"><%=Resources.L.需求管理%></a></li>
				<li id="menu9_3_3"><a href="javascript:;" data-url="AddOn/Project/WorkManage.aspx"><%=Resources.L.流程管理%></a></li>
				<li id="menu9_3_5"><a href="javascript:;" data-url="AddOn/Project/ProjectsModelField.aspx"><%=Resources.L.项目模型%></a></li>
			</ul>
			<ul class="m_left_ulin" id="menu9_4" style="height: 700px; overflow: auto;">
				<li class="menu_tit"><span class="fa fa-chevron-down"></span><%=Resources.L.CRM管理%></li>
				<li id="menu9_4_1"><a href="javascript:;" data-url="AddCRM/CustomerList.aspx?usertype=0&modelid=48">客户管理</a></li>
				<li id="menu9_4_4"><a href="javascript:;" data-url="Content/ModelManage.aspx?ModelType=11">模型管理</a></li>
			</ul>
			<ul class="m_left_ulin" id="menu9_5">
				<li class="menu_tit"><span class="fa fa-chevron-down"></span><%=Resources.L.有问必答%></li>
				<li id="menu9_5_1"><a href="javascript:;" data-url="iServer/BiServer.aspx?num=-3"><%=Resources.L.有问必答%></a></li>
				<li id="menu9_5_2"><a href="javascript:;" data-url="iServer/BselectiServer.aspx?num=-3"><%=Resources.L.问题列表%></a></li>
				<li id="menu9_5_3"><a href="javascript:;" data-url="iServer/AddQuestionRecord.aspx"><%=Resources.L.创建问题记录%></a></li>
			</ul>
			<ul class="m_left_ulin" id="menu9_6">
				<li class="menu_tit"><span class="fa fa-chevron-down"></span><%=Resources.L.客服通%></li>
				<li><a href="javascript:;" data-url="User/Service/ServiceSeat.aspx"><%=Resources.L.客服席位%></a></li>
				<li><a href="javascript:;" data-url="User/Service/CodeList.aspx"><%=Resources.L.引用管理%></a></li>
				<li><a href="javascript:;" data-url="User/Service/MsgEx.aspx"><%=Resources.L.聊天记录%></a></li>
				<li><a href="javascript:;" data-url="User/Addon/UAction.aspx"><%=Resources.L.行为跟踪%></a></li>
				<%--	<li><a href="javascript:;" data-url="User/UserGroup.aspx"><%=Resources.L.用户组管理%></a></li>--%>
			</ul>
			<ul class="m_left_ulin" id="menu9_7">
				<li class="menu_tit"><span class="fa fa-chevron-down"></span><%=Resources.L.工作统计%></li>
				<li id="menu9_7_3"><a href="javascript:;" data-url="Workload/WorkCount.aspx"><%=Resources.L.按时间统计%></a></li>
				<li id="menu9_7_1"><a href="javascript:;" data-url="Workload/ContentRank.aspx"><%=Resources.L.排行榜%></a></li>
				<li id="menu9_7_2"><a href="javascript:;" data-url="Workload/Subject.aspx?Type=manager"><%=Resources.L.按管理员统计%></a></li>
			</ul>
			<ul class="m_left_ulin" id="menu9_8">
				<li class="menu_tit"><span class="fa fa-chevron-down"></span><%=Resources.L.日志管理%></li>
				<li id="menu9_8_1"><a href="javascript:;" data-url="Plus/LogManage.aspx?LogType=4"><%=Resources.L.管理登录%></a></li>
				<li id="menu9_8_2"><a href="javascript:;" data-url="Plus/LogManage.aspx?LogType=0"><%=Resources.L.内容管理%></a></li>
				<li id="menu9_8_3"><a href="javascript:;" data-url="Plus/TxtLog.aspx?LogType=safe"><%=Resources.L.安全日志%></a></li>
				<li id="menu9_8_4"><a href="javascript:;" data-url="Plus/TxtLog.aspx?LogType=fileup"><%=Resources.L.上传日志%></a></li>
				<li id="menu9_8_5"><a href="javascript:;" data-url="Plus/TxtLog.aspx?LogType=exception"><%=Resources.L.异常日志%></a></li>
				<li id="menu9_8_6"><a href="javascript:;" data-url="Plus/TxtLog.aspx?LogType=labelex"><%=Resources.L.标签异常%></a></li>
				<%--        <li id="menu9_8_6"><a href="javascript:;" data-url="Plus/TxtLog.aspx?LogType=4"><%=Resources.L.支付日志%></a></li>--%>
				<%--       <li id="menu9_8_6"><a href="javascript:;" data-url="Plus/AuditNotes.aspx"><%=Resources.L.审核记录%></a></li>--%>
			</ul>
			<ul class="m_left_ulin" id="menu9_9">
				<li class="menu_tit"><span class="fa fa-chevron-down"></span><%=Resources.L.能力中心%></li>
				<li id="menu9_9_1"><a href="javascript:;" data-url="Plat/PlatInfoManage.aspx"><%=Resources.L.信息管理%></a></li>
				<li id="menu9_9_3"><a href="javascript:;" data-url="Plat/CompList.aspx"><%=Resources.L.公司管理%></a></li>
				<li id="menu9_9_4"><a href="javascript:;" data-url="Plat/TopicList.aspx">话题管理</a></li>
				<li id="menu9_9_7"><a href="javascript:;" data-url="Plat/AuditApply.aspx">申请管理</a></li>
				<li id="menu9_9_5"><a href="javascript:;" data-url="Plat/AuditComp.aspx">公司认证</a></li>
				<li id="menu9_9_6"><a href="javascript:;" data-url="Plat/CreateComp.aspx">创建企业</a></li>
			</ul>
			<ul class="m_left_ulin" id="menu9_11">
				<li class="menu_tit"><span class="fa fa-chevron-down"></span><%=Resources.L.智慧图表%></li>
				<li id="menu9_11_1"><a href="javascript:;" data-url="Content/ECharts/AddChart.aspx"><%=Resources.L.创建图表%></a></li>
				<li id="menu9_11_2"><a href="javascript:;" data-url="Content/ECharts/Default.aspx"><%=Resources.L.图表列表%></a></li>
			</ul>
		</div>
		<div id="menu10" class="m_left_ul">
			<ul class="m_left_ulin open" id="menu10_1">
				<li class="menu_tit"><span class="fa fa-chevron-down"></span><%=Resources.L.移动应用%></li>
				<li id="menu10_1_2"><a href="javascript:;" data-url="/APP/Default.aspx"><%=Resources.L.网站APP%></a></li>
				<li id="menu10_1_6"><a href="javascript:;" data-url="/APP/Default.aspx?APKMode=1"><%=Resources.L.云打包APP%></a></li>
				<li id="menu10_1_1"><a href="javascript:;" data-url="WeiXin/home.aspx"><%=Resources.L.应用列表%></a></li>
				<%--<li id="menu10_1_3"><a href="http://app.z01.com/" target="_blank"><%=Resources.L.移动模拟器%></a></li>--%>
				<li id="menu10_1_4"><a href="javascript:;" data-url="/APP/CLList.aspx">APP颁发</a></li>
				<li id="menu10_1_8"><a href="javascript:;" data-url="App/AppTlpOnline.aspx"><%=Resources.L.我的模板%></a></li>
				<li id="menu10_1_9"><a href="javascript:;" data-url="/APP/APPList.aspx"><%=Resources.L.我的APP%></a></li>
			</ul>
			<ul class="m_left_ulin" id="menu10_2">
				<li class="menu_tit"><span class="fa fa-chevron-down"></span><%=Resources.L.微信配置%></li>
				<%--            <li id="menu10_2_1"><a href="javascript:;" data-url="WeiXin/WxConfig.aspx"><%=Resources.L.微信配置%></a></li>--%>
				<li id="menu10_2_2"><a href="javascript:;" data-url="WeiXin/WxAppManage.aspx"><%=Resources.L.公众号码%></a></li>
				<li id="menu10_2_7"><a href="javascript:;" data-url="WeiXin/QrCodeManage.aspx"><%=Resources.L.二维扫码%></a></li>
				<li id="menu10_2_8"><a href="javascript:;" data-url="WeiXin/QrCodeDecode.aspx"><%=Resources.L.解码管理%></a></li>
				<li id="menu10_2_9"><a href="javascript:;" data-url="AddOn/UAgent.aspx"><%=Resources.L.设备接入%></a></li>
			</ul>
			<ul class="m_left_ulin" id="menu10_3">
				<li class="menu_tit"><span class="fa fa-chevron-down"></span>智能硬件</li>
				<li id="menu10_3_1"><a href="javascript:;" data-url="Shop/Printer/ListDevice.aspx">设备列表</a></li>
				<li id="menu10_3_2"><a href="javascript:;" data-url="Shop/Printer/MessageList.aspx">打印流水</a></li>
				<li id="menu10_3_3"><a href="javascript:;" data-url="Shop/Printer/QuickPrint.aspx">模板管理</a></li>
				<li id="menu10_3_5"><a href="javascript:;" data-url="Shop/Printer/OrderPrint.aspx">订单输出</a></li>
				<li id="menu10_3_4"><a href="javascript:;" data-url="Shop/Printer/TestPrint.aspx">模拟打印</a></li>
				<li id="menu10_3_6" class="input-group">
					<div class="input-group margintop10">
						<input type="text" id="Printer_Skey_T" class="form-control" placeholder="关键词或订单号" onkeydown="return ASCX.OnEnterSearch('Shop/Printer/MessageList.aspx?skey=',this);" />
						<span class="input-group-btn">
							<button class="btn btn-default" type="button" onclick="ASCX.Search('Shop/Printer/MessageList.aspx?skey=','Printer_Skey_T');"><span class="fa fa-search"></span></button>
						</span>
					</div>
				</li>
			</ul>
			<ul class="m_left_ulin" id="menu10_4">
				<li class="menu_tit"><span class="fa fa-chevron-down"></span>消息推送</li>
				<li id="menu10_4_1"><a href="javascript:;" data-url="Mobile/Push/APIList.aspx">API列表</a></li>
				<li id="menu10_4_2"><a href="javascript:;" data-url="Mobile/Push/PushMsg.aspx">消息推送</a></li>
				<li id="menu10_4_3"><a href="javascript:;" data-url="Mobile/Push/Default.aspx">历史推送</a></li>
			</ul>
			<ul class="m_left_ulin" id="menu10_6">
				<li class="menu_tit"><span class="fa fa-chevron-down"></span>H5场景</li>
				<li>
					<div class="input-group ">
						<input type="text" placeholder="场景名称或用户名" class="form-control" id="scence_text" />
						<span class="input-group-btn">
							<button class="btn btn-default" type="button" onclick="ASCX.Search('design/SceneList.aspx?KeyWord=','scence_text');" ><span class="fa fa-search"></span></button>
						</span>
					</div>
				</li>
				<li id="menu11_11_2"><a href="javascript:;" data-url="design/SceneList.aspx">场景列表</a></li>
				<li id="menu11_11_1"><a href="javascript:;" data-url="design/TlpList.aspx?type=1">场景模板</a></li>
                <li id="menu11_11_5"><a href="javascript:;" data-url="design/AlbumList.aspx">智能相册</a></li>
				<li id="menu11_11_4"><a href="javascript:;" data-url="design/PubList.aspx">互动信息</a></li>
				<li id="menu11_11_3"><a href="javascript:;" data-url="design/Addon/VisitCount.aspx">访问统计</a></li>
				<li>
					<p class="bg-info laybtn"><i class="fa fa-arrow-circle-down"></i>动力问卷</p>
					<ul class="list-unstyled">
						<li id="menu11_11_6"><a href="javascript:;" data-url="design/ask/asklist.aspx">问卷列表</a></li>
						<li id="menu11_11_7"><a href="javascript:;" data-url="design/ask/anslist.aspx">回答列表</a></li>
					</ul>
				</li>
			</ul>
		</div>
		<div id="menu11" class="m_left_ul">
			<ul class="m_left_ulin" id="menu11_1">
				<li class="menu_tit"><span class="fa fa-chevron-down"></span><%=Resources.L.站群管理%></li>
				<li id="menu11_1_1"><a href="javascript:;" data-url="Site/SiteConfig.aspx"><%=Resources.L.全局配置%></a></li>
				<li id="menu11_1_2"><a href="javascript:;" data-url="Site/ServerClusterConfig.aspx"><%=Resources.L.服务器集群%></a></li>
				<li id="menu11_1_3"><a href="javascript:;" data-url="Site/DBManage.aspx"><%=Resources.L.数据库管理%></a></li>
				<li id="menu11_1_4"><a href="javascript:;" data-url="Site/Default.aspx"><%=Resources.L.站点列表%></a></li>
				<li id="menu11_1_5"><a href="javascript:;" data-url="Site/SitePool.aspx"><%=Resources.L.应用程序池%></a></li>
				<li id="menu11_1_6"><a href="javascript:;" data-url="Template/CloudLead.aspx"><%=Resources.L.模板云台%></a></li>
				<li id="menu11_1_7"><a href="javascript:;" data-url="Site/SiteCloudSetup.aspx"><%=Resources.L.快云安装%></a></li>
				<li id="menu11_1_8"><a href="javascript:;" data-url="Site/SiteDataCenter.aspx"><%=Resources.L.智能采集%></a></li>
			</ul>
			<ul class="m_left_ulin" id="menu11_2">
				<li class="menu_tit"><span class="fa fa-chevron-down"></span><%=Resources.L.域名注册%></li>
				<li id="menu11_2_1"><a href="javascript:;" data-url="Site/DomPrice.aspx"><%=Resources.L.域名定价%></a></li>
				<li id="menu11_2_2"><a href="javascript:;" data-url="Site/DomManage.aspx"><%=Resources.L.域名管理%></a></li>
				<li id="menu11_2_3"><a href="javascript:;" data-url="Site/DNSManage.aspx">DNS<%=Resources.L.管理%></a></li>
				<%--<li id="menu11_2_4"><a href="javascript:;" data-url="Site/SiteDataCenter.aspx"><%=Resources.L.智能采集%></a></li>--%>
				<%--<li id="menu11_2_5"><a href="javascript:;" data-url="Site/ProductManage.aspx"><%=Resources.L.域名定价%></a></li>--%>
				<li id="menu11_2_6"><a href="javascript:;" data-url="Site/SiteConfig.aspx?remote=true"><%=Resources.L.全局配置%></a></li>
			</ul>
			<ul class="m_left_ulin" id="menu11_10">
				<li class="menu_tit"><span class="fa fa-chevron-down"></span>动力设计</li>
				<li>
					<div class="input-group ">
						<input type="text" placeholder="站点名称或用户名" class="form-control" id="site_text" />
						<span class="input-group-btn">
							<button class="btn btn-default" type="button" onclick="ASCX.Search('design/SiteList.aspx?KeyWord=','site_text');"><span class="fa fa-search"></span></button>
						</span>
					</div>
				</li>
				<li id="menu11_10_1"><a href="javascript:;" data-url="design/SiteList.aspx">站点列表</a></li>
				<li id="menu11_10_2"><a href="javascript:;" data-url="design/TlpList.aspx?type=0">动力模板</a></li>
				<li id="menu11_10_3"><a href="javascript:;" data-url="design/TlpClass.aspx">模板类别</a></li>
				<li id="menu11_10_4"><a href="javascript:;" data-url="design/ResList.aspx">资源管理</a></li>
				<li id="menu11_10_5"><a href="javascript:;" data-url="design/Config.aspx">动力配置</a></li>
				<li id="menu11_10-6"><a href="javascript:;" data-url="design/MBSiteList.aspx">微建站</a></li>
			</ul>
		</div>
	</div>
	<asp:UpdatePanel ID="LeftPanel" runat="server" EnableViewState="false">
		<ContentTemplate>
			<div runat="server" id="left_Div" style="overflow-y: auto; position: relative;"></div>
			<asp:Button runat="server" ID="leftSwitch_Btn" OnClick="leftSwitch_Btn_Click" Style="display: none;" />
			<asp:HiddenField runat="server" ID="left_Hid" />
		</ContentTemplate>
	</asp:UpdatePanel>
	<div class="clearfix"></div>
</div>
<div id="leftSwitch" style="cursor: pointer; position: absolute; margin-top: 12%; z-index: 10; display: none;" class="hidden-xs hidden-sm" onclick="hideleft();">
	<img src="/App_Themes/Admin/butClose.gif" />
</div>
<div class="col-lg-10 col-md-10 col-sm-10 col-xs-12 main_right pull-right" id="right" style="overflow: hidden;">
	<div class="m_tabs">
		<table style="height: 100%; width: 100%;">
			<tr style="vertical-align: top">
				<td id="frmRtd" style="text-align: left;">
					<div class="FrameTabs_bg">
						<div class="FrameTab_nav">
							<div id="FrameTabs" style="overflow: hidden; width: 100%;">
								<div class="tab-right"></div>
								<div class="tab-left"></div>
								<div class="tab-strip-wrap">
									<ul class="tab-strip pull-left" id="tab_ul" style="position: fixed; background: #FFF;">
										<li class="current first" id="iFrameTab1"><a href="javascript:"><span id="frameTabTitle"><%=Resources.L.新选项卡%></span></a><a class="closeTab" title="关闭"><span class="fa fa-remove"></span></a></li>
										<li class="end" id="newFrameTab"><a title="<%=Resources.L.新选项卡%>" href="javascript:"><span class="fa fa-plus"></span></a></li>
									</ul>
									<div class="clearfix"></div>
								</div>
							</div>
						</div>
					</div>
					<!-- 书签结束 -->
					<div class="thumbnail">
						<div id="main_right_frame">
							<iframe id="main_right" onload="SetTabTitle(this)" style="z-index: 2; background: #fff; visibility: inherit; overflow: auto; overflow-x: hidden; width: 100%;" name="main_right" src="Profile/Worktable.aspx" frameborder="0" tabid="1"></iframe>
							<div class="clearfix"></div>
						</div>
					</div>
				</td>
			</tr>
		</table>
	</div>
	<div id="Meno" style="position: inherit; width: 100%;">
		<div id="iframeGuideTemplate" style="display: none;">
			<iframe style="z-index: 2; visibility: inherit; width: 100%;" src="about:blank" frameborder="0" tabid="0"></iframe>
			<div class="clearfix"></div>
		</div>
		<div id="iframeMainTemplate" style="display: none">
			<iframe style="z-index: 2; visibility: inherit; overflow-x: hidden; width: 100%;" src="about:blank" frameborder="0" scrolling="yes" onload="SetTabTitle(this)" tabid="0"></iframe>
			<div class="clearfix"></div>
		</div>
		<div class="clearfix"></div>
	</div>
	<div class="clearfix"></div>
</div>
<div class="clearfix"></div>
</div>
<div style="position: absolute; left: 0px; top: 0px; z-index: 10001; text-align: center;" id="infoDiv"></div>
<!--锁屏-->
<div style="position: absolute; display: none; left: 0px; top: 0px; height: 100%; z-index: 10000; background: #1c6297;" id="tranDiv" class="insbox">
<div style="position: absolute; left: 0px; top: 0px; width: 100%; height: 100%; filter: alpha(Opacity=20)" id="tranDivBack"></div>
</div>
<%=CreateSiteColorCSS() %>
<script src="/dist/js/bootstrap.min.js"></script>
<script src="/JS/jquery.zclip.min.js"></script>
<script src="/JS/Plugs/transtool.js"></script>
<script src="/JS/ICMS/ascx.js?ver=20150602"></script>
<script src="/JS/ICMS/alt.js"></script>
<script src="/JS/ICMS/Main.js?ver=20160302"></script>
<script src="/JS/Controls/ZL_Dialog.js"></script>
<script>
var Path = '<%=CustomerPageAction.customPath2%>';
var adDiag = new ZL_Dialog();
$(function () {
$("li a[data-url]").click(function () {
ShowMain('', $(this).attr("data-url"), this);
});
$("#tab_ul").sortable({ containment: 'parent' });
$(".laybtn").bind("click", BindLayerUL());
//锁屏
if (getCookie("SetLock") == "1") {
showWindow('Lockin.aspx', 900, 460);
}
//小屏下菜单显示 
$(".mb_nav .navbar-nav li a").click(function (e) {
var snav = $(this).attr("data-label");
$(".mb_nav .navbar-nav a").removeClass("active");
$(this).addClass("active");
$(".sub_nav").slideDown();
$(".sub_nav").html("");
$(".sub_nav").append($("ul." + snav).html());
$(".sub_nav li a").removeAttr("onclick");
$(".sub_nav li a").click(function (e) {
	showThiNav(this);
})
});
$('.popover-dismiss').popover({
trigger: 'toggle'
});
//顶部按钮
$(".m_top_menu").find("ul li").click(function () {
$(this).siblings("li").removeClass("active");
$(this).addClass("active");
});
$(".m_top_bottom").find("ul li").click(function () {
$(this).siblings("li").removeClass("active");
$(this).addClass("active");
})
});
window.onresize = setLayout;
document.body.scroll = "no";
function setLayout() {
document.getElementById("main_right_frame").style.width = document.documentElement.clientWidth;
document.getElementById("main_right").height = document.documentElement.clientHeight - 140;
//----边栏切换按钮
if ($(window).width() < 768) {
$("#leftSwitch").hide();
}
else {
LeftSwitchFunc();
}
//----边栏滚动条高度
$("#left_Div").css("height", $(window).height() - 150 + "px");
}
setLayout();
function IsEnter(obj) {
if (event.keyCode == 13) {
SearchPage(); return false;
}
}
//搜索
function SearchPage() {
var key = $("#keyText").val();
if (!key || key == "") return;
ShowMain('', Path + 'Main.aspx?key=' + key);
}
function DivCache(url) {
myFrame.AddTabJson(currentFrameTabId, url, $("#left_Div").html());
}
function ShowAD() {
adDiag.title = "选择工作环境";
adDiag.url = "Scence.aspx";
adDiag.maxbtn = false;
adDiag.height = 675;
adDiag.ShowModal();
}
function ShowStartScreen() {
$('#newbody').show();
setTimeout(function () { $('#newbody').hide(); }, 20 * 1000);
}
function PrePageInit() {
var ver = Math.random();
var pages = "Content/ContentManage,Content/AddContent,Content/EditContent,Content/NodeManage,";
pages += "Shop/ProductManage,Shop/AddProduct,Template/LabelManage,";
pages += "User/UserManage,";
pages += "Config/SiteOption";
var pageArr = pages.split(',');
for (var i = 0; i < pageArr.length; i++) {
$.post(Path + pageArr[i] + ".aspx?v=" + ver);
}
$.post("/User/Default.aspx?v=" + ver);
}
//关闭云模板提示
function cloud_close() {
$(".toCloud").fadeOut();
}
function showThiNav(obj) {
$(".sub_nav a").removeClass("active");
$(obj).addClass("active");
var tnav = $(obj).parent().attr("class");
$(".thi_nav").slideDown();
$(".thi_nav").html("");
$(".thi_nav").append($("ul#" + tnav).html());
$(".thi_nav li a").click(function (e) {
showA(this);
})
}
function showA(obj) {
$(".thi_nav a").removeClass("active");
$(obj).addClass("active");
$(".mb_nav button").click();
$("#main_right").attr("src", $(obj).attr("data-url"));
}
$(function (e) {
drawCanvasOne();
$(".newbody_c").animate(
{
	"opacity": "0"
}, 3000, function () {
	$(".newbody_c").remove();
	$(".newbody_c1t").show().addClass("animated").css("-webkit-animation-name", "fadeIn").css("animation-name", "fadeIn").css("-webkit-animation-duration", "3s").css("animation-duration", "3s").css("-webkit-animation-fill-mode", "both").css("animation-fill-mode", "both");
	$(".newbody_clm").show().addClass("animated").css("-webkit-animation-name", "fadeIn").css("animation-name", "fadeIn").css("-webkit-animation-duration", "3s").css("animation-duration", "3s").css("-webkit-animation-fill-mode", "both").css("animation-fill-mode", "both");;
});
});
function drawCanvasOne() {
var t = document.getElementById("wave"),
e = echarts.init(t),
i = e.getZr(),
a = e.getWidth(),
c = e.getHeight(),
o = new echarts.graphic.Group;
i.add(o);
var n = 3;
800 > a && (n = 2);
for (var r = 0; 3 > r; r++) {
for (var s = [], l = 0; n + 1 >= l; l++) {
	var h = c / 10 * r + c / 6,
		d = Math.random() * c / 8 + h,
		g = c - Math.random() * c / 8 - h,
		m = [
			[2 * l * a / n / 2, r % 2 ? d : g],
			[(2 * l + 1) * a / n / 2, r % 2 ? g : d]
		];
	s.push(m[0], m[1])
}
var u = new echarts.graphic.Polyline({
	shape: {
		points: s,
		smooth: .4
	},
	style: {
		stroke: "#fff",
		opacity: 1 / (r + 1),
		lineWidth: 1.2 / (r + 1) + .8
	},
	silent: !0,
	position: [-r * a / 8, 35 * -(r - .5)]
}),
	y = new echarts.graphic.Rect({
		shape: {
			x: 0,
			y: 0,
			width: 0,
			height: c
		},
		position: [r * a / 8, 0]
	});
o.add(u), y.animateTo({
	shape: {
		width: a
	}
}, 5e3, 800 * Math.random()), u.setClipPath(y), n += 1
}
$(window).on("resize", function () {
var t = e.getWidth(),
	i = e.getHeight();
e.resize();
var a = e.getWidth(),
	c = e.getHeight();
y.setShape({
	width: a,
	height: c
});
var n = a / t,
	r = c / i;
o.eachChild(function (t) {
	t.position[0] *= n, t.position[1] *= r, t.shape.points.forEach(function (t) {
		t[0] *= n, t[1] *= r
	}), t.dirty(!0)
})
})
}
</script>
</form>
</body>
</html>
