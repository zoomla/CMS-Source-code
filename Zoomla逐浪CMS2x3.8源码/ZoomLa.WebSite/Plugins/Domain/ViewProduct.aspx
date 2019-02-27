<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewProduct.aspx.cs" Inherits="Plugins_Domain_ViewProduct" MasterPageFile="~/Common/Common.master" ClientIDMode="Static" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<style>
    .redStar {color: #ff0000;padding: 0 0 0 3px;}
    .nochoose, .choose{	width: 100px;height: 22px;padding-bottom: 1px;padding-left: 1px;padding-right: 1px;padding-top: 1px;font-weight: normal;cursor: pointer;/*line-height: 120%;*/ font-size:14px;}
    .nochoose { color: #ffffff;}
    .choose{ background:#03a1f0;color: #ffffff; font-weight:bold;}
</style>
<script type="text/javascript" src="/JS/ZL_Regex.js"></script>
<title>浏览服务</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div id="site_main" style="margin-top:15px;">
<div id="tab3">
	<div id="viewDiv" runat="server">
        <div class="top_opbar">
            <asp:TextBox runat="server" CssClass="form-control text_md" ID="keyWord" placeholder="服务搜索" />
            <asp:Button runat="server" ID="searchBtn" Text="搜索" OnClick="searchBtn_Click" CssClass="btn btn-primary" />
        </div>
		<ZL:ExGridView runat="server" ID="EGV" AutoGenerateColumns="false" AllowPaging="true"  OnPageIndexChanging="EGV_PageIndexChanging" OnRowCommand="EGV_RowCommand"
		 OnRowCancelingEdit="EGV_RowCancelingEdit" PageSize="10" CssClass="table table-bordered table-striped table-hover"
		 EnableTheming="False" EmptyDataText="没有任何数据!"  AllowSorting="True"  IsHoldState="false" >
		<PagerStyle HorizontalAlign="Center" />
		<RowStyle Height="24px" HorizontalAlign="Center" />
			<Columns>
				 <asp:BoundField HeaderText="序号" DataField="ID" ReadOnly="true" />
				   <asp:TemplateField HeaderText="品名">
					<ItemTemplate>
					   <a href="/Shop.aspx?ID=<%#Eval("ID") %>" target="_viewDetail"><%#Eval("ProName") %></a>
					</ItemTemplate>
				</asp:TemplateField>
				<asp:TemplateField HeaderText="单价">
					<ItemTemplate>
						<%#DataBinder.Eval(Container.DataItem,"LinPrice","{0:.00}")%>
					</ItemTemplate>
				</asp:TemplateField>
				<asp:TemplateField HeaderText="服务期限">
					<ItemTemplate>
						<%#GetServerPeriod(Eval("ServerPeriod"),Eval("ServerType"))%>
					</ItemTemplate>
				</asp:TemplateField>
				<asp:BoundField HeaderText="简述" DataField="ProInfo"/>
				 <asp:TemplateField HeaderText="操作">
					<ItemTemplate>
						<asp:LinkButton runat="Server" CommandArgument='<%#Eval("ID") %>' CommandName="wantPay">购买</asp:LinkButton>
						<a href="/Shop.aspx?ID=<%#Eval("ID") %>" target="_viewDetail">查看详情</a>
					</ItemTemplate>
				 </asp:TemplateField>
			</Columns>
	</ZL:ExGridView>
	</div>
<div runat="server" id="payDiv" visible="false">
	<table class="table table-bordered table-hover table-striped" style="text-align:center;">
		<tr><td>品名</td><td>图片</td><td>服务期限</td><td>到期提醒</td><td>单价</td><td>数量</td><td>总金额</td><td>简述</td></tr>
		<tr>
			<td>
				<asp:HyperLink runat="server" ID="proNameL" ToolTip="点击查看详情"></asp:HyperLink>
				<asp:HiddenField runat="server" ID="dataField" /></td>
			<td><img src="#" runat="server" id="proPic" class="img_50"/></td>
			<td><asp:Label runat="server" ID="proPeriod" /></td>
			<td><asp:Label runat="server" ID="proRemind" /></td>
			<td><asp:Label runat="server" ID="proPrice"></asp:Label></td>
			<td><asp:TextBox runat="server" ID="proNum" Text="1" style="text-align:center;width:50px;" oninput="calcNum();"/>
			<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="proNum" ValidationGroup="add" ForeColor="Red" Display="Dynamic" ErrorMessage="不能为空" />
			<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="proNum" ValidationGroup="add" ForeColor="Red" Display="Dynamic" ErrorMessage="只能是数字" ValidationExpression="^\d+$" /></td>
			<td><label id="allNum"></label></td>
			<td><asp:Label runat="server" ID="proDetail"></asp:Label></td>
		</tr>
	</table>
	<asp:Button runat="server" ID="sureBtn" CssClass="btn btn-primary" Text="确定" OnClick="sureBtn_Click" ValidationGroup="add"/> 
	<input type="button" value="返回" class="btn btn-primary" onclick="location = location;" />
</div>
</div>
</div>
<script type="text/javascript">
	$().ready(function () {
		$(":text").addClass("site_input");
		$(":submit").addClass("site_button");
		$(":button").addClass("site_button");
		$("#EGV tr th").css("text-align", "center");
		calcNum();
	});
	function calcNum()
	{
		money = $("#proPrice").text() * $("#proNum").val();
		$("#allNum").text(money);
	}
</script>
</asp:Content>