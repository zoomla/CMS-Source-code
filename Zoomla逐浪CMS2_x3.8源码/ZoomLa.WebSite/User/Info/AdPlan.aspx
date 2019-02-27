<%@ Page Title="" Language="C#" MasterPageFile="~/User/Default.master" AutoEventWireup="true" CodeFile="AdPlan.aspx.cs" Inherits="User_Info_AdPlan" ClientIDMode="Static" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
<title>广告计划</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div id="pageflag" data-nav="group" data-ban="AdPlan"></div>
<div class="container margin_t5">
<ol class="breadcrumb">
<li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
<li class="active">广告计划<a href="AdPlanAdd.aspx">[申请广告]</a></li>
</ol> 
</div>
<div class="container u_cnt">
<div class="adplan">
	 <ZL:ExGridView runat="server" ID="EGV" DataKeyNames="ZoneID" AutoGenerateColumns="false" AllowPaging="true" PageSize="10"  EnableTheming="False"  
			CssClass="table table-striped table-bordered table-hover" EmptyDataText="无相关信息!!" 
			OnPageIndexChanging="EGV_PageIndexChanging" OnRowCommand="EGV_RowCommand"  OnRowDataBound="GridView1_RowDataBound" EnableModelValidation="True" >
		<Columns>
			<asp:BoundField HeaderText="ID" DataField="ID">
				<ItemStyle Width="3%" HorizontalAlign="Center" Wrap="False" />
			</asp:BoundField>
			<asp:TemplateField HeaderText="版位名称">
				<HeaderStyle Width="8%" />
				<ItemTemplate><%# SetZoomName(Eval("ADID", "{0}"))%> </ItemTemplate>
				<ItemStyle HorizontalAlign="Center" Wrap="False" />
			</asp:TemplateField>
			<asp:TemplateField HeaderText="投放时间">
				<ItemTemplate>
					<asp:HyperLink ID="LnkTime" ToolTip='<%# SetTime(Eval("Ptime", "{0}"))%>' runat="server"><%# SetTime(Eval("Ptime", "{0}"))%></asp:HyperLink>
				</ItemTemplate>
				<ItemStyle Width="8%" HorizontalAlign="Center" Wrap="True" />
			</asp:TemplateField>
			<asp:TemplateField HeaderText="计划费用">
				<HeaderStyle Width="10%" />
				<ItemTemplate><%#priceType(Eval("Price", "{0}"))%> </ItemTemplate>
				<ItemStyle HorizontalAlign="Center" Wrap="False" />
			</asp:TemplateField>
			<asp:TemplateField HeaderText="广告附件">
				<HeaderStyle Width="10%" />
				<ItemTemplate><%#LnkFiles(Eval("Files", "{0}"))%> </ItemTemplate>
				<ItemStyle HorizontalAlign="Center" Wrap="True" />
			</asp:TemplateField>
			<asp:BoundField HeaderText="申请时间" DataField="Time" DataFormatString="{0:d}">
				<ItemStyle Width="7%" HorizontalAlign="Center" Wrap="True" />
			</asp:BoundField>
			<asp:TemplateField HeaderText="操作">
				<HeaderStyle />
				<ItemTemplate>
					<a href='AdPlanAdd.aspx?id=<%# Eval("ID") %>'>修改</a>
					<asp:LinkButton ID="LinkButton2" runat="server" CommandName="Del" CommandArgument='<%# Eval("ID") %>'>取消申请</asp:LinkButton>
					<asp:LinkButton ID="LinkButton1" runat="server" CommandName="AddAdv" CommandArgument='<%# Eval("ID") %>'>支付费用</asp:LinkButton>
				</ItemTemplate>
				<ItemStyle Width="10%" HorizontalAlign="Center" Wrap="True" />
			</asp:TemplateField>
		</Columns>
	 <PagerStyle HorizontalAlign="Center" />
	<RowStyle Height="24px" HorizontalAlign="Center"  />
</ZL:ExGridView>
</div>
</div> 
</asp:Content>
