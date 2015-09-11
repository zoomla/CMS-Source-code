<%@ Page Language="C#" Title="角色管理" AutoEventWireup="true" CodeFile="RoleManage.aspx.cs" Inherits="User.RoleManage" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>角色管理</title>
    <link href="../../App_Themes/AdminDefaultTheme/Guide.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/index.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/main.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="r_navigation">
    <div class="r_n_pic"></div>
    <span>后台管理</span>&gt;&gt;<span><a href="AdminManage.aspx">管理员管理</a></span> &gt;&gt;角色管理
    </div>
    <div class="clearbox"></div>
    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" CssClass="border" Width="100%" ForeColor="#333333" AutoGenerateColumns="False" DataKeyNames="RoleID" PageSize="10" OnPageIndexChanging="GridView1_PageIndexChanging" OnRowCommand="LnkModify_Click">
        <Columns>
            <asp:BoundField DataField="RoleID" HeaderText="ID">
                <HeaderStyle Width="10%" />
                <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="RoleName" HeaderText="角色名">
                <HeaderStyle Width="10%" />
                <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="Description" HeaderText="描述">
                <HeaderStyle Width="30%" />
                <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
            </asp:BoundField>                            
            <asp:TemplateField HeaderText="操作">
            <ItemTemplate>
                <asp:LinkButton ID="LinkEdit" CommandName="ModifyRole" CommandArgument='<%# Eval("RoleID")%>'
                    runat="server">修改</asp:LinkButton>
                <asp:LinkButton ID="LnkDel" CommandName="Del" CommandArgument='<%# Eval("RoleID")%>'
                    runat="server">删除</asp:LinkButton>
                <asp:LinkButton ID="LinkButton1" CommandName="ModifyPower" CommandArgument='<%# Eval("RoleID")%>'
                    runat="server">权限设置</asp:LinkButton>
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
