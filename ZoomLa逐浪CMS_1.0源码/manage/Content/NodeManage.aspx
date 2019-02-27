<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NodeManage.aspx.cs" Inherits="ZoomLa.WebSite.Manage.Content.NodeManage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>节点管理</title>
    <link href="../../App_Themes/AdminDefaultTheme/Guide.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/index.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/main.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="r_navigation">
		<div class="r_n_pic"></div>
		<span>后台管理</span>&gt;&gt;<span>系统设置</span> &gt;&gt;<a href="NodeManage.aspx">节点管理</a>
	</div>
    <div class="clearbox"></div>
    <table width="100%" border="0" cellpadding="0" cellspacing="1" class="border" align="center">
        <tr class="gridtitle" align="center" style="height:25px;">
            <td width="3%"><strong>ID</strong></td>
            <td width="40%" height="20">
                <strong>节点名称</strong></td>
            <td width="12%" height="20">
                <strong>节点类型</strong></td>
            <td width="45%">
                <strong>操作</strong></td>
        </tr>
        <asp:Repeater ID="Repeater1" runat="server">
        <ItemTemplate>
        <tr class="tdbg" align="center" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
            <td width="3%" align="center"><strong><%# Eval("NodeID") %></strong></td>
            <td width="40%" align="left">
                <%# GetIcon(DataBinder.Eval(Container, "DataItem.NodeName", "{0}"), DataBinder.Eval(Container, "DataItem.NodeID", "{0}"), DataBinder.Eval(Container, "DataItem.Depth", "{0}"), DataBinder.Eval(Container, "DataItem.NodeType", "{0}"))%></td>
            <td width="12%" height="20">
                <strong><%# GetNodeType(DataBinder.Eval(Container, "DataItem.NodeType", "{0}"))%></strong></td>
            <td width="45%">
            <%# GetOper(DataBinder.Eval(Container, "DataItem.NodeID", "{0}"), DataBinder.Eval(Container, "DataItem.NodeType", "{0}"))%>            </td>
        </tr>
        </ItemTemplate>
        </asp:Repeater>
    </table>
    
</form>
</body>
</html>
