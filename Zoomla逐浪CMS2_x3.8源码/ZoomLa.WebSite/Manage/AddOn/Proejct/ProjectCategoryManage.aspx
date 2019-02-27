<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Manage/I/Default.master" CodeFile="ProjectCategoryManage.aspx.cs" Inherits="manage_AddOn_ProjectCategoryManage" EnableViewStateMac="false" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>项目分类管理</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<ZL:ExGridView RowStyle-HorizontalAlign="Center" ID="GridView1" DataKeyNames="ProjectCategoryID" AllowSorting="true" runat="server"  OnRowCommand="GridView1_RowCommand" AutoGenerateColumns="False" AllowPaging="True" PageSize="6" Width="100%" OnPageIndexChanging="GridView1_PageIndexChanging" EmptyDataText="无相关数据">
			<Columns>
				<asp:TemplateField HeaderText="选中">                     
					<ItemTemplate>
						<asp:CheckBox ID="SelectCheckBox" runat="server" />
					</ItemTemplate>
					<ItemStyle CssClass="tdbg" />
				</asp:TemplateField>
				<asp:BoundField DataField="ProjectCategoryID" HeaderText="序号">
				 <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
				  </asp:BoundField>                  
						  
				<asp:TemplateField HeaderText="分类名称">
				   <ItemTemplate>
					  
							<%# DataBinder.Eval(Container.DataItem,"ProjectCategoryName")%>
			
					</ItemTemplate>
					 <ItemStyle  CssClass="tdbg" HorizontalAlign="Center" />
				</asp:TemplateField>
									 
				<asp:TemplateField HeaderText="操作" ItemStyle-HorizontalAlign="Center">
			<ItemTemplate>                                            
				<asp:LinkButton ID="LnkDelete"  runat="server" CommandName="DelProCate" OnClientClick="return confirm('确实要删除吗？');"
					CommandArgument='<%# Eval("ProjectCategoryID")%>'>删除</asp:LinkButton>                         
				</ItemTemplate>
			  <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
		</asp:TemplateField>
			</Columns>
		  <RowStyle ForeColor="Black" BackColor="#DEDFDE" Height="25px" />
 <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
 <PagerStyle CssClass="tdbg" ForeColor="Black" HorizontalAlign="Center" />
 <HeaderStyle CssClass="tdbg" Font-Bold="True" ForeColor="#E7E7FF" BorderStyle="None" Height="30px" Font-Overline="False" />
		</ZL:ExGridView>
		   <div class="clearbox"></div>   
<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="AddProjectCategory.aspx">添加分类</asp:HyperLink>           
 <asp:Button ID="btndelete" runat="server" OnClientClick="if(!IsSelectedId()){alert('请选择删除项');return false;}else{return confirm('你确定要将所有选择项删除吗？')}"
					Text="删除选定分类" OnClick="btndelete_Click" class="btn btn-primary"/>
</asp:Content>

