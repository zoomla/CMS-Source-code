<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WebUserControlCollect.ascx.cs" Inherits="WebUserControlCollect" %>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <td>
        
        <asp:Label ID="Label1" runat="server"></asp:Label>
        <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click1"
         Visible="false">删除</asp:LinkButton></td>
  </tr> 
</table>
<asp:HiddenField ID="Hfid" runat="server" OnValueChanged="Hfid_ValueChanged" />
