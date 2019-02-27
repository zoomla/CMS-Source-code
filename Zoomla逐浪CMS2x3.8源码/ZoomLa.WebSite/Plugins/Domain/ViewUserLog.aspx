<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewUserLog.aspx.cs" Inherits="Manage_Site_User_ViewUserLog" MasterPageFile="~/Common/Common.master"%>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>用户操作日志</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<ol class="breadcrumb">
<li><a href="#">站群中心</a></li>
<li><a href="/user/">用户中心</a></li>
<li class="active">用户操作日志</li>
</ol>
<div id="site_main">
<div id="tab3">
<ZL:ExGridView runat="server" ID="EGV" AutoGenerateColumns="false" AllowPaging="true" PageSize="10" RowStyle-CssClass="tdbg"
EnableTheming="False" CssClass="table table-striped table-bordered table-hover"
EmptyDataText="无日志记录.">
<Columns>
	<asp:TemplateField>
		<ItemTemplate>
			<%#Eval("Type") %>
		</ItemTemplate>
	</asp:TemplateField>
	<asp:BoundField HeaderText="详情" DataField="Remind"/>
   <asp:TemplateField HeaderText="时间">
	   <ItemTemplate>
		   <%#Eval("CreateDate","{0:yyyy年MM月dd日 hh:mm}") %>
	   </ItemTemplate>
   </asp:TemplateField>
</Columns>
<PagerStyle HorizontalAlign="Center" />
<RowStyle Height="24px" HorizontalAlign="Center" />
</ZL:ExGridView>
</div>
</div>
</asp:Content>