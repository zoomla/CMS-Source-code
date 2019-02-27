<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SetNodeOrder.aspx.cs" Inherits="ZoomLa.WebSite.Manage.Content.SetNodeOrder" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>节点排序</title>
    <link href="../../App_Themes/AdminDefaultTheme/Guide.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/index.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/main.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="r_navigation">
		<div class="r_n_pic"></div>
		<span>后台管理</span>&gt;&gt;<span>系统设置</span> &gt;&gt;<a href="NodeManage.aspx">节点管理</a>&gt;&gt;<span><asp:Literal ID="Literal1" runat="server"></asp:Literal>&gt;&gt;子节点排序</span>
	</div>
    <div class="clearbox"></div>
    <table width="100%" border="0" cellpadding="0" cellspacing="1" class="border" align="center">
        <tr class="gridtitle" align="center" style="height:25px;">
            <td style="width:10%;height:20px;">
                <strong>节点ID</strong></td>
            <td style="width:20%;height:20px;">
                <strong>节点名</strong></td>
            <td style="width:20%">
                <strong>节点目录</strong></td>
            <td style="width:20%">
                <strong>节点类型</strong></td>            
            <td style="width:20%">
                <strong>排序</strong></td>                        
            
        </tr>
        <asp:Repeater ID="RepSystemModel" runat="server" OnItemCommand="Repeater1_ItemCommand">
            <ItemTemplate>
                <tr class="tdbg" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
                    <td align="center">
                        <%#Eval("NodeID")%>
                    </td>
                    <td align="center">
                        <%#Eval("NodeName")%>
                    </td>
                    <td align="center">
                        <%# Eval("NodeDir")%>
                    </td>
                    <td align="center">
                        <%# GetNodeType(Eval("NodeType", "{0}"))%>                        
                    </td>
                    <td align="center">
                    <asp:LinkButton ID="LinkButton2" runat="server" CommandName="UpMove" CommandArgument='<%# Eval("NodeID") %>'>上移</asp:LinkButton> 
                    | <asp:LinkButton ID="LinkButton3" runat="server" CommandName="DownMove" CommandArgument='<%# Eval("NodeID") %>'>下移</asp:LinkButton>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>            
    </table>
    
    </form>
</body>
</html>
