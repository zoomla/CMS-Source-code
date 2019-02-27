<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProjectExam.aspx.cs" MasterPageFile="~/Manage/I/Default.master" Inherits="manage_AddOn_ProjectExam" EnableViewStateMac="false" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>项目审核</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <ZL:ExGridView ID="Egv" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass="table table-bordered table-responsive table-hover"
DataKeyNames="ProjectID" PageSize="10" OnPageIndexChanging="Egv_PageIndexChanging" Width="100%" OnRowCommand="GridView1_RowCommand" EmptyDataText="无任何相关数据">
	<Columns>
		<asp:TemplateField HeaderText="选择" ItemStyle-HorizontalAlign="Center">
			  <ItemTemplate>
				  <asp:CheckBox ID="chkSel" runat="server" />
			  </ItemTemplate>
			  <HeaderStyle Width="4%" />
			  <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
		</asp:TemplateField> 
		<asp:TemplateField HeaderText="项目名称" ItemStyle-HorizontalAlign="Center">
			<ItemTemplate>  
				<%#DataBinder.Eval(Container.DataItem, "ProjectName").ToString()%>      
			</ItemTemplate>
			<HeaderStyle Width="15%" />
			<ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
		</asp:TemplateField>
		<asp:TemplateField HeaderText="项目简介" ItemStyle-HorizontalAlign="Center">
			<ItemTemplate>  
				<%#DataBinder.Eval(Container.DataItem, "ProjectIntro").ToString()%>      
			</ItemTemplate>
			<HeaderStyle Width="25%" />
			<ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
		</asp:TemplateField>
		<asp:TemplateField HeaderText="已审核" ItemStyle-HorizontalAlign="Center">
			<HeaderStyle Width="8%" />
			<ItemTemplate>  
			 <%# (bool)Eval("Passed") ==false ? "<span style=\"color: #ff0033\">×</span>" : "√"%>                
					 
			</ItemTemplate>
			<ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
	   </asp:TemplateField>
		 <asp:TemplateField HeaderText="操作" ItemStyle-HorizontalAlign="Center">
			<ItemTemplate> 
			<a href="ProjectExam.aspx?Action=<%# (bool)Eval("Passed") == false ? "Passed" : "CancelPassed"%>&PId=<%#Eval("ProjectID")%>">
								<%# (bool)Eval("Passed") == false ? "通过审核" : "取消审核"%>
								</a>                                         
				<asp:LinkButton ID="LnkDelete"  runat="server" CommandName="DelProject" OnClientClick="return confirm('确实要删除吗？');"
					CommandArgument='<%# Eval("ProjectID")%>'>删除</asp:LinkButton>
					 </ItemTemplate>
			  <ItemStyle CssClass="tdbg" HorizontalAlign="Center" Width="15%"/>
		</asp:TemplateField>   
		　</Columns>
	 <RowStyle ForeColor="Black" BackColor="#DEDFDE" Height="25px" />
	<SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
	<PagerStyle CssClass="tdbg" ForeColor="Black" HorizontalAlign="Center" />
	<HeaderStyle CssClass="tdbg" Font-Bold="True" ForeColor="#E7E7FF" BorderStyle="None" Height="30px" Font-Overline="False" />
</ZL:ExGridView>
<div class="clearbox"></div>         
<asp:Button ID="btnPassed" runat="server" Text="批量通过审核" OnClick="btnPassed_Click" class="btn btn-primary"/>
</asp:Content>
