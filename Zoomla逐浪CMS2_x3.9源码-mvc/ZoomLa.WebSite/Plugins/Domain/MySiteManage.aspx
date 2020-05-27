<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MySiteManage.aspx.cs" Inherits="ZoomLaCMS.Plugins.Domain.MySiteManage" MasterPageFile="~/Common/Common.master" ClientIDMode="Static" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<script type="text/javascript">
$().ready(function () {
	$(":button").addClass("btn btn-primary");
	$(":submit").addClass("btn btn-primary");
	$("#EGV tr:last >td>:text").css("line-height", "normal");
	$("#EGV tr:first >th").css("text-align", "center");
});
</script>
<style type="text/css">
#EGV tr th {color:black;background:url(""); }
#nav_site {background:white;}
body {font-family:'Microsoft YaHei';}
</style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<ol class="breadcrumb">
<li><a href="#">站群中心</a></li>
<li><a href="/Site/Default.aspx">智能建站</a></li>
<li class="active">站点管理</li>
</ol>
<div id="site_main" style="margin-top: 5px;">
<div class="input-group" style=" width:400px;float:left;margin-bottom:10px;">
	<asp:TextBox runat="server" ID="searchText" placeholder="请输入需要查询的信息" CssClass="form-control"/>
	<span class="input-group-btn">
		<asp:Button runat="server" CssClass="btn btn-primary" ID="searchBtn" Text="搜索" OnClick="searchBtn_Click"/>
	</span>
</div>
<div class="tab3">
	<ZL:ExGridView runat="server" ID="EGV" AutoGenerateColumns="false" AllowPaging="true" PageSize="10"  EnableTheming="False" CssClass="table table-striped table-bordered table-hover" EmptyDataText="当前没有信息!!" OnPageIndexChanging="EGV_PageIndexChanging" OnRowCommand="EGV_RowCommand" >
		<Columns>
			<asp:BoundField HeaderText="ID" DataField="ID" />
			<asp:BoundField HeaderText="数据库名" DataField="DBName"/>
			<asp:BoundField HeaderText="站点名" DataField="SiteName" />
			<asp:BoundField HeaderText="绑定域名" DataField="Remind" />
			<asp:BoundField HeaderText="用户名" DataField="UserName" />
			<asp:BoundField HeaderText="初始密码" DataField="DBInitPwd" />
			<asp:BoundField HeaderText="创建时间" DataField="CreateTime" />
			<asp:TemplateField HeaderText="操作">
				<ItemTemplate>
				   <a href='<%#"http://"+Eval("Remind") %>'>前往站点</a>
				</ItemTemplate>
			</asp:TemplateField>
		   </Columns>
	</ZL:ExGridView>
</div>
</div>
</asp:Content>