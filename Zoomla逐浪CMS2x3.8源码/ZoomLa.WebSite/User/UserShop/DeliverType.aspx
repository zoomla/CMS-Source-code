<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DeliverType.aspx.cs" Inherits="User_Shop312_DeliverType" MasterPageFile="~/User/Default.master"%>
<%@ Register Src="WebUserControlTop.ascx" TagName="WebUserControlTop" TagPrefix="uc2" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>模板列表</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div id="pageflag" data-nav="shop" data-ban="store"></div> 
<div class="container margin_t5">
	<ol class="breadcrumb">
		<li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
		<li class="active">运费模板[<a href="AddDeliverType.aspx">添加模板</a>]</li>
		<div class="clearfix"></div>
	</ol>
</div>
<div class="container">
	<div class="btn_green"><uc2:WebUserControlTop ID="WebUserControlTop1" runat="server" /></div>
 <ZL:ExGridView ID="Egv" runat="server" AutoGenerateColumns="False" DataKeyNames="ID" PageSize="10" 
	 OnRowDataBound="Egv_RowDataBound" OnPageIndexChanging="Egv_PageIndexChanging" OnRowCommand="Egv_RowCommand" IsHoldState="false" AllowPaging="True" AllowSorting="True" CssClass="table table-striped table-bordered table-hover" EnableTheming="False" EnableModelValidation="True" EmptyDataText="暂无信息！！">
	<Columns>
		<asp:TemplateField HeaderText="ID">
			<ItemTemplate>
				<input type="checkbox" name="idchk" value="<%#Eval("ID") %>" />
				<%#Eval("ID") %>
			</ItemTemplate>
		</asp:TemplateField>
		<asp:TemplateField HeaderText="模板名称">
			<ItemTemplate>
				 <a href="AddDeliverType.aspx?id=<%#Eval("id") %>"><%#Eval("TlpName") %></a>
			</ItemTemplate>
		</asp:TemplateField>
		<asp:TemplateField HeaderText="计价方式">
			<ItemTemplate>
				 <%#GetMode() %>
			</ItemTemplate>
		</asp:TemplateField>
		<asp:BoundField HeaderText="备注" DataField="Remind" />
		<asp:BoundField HeaderText="备注(仅卖家)" DataField="Remind2" />
		<asp:TemplateField HeaderText="操作">
			<ItemTemplate>
				<a runat="server" id="edit_btn" class="option_style" href='<%#"AddDeliverType.aspx?id="+Eval("id") %>'><i class="fa fa-pencil" title="修改"></i></a>
				<asp:LinkButton CssClass="option_style" CommandName="Del" CommandArgument='<%# Eval("ID") %>' OnClientClick="return confirm('不可恢复性删除数据,你确定将该数据删除吗？');" runat="server"><i class="fa fa-trash-o" title="删除"></i></asp:LinkButton>
			</ItemTemplate>
		</asp:TemplateField>
	</Columns>
</ZL:ExGridView>
说明：“禁用”某送货方式后，前台订购时将不再显示此送货方式，但已有订单中仍然显示。
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">

</asp:Content>