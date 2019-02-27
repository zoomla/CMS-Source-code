<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AuditManage.aspx.cs" Inherits="ZoomLaCMS.Manage.I.ASCX.AuditManage" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>内容预审核管理</title>
<script type="text/javascript" src="/JS/SelectCheckBox.js"></script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div id="nocontent"  runat="server" style="display:none">
暂无预审核信息
</div>
<ZL:ExGridView ID="Egv" runat="server"  AutoGenerateColumns="False" 
	CssClass="table table-striped table-bordered table-hover"  Width="100%" AllowSorting="true" 
	onrowdatabound="Egv_RowDataBound" OnPageIndexChanging="Egv_PageIndexChanging" onrowcommand="Egv_RowCommand">
	<Columns>
		<asp:TemplateField>
			<ItemTemplate>
				<asp:CheckBox ID="chkSel" runat="server" />
			</ItemTemplate>
			<ItemStyle  HorizontalAlign="Center" />
		</asp:TemplateField>
		<asp:TemplateField HeaderText="ID">
			<ItemTemplate>
			<asp:HiddenField ID="hfIds" runat="server" Value='<%#Eval("id") %>' />
			<asp:HyperLink ID="id" runat="server" Text='<%# Eval("id") %>' NavigateUrl='AddAudit.aspx?menu=edit&id=<%# Eval("id") %>'></asp:HyperLink>
			</ItemTemplate>
			<ItemStyle  HorizontalAlign="Center" />
		</asp:TemplateField>
		<asp:TemplateField  HeaderText="指定审核内容">
			<ItemTemplate>
				<asp:HiddenField ID="hfId" runat="server" Value='<%#Eval("NodeId") %>' />
				<asp:Label ID="lblContent" runat="server" Text=""></asp:Label>
			</ItemTemplate>
			 <ItemStyle  HorizontalAlign="Center" />
		  </asp:TemplateField>
		   <asp:TemplateField  HeaderText="开始时间">
			<ItemTemplate>
				<asp:Label ID="time" runat="server" Text='<%#Eval("BeginTime") %>'></asp:Label>
			</ItemTemplate>
			 <ItemStyle  HorizontalAlign="Center" />
		  </asp:TemplateField>
		   <asp:TemplateField  HeaderText="结束时间">
			<ItemTemplate>
				<asp:Label ID="endTime" runat="server" Text='<%#Eval("endTime") %>'></asp:Label>
			</ItemTemplate>
			 <ItemStyle  HorizontalAlign="Center" />
		  </asp:TemplateField>
		<asp:TemplateField HeaderText="操作">
			<ItemTemplate>
				<asp:LinkButton ID="LnkDelete" runat="server" CommandName="Del" OnClientClick="if(!this.disabled) return confirm('确实要删除吗？');"
					CommandArgument='<%# Eval("id")%>'>删除</asp:LinkButton>
			 <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Upd" CommandArgument='<%# Eval("id")%>'>修改</asp:LinkButton>
			</ItemTemplate>
			<ItemStyle  HorizontalAlign="Center" />
		</asp:TemplateField>
	</Columns>
	<RowStyle ForeColor="Black" CssClass="tdbg" Height="25px" />
	<SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
	<PagerStyle CssClass="tdbg" ForeColor="Black" HorizontalAlign="Center" />
	<HeaderStyle CssClass="tdbg" Font-Bold="True" ForeColor="#E7E7FF" BorderStyle="None" Height="30px" Font-Overline="False" />
</ZL:ExGridView>
<br />
<asp:CheckBox ID="cbAll" runat="server" AutoPostBack="True" Font-Size="9pt" onclick="CheckAll(this)" Text="全选" />
<asp:Button ID="btnDel" runat="server" Text="批量删除" OnClick="btnDel_Click" class="btn btn-primary" OnClientClick="if(!IsSelectedId()){alert('请选择内容');return false;}else{return confirm('确定删除？')};" />
</asp:Content>
