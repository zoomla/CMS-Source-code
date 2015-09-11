<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MyInfo.aspx.cs" Inherits="ZoomLa.WebSite.User.MyInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>我的信息</title>
    <link href="../App_Themes/UserDefaultTheme/Default.css" type="text/css" rel="stylesheet" />
    <link href="../App_Themes/UserDefaultTheme/xtree.css" type="text/css" rel="stylesheet" />
    <link href="css/user.css" rel="stylesheet" type="text/css" />
    <link href="css/default1.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <div class="r_navigation">
    <div class="r_n_pic"></div>
    您现在的位置：<span id="YourPosition"><span><a title="网站首页" href="/"><asp:Label ID="LblSiteName" runat="server" Text="Label"></asp:Label></a></span><span> &gt;&gt; </span><span><a title="会员中心" href="/User/Default.aspx">会员中心</a></span><span> &gt;&gt; </span><span><a title="信息管理" href="/User/MyInfo.aspx">个人信息</a></span></span></div>
    <table width="100%" border="0" cellpadding="5" cellspacing="1" class="border" style="text-align: left;">
                <tr class="tdbg">
                    <td style="width: 100%;" valign="top">
                        <table cellpadding="2" cellspacing="1" style="width: 100%; background-color: white;">
                            <tbody id="Tabs0">
                                <tr class="tdbg">
                                    <td style="width: 15%" align="right" class="tdbgleft">
                                        用 户 名：</td>
                                    <td style="width: 30%">
                                        <asp:Label ID="LblUserName" runat="server" Text="" /></td>
                                    <td style="width: 15%" align="right" class="tdbgleft">
                                        邮箱地址：</td>
                                    <td style="width: 40%">
                                        <asp:Label ID="LblEmail" runat="server" Text="" /></td>
                                </tr>
                                <tr class="tdbg">
                                    <td align="right" class="tdbgleft">
                                        会员组别：</td>
                                    <td>
                                        <asp:Label ID="LblGroupName" runat="server" Text="" /></td>
                                    <td align="right" class="tdbgleft">
                                        待阅短信：</td>
                                    <td>
                                        <asp:Label ID="LblUnreadMsg" runat="server" Text="" />
                                        条</td>
                                </tr>                                
                                <tr class="tdbg">
                                    <td align="right" class="tdbgleft">
                                        注册日期：</td>
                                    <td><asp:Label ID="LblRegTime" runat="server" /></td>
                                    <td align="right" class="tdbgleft">
                                        登录次数：</td>
                                    <td>
                                        <asp:Label ID="LblLoginTimes" runat="server" /></td>
                                </tr>
                                <tr class="tdbg">
                                    <td align="right" class="tdbgleft">
                                        最近修改密码时间：</td>
                                    <td>
                                        <asp:Label ID="LblChgPswTime" runat="server" /></td>
                                    <td align="right" class="tdbgleft">
                                        上次被锁定时间：</td>
                                    <td><asp:Label ID="LblLastLockTime" runat="server" /></td>
                                </tr>
                                <tr class="tdbg">
                                    <td align="right" class="tdbgleft">
                                        最后登录时间：</td>
                                    <td>
                                        <asp:Label ID="LblLastLoginTime" runat="server" /></td>
                                    <td align="right" class="tdbgleft">
                                        最后登录IP：</td>
                                    <td>
                                        <asp:Label ID="LblLastLoginIP" runat="server" /></td>
                                </tr>
                            </tbody>                                                 
                        </table>
                    </td>
                </tr>
            </table>
    </div>
    </form>
</body>
</html>
