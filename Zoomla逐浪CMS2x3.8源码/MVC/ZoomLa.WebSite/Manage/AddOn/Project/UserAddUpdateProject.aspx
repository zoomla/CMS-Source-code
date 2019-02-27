<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Manage/I/Default.master" CodeBehind="UserAddUpdateProject.aspx.cs" Inherits="ZoomLaCMS.Manage.AddOn.Project.UserAddUpdateProject" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>查看或修改项目资料</title>
<script src="/JS/calendar.js"></script>
<style type="text/css">
.style1{ background: #e0f7e5;  padding: 2px;width: 262px;height: 49px;}
.style2{height: 49px;}
.style3{background: #e0f7e5;   padding: 2px;width: 262px;}
</style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<table class="table table-striped table-bordered table-hover">
	<tbody id="Tabs0">
		<tr>
			<td class="text-center" colspan="2" ><asp:Label ID="lblText" runat="server">修改项目</asp:Label></td>
		</tr>
		<tr>
			<td class="style3"><strong>名称：</strong><br />项目名称</td>
			<td>
				<asp:TextBox ID="TxtProName" class="form-control" runat="server" Width="222px" />
				<asp:RequiredFieldValidator ID="rfv" runat="server" ControlToValidate="TxtProName" Display="Dynamic" ErrorMessage="项目名称不可为空">项目名称不可为空</asp:RequiredFieldValidator>
			</td>
		</tr>
		<tr>
			<td class="style3"><strong>项目类型：</strong><br />请选择项目类型</td>
			<td><asp:DropDownList ID="DDListType" runat="server" Width="145px"></asp:DropDownList>
				<asp:LinkButton ID="LkBtn" runat="server" PostBackUrl="ProjectsAddType.aspx" CausesValidation="False">新增项目类型</asp:LinkButton>
			</td>
		</tr>
		<tr>
			<td class="style3"><strong>关联订单：</strong><br />相关联的订单ID</td>
			<td><asp:TextBox ID="TxtOrderID" class="form-control" runat="server" Width="222px" />
				<asp:RangeValidator ID="Rv" runat="server" ControlToValidate="TxtOrderID" Display="Dynamic" ErrorMessage="*" MaximumValue="9999" MinimumValue="0">请输入数字1-9999</asp:RangeValidator>
			</td>
		</tr>        
		<tr>
			<td class="style3"><strong>关联会员：</strong><br />相关联的会员ID</td>
			<td><asp:TextBox ID="TxtUserID" class="form-control" runat="server" Width="222px" />
				<asp:RangeValidator ID="RvTxtUser" runat="server" ControlToValidate="TxtUserID" 
					ErrorMessage="*" MaximumValue="9999" MinimumValue="0">请输入数字1-9999</asp:RangeValidator>
			</td>
		</tr>
		<tr runat="server" id="info">
			<td class="style3"><strong>用户信息：</strong><br />用户相关资料</td>
			<td>    
				<table>
					<tr><td align="right" style="font-weight:bold">姓名</td><td><asp:TextBox ID="TxtName" runat="server" class="form-control text_300"></asp:TextBox></td><td align="right" style="font-weight:bold">手机</td><td>
						<asp:TextBox ID="TxtMobile" runat="server" class="form-control" Width="220px"></asp:TextBox></td></tr>
					<tr><td align="right" style="font-weight:bold">QQ</td><td><asp:TextBox ID="TxtQQ" runat="server" class="form-control text_300"></asp:TextBox></td><td align="right" style="font-weight:bold">联系地址</td><td>
						<asp:TextBox ID="TxtAddress" runat="server" class="form-control" Width="220px"></asp:TextBox></td></tr>
					<tr><td align="right" style="font-weight:bold">联系电话</td><td><asp:TextBox ID="TxtTel" runat="server" class="form-control text_300"></asp:TextBox></td><td align="right" style="font-weight:bold">邮箱</td><td>
						<asp:TextBox ID="TxtEmail" runat="server" class="form-control" Width="221px"></asp:TextBox></td></tr>
					<tr><td align="right"style="font-weight:bold">MSN</td><td><asp:TextBox ID="TxtMSN" runat="server" class="form-control text_300"></asp:TextBox></td><td align="right" style="font-weight:bold">单位</td><td>
						<asp:TextBox ID="TxtCom" runat="server" class="form-control" Width="220px"></asp:TextBox></td></tr>
				</table>
			</td>
		</tr>
		<tr>
			<td class="style3"><strong>项目要求：</strong><br />项目需求说明</td>
			<td><asp:TextBox ID="TxtRequire" class="form-control" runat="server" Height="82px" TextMode="MultiLine" Width="380px" /></td>
		</tr>
		<tr>
			<td class="style3"><strong>价格：</strong><br />该项目的价格(人民币)</td>
			<td><asp:TextBox ID="TxtPrice" class="form-control" runat="server" Width="222px" />元<asp:RangeValidator 
					ID="RvPrice" runat="server" ControlToValidate="TxtPrice" Display="Dynamic" 
					ErrorMessage="价格格式不对" MaximumValue="99999" MinimumValue="0"></asp:RangeValidator>
			</td>
		</tr>
		
		
		<tr>
			<td class="style3"><strong>审核状态：</strong><br />审核中.已通过审核</td>
			<td>
				<asp:RadioButtonList ID="RBLAudit" runat="server" RepeatDirection="Horizontal" Width="222px" AutoPostBack="True">
					<asp:ListItem Value="1">等待审核</asp:ListItem>
					<asp:ListItem Value="2">通过审核</asp:ListItem>
				</asp:RadioButtonList>
			</td>
		</tr>
		<tr>
			<td class="style3"><strong>负责人：</strong><br />该项目的负责人或组长</td>
			<td><asp:TextBox ID="TxtLeader" class="form-control" runat="server" Width="222px" /></td>
		</tr>
		<tr>
			<td class="style3"><strong>项目状态：</strong><br />未启动:已通过审核,等待启动中.<br />启动:项目开始做了.<br />完成-2.<br />存档:存档后该项目不可再度修改</td>
			<td>
				<asp:RadioButtonList ID="RBListStatus" runat="server" AutoPostBack="True">
					<asp:ListItem Value="0">未启动</asp:ListItem>
					<asp:ListItem Value="1">启动</asp:ListItem>
					<asp:ListItem Value="2">完成</asp:ListItem>
					<asp:ListItem Value="3">存档</asp:ListItem>
				</asp:RadioButtonList>
			</td>
		</tr>
		<tr>
			<td class="style3"><strong>启动时间：</strong><br />通过审核后，项目开工的时间</td>
			<td><asp:TextBox ID="TxtBeginTime" class="form-control" runat="server" Width="222px" onfocus="calendar(this)"/></td>
		</tr>
		<tr>
			<td class="style3"><strong>当前进度：</strong><br /></td>
			<td><asp:DropDownList ID="DDListProcess" runat="server"></asp:DropDownList><a href="ProjectsProcesses.aspx?id=<%=GetID() %>">查看详情</a></td>
		</tr>
		<tr>
			<td class="style3"><strong>完成时间：</strong><br /></td>
			<td><asp:TextBox ID="TxtCompleteTime" class="form-control" runat="server" Width="222px" onfocus="calendar(this)"/></td>
		</tr>
		<tr>
			<td class="style1"><strong>评分：</strong><br />请输入0-100间的数字</td>
			<td class="style2"><asp:TextBox ID="TxtRating" class="form-control" runat="server" Width="222px"/>
				<asp:RangeValidator ID="RvRating" runat="server" ControlToValidate="TxtRating" ErrorMessage="*" MaximumValue="99" MinimumValue="0">请打分0-99之间</asp:RangeValidator>
			</td>
		</tr>
		<tr>
			<td class="style3"><strong>评价：</strong><br />项目完成后，客户对它的评价</td>
			<td><asp:TextBox ID="TxtEvaluation" class="form-control" runat="server" Width="380px" Height="84px" TextMode="MultiLine"/></td>
				   
		</tr>
		<tr>
			<td class="style3"><strong>申请时间：</strong><br /></td>
			<td><asp:TextBox ID="TxtApplicationTime" class="form-control" runat="server" Width="222px" onfocus="calendar(this)" ToolTip="选择申请时间" /></td>
		</tr>
		<asp:Literal ID="lblHtml" runat="server"></asp:Literal>
	</table>
	<div class="clearbox"></div>
	<div style=" text-align:center; width:792px"><asp:Button ID="BtnCommit" 
			runat="server" Text="修改"  class="btn btn-primary" onclick="BtnCommit_Click"/>
		<asp:Button ID="BtnBack" runat="server" Text="返回" class="btn btn-primary" onclick="Button2_Click" />
	</div>
</asp:Content>
