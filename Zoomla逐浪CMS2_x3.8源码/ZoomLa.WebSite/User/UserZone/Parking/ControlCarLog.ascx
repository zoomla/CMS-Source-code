<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ControlCarLog.ascx.cs" Inherits="User_UserZone_Parking_ControlCarLog" %>
<asp:Repeater ID="Repeater1" runat="server">
<HeaderTemplate>
<table width="100%">
<tr>
<td><h4>我的记录</h4></td>
<td align="right"><a href="#">更多...</a></td>
</tr>
</table>
<hr />
</HeaderTemplate>
<ItemTemplate>
<table width="100%">
<tr>
<td>我<%#DataBinder.Eval(Container.DataItem, "P_content")%></td>
<td width="20%" align="right"><%#DataBinder.Eval(Container.DataItem, "P_introtime")%></td>
</tr>
</table>
</ItemTemplate>
</asp:Repeater>