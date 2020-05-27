<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TlpDP.ascx.cs" Inherits="test_TlpDP" %>
    <div style="display:none;">
        <ul id="templist_ul">
            <asp:Repeater ID="TempList_RPT" runat="server" EnableViewState="false">
                <ItemTemplate>
                    <li><%#GetFileInfo() %></li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>
    </div>