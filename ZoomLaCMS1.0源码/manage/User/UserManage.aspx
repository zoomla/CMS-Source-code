<%@ Page Language="C#" 　AutoEventWireup="true" CodeFile="UserManage.aspx.cs" Inherits="User.UserManage"  Title="会员管理"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>会员管理</title>    
    <link href="../../App_Themes/AdminDefaultTheme/Guide.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/index.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/main.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="r_navigation">
		<div class="r_n_pic"></div>
		<span>后台管理</span>&gt;&gt;用户管理&gt;&gt;<span>会员管理</span>
	</div>
    <div class="clearbox"></div>
    
        
        <asp:GridView ID="Egv" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass="border"
           DataKeyNames="UserID" PageSize="5" OnRowEditing="Egv_RowEditing" OnRowCommand="Lnk_Click" OnPageIndexChanging="Egv_PageIndexChanging" Width="100%">
            <Columns>
                <asp:TemplateField HeaderText="选择" ItemStyle-HorizontalAlign="Center">
                      <ItemTemplate>
                          <asp:CheckBox ID="chkSel" runat="server" />
                      </ItemTemplate>
                      <HeaderStyle Width="4%" />
                      <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
                </asp:TemplateField> 
                <asp:BoundField DataField="UserID" HeaderText="ID" ItemStyle-HorizontalAlign="Center">
                    <HeaderStyle Width="5%" />
                    <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="状态" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>  
                        <%#DataBinder.Eval(Container.DataItem, "Status").ToString() == "0" ? "<span stytle='color:red;'>正常</span>" : "锁定"%>       
                    </ItemTemplate>
                    <HeaderStyle Width="10%" />
                    <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:BoundField DataField="UserName" HeaderText="会员名" ItemStyle-HorizontalAlign="Center">
                    <HeaderStyle Width="10%" />
                    <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="RegTime" HeaderText="注册时间" ItemStyle-HorizontalAlign="Center" >
                    <HeaderStyle Width="16%" />
                    <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="LastLoginIP" HeaderText="最后登录IP" ItemStyle-HorizontalAlign="Center">
                    <HeaderStyle Width="15%" />
                    <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="LoginTimes" HeaderText="登录次数" ItemStyle-HorizontalAlign="Center">
                    <HeaderStyle Width="10%" />
                    <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="LastLockTime" HeaderText="上次被锁定时间" ItemStyle-HorizontalAlign="Center">
                    <HeaderStyle Width="15%" />
                    <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="操作" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>                        
                        <asp:LinkButton ID="LnkDelete"  runat="server" CommandName="DeleteUser" OnClientClick="if(!this.disabled) return confirm('确实要删除此会员吗？');"
                            CommandArgument='<%# Eval("UserID")%>'>删除</asp:LinkButton> |
                        <asp:LinkButton ID="LinkButton1"  runat="server" CommandName="ChgPsw" CommandArgument='<%# Eval("UserID")%>'>修改密码</asp:LinkButton>
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
        <asp:Button ID="btnCan" runat="server" Text="批量认证" OnClick="Button1_Click" />
        <asp:Button ID="btnLock" runat="server" Text="批量锁定" OnClick="btnLock_Click" />
        <asp:Button ID="btnDel" runat="server" Text="批量删除" OnClick="btnDel_Click" />
        <asp:Button ID="btnNormal" runat="server" Text="置为正常" OnClick="btnNormal_Click" />
            
 
    
    </form>
</body>
</html>
