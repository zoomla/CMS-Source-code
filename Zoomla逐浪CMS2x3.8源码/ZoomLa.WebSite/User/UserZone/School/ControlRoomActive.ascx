<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ControlRoomActive.ascx.cs"
    Inherits="User_UserZone_School_ControlRoomActive" %>
<table width="500px" cellpadding="0" cellspacing="0" border="0" style="height: 150px">
    <tr style="height: 2%">
        <td valign="top">
            <strong>班级活动</strong></td>
        <td align="right" valign="top">
            <a href="RoomActiveList.aspx?Roomid=<%=RoomID %>">查看更多</a></td>
    </tr>
    <tr>
        <td colspan="2" id="tdr" runat ="server" valign="top">
            <asp:Repeater ID="Repeater1" runat="server">
                <ItemTemplate>
                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                        <tr>
                            <td valign="top">
                                <%#DataBinder.Eval(Container.DataItem, "ActiveTitle")%>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <%#GetDate(DataBinder.Eval(Container.DataItem, "ActiveStateTime").ToString(),DataBinder.Eval(Container.DataItem, "ActiveEndTime").ToString())%>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <%#GetStr(DataBinder.Eval(Container.DataItem, "ActiveContext").ToString(), DataBinder.Eval(Container.DataItem, "AID").ToString())%>
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
            </asp:Repeater>
        </td>
    </tr>
</table>
