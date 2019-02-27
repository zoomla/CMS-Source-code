<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LabelManage.aspx.cs" Inherits="ZoomLa.WebSite.Manage.Template.LabelManage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>标签管理</title>
    <link href="../../App_Themes/AdminDefaultTheme/Guide.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/index.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/main.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="r_navigation">
		<div class="r_n_pic"></div>
		<span>后台管理</span>&gt;&gt;<span>系统设置</span> &gt;&gt;<span>标签管理</span>
	</div>
    <div class="clearbox"></div> 
    
    <table width="100%" border="0" cellpadding="0" cellspacing="1" class="border">
        <tr class="tdbg">
			<td align="left">
				&nbsp;<a href="LabelHtml.aspx">添加静态标签</a>&nbsp;&nbsp;|&nbsp;&nbsp;<a href="LabelSql.aspx">添加动态标签</a>
			</td>
		</tr>
    </table>
    <div class="clearbox"></div> 
    <table width="100%" border="0" cellpadding="0" cellspacing="1" class="border">
        <tr class="tdbg">
			<td align="left">
				标签分类：<asp:Label ID="lblLabel" runat="server" Text="Label"></asp:Label>
			</td>
		</tr>
    </table>
    <div class="clearbox"></div> 
    <table width="100%" border="0" cellpadding="0" cellspacing="1" class="border" align="center">
        <tr class="gridtitle" align="center" style="height:25px;">
            <td width="40%">名称</td><td width="20%">标签分类</td><td width="20%">标签类别</td><td>操作</td>
        </tr>
        <asp:Repeater ID="repFile" runat="server" OnItemCommand="repFileReName_ItemCommand">
        <ItemTemplate>
            <tr class="tdbg" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">            
                <td align="left">
                <%# GetLabelLink(Eval("LabelID","{0}")) %>				
			    </td>
			    <td align="center"><%#Eval("LabelCate") %></td>
			    <td align="center"><%#GetLabelType(Eval("LabelType").ToString()) %></td>
			    <td align="center">&nbsp;
				    <asp:LinkButton ID="LinkButton2" runat="server" CommandName="Edit" CommandArgument='<%# Eval("LabelID") %>'>修改</asp:LinkButton> | 
				    <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Del" CommandArgument='<%# Eval("LabelID") %>' OnClientClick="return confirm('确实要删除此标签吗？');">删除</asp:LinkButton> |
				    <asp:LinkButton ID="LinkButton3" runat="server" CommandName="Copy" CommandArgument='<%# Eval("LabelID") %>' >复制</asp:LinkButton>
			    </td>
            </tr>
        </ItemTemplate>
        </asp:Repeater>
    </table>
    <div class="clearbox"></div> 
    <div id="pager1" runat="server"></div>    
    
    </form>
</body>
</html>
