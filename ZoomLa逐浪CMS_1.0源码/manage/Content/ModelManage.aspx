<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ModelManage.aspx.cs" Inherits="ZoomLa.WebSite.Manage.ModelManage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>内容模型管理</title>
    <link href="../../App_Themes/AdminDefaultTheme/Guide.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/index.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/main.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="r_navigation">
		<div class="r_n_pic"></div>
		<span>后台管理</span>&gt;&gt;<span>系统设置</span> &gt;&gt;<a href="ModelManage.aspx">内容模型管理</a>
	</div>
    <div class="clearbox"></div>    
    <table width="100%" border="0" cellpadding="0" cellspacing="1" class="border" align="center">
        <tbody>
            <tr class="gridtitle" align="center" style="height:25px;">
                <td width="5%" height="20">
                    <strong>ID</strong></td>
                <td width="5%">
                    <strong>图标</strong></td>
                <td width="10%">
                    <strong>模型名称</strong></td>
                <td width="20%">
                    <strong>模型描述</strong></td>
                <td width="10%">
                    <strong>项目名称</strong></td>
                <td width="20%">
                    <strong>表名</strong></td>                        
                <td width="30%">
                    <strong>操作</strong></td>
            </tr>
            <asp:Repeater ID="Repeater1" runat="server" OnItemCommand="Repeater1_ItemCommand">
                <ItemTemplate>
                    <tr class="tdbg" align="center" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">                        
                        <td>
                            <strong><%# Eval("ModelID") %></strong></td>
                        <td>
                            <img src="<%# GetIcon(DataBinder.Eval(Container, "DataItem.ItemIcon", "{0}"))%>" style="border-width:0px;" /></td>
                        <td>
                            <strong><%# Eval("ModelName")%></strong></td>
                        <td align="left">
                            <strong><%# Eval("Description")%></strong></td>
                        <td>
                            <strong><%# Eval("ItemName")%></strong></td>
                        <td align="left">
                            <strong><%# Eval("TableName")%></strong></td>                        
                        <td>
                            <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Edit" CommandArgument='<%# Eval("ModelID") %>'>修改</asp:LinkButton> | 
                            <asp:LinkButton ID="LinkButton2" runat="server" CommandName="Del" CommandArgument='<%# Eval("ModelID") %>' OnClientClick="return confirm('确实要删除此模型吗？');">删除</asp:LinkButton> | 
                            <asp:LinkButton ID="LinkButton3" runat="server" CommandName="Field" CommandArgument='<%# Eval("ModelID") %>'>字段列表</asp:LinkButton>                            
                            </td>
                    </tr>
                </ItemTemplate>
             </asp:Repeater>                        
        </tbody>
    </table>
    
    </form>
</body>
</html>
