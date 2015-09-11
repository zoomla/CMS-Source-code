<%@ Page Language="C#" AutoEventWireup="true" validateRequest="false" CodeFile="SiteInfo.aspx.cs" Inherits="manage_Config_SiteInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>网站信息配置</title>
    <link href="../../App_Themes/AdminDefaultTheme/Guide.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/index.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/main.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="r_navigation">
		<div class="r_n_pic"></div>
		<span>后台管理</span>&gt;&gt;<span>系统设置</span> &gt;&gt;<span>网站信息配置</span>
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
        <table width="99%" cellspacing="1" cellpadding="0" class="border">
            <tr class="tdbg">
                <td class="tdbgleft" style="width: 200px">
                    <strong>网站名称：</strong></td>
                <td>
                    <asp:TextBox ID="TextBox1" runat="server" Width="210px"></asp:TextBox></td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft" style="width: 200px">
                    <strong>网站标题：</strong></td>
                <td>
                    <asp:TextBox ID="TextBox2" runat="server" Width="210px"></asp:TextBox></td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft" style="width: 200px">
                    <strong>网站地址：</strong></td>
                <td>
                    <asp:TextBox ID="TextBox3" runat="server" Width="210px"></asp:TextBox></td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft" style="width: 200px; ">
                    <strong>LOGO地址：</strong></td>
                <td>
                    <asp:TextBox ID="TextBox4" runat="server" Width="210px"></asp:TextBox></td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft" style="width: 200px; height: 23px;">
                    <strong>Banner地址：</strong></td>
                <td>
                    <asp:TextBox ID="TextBox5" runat="server" Width="210px"></asp:TextBox></td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft" style="width: 200px">
                    <strong>站长姓名：</strong></td>
                <td>
                    <asp:TextBox ID="TextBox6" runat="server" Width="210px" ></asp:TextBox></td>                    
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft" style="width: 200px">
                    <strong>站长信箱：</strong></td>
                <td>
                    <asp:TextBox ID="TextBox7" runat="server" Width="210px"></asp:TextBox></td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft" style="width: 200px; height: 68px;">
                    <strong>版权信息：</strong></td>
                <td>
                    <asp:TextBox ID="TextBox8" runat="server" TextMode="MultiLine" Rows="5" Columns="60"></asp:TextBox></td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft" style="width: 200px">
                    <strong>网站META关键词：</strong></td>
                <td>
                    <asp:TextBox ID="TextBox9" runat="server" TextMode="MultiLine" Rows="5" Columns="60"></asp:TextBox></td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft" style="width: 200px">
                    <strong>网站META网页描述：</strong></td>
                <td>
                    <asp:TextBox ID="TextBox10" runat="server" TextMode="MultiLine" Rows="5" Columns="60"></asp:TextBox></td>
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
