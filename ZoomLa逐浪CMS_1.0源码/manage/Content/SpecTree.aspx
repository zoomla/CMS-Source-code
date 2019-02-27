<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SpecTree.aspx.cs" Inherits="ZoomLa.WebSite.Manage.Content.SpecTree" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
    <link href="../../App_Themes/AdminDefaultTheme/Guide.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/index.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/main.css" type="text/css" rel="stylesheet" />
</head>
<body id="Guidebody" style="margin: 0px; margin-top:1px;">
<form id="formGuide" runat="server">
    <div id="Guide_back">
    <ul>
        <li id="Guide_top">
            <div id="Guide_toptext">
                专题内容管理</div>
        </li>
        <li id="Guide_main">
            <div id="Guide_box">
                <asp:TreeView ID="tvNav" runat="server" ExpandDepth="0" ShowLines="True" EnableViewState="False" NodeIndent="10" LineImagesFolder="~/TreeLineImages">
                    <NodeStyle BorderStyle="None" />
                </asp:TreeView>
            </div>
        </li>
     </ul>
    </div>
</form>
</body>
</html>
