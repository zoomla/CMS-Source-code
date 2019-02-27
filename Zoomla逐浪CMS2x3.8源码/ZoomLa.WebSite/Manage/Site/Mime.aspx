<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Mime.aspx.cs" Inherits="manage_Site_mime" MasterPageFile="~/manage/Site/OptionMaster.master"%>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>MIME管理</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="pageContent">
<div id="m_site">
	<p style="float: left;">站群中心 >> MIME管理</p>
	<span>
		<asp:CheckBox runat="server" ID="displayAll" Checked="false" Text="显示系统默认映射" AutoPostBack="true" OnCheckedChanged="displayAll_CheckedChanged" Style="margin-left: 10px;" /></span>
</div>
<div id="site_main">
	<asp:ScriptManager runat="server"></asp:ScriptManager>
	<asp:UpdatePanel runat="server">
		<ContentTemplate>
			<div id="tab3">
			<ZL:ExGridView runat="server" ID="mimeEGV" AutoGenerateColumns="false" AllowPaging="true" PageSize="10" RowStyle-CssClass="tdbg"
				EnableTheming="False"  GridLines="None" CellPadding="2" CellSpacing="1"  Width="98%" CssClass="border"
				EmptyDataText="你当前没添加任何映射,如想查看系统映射!请点击:<input type='button' value='查看所有映射' onclick='disAll();' class='site_button'/>"
				OnRowCommand="mimeEGV_RowCommand" OnPageIndexChanging="mimeEGV_PageIndexChanging"  >
				<Columns>
					<asp:BoundField HeaderText="序列号" DataField="ID" />
					<asp:BoundField HeaderText="扩展名" DataField="Ext" />
					<asp:BoundField HeaderText="MIME类型" DataField="Type" />
					<asp:TemplateField HeaderText="操作">
						<ItemTemplate>
							<asp:LinkButton runat="server" CommandName="Del2" CommandArgument='<%#Eval("Ext") %>' OnClientClick="return confirm('你确定要删除吗!');" 
								Enabled='<%#Convert.ToInt32(Eval("ID").ToString()) > systemInt ? true : false %>'>删除</asp:LinkButton>
						</ItemTemplate>
					</asp:TemplateField>
				</Columns>
				<PagerStyle HorizontalAlign="Center" />
				<RowStyle Height="24px" HorizontalAlign="Center" />
			</ZL:ExGridView>
			<asp:TextBox runat="server" ID="extText" class="site_input" Style="margin-top: 2px; margin-bottom: 2px;" MaxLength="20"/>示例:.exe<br />
			<asp:TextBox runat="server" ID="typeText" class="site_input" Style="margin-top: 2px; margin-bottom: 2px;" MaxLength="20"/><br />
			<asp:Button runat="server" ID="saveBtn" Text="添加" class='site_button' OnClick="saveBtn_Click" />
			<input type="button" value="返回" onclick="parent.disOptionDiv();" class="site_button" />
				</div>
		</ContentTemplate>
	</asp:UpdatePanel>
</div>
<script type="text/javascript">
	function disAll() {
		$("#<%=displayAll.ClientID%>").trigger("click");
	}
</script>
</asp:Content>