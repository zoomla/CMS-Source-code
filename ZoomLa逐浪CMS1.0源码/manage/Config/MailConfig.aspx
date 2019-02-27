<%@ Page Language="C#" AutoEventWireup="true" validateRequest="false" CodeFile="MailConfig.aspx.cs" Inherits="manage_Config_MailConfig" Title="Untitled Page" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>邮件参数</title>
    <link href="../../App_Themes/AdminDefaultTheme/Guide.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/index.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/main.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="r_navigation">
		<div class="r_n_pic"></div>
		<span>后台管理</span>&gt;&gt;<span>系统设置</span> &gt;&gt;<span>邮件参数配置</span>
	</div>
    <div class="clearbox"></div>
    <table width="100%" cellspacing="1" cellpadding="0" class="border" align="center">
    <tr class="wzlist">
        <td align="center" id="Nav">
            <a href="SiteInfo.aspx">网站信息</a></td>
        <td align="center" id="Td1">
            <a href="SiteOption.aspx">网站参数</a></td>
        <td align="center" id="Td3">
            <a href="UserConfig.aspx">用户参数</a></td>
        <td align="center" id="Td5">
            <a href="MailConfig.aspx">邮件参数</a></td>
        <td align="center" id="Td4">
            <a href="ThumbConfig.aspx">缩略图参数</a></td>
        <td align="center" id="Td2">
            <a href="IPLockConfig.aspx">IP访问限定</a></td>
    </tr>
    </table>
    <div class="clearbox"></div> 
    <table cellspacing="1" cellpadding="0" width="100%" class="border">
        <tr class="tdbg">
            <td class="tdbgleft" style="width: 200px">
                <strong>发送人邮箱：</strong></td>
            <td style="width: 500px">
                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" style="width: 200px">
                <strong>发送邮件服务器(SMTP)：</strong></td>
            <td style="width: 500px">
                <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" style="width: 200px">
                <strong>端口号：</strong></td>
            <td style="width: 500px">
                <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" style="width: 200px">
                <strong>此服务器要求安全连接(SSL)：</strong></td>
            <td style="width: 500px">
                <asp:CheckBox ID="CheckBox1" runat="server" Checked="True" /></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" style="width: 200px">
                <strong>身份验证方式：</strong></td>
            <td style="width: 500px; height: 23px;">
                <asp:RadioButton ID="RadioButton1" runat="server" GroupName="AuthenticationType"/>无<br />
                <asp:RadioButton ID="RadioButton2" runat="server" GroupName="AuthenticationType"/>基本<br />
                    如果您的电子邮件服务器要求在发送电子邮件时显式传入用户名和密码，请选择此选项。
                    <br />发件人的用户名：<asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
                    <br />发件人的密&nbsp;&nbsp;码：<asp:TextBox ID="TextBox6" runat="server" TextMode="Password"></asp:TextBox>
                    <br />
                <asp:RadioButton ID="RadioButton3" runat="server" GroupName="AuthenticationType"/>NTLM (Windows 身份验证)
                    <br/>如果您的电子邮件服务器位于局域网上，并且您使用 Windows 凭据连接到该服务器，请选择此选项。
           </td>
        </tr>
    </table>
    <div class="clearbox"></div>        
    <asp:Button ID="Button1" runat="server" Text="保存设置" OnClick="Button1_Click" />
    </form>
</body>
</html>

