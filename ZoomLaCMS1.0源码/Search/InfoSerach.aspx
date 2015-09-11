<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InfoSerach.aspx.cs" Inherits="ZoomLa.WebSite.InfoSerach" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>信息搜索</title>
    <link href="../App_Themes/UserDefaultTheme/Default.css" type="text/css" rel="stylesheet" />
    <link href="../App_Themes/UserDefaultTheme/xtree.css" type="text/css" rel="stylesheet" />
    <link href="../User/css/default1.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/checkarticle.gif" />站内搜索
        <asp:DropDownList ID="DDLtype" runat="server">            
            <asp:ListItem Value="1">图片标题</asp:ListItem>
            <asp:ListItem Value="2">录入者</asp:ListItem>
        </asp:DropDownList>
        <asp:DropDownList ID="DDLNode" runat="server">
        </asp:DropDownList>
        <asp:TextBox ID="TxtKeyword" runat="server">关键字</asp:TextBox>
        <asp:Button ID="Button1" runat="server" Text="搜  索" OnClick="Button1_Click" />
    </div>
    </form>
</body>
</html>
