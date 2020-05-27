<%@ Page Title="" Language="C#" MasterPageFile="~/User/Default.master" AutoEventWireup="true" CodeFile="UserOrderlist.aspx.cs" Inherits="User_UserShop_UserOrderlist" ClientIDMode="Static" ValidateRequest="false" %>
<%@ Register Src="WebUserControlTop.ascx" TagName="WebUserControlTop" TagPrefix="uc2" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
<title>用户店铺订单管理</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<ol class="breadcrumb">
	<li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
	<li><a href="ProductList.aspx">我的店铺</a></li>
	<li class="active">用户店铺订单管理</li>
</ol>
<div class="us_seta" style="margin-top: 10px;">
	<table class="table table-striped table-bordered table-hover">
		<tbody id="Tabs">
			<tr>
				<td width="34%" height="24" align="center" class="title">快速查找：
				<asp:DropDownList CssClass="form-control" ID="quicksouch" runat="server" AutoPostBack="True">
					<asp:ListItem Selected="True">请选择查找方式</asp:ListItem>
					<asp:ListItem Value="2">今天的新订单</asp:ListItem>
					<asp:ListItem Value="3">所有订单</asp:ListItem>
					<asp:ListItem Value="5">最近一个月内的新订单</asp:ListItem>
					<asp:ListItem Value="6">未确认的订单</asp:ListItem>
					<asp:ListItem Value="7">未付款的订单</asp:ListItem>
					<asp:ListItem Value="9">未送货的订单</asp:ListItem>
					<asp:ListItem Value="10">未签收的订单</asp:ListItem>
					<asp:ListItem Value="12">未开发票的订单</asp:ListItem>
					<asp:ListItem Value="13">已经作废的订单</asp:ListItem>
					<asp:ListItem Value="15">已发货的订单</asp:ListItem>
					<asp:ListItem Value="16">已签收的订单</asp:ListItem>
					<asp:ListItem Value="17">已结清的订单</asp:ListItem>
				</asp:DropDownList>
				</td>
				<td width="66%" align="center" class="title">高级查询：
				<asp:DropDownList CssClass="form-control" ID="souchtable" runat="server">
					<asp:ListItem Selected="True" Value="1">订单编号</asp:ListItem>
					<asp:ListItem Value="2">客户名称</asp:ListItem>
					<asp:ListItem Value="3">用户名</asp:ListItem>
					<asp:ListItem Value="4">收货人</asp:ListItem>
					<asp:ListItem Value="6">联系电话</asp:ListItem>
					<asp:ListItem Value="7">下单时间</asp:ListItem>
				</asp:DropDownList>
					<asp:TextBox ID="souchkey" runat="server" class="form-control" />
					<asp:Button ID="souchok" runat="server" Text=" 搜索 " class="btn btn-primary" OnClick="souchok_Click" />
				</td>
			</tr>
		</tbody>
	</table>
	<table class="table table-striped table-bordered table-hover">
		<tbody id="Tbody1">
			<tr>
				<td width="12%" align="center" class="title">订单编号</td>
				<td width="10%" align="center" class="title">客户名称</td>
				<td width="8%" align="center" class="title">用户名</td>
				<td width="14%" align="center" class="title">下单时间</td>
				<td align="center" class="title" width="6%">实际金额</td>
				<td width="8%" align="center" class="title">收款金额</td>
				<td width="8%" align="center" class="title">需要发票</td>
				<td width="8%" align="center" class="title">已开发票</td>
				<td width="8%" align="center" class="title">订单状态</td>
				<td width="8%" align="center" class="title">付款状态</td>
				<td width="8%" align="center" class="title">物流状态</td>
			</tr>
			<asp:Repeater ID="Orderlisttable" runat="server">
				<ItemTemplate>
					<tr id="<%#Eval("id") %>">
						<td height="24" align="center"><%#getorderno(Eval("id","{0}"))%></td>
						<td height="24" align="center">
							<%#GetUser(DataBinder.Eval(Container, "DataItem.Reuser", "{0}"))%>
						</td>
						<td height="24" align="center">
							<%#GetUsers(DataBinder.Eval(Container, "DataItem.userId", "{0}"))%>
						</td>
						<td height="24" align="center">
							<%#Eval("AddTime") %>
						</td>
						<td height="24" align="center">
							<%#getshijiage(DataBinder.Eval(Container, "DataItem.id", "{0}"))%>
						</td>
						<td height="24" align="center">
							<%#formatcs(Eval("ordertype","{0}"),DataBinder.Eval(Container,"DataItem.Receivablesamount","{0:N2}"))%>
						</td>
						<td height="24" align="center">
							<%#fapiao(DataBinder.Eval(Container,"DataItem.Invoiceneeds","{0}"))%>
						</td>
						<td height="24" align="center">
							<%#fapiao(DataBinder.Eval(Container, "DataItem.Developedvotes","{0}")) %>
						</td>
						<td height="24" align="center">
							<%#formatzt(DataBinder.Eval(Container, "DataItem.OrderStatus", "{0}"),"0")%>
						</td>
						<td height="24" align="center">
							<%#formatzt(DataBinder.Eval(Container, "DataItem.Paymentstatus", "{0}"),"1")%>
						</td>
						<td height="24" align="center">
							<%#formatzt(DataBinder.Eval(Container, "DataItem.StateLogistics", "{0}"),"2")%>
						</td>
					</tr>
				</ItemTemplate>
			</asp:Repeater>
			<tr onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
				<td height="24" colspan="6" align="right">总计金额：<br />
				</td>
				<td height="24" align="right" style="width: 68px">
					<asp:Label ID="thisall" runat="server"></asp:Label><br />
				</td>
				<td height="24" align="center" style="width: 88px">&nbsp;
				</td>
				<td height="24" colspan="5" align="center">&nbsp;
				</td>
			</tr>
			<tr>
				<td height="24" colspan="13" align="center">共
				<asp:Label ID="Allnum" runat="server" Text=""></asp:Label>
					条记录
				<asp:Label ID="Toppage" runat="server" Text="" />
					<asp:Label ID="Nextpage" runat="server" Text="" />
					<asp:Label ID="Downpage" runat="server" Text="" />
					<asp:Label ID="Endpage" runat="server" Text="" />
					页次：<asp:Label ID="Nowpage" runat="server" Text="" />/<asp:Label ID="PageSize" runat="server"
						Text="" />页
				<asp:TextBox ID="txtPage" runat="server" Width="25px" AutoPostBack="True"
					OnTextChanged="txtPage_TextChanged1"></asp:TextBox>
					条记录/页 转到第
				<asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True"
					OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
				</asp:DropDownList>
					页
				</td>
			</tr>
			<tr>
				<td height="24" colspan="13">说明：“已结清”与“已付款”的订单不允许删除,当订单号码成“灰色”代表此订单已“作废”
				</td>
			</tr>
		</tbody>
	</table>
</div>
</asp:Content>