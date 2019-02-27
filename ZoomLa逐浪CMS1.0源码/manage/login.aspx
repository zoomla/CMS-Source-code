<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="ZoomLa.WebSite.Manage.login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>后台管理登陆</title>
    <link rel="stylesheet" type="text/css" href="../App_Themes/AdminDefaultTheme/style.css" />
    <script type="text/javascript">
    <!--
    if(self!=top){top.location=self.location;} 
    function ShowSoftKeyboard(obj)
    {
        if((typeof(CheckLoaded) == "function"))
        {
            password1 = obj;
            showkeyboard();
            Calc.password.value = '';
        }
        else
        {
            return false;
        }
    }
    // -->
    </script>
    <script type="text/javascript" src="../Manage/JS/softkeyboard.js"></script>
</head>
<body id="loginbody"style="background-color:#14BA4C">
    <form id="Login" runat="server" defaultbutton="IbtnEnter">
<table border="0" cellspacing="0" cellpadding="0" style="width:100%; height:100%">
  <tbody>
  <tr>
    <td style="height:119px;background:url(images/LoginImg/loginimg04.gif)">&nbsp;</td>
    <td style="height:119px;background:url(images/LoginImg/loginimg04.gif)">&nbsp;</td>
    <td style="height:119px;background:url(images/LoginImg/loginimg04.gif)">&nbsp;</td>
  </tr>
  <tr>
    <td style="height:78px; background:url(images/LoginImg/loginimg13.gif)">&nbsp;</td>
    <td style="height:78px; background:url(images/LoginImg/loginimg1.gif)">&nbsp;</td>
    <td style="height:78px; background:url(images/LoginImg/loginimg13.gif)">&nbsp;</td></tr>
  <tr>
    <td style="height:177px; background:url(images/LoginImg/loginimg06.gif)">&nbsp;</td>
    <td style="height:177px; background:url(images/LoginImg/loginimg2.gif); width:553px" valign="bottom">
      <table border="0" cellspacing="0" cellpadding="0" align="right" style="width: 297px">
        <tbody>
        <tr>
          <td align="right">
            <table border="0" cellspacing="0" cellpadding="3" style="width: 220px">
              <tbody>
              <tr>
                <td>&nbsp;</td>
                <td align="left"><img src="images/LoginImg/loginimg.gif" alt="" width="130" height="5" /></td></tr>
              <tr>
                <td align="right"><img src="images/LoginImg/name.gif" width="53" height="19" alt="" /></td>
                <td align="left"> <asp:TextBox ID="TxtUserName" Width="140px" Height="12px" MaxLength="20" runat="server" CssClass="boxcontent" ></asp:TextBox></td>
              </tr>
              <tr>
                <td align="right"><img src="images/LoginImg/password.gif" width="53" height="19" alt="" /></td>
                <td align="left"><asp:TextBox ID="TxtPassword" Width="140px" Height="12px" MaxLength="20" TextMode="password" runat="server" CssClass="boxcontent"></asp:TextBox></td>
              </tr>
              <tr id="LiSiteManageCode" runat="server">
                <td align="right"><img src="images/LoginImg/safety.gif" width="53" height="19" alt="" /></td>
                <td align="left"><asp:TextBox ID="TxtAdminValidateCode" Width="140px" Height="12px" MaxLength="20" TextMode="Password" runat="server" CssClass="boxcontent"></asp:TextBox></td>
              </tr>
              <tr>
                <td align="right"><img src="images/LoginImg/validate.gif" width="53" height="19" alt="" /></td>
                <td align="left">
                  <table border="0" cellspacing="0" cellpadding="0">
                    <tbody>
                    <tr>
                      <td> 
                      <asp:TextBox ID="TxtValidateCode"  Width="67px" MaxLength="20" runat="server" CssClass="boxcontent2"></asp:TextBox></td>
                      <td width="3"></td>
                      <td><asp:Image ID="VcodeLogin" runat="server" ImageUrl="~/Common/ValidateCode.aspx" Height="20px" /></td>
                    </tr>
                    </tbody>
                  </table>
                </td>
              </tr>
              </tbody>
            </table>
          </td>
          <td valign="bottom" width="5">&nbsp;</td>
          <td valign="bottom" width="65" align="left">
          <asp:ImageButton ID="IbtnEnter" ImageUrl="images/LoginImg/loginimg12.gif" runat="server" Style="cursor:hand;" OnClick="IbtnEnter_Click" />
          </td>
          </tr>
        <tr>
          <td height="21">&nbsp;</td>
          <td height="21" width="10">&nbsp;</td>
          <td height="21" width="60">&nbsp;</td></tr>
        <tr>
          <td colspan="3">
            <table border="0" cellspacing="0" cellpadding="0" align="right">
              <tbody>
              <tr>
                <td valign="bottom">
                <img src="images/LoginImg/login.gif" width="113" height="19" alt="" /></td>
                <td width="15">&nbsp;</td>
                <td valign="bottom"></td>
                <td width="1"></td>
              </tr>
              </tbody>
            </table>
          </td>
        </tr>
     </tbody>
    </table>
    </td>
    <td style="height:177px; background:url(images/LoginImg/loginimg06.gif)">&nbsp;</td></tr>
  <tr>
    <td style="height:154px; background:url(images/LoginImg/loginimg07.gif)">&nbsp;</td>
    <td style="height:154px; background:url(images/LoginImg/loginimg03.gif); width:553px">&nbsp;</td>
    <td style="height:154px; background:url(images/LoginImg/loginimg07.gif)">&nbsp;</td></tr>
  <tr>
    <td style="height:59px; background:url(images/LoginImg/loginimg05.gif)">&nbsp;</td>
    <td style="height:59px; background:url(images/LoginImg/loginimg05.gif)">&nbsp;</td>
    <td style="height:59px; background:url(images/LoginImg/loginimg05.gif)">&nbsp;</td></tr>
  <tr>
    <td style="background:url(images/LoginImg/loginimg14.gif); height: 19px;">&nbsp;</td>
    <td style="background:url(images/LoginImg/loginimg14.gif); height: 19px;">&nbsp;</td>
    <td style="background:url(images/LoginImg/loginimg14.gif); height: 19px;">&nbsp;</td></tr></tbody></table>
 <asp:RequiredFieldValidator ID="ValrUserName" runat="server" ErrorMessage="请输入用户名！"
            ControlToValidate="TxtUserName" Display="None" SetFocusOnError="True"></asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator ID="ValrPassword" runat="server" ErrorMessage="请输入密码！"
            ControlToValidate="TxtPassword" Display="None" SetFocusOnError="True"></asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator ID="ValrAdminValidateCode" runat="server" ErrorMessage="请输入管理认证码！"
            ControlToValidate="TxtAdminValidateCode" Display="None" SetFocusOnError="True"></asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator ID="ValrValidateCode" runat="server" ErrorMessage="请输入验证码！"
            ControlToValidate="TxtValidateCode" Display="None" SetFocusOnError="True"></asp:RequiredFieldValidator>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
            ShowSummary="False" /></form>
</body>
</html>