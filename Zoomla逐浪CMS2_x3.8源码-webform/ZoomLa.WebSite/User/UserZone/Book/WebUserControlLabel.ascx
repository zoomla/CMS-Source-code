<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WebUserControlLabel.ascx.cs" Inherits="WebUserControlLabel" %>
<table width="100%" height="41" border="0" cellpadding="0" cellspacing="0">
  <tr>
    <td><strong><font color="#663300">标签汇总</font></strong></td>
  </tr>
  <tr>
    <td valign="top"><asp:DataList ID="DataList1" runat="server" RepeatColumns="3" RepeatDirection="Horizontal">
            <ItemTemplate>
                <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# ((string)Container.DataItem)%>' OnClick="LinkButton1_Click" CausesValidation="False"><%# ((string)Container.DataItem) %></asp:LinkButton>&nbsp;
            </ItemTemplate>
        </asp:DataList></td>
  </tr>
</table>