<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ModifyPassword.aspx.cs" Inherits="ZoomLa.WebSite.ModifyPassword" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>修改密码</title>
    <link href="../../App_Themes/AdminDefaultTheme/Guide.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/index.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/main.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
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
            <td>
                <asp:TextBox ID="TxtOldPassword" runat="server" TextMode="Password" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="TxtOldPassword"
                    runat="server" ErrorMessage="原密码不能为空！" Display="Dynamic" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right">
                <strong>新 密 码：</strong>
            </td>
            <td>
                <asp:TextBox ID="TxtPassword" runat="server" TextMode="Password" />
                <asp:RequiredFieldValidator ID="ValrUserPassword" ControlToValidate="TxtPassword"
                    runat="server" ErrorMessage="新密码不能为空！" Display="Dynamic" />
                <asp:RegularExpressionValidator ID="RegularExpressionValidatorPassword" runat="server"
                            ControlToValidate="TxtPassword" SetFocusOnError="false" Display="None" ValidationExpression="[\S]{6,}"
                            ErrorMessage="密码至少6位"></asp:RegularExpressionValidator>                
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right">
                <strong>确认密码：</strong>
            </td>
            <td>
                <asp:TextBox ID="TxtPassword2" runat="server" TextMode="Password" />
                <asp:CompareValidator ID="CompareValidator1" ControlToValidate="TxtPassword2" ControlToCompare="TxtPassword"
                    ErrorMessage="新密码和确认密码不一致！" runat="server" />
            </td>
        </tr>
        <tr class="tdbgbottom">
            <td colspan="2">
                <asp:Button ID="BtnSubmit" runat="server" Text="修改" OnClick="BtnSubmit_Click" />
                &nbsp;&nbsp;
                <asp:Button ID="BtnCancle" runat="server" Text="重写" OnClick="BtnCancle_Click" ValidationGroup="BtnCancel" />
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                            ShowSummary="False" />
            </td>
        </tr>
    </table>
    </div>
    </form>
</body>
</html>
