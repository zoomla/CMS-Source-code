<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProjectRequireList.aspx.cs" Inherits="manage_AddOn_ProjectRequireList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>需求列表</title>
     <link href="../../App_Themes/AdminDefaultTheme/Guide.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/index.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/main.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
     <div class="r_navigation">
		<div class="r_n_pic"></div>
		<span>后台管理</span>&gt;&gt;<span> 需求列表</span>	</div> 
                                        <div class="clearbox"></div>  
                                          <asp:GridView ID="Egv" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass="border"
           DataKeyNames="RequireID" PageSize="10" OnPageIndexChanging="Egv_PageIndexChanging" Width="100%" OnRowCommand="GridView1_RowCommand">
            <Columns>
                <asp:TemplateField HeaderText="选择" ItemStyle-HorizontalAlign="Center">
                      <ItemTemplate>
                          <asp:CheckBox ID="chkSel" runat="server" />
                      </ItemTemplate>
                      <HeaderStyle Width="4%" />
                      <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
                </asp:TemplateField> 
                <asp:BoundField DataField="RequireID" HeaderText="ID" ItemStyle-HorizontalAlign="Center">
                    <HeaderStyle Width="5%" />
                    <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
                </asp:BoundField>                
                <asp:BoundField DataField="Require" HeaderText="需求内容" ItemStyle-HorizontalAlign="Center">
                    <HeaderStyle Width="20%" />
                    <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="ReuqireDate" HeaderText="提交时间" ItemStyle-HorizontalAlign="Center" >
                    <HeaderStyle Width="16%" />
                    <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
                </asp:BoundField>
               <asp:TemplateField HeaderText="立项数目" ItemStyle-HorizontalAlign="Center">
                    <HeaderStyle Width="10%" />
                    <ItemTemplate>  
                        <%# CountProjectNumByRid(Convert.ToInt32(Eval("RequireID")))%>       
                    </ItemTemplate>
                    <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
               </asp:TemplateField>             
                <asp:TemplateField HeaderText="操作" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>                        
                        <asp:LinkButton ID="LnkDelete"  runat="server" CommandName="DeleteRequest" OnClientClick="if(!this.disabled) return confirm('确实要删除吗？');"
                            CommandArgument='<%# Eval("RequireID")%>'>删除</asp:LinkButton>
                        <asp:LinkButton ID="LnkCreat"  runat="server" CommandName="CreateProject"
                            CommandArgument='<%# Eval("RequireID")%>'>立项</asp:LinkButton>
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
        
        <asp:Button ID="btnDel" runat="server" Text="批量删除" OnClientClick="if(!this.disabled) return confirm('确实要删除吗？');" OnClick="btnDel_Click" />
     
            
 
    </form>
</body>
</html>
