<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ThumbConfig.aspx.cs" Inherits="manage_Config_ThumbConfig" Title="Untitled Page" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>SiteInfo</title>
    <link href="../../App_Themes/AdminDefaultTheme/Guide.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/index.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/main.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="r_navigation">
		<div class="r_n_pic"></div>
		<span>后台管理</span>&gt;&gt;<span>系统设置</span> &gt;&gt;<span>缩略图参数配置</span>
	</div>
	<div class="clearbox"></div>
    <table width="99%" cellspacing="1" cellpadding="0" class="border" align="center">
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
    </table> <br />
                <table cellspacing="1" cellpadding="0" class="border">
                    <tr class="tdbg">
                        <td class="tdbgleft" style="width: 300px">
                            <strong>缩略图默认宽度：</strong></td>
                        <td style="width: 200px">
                            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr class="tdbg">
                        <td class="tdbgleft" style="width: 300px">
                            <strong>缩略图默认高度：</strong></td>
                        <td style="width: 100px">
                            <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr class="tdbg">
                        <td class="tdbgleft" style="width: 300px">
                            <strong>缩略图算法：</strong></td>
                        <td style="width: 500px">
                            <asp:RadioButtonList ID="RadioButtonList1" runat="server">
                                <asp:ListItem>常规算法：宽度和高度都大于0时，直接缩小成指定大小，其中一个为0时，按比例缩小</asp:ListItem>
                                <asp:ListItem>裁剪法：宽度和高度都大于0时，先按最佳比例缩小再裁剪成指定大小，其中一个为0时，按比例缩小。</asp:ListItem>
                                <asp:ListItem>补充法：在指定大小的背景图上附加上按最佳比例缩小的图片。 </asp:ListItem>
                            </asp:RadioButtonList></td>
                    </tr>
                    <tr class="tdbg">
                        <td class="tdbgleft" style="width: 300px">
                            <strong>缩略图底色：</strong></td>
                        <td style="width: 500px">
                            <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox></td>
                    </tr>
                </table>
        <br />
            
        <asp:Button ID="Button1" runat="server" Text="保存设置" OnClick="Button1_Click" /><br />
        <br />
        <br />
        <br />
        <br />
        <br />
        &nbsp;<br />
        <br />
        &nbsp;
    </form>
</body>
</html>

