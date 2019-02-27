<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ZoneFriendSearch.aspx.cs" Inherits="Zone_ZoneFriendSearch" EnableViewStateMac="false" %>
<!DOCTYPE HTML>
<html>
<head runat="server">
<title>好友查找</title>
<link href="../../App_Themes/UserThem/user_user.css" rel="stylesheet" type="text/css" />
</head>
<body>
<form id="form1" runat="server">
  <div>
    <table>
      <tr>
        <td> 我要寻找</td>
      </tr>
      <tr>
        <td><asp:RadioButtonList ID="RadioButtonList1" name="RadioButtonList1" runat="server"
					RepeatDirection="Horizontal">
            <asp:ListItem>男生</asp:ListItem>
            <asp:ListItem Selected="True">女生</asp:ListItem>
          </asp:RadioButtonList></td>
      </tr>
      <tr>
        <td>地区 </td>
      </tr>
      <tr>
        <td><asp:DropDownList ID="DropDownList3" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged"> </asp:DropDownList></td>
      </tr>
      <tr>
        <td><asp:DropDownList ID="DropDownList4" runat="server" Visible="false"> </asp:DropDownList></td>
      </tr>
      <tr>
        <td>年龄 </td>
      </tr>
      <tr>
        <td><asp:TextBox ID="TextBox1" runat="server" Width="30px"></asp:TextBox>
          ～
          <asp:TextBox ID="TextBox2" runat="server" Width="30px"></asp:TextBox>
          岁 </td>
      </tr>
      <tr>
        <td align="center"><asp:Button ID="Button1" runat="server" Text="立即寻找" OnClick="Button1_Click" /></td>
      </tr>
    </table>
  </div>
</form>
</body>
</html>