<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SpecialManage.aspx.cs" Inherits="ZoomLa.WebSite.Manage.SpecialManage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>专题分类</title>
    <link href="../../App_Themes/AdminDefaultTheme/Guide.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/index.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/main.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="r_navigation">
		<div class="r_n_pic"></div>
		<span>后台管理</span>&gt;&gt;<span>系统设置</span> &gt;&gt;<span><a href="SpecialManage.aspx">专题分类管理</a></span>
	</div>
    <div class="clearbox"></div>    
    <asp:GridView ID="GV" runat="server" AutoGenerateColumns="False" AllowPaging="true" DataKeyNames="SpecCateID" PageSize="20" Width="100%" OnPageIndexChanging="GV_PageIndexChanging" OnRowEditing="Egv_RowEditing" OnRowDeleting="GV_RowDeleting" OnRowCommand="Lnk_Click">
        <Columns>
            <asp:BoundField DataField="SpecCateID" HeaderText="ID" ItemStyle-HorizontalAlign=Center>
                <HeaderStyle Width="6%" />
            </asp:BoundField>
            <asp:BoundField DataField="SpecCateName" HeaderText="专题类别名称" ItemStyle-HorizontalAlign=Center>
                <HeaderStyle Width="20%" />
            </asp:BoundField>
            <asp:BoundField DataField="SpecCateDir" HeaderText="专题类别目录" ItemStyle-HorizontalAlign=Center>
                <HeaderStyle Width="10%" />
            </asp:BoundField>
            <asp:BoundField DataField="SpecCateDesc" HeaderText="专题类别描述" ItemStyle-HorizontalAlign=left>
                <HeaderStyle Width="30%" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="操作" ItemStyle-HorizontalAlign=Center >
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" CommandName="AddSpec" CommandArgument='<%# Eval("SpecCateID")%>'
                        runat="server">添加专题</asp:LinkButton> |
                    <asp:LinkButton ID="LinkButton2" CommandName="SpecList" CommandArgument='<%# Eval("SpecCateID")%>'
                        runat="server">专题列表</asp:LinkButton> |
                    <asp:LinkButton ID="LnkModify" CommandName="Modify" CommandArgument='<%# Eval("SpecCateID")%>'
                        runat="server">修改</asp:LinkButton> |
                    <asp:LinkButton ID="LnkDelete"  runat="server" CommandName="Delete" OnClientClick="if(!this.disabled) return confirm('确实要删除此专题类别吗？');"
                        CommandArgument='<%# Eval("SpecCateID")%>'>删除</asp:LinkButton>                    
                  </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <RowStyle ForeColor="Black" CssClass="tdbg" Height="25px" />
         <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
         <PagerStyle CssClass="tdbg" ForeColor="Black" HorizontalAlign="Center" />
         <HeaderStyle CssClass="tdbg" Font-Bold="True" ForeColor="#E7E7FF" BorderStyle="None" Height="30px" Font-Overline="False" />
         <PagerSettings FirstPageText="第一页" LastPageText="最后页" Mode="NextPreviousFirstLast" NextPageText="下一页" PreviousPageText="上一页" />
    </asp:GridView>
    
    
    </form>
</body>
</html>
