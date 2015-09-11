<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WorkManage.aspx.cs" Inherits="manage_AddOn_WorkManage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>项目工作内容列表</title>
    <link href="../../App_Themes/AdminDefaultTheme/Guide.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/index.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/main.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
     <div class="r_navigation">
		<div class="r_n_pic"></div>
		<span>后台管理</span>&gt;&gt<span><a href="ProjectManage.aspx"> 项目管理</a></span>&gt;&gt;<span>项目节点</span>&gt;&gt;<%=ProjectName%>	</div> 
                                        <div class="clearbox"></div>
    
    <asp:GridView ID="Egv" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass="border"
           DataKeyNames="WorkID" PageSize="10" OnPageIndexChanging="Egv_PageIndexChanging" Width="100%" OnRowCommand="GridView1_RowCommand" EmptyDataText="无任何相关数据">
            <Columns>
                <asp:TemplateField HeaderText="选择" ItemStyle-HorizontalAlign="Center">
                      <ItemTemplate>
                          <asp:CheckBox ID="chkSel" runat="server" />
                      </ItemTemplate>
                      <HeaderStyle Width="4%" />
                      <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
                </asp:TemplateField> 
                <asp:BoundField DataField="WorkID" HeaderText="ID" ItemStyle-HorizontalAlign="Center">
                    <HeaderStyle Width="5%" />
                    <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="内容名称" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>  
                        <a href="DiscussList.aspx?Wid=<%#Eval("WorkID")%>"><%#DataBinder.Eval(Container.DataItem, "WorkName").ToString()%></a>      
                    </ItemTemplate>
                    <HeaderStyle Width="10%" />
                    <ItemStyle CssClass="tdbg" HorizontalAlign="Center" Width="25%"/>
                </asp:TemplateField>                 
               <asp:TemplateField HeaderText="项目ID" ItemStyle-HorizontalAlign="Center">
                    <HeaderStyle Width="10%" />
                    <ItemTemplate>  
                        <%#DataBinder.Eval(Container.DataItem, "ProjectID").ToString()%>       
                    </ItemTemplate>
                    <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
               </asp:TemplateField>  
                <asp:TemplateField HeaderText="已完成" ItemStyle-HorizontalAlign="Center">
                    <HeaderStyle Width="10%" />
                    <ItemTemplate>  
                     <%# (int)Eval("Status") == 0 ? "<span style=\"color: #ff0033\">×</span>" : "√"%>                
                             
                    </ItemTemplate>
                    <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
               </asp:TemplateField>                      
                 <asp:BoundField DataField="EndDate" HeaderText="完成时间" DataFormatString="{0:d}" HtmlEncode="false" ItemStyle-HorizontalAlign="Center" >
                    <HeaderStyle Width="10%" />
                    <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
                </asp:BoundField>      
                <asp:TemplateField HeaderText="操作" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>                        
                        <asp:LinkButton ID="LnkDelete"  runat="server" CommandName="DelWork" OnClientClick="if(!this.disabled) return confirm('确实要删除吗？');"
                            CommandArgument='<%# Eval("WorkID")%>'>删除</asp:LinkButton>
                            <asp:LinkButton ID="LinkButton2"  runat="server" CommandName="EditWork" 
                            CommandArgument='<%# Eval("WorkID")%>'>修改</asp:LinkButton> 
                            <asp:LinkButton ID="LinkButton1"  runat="server" CommandName="FinishWork" 
                            CommandArgument='<%# Eval("WorkID")%>' OnClientClick="return confirm('确实要完成吗？请认真核查!');">完成</asp:LinkButton>                                                          
                        </ItemTemplate>
                      <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
                </asp:TemplateField>
            　</Columns>
             <RowStyle ForeColor="Black" BackColor="#DEDFDE" Height="25px" />
            <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
            <PagerStyle CssClass="tdbg" ForeColor="Black" HorizontalAlign="Center" />
            <HeaderStyle CssClass="tdbg" Font-Bold="True" ForeColor="#E7E7FF" BorderStyle="None" Height="30px" Font-Overline="False" />
            <PagerSettings FirstPageText="第一页" LastPageText="最后页" Mode="NextPreviousFirstLast" NextPageText="下一页" PreviousPageText="上一页" />
        </asp:GridView>
        <div class="clearbox"></div>                    
        <asp:CheckBox ID="cbAll" runat="server" AutoPostBack="True" Font-Size="9pt" OnCheckedChanged="cbAll_CheckedChanged"
            Text="全选" />    
        
        <asp:Button ID="btnDel" runat="server" Text="批量删除" OnClick="btnDel_Click" OnClientClick="return confirm('确实要删除这些选择吗？');" />
       
    </form>
</body>
</html>
