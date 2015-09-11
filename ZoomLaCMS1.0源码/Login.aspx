<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>会员登录</title>
    <link href="../App_Themes/UserDefaultTheme/Default.css" type="text/css" rel="stylesheet" />
    <link href="/Skin/Default/user.css" rel="stylesheet" type="text/css" />
    <link href="/Skin/Default/default.css" rel="stylesheet" type="text/css" />
</head>
<body id="LoginStatusbody">
    <form id="form1" runat="server">
        <asp:Panel ID="PnlLogin" runat="server">
            <table cellspacing="0">
                <tr>
                    <td>
                        用户名：
                    </td>
                    <td>
                        <asp:TextBox ID="TxtUserName" MaxLength="20" Width="95" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        密　码：
                    </td>
                    <td>
                        <asp:TextBox ID="TxtPassword" runat="server" Width="95" TextMode="Password"></asp:TextBox>
                    </td>
                </tr>
                <asp:PlaceHolder ID="PhValCode" runat="server">
                <tr>
                    <td>
                        验证码：
                    </td>
                    <td>
                        <asp:TextBox ID="TxtValidateCode" MaxLength="6" Width="60" runat="server" onfocus="this.select();"></asp:TextBox> <asp:Image ID="VcodeLogin" runat="server" ImageUrl="~/Common/ValidateCode.aspx" Height="20px" />
                    </td>
                </tr>
                </asp:PlaceHolder>                
            </table>
            <asp:Button ID="BtnLogin" runat="server" Text="登录" OnClick="BtnLogin_Click" />&nbsp;&nbsp;
            <a href="User/Register.aspx" target="_top">注册</a>｜<a href="User/GetPassword.aspx" target="_top">忘记密码</a>
            <asp:RequiredFieldValidator ID="ValrUserName" runat="server" ErrorMessage="请输入用户名！"
                ControlToValidate="TxtUserName" Display="None" SetFocusOnError="True"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="ValrPassword" runat="server" ErrorMessage="请输入密码！"
                ControlToValidate="TxtPassword" Display="None" SetFocusOnError="True"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="ValrValidateCode" runat="server" ErrorMessage="请输入验证码！"
                ControlToValidate="TxtValidateCode" Display="None" SetFocusOnError="True"></asp:RequiredFieldValidator>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                ShowSummary="False" />
        </asp:Panel>
        <asp:Panel ID="PnlLoginStatus" runat="server">
            <div class="u_login">
                <asp:Literal ID="LitUserName" runat="server"></asp:Literal>，您好！<br />                
                <asp:Literal ID="LitMessage" runat="server">0</asp:Literal><br />
                <asp:Literal ID="LitLoginTime" runat="server">0</asp:Literal><br /> 
                <asp:Literal ID="LitLoginDate" runat="server">0</asp:Literal><br />               
                <div style="text-align: left">
                    <a href="User/Default.aspx" target="_top">会员中心</a>&nbsp;&nbsp;|&nbsp;&nbsp;<a href="User/Logout.aspx" target="_top">退出登录</a></div>
            </div>
        </asp:Panel>
        <asp:Panel ID="PnlLoginMessage" runat="server" Visible="false">
            <asp:Literal ID="LitErrorMessage" runat="server"></asp:Literal>
            <asp:Button ID="BtnReturn" runat="server" Text="返回" OnClick="BtnReturn_Click" />
        </asp:Panel>
    </form>
</body>
</html>
