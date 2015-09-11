<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NodeTree.aspx.cs" Inherits="ZoomLa.WebSite.User.Content.NodeTree" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>栏目导航</title>
    <link href="/App_Themes/UserDefaultTheme/Default.css" type="text/css" rel="stylesheet" />
    <link href="/App_Themes/UserDefaultTheme/xtree.css" type="text/css" rel="stylesheet" />
    <link href="../css/user.css" rel="stylesheet" type="text/css" />
    <link href="../css/default1.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">    
    <asp:TreeView ID="tvNav" runat="server" ExpandDepth="0" ShowLines="True" EnableViewState="False" NodeIndent="10" LineImagesFolder="~/TreeLineImages">
        <NodeStyle BorderStyle="None" />
    </asp:TreeView>    
    </form>
</body>
</html>
