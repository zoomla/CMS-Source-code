<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MisAttendanceInfo.aspx.cs" Inherits="MIS_MisAttendanceInfo" %>
<!DOCTYPE html>
<html>
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<title>考勤统计</title>
<link href="/App_Themes/User.css" type="text/css" rel="stylesheet" />
<style type="text/css">
.auto-style1 { width: 112px; }
</style>
</head>
<body>
<form id="form1" runat="server">
  <div>
    <div class="CheckTime">
      <asp:DropDownList ID="Year" runat="server">
        <asp:ListItem Value="2013">2013年</asp:ListItem>
        <asp:ListItem Value="2012">2012年</asp:ListItem>
        <asp:ListItem Value="2011">2011年</asp:ListItem>
      </asp:DropDownList>
      <asp:DropDownList ID="month" runat="server">
        <asp:ListItem Value="1">一月</asp:ListItem>
        <asp:ListItem Value="2">二月</asp:ListItem>
        <asp:ListItem Value="3">三月</asp:ListItem>
        <asp:ListItem Value="4">四月</asp:ListItem>
        <asp:ListItem Value="5">五月</asp:ListItem>
        <asp:ListItem Value="6">六月</asp:ListItem>
        <asp:ListItem Value="7">七月</asp:ListItem>
        <asp:ListItem Value="8">八月</asp:ListItem>
        <asp:ListItem Value="9">九月</asp:ListItem>
        <asp:ListItem Value="10">十月</asp:ListItem>
        <asp:ListItem Value="11">十一月</asp:ListItem>
        <asp:ListItem Value="12">十二月</asp:ListItem>
      </asp:DropDownList>
    </div>
    <div class="HeadCount"> </div>
    <div class="TabShow">
      <table style="width:700px;">
        <tr class="TrInfo">
          <td class="tdName" align="center">姓名</td>
          <td class="tdDepart" align="center">部门</td>
          <td class="tdRel" align="center">实际出勤/次</td>
          <td class="tdNowork" align="center">旷工/次</td>
          <td class="tdLate" align="center">迟到/次</td>
          <td class="tdEarly" align="center">早退/次</td>
          <td class="tdLose" align="center">缺卡/次</td>
          <td class="tdLates" align="center">晚卡/次</td>
        </tr>
        <asp:Repeater ID="repShow" runat="server">
          <ItemTemplate>
            <tr class="TrInfo" onmouseover="this.style.backgroundColor='#eaeae8'" onmouseout="this.style.backgroundColor='#fff'">
              <td align="center"><%#Eval("UserName")%></td>
              <td align="center"></td>
              <td align="center"><%#CheckDays(Eval("UserName").ToString())%></td>
              <td align="center"><%#CheckNoWork(Eval("UserName").ToString())%></td>
              <td align="center"><%#CheckLate(Eval("UserName").ToString())%></td>
              <td align="center"><%#CheckLeave(Eval("UserName").ToString())%></td>
              <td align="center"><%#CheckLost(Eval("UserName").ToString())%></td>
              <td align="center"><%#CheckLates(Eval("UserName").ToString())%></td>
            </tr>
          </ItemTemplate>
        </asp:Repeater>
      </table>
      <input type="hidden" id="HidDays" runat="server" />
    </div>
  </div>
</form>
</body>
</html>