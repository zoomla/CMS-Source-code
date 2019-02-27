<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProjectList.aspx.cs" Inherits="User_Project_ProjectList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>用户项目列表</title>
     <link href="../../App_Themes/AdminDefaultTheme/Guide.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/index.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/main.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
     <div class="r_navigation">
		<div class="r_n_pic"></div>
		<span>服务要求</span>&gt;&gt;<span>用户项目列表</span>	</div> 
                                        <div class="clearbox"></div> 
        <asp:GridView ID="Egv" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass="border"
           DataKeyNames="RequireID" PageSize="10" OnPageIndexChanging="Egv_PageIndexChanging" Width="100%" OnRowCommand="GridView1_RowCommand" EmptyDataText="无任何相关数据">
            <Columns>                         
                <asp:TemplateField HeaderText="项目ID" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>  
                        <%# DataBinder.Eval(Container.DataItem, "ProjectID").ToString()%>       
                    </ItemTemplate>
                    <HeaderStyle Width="10%" />
                    <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:BoundField DataField="ProjectName" HeaderText="项目名称" ItemStyle-HorizontalAlign="Center">
                    <HeaderStyle Width="20%" />
                    <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="StartDate" HeaderText="开始时间" ItemStyle-HorizontalAlign="Center" >
                    <HeaderStyle Width="16%" />
                    <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
                </asp:BoundField>
                            
                <asp:TemplateField HeaderText="操作" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>                        
                        <asp:LinkButton ID="LnkDelete"  runat="server" CommandName="DeleteUser" OnClientClick="if(!this.disabled) return confirm('确实要删除此会员吗？');"
                            CommandArgument='<%# Eval("ProjectID")%>' OnClick="delete_Click" Visible="false">删除</asp:LinkButton>
                        <asp:LinkButton ID="Lnk"  runat="server" CommandName="ShowDetail"
                            CommandArgument='<%# Eval("ProjectID")%>'>详细内容</asp:LinkButton>
                        
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
    </form>
</body>
</html>
