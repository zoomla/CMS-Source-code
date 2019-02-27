<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GetPassword.aspx.cs" Inherits="ZoomLa.WebSite.User.User_GetPassword" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>获取密码</title>
    <link href="../App_Themes/UserDefaultTheme/Default.css" type="text/css" rel="stylesheet" />
    <link href="/Skin/Default/user.css" rel="stylesheet" type="text/css" />
    <link href="/Skin/Default/default.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:Panel ID="PnlStep1" runat="server" Visible="false">
            <asp:TextBox ID="TxtUserName" runat="server"></asp:TextBox><asp:RequiredFieldValidator ID="ValrTxtUserName" runat="server" ErrorMessage="请输入用户名！"
                ControlToValidate="TxtUserName" Display="dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>
            <asp:Button ID="BtnStep1" runat="server" Text="下一步" OnClick="BtnStep1_Click" />
        </asp:Panel>
        <asp:Panel ID="PnlStep2" runat="server" Visible="false">
            <asp:Literal ID="LitQuestion" runat="server"></asp:Literal>
            <asp:TextBox ID="TxtAnswer" runat="server"></asp:TextBox>
            <asp:TextBox ID="TxtValidateCode" runat="server"></asp:TextBox><asp:Image ID="VcodeLogin" runat="server" ImageUrl="~/Common/ValidateCode.aspx" Height="20px" />
            <asp:RequiredFieldValidator ID="ValrValidateCode" runat="server" ErrorMessage="请输入验证码！"
                ControlToValidate="TxtValidateCode" Display="dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>
            <asp:Button ID="BtnStep2" runat="server" Text="完成" OnClick="BtnStep2_Click" />
        </asp:Panel> 
        <asp:Panel ID="PnlStep3" runat="server" Visible="false">
            <asp:TextBox ID="TxtPassword" TextMode="Password" runat="server"></asp:TextBox>
            <asp:TextBox ID="TxtConfirmPassword" runat="server" TextMode="Password"></asp:TextBox><asp:CompareValidator
                ID="CompareValTxtConfirmPassword" ControlToValidate="TxtConfirmPassword" ControlToCompare="TxtPassword" Display="Dynamic" Type="String" Operator="Equal" runat="server" ErrorMessage="两次密码输入不一致！"></asp:CompareValidator>
            <asp:Button ID="BtnStep3" runat="server" Text="修改密码" OnClick="BtnStep3_Click" />
        </asp:Panel>       
    </div>
    </form>
</body>
</html>
