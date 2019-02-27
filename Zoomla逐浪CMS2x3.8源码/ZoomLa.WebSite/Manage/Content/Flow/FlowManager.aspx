<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FlowManager.aspx.cs" Inherits="ZoomLa.WebSite.Manage.Content.FlowManager" EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master"%>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>流程管理</title>
    <style>
        #AllID_Chk{display:none;}
    </style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<asp:ObjectDataSource ID="odsFlow" runat="server" SelectMethod="GetFlowAll" TypeName="ZoomLa.BLL.B_Flow"
	DeleteMethod="DelFlowById">
	<DeleteParameters>
		<asp:Parameter Name="id" Type="Int32" />
	</DeleteParameters>
</asp:ObjectDataSource>
<ZL:ExGridView ID="EGV" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
	DataSourceID="odsFlow" OnPageIndexChanging="EGV_PageIndexChanging"  DataKeyNames="id" RowStyle-HorizontalAlign="Center" 
	OnRowDataBound="gwFlow_RowDataBound" PageSize="20"  class="table table-striped table-bordered table-hover">
	<Columns>
		<asp:BoundField DataField="id" HeaderText="ID" />
		<asp:BoundField DataField="flowName" HeaderText="流程名称"></asp:BoundField>
		<asp:BoundField DataField="flowDepict" HeaderText="流程描述"></asp:BoundField>
		<asp:TemplateField HeaderText="流程操作" ShowHeader="False">
			<ItemTemplate>
                <a href='ModifyFlow.aspx?id=<%#Eval("id") %>' class="option_style"><i class="fa fa-pencil" title="修改"></i></a>
                <asp:LinkButton ID="lbDel" runat="server" CausesValidation="False" CommandName="Delete" CssClass="option_style"><i class="fa fa-trash-o" title="删除"></i></asp:LinkButton>
                <a href='FlowManager.aspx?Action=copy&id=<%#Eval("id") %>' class="option_style"><i class="fa fa-copy" title="复制"></i></a>
				<a href='FlowProcess.aspx?id=<%# Eval("id") %>&name=<%# Eval("flowName") %>' class="option_style"><i class="fa fa-plus" title="添加"></i>添加步骤</a>
                <a href='FlowProcessManager.aspx?id=<%# Eval("id") %>&name=<%#Eval("flowName") %>' class="option_style"><i class="fa fa-list-alt" title="列表"></i>步骤列表</a>
			</ItemTemplate>
		</asp:TemplateField>
	</Columns>
	<PagerStyle HorizontalAlign="Center" Font-Bold="true" />
	<HeaderStyle Height="26px" />
</ZL:ExGridView>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript" src="/js/Common.js"></script>
</asp:Content>

