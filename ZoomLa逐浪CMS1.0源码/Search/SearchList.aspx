<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SearchList.aspx.cs" Inherits="ZoomLa.WebSite.SearchList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>搜索结果</title>
    <link href="../App_Themes/UserDefaultTheme/Default.css" type="text/css" rel="stylesheet" />
    <link href="../App_Themes/UserDefaultTheme/xtree.css" type="text/css" rel="stylesheet" />
    <link href="../User/css/commentary.css" rel="stylesheet" type="text/css" />
    <link href="../User/css/default1.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <div class="r_navigation">
        <div class="r_n_pic"></div>
        您现在的位置：<span id="YourPosition"><span><a title="网站首页" href="/">3KCN</a></span><span> &gt;&gt; </span><span>图片搜索</span></span>
    </div>
    <div class="clearbox"></div>
    <div class="r_navigation">
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
    <div class="clearbox"></div>
    <div>搜索结果：</div>
    <asp:Repeater ID="Repeater1" runat="server">
        <ItemTemplate>
        <div><a href="<%# GetUrl(Eval("GeneralID","{0}")) %>"><%# Eval("Title") %></a></div>
        <div class="clearbox"></div>
        </ItemTemplate>
    </asp:Repeater>
    <div id="Pager1" runat="server"></div>    
    </div>
    </form>
</body>
</html>
