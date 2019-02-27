<%@ Page Language="C#" AutoEventWireup="true" CodeFile="mailList.aspx.cs" Inherits="MIS_Target_mailList" %>
<!DOCTYPE html>
<html>
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>邮件</title>
</head>
<body>
<form id="mformL1" runat="server">
<div id="">
<div class="left_ico"><img src="../../App_Themes/UserThem/images/Mis/jian.jpg" /></div>
<div class="left_Pro">
<a href="javascript:void(0)" onclick="Prolist('<%=Request["ID"] %>','4')">邮件</a>
</div>
<div class="Right_Pro">
<table> 
<asp:Repeater ID="Repeater3" runat="server">
<ItemTemplate>
<tr><td>  <a href="../Memo/MailView.aspx?MID=<%#Eval("ID") %>"><%#Eval("MailTitle") %> </a></td></tr>
</ItemTemplate>
</asp:Repeater> 
</table>
</div></div>
</form>
</body>
</html>
