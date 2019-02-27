<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ControlAuditingMemberList.ascx.cs" Inherits="User_UserZone_School_ControlAuditingMemberList" %>
<table style="height:150px">
<tr  style=" height:5%">
<td valign="top"><strong>新申请用户</strong></td>
</tr>
<tr  style=" height:5%">
<td valign="top">待审核人数：<asp:Label ID="Label1" runat="server" Text=""></asp:Label>人</td>
</tr>
<tr style=" height:90%" valign="top">
<td>
    <asp:DataList ID="DataList1" runat="server">
    <HeaderTemplate>
    <table>
    <tr>
    <td align="center">用户名</td><td align="center">申请时间</td>
    </tr>
    </HeaderTemplate>
    <ItemTemplate>
    <tr>
    <td><%#DataBinder.Eval (Container.DataItem ,"UserName") %></td><td><%#DataBinder.Eval (Container.DataItem ,"AddTime") %></td>
    </tr>
    </ItemTemplate>
    <FooterTemplate>
    </table>
    </FooterTemplate>
    </asp:DataList>
</td>
</tr>
</table>