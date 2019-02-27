<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ControlNotifyTop.ascx.cs"
    Inherits="User_UserZone_School_ControlNotifyTop" %>
<table width="500px" style="height: 50px">
    <tr style=" height:5%">
        <td>
        </td>
        <td valign="top">
            <strong>班级黑板报</strong></td>
        <td align="right" valign="top">
            <a href="RoomNotifyList.aspx?RoomID=<%=RoomID %>">查看更多</a></td>
    </tr>
    <tr style=" height:5%">
        <td>
        </td>
        <td colspan="2" valign="top">
            <table width="100%">
                <tr>
                    <td id="tdTitle" runat="server" colspan="2">
                    </td>
                    <td id="tdTime" runat="server" colspan="2" align="right">
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr style=" height:90%">
        <td>
        </td>
        <td id="tdContext" runat="server" valign="top" colspan="2">
        </td>
    </tr>
</table>
