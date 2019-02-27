<%@ Page Language="C#" AutoEventWireup="true" validateRequest="false" CodeFile="UserConfig.aspx.cs" Inherits="manage_Config_UserConfig" Title="Untitled Page" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>用户参数</title>
    <link href="../../App_Themes/AdminDefaultTheme/Guide.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/index.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/main.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="r_navigation">
		<div class="r_n_pic"></div>
		<span>后台管理</span>&gt;&gt;<span>系统设置</span> &gt;&gt;<span>用户参数配置</span>
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
                <table cellspacing="1" width="100%" cellpadding="0" class="border">
                    <tr class="tdbg">
                        <td class="tdbgleft" style="width: 300px">
                            <strong>是否开启会员注册：</strong></td>
                        <td>
                            <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem>是</asp:ListItem>
                                <asp:ListItem>否</asp:ListItem>
                            </asp:RadioButtonList></td>
                    </tr>
                    <tr class="tdbg">
                        <td class="tdbgleft" style="width: 300px">
                            <strong>是否开启Email注册：</strong></td>
                        <td>
                            <asp:RadioButtonList ID="RadioButtonList2" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem>是</asp:ListItem>
                                <asp:ListItem>否</asp:ListItem>
                            </asp:RadioButtonList></td>
                    </tr>
                    <tr class="tdbg">
                        <td class="tdbgleft" style="width: 300px">
                            <strong>是否开启管理员注册：</strong></td>
                        <td>
                            <asp:RadioButtonList ID="RadioButtonList3" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem>是</asp:ListItem>
                                <asp:ListItem>否</asp:ListItem>
                            </asp:RadioButtonList></td>
                    </tr>
                    <tr class="tdbg">
                        <td class="tdbgleft" style="width: 300px">
                            <strong>是否允许一个Email注册多个会员：</strong></td>
                        <td>
                            <asp:RadioButtonList ID="RadioButtonList4" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem>是</asp:ListItem>
                                <asp:ListItem>否</asp:ListItem>
                            </asp:RadioButtonList></td>
                    </tr>
                    <tr class="tdbg">
                        <td class="tdbgleft" style="width: 300px">
                            <strong>会员注册时是否启用验证码功能：<br />
                            </strong>启用验证码功能可以在一定程度上防止暴力营销软件或注册机自动注册。</td>
                        <td>
                            <asp:RadioButtonList ID="RadioButtonList5" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem>是</asp:ListItem>
                                <asp:ListItem>否</asp:ListItem>
                            </asp:RadioButtonList></td>
                    </tr>
                    <tr class="tdbg">
                        <td class="tdbgleft" style="width: 300px">
                            <strong>新会员注册时用户名最少字符数：</strong></td>
                        <td>
                            <asp:TextBox ID="TextBox6" runat="server" ></asp:TextBox>个字符</td>
                    </tr>
                    <tr class="tdbg">
                        <td class="tdbgleft" style="width: 300px">
                            <strong>新会员注册时用户名最多字符数：</strong></td>
                        <td>
                            <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox>个字符</td>
                    </tr>
                    <tr class="tdbg">
                        <td class="tdbgleft" style="width: 300px">
                            <strong>禁止注册的用户名：<br />
                            </strong>在右边指定的用户名将被禁止注册，每个用户名请用“|”符号分隔</td>
                        <td>
                            <asp:TextBox ID="TextBox8" runat="server" Rows="6" TextMode="MultiLine" Width="50%"></asp:TextBox></td>
                    </tr>
                    <tr class="tdbg">
                        <td class="tdbgleft" style="width: 300px">
                            <strong>会员登陆是否启用验证码功能：<br />
                            </strong>启用验证码功能可以在一定程度上防止会员密码被暴力破解</td>
                        <td>
                            <asp:RadioButtonList ID="RadioButtonList6" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem>是</asp:ListItem>
                                <asp:ListItem>否</asp:ListItem>
                            </asp:RadioButtonList></td>
                    </tr>
                    <tr class="tdbg">
                        <td class="tdbgleft" style="width: 300px">
                            <strong>会员登录否允许多人同时使用同一会员帐号：</strong></td>
                        <td>
                            <asp:RadioButtonList ID="RadioButtonList7" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem>是</asp:ListItem>
                                <asp:ListItem>否</asp:ListItem>
                            </asp:RadioButtonList></td>
                    </tr>
                    <tr class="tdbg">
                        <td class="tdbgleft" style="width: 300px">
                            <strong>会员找回码的方式：</strong></td>
                        <td>
                            <asp:RadioButtonList ID="RadioButtonList8" runat="server">
                                <asp:ListItem>回答正确密码答案后，直接在页面修改密码</asp:ListItem>
                                <asp:ListItem>回答正确密码答案后，发送邮件到会员邮箱（必须在网站信息配置配置邮件服务器与会员注册时填写了邮件地址！）</asp:ListItem>
                            </asp:RadioButtonList></td>
                    </tr>
                    <tr class="tdbg">
                        <td class="tdbgleft" style="width: 300px">
                            <strong>新会员注时发送的验证邮件内容：<br />
                            </strong>邮件内容支持HTML，邮件内容中可用标签说明如下：<br />
                            <span onclick="Insert('{$CheckNum}')" style="cursor: hand">{$CheckNum}</span>：验证码<br />
                            <span onclick="Insert('{$CheckUrl}')" style="cursor: hand">{$CheckUrl}</span>：验证地址</td>
                        <td>
                            <asp:TextBox ID="TextBox12" runat="server" Rows="6" TextMode="MultiLine" Width="50%"></asp:TextBox></td>
                    </tr>
                </table>
        <br />
            
        <asp:Button ID="Button1" runat="server" Text="保存设置" OnClick="Button1_Click" /><br />
        <br />
        <br />        
        <br />
        &nbsp;
    </form>
</body>
</html>

