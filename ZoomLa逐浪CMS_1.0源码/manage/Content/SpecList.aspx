<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SpecList.aspx.cs" Inherits="manage_Content_SpecList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>专题列表</title>
    <link href="../../App_Themes/AdminDefaultTheme/Guide.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/index.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/main.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="r_navigation">
		<div class="r_n_pic"></div>
		<span>后台管理</span>&gt;&gt;<span>系统设置</span> &gt;&gt;<span><a href="SpecialManage.aspx">专题分类管理</a></span> &gt;&gt;专题列表<br/>
	</div>
    <div class="clearbox"></div>
    <asp:GridView ID="GV" runat="server" AutoGenerateColumns="False" AllowPaging="true" DataKeyNames="SpecID" PageSize="20"
      Width="100%" OnPageIndexChanging="GV_PageIndexChanging" OnRowCommand="Lnk_Click">
        <Columns>
            <asp:BoundField DataField="SpecID" HeaderText="ID" ItemStyle-HorizontalAlign="Center">
                <HeaderStyle Width="6%" />
            </asp:BoundField>
            <asp:BoundField DataField="SpecName" HeaderText="专题名称" ItemStyle-HorizontalAlign="Center">
                <HeaderStyle Width="20%" />
            </asp:BoundField>
            <asp:BoundField DataField="SpecDir" HeaderText="专题目录" ItemStyle-HorizontalAlign="Center">
                <HeaderStyle Width="10%" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="打开方式" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="10%" >
                <ItemTemplate>
                    <%# GetOpenType(DataBinder.Eval(Container, "DataItem.OpenType", "{0}"))%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="SpecDesc" HeaderText="专题描述" ItemStyle-HorizontalAlign="left">
                <HeaderStyle Width="30%" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="操作" ItemStyle-HorizontalAlign="Center" >
                <ItemTemplate>                    
                    <asp:LinkButton ID="LnkModify" CommandName="Modify" CommandArgument='<%# Eval("SpecID")%>'
                        runat="server">修改</asp:LinkButton> |
                    <asp:LinkButton ID="LnkDelete"  runat="server" CommandName="Delete" OnClientClick="return confirm('确实要删除此专题吗？');"
                        CommandArgument='<%# Eval("SpecID")%>'>删除</asp:LinkButton>                    
                  </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
         <RowStyle ForeColor="Black" Height="25px" />
         <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
         <PagerStyle ForeColor="Black" HorizontalAlign="Center" />
         <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#E7E7FF" BorderStyle="None" Height="30px" Font-Overline="False" />
         <PagerSettings FirstPageText="第一页" LastPageText="最后页" Mode="NextPreviousFirstLast" NextPageText="下一页" PreviousPageText="上一页" />
    </asp:GridView>    
    
    </form>
</body>
</html>
