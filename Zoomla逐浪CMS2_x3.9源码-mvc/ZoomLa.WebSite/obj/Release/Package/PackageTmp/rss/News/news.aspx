<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="news.aspx.cs" Inherits="ZoomLaCMS.rss.News.news" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div>标题：<asp:Label runat="server" ID="Title_L" /></div>
            <div>日期：<asp:Label runat="server" ID="CDate_L" /></div>
            <div runat="server" id="content_div">

            </div>
        </div>
    </form>
</body>
</html>

