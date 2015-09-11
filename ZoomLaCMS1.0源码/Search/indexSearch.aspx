<%@ Page Language="C#" AutoEventWireup="true" CodeFile="indexSearch.aspx.cs" Inherits="ZoomLa.WebSite.indexSearch" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>信息搜索</title>
<link href="/Skin/style.css" type="text/css" rel="stylesheet" />
</head>
<body style="background-image:url(../skin/default/s_bg.jpg)">
    <form id="form1" runat="server">
    <div style="height:20px;line-height: 20px;">
        <asp:DropDownList ID="DDLtype" runat="server" Height="20px">            
            <asp:ListItem Value="1">信息标题</asp:ListItem>
            <asp:ListItem Value="2">录入者</asp:ListItem>
        </asp:DropDownList>
        <asp:DropDownList ID="DDLNode" runat="server" Height="20px">
        </asp:DropDownList>
        <asp:TextBox ID="TxtKeyword" runat="server" Height="13px" Width="200px">关键字</asp:TextBox>
        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="../skin/default/s_bottom.jpg" OnClick="ImageButton1_Click" Height="20px" /></div>
    </form>
</body>
</html>
