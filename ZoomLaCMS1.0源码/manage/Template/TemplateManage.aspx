<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TemplateManage.aspx.cs" Inherits="ZoomLa.WebSite.Manage.Template.TemplateManage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>模板管理</title>
    <link href="../../App_Themes/AdminDefaultTheme/Guide.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/index.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/main.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="r_navigation">
		<div class="r_n_pic"></div>
		<span>后台管理</span>&gt;&gt;<span>系统设置</span> &gt;&gt;<span>模板管理</span>
	</div>
    <div class="clearbox"></div>    
    <table width="100%">
        <tr>
			<td align="left">
				当前目录：<asp:Label ID="lblDir" runat="server" Text="Label"></asp:Label>
			</td>
			<td align="right">
                <asp:Literal ID="LitParentDirLink" runat="server"></asp:Literal>
            </td>
		</tr>
    </table>
    <table width="100%" border="0" cellpadding="0" cellspacing="1" class="border" align="center">
        <tr class="gridtitle" align="center" style="height:25px;">
            <td width="45%">名称</td><td width="10%">大小</td><td width="10%">类型</td><td width="25%">修改时间</td><td>操作</td>
        </tr>
    <asp:Repeater ID="repFile" runat="server" OnItemCommand="repFileReName_ItemCommand">
    <ItemTemplate>
        <tr class="tdbg" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">            
            <td align="left">
                <img alt="" src=' <%# System.Convert.ToInt32(Eval("type")) == 1 ? "../Images/Node/closefolder.gif" :"../Images/Node/singlepage.gif" %>' />
				<a href="<%# System.Convert.ToInt32(Eval("type")) == 1 ?  "TemplateManage.aspx?Dir=" + Server.UrlEncode(Request.QueryString["Dir"] +"/"+ Eval("Name").ToString()):"TemplateEdit.aspx?filepath="+ Server.UrlEncode(Request.QueryString["Dir"] +"/"+ Eval("Name").ToString()) %>">
				<%# Eval("Name") %></a>
			</td>
			<td align="center"> <%# GetSize(Eval("size").ToString()) %></td>			
			<td align="center">
			    <asp:HiddenField ID="HdnFileType" Value='<%#Eval("type") %>' runat="server" />
                <%# System.Convert.ToInt32(Eval("type")) == 1 ? "文件夹" : Eval("content_type").ToString() + "文件" %></td>
			<td align="center"><%#Eval("lastWriteTime")%></td>
			<td align="center">&nbsp;				
				<asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%# Eval("Name").ToString()%>' CommandName='<%# System.Convert.ToInt32(Eval("type")) == 1 ? "DelDir":"DelFiles" %>' OnClientClick="return confirm('你确认要删除该文件夹或文件吗？')" >删除</asp:LinkButton>
			</td>
        </tr>
    </ItemTemplate>
    </asp:Repeater>
    </table>
    <div class="clearbox"></div>
    <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border" align="center">
        <tr class="tdbg">
			<td width="10%" align="center"><asp:Button ID="Button1" runat="server" Text="新建模板"  OnClick="Button1_Click" /></td>
			<td width="10%" align="left">目录名称：</td>
			<td align="left" width="25%"><asp:TextBox ID="txtForderName" runat="server" Width="200"></asp:TextBox></td>
			<td align="left" width="15%"><asp:Button ID="btnCreateFolder" runat="server" Text="创建目录"  OnClick="btnCreateFolder_Click" />
                <asp:HiddenField ID="HdnPath" runat="server" />
            </td>
			<td width="30%"><asp:FileUpload ID="fileUploadTemplate" runat="server" CssClass="btn"  /></td>
			<td width="10%"><asp:Button ID="btnTemplateUpLoad" runat="server" Text="上传模板" OnClick="btnTemplateUpLoad_Click"  /></td>
		</tr>
    </table>
    
    </form>
</body>
</html>
