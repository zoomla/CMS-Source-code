<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LabelExport.aspx.cs" Inherits="ZoomLa.WebSite.Manage.Template.LabelExport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>标签导出</title>
    <link href="../../App_Themes/AdminDefaultTheme/Guide.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/index.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/main.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="r_navigation">
		<div class="r_n_pic"></div>
		<span>后台管理</span>&gt;&gt;<span>系统设置</span> &gt;&gt;<span>标签管理</span>&gt;&gt;<span>标签导出</span>
	</div>
    <div class="clearbox"></div>
    <div style="text-align:center">
        <div style="width:100%;border:1px solid #9bbde6; text-align:left">
            <br />
            导出标签：<br />
            <br />
            导出目标：../App_Data/LabelExport.xml<br /> 
            <br />
            导出状态：<span style="color:Red"><asp:Label ID="Label1" runat="server" Text="状态"></asp:Label></span><br />
            <br />
            <asp:Button ID="Button1" runat="server" Text="导出标签" OnClick="Button1_Click" />
        </div>
    </div>
    
    </form>
</body>
</html>
