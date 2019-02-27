<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ApprovalList.aspx.cs" Inherits="MIS_Target_ApprovalList" %>
<!DOCTYPE html>
<html>
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<title>审批</title>
</head>
<body>
<form id="ApformL1" runat="server">
<div class="left_ico"><img src="../../App_Themes/UserThem/images/Mis/jian.jpg" /></div>
<div class="left_Pro">
<a href="javascript:void(0)" onclick="Prolist('<%=Request["ID"] %>','4')">审批</a>
</div>
<div class="Right_Pro"> 
    <table> 
    <asp:Repeater ID="Repeater3" runat="server">
    <ItemTemplate>
    <tr><td><a href="../Approval/Default.aspx?MID=<%#Eval("ID")%>"><%#Eval("Content") %> </a></td></tr>
    </ItemTemplate>
    </asp:Repeater> 
    </table>
</div>
</form>
</body>
</html>
