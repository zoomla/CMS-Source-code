<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ZoomLaCMS.Design.Default" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<meta name="viewport" content="initial-scale=1, maximum-scale=1, user-scalable=no, width=device-width" />
<link rel="stylesheet" type="text/css" href="/Plugins/ionic/css/ionic.css"/>
<link rel="stylesheet" type="text/css" href="/dist/css/font-awesome.min.css"/>
<link rel="stylesheet" type="text/css" href="/Design/ask/js/global.css"/>
<script src="/JS/jquery-1.11.1.min.js"></script>
<script src="/JS/Modal/APIResult.js?v=<%:Ver %>"></script>
<script src="/JS/ZL_Regex.js"></script>
<script src="/JS/Controls/ZL_Array.js?v=<%:Ver %>"></script>
<script src="/JS/Plugs/moment.js"></script>
<script src="/Plugins/ionic/js/ionic.bundle.min.js"></script>
<script src="/design/ask/js/app.js?v=<%:Ver %>"></script>
<script src="/design/ask/js/controllers.js?v=<%:Ver %>"></script>
<script src="/design/ask/js/services.js?v=<%:Ver %>"></script>
<script>
    var cfg = { scope: null };
</script>
<title>动力逐浪微问卷系统</title>
</head>
<body ng-app="starter">
<form id="form1" runat="server">
<ion-side-menus><ion-side-menu-content drag-content="false"><ion-nav-view></ion-nav-view><div ng-controller="IndexCtrl"></div></ion-side-menu-content></ion-side-menus>
</form>
</body>
</html>
