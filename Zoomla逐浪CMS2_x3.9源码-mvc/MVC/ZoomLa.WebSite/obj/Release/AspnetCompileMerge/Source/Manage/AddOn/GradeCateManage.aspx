<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GradeCateManage.aspx.cs" Inherits="ZoomLaCMS.Manage.AddOn.GradeCateManage" MasterPageFile="~/Manage/I/Default.master"%>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>多级数据字典管理</title> 
    <script src="/JS/Controls/Control.js"></script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content"> 
<ZL:ExGridView ID="EGV" RowStyle-HorizontalAlign="Center" DataKeyNames="CateID" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="10"
	class="table table-striped table-bordered table-hover" OnPageIndexChanging="Egv_PageIndexChanging" OnRowCommand="Lnk_Click" OnRowEditing="Gdv_Editing" EmptyDataText="无相关数据" OnRowDataBound="Gdv_RowDataBound">
	<Columns> 
        <asp:TemplateField HeaderText="选择">
            <ItemTemplate><input type="checkbox" name="idchk" value="<%#Eval("CateID") %>" /></ItemTemplate>
            <ItemStyle CssClass="td_m" HorizontalAlign="Left" />
        </asp:TemplateField>            
        <asp:TemplateField HeaderText="序号">
            <ItemTemplate><label><%#Eval("CateID") %></label></ItemTemplate>
            <ItemStyle CssClass="td_m" />
        </asp:TemplateField>                                                      
		<asp:BoundField DataField="CateName" HeaderText="分类名">
			<ItemStyle HorizontalAlign="Center" />
		</asp:BoundField>
		<asp:BoundField DataField="Remark" HeaderText="备注">
			<ItemStyle  CssClass="tdbg" HorizontalAlign="Center" />
		</asp:BoundField>
		<asp:BoundField DataField="GradeField" HeaderText="分级选项名">
			<ItemStyle  CssClass="tdbg" HorizontalAlign="Center" />
		</asp:BoundField>
		<asp:TemplateField HeaderText="操作">                
			<ItemTemplate>
				<asp:LinkButton runat="server" CommandName="Edit" CommandArgument='<%# Eval("CateID") %>' CssClass="option_style"><i class="fa fa-pencil" title="修改"></i></asp:LinkButton>
				<asp:LinkButton runat="server" CommandName="DicList" CommandArgument='<%# Eval("CateID") %>' CssClass="option_style"><i class="fa fa-list-alt" title="列表"></i>一级选项列表</asp:LinkButton>
			</ItemTemplate>
			<ItemStyle  CssClass="tdbg" HorizontalAlign="Center" />
		</asp:TemplateField>
	</Columns>              
</ZL:ExGridView>
<table class="table table-striped table-bordered table-hover">
<tr>
<td>
    <asp:Button runat="server" ID="BatDel_Btn" OnClick="BatDel_Btn_Click" Text="批量删除" OnClientClick="return confirm('确定要删除吗');" CssClass="btn btn-primary" />
</td>
</tr>
</table> 
<table class="table table-striped table-bordered table-hover">
	<tr>
		<td style="width:10%;text-align:right">分类名：</td>
		<td>
			<asp:TextBox ID="txtCateName" runat="server" class="form-control text_md" data-enter="1"></asp:TextBox>
		</td>
	</tr>
	<tr>
		<td style="width:10%;text-align:right">备注：</td>
		<td>
			<asp:TextBox ID="txtRemark" runat="server" class="form-control text_md" data-enter="2"></asp:TextBox>
		</td>
	</tr>
	<tr>
		<td style="width:10%;text-align:right">
				分级选项别名：<br />
				一行一个名称&nbsp;<br />
				例如：<br />
				省份&nbsp;&nbsp;<br />
				城市&nbsp;&nbsp;
		</td>
		<td>
			<asp:TextBox ID="txtGradeField" runat="server" TextMode="MultiLine" Rows="5" class="form-control text_md" Height="89px" data-enter="3"></asp:TextBox>
		</td>
	</tr>
	<tr class="table table-striped table-bordered table-hover">
		<td colspan="2" >
			<asp:HiddenField ID="HdnCateID" Value="0" runat="server" />
			<asp:Button ID="btnSave" runat="server" Text="添 加" OnClick="btnSave_Click" class="btn btn-primary" data-enter="4"/>
		</td>
	</tr>
</table> 
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script>
        Control.EnableEnter();
    </script>
</asp:Content>

