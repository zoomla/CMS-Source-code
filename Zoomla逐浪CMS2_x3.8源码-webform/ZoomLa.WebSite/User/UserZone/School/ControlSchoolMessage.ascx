<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ControlSchoolMessage.ascx.cs"
    Inherits="User_UserZone_School_ControlSchoolMessage" %>
<table width="500px">
    <tr>
        <td>
            <strong>留言板</strong></td>
        <td align="right">
            <a href='ShowSchoolMessage.aspx?Roomid=<%=Roomid %>'>所有留言</a></td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:TextBox ID="txtMsg" runat="server" TextMode="MultiLine" Width="100%" Rows="4"
                MaxLength="2000"></asp:TextBox></td>
    </tr>
    <tr>
        <td>
            <asp:Button ID="Button1" runat="server" Text="留 言" OnClick="Button1_Click" /></td>
        <td>
        </td>
    </tr>
</table>
