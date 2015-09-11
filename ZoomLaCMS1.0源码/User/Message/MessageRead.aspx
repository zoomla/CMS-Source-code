<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MessageRead.aspx.cs" Inherits="ZoomLa.WebSite.User.MessageRead" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>会员中心 >> 阅读短消息</title>
    <link href="/App_Themes/UserDefaultTheme/Default.css" type="text/css" rel="stylesheet" />
    <link href="/App_Themes/UserDefaultTheme/xtree.css" type="text/css" rel="stylesheet" />
    <link href="../css/user.css" rel="stylesheet" type="text/css" />
    <link href="../css/default1.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">

<div>
    <div class="r_navigation">
    <div class="r_n_pic"></div>
    您现在的位置：<span id="YourPosition"><span><a title="网站首页" href="/"><asp:Label ID="LblSiteName" runat="server" Text="Label"></asp:Label></a></span><span> &gt;&gt; </span><span><a title="会员中心" href="/User/Default.aspx">会员中心</a></span><span> &gt;&gt; </span><span><a title="短消息管理" href="/User/Message/MessageRead.aspx">阅读短信</a></span></span></div>
    <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
        <tr align="center">
            <td colspan="2" class="spacingtitle">
                <b>会员短消息</b></td>
        </tr>
        <tr class="tdbg">
            <td align="left" style="height: 28px; width: 45%;">
                <strong>发件人：</strong>
                <asp:Label ID="LblSender" runat="server" Text="Label"></asp:Label></td>
        </tr>
        <tr class="tdbg">
            <td align="left" style="height: 28px; width: 45%;">
                <strong>收件人：</strong>
                <asp:Label ID="LblIncept" runat="server" Text="Label"></asp:Label></td>
        </tr>
        <tr class="tdbg">
            <td align="left" style="height: 28px; width: 45%;">
                <strong>发送时间：</strong>
                <asp:Label ID="LblSendTime" runat="server" Text="Label"></asp:Label></td>
        </tr>
        <tr class="tdbg">
            <td align="left" style="height: 28px; width: 45%;">
                <strong>消息主题：</strong>
                <asp:Label ID="LblTitle" runat="server" Text="Label"></asp:Label></td>
        </tr>
        <tr class="tdbg">
            <td align="left" style="height: 28px; width: 45%;">
                <strong>消息内容：</strong>
            </td>
        </tr>
        <tr class="tdbg">
            <td align="left" style="height: 28px; width: 45%;">
                <asp:Label ID="LblContent" runat="server" Text="Label"></asp:Label></td>
        </tr>
        <tr class="tdbg">
            <td align="left" style="height: 28px; width: 45%; text-align: center">
                <asp:Button ID="BtnDelete" runat="server" Text="删除" OnClick="BtnDelete_Click" OnClientClick="return confirm('是否要删除此短消息？')" />
                <asp:Button ID="BtnReply" runat="server" Text="回复" OnClick="BtnReply_Click" />                
                <asp:Button ID="BtnReturn" runat="server" OnClick="BtnReturn_Click" Text="返回" />
            </td>
        </tr>
    </table>
    </div>
    </form>
</body>
</html>
