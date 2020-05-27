<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="showfriendsearch.aspx.cs" Inherits="ZoomLaCMS.Zone.showfriendsearch" %>

<!DOCTYPE HTML>
<html>
<head runat="server">
<title>添加好友</title>
<link rel="stylesheet" rev="stylesheet" href="../../css/subModal.css" type="text/css" media="all" />
<link href="../../App_Themes/UserThem/user_user.css" rel="stylesheet" type="text/css" />
</head>
<body>
<form id="form1" runat="server">
  <br />
  <br />
  <table width="100%" border="0" cellpadding="0" cellspacing="0">
    <tr>
      <td align="center"><table width="80%" border="0" cellpadding="0" cellspacing="0">
          <tr>
            <td rowspan="2" style="width: 110px;" align="right">&nbsp;</td>
            <td align="left">&nbsp;</td>
          </tr>
        </table>
        <table border="0" cellpadding="0" cellspacing="0" width="80%">
          <tr>
            <td> 选择好友分组：
              <asp:DropDownList ID="Friendg" runat="server" DataTextField="GroupName" DataValueField="ID"> </asp:DropDownList>
              <asp:Button ID="joinbtn" runat="server" Text="加为好友" OnClick="joinbtn_Click" />
              <asp:Button ID="btnCan" runat="server" Text="取消" OnClientClick='window.parent.hidePopWin(true);' /></td>
          </tr>
        </table></td>
    </tr>
  </table>
</form>
</body>
</html>
