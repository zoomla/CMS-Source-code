<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ControlProblemTop.ascx.cs"
    Inherits="User_UserZone_School_ControlProblemTop" %>
<table width="500px" cellpadding="0" cellspacing="0" border="0" style="height: 50px">
    <tr style=" height:5%">
        <td valign="top">
            <strong>  提问</strong></td>
        <td align="right" valign="top">
            <a href="ShowProblemList.aspx?Roomid=<%=RoomID %>">查看更多</a></td>
    </tr>
    <tr>
        <td colspan="2" valign="top" style=" height:95%"> 
            <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
            <table width="100%" cellpadding="0" cellspacing="0" border="0">
            <tr>
            <td><a href='ShowProblem.aspx?Pid=<%#DataBinder.Eval(Container.DataItem,"ID") %>'><%#DataBinder.Eval(Container.DataItem, "ProblemTitle")%></a></td><td style="width:120px"><%#DataBinder.Eval(Container.DataItem, "AddTime")%></td>
            </tr>
            </table>
            </ItemTemplate>
            </asp:Repeater>
        </td>
    </tr>
</table>
