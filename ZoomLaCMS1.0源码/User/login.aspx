<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="ZoomLa.WebSite.User.User_login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>会员登录</title>
    <link href="../App_Themes/UserDefaultTheme/Default.css" type="text/css" rel="stylesheet" />
    <link href="../App_Themes/UserDefaultTheme/xtree.css" type="text/css" rel="stylesheet" />
    <link href="css/User_Login.css" rel="stylesheet" type="text/css" />
</head>
<body id="userlogin_body">
    <form id="Login" runat="server" defaultfocus="TxtUserName" defaultbutton="IbtnEnter">
        <div id="user_login">
            <dl>
                <dd id="user_top">
                    <ul>
                        <li class="user_top_l"></li>
                        <li class="user_top_c"></li>
                        <li class="user_top_r"></li>
                    </ul>
                </dd>
                <dd id="user_main">
                    <ul>
                        <li class="user_main_l"></li>
                        <li class="user_main_c">
                            <div class="user_main_box">
                                <ul>
                                    <li class="user_main_text">用户名： </li>
                                    <li class="user_main_input">
                                        <asp:TextBox ID="TxtUserName" CssClass="TxtUserNameCssClass" MaxLength="20" runat="server"></asp:TextBox>
                                    </li>
                                </ul>
                                <ul>
                                    <li class="user_main_text">密 码： </li>
                                    <li class="user_main_input">
                                        <asp:TextBox ID="TxtPassword" runat="server" CssClass="TxtPasswordCssClass" TextMode="Password"></asp:TextBox></li>
                                </ul>
                                <asp:PlaceHolder ID="PhValCode" runat="server">
                                    <ul>
                                        <li class="user_main_text">验证码： </li>
                                        <li class="user_main_input">
                                            <asp:TextBox ID="TxtValidateCode" CssClass="TxtValidateCodeCssClass" MaxLength="6"
                                                runat="server" onfocus="this.select();"></asp:TextBox>
                                            <asp:Image ID="VcodeLogin" runat="server" ImageUrl="~/Common/ValidateCode.aspx" Height="20px" />
                                        </li>
                                    </ul>
                                </asp:PlaceHolder>
                                <ul>
                                    <li class="user_main_text">Cookie： </li>
                                    <li class="user_main_input">
                                        <asp:DropDownList ID="DropExpiration" runat="server">
                                            <asp:ListItem Value="None" Text="不保存"></asp:ListItem>
                                            <asp:ListItem Value="Day" Text="保存一天"></asp:ListItem>
                                            <asp:ListItem Value="Month" Text="保存一月"></asp:ListItem>
                                            <asp:ListItem Value="Yaer" Text="保存一年"></asp:ListItem>
                                        </asp:DropDownList></li>
                                </ul>
                            </div>
                        </li>
                        <li class="user_main_r">
                            <asp:ImageButton ID="IbtnEnter" ImageUrl="~/User/Images/user_botton.gif" runat="server"
                                CssClass="IbtnEnterCssClass" OnClick="IbtnEnter_Click" /></li>
                    </ul>
                </dd>
                <dd id="user_bottom">
                    <ul>
                        <li class="user_bottom_l"></li>
                        <li class="user_bottom_c"><span style="margin-top: 40px;">如果您尚未在本站注册为用户，请先点此 <a href="Register.aspx">
                            注册</a> 。</span> </li>
                        <li class="user_bottom_r"></li>
                    </ul>
                </dd>
            </dl>
        </div>
        <asp:RequiredFieldValidator ID="ValrUserName" runat="server" ErrorMessage="请输入用户名！"
            ControlToValidate="TxtUserName" Display="None" SetFocusOnError="True"></asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator ID="ValrPassword" runat="server" ErrorMessage="请输入密码！"
            ControlToValidate="TxtPassword" Display="None" SetFocusOnError="True"></asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator ID="ValrValidateCode" runat="server" ErrorMessage="请输入验证码！"
            ControlToValidate="TxtValidateCode" Display="None" SetFocusOnError="True"></asp:RequiredFieldValidator>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
            ShowSummary="False" />
    </form>
</body>
</html>