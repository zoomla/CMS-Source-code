<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChangPSW.aspx.cs" Inherits="ZoomLa.WebSite.User.ChangPSW" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>会员中心 >> 修改密码</title>
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
    您现在的位置：<span id="YourPosition"><span><a title="网站首页" href="/"><asp:Label ID="LblSiteName" runat="server" Text="Label"></asp:Label></a></span><span> &gt;&gt; </span><span><a title="会员中心" href="/User/Default.aspx">会员中心</a></span><span> &gt;&gt; </span><span><a title="信息管理" href="/User/ChangPSW.aspx">修改密码</a></span></span></div>
        <!-- 网页内容 -->
    <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
        <tr align="center">
            <td colspan="2" class="spacingtitle">
                <strong>修改密码</strong>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" style="width: 50%" align="right">
                <strong>原 密 码：</strong>
            </td>
            <td align="left">
                <asp:TextBox ID="TxtOldPassword" runat="server" TextMode="Password" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="TxtOldPassword"
                    runat="server" ErrorMessage="原密码不能为空！" Display="Dynamic" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right">
                <strong>新 密 码：</strong>
            </td>
            <td align="left">
                <asp:TextBox ID="TxtPassword" runat="server" TextMode="Password" />
                <asp:RequiredFieldValidator ID="ValrUserPassword" ControlToValidate="TxtPassword"
                    runat="server" ErrorMessage="密码不能为空！" Display="Dynamic" />                
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right">
                <strong>确认密码：</strong>
            </td>
            <td align="left">
                <asp:TextBox ID="TxtPassword2" runat="server" TextMode="Password" />
                <asp:CompareValidator ID="CompareValidator1" ControlToValidate="TxtPassword2" ControlToCompare="TxtPassword"
                    ErrorMessage="两次输入的密码不一致！" runat="server" />
            </td>
        </tr>
        <tr class="tdbgbottom">
            <td colspan="2">
                <asp:Button ID="BtnSubmit" runat="server" Text="修改" OnClick="BtnSubmit_Click" />
                &nbsp;&nbsp;
                <asp:Button ID="BtnCancle" runat="server" Text="取消" OnClick="BtnCancle_Click" ValidationGroup="BtnCancel" />
            </td>
        </tr>
    </table>    
</div>
    </form>
</body>
</html>
