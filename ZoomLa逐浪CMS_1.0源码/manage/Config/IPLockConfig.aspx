<%@ Page Language="C#" AutoEventWireup="true" validateRequest="false" CodeFile="IPLockConfig.aspx.cs" Inherits="manage_Config_IPLockConfig" Title="Untitled Page" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>IP访问限定</title>
    <link href="../../App_Themes/AdminDefaultTheme/Guide.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/index.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/main.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="r_navigation">
		<div class="r_n_pic"></div>
		<span>后台管理</span>&gt;&gt;<span>系统设置</span> &gt;&gt;<span>IP访问限定</span>
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
    <table cellspacing="1" cellpadding="0" class="border">
        <tr class="tdbg">
            <td class="tdbgleft" style="width: 300px">
                <strong>全站来访限定方式：</strong></td>
            <td>
                <asp:RadioButton ID="RadioButton1" runat="server" GroupName="LockIPType" TabIndex="1" />不启用来访限定功能，任何IP都可以访问本站。<br />
                <asp:RadioButton ID="RadioButton2" runat="server" GroupName="LockIPType" TabIndex="2"/>仅仅启用白名单，只允许白名单中的IP访问本站。<br />
                <asp:RadioButton ID="RadioButton3" runat="server" GroupName="LockIPType" TabIndex="3"/>仅仅启用黑名单，只禁止黑名单中的IP访问本站。<br />
                <asp:RadioButton ID="RadioButton4" runat="server" GroupName="LockIPType" TabIndex="4"/>同时启用白名单与黑名单，先判断IP是否在白名单中，如果不在，则禁止访问；如果在则再判断是否在黑名单中，如果IP在黑名单中则禁止访问，否则允许访问。<br />
                <asp:RadioButton ID="RadioButton5" runat="server" GroupName="LockIPType" TabIndex="5"/>同时启用白名单与黑名单，先判断IP是否在黑名单中，如果不在，则允许访问；如果在则再判断是否在白名单中，如果IP在白名单中则允许访问，否则禁止访问。</td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" style="width: 300px">
                <strong>全站IP段白名单：</strong></td>
            <td>
                <asp:TextBox ID="TextBox2" runat="server" Rows="6" TextMode="MultiLine" Width="50%"></asp:TextBox></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" style="width: 300px">
                <strong>全站IP段黑名单：</strong></td>
            <td>
                <asp:TextBox ID="TextBox3" runat="server" Rows="6" TextMode="MultiLine" Width="50%"></asp:TextBox></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" style="width: 300px">
                <strong>后台来访限定方式：</strong></td>
            <td>
                <asp:RadioButton ID="RadioButton6" runat="server" GroupName="AdminLockIPType" TabIndex="1" />不启用来访限定功能，任何IP都可以访问本站。<br />
                <asp:RadioButton ID="RadioButton7" runat="server" GroupName="AdminLockIPType" TabIndex="2"/>仅仅启用白名单，只允许白名单中的IP访问本站。<br />
                <asp:RadioButton ID="RadioButton8" runat="server" GroupName="AdminLockIPType" TabIndex="3"/>仅仅启用黑名单，只禁止黑名单中的IP访问本站。<br />
                <asp:RadioButton ID="RadioButton9" runat="server" GroupName="AdminLockIPType" TabIndex="4"/>同时启用白名单与黑名单，先判断IP是否在白名单中，如果不在，则禁止访问；如果在则再判断是否在黑名单中，如果IP在黑名单中则禁止访问，否则允许访问。<br />
                <asp:RadioButton ID="RadioButton10" runat="server" GroupName="AdminLockIPType" TabIndex="5"/>同时启用白名单与黑名单，先判断IP是否在黑名单中，如果不在，则允许访问；如果在则再判断是否在白名单中，如果IP在白名单中则允许访问，否则禁止访问。</td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" style="width: 300px">
                <strong>后台IP段白名单：</strong></td>
            <td>
                <asp:TextBox ID="TextBox5" runat="server" Rows="6" TextMode="MultiLine" Width="50%"></asp:TextBox></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" style="width: 300px">
                <strong>后台IP段黑名单：</strong></td>
            <td>
                <asp:TextBox ID="TextBox6" runat="server" Rows="6" TextMode="MultiLine" Width="50%" ></asp:TextBox></td>
        </tr>
        
    </table>
        <br />            
        <asp:Button ID="Button1" runat="server" Text="保存设置" OnClick="Button1_Click" /><br />
    </form>
</body>
</html>

