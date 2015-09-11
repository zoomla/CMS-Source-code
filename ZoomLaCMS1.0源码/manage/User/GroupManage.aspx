<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GroupManage.aspx.cs" Inherits="ZoomLa.WebSite.Manage.User.GroupManage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>会员组管理</title>
    <link href="../../App_Themes/AdminDefaultTheme/Guide.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/index.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/main.css" type="text/css" rel="stylesheet" />
</head>
<body>
<form id="form1" runat="server">
<div class="r_navigation">
<div class="r_n_pic"></div>
<span>后台管理</span>&gt;&gt;<span><a href="UserManage.aspx">会员管理</a></span> &gt;&gt;会员组管理
</div>
<div class="clearbox"></div>
<div class="divbox" id="nocontent" runat="server">暂无会员组</div>
<asp:GridView ID="Gdv" runat="server" DataKeyNames="GroupID" PageSize="20" OnPageIndexChanging="Gdv_PageIndexChanging" OnRowCommand="Lnk_Click" Width="100%" AutoGenerateColumns="False">
    <Columns>
        <asp:BoundField HeaderText="会员组ID" DataField="GroupID">
        <HeaderStyle Width="10%" />
        <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:BoundField HeaderText="会员组名" DataField="GroupName">
        <HeaderStyle Width="20%" />
        <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:BoundField HeaderText="会员组说明" DataField="Description">
        <HeaderStyle Width="30%" />
        <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:TemplateField HeaderText="注册可选">
          <HeaderStyle Width="10%" />
          <ItemTemplate>
              <%# GetRegStatus(Eval("RegSelect","{0}"))%>
          </ItemTemplate>
          <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
        </asp:TemplateField> 
        <asp:TemplateField HeaderText="操作">
            <ItemTemplate>
            <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Edit" CommandArgument='<%# Eval("GroupID") %>'>修改</asp:LinkButton> |
            <asp:LinkButton ID="LinkButton2" runat="server" CommandName="Del" CommandArgument='<%# Eval("GroupID") %>' OnClientClick="return confirm('你确定将该会员组彻底删除吗？')">删除</asp:LinkButton> |
            <asp:LinkButton ID="LinkButton3" runat="server" CommandName="Set" CommandArgument='<%# Eval("GroupID") %>'>会员组设置</asp:LinkButton>
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
