<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TlpDP.ascx.cs" Inherits="ZoomLaCMS.Manage.I.ASCX.TlpDP" %>
<div style="display: none;">
    <ul id="templist_ul">
        <asp:Repeater ID="TempList_RPT" runat="server" EnableViewState="false">
            <ItemTemplate>
                <li><%#GetFileInfo() %></li>
            </ItemTemplate>
        </asp:Repeater>
    </ul>
</div>
