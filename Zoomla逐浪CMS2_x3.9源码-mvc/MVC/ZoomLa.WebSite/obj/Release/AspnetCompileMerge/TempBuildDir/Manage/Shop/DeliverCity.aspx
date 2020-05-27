<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DeliverCity.aspx.cs" Inherits="ZoomLaCMS.Manage.Shop.DeliverCity" MasterPageFile="~/Manage/I/Default.master"%>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>确定默认地区</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-striped table-bordered table-hover">
    <tr class="tdbg">
      <td height="24" colspan="2" align="center" class="title">确定默认地区</td>
    </tr>
    <tr class="tdbg">
      <td width="24%" height="24" class="tdbg" align="center"><strong>默认城市</strong></td>
      <td width="76%" height="24" class="tdbg">
          <asp:DropDownList ID="tx_city" runat="server">
          </asp:DropDownList>
      </td>
    </tr>                 
    <tr>
      <td height="24" colspan="2" align="center"><asp:Button ID="Button1" class="btn btn-primary"  Text="保存" runat="server" onclick="Button1_Click" /></td>
    </tr>
</table>
</asp:Content>