<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ServiceInfo.aspx.cs" Inherits="ZoomLaCMS.Manage.User.Service.ServiceInfo" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>客服信息</title>
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="Content">
  <div style="white-space:normal">
    <asp:Repeater ID="rpServiceInfo" runat="server" >
      <ItemTemplate> <%#DataBinder.Eval(Container.DataItem,"") %> </ItemTemplate>
    </asp:Repeater>
  </div>
  <table class="table table-striped table-bordered table-hover">
    <tr class="tdbg">
      <td align="center"> 共
        <asp:Label ID="Allnum" runat="server" Text=""></asp:Label>
        &nbsp;
        <asp:Label ID="Toppage" runat="server" Text=""></asp:Label>
        <asp:Label ID="Nextpage" runat="server" Text=""></asp:Label>
        <asp:Label ID="Downpage" runat="server" Text=""></asp:Label>
        <asp:Label ID="Endpage" runat="server" Text=""></asp:Label>
        页次：
        <asp:Label ID="Nowpage" runat="server" Text=""></asp:Label>
        /
        <asp:Label ID="PageSize" runat="server" Text=""></asp:Label>
        页
        <asp:Label ID="pagess" runat="server" Text=""></asp:Label>
        个/页 转到第
        <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True"> </asp:DropDownList>
        页</td>
    </tr>
  </table>
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">

</asp:Content>
