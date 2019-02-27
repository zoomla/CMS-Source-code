<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NodeTree.aspx.cs" Inherits="ZoomLa.WebSite.User.Content.NodeTree" EnableViewStateMac="false" %>
<html>
<head runat="server">
<title>栏目导航</title>
<link href="../../App_Themes/UserThem/user_user.css" rel="stylesheet" type="text/css" />
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<style type="text/css">
body, td, th{font-size: 9pt;}
</style>
<script type="text/javascript">
function gotourl(url) {
        parent.frames["I2"].location = "../../" + url; void (0);

}
</script>
</head>
<body style="_margin-top:45px;margin-top:45px;">
<form id="form1" runat="server">
<asp:TreeView ID="tvNav" runat="server" ExpandDepth="0" ShowLines="True" EnableViewState="False" NodeIndent="10">
    <NodeStyle BorderStyle="None" ImageUrl="~/Images/TreeLineImages/plus.gif" />
    <ParentNodeStyle ImageUrl="~/Images/TreeLineImages/plus.gif" />
    <SelectedNodeStyle ImageUrl="~/Images/TreeLineImages/dashminus.gif" />
    <RootNodeStyle ImageUrl="~/Images/TreeLineImages/dashminus.gif" />
</asp:TreeView>
</form>
</body>
</html>