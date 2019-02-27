<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LabelImport.aspx.cs" Inherits="ZoomLa.WebSite.Manage.Template.LabelImport" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>标签导入</title>
    <link href="../../App_Themes/AdminDefaultTheme/Guide.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/index.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/main.css" type="text/css" rel="stylesheet" />    
</head>
<body>
    <form id="form1" runat="server">
    <div class="r_navigation">
		<div class="r_n_pic"></div>
		<span>后台管理</span>&gt;&gt;<span>系统设置</span> &gt;&gt;<span><a href="LabelManage.aspx">标签管理</a></span>&gt;&gt;<span>标签导入</span>
	</div> 
	<div class="clearbox"></div>   
    <div style="text-align:center">
        <div style="width:500px;border:1px solid gray;text-align:left">
            <asp:Image ID="tp" ImageUrl="../images/loading.gif" Height="22" Width="0" runat="server" />                       
        </div>
        <table width="500px" style="text-align:center">
            <tr>
                <td align="center" style="width:250px">
                    <asp:Label ID="tn" runat="server" Text="0%"></asp:Label>
                </td>
                <td align="center" style="width:250px">
                    <asp:Label ID="finallytd" runat="server" Text=""></asp:Label>
                </td>
                <td align="center" style="width:250px">
                    <asp:Label ID="tc" runat="server" Text="0"></asp:Label>
                </td>
            </tr>
        </table>        
        <br />  
        <asp:Button ID="Button1" runat="server" Text="开始导入" OnClick="Button1_Click" />
    </div>    
    </form>
</body>
</html>