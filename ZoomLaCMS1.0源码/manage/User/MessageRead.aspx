<%@ Page Language="C#" Title="读取短消息" AutoEventWireup="true" CodeFile="MessageRead.aspx.cs" Inherits="User.MessageRead" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>读取短消息</title>
    <link href="../../App_Themes/AdminDefaultTheme/Guide.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/index.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/main.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div>

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