<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserRen.aspx.cs" Inherits="ZoomLaCMS.Plugins.Domain.UserRen" ClientIDMode="Static" MasterPageFile="~/Common/Common.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>IDC服务续费</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<ol class="breadcrumb">
<li><a href="#">站群中心</a></li>
<li><a href="/user/">用户中心</a></li>
<li class="active">服务续费</li>
</ol>
<div id="site_main" style="margin-top:15px;">
<div id="tab3">
<table class="table table-striped table-bordered table-hover" style="text-align:center;width:100%;">
	<tr><td colspan="8" style="background-color:#DFDFDF"><h2>订单状态</h2></td></tr>
	<tr><td>序号</td><td>品名</td><td>金额</td><td>绑定主机</td><td>购买日期</td><td>到期日期</td><td colspan="2">状态</td></tr>
	<asp:Repeater runat="server" ID="OrderRP" EnableViewState="false">
		<ItemTemplate>
			 <tr><td><%#Eval("ID") %></td><td><%#Eval("ProName") %></td><td><%#Eval("AllMoney","{0:f2}") %></td><td>
				 <%#BindSite(Eval("Internalrecords")) %></td>
				 <td><%#DataBinder.Eval(Container.DataItem,"AddTime", "{0:yyyy年M月d日}") %></td><td> <%#DataBinder.Eval(Container.DataItem,"EndTime", "{0:yyyy年M月d日}")%></td><td>
					 <%#IsExpire(Eval("EndTime")) %></td></tr>
	   </ItemTemplate>
	</asp:Repeater>
	<tr><td colspan="8" style="background-color:#DFDFDF"><h2>商品信息</h2></td></tr>
	<tr><td>品名</td><td>图片</td><td>服务期限</td><td>到期提醒</td><td>单价</td><td>数量</td><td>总金额</td><td>简述</td></tr>
	<tr>
		<td>
			<asp:HyperLink runat="server" ID="proNameL" ToolTip="点击查看详情"></asp:HyperLink>
			<asp:HiddenField runat="server" ID="dataField" /></td>
		<td><img src="#" runat="server" id="proPic" style="width:50px;height:50px;" title=""/></td>
		<td><asp:Label runat="server" ID="proPeriod" /></td>
		<td><asp:Label runat="server" ID="proRemind" /></td>
		<td><asp:Label runat="server" ID="proPrice"></asp:Label></td>
		<td><asp:TextBox runat="server" ID="proNum" Text="1" style="text-align:center;width:40px;" oninput="calcNum();"/>
		<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="proNum" ValidationGroup="add" ForeColor="Red" Display="Dynamic" ErrorMessage="不能为空" />
		<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="proNum" ValidationGroup="add" ForeColor="Red" Display="Dynamic" ErrorMessage="只能是数字" ValidationExpression="^\d+$" /></td>
		<td><label id="allNum"></label></td>
		<td><asp:Label runat="server" ID="proDetail"></asp:Label></td>
	</tr>
</table>
<div style="margin-top:10px;">
<asp:Button runat="server" ID="sureBtn" Text="确定续费" OnClick="sureBtn_Click" ValidationGroup="add" class="btn btn-primary"/> 
</div>
</div>
</div>
<script type="text/javascript">
function calcNum() {
	money = $("#proPrice").text() * $("#proNum").val();
	$("#allNum").text(money);
}
$().ready(function () {
	calcNum();
 $("tr").mouseover(function () { $(this).removeClass("tdbg").addClass("tdbgmouseover") }).mouseout(function () { $(this).removeClass("tdbgmouseover").addClass("tdbg") });
});
</script>
</asp:Content>